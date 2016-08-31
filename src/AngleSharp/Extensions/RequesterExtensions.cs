namespace AngleSharp.Extensions
{
    using AngleSharp.Dom;
    using AngleSharp.Html;
    using AngleSharp.Network;
    using AngleSharp.Network.Default;
    using System;
    using System.IO;
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
        /// Checks if the status code corresponds to a redirected response.
        /// </summary>
        /// <param name="status">The given status code.</param>
        /// <returns>True if the status code hints redirection, otherwise false.</returns>
        public static Boolean IsRedirected(this HttpStatusCode status)
        {
            return status == HttpStatusCode.Redirect || status == HttpStatusCode.RedirectKeepVerb ||
                   status == HttpStatusCode.RedirectMethod || status == HttpStatusCode.TemporaryRedirect ||
                   status == HttpStatusCode.MovedPermanently || status == HttpStatusCode.MultipleChoices;
        }

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
                return loader.FetchFromSameOrigin(url, cors);
            }
            else if (setting == CorsSetting.Anonymous || setting == CorsSetting.UseCredentials)
            {
                return loader.FetchFromDifferentOrigin(cors);
            }
            else if (setting == CorsSetting.None)
            {
                return loader.FetchWithoutCors(request, cors.Behavior);
            }

            throw new DomException(DomError.Network);
        }

        #endregion

        #region Fetching

        private static IDownload FetchFromSameOrigin(this IResourceLoader loader, Url url, CorsRequest cors)
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

                    return request.Origin.Is(url.Origin) ?
                        loader.FetchWithCors(cors.RedirectTo(url)) :
                        loader.FetchFromSameOrigin(url, cors);
                }

                return cors.CheckIntegrity(download);
            });
        }

        private static IDownload FetchFromDifferentOrigin(this IResourceLoader loader, CorsRequest cors)
        {
            var request = cors.Request;
            request.IsCredentialOmitted = cors.IsAnonymous();
            var download = loader.DownloadAsync(request);
            return download.Wrap(response =>
            {
                if (response?.StatusCode != HttpStatusCode.OK)
                {
                    response?.Dispose();
                    throw new DomException(DomError.Network);
                }

                return cors.CheckIntegrity(download);
            });
        }

        private static IDownload FetchWithoutCors(this IResourceLoader loader, ResourceRequest request, OriginBehavior behavior)
        {
            if (behavior == OriginBehavior.Fail)
                throw new DomException(DomError.Network);

            return loader.DownloadAsync(request);
        }

        #endregion

        #region Helpers

        private static Boolean IsAnonymous(this CorsRequest cors)
        {
            return cors.Setting == CorsSetting.Anonymous;
        }

        private static IDownload Wrap(this IDownload download, Func<IResponse, IDownload> callback)
        {
            var cts = new CancellationTokenSource();
            var task = download.Task.Wrap(callback);
            return new Download(task, cts, download.Target, download.Originator);
        }

        private static IDownload Wrap(this IDownload download, IResponse response)
        {
            var cts = new CancellationTokenSource();
            var task = TaskEx.FromResult(response);
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
            return (response?.StatusCode ?? HttpStatusCode.NotFound).IsRedirected();
        }

        private static CorsRequest RedirectTo(this CorsRequest cors, Url url)
        {
            var oldRequest = cors.Request;
            var newRequest = new ResourceRequest(oldRequest.Source, url)
            {
                IsCookieBlocked = oldRequest.IsCookieBlocked,
                IsSameOriginForced = oldRequest.IsSameOriginForced,
                Origin = oldRequest.Origin
            };
            return new CorsRequest(newRequest)
            {
                Setting = cors.Setting,
                Behavior = cors.Behavior,
                Integrity = cors.Integrity
            };
        }

        private static IDownload CheckIntegrity(this CorsRequest cors, IDownload download)
        {
            var response = download.Task.Result;
            var value = cors.Request.Source?.GetAttribute(AttributeNames.Integrity);
            var integrity = cors.Integrity;

            if (!String.IsNullOrEmpty(value) && integrity != null && response != null)
            {
                var content = new MemoryStream();
                response.Content.CopyTo(content);
                content.Position = 0;

                if (!integrity.IsSatisfied(content.ToArray(), value))
                {
                    response.Dispose();
                    throw new DomException(DomError.Security);
                }

                return download.Wrap(new Response
                {
                    Address = response.Address,
                    Content = content,
                    Headers = response.Headers,
                    StatusCode = response.StatusCode
                });
            }

            return download;
        }

        #endregion
    }
}
