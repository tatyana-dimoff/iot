using IoT.Lib.ECS;
using Moq;
using System;
using System.IO;
using System.Linq;
using Xunit;
using static Moq.It;

namespace IoT.Lib.Tests
{
    public class Sam100UnitTests
    {
        private const string DeviceId = "debakke-sam100reader-test";

        private Mock<ISamHost> deviceMoq = null;

        [Fact]
        public void ReadingConfig_ShouldSucceed()
        {
            const byte dataSize = 32;

            byte[] expectedResult = new byte[dataSize];

            deviceMoq = new Mock<ISamHost>(MockBehavior.Loose);

            deviceMoq.Setup(moq => moq.Read(IsAny<byte[]>(), dataSize, IsAny<byte[]>())).Returns(expectedResult);

            var unit = new Sam100Reader(deviceMoq.Object);

            var config = unit.GetConfig().ToPortalParameters(DeviceId);

            Assert.NotEmpty(config);

            deviceMoq.VerifyAll();
        }

        [Fact]
        public void ReadingAlarms_ShouldSucceed()
        {
            const byte dataSize = 9;

            deviceMoq = new Mock<ISamHost>();

            deviceMoq.Setup(moq => moq.Read(IsAny<byte[]>(), dataSize, IsAny<byte[]>())).Returns(new byte[dataSize]);

            var unit = new Sam100Reader(deviceMoq.Object);

            var alarms = unit.GetAlarms().ToPortalParameters(DeviceId);

            Assert.NotEmpty(alarms);

            deviceMoq.VerifyAll();
        }

        [Fact]
        public void ReadingAnalogValues_ShouldSucceed()
        {
            const byte dataSize = 6;

            deviceMoq = new Mock<ISamHost>();

            var unit = new Sam100Reader(deviceMoq.Object);

            deviceMoq.Setup(moq => moq.Read(IsAny<byte[]>(), dataSize, IsAny<byte[]>())).Returns(new byte[dataSize]);

            var analog = unit.GetAnalogValues().ToPortalParameters(DeviceId);

            Assert.NotEmpty(analog);

            deviceMoq.VerifyAll();
        }


        [Fact]
        public void Reading_ShouldFail_WhenUnexpectedDataSizeIsReceived()
        {
            const byte incorrectDataSize = 0;

            deviceMoq = new Mock<ISamHost>();

            var unit = new Sam100Reader(deviceMoq.Object);

            deviceMoq.Setup(moq => moq.Read(IsAny<byte[]>(), IsAny<byte>(), IsAny<byte[]>())).Returns(new byte[incorrectDataSize]);

            Assert.Throws<InvalidDataException>(() => unit.GetConfig().ToPortalParameters(DeviceId));
            Assert.Throws<InvalidDataException>(() => unit.GetAnalogValues().ToPortalParameters(DeviceId));
            Assert.Throws<InvalidDataException>(() => unit.GetAlarms().ToPortalParameters(DeviceId));
        }

    }
}

