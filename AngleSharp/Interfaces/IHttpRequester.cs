namespace AngleSharp.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the required methods any Http requester object must have.
    /// </summary>
    public interface IHttpRequester
    {
        /// <summary>
        /// Performs a blocking http request with the given options.
        /// </summary>
        /// <param name="request">The options to consider.</param>
        /// <returns>The response data.</returns>
        IHttpResponse Request(IHttpRequest request);

        /// <summary>
        /// Performs an asynchronous http request with the given options.
        /// </summary>
        /// <param name="request">The options to consider.</param>
        /// <returns>The task that will eventually give the response data.</returns>
        Task<IHttpResponse> RequestAsync(IHttpRequest request);

        /// <summary>
        /// Performs an asynchronous http request that can be cancelled.
        /// </summary>
        /// <param name="request">The options to consider.</param>
        /// <param name="cancellationToken">The token for cancelling the task.</param>
        /// <returns>The task that will eventually give the response data.</returns>
        Task<IHttpResponse> RequestAsync(IHttpRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Gets or sets the default headers to send with every request.
        /// </summary>
        Dictionary<String, String> Headers { get; }

        /// <summary>
        /// Gets or sets the number of milliseconds to wait before the request times out.
        /// </summary>
        TimeSpan Timeout { get; set; }
    }
}
