using ITG.Brix.Diagnostics.Guards;
using ITG.Brix.Diagnostics.Logging.Abstractions;
using ITG.Brix.Diagnostics.Logging.Extensions;
using Microsoft.Extensions.Logging;
using System;

namespace ITG.Brix.Diagnostics.Logging
{
    public class LogAs : ILogAs
    {
        private readonly ILogger _logger;

        public LogAs(ILogger logger)
        {
            Guard.On(logger, Exceptions.Error.LoggerShouldNotBeNull()).AgainstNull();

            _logger = logger;
        }

        public void Info(string message)
        {
            _logger.LogInformation(message);
        }

        public void Error(Exception exception)
        {
            _logger.LogInformation(exception, exception.Message);
        }

        public void Error(string message, Exception exception)
        {
            _logger.LogInformation(exception, message);
        }

        public void Exception(Exception exception)
        {
            _logger.LogError(exception, exception.Message);
        }

        public void Critical(string message, Exception exception)
        {
            _logger.LogCritical(exception, message);
        }
    }
}
