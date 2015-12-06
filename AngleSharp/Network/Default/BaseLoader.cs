namespace AngleSharp.Network.Default
{
    using AngleSharp.Dom;
    using AngleSharp.Events;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents the base class for all loaders.
    /// </summary>
    public abstract class BaseLoader : ILoader
    {
        readonly IEnumerable<IRequester> _requesters;
        readonly IConfiguration _configuration;
        readonly Predicate<IRequest> _filter;
        readonly List<IDownload> _downloads;

        /// <summary>
        /// Creates a new document loader.
        /// </summary>
        /// <param name="requesters">The requesters to use.</param>
        /// <param name="configuration">The configuration to use..</param>
        /// <param name="filter">The optional request filter to use.</param>
        public BaseLoader(IEnumerable<IRequester> requesters, IConfiguration configuration, Predicate<IRequest> filter)
        {
            _requesters = requesters;
            _configuration = configuration;
            _filter = filter ?? (_ => true);
            _downloads = new List<IDownload>();
        }

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
            return _configuration.GetCookie(url.Origin);
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

            if (_filter(request))
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
            return _downloads.ToArray();
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
            foreach (var requester in _requesters)
            {
                if (requester.SupportsProtocol(request.Address.Scheme))
                {
                    var events = _configuration.Events;
                    var evt = new RequestStartEvent(requester, request);

                    if (events != null)
                        events.Publish(evt);

                    var result = await requester.RequestAsync(request, cancel).ConfigureAwait(false);
                    evt.FireEnd();
                    return result;
                }
            }

            return default(IResponse);
        }
    }
}
