using System;
using System.Collections.Generic;
using System.Text;

namespace IoT.Lib.ECS
{
    public interface ISamController
    {
        void Attach(INetworkAddress networkAddress);

        byte[] Execute(byte[] commandBuff, byte requestedByteCount, byte[] responseHeader);
    }
}
