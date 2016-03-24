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

        readonly IConfiguration _options;
        TResource _resource;

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
            get { return _resource != null ? _resource.Source.Href : String.Empty; }
        }

        public Boolean IsReady
        {
            get { return _resource != null; }
        }

        public TResource Resource
        {
            get { return _resource; }
            protected set { _resource = value; }
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

        Boolean IsDifferentToCurrentResourceUrl(Url target)
        {
            return _resource == null || !target.Equals(_resource.Source);
        }

        #endregion
    }
}

