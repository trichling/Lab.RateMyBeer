using Lab.RateMyBeer.Ratings.Data.StarRatings;
using Microsoft.EntityFrameworkCore;

namespace Lab.RateMyBeer.Ratings.Api.StarRatings
{
    public static class StarRatingsModule
    {
        
    public static IServiceCollection RegisterStarRatingsModule(this IServiceCollection services, IConfiguration configuration)
    {
        var ratingsDbConnectionString = configuration.GetConnectionString("RatingsDbConnectionString");
        ratingsDbConnectionString = string.Format(ratingsDbConnectionString, "RatingsDb");

        services.AddDbContext<StarRatingContext>(options =>
            options.UseSqlServer(ratingsDbConnectionString));

        return services;
    }

    public static IEndpointRouteBuilder MapStarRatingsEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/ratings", GetStarRatings.Handle);
        endpoints.MapGet("/rating", GetStarRating.Handle);


        return endpoints;
    }

    }
}