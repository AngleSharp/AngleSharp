namespace AngleSharp.Html.LinkRels
{
    using AngleSharp.Dom;
    using AngleSharp.Extensions;
    using AngleSharp.Html.Dom;
    using AngleSharp.Io.Processors;
    using System.Threading.Tasks;

    class StyleSheetLinkRelation : BaseLinkRelation
    {
        #region ctor

        public StyleSheetLinkRelation(HtmlLinkElement link)
            : base(link, new StyleSheetRequestProcessor(link.Context, link))
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
            var request = Link.CreateRequestFor(Url);
            return Processor?.ProcessAsync(request);
        }

        #endregion
    }
}
