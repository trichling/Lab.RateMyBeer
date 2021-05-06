using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NServiceBus;

namespace Lab.RateMyBeer.Checkins
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseNServiceBus(context =>
                {
                    var configuration = new EndpointConfiguration("Lab.RateMyBeer.Checkins");
                    configuration.UseTransport<LearningTransport>();
                    configuration.UsePersistence<LearningPersistence>();
                    configuration.UseSerialization<NewtonsoftSerializer>();
                    configuration.Conventions()
                        .DefiningMessagesAs(t => t.Namespace.Contains("Messages"))
                        .DefiningCommandsAs(t => t.Namespace.EndsWith("Commands"))
                        .DefiningEventsAs(t => t.Namespace.EndsWith("Events"));

                    return configuration;
                });
    }
}
