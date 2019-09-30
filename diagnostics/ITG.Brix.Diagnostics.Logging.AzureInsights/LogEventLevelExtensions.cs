using Microsoft.ApplicationInsights.DataContracts;
using Serilog.Events;

namespace ITG.Brix.Diagnostics.Logging.AzureInsights
{
    internal static class LogEventLevelExtensions
    {
        public static SeverityLevel? ToSeverityLevel(this LogEventLevel logEventLevel)
        {
            switch (logEventLevel)
            {
                case LogEventLevel.Verbose:
                case LogEventLevel.Debug:
                    return SeverityLevel.Verbose;
                case LogEventLevel.Information:
                    return SeverityLevel.Information;
                case LogEventLevel.Warning:
                    return SeverityLevel.Warning;
                case LogEventLevel.Error:
                    return SeverityLevel.Error;
                case LogEventLevel.Fatal:
                    return SeverityLevel.Critical;
                default:
                    return null;
            }
        }
    }
}
