namespace AngleSharp.Io.Processors
{
    using AngleSharp.Media;
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

        public ImageRequestProcessor(IBrowsingContext context)
            : base(context)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// The density of the pixels of the image (e.g. 2 for a 2x candidate of a srcset),
        /// which its natural size is divided by.
        /// </summary>
        public Double Density { get; set; } = 1.0;

        public Int32 Width => IsReady ? Scale(Resource.Width) : 0;

        public Int32 Height => IsReady ? Scale(Resource.Height) : 0;

        private Int32 Scale(Int32 size) => Density == 1.0 || size <= 0 ? size : Math.Max(1, (Int32)Math.Round(size / Density));

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
