using Lab.RateMyBeer.Comments.Data.Comments;
using Lab.RateMyBeer.Framework;
using Microsoft.EntityFrameworkCore;
using NServiceBus;

var builder = Host.CreateDefaultBuilder(args);
builder
    .UseNServiceBus(context =>
    {
        var configuration = new EndpointConfiguration("Lab.RateMyBeer.Comments");

        configuration.Configure(context, routing => { });

        return configuration;
    })
    .ConfigureServices((host, services) =>
    {
       var checkinDbConnectionString = host.Configuration.GetConnectionString("CommentsDbConnectionString");
        services.AddDbContext<CommentsContext>(options =>
        {
            options.UseSqlServer(checkinDbConnectionString);
        });

    });            

var host = builder.Build();

await host.RunAsync();

