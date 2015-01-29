namespace AngleSharp
{
    using AngleSharp.Dom;

    /// <summary>
    /// A simple and lightweight browsing context.
    /// </summary>
    sealed class SimpleBrowsingContext : IBrowsingContext
    {
        #region Fields

        readonly IConfiguration _configuration;
        readonly Sandboxes _security;
        IDocument _active;

        #endregion

        #region ctor

        public SimpleBrowsingContext(IConfiguration configuration, Sandboxes security)
        {
            _configuration = configuration;
            _security = security;
        }

        #endregion

        #region Properties

        public IDocument Active
        {
            get { return _active; }
        }

        public IConfiguration Configuration
        {
            get { return _configuration; }
        }

        public IDocument Creator
        {
            get { return null; }
        }

        public IWindow Current
        {
            get { return _active != null ? _active.DefaultView : null; }
        }

        public IBrowsingContext Parent
        {
            get { return null; }
        }

        public IHistory SessionHistory
        {
            get { return null; }
        }

        public Sandboxes Security
        {
            get { return _security; }
        }

        public void NavigateTo(IDocument document)
        {
            _active = document;
        }

        #endregion
    }
}
