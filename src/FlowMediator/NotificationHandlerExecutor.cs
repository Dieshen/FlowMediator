namespace FlowMediator
{
    /// <summary>
    /// Represents an async continuation for the next task to execute in the pipeline
    /// </summary>
    /// <returns>Awaitable task</returns>
    public delegate Task NotificationHandlerExecutor(CancellationToken t = default);
}
