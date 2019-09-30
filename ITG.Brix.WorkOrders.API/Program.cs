using ITG.Brix.Diagnostics.Logging;
using ITG.Brix.Diagnostics.Logging.Abstractions;
using ITG.Brix.Diagnostics.Logging.AzureInsights;
using ITG.Brix.WorkOrders.API.Constants;
using ITG.Brix.WorkOrders.API.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.ServiceFabric.Services.Runtime;
using System;
using System.Diagnostics;
using System.Fabric;
using System.Threading;

namespace ITG.Brix.WorkOrders.API
{
    internal static class Program
    {
        /// <summary>
        /// This is the entry point of the service host process.
        /// </summary>
        private static void Main()
        {
            ILogAs logAs = null;

            var applicationInsightsKey = FabricRuntime.GetActivationContext()?
                                            .GetConfigurationPackageObject(Consts.Config.ConfigurationPackageObject)?
                                            .Settings.Sections[Consts.Config.ApplicationInsights.Section]?
                                            .Parameters[Consts.Config.ApplicationInsights.Param]?.Value;
            try
            {

                // The ServiceManifest.XML file defines one or more service type names.
                // Registering a service maps a service type name to a .NET type.
                // When Service Fabric creates an instance of this service type,
                // an instance of the class is created in this host process.

                ServiceRuntime.RegisterServiceAsync("ITG.Brix.WorkOrders.APIType",
                    context =>
                    {
                        var loggerFactory = new LoggerFactoryBuilder(context).CreateLoggerFactory(applicationInsightsKey);
                        var logger = loggerFactory.CreateLogger<ApiStatelessService>();
                        logAs = new LogAs(logger);
                        return new ApiStatelessService(context, loggerFactory, logAs);
                    }).GetAwaiter().GetResult();
                ServiceEventSource.Current.ServiceTypeRegistered(Process.GetCurrentProcess().Id, typeof(ApiStatelessService).Name);

                // Prevents this host process from terminating so services keeps running. 
                Thread.Sleep(Timeout.Infinite);
            }
            catch (Exception ex)
            {
                logAs?.ServiceInitalizationFailed(typeof(ApiStatelessService).FullName, ex);
                throw;
            }
        }

    }
}
