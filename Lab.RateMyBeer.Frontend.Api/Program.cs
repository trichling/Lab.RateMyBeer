using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab.RateMyBeer.Checkins.Contracts.Checkins.Messages.Commands;
using Lab.RateMyBeer.Comments.Contracts.Comments.Messages.Commands;
using Lab.RateMyBeer.Ratings.Contracts.StarRatings.Messages.Commands;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NServiceBus;

namespace Lab.RateMyBeer.Frontend.Api
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
                    var configuration = new EndpointConfiguration("Lab.RateMyBeer.Frontend.Api");

                    var transport = configuration.UseTransport<RabbitMQTransport>();
                    var transportConnectionString =
                        context.Configuration["Dependencies:NServiceBus:TransportConnectionString"];
                    transport.ConnectionString(transportConnectionString);
                    transport.UseConventionalRoutingTopology();

                    var routing = transport.Routing();
                    routing.RouteToEndpoint(typeof(CreateCheckinCommand).Assembly, "Lab.RateMyBeer.Checkins");
                    routing.RouteToEndpoint(typeof(CreateCommentCommand).Assembly, "Lab.RateMyBeer.Comments");
                    routing.RouteToEndpoint(typeof(CreateStarRatingCommand).Assembly, "Lab.RateMyBeer.Ratings");


                    configuration.UsePersistence<LearningPersistence>();
                    configuration.UseSerialization<NewtonsoftSerializer>();
                    configuration.Conventions()
                        .DefiningMessagesAs(t => t.Namespace.Contains("Messages"))
                        .DefiningCommandsAs(t => t.Namespace.EndsWith("Commands"))
                        .DefiningEventsAs(t => t.Namespace.EndsWith("Events"));

                    configuration.SendOnly();
                    configuration.EnableInstallers();

                    return configuration;
                });
    }
}
