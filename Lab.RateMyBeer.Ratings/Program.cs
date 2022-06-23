using NServiceBus;

var builder = Host.CreateDefaultBuilder(args);
builder
    .UseNServiceBus(context =>
    {
        var configuration = new EndpointConfiguration("Lab.RateMyBeer.Ratings");

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
      
    });            

var host = builder.Build();

await host.RunAsync();
