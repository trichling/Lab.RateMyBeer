using Lab.RateMyBeer.Checkins.Api.Checkins;
using Lab.RateMyBeer.Checkins.Data.Checkins;
using Microsoft.EntityFrameworkCore;
using NServiceBus;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseNServiceBus(context =>
{
    var configuration = new EndpointConfiguration("Lab.RateMyBeer.Checkins.Api");
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

builder.Services.RegisterCheckinsModule(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    Console.WriteLine($"Development: {app.Configuration.GetConnectionString("CheckinsDbConnectionString")}");

    using (var scope = app.Services.CreateScope())
    {
        var checkinsContext = scope.ServiceProvider.GetRequiredService<CheckinsContext>();
        checkinsContext.Database.Migrate();
    }
}

app.UseSwagger();
app.UseSwaggerUI();

app.MapCheckinsEndpoints();

app.Run();
