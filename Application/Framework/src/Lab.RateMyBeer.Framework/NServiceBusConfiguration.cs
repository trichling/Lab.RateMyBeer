using Microsoft.Extensions.Hosting;

namespace Lab.RateMyBeer.Framework;

public static class  NServiceBusConfiguration
{
    public static void Configure(this EndpointConfiguration configuration, HostBuilderContext context, Action<RoutingSettings> configureRouting)
    {
        var transport = configuration.UseTransport<AzureServiceBusTransport>();
        var transportConnectionString =
            context.Configuration["Dependencies:NServiceBus:TransportConnectionString"];
        transport.ConnectionString(transportConnectionString);
        transport.SubscriptionRuleNamingConvention(t => t.Name);

        var routing = transport.Routing();
        configureRouting(routing);
        
        configuration.UsePersistence<LearningPersistence>();
        configuration.UseSerialization<NewtonsoftJsonSerializer>();
        configuration.Conventions()
            .DefiningMessagesAs(t => t.Namespace.Contains("Messages"))
            .DefiningCommandsAs(t => t.Namespace.EndsWith("Commands"))
            .DefiningEventsAs(t => t.Namespace.EndsWith("Events"));

        configuration.EnableInstallers();
    }
}