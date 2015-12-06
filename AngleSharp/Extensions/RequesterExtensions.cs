namespace AngleSharp.Extensions
{
    using AngleSharp.Dom;
    using AngleSharp.Network;
    using System.Diagnostics;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Useful extensions for IRequester objects.
    /// </summary>
    [DebuggerStepThrough]
    static class RequesterExtensions
    {
        #region Sending

        /// <summary>
        /// Loads the given URI by using an asynchronous request with the given
        /// method and body.
        /// </summary>
        /// <param name="loader">The document loader to use.</param>
        /// <param name="request">The request to issue.</param>
        /// <param name="cancel">The cancellation token.</param>
        /// <returns>
        /// The task which will eventually return the response.
        /// </returns>
        public static Task<IResponse> SendAsync(this IDocumentLoader loader, DocumentRequest request, CancellationToken cancel)
        {
            if (loader != null)
            {
                var download = loader.DownloadAsync(request);
                cancel.Register(download.Cancel);
                return download.Task;
            }

            return TaskEx.FromResult(default(IResponse));
        }

        #endregion

        #region Fetching

        /// <summary>
        /// Performs a fetch from the given URI by using an asynchronous
        /// request.
        /// </summary>
        /// <param name="loader">The resource loader to use.</param>
        /// <param name="request">The request to issue.</param>
        /// <returns>
        /// The task which will eventually return the stream.
        /// </returns>
        public static Task<IResponse> FetchAsync(this IResourceLoader loader, ResourceRequest request)
        {
            return loader != null ? loader.DownloadAsync(request).Task : TaskEx.FromResult(default(IResponse));
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
        /// <returns>
        /// The task which will eventually return the stream.
        /// </returns>
        public static async Task<IResponse> FetchWithCorsAsync(this IResourceLoader loader, ResourceRequest request, CorsSetting setting, OriginBehavior behavior)
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

                    var result = await loader.DownloadAsync(data).Task.ConfigureAwait(false);

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
                            return await loader.FetchWithCorsAsync(request, setting, behavior).ConfigureAwait(false);
                        }
                    }
                    else
                    {
                        return result;
                    }
                }
            }
            else if (setting == CorsSetting.None)
            {
                if (behavior == OriginBehavior.Fail)
                    throw new DomException(DomError.Network);

                return await loader.DownloadAsync(request).Task.ConfigureAwait(false);
            }
            else if (setting == CorsSetting.Anonymous || setting == CorsSetting.UseCredentials)
            {
                request.IsCredentialOmitted = setting == CorsSetting.Anonymous;
                var result = await loader.FetchAsync(request).ConfigureAwait(false);

                if (result != null && result.StatusCode == HttpStatusCode.OK)
                    return result;
                else if (result != null)
                    result.Dispose();
            }

            throw new DomException(DomError.Network);
        }

        #endregion
    }
}
