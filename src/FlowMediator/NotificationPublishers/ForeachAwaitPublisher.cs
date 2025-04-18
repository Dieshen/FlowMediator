namespace FlowMediator.NotificationPublishers
{
    /// <summary>
    /// Awaits each notification handler in a single foreach loop:
    /// <code>
    /// foreach (var handler in handlers) {
    ///     await handler(notification, cancellationToken);
    /// }
    /// </code>
    /// </summary>
    public class ForeachAwaitPublisher : INotificationPublisher
    {
        public async Task Publish(IEnumerable<NotificationHandlerExecutor> handlerExecutors, CancellationToken cancellationToken)
        {
            foreach (NotificationHandlerExecutor handler in handlerExecutors)
            {
                await handler(cancellationToken).ConfigureAwait(false);
            }
        }
    }
}
