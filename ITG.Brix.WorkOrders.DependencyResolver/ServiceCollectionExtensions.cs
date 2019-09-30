using AutoMapper;
using ITG.Brix.WorkOrders.API.Context.Providers;
using ITG.Brix.WorkOrders.API.Context.Providers.Impl;
using ITG.Brix.WorkOrders.API.Context.Services;
using ITG.Brix.WorkOrders.API.Context.Services.Arrangements;
using ITG.Brix.WorkOrders.API.Context.Services.Arrangements.Impl;
using ITG.Brix.WorkOrders.API.Context.Services.Impl;
using ITG.Brix.WorkOrders.API.Context.Services.Requests;
using ITG.Brix.WorkOrders.API.Context.Services.Requests.Impl;
using ITG.Brix.WorkOrders.API.Context.Services.Requests.Mappers;
using ITG.Brix.WorkOrders.API.Context.Services.Requests.Mappers.Impl;
using ITG.Brix.WorkOrders.API.Context.Services.Requests.Validators;
using ITG.Brix.WorkOrders.API.Context.Services.Requests.Validators.Components;
using ITG.Brix.WorkOrders.API.Context.Services.Requests.Validators.Components.Impl;
using ITG.Brix.WorkOrders.API.Context.Services.Requests.Validators.Impl;
using ITG.Brix.WorkOrders.API.Context.Services.Responses;
using ITG.Brix.WorkOrders.API.Context.Services.Responses.Impl;
using ITG.Brix.WorkOrders.API.Context.Services.Responses.Mappers;
using ITG.Brix.WorkOrders.API.Context.Services.Responses.Mappers.Impl;
using ITG.Brix.WorkOrders.Application.Cqs.Commands.Handlers;
using ITG.Brix.WorkOrders.Application.MappingProfiles;
using ITG.Brix.WorkOrders.Application.Services;
using ITG.Brix.WorkOrders.Application.Services.Acls;
using ITG.Brix.WorkOrders.Application.Services.Acls.Impl;
using ITG.Brix.WorkOrders.Application.Services.Impl;
using ITG.Brix.WorkOrders.Domain.Repositories;
using ITG.Brix.WorkOrders.Infrastructure.Converters;
using ITG.Brix.WorkOrders.Infrastructure.Converters.Impl;
using ITG.Brix.WorkOrders.Infrastructure.DataAccess.ClassMaps;
using ITG.Brix.WorkOrders.Infrastructure.DataAccess.Configurations;
using ITG.Brix.WorkOrders.Infrastructure.DataAccess.Configurations.Impl;
using ITG.Brix.WorkOrders.Infrastructure.DataAccess.Repositories;
using ITG.Brix.WorkOrders.Infrastructure.Orchestrations;
using ITG.Brix.WorkOrders.Infrastructure.Orchestrations.Impl;
using ITG.Brix.WorkOrders.Infrastructure.Orchestrators;
using ITG.Brix.WorkOrders.Infrastructure.Orchestrators.Impl;
using ITG.Brix.WorkOrders.Infrastructure.Providers;
using ITG.Brix.WorkOrders.Infrastructure.Providers.Impl;
using ITG.Brix.WorkOrders.Infrastructure.RestApis;
using ITG.Brix.WorkOrders.Infrastructure.RestApis.Configurations;
using ITG.Brix.WorkOrders.Infrastructure.RestApis.Configurations.Impl;
using ITG.Brix.WorkOrders.Infrastructure.RestApis.Impl;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ITG.Brix.WorkOrders.DependencyResolver
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AutoMapper(this IServiceCollection services)
        {
            var assembly = typeof(DomainProfile).GetTypeInfo().Assembly;

            services.AddAutoMapper(assembly);

            return services;
        }

        public static IServiceCollection Persistence(this IServiceCollection services, string connectionString)
        {
            ClassMapsRegistrator.RegisterMaps();

            services.AddMediatR(typeof(CreateWorkOrderCommandHandler));

            services.AddScoped<IPersistenceContext, PersistenceContext>();
            services.AddScoped<IPersistenceConfiguration>(x => new PersistenceConfiguration(connectionString));

            services.AddScoped<IModelConverter, ModelConverter>();

            services.AddScoped<IWorkOrderReadRepository, WorkOrderReadRepository>();
            services.AddScoped<IWorkOrderWriteRepository, WorkOrderWriteRepository>();
            services.AddScoped<IPlatoOrderWriteRepository, PlatoOrderWriteRepository>();

            return services;
        }

        public static IServiceCollection ApiContextProviders(this IServiceCollection services)
        {
            services.AddScoped<IJsonProvider, JsonProvider>();

            return services;
        }

        public static IServiceCollection Providers(this IServiceCollection services)
        {
            services.AddScoped<ITypeConverterProvider, TypeConverterProvider>();
            services.AddScoped<IIdentifierProvider, IdentifierProvider>();
            services.AddScoped<IVersionProvider, VersionProvider>();
            services.AddScoped<IOdataProvider, OdataProvider>();
            services.AddScoped<IWorkOrderProvider, WorkOrderProvider>();
            services.AddScoped<IPlatoOrderProvider, PlatoOrderProvider>();
            services.AddScoped<IDateTimeProvider, DateTimeProvider>();
            services.AddScoped<IFilterProvider, FilterProvider>();

            return services;
        }

        public static IServiceCollection AppServices(this IServiceCollection services)
        {
            services.AddScoped<IPlatoDataAcl, PlatoDataAcl>();
            services.AddScoped<IPlatoOrderChecker, PlatoOrderChecker>();
            services.AddScoped<IDomainConverter, DomainConverter>();

            return services;
        }

        public static IServiceCollection ApiContextServices(this IServiceCollection services)
        {
            services.AddScoped<IRequestComponentValidator, RequestComponentValidator>();
            services.AddScoped<IRequestValidator, GetWorkOrderRequestValidator>();
            services.AddScoped<IRequestValidator, ListWorkOrderRequestValidator>();
            services.AddScoped<IRequestValidator, CreateWorkOrderRequestValidator>();
            services.AddScoped<IRequestValidator, CreatePlatoOrderRequestValidator>();
            services.AddScoped<IRequestValidator, UpdateWorkOrderRequestValidator>();
            services.AddScoped<IRequestValidator, DeleteWorkOrderRequestValidator>();
            services.AddScoped<IRequestValidator, ListPropertyRequestValidator>();
            services.AddScoped<IRequestValidator, ListOrderItemRequestValidator>();
            services.AddScoped<IRequestValidator, ListProductItemRequestValidator>();


            services.AddScoped<IApiRequest, ApiRequest>();
            services.AddScoped<IApiResponse, ApiResponse>();
            services.AddScoped<IApiResult, ApiResult>();
            services.AddScoped<ICqsMapper, CqsMapper>();

            services.AddScoped<IErrorMapper, ErrorMapper>();
            services.AddScoped<IHttpStatusCodeMapper, HttpStatusCodeMapper>();


            services.AddScoped<IValidationArrangement, ValidationArrangement>();
            services.AddScoped<IOperationArrangement, OperationArrangement>();


            return services;
        }

        public static IServiceCollection RestApis(this IServiceCollection services, string biztalk)
        {
            services.AddScoped<IBiztalkContext, BiztalkContext>();
            services.AddScoped<IBiztalkConfiguration>(x => new BiztalkConfiguration(biztalk));

            services.AddHttpClient<IBiztalkRestApi, BiztalkRestApi>();

            return services;
        }

        public static IServiceCollection Orchestrations(this IServiceCollection services)
        {
            services.AddScoped<IBiztalkOrchestration, BiztalkOrchestration>();
            services.AddScoped<IOrchestrator, Orchestrator>();

            return services;
        }
    }
}
