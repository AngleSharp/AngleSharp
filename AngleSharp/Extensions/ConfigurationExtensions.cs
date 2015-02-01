namespace AngleSharp.Extensions
{
    using AngleSharp.Dom;
    using AngleSharp.Network;
    using AngleSharp.Services;
    using AngleSharp.Services.Media;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a helper to construct objects with externally defined
    /// classes and libraries.
    /// </summary>
    [DebuggerStepThrough]
    static class ConfigurationExtensions
    {
        #region Encoding

        /// <summary>
        /// Gets the default encoding for the given configuration.
        /// </summary>
        /// <param name="configuration">
        /// The configuration to use for getting the default encoding.
        /// </param>
        /// <returns>The current encoding.</returns>
        public static Encoding DefaultEncoding(this IConfiguration configuration)
        {
            if (configuration == null)
                configuration = Configuration.Default;

            return TextEncoding.Suggest(configuration.GetLanguage());
        }

        #endregion

        #region Languages

        /// <summary>
        /// Gets the provided current culture.
        /// </summary>
        /// <param name="options">The configuration to use.</param>
        /// <returns>The culture information.</returns>
        public static CultureInfo GetCulture(this IConfiguration options)
        {
            return options.Culture ?? CultureInfo.CurrentUICulture;
        }

        /// <summary>
        /// Gets the culture from the given language string or falls back to
        /// the default culture of the provided configuration (if any). Last
        /// resort is to use the current UI culture.
        /// </summary>
        /// <param name="options">The configuration to use.</param>
        /// <param name="language">The language string, e.g. en-US.</param>
        /// <returns>The culture information.</returns>
        public static CultureInfo GetCultureFromLanguage(this IConfiguration options, String language)
        {
            try
            {
                return new CultureInfo(language);
            }
            catch (CultureNotFoundException)
            {
                return options.GetCulture();
            }
        }

        /// <summary>
        /// Gets the provided current language.
        /// </summary>
        /// <param name="options">The configuration to use.</param>
        /// <returns>The language string, e.g. en-US.</returns>
        public static String GetLanguage(this IConfiguration options)
        {
            return options.GetCulture().Name;
        }

        #endregion

        #region Services

        /// <summary>
        /// Gets a service with a specific type from the configuration, if it
        /// has been registered.
        /// </summary>
        /// <typeparam name="TService">
        /// The type of the service to get.
        /// </typeparam>
        /// <param name="configuration">
        /// The configuration instance to use.
        /// </param>
        /// <returns>The service, if any.</returns>
        public static TService GetService<TService>(this IConfiguration configuration)
            where TService : IService
        {
            foreach (var service in configuration.Services)
            {
                if (service is TService)
                    return (TService)service;
            }

            return default(TService);
        }

        /// <summary>
        /// Gets services with a specific type from the configuration, if it
        /// has been registered.
        /// </summary>
        /// <typeparam name="TService">
        /// The type of the service to get.
        /// </typeparam>
        /// <param name="configuration">
        /// The configuration instance to use.
        /// </param>
        /// <returns>An enumerable over all services.</returns>
        public static IEnumerable<TService> GetServices<TService>(this IConfiguration configuration)
            where TService : IService
        {
            foreach (var service in configuration.Services)
            {
                if (service is TService)
                    yield return (TService)service;
            }
        }

        #endregion

        #region Cookies

        /// <summary>
        /// Gets the cookie for the provided address.
        /// </summary>
        /// <param name="options">The configuration to use.</param>
        /// <param name="origin">The origin of the cookie.</param>
        /// <returns>The value of the cookie.</returns>
        public static String GetCookie(this IConfiguration options, String origin)
        {
            var service = options.GetService<ICookieService>();

            if (service != null)
                return service[origin];

            return String.Empty;
        }

        /// <summary>
        /// Sets the cookie for the provided address.
        /// </summary>
        /// <param name="options">The configuration to use.</param>
        /// <param name="origin">The origin of the cookie.</param>
        /// <param name="value">The value of the cookie.</param>
        public static void SetCookie(this IConfiguration options, String origin, String value)
        {
            var service = options.GetService<ICookieService>();

            if (service != null)
                service[origin] = value;
        }

        #endregion

        #region Spell Check

        /// <summary>
        /// Gets a spellchecker for the given language.
        /// </summary>
        /// <param name="options">The configuration to use.</param>
        /// <param name="language">The language to consider.</param>
        /// <returns>The spellchecker or null, if there is none.</returns>
        public static ISpellCheckService GetSpellCheck(this IConfiguration options, String language)
        {
            ISpellCheckService substitute = null;
            var culture = options.GetCultureFromLanguage(language);

            foreach (var spellchecker in options.GetServices<ISpellCheckService>())
            {
                if (spellchecker.Culture.Equals(culture))
                    return spellchecker;
                else if (spellchecker.Culture.TwoLetterISOLanguageName == culture.TwoLetterISOLanguageName)
                    substitute = spellchecker;
            }

            return substitute;
        }

        #endregion

        #region Resource Services

        /// <summary>
        /// Tries to get a requester for the given scheme.
        /// </summary>
        /// <param name="options">The configuration to use.</param>
        /// <param name="protocol">The scheme to find a requester for.</param>
        /// <returns>A requester for the scheme or null.</returns>
        public static IRequester GetRequester(this IConfiguration options, String protocol)
        {
            foreach (var requester in options.Requesters)
            {
                if (requester.SupportsProtocol(protocol))
                    return requester;
            }

            return null;
        }
        
        /// <summary>
        /// Tries to load an image if a proper image service can be found.
        /// </summary>
        /// <param name="options">The configuration to use.</param>
        /// <param name="url">The address of the image.</param>
        /// <returns>A task that will end with an image info or null.</returns>
        public static Task<TResource> LoadResource<TResource>(this IConfiguration options, Url url)
            where TResource : IResourceInfo
        {
            return options.LoadResource<TResource>(url, CancellationToken.None);
        }

        /// <summary>
        /// Tries to load an image if a proper image service can be found.
        /// </summary>
        /// <param name="options">The configuration to use.</param>
        /// <param name="url">The address of the image.</param>
        /// <param name="cancel">
        /// Token to trigger in case of cancellation.
        /// </param>
        /// <returns>A task that will end with an image info or null.</returns>
        public static async Task<TResource> LoadResource<TResource>(this IConfiguration options, Url url, CancellationToken cancel)
            where TResource : IResourceInfo
        {
            var requester = options.GetRequester(url.Scheme);

            if (requester != null)
            {
                using (var response = await requester.LoadAsync(url, cancel).ConfigureAwait(false))
                {
                    if (response == null)
                        return default(TResource);

                    var resourceServices = options.GetServices<IResourceService<TResource>>();

                    foreach (var resourceService in resourceServices)
                    {
                        if (resourceService.SupportsType(response.Headers[HeaderNames.ContentType]))
                            return await resourceService.CreateAsync(response, cancel).ConfigureAwait(false);
                    }
                }
            }

            return default(TResource);
        }

        #endregion

        #region Parsing Styles

        /// <summary>
        /// Tries to resolve a style engine for the given type name.
        /// </summary>
        /// <param name="configuration">The configuration to use.</param>
        /// <param name="type">The mime-type of the source code.</param>
        /// <returns>
        /// The style engine or null, if the type if unknown.
        /// </returns>
        public static IStyleEngine GetStyleEngine(this IConfiguration configuration, String type)
        {
            foreach (var styleEngine in configuration.StyleEngines)
            {
                if (styleEngine.Type.Equals(type, StringComparison.OrdinalIgnoreCase))
                    return styleEngine;
            }

            return null;
        }
        
        /// <summary>
        /// Parses the given source code by using the supplied type name
        /// (otherwise it is text/css) and returns the created stylesheet.
        /// </summary>
        /// <param name="configuration">The configuration to use.</param>
        /// <param name="source">
        /// The source code describing the style sheet.
        /// </param>
        /// <param name="options">
        /// The options with the parameters for evaluating the style.
        /// </param>
        /// <param name="type">
        /// The optional mime-type of the source code.
        /// </param>
        /// <returns>A freshly created stylesheet, if any.</returns>
        public static IStyleSheet ParseStyling(this IConfiguration configuration, String source, StyleOptions options, String type = null)
        {
            var engine = configuration.GetStyleEngine(type ?? MimeTypes.Css);

            if (engine != null)
                return engine.Parse(source, options);

            return null;
        }

        /// <summary>
        /// Parses the given source code by using the supplied type name
        /// (otherwise it is text/css) and returns the created stylesheet.
        /// </summary>
        /// <param name="configuration">The configuration to use.</param>
        /// <param name="response">
        /// The response with the stream representing the source of the
        /// stylesheet.
        /// </param>
        /// <param name="options">
        /// The options with the parameters for evaluating the style.
        /// </param>
        /// <param name="type">
        /// The optional mime-type of the source code.
        /// </param>
        /// <returns>A freshly created stylesheet, if any.</returns>
        public static IStyleSheet ParseStyling(this IConfiguration configuration, IResponse response, StyleOptions options, String type = null)
        {
            var engine = configuration.GetStyleEngine(type ?? MimeTypes.Css);

            if (engine != null)
                return engine.Parse(response, options);

            return null;
        }

        #endregion

        #region Parsing Scripts

        /// <summary>
        /// Tries to resolve a script engine for the given type name.
        /// </summary>
        /// <param name="configuration">The configuration to use.</param>
        /// <param name="type">The mime-type of the source code.</param>
        /// <returns>
        /// The script engine or null, if the type if unknown.
        /// </returns>
        public static IScriptEngine GetScriptEngine(this IConfiguration configuration, String type)
        {
            foreach (var scriptEngine in configuration.ScriptEngines)
            {
                if (scriptEngine.Type.Equals(type, StringComparison.OrdinalIgnoreCase))
                    return scriptEngine;
            }

            return null;
        }

        /// <summary>
        /// Parses the given source code by using the supplied type name
        /// (otherwise it is text/css) and returns the created stylesheet.
        /// </summary>
        /// <param name="configuration">The configuration to use.</param>
        /// <param name="source">The source code of the style sheet.</param>
        /// <param name="options">The options for running the script.</param>
        /// <param name="type">
        /// The optional mime-type of the source code.
        /// </param>
        public static void RunScript(this IConfiguration configuration, String source, ScriptOptions options, String type = null)
        {
            if (configuration.IsScripting)
            {
                var engine = configuration.GetScriptEngine(type ?? MimeTypes.DefaultJavaScript);

                if (engine != null)
                    engine.Evaluate(source, options);
            }
        }

        /// <summary>
        /// Parses the given source code by using the supplied type name
        /// (otherwise it is text/css) and returns the created stylesheet.
        /// </summary>
        /// <param name="configuration">The configuration to use.</param>
        /// <param name="response">
        /// The response with the stream representing the source of the script.
        /// </param>
        /// <param name="options">
        /// The options for running the script.
        /// </param>
        /// <param name="type">
        /// The optional mime-type of the source code.
        /// </param>
        public static void RunScript(this IConfiguration configuration, IResponse response, ScriptOptions options, String type = null)
        {
            if (configuration.IsScripting)
            {
                var engine = configuration.GetScriptEngine(type ?? MimeTypes.DefaultJavaScript);

                if (engine != null)
                    engine.Evaluate(response, options);
            }
        }

        #endregion

        #region Context

        /// <summary>
        /// Creates a new browsing context without any name.
        /// </summary>
        /// <param name="options">The configuration to use.</param>
        /// <param name="security">The sandboxing flag to use.</param>
        /// <returns>The new context.</returns>
        public static IBrowsingContext NewContext(this IConfiguration options, Sandboxes security)
        {
            var service = options.GetService<IContextService>();

            if (service == null)
                return new SimpleBrowsingContext(options, security);

            return service.Create(options, security);
        }

        /// <summary>
        /// Finds an existing browsing context with the given name.
        /// </summary>
        /// <param name="options">The configuration to use.</param>
        /// <param name="name">The name of the context to find.</param>
        /// <returns>
        /// The existing context, or null, if no context with the provided
        /// name could be find.
        /// </returns>
        public static IBrowsingContext FindContext(this IConfiguration options, String name)
        {
            var service = options.GetService<IContextService>();

            if (service != null)
                return service.Find(name);

            return null;
        }

        #endregion

        #region Commands

        /// <summary>
        /// Tries to resolve a command service with the given command id.
        /// </summary>
        /// <param name="options">
        /// The configuration that contains all command services.
        /// </param>
        /// <param name="commandId">The id of the command to find.</param>
        /// <returns>
        /// The command with the given id if that exists, otherwise null.
        /// </returns>
        public static ICommandService GetCommand(this IConfiguration options, String commandId)
        {
            foreach (var command in options.GetServices<ICommandService>())
            {
                if (command.CommandId.Equals(commandId, StringComparison.OrdinalIgnoreCase))
                    return command;
            }

            return null;
        }

        #endregion

        #region Cache loading

        /// <summary>
        /// Tries to load the content for the given url from cache.
        /// </summary>
        /// <param name="options">The configuration to use.</param>
        /// <param name="url">The address of the resource.</param>
        /// <returns>A task with the contents or null.</returns>
        public static Task<Stream> LoadCached(this IConfiguration options, Url url)
        {
            return options.LoadCached(url, CancellationToken.None);
        }

        /// <summary>
        /// Tries to load the content for the given url from cache.
        /// </summary>
        /// <param name="options">The configuration to use.</param>
        /// <param name="url">The address of the resource.</param>
        /// <param name="cancel">
        /// Token to trigger in case of cancellation.
        /// </param>
        /// <returns>A task with the contents or null.</returns>
        public static async Task<Stream> LoadCached(this IConfiguration options, Url url, CancellationToken cancel)
        {
            var caches = options.GetServices<ICacheLoaderService>();

            foreach (var cache in caches)
            {
                var result = await cache.LoadAsync(url, cancel);

                if (result != null)
                    return result;
            }

            return null;
        }

        #endregion
    }
}
