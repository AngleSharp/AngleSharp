namespace AngleSharp.Extensions
{
    using AngleSharp.Dom;
    using AngleSharp.Network;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Useful extensions for IRequester objects.
    /// </summary>
    [DebuggerStepThrough]
    static class RequesterExtensions
    {
        #region Loading

        /// <summary>
        /// Loads the given URI by using an asynchronous request.
        /// </summary>
        /// <param name="requesters">The requesters to try.</param>
        /// <param name="request">The data of the request to send.</param>
        /// <param name="cancel">
        /// The token which can be used to cancel the request.
        /// </param>
        /// <returns>
        /// The task which will eventually return the response.
        /// </returns>
        public static Task<IResponse> LoadAsync(this IEnumerable<IRequester> requesters, IRequest request, CancellationToken cancel)
        {
            foreach (var requester in requesters)
            {
                if (requester.SupportsProtocol(request.Address.Scheme))
                    return requester.RequestAsync(request, cancel);
            }

            return DefaultResponse;
        }

        #endregion

        #region Sending

        /// <summary>
        /// Loads the given URI by using an asynchronous request with the given
        /// method and body.
        /// </summary>
        /// <param name="loader">The document loader to use.</param>
        /// <param name="request">The request to issue.</param>
        /// <param name="cancel">
        /// The token which can be used to cancel the request.
        /// </param>
        /// <returns>
        /// The task which will eventually return the response.
        /// </returns>
        public static Task<IResponse> SendAsync(this IDocumentLoader loader, DocumentRequest request, CancellationToken cancel)
        {
            if (loader == null)
                return DefaultResponse;

            return loader.LoadAsync(request, cancel);
        }

        #endregion

        #region Fetching

        /// <summary>
        /// Performs a fetch from the given URI by using an asynchronous
        /// request.
        /// </summary>
        /// <param name="loader">The resource loader to use.</param>
        /// <param name="request">The request to issue.</param>
        /// <param name="cancel">
        /// The token which can be used to cancel the request.
        /// </param>
        /// <returns>
        /// The task which will eventually return the stream.
        /// </returns>
        public static Task<IResponse> FetchAsync(this IResourceLoader loader, ResourceRequest request, CancellationToken cancel)
        {
            if (loader == null)
                return DefaultResponse;

            return loader.LoadAsync(request, cancel);
        }

        /// <summary>
        /// Performs a potentially CORS-enabled fetch from the given URI by
        /// using an asynchronous GET request.
        /// </summary>
        /// <param name="loader">The resource loader to use.</param>
        /// <param name="request">The request to issue.</param>
        /// <param name="setting">The cross origin settings to use.</param>
        /// <param name="behavior">
        /// The default behavior in case it is undefined.
        /// </param>
        /// <param name="cancel">
        /// The token which can be used to cancel the request.
        /// </param>
        /// <returns>
        /// The task which will eventually return the stream.
        /// </returns>
        public static Task<IResponse> FetchWithCorsAsync(this IResourceLoader loader, ResourceRequest request, CorsSetting setting, OriginBehavior behavior, CancellationToken cancel)
        {
            if (loader == null)
                return DefaultResponse;

            //TODO
            //http://www.w3.org/TR/html5/infrastructure.html#potentially-cors-enabled-fetch
            return loader.LoadAsync(request, cancel);
        }

        #endregion

        #region Helpers

        static readonly Task<IResponse> DefaultResponse = CreateDefaultResponse();

        static Task<IResponse> CreateDefaultResponse()
        {
            var tcs = new TaskCompletionSource<IResponse>();
            tcs.SetResult(null);
            return tcs.Task;
        }

        #endregion
    }
}
