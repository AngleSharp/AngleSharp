namespace AngleSharp
{
    using AngleSharp.DOM;

    /// <summary>
    /// A simple and lightweight browsing context.
    /// </summary>
    sealed class SimpleBrowsingContext : IBrowsingContext
    {
        #region Fields

        readonly IConfiguration _configuration;
        IDocument _active;

        #endregion

        #region ctor

        public SimpleBrowsingContext(IConfiguration configuration)
        {
            _configuration = configuration;
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

        public void NavigateTo(IDocument document)
        {
            _active = document;
        }

        #endregion
    }
}
