namespace AngleSharp.Html.LinkRels
{
    using AngleSharp.Dom.Html;
    using AngleSharp.Network.RequestProcessors;
    using System.Threading.Tasks;

    /// <summary>
    /// Base type for the all link rel field types.
    /// </summary>
    abstract class BaseLinkRelation
    {
        #region Fields

        private readonly HtmlLinkElement _link;
        private readonly IRequestProcessor _processor;

        #endregion

        #region ctor

        public BaseLinkRelation(HtmlLinkElement link, IRequestProcessor processor)
        {
            _link = link;
            _processor = processor;
        }

        #endregion

        #region Properties

        public IRequestProcessor Processor
        {
            get { return _processor; }
        }

        public HtmlLinkElement Link
        {
            get { return _link; }
        }

        public Url Url
        {
            get { return new Url(_link.Href); }
        }

        #endregion

        #region Methods

        public abstract Task LoadAsync();

        #endregion
    }
}
