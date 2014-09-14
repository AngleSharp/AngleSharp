namespace AngleSharp.Network
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the required methods any requester object must have.
    /// </summary>
    public interface IRequester
    {
        /// <summary>
        /// Checks if the given protocol is supported.
        /// </summary>
        /// <param name="protocol">The protocol to check for, e.g. http.</param>
        /// <returns>True if the protocol is supported, otherwise false.</returns>
        Boolean SupportsProtocol(String protocol);

        /// <summary>
        /// Performs a blocking request with the given options.
        /// </summary>
        /// <param name="request">The options to consider.</param>
        /// <returns>The response data.</returns>
        IResponse Request(IRequest request);

        /// <summary>
        /// Performs an asynchronous request that can be cancelled.
        /// </summary>
        /// <param name="request">The options to consider.</param>
        /// <param name="cancellationToken">The token for cancelling the task.</param>
        /// <returns>The task that will eventually give the response data.</returns>
        Task<IResponse> RequestAsync(IRequest request, CancellationToken cancellationToken);
    }
}
