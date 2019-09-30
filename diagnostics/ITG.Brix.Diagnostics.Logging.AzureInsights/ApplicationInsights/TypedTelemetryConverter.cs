using ITG.Brix.Diagnostics.Logging.AzureInsights.PropertyMap;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Serilog.Events;
using Serilog.Sinks.ApplicationInsights.Sinks.ApplicationInsights.TelemetryConverters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Fabric;
using System.Linq;
using System.Text;

namespace ITG.Brix.Diagnostics.Logging.AzureInsights.ApplicationInsights
{
    internal class TypedTelemetryConverter : TraceTelemetryConverter
    {
        private readonly ServiceContext _context;
        private LogEvent _logEvent;

        public TypedTelemetryConverter(ServiceContext context)
        {
            _context = context;
        }

        public override IEnumerable<ITelemetry> Convert(LogEvent logEvent, IFormatProvider formatProvider)
        {

            _logEvent = logEvent;
            int serviceFabricEvent = ServiceFabricEvent.Undefined;
            if (logEvent.Properties.TryGetValue(SharedProperties.EventId, out LogEventPropertyValue eventId))
            {
                int.TryParse(((StructureValue)eventId).Properties[0].Value.ToString(), out serviceFabricEvent);
            }
            else if (logEvent.Level == LogEventLevel.Information && logEvent.Exception != null)
            {
                serviceFabricEvent = ServiceFabricEvent.TraceError;
            }
            else if (logEvent.Level == LogEventLevel.Error)
            {
                serviceFabricEvent = ServiceFabricEvent.Exception;
            }
            else if (logEvent.Level == LogEventLevel.Fatal)
            {
                serviceFabricEvent = ServiceFabricEvent.Exception;
            }

            ITelemetry telemetry;
            switch (serviceFabricEvent)
            {
                case ServiceFabricEvent.ServiceInitializationFailed:
                case ServiceFabricEvent.Exception:
                    telemetry = CreateExceptionTelemetry();
                    break;
                case ServiceFabricEvent.ApiRequest:
                    telemetry = CreateRequestTelemetry();
                    break;
                case ServiceFabricEvent.Metric:
                    telemetry = CreateMetricTelemetry();
                    break;
                case ServiceFabricEvent.ServiceRequest:
                case ServiceFabricEvent.Dependency:
                    telemetry = CreateDependencyTelemetry();
                    break;
                case ServiceFabricEvent.TraceError:
                    telemetry = CreateTraceErrorTelemetry();
                    break;
                default:
                    telemetry = CreateTraceTelemetry();
                    break;
            }

            SetContextProperties(telemetry);

            yield return telemetry;
        }

        private ITelemetry CreateRequestTelemetry()
        {
            var requestTelemetry = new RequestTelemetry
            {
                ResponseCode = TryGetStringValue(ApiRequestProperties.StatusCode),
                Url = new Uri($"{TryGetStringValue(ApiRequestProperties.Scheme)}://{TryGetStringValue(ApiRequestProperties.Host)}{TryGetStringValue(ApiRequestProperties.Path)}"),
                Name = $"{TryGetStringValue(ApiRequestProperties.Method)} {TryGetStringValue(ApiRequestProperties.Path)}",
                Timestamp = DateTime.Parse(TryGetStringValue(ApiRequestProperties.StartTime)),
                Duration = TimeSpan.FromMilliseconds(double.Parse(TryGetStringValue(ApiRequestProperties.DurationInMs))),
                Success = bool.Parse(TryGetStringValue(ApiRequestProperties.Success)),
                Properties =
                {
                    { ApiRequestProperties.Method, TryGetStringValue(ApiRequestProperties.Method) },
                    { ApiRequestProperties.Headers, TryGetStringValue(ApiRequestProperties.Headers) },
                    { ApiRequestProperties.Body,  TryGetStringValue(ApiRequestProperties.Body) },
                    { ApiRequestProperties.Response,  TryGetStringValue(ApiRequestProperties.Response) },

                }
            };

            requestTelemetry.Context.Operation.Name = requestTelemetry.Name;
            requestTelemetry.Id = TryGetStringValue(SharedProperties.TraceId);

            AddLogEventProperties(requestTelemetry, typeof(ApiRequestProperties).GetFields().Select(f => f.GetRawConstantValue().ToString()));

            return requestTelemetry;
        }

        private ITelemetry CreateDependencyTelemetry()
        {
            var dependencyTelemetry = new DependencyTelemetry
            {
                Name = TryGetStringValue(DependencyProperties.DependencyTypeName),
                Duration = TimeSpan.FromMilliseconds(double.Parse(TryGetStringValue(DependencyProperties.DurationInMs))),
                Data = TryGetStringValue(DependencyProperties.Name),
                Success = bool.Parse(TryGetStringValue(DependencyProperties.Success)),
                Type = TryGetStringValue(DependencyProperties.Type),
                Timestamp = DateTime.Parse(TryGetStringValue(DependencyProperties.StartTime)),
            };

            dependencyTelemetry.Id = dependencyTelemetry.Data;
            dependencyTelemetry.Context.Operation.Name = dependencyTelemetry.Name;

            AddLogEventProperties(dependencyTelemetry, typeof(DependencyProperties).GetFields().Select(f => f.GetRawConstantValue().ToString()));

            return dependencyTelemetry;
        }

        private ITelemetry CreateMetricTelemetry()
        {
            var metricTelemetry = new MetricTelemetry
            {
                Name = TryGetStringValue(MetricProperties.Name),
                Sum = double.Parse(TryGetStringValue(MetricProperties.Value)),
                Timestamp = _logEvent.Timestamp
            };

            if (_logEvent.Properties.TryGetValue(MetricProperties.MinValue, out LogEventPropertyValue min))
                metricTelemetry.Min = double.Parse(min.ToString());

            if (_logEvent.Properties.TryGetValue(MetricProperties.MaxValue, out LogEventPropertyValue max))
                metricTelemetry.Max = double.Parse(max.ToString());

            AddLogEventProperties(metricTelemetry, typeof(MetricProperties).GetFields().Select(f => f.GetRawConstantValue().ToString()));

            return metricTelemetry;
        }

        private ITelemetry CreateExceptionTelemetry()
        {
            var exceptionTelemetry = new ExceptionTelemetry(_logEvent.Exception)
            {
                SeverityLevel = _logEvent.Level.ToSeverityLevel(),
                Timestamp = _logEvent.Timestamp,
            };

            AddLogEventProperties(exceptionTelemetry);

            return exceptionTelemetry;
        }

        private ITelemetry CreateTraceErrorTelemetry()
        {
            var traceTelemetry = new TraceTelemetry(_logEvent.RenderMessage())
            {
                SeverityLevel = SeverityLevel.Error,
                Timestamp = _logEvent.Timestamp,
                Message = FlattenException(_logEvent.Exception)
            };

            AddLogEventProperties(traceTelemetry);

            return traceTelemetry;
        }

        private static string FlattenException(Exception exception)
        {
            var stringBuilder = new StringBuilder();

            while (exception != null)
            {
                stringBuilder.AppendLine(exception.Message);
                stringBuilder.AppendLine(exception.StackTrace);

                exception = exception.InnerException;
            }

            return stringBuilder.ToString();
        }

        private ITelemetry CreateTraceTelemetry()
        {
            var traceTelemetry = new TraceTelemetry(_logEvent.RenderMessage())
            {
                SeverityLevel = _logEvent.Level.ToSeverityLevel(),
                Timestamp = _logEvent.Timestamp
            };

            AddLogEventProperties(traceTelemetry);

            return traceTelemetry;
        }

        private void SetContextProperties(ITelemetry telemetry)
        {
            telemetry.Context.Cloud.RoleName = FabricEnvironmentVariable.ServicePackageName;
            telemetry.Context.Cloud.RoleInstance = FabricEnvironmentVariable.ServicePackageActivationId ?? FabricEnvironmentVariable.ServicePackageInstanceId;
            telemetry.Context.Component.Version = _context.CodePackageActivationContext.CodePackageVersion;


            ISupportProperties propTelematry = (ISupportProperties)telemetry;

            if (!propTelematry.Properties.ContainsKey(ServiceContextProperties.NodeName))
            {
                if (!string.IsNullOrEmpty(FabricEnvironmentVariable.NodeName))
                {
                    propTelematry.Properties.Add(ServiceContextProperties.NodeName, FabricEnvironmentVariable.NodeName);
                }
            }

#if Debug
            telemetry.Context.Operation.SyntheticSource = "DebugSession";
#else
            if (Debugger.IsAttached)
            {
                telemetry.Context.Operation.SyntheticSource = "DebuggerAttached";
            }
#endif

            if (_logEvent.Properties.TryGetValue(SharedProperties.TraceId, out LogEventPropertyValue value))
            {
                var id = ((ScalarValue)value).Value.ToString();
                telemetry.Context.Operation.ParentId = id;
                telemetry.Context.Operation.Id = id;
            }
        }

        private void AddLogEventProperties(ISupportProperties telemetry, IEnumerable<string> excludePropertyKeys = null)
        {
            var excludedPropertyKeys = new List<string>
            {
                ServiceContextProperties.NodeName,
                ServiceContextProperties.ServicePackageVersion
            };

            if (excludePropertyKeys != null)
                excludedPropertyKeys.AddRange(excludePropertyKeys);

            foreach (var property in _logEvent
                .Properties
                .Where(property => property.Value != null && !excludedPropertyKeys.Contains(property.Key) && !telemetry.Properties.ContainsKey(property.Key)))
            {
                ApplicationInsightsPropertyFormatter.WriteValue(property.Key, property.Value, telemetry.Properties);
            }
        }

        private string TryGetStringValue(string propertyName)
        {
            if (!_logEvent.Properties.TryGetValue(propertyName, out LogEventPropertyValue value))
                throw new ArgumentException($"LogEvent does not contain required property {propertyName} for EventId {_logEvent.Properties[SharedProperties.EventId]}", propertyName);

            return ((ScalarValue)value).Value.ToString();
        }
    }
}
