namespace AngleSharp.Network.RequestProcessors
{
    using AngleSharp.Dom;
    using AngleSharp.Services.Media;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    sealed class ObjectRequestProcessor : ResourceRequestProcessor<IObjectInfo>
    {
        #region ctor

        private ObjectRequestProcessor(IConfiguration options, IResourceLoader loader)
            : base(options, loader)
        {
        }

        internal static ObjectRequestProcessor Create(Element element)
        {
            var document = element.Owner;
            var options = document.Options;
            var loader = document.Loader;

            return options != null && loader != null ?
                new ObjectRequestProcessor(options, loader) : null;
        }

        #endregion

        #region Properties

        public Int32 Width
        {
            get { return Resource?.Width ?? 0; }
        }

        public Int32 Height
        {
            get { return Resource?.Height ?? 0; }
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
