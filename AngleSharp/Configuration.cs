namespace AngleSharp
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using AngleSharp.Events;
    using AngleSharp.Network;
    using AngleSharp.Services;

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

        readonly List<IService> _services;
        readonly List<IRequester> _requesters;

        IEventAggregator _events;
        CultureInfo _culture;

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
            _culture = CultureInfo.CurrentUICulture;
            _services = new List<IService>();
            _requesters = new List<IRequester>();
        }

        #endregion

        #region Default

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
        /// Gets an enumeration over the available requesters.
        /// </summary>
        public IEnumerable<IRequester> Requesters
        {
            get { return _requesters; }
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
