using Autofac;
using Autofac.Extensions.DependencyInjection;
using ITG.Brix.WorkOrders.DependencyResolver.AutofacModules;
using Microsoft.Extensions.DependencyInjection;

namespace ITG.Brix.WorkOrders.DependencyResolver
{
    public static class Resolver
    {
        public static AutofacServiceProvider BuildServiceProvider(IServiceCollection services, string connectionString, string biztalk)
        {
            services
                .AutoMapper()
                .Persistence(connectionString)
                .Providers()
                .AppServices()
                .ApiContextProviders()
                .ApiContextServices()
                .RestApis(biztalk)
                .Orchestrations();

            var containerBuilder = BuildContainer(services);

            var container = containerBuilder.Build();

            return new AutofacServiceProvider(container);
        }

        private static ContainerBuilder BuildContainer(IServiceCollection services)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.Populate(services);
            containerBuilder
                .RegisterModule(new MediatorModule())
                .RegisterModule(new CommandHandlerModule())
                .RegisterModule(new ValidatorModule())
                .RegisterModule(new BehaviorModule());

            return containerBuilder;
        }
    }
}
