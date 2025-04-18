namespace FlowMediator
{
    /// <summary>
    /// Send a request through the mediator pipeline to be handled by a single handler.
    /// </summary>
    public interface ISender
    {
        /// <summary>
        /// Asynchronously send a request to a single handler
        /// </summary>
        /// <typeparam name="TRequest">Request type</typeparam>
        /// <typeparam name="TResponse">Response type</typeparam>
        /// <param name="request">Request object</param>
        /// <param name="cancellationToken">Optional cancellation token</param>
        /// <returns>A task that represents the send operation. The task result contains the handler response</returns>
        Task<TResponse> Send<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default)
            where TRequest : IRequest<TResponse>;

        /// <summary>
        /// Asynchronously send a request to a single handler with no response
        /// </summary>
        /// <param name="request">Request object</param>
        /// <param name="cancellationToken">Optional cancellation token</param>
        /// <returns>A task that represents the send operation.</returns>
        Task Send<TRequest>(TRequest request, CancellationToken cancellationToken = default)
            where TRequest : IRequest;
    }
}
