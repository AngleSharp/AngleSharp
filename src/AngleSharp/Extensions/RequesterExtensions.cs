namespace AngleSharp.Extensions
{
    using AngleSharp.Dom;
    using AngleSharp.Network;
    using System;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Useful extensions for IRequester objects.
    /// </summary>
    static class RequesterExtensions
    {
        #region Methods

        /// <summary>
        /// Performs a potentially CORS-enabled fetch from the given URI by
        /// using an asynchronous GET request. For more information see:
        /// http://www.w3.org/TR/html5/infrastructure.html#potentially-cors-enabled-fetch
        /// </summary>
        /// <param name="loader">The resource loader to use.</param>
        /// <param name="cors">The CORS request to issue.</param>
        /// <returns>
        /// The active download.
        /// </returns>
        public static IDownload FetchWithCors(this IResourceLoader loader, CorsRequest cors)
        {
            var request = cors.Request;
            var setting = cors.Setting;
            var url = request.Target;

            if (request.Origin == url.Origin || url.Scheme == ProtocolNames.Data || url.Href == "about:blank")
            {
                return loader.FetchWithCors(url, cors);
            }
            else if (setting == CorsSetting.Anonymous || setting == CorsSetting.UseCredentials)
            {
                return loader.FetchWithCors(request, setting);
            }
            else if (setting == CorsSetting.None)
            {
                return loader.FetchWithoutCors(request, cors.Behavior);
            }

            throw new DomException(DomError.Network);
        }

        #endregion

        #region Fetching

        private static IDownload FetchWithCors(this IResourceLoader loader, Url url, CorsRequest cors)
        {
            var request = cors.Request;
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
                        var newRequest = new ResourceRequest(request.Source, url)
                        {
                            IsCookieBlocked = request.IsCookieBlocked,
                            IsSameOriginForced = request.IsSameOriginForced,
                            Origin = request.Origin
                        };
                        return loader.FetchWithCors(new CorsRequest(newRequest)
                        {
                            Setting = cors.Setting,
                            Behavior = cors.Behavior
                        });
                    }

                    return loader.FetchWithCors(url, cors);
                }

                return download;
            });
        }

        private static IDownload FetchWithoutCors(this IResourceLoader loader, ResourceRequest request, OriginBehavior behavior)
        {
            if (behavior == OriginBehavior.Fail)
                throw new DomException(DomError.Network);

            return loader.DownloadAsync(request);
        }

        private static IDownload FetchWithCors(this IResourceLoader loader, ResourceRequest request, CorsSetting setting)
        {
            request.IsCredentialOmitted = setting == CorsSetting.Anonymous;
            var download = loader.DownloadAsync(request);
            return download.Wrap(response =>
            {
                if (response?.StatusCode == HttpStatusCode.OK)
                {
                    return download;
                }

                response?.Dispose();
                throw new DomException(DomError.Network);
            });
        }

        #endregion

        #region Helpers

        private static IDownload Wrap(this IDownload download, Func<IResponse, IDownload> callback)
        {
            var cts = new CancellationTokenSource();
            var task = download.Task.Wrap(callback);
            return new Download(task, cts, download.Target, download.Originator);
        }

        private static async Task<IResponse> Wrap(this Task<IResponse> task, Func<IResponse, IDownload> callback)
        {
            var response = await task.ConfigureAwait(false);
            var download = callback(response);
            return await download.Task.ConfigureAwait(false);
        }

        private static Boolean IsRedirected(this IResponse response)
        {
            var status = response?.StatusCode ?? HttpStatusCode.NotFound;
            return status == HttpStatusCode.Redirect || status == HttpStatusCode.RedirectKeepVerb ||
                   status == HttpStatusCode.RedirectMethod || status == HttpStatusCode.TemporaryRedirect ||
                   status == HttpStatusCode.MovedPermanently || status == HttpStatusCode.MultipleChoices;
        }

        #endregion
    }
}
