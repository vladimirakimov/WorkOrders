using ITG.Brix.Diagnostics.Logging.AzureInsights.PropertyMap;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;

namespace ITG.Brix.Diagnostics.Logging.AzureInsights.Extensions
{
    public static class ILoggerExtensions
    {
        public static void LogUnhandled(this ILogger logger, Exception exception)
        {
            logger.LogError(ServiceFabricEvent.Exception, exception, "Unhandled scenario encountered.");
        }

        public static void LogMetric(this ILogger logger, string name, double value)
        {
            logger.LogInformation(ServiceFabricEvent.Metric,
                $"Metric {{{MetricProperties.Name}}}, value: {{{MetricProperties.Value}}}",
                name,
                value);
        }

        public static void LogMetric(this ILogger logger, string name, double value, double? max = null,
            double? min = null)
        {
            logger.LogInformation(ServiceFabricEvent.Metric,
                $"Metric {{{MetricProperties.Name}}}, value: {{{MetricProperties.Value}}}, min: {{{MetricProperties.MinValue}}}, max: {{{MetricProperties.MaxValue}}}",
                name,
                value,
                min,
                max);
        }


        public static void LogDependency(this ILogger logger, string service, string method,
            DateTime started, TimeSpan duration, bool success)
        {
            logger.LogInformation(ServiceFabricEvent.ServiceRequest,
                $"The call to {{{DependencyProperties.Type}}} dependency {{{DependencyProperties.DependencyTypeName}}} named {{{DependencyProperties.Name}}} finished in {{{DependencyProperties.DurationInMs}}} ms (success: {{{DependencyProperties.Success}}}) ({{{DependencyProperties.StartTime}}})",
                "ServiceFabric",
                service,
                method,
                duration.TotalMilliseconds,
                success,
                started);
        }


        public static void LogRequest(this ILogger logger, HttpContext context, DateTime started, TimeSpan duration, bool success)
        {
            var request = context.Request;
            logger.LogInformation(ServiceFabricEvent.ApiRequest,
                $"The {{{ApiRequestProperties.Method}}} action to {{{ApiRequestProperties.Scheme}}}{{{ApiRequestProperties.Host}}}{{{ApiRequestProperties.Path}}} finished in {{{ApiRequestProperties.DurationInMs}}} ms with status code {{{ApiRequestProperties.StatusCode}}}({{{ApiRequestProperties.Success}}}) Headers: {{{ApiRequestProperties.Headers}}} Body: {{{ApiRequestProperties.Body}}} ({{{ApiRequestProperties.Response}}}) ({{{ApiRequestProperties.StartTime}}})",
                request.Method,
                request.Scheme,
                request.Host.Value,
                request.Path.Value,
                duration.TotalMilliseconds,
                context.Response.StatusCode,
                success,
                request.ReadHeadersAsString(),
                request.ReadRequestBodyAsString(),
                context.Response,
                started);
        }

        public static void LogServiceStartedListening(this ILogger logger, string serviceTypeName, string endpoint)
        {
            logger.LogInformation(ServiceFabricEvent.ServiceListening,
                "Service {ServiceType} started (endpoint {Endpoint})", serviceTypeName, endpoint);
        }

        public static void LogServiceInitalizationFailed(this ILogger logger, string serviceTypeName, Exception exception)
        {
            logger.LogCritical(ServiceFabricEvent.ServiceInitializationFailed, exception,
                "Service {ServiceType} failed to initialize.", serviceTypeName);
        }
    }
}
