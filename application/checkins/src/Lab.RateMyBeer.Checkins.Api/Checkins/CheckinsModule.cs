using Lab.RateMyBeer.Checkins.Data.Checkins;
using Microsoft.EntityFrameworkCore;

namespace Lab.RateMyBeer.Checkins.Api.Checkins;

public static class CheckinsModule
{
    
    public static IServiceCollection RegisterCheckinsModule(this IServiceCollection services, IConfiguration configuration)
    {
        var checkinDbConnectionString = configuration.GetConnectionString("CheckinsDbConnectionString");
        checkinDbConnectionString = string.Format(checkinDbConnectionString, "checkinsDb");
        services.AddDbContext<CheckinsContext>(options =>
        {
            options.UseSqlServer(checkinDbConnectionString);
        });
        return services;
    }

    public static IEndpointRouteBuilder MapCheckinsEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/checkins", GetCheckins.Handle);
        endpoints.MapGet("/checkinsByIds", GetCheckinsByIds.Handle);
        
        return endpoints;
    }

}