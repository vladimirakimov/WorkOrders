using FluentValidation.AspNetCore;
using ITG.Brix.Diagnostics.Logging.Abstractions;
using ITG.Brix.WorkOrders.API.Constants;
using ITG.Brix.WorkOrders.API.Middleware;
using ITG.Brix.WorkOrders.DependencyResolver;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Threading.Tasks;

namespace ITG.Brix.WorkOrders.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                    .AddJsonOptions(options =>
                    {
                        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    })
                    .AddFluentValidation();

            services.AddCors();
            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "WorkOrders API", Version = "v1" });
            });

            var connectionString = Configuration[Consts.Configuration.ConnectionString];
            var biztalk = Configuration[Consts.Configuration.Biztalk];

            var result = Resolver.BuildServiceProvider(services, connectionString, biztalk);

            return result;
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILogAs logAs)
        {
            app.UseCors(builder => builder
                                   .AllowAnyOrigin()
                                   .AllowAnyMethod()
                                   .AllowAnyHeader()
                                   .AllowCredentials());

            app.UseMiddleware<ErrorHandlingMiddleware>(logAs);
            //app.UseRequestTrackingMiddleware();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WorkOrders API v1");
            });

            app.Run(context =>
            {
                context.Response.Redirect("swagger");
                return Task.CompletedTask;
            });
        }
    }
}
