namespace AngleSharp.Html.LinkRels
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Html;
    using AngleSharp.Extensions;
    using AngleSharp.Network.RequestProcessors;
    using System.Threading.Tasks;
    
    class StyleSheetLinkRelation : BaseLinkRelation
    {
        #region ctor

        public StyleSheetLinkRelation(HtmlLinkElement link)
            : base(link, StyleSheetRequestProcessor.Create(link))
        {
        }

        #endregion

        #region Properties

        public IStyleSheet Sheet
        {
            get 
            {
                var processor = Processor as StyleSheetRequestProcessor;
                return processor != null ? processor.Sheet : null; 
            }
        }

        #endregion

        #region Methods

        public override Task LoadAsync()
        {
            var processor = Processor;

            if (processor != null)
            {
                var request = Link.CreateRequestFor(Url);
                return processor.ProcessAsync(request);
            }

            return null;
        }

        #endregion
    }
}
