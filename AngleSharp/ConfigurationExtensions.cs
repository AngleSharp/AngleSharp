namespace AngleSharp
{
    using AngleSharp.Dom.Css;
    using AngleSharp.Extensions;
    using AngleSharp.Network.Default;
    using AngleSharp.Services;
    using AngleSharp.Services.Default;
    using System;
    using System.Linq;

    /// <summary>
    /// A set of useful extensions for Configuration (or derived) objects.
    /// </summary>
    public static class ConfigurationExtensions
    {
        #region Styling
        
        /// <summary>
        /// Registers the default styling service with a new CSS style engine
        /// to retrieve, if no other styling service has been registered yet.
        /// </summary>
        /// <typeparam name="TConfiguration">Configuration type.</typeparam>
        /// <param name="configuration">The configuration to modify.</param>
        /// <returns>The same object, for chaining.</returns>
        public static TConfiguration WithCss<TConfiguration>(this TConfiguration configuration)
            where TConfiguration : Configuration
        {
            if (configuration == null)
                throw new ArgumentNullException("configuration");

            if (configuration.GetServices<IStylingService>().Any() == false)
            {
                var service = new StylingService();
                var engine = new CssStyleEngine();
                service.Register(engine);
                configuration.Register(service);
            }

            return configuration;
        }

        /// <summary>
        /// Unregisters the styling services.
        /// </summary>
        /// <typeparam name="TConfiguration">Configuration type.</typeparam>
        /// <param name="configuration">The configuration to modify.</param>
        /// <returns>The same object, for chaining.</returns>
        public static TConfiguration WithoutCss<TConfiguration>(this TConfiguration configuration)
            where TConfiguration : Configuration
        {
            if (configuration == null)
                throw new ArgumentNullException("configuration");

            var services = configuration.GetServices<IStylingService>().ToArray();

            foreach (var service in services)
                configuration.Unregister(service);

            return configuration;
        }

        #endregion

        #region Loading Resources

        /// <summary>
        /// Registers the default loader service, if no other loader has been
        /// registered yet.
        /// </summary>
        /// <typeparam name="TConfiguration">Configuration type.</typeparam>
        /// <param name="configuration">The configuration to modify.</param>
        /// <param name="setup">
        /// The optional setup for the loader service.
        /// </param>
        /// <returns>The same object, for chaining.</returns>
        public static TConfiguration WithDefaultLoader<TConfiguration>(this TConfiguration configuration, Action<LoaderService> setup = null)
            where TConfiguration : Configuration
        {
            if (configuration == null)
                throw new ArgumentNullException("configuration");

            if (configuration.GetServices<ILoaderService>().Any() == false)
            {
                if (configuration.Requesters.Any() == false)
                {
                    var requester = new HttpRequester();
                    configuration.Register(requester);
                }

                var service = new LoaderService(configuration.Requesters);

                if (setup != null)
                    setup(service);

                configuration.Register(service);
            }

            return configuration;
        }

        #endregion
    }
}
