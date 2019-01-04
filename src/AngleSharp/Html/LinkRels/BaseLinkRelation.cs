namespace AngleSharp.Html.LinkRels
{
    using AngleSharp.Html.Dom;
    using AngleSharp.Io.Processors;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Base type for the all link rel field types.
    /// </summary>
    public abstract class BaseLinkRelation
    {
        #region Fields

        private readonly IHtmlLinkElement _link;
        private readonly IRequestProcessor _processor;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new instance of the relation processor.
        /// </summary>
        public BaseLinkRelation(IHtmlLinkElement link, IRequestProcessor processor)
        {
            _link = link;
            _processor = processor;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the assigned request processor.
        /// </summary>
        public IRequestProcessor Processor => _processor;

        /// <summary>
        /// Gets the associated link element.
        /// </summary>
        public IHtmlLinkElement Link => _link;

        /// <summary>
        /// Gets the currently used URL.
        /// </summary>
        public Url Url => String.IsNullOrEmpty(_link.Href) ? null : new Url(_link.Href);

        #endregion

        #region Methods

        /// <summary>
        /// Starts loading the associated resource(s) asynchronously.
        /// </summary>
        /// <returns></returns>
        public abstract Task LoadAsync();

        #endregion
    }
}
