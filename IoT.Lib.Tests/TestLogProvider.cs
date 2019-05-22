using System;

namespace IoT.Lib.Tests
{
    internal class TestLogProvider<TCategory> : ILogProvider<TCategory>
    {
        public void Debug(string message)
        {
            System.Diagnostics.Debug.Print("DEBUG: " + message);
        }

        public void Error(string message)
        {
            System.Diagnostics.Debug.Print("ERROR: " + message); 
        }

        public void Error(Exception ex, string message)
        {
            System.Diagnostics.Debug.Print("ERROR" + message);
            System.Diagnostics.Debug.Print(ex.ToString());
        }

        public void Info(string message)
        {
            System.Diagnostics.Debug.Print("INFO: " + message);
        }

        public void Warning(string message)
        {
            System.Diagnostics.Debug.Print("WARN: " + message);
        }
    }
}
