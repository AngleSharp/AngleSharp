namespace AngleSharp.Io.Processors
{
    using AngleSharp.Common;
    using AngleSharp.Css;
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using AngleSharp.Text;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    sealed class StyleSheetRequestProcessor : BaseRequestProcessor
    {
        #region Fields

        private readonly IHtmlLinkElement _link;
        private readonly IBrowsingContext _context;
        private IStylingService _engine;

        #endregion

        #region ctor

        public StyleSheetRequestProcessor(IBrowsingContext context, IHtmlLinkElement link)
            : base(context?.GetService<IResourceLoader>())
        {
            _context = context;
            _link = link;
        }

        #endregion

        #region Properties

        public IStyleSheet Sheet
        {
            get;
            private set;
        }

        public IStylingService Engine => _engine ?? (_engine = _context.GetStyling(LinkType));

        public String LinkType => _link.Type ?? MimeTypeNames.Css;

        #endregion

        #region Methods

        public override Task ProcessAsync(ResourceRequest request)
        {
            if (IsAvailable && Engine != null && IsDifferentToCurrentDownloadUrl(request.Target))
            {
                CancelDownload();
                Download = DownloadWithCors(new CorsRequest(request)
                {
                    Setting = _link.CrossOrigin.ToEnum(CorsSetting.None),
                    Behavior = OriginBehavior.Taint,
                    Integrity = _context.GetProvider<IIntegrityProvider>()
                });
                return FinishDownloadAsync();
            }

            return Task.CompletedTask;
        }

        protected override async Task ProcessResponseAsync(IResponse response)
        {
            var cancel = CancellationToken.None;
            var options = new StyleOptions(_link.Owner)
            {
                Element = _link,
                IsDisabled = _link.IsDisabled,
                IsAlternate = _link.RelationList.Contains(Keywords.Alternate)
            };

            var task = _engine.ParseStylesheetAsync(response, options, cancel);
            var sheet = await task.ConfigureAwait(false);
            sheet.Media.MediaText = _link.Media ?? String.Empty;
            Sheet = sheet;
        }

        #endregion
    }
}
