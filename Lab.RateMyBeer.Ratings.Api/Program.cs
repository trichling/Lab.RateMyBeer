using Lab.RateMyBeer.Ratings.Api.StarRatings;
using NServiceBus;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseNServiceBus(context =>
{
    var configuration = new EndpointConfiguration("Lab.RateMyBeer.Ratings.Api");
    configuration.SendOnly();

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
});

builder.Services.RegisterStarRatingsModule(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.MapStarRatingsEndpoints();

app.Run();
