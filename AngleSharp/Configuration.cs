namespace AngleSharp
{
    using AngleSharp.Network;
    using AngleSharp.Parser;
    using System;
    using System.Diagnostics;
    using System.Globalization;

    /// <summary>
    /// Represents context configuration for the AngleSharp library.
    /// Custom configurations can be made by deriving from this class,
    /// just implementing IConfiguration or modifying an instance of
    /// this specific class. Default configurations need to be stored
    /// in the dependency resolver.
    /// </summary>
    public class Configuration : IConfiguration
    {
        #region Fields

        protected CultureInfo _culture;
        protected Boolean _scripting;
        protected Boolean _styling;
        protected Boolean _embedded;
        protected Boolean _quirks;
        protected Boolean _http;

        static readonly Configuration defaultConfiguration = new Configuration();

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
            _http = false;
            _culture = CultureInfo.CurrentUICulture;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets if HTTP requests should be allowed.
        /// Default is false.
        /// </summary>
        public Boolean AllowHttpRequests
        {
            get { return _http; }
            set { _http = value; }
        }

        /// <summary>
        /// Gets the default configuration to use. The default
        /// configuration can be overriden by placing some
        /// configuration in the DependencyResolver.
        /// </summary>
        internal static IConfiguration Default
        {
            get { return DependencyResolver.Current.GetService<IConfiguration>() ?? defaultConfiguration; }
        }

        /// <summary>
        /// Gets or sets the current scripting mode.
        /// Default is false.
        /// </summary>
        public Boolean IsScripting
        {
            get { return _scripting; }
            set { _scripting = value; }
        }

        /// <summary>
        /// Gets or sets the current CSS mode.
        /// Default is true.
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
        /// Default is false.
        /// </summary>
        public Boolean IsEmbedded
        {
            get { return _embedded; }
            set { _embedded = value; }
        }

        /// <summary>
        /// Gets or sets the language (code, e.g. en-US, de-DE) to use.
        /// Default is the system (UI) language.
        /// </summary>
        public String Language
        {
            get { return _culture.Name; }
            set { Culture = new CultureInfo(value); }
        }

        /// <summary>
        /// Gets or sets the culture to use.
        /// Default is the system (UI) culture.
        /// </summary>
        public CultureInfo Culture
        {
            get { return _culture; }
            set { _culture = value ?? CultureInfo.CurrentUICulture; }
        }

        /// <summary>
        /// Gets or sets if the quirks mode should be used for HTML / CSS parsing.
        /// Default is false.
        /// </summary>
        public Boolean UseQuirksMode
        {
            get { return _quirks; }
            set { _quirks = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a HTTP requester for performing HTTP requests.
        /// This method is using the default HTTP requester set over
        /// the dependency resolver. If nothing is found the 
        /// </summary>
        /// <returns>The constructed HTTP requester.</returns>
        public virtual IHttpRequester CreateHttpRequest()
        {
            return DependencyResolver.Current.GetService<IHttpRequester>()
                ?? new DefaultHttpRequester();
        }

        /// <summary>
        /// Reports an error by writing to the debug console.
        /// </summary>
        /// <param name="e">The parse error event arguments.</param>
        public virtual void ReportError(ParseErrorEventArgs e)
        {
            Debug.WriteLine(e.ToString());
        }

        #endregion
    }
}
