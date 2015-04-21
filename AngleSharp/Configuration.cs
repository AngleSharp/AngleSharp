namespace AngleSharp
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using AngleSharp.Events;
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

        readonly IEnumerable<IService> _services;
        readonly IEventAggregator _events;
        readonly CultureInfo _culture;

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
        /// Creates a new immutable configuration.
        /// </summary>
        /// <param name="services">The services to expose.</param>
        /// <param name="events">The event aggregator.</param>
        /// <param name="culture">The current culture.</param>
        public Configuration(IEnumerable<IService> services = null, IEventAggregator events = null, CultureInfo culture = null)
        {
            _services = services ?? Enumerable.Empty<IService>();
            _culture = culture ?? CultureInfo.CurrentUICulture;
            _events = events;
        }

        #endregion

        #region Default

        /// <summary>
        /// Gets the default configuration to use. The default configuration
        /// can be overriden by calling the SetDefault method.
        /// </summary>
        internal static IConfiguration Default
        {
            get { return customConfiguration ?? defaultConfiguration; }
        }

        /// <summary>
        /// Sets the default configuration to use, when the configuration
        /// is omitted. Providing a null-pointer will reset the default
        /// configuration.
        /// </summary>
        /// <param name="configuration">The configuration to set.</param>
        public static void SetDefault(IConfiguration configuration)
        {
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
        /// Gets the culture to use. Default is the system (UI) culture.
        /// </summary>
        public CultureInfo Culture
        {
            get { return _culture; }
        }

        /// <summary>
        /// Gets the event aggregator to use. By default no aggregator is used.
        /// </summary>
        public IEventAggregator Events
        {
            get { return _events; }
        }

        #endregion
    }
}
