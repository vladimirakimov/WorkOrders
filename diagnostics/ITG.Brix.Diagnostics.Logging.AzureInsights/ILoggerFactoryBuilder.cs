using Microsoft.Extensions.Logging;

namespace ITG.Brix.Diagnostics.Logging.AzureInsights
{
    public interface ILoggerFactoryBuilder
    {
        ILoggerFactory CreateLoggerFactory(string aiKey);
    }
}
