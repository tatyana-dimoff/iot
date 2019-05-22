using IoT.Lib.ECS;
using System;
using Xunit;

namespace IoT.Lib.Tests
{
    public class EvantonIntegrationTests : IDisposable
    {
        private const string IpAddress = "10.27.50.194";
        private const string DeviceId = "evanton-sam205reader-test";

        private readonly ISamReader SamReader = null;

        public EvantonIntegrationTests()
        {
            NetworkHostAddress cfg = $"{IpAddress}:301";
            var host = new SamHost(new TestLogProvider<SamHost>());
            host.Connect(cfg);

            SamReader = new Sam205Reader(host);
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
