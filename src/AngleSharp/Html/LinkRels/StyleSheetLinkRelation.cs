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

        public IStyleSheet Sheet
        {
            get 
            {
                var processor = Processor as StyleSheetRequestProcessor;
                return processor?.Sheet; 
            }
        }

        #endregion

        #region Methods

        public override Task LoadAsync()
        {
            if (Url == null)
            {
                return Task.FromResult(0);
            }

            var request = Link.CreateRequestFor(Url);
            return Processor?.ProcessAsync(request);
        }

        #endregion
    }
}
