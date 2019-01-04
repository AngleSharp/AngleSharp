namespace AngleSharp.Html.LinkRels
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using AngleSharp.Io.Processors;
    using System.Threading.Tasks;

    class StyleSheetLinkRelation : BaseLinkRelation
    {
        #region ctor

        public StyleSheetLinkRelation(IHtmlLinkElement link)
            : base(link, new StyleSheetRequestProcessor(link?.Owner.Context, link))
        {
        }

        #endregion

        #region Properties

        public IStyleSheet Sheet => (Processor as StyleSheetRequestProcessor)?.Sheet;

        #endregion

        #region Methods

        public override async Task LoadAsync()
        {
            if (Url != null)
            {
                var request = Link.CreateRequestFor(Url);
                await Processor?.ProcessAsync(request);
            }
        }

        #endregion
    }
}
