namespace AngleSharp
{
    using AngleSharp.Browser;
    using AngleSharp.Browser.Services;
    using AngleSharp.Common;
    using AngleSharp.Extensions;
    using AngleSharp.Io;
    using AngleSharp.Io.Services;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    /// <summary>
    /// A set of useful extensions for Configuration (or derived) objects.
    /// </summary>
    public static class ConfigurationExtensions
    {
        #region General

        /// <summary>
        /// Returns a new configuration that includes the given service.
        /// </summary>
        /// <param name="configuration">The configuration to extend.</param>
        /// <param name="service">The service to register.</param>
        /// <returns>The new instance with the service.</returns>
        public static Configuration With(this IConfiguration configuration, Object service)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            if (service == null)
                throw new ArgumentNullException(nameof(service));
            
            return new Configuration(configuration.Services.Concat(service));
        }

        /// <summary>
        /// Returns a new configuration that includes the given services.
        /// </summary>
        /// <param name="configuration">The configuration to extend.</param>
        /// <param name="services">The services to register.</param>
        /// <returns>The new instance with the services.</returns>
        public static Configuration With(this IConfiguration configuration, IEnumerable<Object> services)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            if (services == null)
                throw new ArgumentNullException(nameof(services));

            return new Configuration(configuration.Services.Concat(services));
        }

        /// <summary>
        /// Returns a new configuration that includes the given service creator.
        /// </summary>
        /// <typeparam name="TService">The type of service to create.</typeparam>
        /// <param name="configuration">The configuration to extend.</param>
        /// <param name="creator">The creator to register.</param>
        /// <returns>The new instance with the services.</returns>
        public static IConfiguration With<TService>(this IConfiguration configuration, Func<IBrowsingContext, TService> creator)
        {
            var original = configuration.Services;
            var available = configuration.Services.OfType<Func<IBrowsingContext, TService>>();

            if (available.Any())
            {
                original = original.Except(available);
            }

            var services = original.Concat(creator);
            return new Configuration(services);
        }

        /// <summary>
        /// Returns a new configuration that uses the culture with the provided
        /// name.
        /// </summary>
        /// <param name="configuration">The configuration to extend.</param>
        /// <param name="name">The name of the culture to set.</param>
        /// <returns>The new instance with the culture being set.</returns>
        public static Configuration SetCulture(this IConfiguration configuration, String name)
        {
            var culture = new CultureInfo(name);
            return configuration.SetCulture(culture);
        }

        /// <summary>
        /// Returns a new configuration that uses the given culture. Providing
        /// null will reset the culture to the default one.
        /// </summary>
        /// <param name="configuration">The configuration to extend.</param>
        /// <param name="culture">The culture to set.</param>
        /// <returns>The new instance with the culture being set.</returns>
        public static Configuration SetCulture(this IConfiguration configuration, CultureInfo culture)
        {
            return configuration.With(culture);
        }

        #endregion

        #region Loading Resources

        /// <summary>
        /// Registers the default loader service, if no other loader has been
        /// registered yet.
        /// </summary>
        /// <param name="configuration">The configuration to extend.</param>
        /// <param name="setup">Optional setup for the loader service.</param>
        /// <param name="requesters">Optional requesters to use.</param>
        /// <returns>The new instance with the service.</returns>
        public static IConfiguration WithDefaultLoader(this IConfiguration configuration, Action<LoaderSetup> setup = null, IEnumerable<IRequester> requesters = null)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            configuration = configuration.With(requesters ?? new IRequester[] { new DefaultHttpRequester(), new DataRequester() });

            var config = new LoaderSetup
            {
                Filter = null,
                IsNavigationEnabled = true,
                IsResourceLoadingEnabled = false
            };
            var factory = configuration.GetFactory<IServiceFactory>();
            setup?.Invoke(config);

            if (config.IsNavigationEnabled)
            {
                configuration = configuration.With<IDocumentLoader>(ctx => new DefaultDocumentLoader(ctx, config.Filter));
            }

            if (config.IsResourceLoadingEnabled)
            {
                configuration = configuration.With<IResourceLoader>(ctx => new DefaultResourceLoader(ctx, config.Filter));
            }

            return configuration;
        }

        /// <summary>
        /// Configures the loader.
        /// </summary>
        public sealed class LoaderSetup
        {
            /// <summary>
            /// Gets or sets if navigation is enabled.
            /// </summary>
            public Boolean IsNavigationEnabled { get; set; }

            /// <summary>
            /// Gets or sets if resource loading is enabled.
            /// </summary>
            public Boolean IsResourceLoadingEnabled { get; set; }

            /// <summary>
            /// Gets or sets the filter, if any.
            /// </summary>
            public Predicate<Request> Filter { get; set; }
        }

        #endregion

        #region Setting Encoding

        /// <summary>
        /// Registeres the default encoding determination algorithm, as
        /// specified by the W3C.
        /// </summary>
        /// <param name="configuration">The configuration to extend.</param>
        /// <returns>The new instance with the service.</returns>
        public static IConfiguration WithLocaleBasedEncoding(this IConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentException(nameof(configuration));

            if (!configuration.GetServices<IEncodingProvider>().Any())
            {
                var service = new LocaleEncodingProvider();
                return configuration.With(service);
            }

            return configuration;
        }

        #endregion

        #region Cookies

        /// <summary>
        /// Registers the default cookie service if no other cookie service has
        /// been registered yet.
        /// </summary>
        /// <param name="configuration">The configuration to extend.</param>
        /// <returns>The new instance with the service.</returns>
        public static IConfiguration WithCookies(this IConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            if (!configuration.GetServices<ICookieProvider>().Any())
            {
                var service = new MemoryCookieProvider();
                return configuration.With(service);
            }

            return configuration;
        }

        #endregion
    }
}
