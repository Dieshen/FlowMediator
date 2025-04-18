using FlowMediator.NotificationPublishers;
using Microsoft.Extensions.DependencyInjection;

namespace FlowMediator
{
    internal sealed class Mediator : IMediator
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly INotificationPublisher _publisher;

        /// <summary>
        /// Initializes a new instance of the <see cref="Mediator"/> class.
        /// </summary>
        /// <param name="serviceProvider">Service provider. Can be a scoped or root provider</param>
        public Mediator(IServiceProvider serviceProvider) :
            this(serviceProvider, new ForeachAwaitPublisher())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Mediator"/> class.
        /// </summary>
        /// <param name="serviceProvider">Service provider. Can be a scoped or root provider</param>
        /// <param name="publisher">Notification publisher. Defaults to <see cref="ForeachAwaitPublisher"/>.</param>
        public Mediator(IServiceProvider serviceProvider, INotificationPublisher publisher)
        {
            _serviceProvider = serviceProvider;
            _publisher = publisher;
        }

        public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
            where TNotification : INotification
        {
            IEnumerable<INotificationHandler<TNotification>>? handlers = _serviceProvider.GetServices<INotificationHandler<TNotification>>();
            if (handlers is null || !handlers.Any())
                throw new InvalidOperationException($"No handler registered for {typeof(TNotification).Name}");

            List<NotificationHandlerExecutor> handlerDelegates = [];
            foreach (INotificationHandler<TNotification> handler in handlers)
            {
                NotificationHandlerExecutor handlerDelegate = t => handler.Handle(notification, t);
                handlerDelegates.Add(handlerDelegate);
            }

            return _publisher.Publish(handlerDelegates, cancellationToken);
        }

        public Task<TResponse> Send<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default)
            where TRequest : IRequest<TResponse>
        {
            IRequestHandler<TRequest, TResponse>? handler = _serviceProvider.GetService<IRequestHandler<TRequest, TResponse>>();
            if (handler is null)
                throw new InvalidOperationException($"No handler registered for {typeof(TRequest).Name}");

            IEnumerable<IPipelineBehavior<TRequest, TResponse>> behaviors = _serviceProvider.GetServices<IPipelineBehavior<TRequest, TResponse>>()
                                                                                            .OrderBy(b => b.Order)
                                                                                            .Reverse();
            RequestHandlerDelegate<TResponse> handlerDelegate = (t) => handler.Handle(request, t);
            foreach (IPipelineBehavior<TRequest, TResponse>? behavior in behaviors)
            {
                RequestHandlerDelegate<TResponse> next = handlerDelegate;
                handlerDelegate = t => behavior.Handle(request, next, t);
            }
            return handlerDelegate(cancellationToken);
        }

        public Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = default)
            where TRequest : IRequest
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            IRequestHandler<TRequest>? handler = _serviceProvider.GetService<IRequestHandler<TRequest>>();
            if (handler is null)
                throw new InvalidOperationException($"No handler registered for {typeof(TRequest).Name}");

            RequestHandlerDelegate handlerDelegate = t => handler.Handle(request, t);
            return handlerDelegate(cancellationToken);
        }
    }
}
