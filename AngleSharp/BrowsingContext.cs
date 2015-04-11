namespace AngleSharp
{
    using AngleSharp.Dom;
    using AngleSharp.Extensions;
    using AngleSharp.Network;

    /// <summary>
    /// A simple and lightweight browsing context.
    /// </summary>
    public sealed class BrowsingContext : IBrowsingContext
    {
        #region Fields

        readonly IConfiguration _configuration;
        readonly Sandboxes _security;
        readonly IBrowsingContext _parent;
        readonly IDocument _creator;
        readonly IDocumentLoader _loader;
        readonly IHistory _history;
        IDocument _active;

        #endregion

        #region ctor
        
        internal BrowsingContext(IConfiguration configuration, Sandboxes security)
        {
            _configuration = configuration;
            _security = security;
            _loader = this.CreateLoader();
            _history = this.CreateHistory();
        }
        
        internal BrowsingContext(IBrowsingContext parent, Sandboxes security)
            : this(parent.Configuration, security)
        {
            _parent = parent;
            _creator = _parent.Active;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a new browsing context with the given configuration, or the
        /// default configuration, if no configuration is provided.
        /// </summary>
        /// <param name="configuration">The optional configuration.</param>
        /// <returns>The browsing context to use.</returns>
        public static IBrowsingContext New(IConfiguration configuration = null)
        {
            if (configuration == null)
                configuration = AngleSharp.Configuration.Default;

            return configuration.NewContext();
        }

        /// <summary>
        /// Navigates to the given document. Includes the document in the
        /// session history and sets it as the active document.
        /// </summary>
        /// <param name="document">The new document.</param>
        public void NavigateTo(IDocument document)
        {
            if (_history != null)
                _history.PushState(document, document.Title, document.Url);

            _active = document;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the currently active document.
        /// </summary>
        public IDocument Active
        {
            get { return _active; }
        }

        /// <summary>
        /// Gets the assigned document loader, if any.
        /// </summary>
        public IDocumentLoader Loader
        {
            get { return _loader; }
        }

        /// <summary>
        /// Gets the configuration for the browsing context.
        /// </summary>
        public IConfiguration Configuration
        {
            get { return _configuration; }
        }

        /// <summary>
        /// Gets the document that created the current context, if any. The
        /// creator is the active document of the parent at the time of
        /// creation.
        /// </summary>
        public IDocument Creator
        {
            get { return _creator; }
        }

        /// <summary>
        /// Gets the current window proxy.
        /// </summary>
        public IWindow Current
        {
            get { return _active != null ? _active.DefaultView : null; }
        }

        /// <summary>
        /// Gets the parent of the current context, if any. If a parent is
        /// available, then the current context contains only embedded
        /// documents.
        /// </summary>
        public IBrowsingContext Parent
        {
            get { return _parent; }
        }

        /// <summary>
        /// Gets the session history of the given browsing context, if any.
        /// </summary>
        public IHistory SessionHistory
        {
            get { return _history; }
        }

        /// <summary>
        /// Gets the sandboxing flag of the context.
        /// </summary>
        public Sandboxes Security
        {
            get { return _security; }
        }

        #endregion
    }
}
