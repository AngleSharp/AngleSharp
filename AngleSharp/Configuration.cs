namespace AngleSharp
{
    using AngleSharp.Infrastructure;
    using AngleSharp.Network;
    using AngleSharp.Parser;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;

    /// <summary>
    /// Represents context configuration for the AngleSharp library.
    /// Custom configurations can be made by deriving from this class,
    /// just implementing IConfiguration or modifying an instance of
    /// this specific class. To change the default configuration one
    /// needs to provide a service that implements IConfiguration in
    /// the dependency resolver.
    /// </summary>
    public class Configuration : IConfiguration
    {
        #region Fields

        CultureInfo _culture;
        Boolean _scripting;
        Boolean _styling;
        Boolean _embedded;
        Boolean _quirks;
        Boolean _requests;
        IInfo _info;
        List<IScriptEngine> _scripts;
        List<IStyleEngine> _styles;

        /// <summary>
        /// A fixed configuration that cannot be changed.
        /// </summary>
        static readonly Configuration defaultConfiguration = new Configuration();

        /// <summary>
        /// A custom configuration that is user-defined.
        /// </summary>
        static IConfiguration customConfiguration;

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
            _requests = false;
            _culture = CultureInfo.CurrentUICulture;
            _info = new DefaultInfo();
            _scripts = new List<IScriptEngine>();
            _styles = new List<IStyleEngine>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets an enumeration over the available script engines.
        /// By default no script engine is integrated.
        /// </summary>
        public IEnumerable<IScriptEngine> ScriptEngines
        {
            get { return _scripts; }
        }

        /// <summary>
        /// Gets an enumeration over the available style engines,
        /// besides the default CSS engine.
        /// </summary>
        public IEnumerable<IStyleEngine> StyleEngines
        {
            get { return _styles; }
        }

        /// <summary>
        /// Gets or sets if external requests should be allowed.
        /// Default is false.
        /// </summary>
        public Boolean AllowRequests
        {
            get { return _requests; }
            set { _requests = value; }
        }

        /// <summary>
        /// Gets the default configuration to use. The default
        /// configuration can be overriden by placing some
        /// configuration in the DependencyResolver.
        /// </summary>
        internal static IConfiguration Default
        {
            get { return customConfiguration ?? defaultConfiguration; }
        }

        /// <summary>
        /// Gets or sets the user-agent information.
        /// </summary>
        public IInfo UserAgentInfo
        {
            get { return _info; }
            set 
            {
                if (_info == null)
                    throw new ArgumentException("The user-agent information cannot be omitted by passing a null reference.");
                    
                _info = value; 
            }
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
        /// Sets the default configuration to use, when the configuration
        /// is omitted.
        /// </summary>
        /// <param name="configuration">The configuration to set.</param>
        public static void SetDefault(IConfiguration configuration)
        {
            customConfiguration = configuration;
        }

        /// <summary>
        /// Creates a requester for performing HTTP / web requests.
        /// This method is using the default requester set over the
        /// dependency resolver. If nothing is found the default
        /// requester is constructed.
        /// </summary>
        /// <returns>The constructed requester.</returns>
        public virtual IRequester GetRequester()
        {
            return new DefaultRequester(_info);
        }

        /// <summary>
        /// Creates a request container that is passed to a requester
        /// object for sending a request to a server. This method is using
        /// the default request object set over the dependency resolver.
        /// If nothing is found the default request is constructed.
        /// </summary>
        /// <returns>The constructed request object.</returns>
        public virtual IRequest CreateRequest()
        {
            return new DefaultRequest();
        }

        /// <summary>
        /// Reports an error by writing to the debug console.
        /// </summary>
        /// <param name="e">The parse error event arguments.</param>
        public virtual void ReportError(ParseErrorEventArgs e)
        {
            Debug.WriteLine(e.ToString());
        }

        /// <summary>
        /// Adds the given script engine.
        /// </summary>
        /// <param name="scriptEngine">The engine to register.</param>
        /// <returns>The current instance for chaining.</returns>
        public IConfiguration Register(IScriptEngine scriptEngine)
        {
            _scripts.Add(scriptEngine);
            return this;
        }

        /// <summary>
        /// Adds the given styling engine.
        /// </summary>
        /// <param name="styleEngine">The engine to register.</param>
        /// <returns>The current instance for chaining.</returns>
        public IConfiguration Register(IStyleEngine styleEngine)
        {
            _styles.Add(styleEngine);
            return this;
        }

        /// <summary>
        /// Removes the given script engine.
        /// </summary>
        /// <param name="scriptEngine">The script engine to unregister.</param>
        /// <returns>The current instance for chaining.</returns>
        public IConfiguration Unregister(IScriptEngine scriptEngine)
        {
            _scripts.Remove(scriptEngine);
            return this;
        }

        /// <summary>
        /// Removes the given style engine.
        /// </summary>
        /// <param name="styleEngine">The style engine to unregister.</param>
        /// <returns>The current instance for chaining.</returns>
        public IConfiguration Unregister(IStyleEngine styleEngine)
        {
            _styles.Remove(styleEngine);
            return this;
        }

        #endregion
    }
}
