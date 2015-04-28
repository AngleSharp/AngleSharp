namespace AngleSharp.Extensions
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using AngleSharp.Dom;
    using AngleSharp.Events;
    using AngleSharp.Network;

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
        /// <param name="events">The event aggregator.</param>
        /// <param name="cancel">
        /// The token which can be used to cancel the request.
        /// </param>
        /// <returns>
        /// The task which will eventually return the response.
        /// </returns>
        public static async Task<IResponse> LoadAsync(this IEnumerable<IRequester> requesters, IRequest request, IEventAggregator events, CancellationToken cancel)
        {
            foreach (var requester in requesters)
            {
                if (requester.SupportsProtocol(request.Address.Scheme))
                {
                    var evt = new RequestStartEvent(requester, request);

                    if (events != null)
                        events.Publish(evt);

                    var result = await requester.RequestAsync(request, cancel).ConfigureAwait(false);
                    evt.SetResult(result);
                    return result;
                }
            }

            return default(IResponse);
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
        /// using an asynchronous GET request. For more information see:
        /// http://www.w3.org/TR/html5/infrastructure.html#potentially-cors-enabled-fetch
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
        public static async Task<IResponse> FetchWithCorsAsync(this IResourceLoader loader, ResourceRequest request, CorsSetting setting, OriginBehavior behavior, CancellationToken cancel)
        {
            var url = request.Target;

            if (request.Origin == url.Origin || url.Scheme == KnownProtocols.Data || url.Href == "about:blank")
            {
                while (true)
                {
                    var data = new ResourceRequest(request.Source, url)
                    {
                        Origin = request.Origin,
                        IsManualRedirectDesired = true
                    };

                    var result = await loader.LoadAsync(data, cancel).ConfigureAwait(false);

                    if (result.StatusCode == HttpStatusCode.Redirect ||
                        result.StatusCode == HttpStatusCode.RedirectKeepVerb ||
                        result.StatusCode == HttpStatusCode.RedirectMethod ||
                        result.StatusCode == HttpStatusCode.TemporaryRedirect ||
                        result.StatusCode == HttpStatusCode.MovedPermanently ||
                        result.StatusCode == HttpStatusCode.MultipleChoices)
                    {
                        url = new Url(result.Headers.GetOrDefault(HeaderNames.Location, url.Href));

                        if (request.Origin == url.Origin)
                        {
                            request = new ResourceRequest(request.Source, url)
                            {
                                IsCookieBlocked = request.IsCookieBlocked,
                                IsSameOriginForced = request.IsSameOriginForced,
                                Origin = request.Origin
                            };
                            return await loader.FetchWithCorsAsync(request, setting, behavior, cancel).ConfigureAwait(false);
                        }
                    }
                    else
                    {
                        return result;
                    }
                }
            }

            if (setting == CorsSetting.None)
            {
                if (behavior == OriginBehavior.Taint)
                    await loader.LoadAsync(request, cancel).ConfigureAwait(false);
            }

            if (setting == CorsSetting.Anonymous)
                request.IsCredentialOmitted = true;

            if (setting == CorsSetting.Anonymous || setting == CorsSetting.UseCredentials)
            {
                var result = await loader.FetchAsync(request, cancel).ConfigureAwait(false);

                //TODO If CORS cross-origin request is success
                if (result.StatusCode == HttpStatusCode.OK)
                    return result;
            }

            throw new DomException(DomError.Network);
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
