namespace AngleSharp
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents context configuration for the AngleSharp library. Custom
    /// configurations can be made by deriving from this class, just
    /// implementing IConfiguration or modifying an instance of this specific
    /// class.
    /// </summary>
    public class Configuration : IConfiguration
    {
        #region Fields

        private readonly IEnumerable<Object> _services;

        private static readonly Object[] standardServices = new Object[]
        {
            Factory.HtmlElements,
            Factory.MathElements,
            Factory.SvgElements,
            Factory.Events,
            Factory.AttributeSelector,
            Factory.InputTypes,
            Factory.LinkRelations,
            Factory.MediaFeatures,
            Factory.Properties,
            Factory.PseudoClassSelector,
            Factory.PseudoElementSelector,
            Factory.Document,
            Factory.BrowsingContext,
            Factory.Service,
            new Func<IBrowsingContext, IEventLoop>(ctx => new TaskEventLoop()),
        };

        /// <summary>
        /// A fixed configuration that cannot be changed.
        /// </summary>
        private static readonly Configuration defaultConfiguration = new Configuration();

        /// <summary>
        /// A custom configuration that is user-defined.
        /// </summary>
        private static IConfiguration customConfiguration;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new immutable configuration.
        /// </summary>
        /// <param name="services">The services to expose.</param>
        public Configuration(IEnumerable<Object> services = null)
        {
            _services = services ?? standardServices;
        }

        #endregion

        #region Default

        /// <summary>
        /// Gets the default configuration to use. The default configuration
        /// can be overriden by calling the SetDefault method.
        /// </summary>
        public static IConfiguration Default
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
        public IEnumerable<Object> Services
        {
            get { return _services; }
        }

        #endregion
    }
}
