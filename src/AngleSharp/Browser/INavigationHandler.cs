namespace AngleSharp.Browser
{
    using AngleSharp.Dom;
    using AngleSharp.Io;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines a handler for navigation requests.
    /// </summary>
    public interface INavigationHandler
    {
        /// <summary>
        /// Determines if the given protocol is supported by
        /// the current handler.
        /// </summary>
        /// <param name="protocol">The protocol of the navigation target.</param>
        /// <returns>True if the protocol is supported, otherwise false.</returns>
        Boolean SupportsProtocol(String protocol);

        /// <summary>
        /// Performs the navigation with respect to a given request.
        /// </summary>
        /// <param name="request">The navigation request.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>
        /// The task yielding a document representing the navigation result.
        /// </returns>
        Task<IDocument> NavigateAsync(DocumentRequest request, CancellationToken token);
    }
}
