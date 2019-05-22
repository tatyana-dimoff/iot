using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace IoT.Lib.Tests
{
    public class SamHostConfigurationTests
    {
        [Fact]
        public void ImplicitFromStringConversion_ShouldSuccess()
        {
            var ipAddr = "10.34.53.111";
            var port = 654;

            NetworkHostAddress cfg = $"{ipAddr}:{port}";

            Assert.Equal(ipAddr, cfg.Host);
            Assert.Equal(port, cfg.Port);
        }
    }
}
