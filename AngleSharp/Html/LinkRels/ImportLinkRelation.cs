namespace AngleSharp.Html.LinkRels
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Html;
    using AngleSharp.Extensions;
    using AngleSharp.Network;
    using AngleSharp.Parser.Html;
    using System.Threading;
    using System.Threading.Tasks;

    class ImportLinkRelation : BaseLinkRelation
    {
        #region Fields

        IDocument _import;

        #endregion

        #region ctor

        public ImportLinkRelation(IHtmlLinkElement link)
            : base(link)
        {
        }

        #endregion

        #region Properties

        public IDocument Import
        {
            get { return _import; }
        }

        #endregion

        #region Methods

        public override async Task LoadAsync(IConfiguration configuration, IResourceLoader loader, CancellationToken cancel)
        {
            var link = Link;
            var request = link.CreateRequestFor(Url);

            using (var response = await loader.FetchAsync(request, cancel).ConfigureAwait(false))
            {
                if (response != null)
                {
                    var parser = new HtmlParser(configuration);
                    _import = await parser.ParseAsync(response.Content, cancel).ConfigureAwait(false);
                }
            }
        }

        #endregion
    }
}
