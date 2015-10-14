namespace AngleSharp.Html.LinkRels
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Html;
    using AngleSharp.Extensions;
    using AngleSharp.Network;
    using AngleSharp.Services.Styling;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    class StyleSheetLinkRelation : BaseLinkRelation
    {
        #region Fields

        IStyleSheet _sheet;

        #endregion

        #region ctor

        public StyleSheetLinkRelation(IHtmlLinkElement link)
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

        public override async Task LoadAsync(IConfiguration configuration, IResourceLoader loader, CancellationToken cancel)
        {
            var link = Link;
            var request = link.CreateRequestFor(Url);

            using (var response = await loader.FetchAsync(request, cancel).ConfigureAwait(false))
            {
                var engine = configuration.GetStyleEngine(link.Type ?? MimeTypes.Css);

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

                    if (response != null)
                    {
                        try 
                        {
                            _sheet = engine.ParseStylesheet(response, options); 
                            _sheet.Media.MediaText = link.Media ?? String.Empty;
                        }
                        catch { /* Do not care here */ }
                    }
                }
            }
        }

        #endregion
    }
}
