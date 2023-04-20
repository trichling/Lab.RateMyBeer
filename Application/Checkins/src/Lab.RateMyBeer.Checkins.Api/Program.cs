using Lab.RateMyBeer.Checkins.Api.Checkins;
using Lab.RateMyBeer.Checkins.Data.Checkins;
using Lab.RateMyBeer.Framework;
using Microsoft.EntityFrameworkCore;
using Google.Protobuf;
using Google.Protobuf.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseNServiceBus(context =>
{
    var configuration = new EndpointConfiguration("Lab.RateMyBeer.Checkins.Api");
    configuration.SendOnly();
    configuration.Configure(context, routing => { });
    
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



