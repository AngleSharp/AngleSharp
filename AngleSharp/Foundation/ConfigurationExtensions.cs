namespace AngleSharp
{
    using AngleSharp.DOM;
    using AngleSharp.DOM.Css;
    using AngleSharp.Infrastructure;
    using AngleSharp.Parser.Css;
    using System;
    using System.IO;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents a helper to construct objects with externally
    /// defined classes and libraries.
    /// </summary>
    static class ConfigurationExtensions
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

            return DocumentEncoding.Suggest(configuration.Language);
        }

        #endregion

        #region Loading

        /// <summary>
        /// Loads the given URI by using an asynchronous GET request.
        /// </summary>
        /// <param name="configuration">The configuration to use.</param>
        /// <param name="url">The url that yields the path to the desired action.</param>
        /// <returns>The task which will eventually return the stream.</returns>
        public static Task<Stream> LoadAsync(this IConfiguration configuration, Uri url)
        {
            return configuration.LoadAsync(url, CancellationToken.None);
        }

        /// <summary>
        /// Loads the given URI by using an asynchronous GET request.
        /// </summary>
        /// <param name="configuration">The configuration to use.</param>
        /// <param name="url">The url that yields the path to the desired action.</param>
        /// <param name="cancel">The token which can be used to cancel the request.</param>
        /// <param name="force">[Optional] True if the request will be considered despite no allowed external request.</param>
        /// <returns>The task which will eventually return the stream.</returns>
        public static async Task<Stream> LoadAsync(this IConfiguration configuration, Uri url, CancellationToken cancel, Boolean force = false)
        {
            if (!configuration.AllowRequests && !force)
                return Stream.Null;

            var requester = configuration.GetRequester();

            if (requester == null)
                throw new NullReferenceException("No HTTP requester has been set up in the configuration.");

            var request = configuration.CreateRequest();

            if (request == null)
                throw new NullReferenceException("Unable to create instance of IRequest. Try changing the provided configuration.");

            request.Address = url;
            request.Method = HttpMethod.Get;
            var response = await requester.RequestAsync(request, cancel);
            return response.Content;
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
        /// <returns>The task which will eventually return the stream.</returns>
        public static Task<Stream> SendAsync(this IConfiguration configuration, Uri url, Stream content = null, String mimeType = null, HttpMethod method = HttpMethod.Post)
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
        /// <param name="force">[Optional] True if the request will be considered despite no allowed external request.</param>
        /// <returns>The task which will eventually return the stream.</returns>
        public static async Task<Stream> SendAsync(this IConfiguration configuration, Uri url, Stream content, String mimeType, HttpMethod method, CancellationToken cancel, Boolean force = false)
        {
            if (!configuration.AllowRequests && !force)
                return Stream.Null;

            var requester = configuration.GetRequester();

            if (requester == null)
                throw new NullReferenceException("No HTTP requester has been set up in the configuration.");

            var request = configuration.CreateRequest();

            if (request == null)
                throw new NullReferenceException("Unable to create instance of IRequest. Try changing the provided configuration.");

            request.Address = url;
            request.Content = content;

            if (mimeType != null)
                request.Headers[HeaderNames.ContentType] = mimeType;

            request.Method = method;
            var response = await requester.RequestAsync(request, cancel);
            return response.Content;
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
        /// <param name="type">The optional mime-type of the source code.</param>
        /// <returns>A freshly created stylesheet, if any.</returns>
        public static StyleSheet ParseStyling(this IConfiguration configuration, String source, String type = null)
        {
            if (configuration.IsStyling)
            {
                var engine = configuration.GetStyleEngine(type ?? MimeTypes.Css);

                if (engine != null)
                    return engine.CreateStyleSheetFor(source);
            }

            return null;
        }

        #endregion

        #region Styling Styles

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

        #endregion
    }
}
