using ITG.Brix.Diagnostics.Logging.Abstractions;
using ITG.Brix.WorkOrders.API;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics;
using System.Net.Http;

namespace ITG.Brix.WorkOrders.IntegrationTests.Bases
{
    public static class ControllerTestsHelper
    {
        private static TestServer _server;
        public static void InitServer()
        {
            var logAs = new LogAsDebug();

            var config = new ConfigurationBuilder().AddJsonFile("settings.json", optional: false).Build();
            _server = new TestServer(new WebHostBuilder()
                                        .ConfigureServices(services => services.AddSingleton<ILogAs>(new LogAsDebug()))
                                        .UseConfiguration(config)
                                        .UseStartup<Startup>()
            );
        }

        public static HttpClient GetClient()
        {
            var client = _server.CreateClient();
            return client;
        }
    }

    public class LogAsDebug : ILogAs
    {
        public void Critical(string message, Exception exception)
        {
            Debug.WriteLine(":: LogAs Critical ::");
            Debug.WriteLine("-> Message :" + message);
            Debug.WriteLine("-> Exception :" + exception.Message);
        }

        public void Error(Exception exception)
        {
            Debug.WriteLine(":: LogAs Error ::");
            Debug.WriteLine("-> Exception :" + exception.Message);
        }

        public void Error(string message, Exception exception)
        {
            Debug.WriteLine(":: LogAs Error ::");
            Debug.WriteLine("-> Message :" + message);
            Debug.WriteLine("-> Exception :" + exception.Message);
        }

        public void Exception(Exception exception)
        {
            Debug.WriteLine(":: LogAs Exception ::");
            Debug.WriteLine("-> Exception :" + exception.Message);
        }

        public void Info(string message)
        {
            Debug.WriteLine(":: LogAs Info ::");
            Debug.WriteLine("-> Message :" + message);
        }
    }
}
