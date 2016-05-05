namespace AngleSharp.Extensions
{
    using AngleSharp.Dom;
    using AngleSharp.Network;
    using System;
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
        /// The task that will eventually give the resource's response data.
        /// </returns>
        public static IDownload FetchWithCors(this IResourceLoader loader, ResourceRequest request, CorsSetting setting, OriginBehavior behavior)
        {
            var url = request.Target;

            if (request.Origin == url.Origin || url.Scheme == ProtocolNames.Data || url.Href == "about:blank")
            {
                return loader.FetchWithCors(url, request, setting, behavior);
            }
            else if (setting == CorsSetting.Anonymous || setting == CorsSetting.UseCredentials)
            {
                return loader.FetchWithCors(request, setting);
            }
            else if (setting == CorsSetting.None)
            {
                return loader.FetchWithoutCors(request, behavior);
            }

            throw new DomException(DomError.Network);
        }

        static IDownload FetchWithCors(this IResourceLoader loader, Url url, ResourceRequest request, CorsSetting setting, OriginBehavior behavior)
        {
            var download = loader.DownloadAsync(new ResourceRequest(request.Source, url)
            {
                Origin = request.Origin,
                IsManualRedirectDesired = true
            });

            return download.Wrap(response =>
            {
                if (response.IsRedirected())
                {
                    url.Href = response.Headers.GetOrDefault(HeaderNames.Location, url.Href);

                    if (request.Origin.Is(url.Origin))
                    {
                        return loader.FetchWithCors(new ResourceRequest(request.Source, url)
                        {
                            IsCookieBlocked = request.IsCookieBlocked,
                            IsSameOriginForced = request.IsSameOriginForced,
                            Origin = request.Origin
                        }, setting, behavior);
                    }

                    return loader.FetchWithCors(url, request, setting, behavior);
                }
                else
                {
                    return download;
                }
            });
        }

        static IDownload FetchWithoutCors(this IResourceLoader loader, ResourceRequest request, OriginBehavior behavior)
        {
            if (behavior == OriginBehavior.Fail)
            {
                throw new DomException(DomError.Network);
            }

            return loader.DownloadAsync(request);
        }

        static IDownload FetchWithCors(this IResourceLoader loader, ResourceRequest request, CorsSetting setting)
        {
            request.IsCredentialOmitted = setting == CorsSetting.Anonymous;
            var download = loader.DownloadAsync(request);
            return download.Wrap(response =>
            {
                if (response != null && response.StatusCode == HttpStatusCode.OK)
                {
                    return download;
                }
                else if (response != null)
                {
                    response.Dispose();
                }

                throw new DomException(DomError.Network);
            });
        }

        static IDownload Wrap(this IDownload download, Func<IResponse, IDownload> callback)
        {
            var cts = new CancellationTokenSource();
            var task = download.Task.Wrap(callback);
            return new Download(task, cts, download.Target, download.Originator);
        }

        static async Task<IResponse> Wrap(this Task<IResponse> task, Func<IResponse, IDownload> callback)
        {
            var response = await task.ConfigureAwait(false);
            var download = callback(response);
            return await download.Task.ConfigureAwait(false);
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
