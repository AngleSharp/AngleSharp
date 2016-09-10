namespace AngleSharp.Network.RequestProcessors
{
    using AngleSharp.Extensions;
    using AngleSharp.Services;
    using AngleSharp.Services.Media;
    using System;
    using System.Threading.Tasks;

    abstract class ResourceRequestProcessor<TResource> : BaseRequestProcessor
        where TResource : IResourceInfo
    {
        #region Fields

        private readonly IConfiguration _options;

        #endregion

        #region ctor

        public ResourceRequestProcessor(IConfiguration options, IResourceLoader loader)
            : base(loader)
        {
            _options = options;
        }

        #endregion

        #region Properties

        public String Source
        {
            get { return Resource?.Source.Href ?? String.Empty; }
        }

        public Boolean IsReady
        {
            get { return Resource != null; }
        }

        public TResource Resource
        {
            get;
            protected set;
        }

        #endregion

        #region Methods

        public override Task ProcessAsync(ResourceRequest request)
        {
            if (IsDifferentToCurrentResourceUrl(request.Target))
            {
                return base.ProcessAsync(request);
            }

            return null;
        }

        #endregion

        #region Helpers

        protected IResourceService<TResource> GetService(IResponse response)
        {
            var type = response.GetContentType();
            return _options.GetResourceService<TResource>(type.Content);
        }

        private Boolean IsDifferentToCurrentResourceUrl(Url target)
        {
            var resource = Resource;
            return resource == null || !target.Equals(resource.Source);
        }

        #endregion
    }
}

