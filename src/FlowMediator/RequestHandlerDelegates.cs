namespace FlowMediator
{
    /// <summary>
    /// Represents an async continuation for the next task to execute in the pipeline
    /// </summary>
    /// <typeparam name="TResponse">Response type</typeparam>
    /// <returns>Awaitable task returning a <typeparamref name="TResponse"/></returns>
    public delegate Task<TResponse> RequestHandlerDelegate<TResponse>(CancellationToken t = default);

    /// <summary>
    /// Represents an async continuation for the next task to execute in the pipeline
    /// </summary>
    /// <returns>Awaitable task</returns>
    public delegate Task RequestHandlerDelegate(CancellationToken t = default);
}
