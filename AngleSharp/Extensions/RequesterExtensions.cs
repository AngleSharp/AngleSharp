namespace AngleSharp.Extensions
{
    using AngleSharp.Dom;
    using AngleSharp.Network;
    using System;
    using System.Diagnostics;
    using System.Net;
    using System.Threading.Tasks;

    /// <summary>
    /// Useful extensions for IRequester objects.
    /// </summary>
    [DebuggerStepThrough]
    static class RequesterExtensions
    {
        #region Methods

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
        public static async Task<IDownload> FetchWithCorsAsync(this IResourceLoader loader, ResourceRequest request, CorsSetting setting, OriginBehavior behavior)
        {
            var url = request.Target;

            if (request.Origin == url.Origin || url.Scheme == ProtocolNames.Data || url.Href == "about:blank")
            {
                while (true)
                {
                    var download = loader.DownloadAsync(new ResourceRequest(request.Source, url)
                    {
                        Origin = request.Origin,
                        IsManualRedirectDesired = true
                    });
                    var response = await download.Task.ConfigureAwait(false);

                    if (response.IsRedirected())
                    {
                        url.Href = response.Headers.GetOrDefault(HeaderNames.Location, url.Href);

                        if (request.Origin.Is(url.Origin))
                        {
                            return await loader.FetchWithCorsAsync(new ResourceRequest(request.Source, url)
                            {
                                IsCookieBlocked = request.IsCookieBlocked,
                                IsSameOriginForced = request.IsSameOriginForced,
                                Origin = request.Origin
                            }, setting, behavior).ConfigureAwait(false);
                        }
                    }
                    else
                    {
                        return download;
                    }
                }
            }
            else if (setting == CorsSetting.None)
            {
                if (behavior == OriginBehavior.Fail)
                {
                    throw new DomException(DomError.Network);
                }

                return loader.DownloadAsync(request);
            }
            else if (setting == CorsSetting.Anonymous || setting == CorsSetting.UseCredentials)
            {
                request.IsCredentialOmitted = setting == CorsSetting.Anonymous;
                var download = loader.DownloadAsync(request);
                var result = await download.Task.ConfigureAwait(false);

                if (result != null && result.StatusCode == HttpStatusCode.OK)
                {
                    return download;
                }
                else if (result != null)
                {
                    result.Dispose();
                }
            }

            throw new DomException(DomError.Network);
        }

        #endregion

        #region Helpers

        static Boolean IsRedirected(this IResponse response)
        {
            var status = response != null ? response.StatusCode : HttpStatusCode.NotFound;
            return status == HttpStatusCode.Redirect || status == HttpStatusCode.RedirectKeepVerb ||
                   status == HttpStatusCode.RedirectMethod || status == HttpStatusCode.TemporaryRedirect ||
                   status == HttpStatusCode.MovedPermanently || status == HttpStatusCode.MultipleChoices;
        }

        #endregion
    }
}
