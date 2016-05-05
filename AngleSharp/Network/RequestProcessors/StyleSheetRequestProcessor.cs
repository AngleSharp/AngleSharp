namespace AngleSharp.Network.RequestProcessors
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Html;
    using AngleSharp.Extensions;
    using AngleSharp.Services.Styling;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    class StyleSheetRequestProcessor :  BaseRequestProcessor
    {
        #region Fields

        readonly HtmlLinkElement _link;
        readonly IBrowsingContext _context;
        IStyleEngine _engine;
        IStyleSheet _sheet;

        #endregion

        #region ctor

        private StyleSheetRequestProcessor(HtmlLinkElement link, IBrowsingContext context, IResourceLoader loader)
            : base(loader)
        {
            _link = link;
            _context = context;
        }

        internal static StyleSheetRequestProcessor Create(HtmlLinkElement element)
        {
            var document = element.Owner;
            var context = document.Context;
            var loader = document.Loader;

            return context != null && loader != null ?
                new StyleSheetRequestProcessor(element, context, loader) : null;
        }

        #endregion

        #region Properties

        public IStyleSheet Sheet
        {
            get { return _sheet; }
        }

        #endregion

        #region Methods

        public override Task ProcessAsync(ResourceRequest request)
        {
            var type = _link.Type ?? MimeTypeNames.Css;
            var engine = _context.Configuration.GetStyleEngine(type);

            if (engine != null)
            {
                _engine = engine;
                return base.ProcessAsync(request);
            }

            return null;
        }

        protected override async Task ProcessResponseAsync(IResponse response)
        {
            var cancel = CancellationToken.None;
            var options = new StyleOptions(_context)
            {
                Element = _link,
                IsDisabled = _link.IsDisabled,
                IsAlternate = _link.RelationList.Contains(Keywords.Alternate)
            };

            var task = _engine.ParseStylesheetAsync(response, options, cancel);
            _sheet = await task.ConfigureAwait(false);
            _sheet.Media.MediaText = _link.Media ?? String.Empty;
        }

        #endregion
    }
}
