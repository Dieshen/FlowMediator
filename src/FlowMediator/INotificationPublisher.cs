namespace FlowMediator
{
    public interface INotificationPublisher
    {
        Task Publish(IEnumerable<NotificationHandlerExecutor> handlerExecutors,
            CancellationToken cancellationToken);
    }
}
