using IoT.Lib.ECS;
using System;
using System.Diagnostics;
using Xunit;

namespace IoT.Lib.Tests
{
    public class DeDeurIntegrationTests : IDisposable
    {
        private const string IpAddress = "10.27.37.242";
        private const string DeviceId = "dedeur-sam100reader-test";

        private readonly ISamReader SamReader = null;

        public DeDeurIntegrationTests()
        {
            NetworkHostAddress cfg = $"{IpAddress}:310";
            ISamHost host = new SamHost(new TestLogProvider<SamHost>());
            host.Connect(cfg);

            SamReader = new Sam100Reader(host);
        }

        public void Dispose()
        {
            (SamReader as IDisposable)?.Dispose();
        }

        [Fact]
        public void ReadConfig()
        {
            var config = SamReader.GetConfig().ToPortalParameters(DeviceId);

            Assert.NotEmpty(config);
        }

        [Fact]
        public void ReadAlarms()
        {
            var alarms = SamReader.GetAlarms().ToPortalParameters(DeviceId);

            Assert.NotEmpty(alarms);
        }

        [Fact]
        public void ReadAnalogValues()
        {
            var analog = SamReader.GetAnalogValues().ToPortalParameters(DeviceId);

            Assert.NotEmpty(analog);
        }

    }
}
