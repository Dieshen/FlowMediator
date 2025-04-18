namespace FlowMediator.NotificationPublishers
{
    /// <summary>
    /// Uses Task.WhenAll with the list of Handler tasks:
    /// <code>
    /// var tasks = handlers
    ///                .Select(handler => handler.Handle(notification, cancellationToken))
    ///                .ToList();
    /// 
    /// return Task.WhenAll(tasks);
    /// </code>
    /// </summary>
    public class TaskWhenAllPublisher : INotificationPublisher
    {
        public Task Publish(IEnumerable<NotificationHandlerExecutor> handlerExecutors, CancellationToken cancellationToken)
        {
            Task[] tasks = handlerExecutors
                .Select(handler => handler(cancellationToken))
                .ToArray();

            return Task.WhenAll(tasks);
        }
    }
}
