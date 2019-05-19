namespace AngleSharp.Io.Processors
{
    using AngleSharp.Dom;
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;

    /// <summary>
    /// Basic implementation of a request processor.
    /// </summary>
    public abstract class BaseRequestProcessor : IRequestProcessor
    {
        #region Fields

        private readonly IResourceLoader _loader;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new request processor.
        /// </summary>
        public BaseRequestProcessor(IResourceLoader loader)
        {
            _loader = loader;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the status if downloads can be created.
        /// </summary>
        public Boolean IsAvailable => _loader != null;

        /// <summary>
        /// Gets the associated download.
        /// </summary>
        public IDownload Download
        {
            get;
            protected set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Processes the given request asynchronously.
        /// </summary>
        public virtual Task ProcessAsync(ResourceRequest request)
        {
            if (IsAvailable && IsDifferentToCurrentDownloadUrl(request.Target))
            {
                CancelDownload();
                Download = _loader.FetchAsync(request);
                return FinishDownloadAsync();
            }

            return Task.CompletedTask;
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Processes the response.
        /// </summary>
        protected abstract Task ProcessResponseAsync(IResponse response);

        /// <summary>
        /// Finishes the download.
        /// </summary>
        protected async Task FinishDownloadAsync()
        {
            var download = Download;
            var response = await download.Task.ConfigureAwait(false);
            var eventName = EventNames.Error;

            if (response != null)
            {
                try
                {
                    await ProcessResponseAsync(response).ConfigureAwait(false);
                    eventName = EventNames.Load;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
                finally
                {
                    response.Dispose();
                }
            }

            if (download.Source is EventTarget eventTarget)
            {
                if (eventTarget is Element element)
                {
                    element.Owner.QueueTask(() => eventTarget.FireSimpleEvent(eventName));
                }
                else
                {
                    eventTarget.FireSimpleEvent(eventName);
                }
            }
        }

        /// <summary>
        /// Fetches the given request with CORS.
        /// </summary>
        protected IDownload DownloadWithCors(CorsRequest request) =>
            _loader.FetchWithCorsAsync(request);

        /// <summary>
        /// Cancels the current download, if any.
        /// </summary>
        protected void CancelDownload()
        {
            var download = Download;

            if (download != null && !download.IsCompleted)
            {
                download.Cancel();
            }
        }

        /// <summary>
        /// Checks if the given target is different than the current download.
        /// </summary>
        protected Boolean IsDifferentToCurrentDownloadUrl(Url target)
        {
            var download = Download;
            return download == null || !target.Equals(download.Target);
        }

        #endregion
    }
}
