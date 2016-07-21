namespace AngleSharp.Extensions
{
    using AngleSharp.Commands;
    using AngleSharp.Dom;
    using AngleSharp.Network;
    using AngleSharp.Services;
    using AngleSharp.Services.Media;
    using AngleSharp.Services.Scripting;
    using AngleSharp.Services.Styling;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Represents a helper to construct objects with externally defined
    /// classes and libraries.
    /// </summary>
    static class ConfigurationExtensions
    {
        #region Encoding
        
        public static Encoding DefaultEncoding(this IConfiguration configuration)
        {
            var provider = configuration.GetProvider<IEncodingProvider>();
            var locale = configuration.GetLanguage();
            return provider?.Suggest(locale) ?? Encoding.UTF8;
        }

        #endregion

        #region Languages
        
        public static CultureInfo GetCulture(this IConfiguration configuration)
        {
            return configuration.GetService<CultureInfo>() ?? CultureInfo.CurrentUICulture;
        }
        
        public static CultureInfo GetCultureFromLanguage(this IConfiguration configuration, String language)
        {
            try
            {
                return new CultureInfo(language);
            }
            catch (CultureNotFoundException)
            {
                return configuration.GetCulture();
            }
        }
        
        public static String GetLanguage(this IConfiguration configuration)
        {
            return configuration.GetCulture().Name;
        }

        #endregion

        #region Services
        
        public static TFactory GetFactory<TFactory>(this IConfiguration configuration)
        {
            return configuration.GetServices<TFactory>().Single();
        }

        public static TProvider GetProvider<TProvider>(this IConfiguration configuration)
        {
            return configuration.GetServices<TProvider>().SingleOrDefault();
        }

        public static TService GetService<TService>(this IConfiguration configuration)
        {
            return configuration.GetServices<TService>().FirstOrDefault();
        }

        public static IEnumerable<TService> GetServices<TService>(this IConfiguration configuration)
        {
            return configuration.Services.OfType<TService>();
        }
        
        public static IResourceService<TResource> GetResourceService<TResource>(this IConfiguration configuration, String type)
            where TResource : IResourceInfo
        {
            var services = configuration.GetServices<IResourceService<TResource>>();

            foreach (var service in services)
            {
                if (service.SupportsType(type))
                {
                    return service;
                }
            }

            return default(IResourceService<TResource>);
        }

        #endregion

        #region Cookies
        
        public static String GetCookie(this IConfiguration configuration, String origin)
        {
            var provider = configuration.GetProvider<ICookieProvider>();
            return provider?.GetCookie(origin) ?? String.Empty;
        }
        
        public static void SetCookie(this IConfiguration configuration, String origin, String value)
        {
            var provider = configuration.GetProvider<ICookieProvider>();
            provider?.SetCookie(origin, value);
        }

        #endregion

        #region Spell Check
        
        public static ISpellCheckService GetSpellCheck(this IConfiguration configuration, String language)
        {
            var substitute = default(ISpellCheckService);
            var culture = configuration.GetCultureFromLanguage(language);
            var services = configuration.GetServices<ISpellCheckService>();

            foreach (var service in services)
            {
                if (service.Culture.Equals(culture))
                {
                    return service;
                }
                else if (service.Culture.TwoLetterISOLanguageName.Is(culture.TwoLetterISOLanguageName))
                {
                    substitute = service;
                }
            }

            return substitute;
        }

        #endregion

        #region Parsing Styles
        
        public static ICssStyleEngine GetCssStyleEngine(this IConfiguration configuration)
        {
            return configuration.GetStyleEngine(MimeTypeNames.Css) as ICssStyleEngine;
        }
        
        public static IStyleEngine GetStyleEngine(this IConfiguration configuration, String type)
        {
            var provider = configuration.GetProvider<IStylingProvider>();
            return provider?.GetEngine(type);
        }

        #endregion

        #region Parsing Scripts

        public static Boolean IsScripting(this IConfiguration configuration)
        {
            return configuration?.GetProvider<IScriptingProvider>() != null;
        }
        
        public static IScriptEngine GetScriptEngine(this IConfiguration configuration, String type)
        {
            var provider = configuration.GetProvider<IScriptingProvider>();
            return provider?.GetEngine(type);
        }

        #endregion

        #region Context
        
        public static IBrowsingContext NewContext(this IConfiguration configuration, Sandboxes security = Sandboxes.None)
        {
            var factory = configuration.GetFactory<IContextFactory>();
            return factory.Create(configuration, security);
        }
        
        public static IBrowsingContext FindContext(this IConfiguration configuration, String name)
        {
            var factory = configuration.GetFactory<IContextFactory>();
            return factory.Find(name);
        }

        #endregion

        #region Commands
        
        public static ICommand GetCommand(this IConfiguration configuration, String commandId)
        {
            var provider = configuration.GetProvider<ICommandProvider>();
            return provider?.GetCommand(commandId);
        }

        #endregion
    }
}
