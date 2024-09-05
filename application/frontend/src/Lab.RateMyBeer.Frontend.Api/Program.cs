using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab.RateMyBeer.Checkins.Contracts.Checkins.Messages.Commands;
using Lab.RateMyBeer.Comments.Contracts.Comments.Messages.Commands;
using Lab.RateMyBeer.Framework;
using Lab.RateMyBeer.Framework.Composition;
using Lab.RateMyBeer.Ratings.Contracts.StarRatings.Messages.Commands;
using Microsoft.AspNetCore.Builder;
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
            //CreateHostBuilder(args).Build().Run();

            var builder = WebApplication.CreateBuilder();
            var startup = new Startup(builder.Configuration);
            startup.ConfigureServices(builder.Services);

            var app = BuildWebApplication(builder);
            startup.Configure(app, app.Environment);
            app.MapDefaultEndpoints();
            app.Run();
        }

        public static WebApplication BuildWebApplication(WebApplicationBuilder builder)
        {
            builder
                .WebHost
                    .AddServiceDefaults();

            builder
                .Host
                    .UseNServiceBus(context =>
                    {
                        var configuration = new EndpointConfiguration("Lab.RateMyBeer.Frontend.Api");

                        configuration.Configure(context, routing =>
                        {
                            routing.RouteToEndpoint(typeof(CreateCheckinCommand).Assembly, "Lab.RateMyBeer.Checkins");
                            routing.RouteToEndpoint(typeof(CreateUserCommentCommand).Assembly, "Lab.RateMyBeer.Comments");
                            routing.RouteToEndpoint(typeof(CreateStarRatingCommand).Assembly, "Lab.RateMyBeer.Ratings");
                        });

                        return configuration;
                    });

            return builder.Build();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.AddServiceDefaults();
                })
                .UseNServiceBus(context =>
                {
                    var configuration = new EndpointConfiguration("Lab.RateMyBeer.Frontend.Api");

                    configuration.Configure(context, routing =>
                    {
                        routing.RouteToEndpoint(typeof(CreateCheckinCommand).Assembly, "Lab.RateMyBeer.Checkins");
                        routing.RouteToEndpoint(typeof(CreateUserCommentCommand).Assembly, "Lab.RateMyBeer.Comments");
                        routing.RouteToEndpoint(typeof(CreateStarRatingCommand).Assembly, "Lab.RateMyBeer.Ratings");
                    });

                    return configuration;
                });
    }
}
