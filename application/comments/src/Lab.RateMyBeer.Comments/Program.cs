using Lab.RateMyBeer.Comments.Data.Comments;
using Lab.RateMyBeer.Framework;
using Microsoft.EntityFrameworkCore;
using NServiceBus;

var builder = Host.CreateDefaultBuilder(args);
builder
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.AddServiceDefaults();
    })
    .UseNServiceBus(context =>
    {
        var configuration = new EndpointConfiguration("Lab.RateMyBeer.Comments");

        configuration.Configure(context, routing => { });

        return configuration;
    })
    .ConfigureServices((host, services) =>
    {
        var commentsDbConnectionString = host.Configuration.GetConnectionString("CommentsDbConnectionString");
        commentsDbConnectionString = string.Format(commentsDbConnectionString, "CommentsDb");

        services.AddDbContext<CommentsContext>(options =>
        {
            options.UseSqlServer(commentsDbConnectionString);
        });

    });

var host = builder.Build();

await host.RunAsync();

