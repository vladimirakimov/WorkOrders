using ITG.Brix.Diagnostics.Logging.AzureInsights.ApplicationInsights;
using ITG.Brix.Diagnostics.Logging.AzureInsights.PropertyMap;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Context;
using System.Fabric;
using System.Globalization;

namespace ITG.Brix.Diagnostics.Logging.AzureInsights
{
    /// <summary>
    /// Implementation of <see cref="ILoggerFactoryBuilder"/> 
    /// </summary>
    public class LoggerFactoryBuilder : ILoggerFactoryBuilder
    {
        private readonly ServiceContext _context;

        /// <summary>
        /// Create a new instance
        /// </summary>
        /// <param name="context">The <see cref="ServiceContext"/> of the service or actor to monitor</param>
        public LoggerFactoryBuilder(ServiceContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates an instance of <see cref="ILoggerFactory"/> that provides logging to application insights using SeriLog
        /// </summary>
        /// <param name="aiKey">The Application Insights key used for logging</param>
        /// <returns>An instance of <see cref="LoggerFactory"/></returns>
        public ILoggerFactory CreateLoggerFactory(string aiKey)
        {
            var configuration = new TelemetryConfiguration
            {
                InstrumentationKey = aiKey
            };

            new LiveStreamProvider(configuration).Enable();

            var loggerFactory = new LoggerFactory();
            var logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .Enrich.FromLogContext()
                .WriteTo.ApplicationInsights(configuration, new TypedTelemetryConverter(_context))
                .WriteTo.Console()
                .CreateLogger();

            InitContextProperties();

            loggerFactory.AddSerilog(logger, true);

            return loggerFactory;
        }

        private void InitContextProperties()
        {
            LogContext.PushProperty(ServiceContextProperties.ServiceTypeName, _context.ServiceTypeName);
            LogContext.PushProperty(ServiceContextProperties.ServiceName, _context.ServiceName);
            LogContext.PushProperty(ServiceContextProperties.PartitionId, _context.PartitionId);
            LogContext.PushProperty(ServiceContextProperties.NodeName, _context.NodeContext.NodeName);
            LogContext.PushProperty(ServiceContextProperties.ApplicationName, _context.CodePackageActivationContext.ApplicationName);
            LogContext.PushProperty(ServiceContextProperties.ApplicationTypeName, _context.CodePackageActivationContext.ApplicationTypeName);
            LogContext.PushProperty(ServiceContextProperties.ServicePackageVersion, _context.CodePackageActivationContext.CodePackageVersion);

            if (_context is StatelessServiceContext)
            {
                LogContext.PushProperty(ServiceContextProperties.InstanceId, _context.ReplicaOrInstanceId.ToString(CultureInfo.InvariantCulture));
            }
        }
    }
}
