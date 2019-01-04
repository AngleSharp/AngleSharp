namespace AngleSharp.Io.Processors
{
    using AngleSharp.Media;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    sealed class ObjectRequestProcessor : ResourceRequestProcessor<IObjectInfo>
    {
        #region ctor

        public ObjectRequestProcessor(IBrowsingContext context)
            : base(context)
        {
        }

        #endregion

        #region Properties

        public Int32 Width => Resource?.Width ?? 0;

        public Int32 Height => Resource?.Height ?? 0;

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
