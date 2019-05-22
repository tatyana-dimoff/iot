using System;
using System.Collections.Generic;
using System.Text;

namespace IoT.Lib.ECS
{
    public interface ISamDataStream
    {
        bool HasData { get; }

        void Connect(INetworkAddress address);

        void Flush();

        byte[] Read(int dataSize);

        void Write(byte[] buff);
    }
}
