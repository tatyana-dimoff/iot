using IoTnxt.DAPI.RedGreenQueue.Abstractions;
using IoTnxt.DAPI.RedGreenQueue.Adapter;
using IoTnxt.DAPI.RedGreenQueue.Extensions;
using IoTnxt.DAPI.RedGreenQueue.Proxy;
using IoTnxt.Gateway.API.Abstractions;
using IoTnxt.RedGreenQueue;
using IoTnxt.RedGreenQueue.Abstractions.Exceptions;
using IoT.Lib;
using IoT.Lib.DTO;
using IoT.Lib.ECS;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IoT.CLI
{
    internal class Program
    {
        private const int SensorDataRefreshIn = 1000;
        private const string GatewayId = "SAM-POWERED-SITES";
        //private const string GatewayId = "sam-equipped-sites";
        private const string TenantId = "t000000018";

        public static string SecretKey = Convert.ToBase64String(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(GatewayId)));
        private static IRedGreenQueueAdapter RedQ = null;
        private static ILogger Journal = null;
        private static ServiceProvider SP = null;

        private static CancellationTokenSource CTS = new CancellationTokenSource();

        private static readonly Dictionary<string, DeviceProperty> sam100DeviceProperties = new Dictionary<string, DeviceProperty>
        {
            [nameof(Sam100AlarmsDto.AccessControl)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam100AlarmsDto.AccessControl)}" },
            [nameof(Sam100AlarmsDto.AcPhaseFail)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam100AlarmsDto.AcPhaseFail)}" },
            [nameof(Sam100AlarmsDto.Aircon1Fail)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam100AlarmsDto.Aircon1Fail)}" },
            [nameof(Sam100AlarmsDto.Aircon2Fail)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam100AlarmsDto.Aircon2Fail)}" },
            [nameof(Sam100AlarmsDto.Aircon3Fail)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam100AlarmsDto.Aircon3Fail)}" },
            [nameof(Sam100AlarmsDto.Aircon1On)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam100AlarmsDto.Aircon1On)}" },
            [nameof(Sam100AlarmsDto.Aircon2On)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam100AlarmsDto.Aircon2On)}" },
            [nameof(Sam100AlarmsDto.Aircon3On)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam100AlarmsDto.Aircon3On)}" },
            [nameof(Sam100AlarmsDto.AircraftLights)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam100AlarmsDto.AircraftLights)}" },
            [nameof(Sam100AlarmsDto.Aircon1Power)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam100AlarmsDto.Aircon1Power)}" },
            [nameof(Sam100AlarmsDto.Aircon2Power)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam100AlarmsDto.Aircon2Power)}" },
            [nameof(Sam100AlarmsDto.Aircon3Power)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam100AlarmsDto.Aircon3Power)}" },
            [nameof(Sam100AlarmsDto.Aux1Alarm)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam100AlarmsDto.Aux1Alarm)}" },
            [nameof(Sam100AlarmsDto.Aux2Alarm)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam100AlarmsDto.Aux2Alarm)}" },
            [nameof(Sam100AlarmsDto.Aux3Alarm)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam100AlarmsDto.Aux3Alarm)}" },
            [nameof(Sam100AlarmsDto.Aux4Alarm)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam100AlarmsDto.Aux4Alarm)}" },
            [nameof(Sam100AlarmsDto.Aux5Alarm)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam100AlarmsDto.Aux5Alarm)}" },
            [nameof(Sam100AlarmsDto.Aux6Alarm)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam100AlarmsDto.Aux6Alarm)}" },
            [nameof(Sam100AlarmsDto.Aux7Alarm)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam100AlarmsDto.Aux7Alarm)}" },
            [nameof(Sam100AlarmsDto.Aux8Alarm)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam100AlarmsDto.Aux8Alarm)}" },
            [nameof(Sam100AlarmsDto.AuxiliaryPower)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam100AlarmsDto.AuxiliaryPower)}" },
            [nameof(Sam100AlarmsDto.BatteryIntegrity)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam100AlarmsDto.BatteryIntegrity)}" },
            [nameof(Sam100AlarmsDto.BatteryLow)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam100AlarmsDto.BatteryLow)}" },
            [nameof(Sam100AlarmsDto.ConfigCorrupted)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam100AlarmsDto.ConfigCorrupted)}" },
            [nameof(Sam100AlarmsDto.DcFanPower)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam100AlarmsDto.DcFanPower)}" },
            [nameof(Sam100AlarmsDto.DcFanOn)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam100AlarmsDto.DcFanOn)}" },
            [nameof(Sam100AlarmsDto.FreeCoolingOn)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam100AlarmsDto.FreeCoolingOn)}" },
            [nameof(Sam100AlarmsDto.EscomSupply)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam100AlarmsDto.EscomSupply)}" },
            [nameof(Sam100AlarmsDto.HighHumidity)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam100AlarmsDto.HighHumidity)}" },
            [nameof(Sam100AlarmsDto.HighTemperature)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam100AlarmsDto.HighTemperature)}" },
            [nameof(Sam100AlarmsDto.HumidityProbe1)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam100AlarmsDto.HumidityProbe1)}" },
            [nameof(Sam100AlarmsDto.HumidityProbe2)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam100AlarmsDto.HumidityProbe2)}" },
            [nameof(Sam100AlarmsDto.Intruder)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam100AlarmsDto.Intruder)}" },
            [nameof(Sam100AlarmsDto.MainsFail)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam100AlarmsDto.MainsFail)}" },
            [nameof(Sam100AlarmsDto.MovementRoom)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam100AlarmsDto.MovementRoom)}" },
            [nameof(Sam100AlarmsDto.MovementYard)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam100AlarmsDto.MovementYard)}" },
            [nameof(Sam100AlarmsDto.MultiAirconRun)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam100AlarmsDto.MultiAirconRun)}" },
            [nameof(Sam100AlarmsDto.PanicAlarm)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam100AlarmsDto.PanicAlarm)}" },
            [nameof(Sam100AlarmsDto.Pix1)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam100AlarmsDto.Pix1)}" },
            [nameof(Sam100AlarmsDto.Pix2)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam100AlarmsDto.Pix2)}" },
            [nameof(Sam100AlarmsDto.Pix3)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam100AlarmsDto.Pix3)}" },
            [nameof(Sam100AlarmsDto.Pix4)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam100AlarmsDto.Pix4)}" },
            [nameof(Sam100AlarmsDto.Pix5)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam100AlarmsDto.Pix5)}" },
            [nameof(Sam100AlarmsDto.Pix6)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam100AlarmsDto.Pix6)}" },
            [nameof(Sam100AlarmsDto.Pix7)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam100AlarmsDto.Pix7)}" },
            [nameof(Sam100AlarmsDto.Pix8)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam100AlarmsDto.Pix8)}" },
            [nameof(Sam100AlarmsDto.Pix9)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam100AlarmsDto.Pix9)}" },
            [nameof(Sam100AlarmsDto.Pix10)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam100AlarmsDto.Pix10)}" },
            [nameof(Sam100AlarmsDto.Pix11)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam100AlarmsDto.Pix11)}" },
            [nameof(Sam100AlarmsDto.Pix12)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam100AlarmsDto.Pix12)}" },
            [nameof(Sam100AlarmsDto.Pix13)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam100AlarmsDto.Pix13)}" },
            [nameof(Sam100AlarmsDto.Pix14)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam100AlarmsDto.Pix14)}" },
            [nameof(Sam100AlarmsDto.Pix15)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam100AlarmsDto.Pix15)}" },
            [nameof(Sam100AlarmsDto.Pix16)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam100AlarmsDto.Pix16)}" },
            [nameof(Sam100AlarmsDto.RectiferModule)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam100AlarmsDto.RectiferModule)}" },
            [nameof(Sam100AlarmsDto.RectiferSystem)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam100AlarmsDto.RectiferSystem)}" },
            [nameof(Sam100AlarmsDto.SmokeAlarm)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam100AlarmsDto.SmokeAlarm)}" },
            [nameof(Sam100AlarmsDto.TmaVswrFail)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam100AlarmsDto.TmaVswrFail)}" },
            [nameof(Sam100AlarmsDto.Timestamp)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam100AlarmsDto.Timestamp)}" },
            [nameof(Sam100AnalogValuesDto.Aircon1Temperature)] = new DeviceProperty { PropertyName = $"[Analog] {nameof(Sam100AnalogValuesDto.Aircon1Temperature)}" },
            [nameof(Sam100AnalogValuesDto.Aircon1TotalRuntime)] = new DeviceProperty { PropertyName = $"[Analog] {nameof(Sam100AnalogValuesDto.Aircon1TotalRuntime)}" },
            [nameof(Sam100AnalogValuesDto.MultiAirconRun)] = new DeviceProperty { PropertyName = $"[Analog] {nameof(Sam100AnalogValuesDto.MultiAirconRun)}" },
            [nameof(Sam100AnalogValuesDto.Aircon2Temperature)] = new DeviceProperty { PropertyName = $"[Analog] {nameof(Sam100AnalogValuesDto.Aircon2Temperature)}" },
            [nameof(Sam100AnalogValuesDto.Aircon2TotalRuntime)] = new DeviceProperty { PropertyName = $"[Analog] {nameof(Sam100AnalogValuesDto.Aircon2TotalRuntime)}" },
            [nameof(Sam100AnalogValuesDto.OutsideHumidity)] = new DeviceProperty { PropertyName = $"[Analog] {nameof(Sam100AnalogValuesDto.OutsideHumidity)}" },
            [nameof(Sam100AnalogValuesDto.OutsideTemp)] = new DeviceProperty { PropertyName = $"[Analog] {nameof(Sam100AnalogValuesDto.OutsideTemp)}" },
            [nameof(Sam100AnalogValuesDto.RoomHumidity)] = new DeviceProperty { PropertyName = $"[Analog] {nameof(Sam100AnalogValuesDto.RoomHumidity)}" },
            [nameof(Sam100AnalogValuesDto.RecTime)] = new DeviceProperty { PropertyName = $"[Analog] {nameof(Sam100AnalogValuesDto.RecTime)}" },
            [nameof(Sam100AnalogValuesDto.RoomTemperature)] = new DeviceProperty { PropertyName = $"[Analog] {nameof(Sam100AnalogValuesDto.RoomTemperature)}" },
            [nameof(Sam100AnalogValuesDto.TotalAirconRuntime)] = new DeviceProperty { PropertyName = $"[Analog] {nameof(Sam100AnalogValuesDto.TotalAirconRuntime)}" },
            [nameof(Sam100AnalogValuesDto.SwapCycle)] = new DeviceProperty { PropertyName = $"[Analog] {nameof(Sam100AnalogValuesDto.SwapCycle)}" },
            [nameof(Sam100AnalogValuesDto.Timestamp)] = new DeviceProperty { PropertyName = $"[Analog] {nameof(Sam100AnalogValuesDto.SwapCycle)}" },
            [nameof(Sam100ConfigDto.AirconAlarmClearTemp)] = new DeviceProperty { PropertyName = $"[Config] {nameof(Sam100ConfigDto.AirconAlarmClearTemp)}" },
            [nameof(Sam100ConfigDto.AirconDisableHysteresis)] = new DeviceProperty { PropertyName = $"[Config] {nameof(Sam100ConfigDto.AirconDisableHysteresis)}" },
            [nameof(Sam100ConfigDto.AirconEnableSetpoint)] = new DeviceProperty { PropertyName = $"[Config] {nameof(Sam100ConfigDto.AirconEnableSetpoint)}" },
            [nameof(Sam100ConfigDto.AirconRestTime)] = new DeviceProperty { PropertyName = $"[Config] {nameof(Sam100ConfigDto.AirconRestTime)}" },
            [nameof(Sam100ConfigDto.AirconSwapTimeDefault)] = new DeviceProperty { PropertyName = $"[Config] {nameof(Sam100ConfigDto.AirconSwapTimeDefault)}" },
            [nameof(Sam100ConfigDto.AirconSwapTimeHighLimit)] = new DeviceProperty { PropertyName = $"[Config] {nameof(Sam100ConfigDto.AirconSwapTimeHighLimit)}" },
            [nameof(Sam100ConfigDto.AirconSwapTimeLowLimit)] = new DeviceProperty { PropertyName = $"[Config] {nameof(Sam100ConfigDto.AirconSwapTimeLowLimit)}" },
            [nameof(Sam100ConfigDto.AirconSwapTimeSetpoint)] = new DeviceProperty { PropertyName = $"[Config] {nameof(Sam100ConfigDto.AirconSwapTimeSetpoint)}" },
            [nameof(Sam100ConfigDto.AirconTurnOnDelay)] = new DeviceProperty { PropertyName = $"[Config] {nameof(Sam100ConfigDto.AirconTurnOnDelay)}" },
            [nameof(Sam100ConfigDto.CoolingTimeout)] = new DeviceProperty { PropertyName = $"[Config] {nameof(Sam100ConfigDto.CoolingTimeout)}" },
            [nameof(Sam100ConfigDto.FailTemaperature)] = new DeviceProperty { PropertyName = $"[Config] {nameof(Sam100ConfigDto.FailTemaperature)}" },
            [nameof(Sam100ConfigDto.FanCoolingEnableTempDiff)] = new DeviceProperty { PropertyName = $"[Config] {nameof(Sam100ConfigDto.FanCoolingEnableTempDiff)}" },
            [nameof(Sam100ConfigDto.FanOnSetpoint)] = new DeviceProperty { PropertyName = $"[Config] {nameof(Sam100ConfigDto.FanOnSetpoint)}" },
            [nameof(Sam100ConfigDto.FanToAirconSwitchoverTemp)] = new DeviceProperty { PropertyName = $"[Config] {nameof(Sam100ConfigDto.FanToAirconSwitchoverTemp)}" },
            [nameof(Sam100ConfigDto.HighTempAlarmSetpoint)] = new DeviceProperty { PropertyName = $"[Config] {nameof(Sam100ConfigDto.HighTempAlarmSetpoint)}" },
            [nameof(Sam100ConfigDto.MultiRunDefault)] = new DeviceProperty { PropertyName = $"[Config] {nameof(Sam100ConfigDto.MultiRunDefault)}" },
            [nameof(Sam100ConfigDto.MultiRunHighLimit)] = new DeviceProperty { PropertyName = $"[Config] {nameof(Sam100ConfigDto.MultiRunHighLimit)}" },
            [nameof(Sam100ConfigDto.MultiRunLowLimit)] = new DeviceProperty { PropertyName = $"[Config] {nameof(Sam100ConfigDto.MultiRunLowLimit)}" },
            [nameof(Sam100ConfigDto.MultiRunOffHysteresis)] = new DeviceProperty { PropertyName = $"[Config] {nameof(Sam100ConfigDto.MultiRunOffHysteresis)}" },
            [nameof(Sam100ConfigDto.MultiRunSetpoint)] = new DeviceProperty { PropertyName = $"[Config] {nameof(Sam100ConfigDto.MultiRunSetpoint)}" },
            [nameof(Sam100ConfigDto.RegulateHysteresis)] = new DeviceProperty { PropertyName = $"[Config] {nameof(Sam100ConfigDto.RegulateHysteresis)}" },
            [nameof(Sam100ConfigDto.RegulateSetpoint)] = new DeviceProperty { PropertyName = $"[Config] {nameof(Sam100ConfigDto.RegulateSetpoint)}" },
            [nameof(Sam100ConfigDto.TemperatureTestDelay)] = new DeviceProperty { PropertyName = $"[Config] {nameof(Sam100ConfigDto.TemperatureTestDelay)}" },
            [nameof(Sam100ConfigDto.Timestamp)] = new DeviceProperty { PropertyName = $"[Config] {nameof(Sam100ConfigDto.TemperatureTestDelay)}" },
        };

        private static readonly Dictionary<string, DeviceProperty> sam205DeviceProperties = new Dictionary<string, DeviceProperty>
        {
            [nameof(Sam205AlarmsDto.Aircon1Fail)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam205AlarmsDto.Aircon1Fail)}" },
            [nameof(Sam205AlarmsDto.Aircon2Fail)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam205AlarmsDto.Aircon2Fail)}" },
            [nameof(Sam205AlarmsDto.Aircon1Power)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam205AlarmsDto.Aircon1Power)}" },
            [nameof(Sam205AlarmsDto.Aircon2Power)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam205AlarmsDto.Aircon2Power)}" },
            [nameof(Sam205AlarmsDto.FanPowerAlarm)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam205AlarmsDto.FanPowerAlarm)}" },
            [nameof(Sam205AlarmsDto.HighTemperature)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam205AlarmsDto.HighTemperature)}" },
            [nameof(Sam205AlarmsDto.HumidityHigh)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam205AlarmsDto.HumidityHigh)}" },
            [nameof(Sam205AlarmsDto.HumidityProbe)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam205AlarmsDto.HumidityProbe)}" },
            [nameof(Sam205AlarmsDto.MultiAirconRun)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam205AlarmsDto.MultiAirconRun)}" },
            [nameof(Sam205AlarmsDto.ServiceDue)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam205AlarmsDto.ServiceDue)}" },
            [nameof(Sam205AlarmsDto.VeryHighTemperature)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam205AlarmsDto.VeryHighTemperature)}" },
            [nameof(Sam205AlarmsDto.ConfigCorrupt)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam205AlarmsDto.ConfigCorrupt)}" },
            [nameof(Sam205AlarmsDto.DoorsAlarm)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam205AlarmsDto.DoorsAlarm)}" },
            [nameof(Sam205AlarmsDto.FailAlarm)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam205AlarmsDto.FailAlarm)}" },
            [nameof(Sam205AlarmsDto.ModemFail)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam205AlarmsDto.ModemFail)}" },
            [nameof(Sam205AlarmsDto.Movement)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam205AlarmsDto.Movement)}" },
            [nameof(Sam205AlarmsDto.NavLights)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam205AlarmsDto.NavLights)}" },
            [nameof(Sam205AlarmsDto.PanicAlarm)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam205AlarmsDto.PanicAlarm)}" },
            [nameof(Sam205AlarmsDto.SmokeAlarm)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam205AlarmsDto.SmokeAlarm)}" },
            [nameof(Sam205AlarmsDto.SmokeDetector)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam205AlarmsDto.SmokeDetector)}" },
            [nameof(Sam205AlarmsDto.Spare1)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam205AlarmsDto.Spare1)}" },
            [nameof(Sam205AlarmsDto.Spare2)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam205AlarmsDto.Spare2)}" },
            [nameof(Sam205AlarmsDto.Spare3)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam205AlarmsDto.Spare3)}" },
            [nameof(Sam205AlarmsDto.Spare4)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam205AlarmsDto.Spare4)}" },
            [nameof(Sam205AlarmsDto.Spare5)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam205AlarmsDto.Spare5)}" },
            [nameof(Sam205AlarmsDto.Spare6)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam205AlarmsDto.Spare6)}" },
            [nameof(Sam205AlarmsDto.Timestamp)] = new DeviceProperty { PropertyName = $"[Alarm] {nameof(Sam205AlarmsDto.Timestamp)}" },
            [nameof(Sam205AnalogValuesDto.BatteryVoltage)] = new DeviceProperty { PropertyName = $"[Analog] {nameof(Sam205AnalogValuesDto.BatteryVoltage)}" },
            [nameof(Sam205AnalogValuesDto.BatteryChargeCurrent)] = new DeviceProperty { PropertyName = $"[Analog] {nameof(Sam205AnalogValuesDto.BatteryChargeCurrent)}" },
            [nameof(Sam205AnalogValuesDto.BatteryDischargeCurrent)] = new DeviceProperty { PropertyName = $"[Analog] {nameof(Sam205AnalogValuesDto.BatteryDischargeCurrent)}" },
            [nameof(Sam205AnalogValuesDto.Humidity)] = new DeviceProperty { PropertyName = $"[Analog] {nameof(Sam205AnalogValuesDto.Humidity)}" },
            [nameof(Sam205AnalogValuesDto.Ac1Temperature)] = new DeviceProperty { PropertyName = $"[Analog] {nameof(Sam205AnalogValuesDto.Ac1Temperature)}" },
            [nameof(Sam205AnalogValuesDto.Ac2Temperature)] = new DeviceProperty { PropertyName = $"[Analog] {nameof(Sam205AnalogValuesDto.Ac2Temperature)}" },
            [nameof(Sam205AnalogValuesDto.Ac3Temperature)] = new DeviceProperty { PropertyName = $"[Analog] {nameof(Sam205AnalogValuesDto.Ac3Temperature)}" },
            [nameof(Sam205AnalogValuesDto.Ac4Temperature)] = new DeviceProperty { PropertyName = $"[Analog] {nameof(Sam205AnalogValuesDto.Ac4Temperature)}" },
            [nameof(Sam205AnalogValuesDto.Ac1TotalRuntime)] = new DeviceProperty { PropertyName = $"[Analog] {nameof(Sam205AnalogValuesDto.Ac1TotalRuntime)}" },
            [nameof(Sam205AnalogValuesDto.Ac2TotalRuntime)] = new DeviceProperty { PropertyName = $"[Analog] {nameof(Sam205AnalogValuesDto.Ac2TotalRuntime)}" },
            [nameof(Sam205AnalogValuesDto.RoomTemperature)] = new DeviceProperty { PropertyName = $"[Analog] {nameof(Sam205AnalogValuesDto.RoomTemperature)}" },
            [nameof(Sam205AnalogValuesDto.Ac1Rtss)] = new DeviceProperty { PropertyName = $"[Analog] {nameof(Sam205AnalogValuesDto.Ac1Rtss)}" },
            [nameof(Sam205AnalogValuesDto.Ac2Rtss)] = new DeviceProperty { PropertyName = $"[Analog] {nameof(Sam205AnalogValuesDto.Ac2Rtss)}" },
            [nameof(Sam205AnalogValuesDto.Timestamp)] = new DeviceProperty { PropertyName = $"[Analog] {nameof(Sam205AnalogValuesDto.Timestamp)}" },
            [nameof(Sam205ConfigDto.AirconAlarmClearThreshold)] = new DeviceProperty { PropertyName = $"[Config] {nameof(Sam205ConfigDto.AirconAlarmClearThreshold)}" },
            [nameof(Sam205ConfigDto.AirconFailTemp)] = new DeviceProperty { PropertyName = $"[Config] {nameof(Sam205ConfigDto.AirconFailTemp)}" },
            [nameof(Sam205ConfigDto.AirconRestTime)] = new DeviceProperty { PropertyName = $"[Config] {nameof(Sam205ConfigDto.AirconRestTime)}" },
            [nameof(Sam205ConfigDto.AirconsDisableHysteresis)] = new DeviceProperty { PropertyName = $"[Config] {nameof(Sam205ConfigDto.AirconsDisableHysteresis)}" },
            [nameof(Sam205ConfigDto.AirconsEnable)] = new DeviceProperty { PropertyName = $"[Config] {nameof(Sam205ConfigDto.AirconsEnable)}" },
            [nameof(Sam205ConfigDto.AirconsHumidityOff)] = new DeviceProperty { PropertyName = $"[Config] {nameof(Sam205ConfigDto.AirconsHumidityOff)}" },
            [nameof(Sam205ConfigDto.AirconsHumidityOn)] = new DeviceProperty { PropertyName = $"[Config] {nameof(Sam205ConfigDto.AirconsHumidityOn)}" },
            [nameof(Sam205ConfigDto.AirconServiceInterval)] = new DeviceProperty { PropertyName = $"[Config] {nameof(Sam205ConfigDto.AirconServiceInterval)}" },
            [nameof(Sam205ConfigDto.AirconSwapTime)] = new DeviceProperty { PropertyName = $"[Config] {nameof(Sam205ConfigDto.AirconSwapTime)}" },
            [nameof(Sam205ConfigDto.AirconTempTestDelay)] = new DeviceProperty { PropertyName = $"[Config] {nameof(Sam205ConfigDto.AirconTempTestDelay)}" },
            [nameof(Sam205ConfigDto.AirconTurnOnDelay)] = new DeviceProperty { PropertyName = $"[Config] {nameof(Sam205ConfigDto.AirconTurnOnDelay)}" },
            [nameof(Sam205ConfigDto.EmergencyFanOn)] = new DeviceProperty { PropertyName = $"[Config] {nameof(Sam205ConfigDto.EmergencyFanOn)}" },
            [nameof(Sam205ConfigDto.FanCoolingEnableTempDiff)] = new DeviceProperty { PropertyName = $"[Config] {nameof(Sam205ConfigDto.FanCoolingEnableTempDiff)}" },
            [nameof(Sam205ConfigDto.FanEnableHumidityDiff)] = new DeviceProperty { PropertyName = $"[Config] {nameof(Sam205ConfigDto.FanEnableHumidityDiff)}" },
            [nameof(Sam205ConfigDto.FanToAirconSwitchoverTemp)] = new DeviceProperty { PropertyName = $"[Config] {nameof(Sam205ConfigDto.FanToAirconSwitchoverTemp)}" },
            [nameof(Sam205ConfigDto.HighTempAlarm)] = new DeviceProperty { PropertyName = $"[Config] {nameof(Sam205ConfigDto.HighTempAlarm)}" },
            [nameof(Sam205ConfigDto.HumidityAlarmClearTheshold)] = new DeviceProperty { PropertyName = $"[Config] {nameof(Sam205ConfigDto.HumidityAlarmClearTheshold)}" },
            [nameof(Sam205ConfigDto.HumidityAlarmTriggerTheshold)] = new DeviceProperty { PropertyName = $"[Config] {nameof(Sam205ConfigDto.HumidityAlarmTriggerTheshold)}" },
            [nameof(Sam205ConfigDto.HybridAirconRunTime)] = new DeviceProperty { PropertyName = $"[Config] {nameof(Sam205ConfigDto.HybridAirconRunTime)}" },
            [nameof(Sam205ConfigDto.MultiAirconRun)] = new DeviceProperty { PropertyName = $"[Config] {nameof(Sam205ConfigDto.MultiAirconRun)}" },
            [nameof(Sam205ConfigDto.MultiAirconRunOffHysteresis)] = new DeviceProperty { PropertyName = $"[Config] {nameof(Sam205ConfigDto.MultiAirconRunOffHysteresis)}" },
            [nameof(Sam205ConfigDto.RoomTempRegulateHysteresis)] = new DeviceProperty { PropertyName = $"[Config] {nameof(Sam205ConfigDto.RoomTempRegulateHysteresis)}" },
            [nameof(Sam205ConfigDto.RoomTempRegulate)] = new DeviceProperty { PropertyName = $"[Config] {nameof(Sam205ConfigDto.RoomTempRegulate)}" },
            [nameof(Sam205ConfigDto.SiteName)] = new DeviceProperty { PropertyName = $"[Config] {nameof(Sam205ConfigDto.SiteName)}" },
            [nameof(Sam205ConfigDto.Timestamp)] = new DeviceProperty { PropertyName = $"[Config] {nameof(Sam205ConfigDto.SiteName)}" },
            [nameof(Sam205ConfigDto.VeryHighTempAlarm)] = new DeviceProperty { PropertyName = $"[Config] {nameof(Sam205ConfigDto.VeryHighTempAlarm)}" },
        };

        private static Dictionary<string, Device> GatewayDevices = null;

        private static void Main(string[] args)
        {
            const string DeDeurDeviceId = "dedeur-sam100";
            const string DeBakkeDeviceId = "debakke-sam100";
            const string EvantonDeviceId = "evanton-sam205";

            Dictionary<string, (string DeviceId, EcsModel DeviceModel, NetworkHostAddress HostAddress)> config = new Dictionary<string, (string, EcsModel, NetworkHostAddress)>
            {
                ["Evanton"] = (EvantonDeviceId, EcsModel.Sam205, "10.27.50.194:301"),
                ["DeDeur"] = (DeDeurDeviceId, EcsModel.Sam100, "10.27.37.242:310"),
                ["DeBakke"] = (DeBakkeDeviceId, EcsModel.Sam100, "10.9.205.42:310")
            };


            var services = new ServiceCollection();

            services.AddOptions();
            services.AddLogging(lb => lb.AddConsole().SetMinimumLevel(LogLevel.Trace));

            services.AddDapiRedGreenQueueProxy().AddSingletonProxy<IGatewayApi>();

            services.AddDapiRedGreenQueue(new ConfigurationBuilder().Build());
            services.AddDapiRedGreenQueueProxy().AddSingletonProxy<IGatewayApi>();

            //services.AddTransient<ISam205Controller, Sam205Reader>();
            //services.AddTransient<ISam100Controller, Sam100Reader>();
            //services.AddTransient<ISamHost, SamHost>();
            services.AddTransient(typeof(ILogProvider<>), typeof(MsLogger<>));

            services.AddSingleton<ISamReaderBuilder, SamReaderBuilder>();

            services.Configure<RedGreenQueueAdapterOptions>(cfg =>
            {
                cfg.LogSends = true;
                cfg.GreenQueueOptions = new GreenQueueOptions
                {
                    Hosts = new List<string> { "greenqueue.prod.iotnxt.io" },
                    ServiceUniqueId = GatewayId,
                    SecretKey = SecretKey,
                    publicKeyAsXml =
                        "<RSAKeyValue><Exponent>AQAB</Exponent><Modulus>rbltknM3wO5/TAEigft0RDlI6R9yPttweDXtmXjmpxwcuVtqJgNbIQ3VduGVlG6sOg20iEbBWMCdwJ3HZTrtn7qpXRdJBqDUNye4Xbwp1Dp+zMFpgEsCklM7c6/iIq14nymhNo9Cn3eBBM3yZzUKJuPn9CTZSOtCenSbae9X9bnHSW2qB1qRSQ2M03VppBYAyMjZvP1wSDVNuvCtjU2Lg/8o/t231E/U+s1Jk0IvdD6rLdoi91c3Bmp00rVMPxOjvKmOjgPfE5LESRPMlUli4kJFWxBwbXuhFY+TK2I+BUpiYYKX+4YL3OFrn/EpO4bNcI0NHelbWGqZg57x7rNe9Q==</Modulus></RSAKeyValue>"
                };
            });
            services.Configure<DapiRedGreenQueueProxyOptions>(cfg => cfg.Partition = "IOTNXT.DEFAULT");

            SP = services.BuildServiceProvider();

            RedQ = SP.GetService<IRedGreenQueueAdapter>();

            Journal = SP.GetService<ILogger<Program>>();

            var gwApi = SP.GetService<IGatewayApi>();

            GatewayDevices = new Dictionary<string, Device>(config.Count);
            foreach (var site in config)
            {
                var d = new Device
                {
                    DeviceName = site.Value.DeviceId,
                    DeviceType = site.Value.DeviceModel.ToString().ToUpper(),
                    Properties = sam205DeviceProperties
                };
                GatewayDevices.Add(site.Key, d);
            }

            RegisterGateway(gwApi);

            var builder = SP.GetService<ISamReaderBuilder>();

            var runningTasks = new Dictionary<string, Task>(config.Count);
            foreach (var site in config)
            {
                var t = Task.Run(async () =>
                {
                    ISamReader reader = null;

                    if (site.Value.DeviceModel == EcsModel.Sam100)
                    {
                        reader = builder.BuildRemoteSam100Reader(site.Value.HostAddress);
                    }
                    else if (site.Value.DeviceModel == EcsModel.Sam205)
                    {
                        reader = builder.BuildRemoteSam205Reader(site.Value.HostAddress);
                    }
                    else
                    {
                        throw new NotSupportedException($"Invalid or unsupported device model: {site.Value.DeviceModel}");
                    }

                    await GatherTelemetry(reader, site.Value.DeviceId);
                });

                runningTasks.Add(site.Key, t);
            }

            while (true)
            {
                var command = Console.ReadKey(true);

                if ((command.Modifiers == ConsoleModifiers.Control) && (command.Key == ConsoleKey.Q))
                {
                    CTS.Cancel();
                    try
                    {
                        Task.WaitAll(runningTasks.Select(kvp => kvp.Value).ToArray());
                    }
                    catch (AggregateException ae) when (ae.InnerException is TaskCanceledException && CTS.IsCancellationRequested)
                    {
                        // swallow
                    }
                    catch (AggregateException ae)
                    {
                        foreach (var ex in ae.InnerExceptions)
                        {
                            Journal.LogError(ex.ToString());
                        }

                        if (Debugger.IsAttached)
                        {
                            Debugger.Break();
                        }
                    }

                    break;
                }
            }
        }

        private static void RegisterGateway(IGatewayApi api)
        {
            var gw = new Gateway.API.Abstractions.Gateway
            {
                GatewayId = GatewayId,
                Secret = SecretKey,
                Make = "IoTnxt",
                Model = "VodacomEcs",
                FirmwareVersion = "1.1.0",
                Devices = GatewayDevices
            };

            api.RegisterGatewayFromGatewayAsync(gw);
        }

        private static async Task ForwardTelemetry(IEnumerable<(string, string, object)> telemetry)
        {
            if (telemetry?.Any() ?? false)
            {
                try
                {
                    await RedQ.SendGateway1NotificationAsync(TenantId, GatewayId, DateTime.UtcNow, null, null, DateTime.UtcNow, true, false, telemetry.ToArray());
                }
                catch (QueuePublishException qpe)
                {
                    Journal.LogError(qpe, "Failed to send telemetry to the queue.");
                }
                catch (OperationCanceledException oce)
                {
                    Journal.LogError(oce, "Failed to send telemetry to the queue.");
                }
                catch (AggregateException ae) when (ae.InnerException is TaskCanceledException && CTS.IsCancellationRequested)
                {
                    Journal.LogError(ae, "Failed to send telemetry to the queue.");
                }
            }
        }

        private static async Task StartSam205(NetworkHostAddress hostAddress, string deviceId)
        {
            while (!CTS.IsCancellationRequested)
            {
                var cntrlr = SP.GetService<ISamController>();

                ISamReader sam205 = new Sam205Reader(cntrlr);

                var telemetry = sam205.GetConfig().ToPortalParameters(deviceId);

                await ForwardTelemetry(telemetry);

                while (!CTS.IsCancellationRequested)
                {
                    telemetry = Enumerable.Empty<(string, string, object)>();

                    try
                    {
                        telemetry = sam205.GetAnalogValues().ToPortalParameters(deviceId);
                        telemetry = telemetry.Concat(sam205.GetAlarms().ToPortalParameters(deviceId));
                        telemetry = telemetry.Concat(sam205.GetConfig().ToPortalParameters(deviceId));
                        await ForwardTelemetry(telemetry);
                    }
                    catch (InvalidDataException ide)
                    {
                        Journal.LogError($"{deviceId}: Failed to read from the SAM205 device");
                        Journal.LogError(ide.ToString());
                    }
                    catch (SocketException se)
                    {
                        Journal.LogError(se, $"{deviceId}: Connection failed.");
                        break;
                    }
                    catch (Exception ex)
                    {
                        Journal.LogError(ex, $"{deviceId}: Uncaught exception.");

                        break;
                    }

                    await Task.Delay(SensorDataRefreshIn, CTS.Token);
                }
            }
        }

        private static async Task StartSam100(NetworkHostAddress hostAddress, string deviceId)
        {
            while (!CTS.IsCancellationRequested)
            {
                var sam100 = SP.GetService<ISamReader>();

                var telemetry = sam100.GetConfig().ToPortalParameters(deviceId);

                await ForwardTelemetry(telemetry);

                using (var trash = sam100 as IDisposable)
                {
                    while (!CTS.IsCancellationRequested)
                    {
                        telemetry = Enumerable.Empty<(string, string, object)>();

                        try
                        {
                            telemetry = sam100.GetAnalogValues().ToPortalParameters(deviceId);
                            telemetry = telemetry.Concat(sam100.GetAlarms().ToPortalParameters(deviceId));
                            telemetry = telemetry.Concat(sam100.GetConfig().ToPortalParameters(deviceId));
                            await ForwardTelemetry(telemetry);
                        }
                        catch (InvalidDataException ide)
                        {
                            Journal.LogError($"{deviceId}: Failed to read from the SAM100 device.");
                            Journal.LogError(ide.ToString());
                        }
                        catch (SocketException se)
                        {
                            Journal.LogError(se, $"{deviceId}: Connection failed.");
                            break;
                        }
                        catch (Exception ex)
                        {
                            Journal.LogError(ex, $"{deviceId}: Uncaught exception");

                            break;
                        }

                        await Task.Delay(SensorDataRefreshIn);
                    }
                }
            }
        }

        private static async Task GatherTelemetry(ISamReader samReader, string deviceId)
        {
            while (!CTS.IsCancellationRequested)
            {
                var telemetry = samReader.GetConfig().ToPortalParameters(deviceId);

                await ForwardTelemetry(telemetry);

                using (var trash = samReader as IDisposable)
                {
                    while (!CTS.IsCancellationRequested)
                    {
                        telemetry = Enumerable.Empty<(string, string, object)>();

                        try
                        {
                            telemetry = samReader.GetAnalogValues().ToPortalParameters(deviceId);
                            telemetry = telemetry.Concat(samReader.GetAlarms().ToPortalParameters(deviceId));
                            telemetry = telemetry.Concat(samReader.GetConfig().ToPortalParameters(deviceId));
                            await ForwardTelemetry(telemetry);
                        }
                        catch (InvalidDataException ide)
                        {
                            Journal.LogError(ide, $"{deviceId}: Data mismatch.");
                        }
                        catch (Exception ex)
                        {
                            Journal.LogError(ex, $"{deviceId}: Uncaught exception");

                            break;
                        }

                        await Task.Delay(SensorDataRefreshIn);
                    }
                }
            }
        }

    }
}



