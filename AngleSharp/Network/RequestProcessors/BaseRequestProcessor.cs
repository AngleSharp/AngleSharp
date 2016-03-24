namespace AngleSharp.Network.RequestProcessors
{
    using AngleSharp.Dom;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;

    abstract class BaseRequestProcessor : IRequestProcessor
    {
        #region Fields

        IDownload _download;
        IResourceLoader _loader;

        #endregion

        #region ctor

        public BaseRequestProcessor(IResourceLoader loader)
        {
            _loader = loader;
        }

        #endregion

        #region Properties

        public IDownload Download
        {
            get { return _download; }
        }

        #endregion

        #region Methods

        public virtual Task ProcessAsync(ResourceRequest request)
        {
            if (IsDifferentToCurrentDownloadUrl(request.Target))
            {
                StartDownload(request);
                return FinishDownloadAsync();
            }

            return null;
        }

        protected abstract Task ProcessResponseAsync(IResponse response);

        protected void StartDownload(ResourceRequest request)
        {
            if (_download != null && !_download.IsCompleted)
            {
                _download.Cancel();
            }

            _download = _loader.DownloadAsync(request);
        }

        protected async Task FinishDownloadAsync()
        {
            var response = await _download.Task.ConfigureAwait(false);
            var eventTarget = _download.Originator as EventTarget;
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

            if (eventTarget != null)
            {
                eventTarget.FireSimpleEvent(eventName);
            }
        }

        #endregion

        #region Helpers

        Boolean IsDifferentToCurrentDownloadUrl(Url target)
        {
            return _download == null || !target.Equals(_download.Target);
        }

        #endregion
    }
}
