using Lab.RateMyBeer.Framework;
using Lab.RateMyBeer.Ratings.Data.StarRatings;
using Microsoft.EntityFrameworkCore;
using NServiceBus;

var builder = Host.CreateDefaultBuilder(args);
builder
    .UseNServiceBus(context =>
    {
        var configuration = new EndpointConfiguration("Lab.RateMyBeer.Ratings");

        configuration.Configure(context, routing => { });

        return configuration;
    })
    .ConfigureServices((host, services) =>
    {
        var ratingsDbConnectionString = host.Configuration.GetConnectionString("RatingsDbConnectionString");
        services.AddDbContext<StarRatingContext>(options =>
        options.UseSqlServer(ratingsDbConnectionString));
    });            

var host = builder.Build();

await host.RunAsync();
