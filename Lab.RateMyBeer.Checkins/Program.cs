using Lab.RateMyBeer.Checkins.Data.Checkins;
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

        var transport = configuration.UseTransport<RabbitMQTransport>();
        var transportConnectionString =
            context.Configuration["Dependencies:NServiceBus:TransportConnectionString"];
        transport.ConnectionString(transportConnectionString);
        transport.UseConventionalRoutingTopology();

        configuration.UsePersistence<LearningPersistence>();
        configuration.UseSerialization<NewtonsoftSerializer>();
        configuration.Conventions()
            .DefiningMessagesAs(t => t.Namespace.Contains("Messages"))
            .DefiningCommandsAs(t => t.Namespace.EndsWith("Commands"))
            .DefiningEventsAs(t => t.Namespace.EndsWith("Events"));

        configuration.EnableInstallers();

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
