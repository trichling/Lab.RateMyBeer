using Lab.RateMyBeer.Framework;
using Lab.RateMyBeer.Ratings.Api.StarRatings;
using Lab.RateMyBeer.Ratings.Data.StarRatings;
using Microsoft.EntityFrameworkCore;
using NServiceBus;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.UseNServiceBus(context =>
{
    var configuration = new EndpointConfiguration("Lab.RateMyBeer.Ratings.Api");
    configuration.SendOnly();
    configuration.Configure(context, routing => { });

    return configuration;
});

builder.Services.RegisterStarRatingsModule(builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var ratingsContext = scope.ServiceProvider.GetRequiredService<StarRatingContext>();
    ratingsContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.MapStarRatingsEndpoints();

app.Run();
