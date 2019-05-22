using IoT.Lib;
using IoT.Lib.ECS;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoT.CLI
{
    public class SamReaderBuilder : ISamReaderBuilder
    {
        public readonly ISamController Controller = null;

        public SamReaderBuilder(ISamController cntrlr)
        {
            Controller = cntrlr;
        }

        public ISamReader BuildRemoteSam100Reader(INetworkAddress networkAddress)
        {
            Controller.Attach(networkAddress);

            return new Sam100Reader(Controller);
        }

        public ISamReader BuildRemoteSam205Reader(INetworkAddress networkAddress)
        {
            Controller.Attach(networkAddress);

            return new Sam205Reader(Controller);
        }

    }
}
