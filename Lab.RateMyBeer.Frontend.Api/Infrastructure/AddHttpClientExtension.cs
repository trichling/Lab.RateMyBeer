using System;
using System.Net.Http;
using Lab.RateMyBeer.Checkins.Contracts.Checkins.ApiClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestEase;

namespace Lab.RateMyBeer.Frontend.Api.Infrastructure;

public static class AddHttpClientExtension
{

    public static IServiceCollection AddHttpClientWithBaseUrl<T>(this IServiceCollection services, string baseUri) where T : class
    {
        services.AddHttpClient(typeof(T).Name)
            .ConfigurePrimaryHttpMessageHandler(p => 
            {
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, cetChain, policyErrors) => true
                };
                handler.ClientCertificateOptions = ClientCertificateOption.Manual;

                return handler;
            })
            .ConfigureHttpClient((_, client) => 
            {
                client.BaseAddress = new Uri(baseUri);
            });
        
        services.AddTransient(typeof(T), p => 
        {
            var client = p.GetRequiredService<IHttpClientFactory>().CreateClient(typeof(T).Name);
            return RestClient.For<T>(client);
        });
        return services;
    }
    
}