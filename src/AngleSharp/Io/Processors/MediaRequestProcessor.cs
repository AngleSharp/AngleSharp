namespace AngleSharp.Io.Processors
{
    using AngleSharp.Media;
    using AngleSharp.Media.Dom;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// See the following link for more infos:
    /// https://html.spec.whatwg.org/multipage/embedded-content.html#dom-media-load
    /// </summary>
    sealed class MediaRequestProcessor<TMediaInfo> :ResourceRequestProcessor<TMediaInfo>
        where TMediaInfo : IMediaInfo
    {
        #region ctor

        public MediaRequestProcessor(IBrowsingContext context)
            : base(context)
        {
        }

        #endregion

        #region Properties

        public TMediaInfo Media
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
                    else if (Resource == null)
                    {
                        return MediaNetworkState.NoSource;
                    }
                }

                return MediaNetworkState.Idle; 
            }
        }

        #endregion

        #region Methods

        protected override async Task ProcessResponseAsync(IResponse response)
        {
            var service = GetService(response);

            if (service != null)
            {
                var cancel = CancellationToken.None;
                Media = await service.CreateAsync(response, cancel).ConfigureAwait(false);
            }
        }

        #endregion
    }
}
