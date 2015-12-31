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
        IDownload _download;

        #endregion

        #region ctor

        public BaseLinkRelation(HtmlLinkElement link)
        {
            _link = link;
        }

        #endregion

        #region Properties

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

        public void Cancel()
        {
            if (_download != null && !_download.IsCompleted)
            {
                _download.Cancel();
            }
        }

        public abstract Task LoadAsync(IConfiguration configuration, IResourceLoader loader);

        protected void SetDownload(IDownload download)
        {
            _download = download;
        }

        #endregion
    }
}
