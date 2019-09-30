namespace ITG.Brix.WorkOrders.API.Constants
{
    public class Consts
    {
        public static class Configuration
        {
            public const string Id = "ITG.Brix.WorkOrders";
            public const string ConnectionString = "ConnectionString";
            public const string Biztalk = "Biztalk";
        }
        public static class Config
        {
            public const string ConfigurationPackageObject = "Config";

            public static class ApplicationInsights
            {
                public const string Section = "ApplicationInsights";
                public const string Param = "ApplicationInsightsKey";
            }

            public static class Environment
            {
                public const string Section = "Environment";
                public const string Param = "ASPNETCORE_ENVIRONMENT";
            }

            public static class Database
            {
                public const string Section = "Database";
                public const string Param = "DatabaseConnectionString";
            }

            public static class Biztalk
            {
                public const string Section = "Biztalk";
                public const string Param = "Host";
            }
        }
    }
}
