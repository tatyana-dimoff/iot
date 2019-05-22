using IoT.Lib;
using IoT.Lib.ECS;

namespace IoT.CLI
{
    internal interface ISamReaderBuilder
    {
        ISamReader BuildRemoteSam100Reader(INetworkAddress networkAddress);

        ISamReader BuildRemoteSam205Reader(INetworkAddress networkAddress);
    }
}
