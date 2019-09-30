using ITG.Brix.Diagnostics.Logging.Abstractions;
using System;

namespace ITG.Brix.WorkOrders.API.Extensions
{
    public static class ILogAsExtensions
    {
        public static void ServiceStartedListening(this ILogAs logAs, string serviceTypeName, string endpoint)
        {
            logAs.Info(string.Format("Service {0} started (endpoint {1})", serviceTypeName, endpoint));
        }

        public static void ServiceInitalizationFailed(this ILogAs logAs, string serviceTypeName, Exception exception)
        {
            logAs.Critical(string.Format("Service {0} failed to initialize.", serviceTypeName), exception);
        }
    }
}
