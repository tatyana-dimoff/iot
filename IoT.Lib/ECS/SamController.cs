using System;
using System.Linq;

namespace IoT.Lib.ECS
{
    public class SamController : ISamController
    {
        private ISamDataStream DataStream = null;

        public SamController(ISamDataStream dataStream)
        {
            DataStream = dataStream;
        }

        public void Attach(INetworkAddress address)
        {
            DataStream.Connect(address);
        }

        public byte[] Execute(byte[] commandBuff, byte requestedByteCount, byte[] responseHeader)
        {
            const int buffSize = 256;

            byte[] result = null;

            for (int retry = 0; retry < 6; retry++)
            {
                DataStream.Write(commandBuff);

                var headerLocated = false;

                var responseHeaderOffset = -1;
                byte[] responseBuff = null;
                var calculatedHeader = responseHeader.Concat(new byte[1] { requestedByteCount }).ToArray();

                do
                {
                    responseBuff = DataStream.Read(buffSize);

                    var incomingBytesCount = responseBuff.Length;

                    if (incomingBytesCount > 0)
                    {
                        responseHeaderOffset = -1;
                        var eof = false;
                        do
                        {
                            responseHeaderOffset++;
                            headerLocated = responseBuff.Skip(responseHeaderOffset).Take(calculatedHeader.Length).SequenceEqual(calculatedHeader);
                            eof = responseHeaderOffset + calculatedHeader.Length > responseBuff.Length;
                        } while (!headerLocated && !eof);
                    }
                } while (!headerLocated && DataStream.HasData);

                if (headerLocated)
                {
                    var responseFooterOffset = responseHeaderOffset + calculatedHeader.Length + requestedByteCount;
                    var crc = responseBuff.Skip(responseHeaderOffset).Take(calculatedHeader.Length + requestedByteCount).Sum(y => y);

                    // Verify the checksum
                    var checksumBuff = responseBuff.Skip(responseFooterOffset).Take(2).ToArray();
                    var crcPass = crc == ((checksumBuff[0] << 8) | checksumBuff[1]);

                    if (crcPass)
                    {
                        result = responseBuff.Skip(responseHeaderOffset + calculatedHeader.Length).Take(requestedByteCount).ToArray();
                        break;
                    }
                    else
                    {
                        DataStream.Flush();
                    }
                }
            }

            return result;
        }

    }
}
