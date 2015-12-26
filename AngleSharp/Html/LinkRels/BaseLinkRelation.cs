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

        /// <summary>
        /// Gets the link element.
        /// </summary>
        public HtmlLinkElement Link
        {
            get { return _link; }
        }

        /// <summary>
        /// Gets the url of the link element's address.
        /// </summary>
        public Url Url
        {
            get { return new Url(_link.Href); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Cancels the outstanding download, if any.
        /// </summary>
        public void Cancel()
        {
            if (_download != null && !_download.IsCompleted)
            {
                _download.Cancel();
            }
        }

        /// <summary>
        /// Loads the content of the relation asynchronously.
        /// </summary>
        /// <param name="configuration">The configuration to use.</param>
        /// <param name="loader">The optional loader to use.</param>
        /// <returns>The task, which loads the content.</returns>
        public abstract Task LoadAsync(IConfiguration configuration, IResourceLoader loader);

        #endregion
    }
}
