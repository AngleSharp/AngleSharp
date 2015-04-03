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
        /// Sets styling to true and registers a new CSS style engine, if none is available.
        /// </summary>
        /// <typeparam name="TConfiguration">Configuration or derived.</typeparam>
        /// <param name="configuration">The configuration to modify.</param>
        /// <returns>The same object, for chaining.</returns>
        public static TConfiguration WithCss<TConfiguration>(this TConfiguration configuration)
            where TConfiguration : Configuration
        {
            if (configuration == null)
                throw new ArgumentNullException("configuration");

            configuration.IsStyling = true;

            if (configuration.StyleEngines.OfType<CssStyleEngine>().Any() == false)
                configuration.Register(new CssStyleEngine());

            return configuration;
        }

        /// <summary>
        /// Sets styling to true and returns the same instance.
        /// </summary>
        /// <typeparam name="TConfiguration">Implementation of IConfiguration.</typeparam>
        /// <param name="configuration">The configuration to modify.</param>
        /// <returns>The same object, for chaining.</returns>
        public static TConfiguration WithStyling<TConfiguration>(this TConfiguration configuration)
            where TConfiguration : IConfiguration
        {
            if (configuration == null)
                throw new ArgumentNullException("configuration");

            configuration.IsStyling = true;
            return configuration;
        }

        #endregion

        #region Loading Resources

        /// <summary>
        /// Registers the default loader service if no other loader has been
        /// registered yet.
        /// </summary>
        /// <typeparam name="TConfiguration">The type of config.</typeparam>
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
