namespace AngleSharp.Io.Processors
{
    using AngleSharp.Media;
    using AngleSharp.Media.Dom;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// See the following link for more infos:
    /// https://html.spec.whatwg.org/multipage/embedded-content.html#dom-media-load
    /// </summary>
    sealed class MediaRequestProcessor<TMediaInfo> :ResourceRequestProcessor<TMediaInfo>
        where TMediaInfo : IMediaInfo
    {
        #region Fields

        private readonly Action<TMediaInfo>? _created;
        private readonly Action<MediaErrorCode>? _failed;

        #endregion

        #region ctor

        public MediaRequestProcessor(IBrowsingContext context, Action<TMediaInfo>? created = null, Action<MediaErrorCode>? failed = null)
            : base(context)
        {
            _created = created;
            _failed = failed;
        }

        #endregion

        #region Properties

        public TMediaInfo? Media
        {
            get;
            private set;
        }

        public MediaNetworkState NetworkState
        {
            get
            {
                var download = Download;

                if (download != null)
                {
                    if (download.IsRunning)
                    {
                        return MediaNetworkState.Loading;
                    }
                    else if (Resource is null)
                    {
                        return MediaNetworkState.NoSource;
                    }
                }

                return MediaNetworkState.Idle; 
            }
        }

        #endregion

        #region Methods

        public override async Task ProcessAsync(ResourceRequest request)
        {
            try
            {
                await base.ProcessAsync(request).ConfigureAwait(false);
            }
            catch (Exception) when (Download is { Task.IsFaulted: true })
            {
                //  the request failed (the processing of a response reports its own errors)
                _failed?.Invoke(MediaErrorCode.Network);
            }
        }

        protected override async Task ProcessResponseAsync(IResponse response)
        {
            var service = GetService(response);

            if (service is null)
            {
                //  fails, so the element gets an error event
                _failed?.Invoke(MediaErrorCode.SourceNotSupported);
                throw new NotSupportedException($"No media service for {response.GetContentType().Content}.");
            }

            var cancel = CancellationToken.None;
            var media = default(TMediaInfo);

            try
            {
                media = await service.CreateAsync(response, cancel).ConfigureAwait(false);
            }
            catch
            {
                _failed?.Invoke(MediaErrorCode.Decode);
                throw;
            }

            Media = media;
            Resource = media;

            if (media != null)
            {
                _created?.Invoke(media);
            }
        }

        #endregion
    }
}
