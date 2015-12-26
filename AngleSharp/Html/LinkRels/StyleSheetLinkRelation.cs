namespace AngleSharp.Html.LinkRels
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Html;
    using AngleSharp.Extensions;
    using AngleSharp.Network;
    using AngleSharp.Services.Styling;
    using System;
    using System.Threading.Tasks;

    class StyleSheetLinkRelation : BaseLinkRelation
    {
        #region Fields

        IStyleSheet _sheet;

        #endregion

        #region ctor

        public StyleSheetLinkRelation(HtmlLinkElement link)
            : base(link)
        {
        }

        #endregion

        #region Properties

        public IStyleSheet Sheet
        {
            get { return _sheet; }
        }

        #endregion

        #region Methods

        public override Task LoadAsync(IConfiguration configuration, IResourceLoader loader)
        {
            var link = Link;
            var request = link.CreateRequestFor(Url);
            var download = loader.DownloadAsync(request);

            return link.ProcessResponse(download, response =>
            {
                var type = link.Type ?? MimeTypes.Css;
                var engine = configuration.GetStyleEngine(type);

                if (engine != null)
                {
                    var options = new StyleOptions
                    {
                        Element = link,
                        Title = link.Title,
                        IsDisabled = link.IsDisabled,
                        IsAlternate = link.RelationList.Contains(Keywords.Alternate),
                        Configuration = configuration
                    };

                    _sheet = engine.ParseStylesheet(response, options);
                    _sheet.Media.MediaText = link.Media ?? String.Empty;
                }
            });
        }

        #endregion
    }
}
