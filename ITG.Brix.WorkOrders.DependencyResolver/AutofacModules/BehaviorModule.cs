using Autofac;
using ITG.Brix.WorkOrders.Application.Behaviors;
using MediatR;

namespace ITG.Brix.WorkOrders.DependencyResolver.AutofacModules
{
    public class BehaviorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(ValidatorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
        }
    }
}
