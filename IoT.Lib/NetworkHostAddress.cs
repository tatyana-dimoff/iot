using System.Linq;

namespace IoT.Lib
{
    public class NetworkHostAddress : INetworkAddress
    {
        public static implicit operator NetworkHostAddress(string exp)
        {
            var result = new NetworkHostAddress { Host = exp };

            if (!string.IsNullOrWhiteSpace(exp))
            {
                var parts = exp.Split(':');
                if (parts.Count() > 1)
                {
                    result.Host = parts[0];
                    var portNumber = 0;
                    if (int.TryParse(parts[1], out portNumber))
                    {
                        result.Port = portNumber;
                    }
                }
            }

            return result;
        }

        public static implicit operator string(NetworkHostAddress config)
        {
            var result = config?.Host;

            if (config?.Port > 0)
            {
                result += $":{config.Port}";
            }

            return result;
        }

        /// <summary>
        /// Either DNS name or IP address of the machine, hosting the SAM controller.
        /// </summary>
        public string Host { get; set; }

        public int Port { get; set; }

        public override string ToString()
        {
            return Port > 0 ? $"{Host}:{Port}" : Host;
        }
    }
}
