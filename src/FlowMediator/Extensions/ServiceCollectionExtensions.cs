using FlowMediator.Registration;

namespace Microsoft.Extensions.DependencyInjection
{
    public static partial class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFlowMediator(this IServiceCollection services, Action<FlowMediatorConfiguration> configure)
        {
            FlowMediatorConfiguration configuration = new();
            configure.Invoke(configuration);
            return services.AddFlowMediator(configuration);
        }

        public static IServiceCollection AddFlowMediator(this IServiceCollection services, FlowMediatorConfiguration configuration)
        {
            if (!configuration.AssembliesToRegister.Any())
                throw new ArgumentException("No assemblies found to scan. Supply at least one assembly to scan for handlers.");

            ServiceRegistrar.SetGenericRequestHandlerRegistrationLimitations(configuration);

            ServiceRegistrar.AddMediatRClassesWithTimeout(services, configuration);

            ServiceRegistrar.AddRequiredServices(services, configuration);

            return services;
        }
    }
}
