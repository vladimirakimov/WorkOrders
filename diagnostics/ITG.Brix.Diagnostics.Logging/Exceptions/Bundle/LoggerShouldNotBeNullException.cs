using System;

namespace ITG.Brix.Diagnostics.Logging.Exceptions
{
    public sealed class LoggerShouldNotBeNullException : Exception
    {
        internal LoggerShouldNotBeNullException() : base(ExceptionMessage.LoggerShouldNotBeNull) { }
    }
}
