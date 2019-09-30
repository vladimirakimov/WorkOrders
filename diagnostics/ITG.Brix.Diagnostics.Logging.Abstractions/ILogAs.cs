using System;

namespace ITG.Brix.Diagnostics.Logging.Abstractions
{
    public interface ILogAs
    {
        void Info(string message);
        void Error(Exception exception);
        void Error(string message, Exception exception);
        void Exception(Exception exception);
        void Critical(string message, Exception exception);
    }
}
