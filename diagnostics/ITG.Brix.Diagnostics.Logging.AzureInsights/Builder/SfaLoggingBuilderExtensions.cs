using ITG.Brix.Diagnostics.Logging.AzureInsights.Middleware;
using Microsoft.AspNetCore.Builder;

namespace ITG.Brix.Diagnostics.Logging.AzureInsights.Builder
{
    public static class SfaLoggingBuilderExtensions
    {
        public static IApplicationBuilder UseRequestTrackingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestTrackingMiddleware>();
        }
    }
}
