namespace AngleSharp
{
    using AngleSharp.Parser;
    using System;
    using System.Globalization;

    /// <summary>
    /// Represents global configuration for the AngleSharp library.
    /// This is independent of parsing / document creation options
    /// given in the DocumentOptions class.
    /// </summary>
    public sealed class Configuration : IConfiguration
    {
        #region Fields

        CultureInfo _culture;
        Boolean _scripting;
        Boolean _styling;
        Boolean _embedded;
        Boolean _quirks;
        EventHandler<ParseErrorEventArgs> _onerror;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new default configuration.
        /// </summary>
        public Configuration()
        {
            _quirks = false;
            _scripting = false;
            _styling = true;
            _embedded = false;
            _onerror = null;
            _culture = CultureInfo.CurrentUICulture;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the default configuration to use.
        /// </summary>
        internal static IConfiguration Default
        {
            get { return DependencyResolver.Current.GetService<IConfiguration>() ?? new Configuration(); }
        }

        /// <summary>
        /// Gets or sets the current scripting mode.
        /// </summary>
        public Boolean IsScripting
        {
            get { return _scripting; }
            set { _scripting = value; }
        }

        /// <summary>
        /// Gets or sets the current CSS mode. Usually CSS stylesheets and inline-
        /// definitions are parsed (can be deactivated here).
        /// </summary>
        public Boolean IsStyling
        {
            get { return _styling; }
            set { _styling = value; }
        }

        /// <summary>
        /// Gets or sets the current embedding mode. Normally the document is NOT
        /// embedded. Enabling embedding will emulate the document being rendered
        /// in an iframe.
        /// </summary>
        public Boolean IsEmbedded
        {
            get { return _embedded; }
            set { _embedded = value; }
        }

        /// <summary>
        /// Gets or sets the delegate to call in case of an (tolerable) error. If this
        /// is null, then the default behavior is to print these errors on the
        /// debug console (if in debug mode) or to drop the errors completely.
        /// </summary>
        public EventHandler<ParseErrorEventArgs> OnError
        {
            get { return _onerror; }
            set { _onerror = value; }
        }

        /// <summary>
        /// Gets or sets the language (code, e.g. en-US, de-DE) to use.
        /// </summary>
        public String Language
        {
            get { return _culture.Name; }
            set { Culture = new CultureInfo(value); }
        }

        /// <summary>
        /// Gets or sets the culture to use.
        /// </summary>
        public CultureInfo Culture
        {
            get { return _culture; }
            set { _culture = value ?? CultureInfo.CurrentUICulture; }
        }

        /// <summary>
        /// Gets or sets if the quirks mode should be used for HTML / CSS parsing.
        /// </summary>
        public Boolean UseQuirksMode
        {
            get { return _quirks; }
            set { _quirks = value; }
        }

        #endregion
    }
}
