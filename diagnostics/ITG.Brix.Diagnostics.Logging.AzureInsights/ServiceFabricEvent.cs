namespace ITG.Brix.Diagnostics.Logging.AzureInsights
{
    internal static class ServiceFabricEvent
    {
        public const int Undefined = 0;
        public const int Trace = 1000;
        public const int TraceInformation = 100001;
        public const int TraceWarning = 100001;
        public const int TraceError = 100001;
        public const int ApiRequest = 1001;
        public const int ServiceRequest = 1002;
        public const int Exception = 1003;
        public const int Metric = 1004;
        public const int Dependency = 1005;
        public const int ServiceListening = 1006;
        public const int ServiceInitializationFailed = 1007;
    }
}
