using IoT.Lib;
using Microsoft.Extensions.Logging;
using System;

namespace IoT.CLI
{
    public class MsLogger<TCategory> : ILogProvider<TCategory>
    {
        private readonly ILogger<TCategory> Journal = null;

        public MsLogger(ILogger<TCategory> journal)
        {
            Journal = journal ?? throw new ArgumentNullException(nameof(journal));
        }

        public void Debug(string message)
        {
            Journal.LogDebug(message);
        }

        public void Error(string message)
        {
            Journal.LogError(message);
        }

        public void Error(Exception ex, string message)
        {
            Journal.LogError(ex, message);
        }

        public void Info(string message)
        {
            Journal.LogInformation(message);
        }

        public void Warning(string message)
        {
            Journal.LogWarning(message);
        }
    }
}
