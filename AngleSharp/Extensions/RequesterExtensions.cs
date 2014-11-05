namespace AngleSharp.Extensions
{
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
        #region Request

        /// <summary>
        /// Performs an asynchronous http request with the given options without
        /// taking a custom cancellation token.
        /// </summary>
        /// <param name="requester">The requester to use.</param>
        /// <param name="request">The options to consider.</param>
        /// <returns>The task that will eventually give the response data.</returns>
        public static Task<IResponse> RequestAsync(this IRequester requester, IRequest request)
        {
            return requester.RequestAsync(request, CancellationToken.None);
        }

        #endregion

        #region Loading

        /// <summary>
        /// Loads the given URI by using an asynchronous GET request.
        /// </summary>
        /// <param name="requester">The requester to use.</param>
        /// <param name="url">The url that yields the path to the desired action.</param>
        /// <returns>The task which will eventually return the response.</returns>
        public static Task<IResponse> LoadAsync(this IRequester requester, Url url)
        {
            return requester.LoadAsync(url, CancellationToken.None);
        }

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

        #region Fetching

        /// <summary>
        /// Performs a potentially CORS-enabled fetch from the given URI by using an asynchronous GET request.
        /// </summary>
        /// <param name="requester">The requester to use.</param>
        /// <param name="url">The url that yields the path to the desired action.</param>
        /// <param name="cors">The cross origin settings to use.</param>
        /// <param name="origin">The origin of the page that requests the loading.</param>
        /// <param name="defaultBehavior">The default behavior in case it is undefined.</param>
        /// <returns>The task which will eventually return the stream.</returns>
        public static Task<IResponse> LoadWithCorsAsync(this IRequester requester, Url url, CorsSetting cors, String origin, OriginBehavior defaultBehavior)
        {
            return requester.LoadWithCorsAsync(url, cors, origin, defaultBehavior, CancellationToken.None);
        }

        /// <summary>
        /// Performs a potentially CORS-enabled fetch from the given URI by using an asynchronous GET request.
        /// </summary>
        /// <param name="requester">The requester to use.</param>
        /// <param name="url">The url that yields the path to the desired action.</param>
        /// <param name="cors">The cross origin settings to use.</param>
        /// <param name="origin">The origin of the page that requests the loading.</param>
        /// <param name="defaultBehavior">The default behavior in case it is undefined.</param>
        /// <param name="cancel">The token which can be used to cancel the request.</param>
        /// <returns>The task which will eventually return the stream.</returns>
        public static Task<IResponse> LoadWithCorsAsync(this IRequester requester, Url url, CorsSetting cors, String origin, OriginBehavior defaultBehavior, CancellationToken cancel)
        {
            //TODO
            //http://www.w3.org/TR/html5/infrastructure.html#potentially-cors-enabled-fetch
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
        /// <returns>The task which will eventually return the response.</returns>
        public static Task<IResponse> SendAsync(this IRequester requester, Url url, Stream content = null, String mimeType = null, HttpMethod method = HttpMethod.Post)
        {
            return requester.SendAsync(url, content, mimeType, method, CancellationToken.None);
        }

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
    }
}
