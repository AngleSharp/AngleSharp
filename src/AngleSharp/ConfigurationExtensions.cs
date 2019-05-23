namespace AngleSharp
{
    using AngleSharp.Browser;
    using AngleSharp.Common;
    using AngleSharp.Io;
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
        public static IConfiguration With(this IConfiguration configuration, Object service)
        {
            configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            service = service ?? throw new ArgumentNullException(nameof(service));
            return new Configuration(configuration.Services.Concat(service));
        }

        /// <summary>
        /// Returns a new configuration that includes only the given service,
        /// excluding other instances or instance creators for the same service.
        /// </summary>
        /// <typeparam name="TService">The service to include exclusively.</typeparam>
        /// <param name="configuration">The configuration to extend.</param>
        /// <param name="service">The service to include.</param>
        /// <returns>The new instance with only the given service.</returns>
        public static IConfiguration WithOnly<TService>(this IConfiguration configuration, TService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            return configuration.Without<TService>().With(service);
        }

        /// <summary>
        /// Returns a new configuration that includes only the given service
        /// creator, excluding other instances or instance creators for the same
        /// service.
        /// </summary>
        /// <typeparam name="TService">The service to include exclusively.</typeparam>
        /// <param name="configuration">The configuration to extend.</param>
        /// <param name="creator">The service creator to include.</param>
        /// <returns>The new instance with only the given service.</returns>
        public static IConfiguration WithOnly<TService>(this IConfiguration configuration, Func<IBrowsingContext, TService> creator)
        {
            creator = creator ?? throw new ArgumentNullException(nameof(creator));
            return configuration.Without<TService>().With(creator);
        }

        /// <summary>
        /// Returns a new configuration that excludes the given service.
        /// </summary>
        /// <param name="configuration">The configuration to extend.</param>
        /// <param name="service">The service to unregister.</param>
        /// <returns>The new instance without the service.</returns>
        public static IConfiguration Without(this IConfiguration configuration, Object service)
        {
            configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            service = service ?? throw new ArgumentNullException(nameof(service));
            return new Configuration(configuration.Services.Except(service));
        }

        /// <summary>
        /// Returns a new configuration that includes the given services.
        /// </summary>
        /// <param name="configuration">The configuration to extend.</param>
        /// <param name="services">The services to register.</param>
        /// <returns>The new instance with the services.</returns>
        public static IConfiguration With(this IConfiguration configuration, IEnumerable<Object> services)
        {
            configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            services = services ?? throw new ArgumentNullException(nameof(services));
            return new Configuration(services.Concat(configuration.Services));
        }

        /// <summary>
        /// Returns a new configuration that excludes the given services.
        /// </summary>
        /// <param name="configuration">The configuration to extend.</param>
        /// <param name="services">The services to unregister.</param>
        /// <returns>The new instance without the services.</returns>
        public static IConfiguration Without(this IConfiguration configuration, IEnumerable<Object> services)
        {
            configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            services = services ?? throw new ArgumentNullException(nameof(services));
            return new Configuration(configuration.Services.Except(services));
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
            creator = creator ?? throw new ArgumentNullException(nameof(creator));
            return configuration.With((Object)creator);
        }

        /// <summary>
        /// Returns a new configuration that excludes the given service creator.
        /// </summary>
        /// <typeparam name="TService">The type of service to remove.</typeparam>
        /// <param name="configuration">The configuration to extend.</param>
        /// <returns>The new instance without the services.</returns>
        public static IConfiguration Without<TService>(this IConfiguration configuration)
        {
            configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            var items = configuration.Services.OfType<TService>().Cast<Object>();
            var creators = configuration.Services.OfType<Func<IBrowsingContext, TService>>();
            return configuration.Without(items).Without(creators);
        }

        /// <summary>
        /// Checks if the configuration holds any references to the given service.
        /// </summary>
        /// <typeparam name="TService">The type of service to check for.</typeparam>
        /// <param name="configuration">The configuration to examine.</param>
        /// <returns>True if any service / creators are found, otherwise false.</returns>
        public static Boolean Has<TService>(this IConfiguration configuration)
        {
            configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            return configuration.Services.OfType<TService>().Any() || configuration.Services.OfType<Func<IBrowsingContext, TService>>().Any();
        }

        #endregion

        #region Loading Resources

        /// <summary>
        /// Registers the default loader service, if no other loader has been registered yet.
        /// </summary>
        /// <param name="configuration">The configuration to extend.</param>
        /// <param name="setup">Configuration for the loader service.</param>
        /// <returns>The new configuration with the service.</returns>
        public static IConfiguration WithDefaultLoader(this IConfiguration configuration, LoaderOptions setup = null)
        {
            var config = setup ?? new LoaderOptions();

            if (!configuration.Has<IRequester>())
            {
                configuration = configuration
                    .With(new DefaultHttpRequester());
            }

            if (!config.IsNavigationDisabled)
            {
                configuration = configuration
                    .With<IDocumentLoader>(ctx => new DefaultDocumentLoader(ctx, config.Filter));
            }

            if (config.IsResourceLoadingEnabled)
            {
                configuration = configuration
                    .With<IResourceLoader>(ctx => new DefaultResourceLoader(ctx, config.Filter));
            }

            return configuration;
        }

        #endregion

        #region Culture

        /// <summary>
        /// Returns a new configuration that uses the culture with the provided
        /// name.
        /// </summary>
        /// <param name="configuration">The configuration to extend.</param>
        /// <param name="name">The name of the culture to set.</param>
        /// <returns>The new instance with the culture being set.</returns>
        public static IConfiguration WithCulture(this IConfiguration configuration, String name)
        {
            var culture = new CultureInfo(name);
            return configuration.WithCulture(culture);
        }

        /// <summary>
        /// Returns a new configuration that uses the given culture. Providing
        /// null will reset the culture to the default one.
        /// </summary>
        /// <param name="configuration">The configuration to extend.</param>
        /// <param name="culture">The culture to set.</param>
        /// <returns>The new instance with the culture being set.</returns>
        public static IConfiguration WithCulture(this IConfiguration configuration, CultureInfo culture) => configuration.With(culture);

        #endregion

        #region Including Refresh

        /// <summary>
        /// Registeres a handler to include the meta data refresh.
        /// </summary>
        /// <param name="configuration">The configuration to extend.</param>
        /// <param name="shouldRefresh">The optional callback.</param>
        /// <returns>The new instance with the service.</returns>
        public static IConfiguration WithMetaRefresh(this IConfiguration configuration, Predicate<Url> shouldRefresh = null)
        {
            var service = new RefreshMetaHandler(shouldRefresh);
            return configuration.With(service);
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
            var service = new LocaleEncodingProvider();
            return configuration.With(service);
        }

        #endregion

        #region Cookies

        /// <summary>
        /// Registers the default cookie service if no other cookie service has
        /// been registered yet.
        /// </summary>
        /// <param name="configuration">The configuration to extend.</param>
        /// <returns>The new instance with the service.</returns>
        public static IConfiguration WithDefaultCookies(this IConfiguration configuration)
        {
            var service = new MemoryCookieProvider();
            return configuration.With(service);
        }

        #endregion
    }
}
