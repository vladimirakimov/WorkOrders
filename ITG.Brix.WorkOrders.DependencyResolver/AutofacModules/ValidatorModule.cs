using Autofac;
using FluentValidation;
using ITG.Brix.WorkOrders.Application.Cqs.Commands.Validators;
using System.Reflection;

namespace ITG.Brix.WorkOrders.DependencyResolver.AutofacModules
{
    public class ValidatorModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterAssemblyTypes(typeof(CreateWorkOrderCommandValidator).GetTypeInfo().Assembly)
                .Where(t => t.IsClosedTypeOf(typeof(IValidator<>)))
                .AsImplementedInterfaces();
        }
    }
}
