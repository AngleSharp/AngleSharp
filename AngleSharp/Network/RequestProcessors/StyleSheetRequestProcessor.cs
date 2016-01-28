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
        readonly IConfiguration _options;
        IStyleEngine _engine;
        IStyleSheet _sheet;

        #endregion

        #region ctor

        private StyleSheetRequestProcessor(HtmlLinkElement link, IConfiguration options, IResourceLoader loader)
            : base(loader)
        {
            _link = link;
            _options = options;
        }

        internal static StyleSheetRequestProcessor Create(HtmlLinkElement element)
        {
            var document = element.Owner;
            var options = document.Options;
            var loader = document.Loader;

            return options != null && loader != null ?
                new StyleSheetRequestProcessor(element, options, loader) : null;
        }

        #endregion

        #region Properties

        public IStyleSheet Sheet
        {
            get { return _sheet; }
        }

        #endregion

        #region Methods

        public override Task Process(ResourceRequest request)
        {
            var type = _link.Type ?? MimeTypeNames.Css;
            var engine = _options.GetStyleEngine(type);

            if (engine != null)
            {
                _engine = engine;
                return base.Process(request);
            }

            return null;
        }

        protected override async Task ProcessResponse(IResponse response)
        {
            var cancel = CancellationToken.None;
            var options = new StyleOptions
            {
                Element = _link,
                IsDisabled = _link.IsDisabled,
                IsAlternate = _link.RelationList.Contains(Keywords.Alternate),
                Configuration = _options
            };

            var task = _engine.ParseStylesheetAsync(response, options, cancel);
            _sheet = await task.ConfigureAwait(false);
            _sheet.Media.MediaText = _link.Media ?? String.Empty;
        }

        #endregion
    }
}
