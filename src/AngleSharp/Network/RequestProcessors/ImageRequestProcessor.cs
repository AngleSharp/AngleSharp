namespace AngleSharp.Network.RequestProcessors
{
    using AngleSharp.Dom;
    using AngleSharp.Services.Media;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// For more information, see:
    /// http://www.w3.org/html/wg/drafts/html/master/embedded-content.html#update-the-image-data
    /// </summary>
    sealed class ImageRequestProcessor : ResourceRequestProcessor<IImageInfo>
    {
        #region ctor

        private ImageRequestProcessor(IConfiguration options, IResourceLoader loader)
            : base(options, loader)
        {
        }

        internal static ImageRequestProcessor Create(Element element)
        {
            var document = element.Owner;
            var options = document.Options;
            var loader = document.Loader;

            return options != null && loader != null ?
                new ImageRequestProcessor(options, loader) : null;
        }

        #endregion

        #region Properties

        public Int32 Width
        {
            get { return IsReady ? Resource.Width : 0; }
        }

        public Int32 Height
        {
            get { return IsReady ? Resource.Height : 0; }
        }

        #endregion

        #region Methods

        protected override async Task ProcessResponseAsync(IResponse response)
        {
            var service = GetService(response);

            if (service != null)
            {
                var cancel = CancellationToken.None;
                var result = await service.CreateAsync(response, cancel).ConfigureAwait(false);
                Resource = result;
            }
        }

        #endregion
    }
}
