using System;
using System.Collections.Generic;
using System.Text;

namespace IoT.Lib
{
    public interface ILogProvider<TCategory>
    {
        void Debug(string message);

        void Error(string message);

        void Error(Exception ex, string message);

        void Info(string message);

        void Warning(string message);
    }
}