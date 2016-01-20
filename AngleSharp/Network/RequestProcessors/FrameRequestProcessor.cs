namespace AngleSharp.Network.RequestProcessors
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Html;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    class FrameRequestProcessor : BaseRequestProcessor
    {
        #region Fields

        readonly HtmlFrameElementBase _element;
        readonly IConfiguration _options;
        IDocument _document;

        #endregion

        #region ctor

        private FrameRequestProcessor(HtmlFrameElementBase element, IConfiguration options, IResourceLoader loader)
            : base(loader)
        {
            _element = element;
            _options = options;
        }

        internal static FrameRequestProcessor Create(HtmlFrameElementBase element)
        {
            var document = element.Owner;
            var options = document.Options;
            var loader = document.Loader;

            return options != null && loader != null ?
                new FrameRequestProcessor(element, options, loader) : null;
        }

        #endregion

        #region Properties

        public IDocument Document
        {
            get { return _document; }
        }

        #endregion

        #region Methods

        public override Task Process(ResourceRequest request)
        {
            var contentHtml = _element.GetContentHtml();

            if (contentHtml != null)
            {
                var referer = _element.Owner.DocumentUri;
                return ProcessResponse(contentHtml, referer);
            }

            return base.Process(request);
        }

        protected override Task ProcessResponse(IResponse response)
        {
            var cancel = CancellationToken.None;
            var context = _element.NestedContext;
            var task = context.OpenAsync(response, cancel);
            return WaitResponse(task);
        }

        Task ProcessResponse(String response, String referer)
        {
            var cancel = CancellationToken.None;
            var context = _element.NestedContext;
            var task = context.OpenAsync(m => m.Content(response).Address(referer), cancel);
            return WaitResponse(task);
        }

        async Task WaitResponse(Task<IDocument> task)
        {
            _document = await task.ConfigureAwait(false);
        }

        #endregion
    }
}
