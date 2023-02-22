namespace AngleSharp.Html.LinkRels
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using AngleSharp.Io.Processors;
    using System.Threading.Tasks;

    /// <summary>
    /// Base type for the all link rel field types.
    /// </summary>
    public abstract class BaseLinkRelation
    {
        #region Fields

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new instance of the relation processor.
        /// </summary>
        public BaseLinkRelation(IHtmlLinkElement link, IRequestProcessor processor)
        {
            Link = link;
            Processor = processor;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the assigned request processor.
        /// </summary>
        public IRequestProcessor Processor { get; }

        /// <summary>
        /// Gets the associated link element.
        /// </summary>
        public IHtmlLinkElement Link { get; }

        /// <summary>
        /// Gets the currently used URL.
        /// </summary>
        public Url? Url => Link.Href is { Length: > 0 } ? new Url(Link.Href) : null;

        #endregion

        #region Methods

        /// <summary>
        /// Starts loading the associated resource(s) asynchronously.
        /// </summary>
        public abstract Task LoadAsync();

        #endregion
    }
}
