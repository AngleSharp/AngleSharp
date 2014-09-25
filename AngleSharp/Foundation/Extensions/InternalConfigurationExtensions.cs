namespace AngleSharp
{
    using AngleSharp.DOM;
using AngleSharp.Infrastructure;
using AngleSharp.Media;
using AngleSharp.Network;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

    /// <summary>
    /// Represents a helper to construct objects with externally
    /// defined classes and libraries.
    /// </summary>
    [DebuggerStepThrough]
    static class InternalConfigurationExtensions
    {
        #region Encoding

        /// <summary>
        /// Gets the default encoding for the given configuration.
        /// </summary>
        /// <param name="configuration">The configuration to use for getting the default encoding.</param>
        /// <returns>The current encoding.</returns>
        public static Encoding DefaultEncoding(this IConfiguration configuration)
        {
            if (configuration == null)
                configuration = Configuration.Default;

            return DocumentEncoding.Suggest(configuration.GetLanguage());
        }

        /// <summary>
        /// Gets the provided current language.
        /// </summary>
        /// <param name="configuration">The configuration to use.</param>
        /// <returns>The language string, e.g. en-US.</returns>
        public static String GetLanguage(this IConfiguration configuration)
        {
            return (configuration.Culture ?? System.Globalization.CultureInfo.CurrentUICulture).Name;
        }

        #endregion

        #region Loading

        /// <summary>
        /// Loads the given URI by using an asynchronous GET request.
        /// </summary>
        /// <param name="configuration">The configuration to use.</param>
        /// <param name="url">The url that yields the path to the desired action.</param>
        /// <returns>The task which will eventually return the response.</returns>
        public static Task<IResponse> LoadAsync(this IConfiguration configuration, Url url)
        {
            return configuration.LoadAsync(url, CancellationToken.None);
        }

        /// <summary>
        /// Loads the given URI by using an asynchronous GET request.
        /// </summary>
        /// <param name="configuration">The configuration to use.</param>
        /// <param name="url">The url that yields the path to the desired action.</param>
        /// <param name="cancel">The token which can be used to cancel the request.</param>
        /// <returns>The task which will eventually return the response.</returns>
        public static Task<IResponse> LoadAsync(this IConfiguration configuration, Url url, CancellationToken cancel)
        {
            var requester = configuration.GetRequester(url.Scheme);

            if (requester == null)
                return Empty<IResponse>();

            return requester.RequestAsync(new DefaultRequest
            {
                Address = url,
                Method = HttpMethod.Get
            }, cancel);
        }

        /// <summary>
        /// Loads the given URI by using an asynchronous GET request with possibly considering the default requester.
        /// </summary>
        /// <param name="configuration">The configuration to use.</param>
        /// <param name="url">The url that yields the path to the desired action.</param>
        /// <param name="cancel">The token which can be used to cancel the request.</param>
        /// <returns>The task which will eventually return the response.</returns>
        public static Task<IResponse> LoadForcedAsync(this IConfiguration configuration, Url url, CancellationToken cancel)
        {
            var requester = configuration.GetRequester(url.Scheme) ?? new DefaultRequester(new DefaultInfo());
            return requester.RequestAsync(new DefaultRequest
            {
                Address = url,
                Method = HttpMethod.Get
            }, cancel);
        }

        #endregion

        #region Fetching

        /// <summary>
        /// Performs a potentially CORS-enabled fetch from the given URI by using an asynchronous GET request.
        /// </summary>
        /// <param name="configuration">The configuration to use.</param>
        /// <param name="url">The url that yields the path to the desired action.</param>
        /// <param name="cors">The cross origin settings to use.</param>
        /// <param name="origin">The origin of the page that requests the loading.</param>
        /// <param name="defaultBehavior">The default behavior in case it is undefined.</param>
        /// <returns>The task which will eventually return the stream.</returns>
        public static Task<IResponse> LoadWithCorsAsync(this IConfiguration configuration, Url url, CorsSetting cors, String origin, OriginBehavior defaultBehavior)
        {
            return configuration.LoadWithCorsAsync(url, cors, origin, defaultBehavior, CancellationToken.None);
        }

        /// <summary>
        /// Performs a potentially CORS-enabled fetch from the given URI by using an asynchronous GET request.
        /// </summary>
        /// <param name="configuration">The configuration to use.</param>
        /// <param name="url">The url that yields the path to the desired action.</param>
        /// <param name="cors">The cross origin settings to use.</param>
        /// <param name="origin">The origin of the page that requests the loading.</param>
        /// <param name="defaultBehavior">The default behavior in case it is undefined.</param>
        /// <param name="cancel">The token which can be used to cancel the request.</param>
        /// <returns>The task which will eventually return the stream.</returns>
        public static Task<IResponse> LoadWithCorsAsync(this IConfiguration configuration, Url url, CorsSetting cors, String origin, OriginBehavior defaultBehavior, CancellationToken cancel)
        {
            var requester = configuration.GetRequester(url.Scheme);

            if (requester == null)
                return Empty<IResponse>();

            //TODO
            //http://www.w3.org/TR/html5/infrastructure.html#potentially-cors-enabled-fetch
            return requester.RequestAsync(new DefaultRequest
            {
                Address = url,
                Method = HttpMethod.Get
            }, cancel);
        }

        #endregion

        #region Sending

        /// <summary>
        /// Loads the given URI by using an asynchronous request with the given method and body.
        /// </summary>
        /// <param name="configuration">The configuration to use.</param>
        /// <param name="url">The url that yields the path to the desired action.</param>
        /// <param name="content">The body that should be used in the request.</param>
        /// <param name="mimeType">The mime-type of the request.</param>
        /// <param name="method">The method that is used for sending the request asynchronously.</param>
        /// <returns>The task which will eventually return the response.</returns>
        public static Task<IResponse> SendAsync(this IConfiguration configuration, Url url, Stream content = null, String mimeType = null, HttpMethod method = HttpMethod.Post)
        {
            return configuration.SendAsync(url, content, mimeType, method, CancellationToken.None);
        }

        /// <summary>
        /// Loads the given URI by using an asynchronous request with the given method and body.
        /// </summary>
        /// <param name="configuration">The configuration to use.</param>
        /// <param name="url">The url that yields the path to the desired action.</param>
        /// <param name="content">The body that should be used in the request.</param>
        /// <param name="mimeType">The mime-type of the request.</param>
        /// <param name="method">The method that is used for sending the request asynchronously.</param>
        /// <param name="cancel">The token which can be used to cancel the request.</param>
        /// <returns>The task which will eventually return the response.</returns>
        public static Task<IResponse> SendAsync(this IConfiguration configuration, Url url, Stream content, String mimeType, HttpMethod method, CancellationToken cancel)
        {
            var requester = configuration.GetRequester(url.Scheme);

            if (requester == null)
                return Empty<IResponse>();

            var request = new DefaultRequest
            {
                Address = url,
                Content = content,
                Method = method
            };

            if (mimeType != null)
                request.Headers[HeaderNames.ContentType] = mimeType;

            return requester.RequestAsync(request, cancel);
        }

        #endregion

        #region Services

        /// <summary>
        /// Gets a service with a specific type from the configuration, if it has been registered.
        /// </summary>
        /// <typeparam name="TService">The type of the service to get.</typeparam>
        /// <param name="configuration">The configuration instance to use.</param>
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
        /// Gets services with a specific type from the configuration, if it has been registered.
        /// </summary>
        /// <typeparam name="TService">The type of the service to get.</typeparam>
        /// <param name="configuration">The configuration instance to use.</param>
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

        #region Resource Services
        
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
        /// <param name="cancel">Token to trigger in case of cancellation.</param>
        /// <returns>A task that will end with an image info or null.</returns>
        public static async Task<TResource> LoadResource<TResource>(this IConfiguration options, Url url, CancellationToken cancel)
            where TResource : IResourceInfo
        {
            var response = await options.LoadAsync(url, cancel).ConfigureAwait(false);

            if (response != null)
            {
                var imageServices = options.GetServices<IResourceService<TResource>>();

                foreach (var imageService in imageServices)
                {
                    if (imageService.SupportsType(response.Headers[HeaderNames.ContentType]))
                        return await imageService.CreateAsync(response, cancel).ConfigureAwait(false);
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
        /// <returns>The style engine or null, if the type if unknown.</returns>
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
        /// Parses the given source code by using the supplied type name (otherwise it is text/css) and
        /// returns the created stylesheet.
        /// </summary>
        /// <param name="configuration">The configuration to use.</param>
        /// <param name="source">The source code of the style sheet.</param>
        /// <param name="owner">The optional owner of the stylesheet, if any.</param>
        /// <param name="type">The optional mime-type of the source code.</param>
        /// <returns>A freshly created stylesheet, if any.</returns>
        public static IStyleSheet ParseStyling(this IConfiguration configuration, String source, IElement owner = null, String type = null)
        {
            if (configuration.IsStyling)
            {
                var engine = configuration.GetStyleEngine(type ?? MimeTypes.Css);

                if (engine != null)
                    return engine.CreateStyleSheetFor(source, owner);
            }

            return null;
        }

        /// <summary>
        /// Parses the given source code by using the supplied type name (otherwise it is text/css) and
        /// returns the created stylesheet.
        /// </summary>
        /// <param name="configuration">The configuration to use.</param>
        /// <param name="source">The source code of the style sheet.</param>
        /// <param name="owner">The optional owner of the stylesheet, if any.</param>
        /// <param name="type">The optional mime-type of the source code.</param>
        /// <returns>A freshly created stylesheet, if any.</returns>
        public static IStyleSheet ParseStyling(this IConfiguration configuration, Stream source, IElement owner = null, String type = null)
        {
            if (configuration.IsStyling)
            {
                var engine = configuration.GetStyleEngine(type ?? MimeTypes.Css);

                if (engine != null)
                    return engine.CreateStyleSheetFor(source, owner);
            }

            return null;
        }

        #endregion

        #region Parsing Scripts

        /// <summary>
        /// Tries to resolve a script engine for the given type name.
        /// </summary>
        /// <param name="configuration">The configuration to use.</param>
        /// <param name="type">The mime-type of the source code.</param>
        /// <returns>The script engine or null, if the type if unknown.</returns>
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
        /// Parses the given source code by using the supplied type name (otherwise it is text/css) and
        /// returns the created stylesheet.
        /// </summary>
        /// <param name="configuration">The configuration to use.</param>
        /// <param name="source">The source code of the style sheet.</param>
        /// <param name="options">The options for running the script.</param>
        /// <param name="type">The optional mime-type of the source code.</param>
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
        /// Parses the given source code by using the supplied type name (otherwise it is text/css) and
        /// returns the created stylesheet.
        /// </summary>
        /// <param name="configuration">The configuration to use.</param>
        /// <param name="source">The source code of the style sheet.</param>
        /// <param name="options">The options for running the script.</param>
        /// <param name="type">The optional mime-type of the source code.</param>
        public static void RunScript(this IConfiguration configuration, Stream source, ScriptOptions options, String type = null)
        {
            if (configuration.IsScripting)
            {
                var engine = configuration.GetScriptEngine(type ?? MimeTypes.DefaultJavaScript);

                if (engine != null)
                    engine.Evaluate(source, options);
            }
        }

        #endregion

        #region Helpers

        static IRequester GetRequester(this IConfiguration configuration, String protocol)
        {
            foreach (var requester in configuration.Requesters)
            {
                if (requester.SupportsProtocol(protocol))
                    return requester;
            }

            return null;
        }

        static Task<TResult> Empty<TResult>()
            where TResult : class
        {
#if LEGACY
            var task = new TaskCompletionSource<TResult>();
            task.SetResult(null);
            return task.Task;
#else
            return Task.FromResult<TResult>(null);
#endif
        }

        #endregion
    }
}
