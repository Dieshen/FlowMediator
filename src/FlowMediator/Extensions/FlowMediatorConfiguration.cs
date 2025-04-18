using FlowMediator;
using FlowMediator.NotificationPublishers;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    public class FlowMediatorConfiguration
    {
        /// <summary>
        /// Optional filter for types to register. Default value is a function returning true.
        /// </summary>
        public Func<Type, bool> TypeEvaluator { get; set; } = t => true;

        /// <summary>
        /// Strategy for publishing notifications. Defaults to <see cref="ForeachAwaitPublisher"/>
        /// </summary>
        public INotificationPublisher NotificationPublisher { get; set; } = new ForeachAwaitPublisher();

        /// <summary>
        /// Type of notification publisher strategy to register. If set, overrides <see cref="NotificationPublisher"/>
        /// </summary>
        public Type? NotificationPublisherType { get; set; }

        /// <summary>
        /// Service lifetime to register services under. Default value is <see cref="ServiceLifetime.Transient"/>
        /// </summary>
        public ServiceLifetime Lifetime { get; set; } = ServiceLifetime.Transient;

        /// <summary>
        /// Request exception action processor strategy. Default value is <see cref="DependencyInjection.RequestExceptionActionProcessorStrategy.ApplyForUnhandledExceptions"/>
        /// </summary>
        //public RequestExceptionActionProcessorStrategy RequestExceptionActionProcessorStrategy { get; set; }
        //    = RequestExceptionActionProcessorStrategy.ApplyForUnhandledExceptions;

        internal List<Assembly> AssembliesToRegister { get; } = [];

        /// <summary>
        /// List of behaviors to register in specific order
        /// </summary>
        public List<ServiceDescriptor> BehaviorsToRegister { get; } = [];

        /// <summary>
        /// List of stream behaviors to register in specific order
        /// </summary>
        public List<ServiceDescriptor> StreamBehaviorsToRegister { get; } = [];

        /// <summary>
        /// List of request pre processors to register in specific order
        /// </summary>
        public List<ServiceDescriptor> RequestPreProcessorsToRegister { get; } = [];

        /// <summary>
        /// List of request post processors to register in specific order
        /// </summary>
        public List<ServiceDescriptor> RequestPostProcessorsToRegister { get; } = [];


        /// <summary>
        /// Flag that controls whether MediatR will attempt to register handlers that containing generic type parameters.
        /// </summary>
        public bool RegisterGenericHandlers { get; set; } = false;
    }
}
