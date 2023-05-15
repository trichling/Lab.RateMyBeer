using Microsoft.Extensions.Configuration;

namespace Lab.RateMyBeer.Frontend.Shared;

public static class Api
{
    public static string BaseUrl(IConfiguration configuration, string environment) => 
        environment.StartsWith("Kubernetes") ?
        "/api/checkins" :    
        configuration["Dependencies:APIs:CheckinsApiBaseUrl"];
}