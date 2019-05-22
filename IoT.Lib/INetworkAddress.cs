namespace IoT.Lib
{
    public interface INetworkAddress
    {
        string Host { get; }
        int Port { get; }
    }
}