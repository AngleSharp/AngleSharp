namespace AngleSharp
{
    using AngleSharp.Infrastructure;
    using AngleSharp.Network;
    using AngleSharp.Parser;
    using AngleSharp.Services;
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

        readonly List<IScriptEngine> _scripts;
        readonly List<IStyleEngine> _styles;
        readonly List<IRequester> _requesters;
        readonly List<IService> _services;

        CultureInfo _culture;
        Boolean _scripting;
        Boolean _styling;
        Boolean _embedded;

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
            _scripting = false;
            _styling = true;
            _embedded = false;
            _culture = CultureInfo.CurrentUICulture;
            _requesters = new List<IRequester>();
            _services = new List<IService>();
            _scripts = new List<IScriptEngine>();
            _styles = new List<IStyleEngine>();
            Register(new CssStyleEngine());
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets an enumeration over the registered services.
        /// </summary>
        public IEnumerable<IService> Services
        {
            get { return _services; }
        }

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
        /// Gets an enumeration over all available (e.g. http) requesters.
        /// </summary>
        public IEnumerable<IRequester> Requesters
        {
            get { return _requesters; }
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
        /// Gets or sets the culture to use.
        /// Default is the system (UI) culture.
        /// </summary>
        public CultureInfo Culture
        {
            get { return _culture ?? CultureInfo.CurrentUICulture; }
            set { _culture = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a clone of the given configuration. The clone may be used to
        /// change settings without affecting the originally provided configuration.
        /// </summary>
        /// <param name="configuration">The configuration to copy.</param>
        /// <returns>The copied configuration.</returns>
        internal static IConfiguration Clone(IConfiguration configuration)
        {
            return new CopyConfiguration(configuration);
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
        /// Sets the default configuration to use, when the configuration
        /// is omitted.
        /// </summary>
        /// <param name="configuration">The configuration to set.</param>
        public static void SetDefault(IConfiguration configuration)
        {
            customConfiguration = configuration;
        }

        /// <summary>
        /// Adds the provided service.
        /// </summary>
        /// <param name="service">The service to register.</param>
        /// <returns>The current instance for chaining.</returns>
        public Configuration Register(IService service)
        {
            _services.Add(service);
            return this;
        }

        /// <summary>
        /// Adds the given script engine.
        /// </summary>
        /// <param name="scriptEngine">The engine to register.</param>
        /// <returns>The current instance for chaining.</returns>
        public Configuration Register(IScriptEngine scriptEngine)
        {
            _scripts.Add(scriptEngine);
            return this;
        }

        /// <summary>
        /// Adds the given requester.
        /// </summary>
        /// <param name="requester">The requester to register.</param>
        /// <returns>The current instance for chaining.</returns>
        public Configuration Register(IRequester requester)
        {
            _requesters.Add(requester);
            return this;
        }

        /// <summary>
        /// Adds the given styling engine.
        /// </summary>
        /// <param name="styleEngine">The engine to register.</param>
        /// <returns>The current instance for chaining.</returns>
        public Configuration Register(IStyleEngine styleEngine)
        {
            _styles.Add(styleEngine);
            return this;
        }

        /// <summary>
        /// Removes the given script engine.
        /// </summary>
        /// <param name="scriptEngine">The script engine to unregister.</param>
        /// <returns>The current instance for chaining.</returns>
        public Configuration Unregister(IScriptEngine scriptEngine)
        {
            _scripts.Remove(scriptEngine);
            return this;
        }

        /// <summary>
        /// Removes the given style engine.
        /// </summary>
        /// <param name="styleEngine">The style engine to unregister.</param>
        /// <returns>The current instance for chaining.</returns>
        public Configuration Unregister(IStyleEngine styleEngine)
        {
            _styles.Remove(styleEngine);
            return this;
        }

        /// <summary>
        /// Removes the given service.
        /// </summary>
        /// <param name="service">The service to unregister.</param>
        /// <returns>The current instance for chaining.</returns>
        public Configuration Unregister(IService service)
        {
            _services.Remove(service);
            return this;
        }

        /// <summary>
        /// Removes the given requester.
        /// </summary>
        /// <param name="requester">The requester to unregister.</param>
        /// <returns>The current instance for chaining.</returns>
        public Configuration Unregister(IRequester requester)
        {
            _requesters.Remove(requester);
            return this;
        }

        #endregion
    }
}
