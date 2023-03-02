using Lab.RateMyBeer.Comments.Api.Comments;
using Lab.RateMyBeer.Comments.Data.Comments;
using Lab.RateMyBeer.Framework;
using Microsoft.EntityFrameworkCore;
using NServiceBus;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseNServiceBus(context =>
{
    var configuration = new EndpointConfiguration("Lab.RateMyBeer.Comments");
    configuration.SendOnly();
    configuration.Configure(context, routing => { });

    return configuration;
});

builder.Services.RegisterCommentsModule(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    Console.WriteLine($"Development: {app.Configuration.GetConnectionString("CommentsDbConnectionString")}");

    using (var scope = app.Services.CreateScope())
    {
        var checkinsContext = scope.ServiceProvider.GetRequiredService<CommentsContext>();
        checkinsContext.Database.Migrate();
    }
}

app.UseSwagger();
app.UseSwaggerUI();

app.MapCommentsEndpoints();

app.Run();