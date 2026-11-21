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
        private IStylingService? _engine;

        #endregion

        #region ctor

        public StyleSheetRequestProcessor(IBrowsingContext context, IHtmlLinkElement link)
            : base(context?.GetService<IResourceLoader>()!)
        {
            _context = context!;
            _link = link;
        }

        #endregion

        #region Properties

        public IStyleSheet? Sheet
        {
            get;
            private set;
        }

        public IStylingService? Engine => _engine ??= _context?.GetStyling(LinkType);

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
            if (!IsAcceptedContentType(response))
            {
                // results in an error event instead of a load event
                throw new InvalidOperationException("The style sheet was not served as text/css.");
            }

            var cancel = CancellationToken.None;
            var options = new StyleOptions(_link.Owner!)
            {
                Element = _link,
                IsDisabled = _link.IsDisabled,
                IsAlternate = _link.RelationList.Contains(Keywords.Alternate)
            };

            var task = _engine!.ParseStylesheetAsync(response, options, cancel);
            var sheet = await task.ConfigureAwait(false);
            sheet.Media.MediaText = _link.Media ?? String.Empty;
            Sheet = sheet;
        }

        #endregion

        #region Helpers

        /// <summary>
        /// A style sheet served with another type than text/css is only applied in quirks
        /// mode (and there only from the same origin), see the HTML spec's "fetch and
        /// process the linked resource" for stylesheet links.
        /// </summary>
        private Boolean IsAcceptedContentType(IResponse response)
        {
            var header = response.Headers.GetOrDefault(HeaderNames.ContentType, null);

            if (String.IsNullOrEmpty(header) || !LinkType.Is(MimeTypeNames.Css))
            {
                return true;
            }

            var type = new MimeType(header!);

            if (type.Content.Is(MimeTypeNames.Css))
            {
                return true;
            }

            var owner = _link.Owner as Document;
            return owner is not null && owner.QuirksMode == QuirksMode.On &&
                owner.Origin is { } origin && origin.Is(response.Address.Origin);
        }

        #endregion
    }
}
