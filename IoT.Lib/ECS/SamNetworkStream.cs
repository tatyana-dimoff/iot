using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;

namespace IoT.Lib.ECS
{
    public class SamNetworkStream : IDisposable, ISamDataStream
    {
        private readonly ILogProvider<SamNetworkStream> Log = null;

        private INetworkAddress NetworkAddress = null;
        private NetworkStream SamStream = null;
        private TcpClient SamClient = null;

        public SamNetworkStream(ILogProvider<SamNetworkStream> log)
        {
            Log = log;
        }

        public bool HasData
        {
            get { return SamStream.DataAvailable; }
        }

        public void Connect(INetworkAddress hostAddress)
        {
            if (SamStream != null)
            {
                throw new InvalidOperationException("The host is already connected.");
            }
            else if (hostAddress == null)
            {
                throw new ArgumentNullException(nameof(hostAddress));
            }
            else if (string.IsNullOrEmpty(hostAddress.Host) || hostAddress.Port == 0)
            {
                throw new ArgumentException($"Invalid host configuration. {nameof(hostAddress.Host)} must be set to valid DNS name or IP address.\nThe ${nameof(hostAddress.Port)} must be set.");
            }
            else
            {
                NetworkAddress = hostAddress;
            }

            Reconnect();
        }

        public void Reconnect()
        {
            while (true)
            {
                try
                {
                    SamClient = new TcpClient(NetworkAddress.Host, NetworkAddress.Port);
                    SamStream = SamClient.GetStream();
                    break;
                }
                catch (ObjectDisposedException)
                {
                    break;
                }
                catch (IOException ioe) when (ioe.InnerException is SocketException se)
                {
                    Thread.Sleep(2000);
                }
            }
        }

        public void Flush()
        {
            const int buffSize = 1024;

            var dumb = new byte[buffSize];

            try
            {
                while (SamStream.DataAvailable)
                {
                    SamStream.Read(dumb, 0, buffSize);
                }
            }
            catch (IOException)
            {
                /* swallow */
            }
        }

        public byte[] Read(int dataSize)
        {
            var result = new byte[dataSize];
            var incomingBytesCount = 0;

            while (true)
            {
                try
                {
                    incomingBytesCount = SamStream.Read(result, 0, dataSize);
                    break;
                }
                catch (IOException ioe)
                {
                    Log.Error(ioe, "Failed to read from the stream due to connection issues.");
                    Dispose();

                    Log.Warning("Attempting reconnect...");
                    Reconnect();
                }
            }

            if (incomingBytesCount != dataSize)
            {
                Array.Resize(ref result, incomingBytesCount);
            }

            return result;
        }

        public void Write(byte[] buff)
        {
            while (true)
            {
                try
                {
                    SamStream.Write(buff, 0, buff.Length);
                    break;
                }
                catch (IOException ioe)
                {
                    Log.Error(ioe, "Unable to write to the stream due to connection issues.");
                    Dispose();

                    Log.Warning("Attempting reconnect...");
                    Reconnect();
                }
            }
        }

        public void Dispose()
        {
            SamStream?.Dispose();
            SamClient?.Dispose();
        }
    }
}
