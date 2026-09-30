namespace AngleSharp.Io.Processors
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    sealed class FrameRequestProcessor : BaseRequestProcessor
    {
        #region Fields
        
        private readonly HtmlFrameElementBase _element;

        #endregion

        #region ctor

        public FrameRequestProcessor(IBrowsingContext context, HtmlFrameElementBase element)
            : base(context?.GetService<IResourceLoader>()!)
        {
            _element = element;
        }

        #endregion

        #region Properties

        public IDocument? Document
        {
            get;
            private set;
        }

        #endregion

        #region Methods

        public override Task ProcessAsync(ResourceRequest request)
        {
            var contentHtml = _element!.GetContentHtml();

            if (contentHtml != null)
            {
                return ProcessResponse(contentHtml);
            }

            return base.ProcessAsync(request);
        }

        protected override Task ProcessResponseAsync(IResponse response)
        {
            var cancel = CancellationToken.None;
            var context = _element.NestedContext;
            var task = context.OpenAsync(response, cancel);
            return WaitResponse(task);
        }

        #endregion

        #region Helpers

        private Task ProcessResponse(String response)
        {
            var cancel = CancellationToken.None;
            var context = _element.NestedContext;
            var task = context.OpenAsync(m => m.Content(response).Address("about:srcdoc"), cancel);
            return WaitResponse(task);
        }

        private async Task WaitResponse(Task<IDocument> task)
        {
            Document = await task.ConfigureAwait(false);
        }

        #endregion
    }
}
