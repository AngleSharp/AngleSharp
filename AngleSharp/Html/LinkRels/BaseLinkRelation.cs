namespace AngleSharp.Html.LinkRels
{
    using AngleSharp.Dom.Html;
    using AngleSharp.Network;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Base type for the all link rel field types.
    /// </summary>
    abstract class BaseLinkRelation
    {
        #region Fields

        readonly IHtmlLinkElement _link;

        #endregion

        #region ctor

        public BaseLinkRelation(IHtmlLinkElement link)
        {
            _link = link;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the link element.
        /// </summary>
        public IHtmlLinkElement Link
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
        /// Loads the content of the relation asynchronously.
        /// </summary>
        /// <param name="configuration">The configuration to use.</param>
        /// <param name="loader">The optional loader to use.</param>
        /// <param name="cancel">The cancellation token.</param>
        /// <returns>The task, which loads the content.</returns>
        public abstract Task LoadAsync(IConfiguration configuration, IResourceLoader loader, CancellationToken cancel);

        #endregion
    }
}
