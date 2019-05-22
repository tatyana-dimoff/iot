using IoTnxt.DAPI.RedGreenQueue.Abstractions;
using IoTnxt.DAPI.RedGreenQueue.Adapter;
using IoTnxt.RedGreenQueue.Abstractions.Exceptions;
using IoT.Lib;
using IoT.Lib.ECS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IoT.CLI
{
    internal class TelemetryDispatcher
    {
        private const int SensorDataRefreshIn = 1000;

        private readonly ILogProvider<TelemetryDispatcher> Log = null;

        private readonly CancellationTokenSource CTS = new CancellationTokenSource();

        private readonly IRedGreenQueueAdapter RGQ = null;
        private readonly ISamReader SAM = null;

        public TelemetryDispatcher(ILogProvider<TelemetryDispatcher> log, IRedGreenQueueAdapter rgq, ISamReader samReader)
        {
            Log = log;
            RGQ = rgq;
            SAM = samReader;
        }

        public bool IsSpinning { get; private set; }

        public void Cancel()
        {
            CTS.Cancel();
        }

        private string TenantId = null;
        private string GatewayId = null;
        private string DeviceId = null;

        public void SpinDispatch(string tenantId, string gatewayId, string deviceId)
        {
            if (IsSpinning)
            {
                throw new InvalidOperationException("This dispatcher has already been spun.");
            }

            IsSpinning = true;

            TenantId = tenantId;
            GatewayId = gatewayId;
            DeviceId = deviceId;

            Task.Run(() => GatherTelemetry());
        }

        private async Task ForwardTelemetry(IEnumerable<(string, string, object)> telemetry)
        {
            if (telemetry?.Any() ?? false)
            {
                try
                {
                    await RGQ.SendGateway1NotificationAsync(TenantId, GatewayId, DateTime.UtcNow, null, null, DateTime.UtcNow, true, false, telemetry.ToArray());
                }
                catch (QueuePublishException qpe)
                {
                    Log.Error(qpe, "Failed to send telemetry to the queue.");
                }
                catch (OperationCanceledException oce)
                {
                    Log.Error(oce, "Failed to send telemetry to the queue.");
                }
                catch (AggregateException ae) when (ae.InnerException is TaskCanceledException && CTS.IsCancellationRequested)
                {
                    Log.Error(ae, "Forwarding cancelled.");
                }
            }
        }

        private async Task GatherTelemetry()
        {
            while (!CTS.IsCancellationRequested)
            {
                var telemetry = SAM.GetConfig().ToPortalParameters(DeviceId);

                await ForwardTelemetry(telemetry);

                while (!CTS.IsCancellationRequested)
                {
                    telemetry = Enumerable.Empty<(string, string, object)>();

                    try
                    {
                        telemetry = SAM.GetAnalogValues().ToPortalParameters(DeviceId);
                        telemetry = telemetry.Concat(SAM.GetAlarms().ToPortalParameters(DeviceId));
                        telemetry = telemetry.Concat(SAM.GetConfig().ToPortalParameters(DeviceId));
                        await ForwardTelemetry(telemetry);
                    }
                    catch (InvalidDataException ide)
                    {
                        Log.Error(ide, $"{DeviceId}: Data mismatch.");
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex, $"{DeviceId}: Uncaught exception");

                        break;
                    }

                    await Task.Delay(SensorDataRefreshIn);
                }
            }
        }

    }
}
