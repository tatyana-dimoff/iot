using System;
using System.Collections.Generic;
using System.Text;

namespace IoT.Lib.ECS
{
    public class EcsSite
    {
        public ISamController Controller { get; private set; }

        public INetworkAddress NetworkAddress { get; private set; }
    }
}
