namespace AngleSharp.Network.Default
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Events;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents the base class for all loaders.
    /// </summary>
    public abstract class BaseLoader : ILoader
    {
        #region Fields
        
        private readonly IBrowsingContext _context;
        private readonly Predicate<IRequest> _filter;
        private readonly List<IDownload> _downloads;
        
        #endregion

        #region ctor

        /// <summary>
        /// Creates a new resource loader.
        /// </summary>
        /// <param name="context">The context to use.</param>
        /// <param name="filter">The optional request filter to use.</param>
        public BaseLoader(IBrowsingContext context, Predicate<IRequest> filter)
        {
            _context = context;
            _filter = filter ?? (_ => true);
            _downloads = new List<IDownload>();
            MaxRedirects = 50;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the maximum number of redirects. By default this is 50.
        /// </summary>
        public Int32 MaxRedirects
        {
            get;
            protected set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds the download to the active downloads.
        /// </summary>
        /// <param name="download">The download to add.</param>
        protected virtual void Add(IDownload download)
        {
            lock (this)
            {
                _downloads.Add(download);
            }
        }

        /// <summary>
        /// Removes the download from the active downloads.
        /// </summary>
        /// <param name="download">The download to remove.</param>
        protected virtual void Remove(IDownload download)
        {
            lock (this)
            {
                _downloads.Remove(download);
            }
        }

        /// <summary>
        /// Gets the cookie string for the given URL.
        /// </summary>
        /// <param name="url">The requested URL.</param>
        /// <returns>The associated cookie string, if any.</returns>
        protected virtual String GetCookie(Url url)
        {
            return _context.Configuration.GetCookie(url.Origin);
        }

        /// <summary>
        /// Sets the cookie string for the given URL.
        /// </summary>
        /// <param name="url">The requested URL.</param>
        /// <param name="value">The value of the cookie.</param>
        protected virtual void SetCookie(Url url, String value)
        {
            _context.Configuration.SetCookie(url.Origin, value);
        }

        /// <summary>
        /// Starts downloading the request.
        /// </summary>
        /// <param name="request">The request data.</param>
        /// <param name="originator">The request's originator.</param>
        /// <returns>The active download.</returns>
        protected virtual IDownload DownloadAsync(Request request, INode originator)
        {
            var cancel = new CancellationTokenSource();

            if (_filter.Invoke(request))
            {
                var task = LoadAsync(request, cancel.Token);
                var download = new Download(task, cancel, request.Address, originator);
                Add(download);
                task.ContinueWith(m => Remove(download));
                return download;
            }

            return new Download(TaskEx.FromResult(default(IResponse)), cancel, request.Address, originator);
        }

        /// <summary>
        /// Gets the active downloads.
        /// </summary>
        /// <returns>The enumerable over all active downloads.</returns>
        public IEnumerable<IDownload> GetDownloads()
        {
            lock (this)
            {
                return _downloads.ToArray();
            }
        }

        /// <summary>
        /// Loads the given URI by using an asynchronous request.
        /// </summary>
        /// <param name="request">The data of the request to send.</param>
        /// <param name="cancel">The cancellation token to use..</param>
        /// <returns>
        /// The task which will eventually return the response.
        /// </returns>
        protected async Task<IResponse> LoadAsync(Request request, CancellationToken cancel)
        {
            var requesters = _context.Configuration.GetServices<IRequester>();
            var response = default(IResponse);
            var redirectCount = 0;
            AppendCookieTo(request);

            do
            {
                if (response != null)
                {
                    redirectCount++;
                    ExtractCookieFrom(response);
                    request = CreateNewRequest(request, response);
                    AppendCookieTo(request);
                }

                foreach (var requester in requesters)
                {
                    if (requester.SupportsProtocol(request.Address.Scheme))
                    {
                        _context.Fire(new RequestEvent(request, null));
                        response = await requester.RequestAsync(request, cancel).ConfigureAwait(false);
                        _context.Fire(new RequestEvent(request, response));
                        break;
                    }
                }
            }
            while (response != null && response.StatusCode.IsRedirected() && redirectCount < MaxRedirects);

            return response;
        }

        /// <summary>
        /// Creates a new request based on the existing request and given response.
        /// </summary>
        /// <param name="request">The previous request.</param>
        /// <param name="response">The response to the previous request.</param>
        /// <returns>The new request to issue.</returns>
        protected static Request CreateNewRequest(IRequest request, IResponse response)
        {
            var method = request.Method;
            var content = request.Content;
            var headers = new Dictionary<String, String>(request.Headers);
            var location = response.Headers[HeaderNames.Location];

            if (response.StatusCode == HttpStatusCode.Redirect || response.StatusCode == HttpStatusCode.RedirectMethod)
            {
                method = HttpMethod.Get;
                content = Stream.Null;
            }
            else if (content.Length > 0)
            {
                content.Position = 0;
            }

            headers.Remove(HeaderNames.Cookie);

            return new Request
            {
                Address = new Url(request.Address, location),
                Method = method,
                Content = content,
                Headers = headers
            };
        }

        #endregion

        #region Helpers

        private void AppendCookieTo(Request request)
        {
            var cookie = GetCookie(request.Address);

            if (cookie != null)
            {
                request.Headers[HeaderNames.Cookie] = cookie;
            }
        }

        private void ExtractCookieFrom(IResponse response)
        {
            var cookie = response.Headers.GetOrDefault(HeaderNames.SetCookie, null);

            if (cookie != null)
            {
                SetCookie(response.Address, cookie);
            }
        }

        #endregion
    }
}
