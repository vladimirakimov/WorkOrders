using ITG.Brix.Diagnostics.Logging.Abstractions;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ITG.Brix.WorkOrders.API.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogAs _logAs;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogAs logAs)
        {
            _next = next;
            _logAs = logAs;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {

            _logAs.Critical("Unhandled scenario encountered.", exception);

            context.Response.ContentType = "application/json";

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            const string error = @"{""error"":{""code"":""UnhandledServerError"",""message"":""Please contact software development team to fix this issue."",""details"":[{""code"":""unhandled"",""message"":""Unhandled scenario encountered."",""target"":""global""}]}}";

            await context.Response.WriteAsync(error);
        }
    }
}
