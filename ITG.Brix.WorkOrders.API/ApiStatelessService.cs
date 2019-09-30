using ITG.Brix.Diagnostics.Logging.Abstractions;
using ITG.Brix.WorkOrders.API.Constants;
using ITG.Brix.WorkOrders.API.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.ServiceFabric.Services.Communication.AspNetCore;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using System;
using System.Collections.Generic;
using System.Fabric;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.WorkOrders.API
{
    /// <summary>
    /// The FabricRuntime creates an instance of this class for each service type instance. 
    /// </summary>
    internal sealed class ApiStatelessService : StatelessService
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogAs _logAs;

        public ApiStatelessService(StatelessServiceContext context, ILoggerFactory loggerFactory, ILogAs logAs)
            : base(context)
        {
            _loggerFactory = loggerFactory;
            _logAs = logAs;
        }

        /// <summary>
        /// Optional override to create listeners (like tcp, http) for this service instance.
        /// </summary>
        /// <returns>The collection of listeners.</returns>
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return new ServiceInstanceListener[]
            {
                new ServiceInstanceListener(serviceContext =>
                    new KestrelCommunicationListener(serviceContext, "ApiServiceEndpoint", (url, listener) =>
                    {
                        _logAs.ServiceStartedListening(typeof(ApiStatelessService).FullName, url);

                        return new WebHostBuilder()
                                    .UseKestrel()
                                    .ConfigureServices(
                                        services => services
                                            .AddHttpContextAccessor()
                                            .AddSingleton<StatelessServiceContext>(serviceContext)
                                            .AddSingleton<ILogAs>(_logAs)
                                            )
                                    .UseContentRoot(Directory.GetCurrentDirectory())
                                    .UseStartup<Startup>()
                                    .UseEnvironment(GetEnvironment())
                                    .UseServiceFabricIntegration(listener, ServiceFabricIntegrationOptions.None)
                                    .UseUrls(url)
                                    .UseConfiguration(GetConfig())
                                    .Build();
                    }))
            };
        }

        protected override Task OnCloseAsync(CancellationToken cancellationToken)
        {
            _loggerFactory.Dispose();
            return base.OnCloseAsync(cancellationToken);
        }

        private string GetEnvironment()
        {
            var result = FabricRuntime.GetActivationContext()?
                                .GetConfigurationPackageObject(Consts.Config.ConfigurationPackageObject)?
                                .Settings.Sections[Consts.Config.Environment.Section]?
                                .Parameters[Consts.Config.Environment.Param]?.Value;
            return result;
        }

        private IConfiguration GetConfig()
        {

            var connectionString = FabricRuntime.GetActivationContext()?
                .GetConfigurationPackageObject(Consts.Config.ConfigurationPackageObject)?
                .Settings.Sections[Consts.Config.Database.Section]?
                .Parameters[Consts.Config.Database.Param]?.Value;

            var biztalk = FabricRuntime.GetActivationContext()?
                .GetConfigurationPackageObject(Consts.Config.ConfigurationPackageObject)?
                .Settings.Sections[Consts.Config.Biztalk.Section]?
                .Parameters[Consts.Config.Biztalk.Param]?.Value;

            Environment.SetEnvironmentVariable(Consts.Configuration.Id + Consts.Configuration.ConnectionString, connectionString, EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable(Consts.Configuration.Id + Consts.Configuration.Biztalk, biztalk, EnvironmentVariableTarget.Process);
            var result = new ConfigurationBuilder().AddEnvironmentVariables(Consts.Configuration.Id).Build();
            return result;
        }
    }
}
