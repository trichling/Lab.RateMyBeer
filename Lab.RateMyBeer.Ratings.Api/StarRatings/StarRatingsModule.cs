namespace Lab.RateMyBeer.Ratings.Api.StarRatings
{
    public static class StarRatingsModule
    {
        
    public static IServiceCollection RegisterStarRatingsModule(this IServiceCollection services, IConfiguration configuration)
    {
        var ratingsDbConnectionString = configuration.GetConnectionString("RatingsDbConnectionString");
        return services;
    }

    public static IEndpointRouteBuilder MapStarRatingsEndpoints(this IEndpointRouteBuilder endpoints)
    {
        return endpoints;
    }

    }
}