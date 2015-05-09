namespace AngleSharp.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Text;
    using AngleSharp.Css;
    using AngleSharp.Dom;
    using AngleSharp.Scripting;
    using AngleSharp.Services;

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

            var service = configuration.GetService<IEncodingService>();
            var locale = configuration.GetLanguage();
            return service != null ? service.Suggest(locale) : Encoding.UTF8;
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

        #region Parsing Styles

        public static Boolean IsStyling(this IConfiguration configuration)
        {
            return configuration.GetService<IStylingService>() != null;
        }

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
            var service = configuration.GetService<IStylingService>();

            if (service != null)
                return service.GetEngine(type);

            return null;
        }

        #endregion

        #region Parsing Scripts

        public static Boolean IsScripting(this IConfiguration configuration)
        {
            return configuration.GetService<IScriptingService>() != null;
        }

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
            var service = configuration.GetService<IScriptingService>();

            if (service != null)
                return service.GetEngine(type);

            return null;
        }

        #endregion

        #region Context

        /// <summary>
        /// Creates a new browsing context without any name.
        /// </summary>
        /// <param name="options">The configuration to use.</param>
        /// <param name="security">The optional sandboxing flag to use.</param>
        /// <returns>The new context.</returns>
        public static IBrowsingContext NewContext(this IConfiguration options, Sandboxes security = Sandboxes.None)
        {
            var service = options.GetService<IContextService>();

            if (service == null)
                return new BrowsingContext(options, security);

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
    }
}
