namespace AngleSharp
{
    using AngleSharp.Dom.Css;
    using AngleSharp.Network;
    using System;
    using System.Linq;

    /// <summary>
    /// A set of useful extensions for IConfiguration and Configuration objects.
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

        #region Scripting

        /// <summary>
        /// Sets scripting to true and returns the same instance.
        /// </summary>
        /// <typeparam name="TConfiguration">Implementation of IConfiguration.</typeparam>
        /// <param name="configuration">The configuration to modify.</param>
        /// <returns>The same object, for chaining.</returns>
        public static TConfiguration WithScripting<TConfiguration>(this TConfiguration configuration)
            where TConfiguration : IConfiguration
        {
            if (configuration == null)
                throw new ArgumentNullException("configuration");

            configuration.IsScripting = true;
            return configuration;
        }

        #endregion

        #region Requester

        /// <summary>
        /// Include the default http/https requester for external resources. Returns the same instance.
        /// </summary>
        /// <typeparam name="TConfiguration">Configuration or derived.</typeparam>
        /// <param name="configuration">The configuration to modify.</param>
        /// <param name="agent">User-Agent information if any.</param>
        /// <returns>The same object, for chaining.</returns>
        public static TConfiguration WithDefaultRequester<TConfiguration>(this TConfiguration configuration, String agent = null)
            where TConfiguration : Configuration
        {
            if (configuration == null)
                throw new ArgumentNullException("configuration");

            configuration.Register(new DefaultRequester(agent));
            return configuration;
        }

        #endregion
    }
}
