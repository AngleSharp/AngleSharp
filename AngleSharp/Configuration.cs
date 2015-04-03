namespace AngleSharp
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Css;
    using AngleSharp.Events;
    using AngleSharp.Network;
    using AngleSharp.Network.Default;
    using AngleSharp.Services;
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>
    /// Represents context configuration for the AngleSharp library. Custom
    /// configurations can be made by deriving from this class, just
    /// implementing IConfiguration or modifying an instance of this specific
    /// class. To change the default configuration one needs to provide a
    /// service that implements IConfiguration in the dependency resolver.
    /// </summary>
    public class Configuration : IConfiguration
    {
        #region Fields

        readonly List<IScriptEngine> _scripts;
        readonly List<IStyleEngine> _styles;
        readonly List<IService> _services;
        readonly List<IRequester> _requesters;

        IEventAggregator _events;
        CultureInfo _culture;
        Boolean _scripting;
        Boolean _styling;

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
            _culture = CultureInfo.CurrentUICulture;
            _services = new List<IService>();
            _requesters = new List<IRequester>();
            _scripts = new List<IScriptEngine>();
            _styles = new List<IStyleEngine>();
            Register(new CssStyleEngine());
            Register(new HttpRequester());
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
        /// Gets an enumeration over the available requesters.
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
        /// Gets or sets the culture to use.
        /// Default is the system (UI) culture.
        /// </summary>
        public CultureInfo Culture
        {
            get { return _culture ?? CultureInfo.CurrentUICulture; }
            set { _culture = value; }
        }

        /// <summary>
        /// Gets or sets the event aggregator to use. By default
        /// no aggregator is used.
        /// </summary>
        public IEventAggregator Events
        {
            get { return _events; }
            set { _events = value; }
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
            if (configuration == null)
                throw new ArgumentNullException("configuration");

            customConfiguration = configuration;
        }

        /// <summary>
        /// Adds the provided service.
        /// </summary>
        /// <param name="service">The service to register.</param>
        /// <returns>The current instance for chaining.</returns>
        public Configuration Register(IService service)
        {
            if (service == null)
                throw new ArgumentNullException("service");

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
            if (scriptEngine == null)
                throw new ArgumentNullException("scriptEngine");

            _scripts.Add(scriptEngine);
            return this;
        }

        /// <summary>
        /// Adds the given styling engine.
        /// </summary>
        /// <param name="styleEngine">The engine to register.</param>
        /// <returns>The current instance for chaining.</returns>
        public Configuration Register(IStyleEngine styleEngine)
        {
            if (styleEngine == null)
                throw new ArgumentNullException("styleEngine");

            _styles.Add(styleEngine);
            return this;
        }

        /// <summary>
        /// Adds the given requester.
        /// </summary>
        /// <param name="requester">The requester to register.</param>
        /// <returns>The current instance for chaining.</returns>
        public Configuration Register(IRequester requester)
        {
            if (requester == null)
                throw new ArgumentNullException("requester");

            _requesters.Add(requester);
            return this;
        }

        /// <summary>
        /// Removes the given script engine.
        /// </summary>
        /// <param name="scriptEngine">The script engine to unregister.</param>
        /// <returns>The current instance for chaining.</returns>
        public Configuration Unregister(IScriptEngine scriptEngine)
        {
            if (scriptEngine == null)
                throw new ArgumentNullException("scriptEngine");

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
            if (styleEngine == null)
                throw new ArgumentNullException("styleEngine");

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
            if (service == null)
                throw new ArgumentNullException("service");

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
            if (requester == null)
                throw new ArgumentNullException("requester");

            _requesters.Remove(requester);
            return this;
        }

        #endregion
    }
}
