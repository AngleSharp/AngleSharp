namespace AngleSharp.Html.LinkRels
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Html;
    using AngleSharp.Extensions;
    using AngleSharp.Network.RequestProcessors;
    using System.Threading.Tasks;
    
    class StyleSheetLinkRelation : BaseLinkRelation
    {
        #region Fields

        readonly StyleSheetRequestProcessor _request;

        #endregion

        #region ctor

        public StyleSheetLinkRelation(HtmlLinkElement link)
            : base(link)
        {
            _request = StyleSheetRequestProcessor.Create(link);
        }

        #endregion

        #region Properties

        public IStyleSheet Sheet
        {
            get { return _request != null ? _request.Sheet : null; }
        }

        #endregion

        #region Methods

        public override Task LoadAsync()
        {
            if (_request != null)
            {
                var request = Link.CreateRequestFor(Url);
                return _request.Process(request);
            }

            return null;
        }

        #endregion
    }
}
