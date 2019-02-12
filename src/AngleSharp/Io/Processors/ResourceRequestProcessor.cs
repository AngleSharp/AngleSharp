namespace AngleSharp.Io.Processors
{
    using AngleSharp.Media;
    using System;
    using System.Threading.Tasks;

    abstract class ResourceRequestProcessor<TResource> : BaseRequestProcessor
        where TResource : IResourceInfo
    {
        #region Fields

        private readonly IBrowsingContext _context;

        #endregion

        #region ctor

        public ResourceRequestProcessor(IBrowsingContext context)
            : base(context?.GetService<IResourceLoader>())
        {
            _context = context;
        }

        #endregion

        #region Properties

        public String Source => Resource?.Source.Href ?? String.Empty;

        public Boolean IsReady => Resource != null;

        public TResource Resource
        {
            get;
            protected set;
        }

        #endregion

        #region Methods

        public override Task ProcessAsync(ResourceRequest request)
        {
            if (IsAvailable && IsDifferentToCurrentResourceUrl(request.Target))
            {
                return base.ProcessAsync(request);
            }

            return Task.CompletedTask;
        }

        #endregion

        #region Helpers

        protected IResourceService<TResource> GetService(IResponse response)
        {
            var type = response.GetContentType();
            return _context.GetResourceService<TResource>(type.Content);
        }

        private Boolean IsDifferentToCurrentResourceUrl(Url target)
        {
            var resource = Resource;
            return resource == null || !target.Equals(resource.Source);
        }

        #endregion
    }
}

