namespace AngleSharp.Extensions
{
    using AngleSharp.Dom;
    using AngleSharp.Network;
    using System;
    using System.Diagnostics;
    using System.IO;
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
        /// Loads the given URI by using an asynchronous GET request.
        /// </summary>
        /// <param name="requester">The requester to use.</param>
        /// <param name="url">The url that yields the path to the desired action.</param>
        /// <param name="cancel">The token which can be used to cancel the request.</param>
        /// <returns>The task which will eventually return the response.</returns>
        public static Task<IResponse> LoadAsync(this IRequester requester, Url url, CancellationToken cancel)
        {
            return requester.RequestAsync(new DefaultRequest
            {
                Address = url,
                Method = HttpMethod.Get
            }, cancel);
        }

        #endregion

        #region Sending

        /// <summary>
        /// Loads the given URI by using an asynchronous request with the given method and body.
        /// </summary>
        /// <param name="requester">The requester to use.</param>
        /// <param name="url">The url that yields the path to the desired action.</param>
        /// <param name="content">The body that should be used in the request.</param>
        /// <param name="mimeType">The mime-type of the request.</param>
        /// <param name="method">The method that is used for sending the request asynchronously.</param>
        /// <param name="cancel">The token which can be used to cancel the request.</param>
        /// <returns>The task which will eventually return the response.</returns>
        public static Task<IResponse> SendAsync(this IRequester requester, Url url, Stream content, String mimeType, HttpMethod method, CancellationToken cancel)
        {
            var request = new DefaultRequest
            {
                Address = url,
                Content = content,
                Method = method
            };

            if (mimeType != null)
                request.Headers[HeaderNames.ContentType] = mimeType;

            return requester.RequestAsync(request, cancel);
        }

        #endregion

        #region Resources

        /// <summary>
        /// Performs a fetch from the given URI by using an asynchronous GET
        /// request.
        /// </summary>
        /// <param name="loader">The resource loader to use.</param>
        /// <param name="request">The request to issue.</param>
        /// <param name="cancel">
        /// The token which can be used to cancel the request.
        /// </param>
        /// <returns>The task which will eventually return the stream.</returns>
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
