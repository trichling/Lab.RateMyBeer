using Lab.RateMyBeer.Checkins.Data.Checkins;
using Lab.RateMyBeer.Framework;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NServiceBus;

var builder = Host.CreateDefaultBuilder(args);
builder
    .UseNServiceBus(context =>
    {
        var configuration = new EndpointConfiguration("Lab.RateMyBeer.Checkins");

        configuration.Configure(context, routing => { });

        return configuration;
    })
    .ConfigureServices((host, services) =>
    {
       var checkinDbConnectionString = host.Configuration.GetConnectionString("CheckinsDbConnectionString");
        services.AddDbContext<CheckinsContext>(options =>
        {
            options.UseSqlServer(checkinDbConnectionString);
        });
    });            

var host = builder.Build();

await host.RunAsync();
