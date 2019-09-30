using Autofac;
using ITG.Brix.WorkOrders.Application.Cqs.Commands.Definitions;
using MediatR;
using System.Reflection;

namespace ITG.Brix.WorkOrders.DependencyResolver.AutofacModules
{
    public class CommandHandlerModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(CreateWorkOrderCommand).GetTypeInfo().Assembly).AsClosedTypesOf(typeof(IRequestHandler<,>));
        }
    }
}
