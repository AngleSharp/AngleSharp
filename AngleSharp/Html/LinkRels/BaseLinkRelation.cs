namespace AngleSharp.Html.LinkRels
{
    using AngleSharp.Dom.Html;
    using AngleSharp.Network;
    using System.Threading.Tasks;

    /// <summary>
    /// Base type for the all link rel field types.
    /// </summary>
    abstract class BaseLinkRelation
    {
        #region Fields

        readonly HtmlLinkElement _link;

        #endregion

        #region ctor

        public BaseLinkRelation(HtmlLinkElement link)
        {
            _link = link;
        }

        #endregion

        #region Properties

        public abstract IDownload Download
        {
            get;
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
