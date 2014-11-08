namespace AngleSharp
{
    using AngleSharp.DOM;
    using AngleSharp.DOM.Css;
    using AngleSharp.Infrastructure;
    using AngleSharp.Network;
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

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
        public static TConfiguration WithDefaultRequester<TConfiguration>(this TConfiguration configuration, IInfo agent = null)
            where TConfiguration : Configuration
        {
            configuration.Register(new DefaultRequester(agent ?? DefaultInfo.Instance));
            return configuration;
        }

        #endregion

        #region HTML creation

        /// <summary>
        /// Builds a new HTML Document with the given source code string.
        /// </summary>
        /// <param name="configuration">Options to use for the document generation.</param>
        /// <param name="sourceCode">The string to use as source code.</param>
        /// <param name="url">[Optional] The base URL of the document.</param>
        /// <returns>The constructed HTML document.</returns>
        public static IDocument ParseHtml(this IConfiguration configuration, String sourceCode, String url = null)
        {
            return DocumentBuilder.Html(sourceCode, configuration, url);
        }

        /// <summary>
        /// Builds a new HTML Document asynchronously with the given (network) stream.
        /// </summary>
        /// <param name="configuration">Options to use for the document generation.</param>
        /// <param name="content">The stream of chars to use as source code.</param>
        /// <param name="url">[Optional] The base URL of the document.</param>
        /// <returns>The task to construct the HTML document.</returns>
        public static Task<IDocument> ParseHtmlAsync(this IConfiguration configuration, Stream content, String url = null)
        {
            return DocumentBuilder.HtmlAsync(content, configuration, url);
        }

        /// <summary>
        /// Builds a new HTML Document by asynchronously requesting the given URL.
        /// </summary>
        /// <param name="configuration">Options to use for the document generation.</param>
        /// <param name="url">The URL which points to the address containing the source code.</param>
        /// <returns>The task that constructs the HTML document.</returns>
        public static Task<IDocument> ParseHtmlAsync(this IConfiguration configuration, Uri url)
        {
            return DocumentBuilder.HtmlAsync(url, configuration);
        }

        #endregion

        #region CSS creation

        /// <summary>
        /// Builds a new CSSStyleSheet with the given source code string.
        /// </summary>
        /// <param name="configuration">Options to use for the document generation.</param>
        /// <param name="sourceCode">The string to use as source code.</param>
        /// <param name="url">[Optional] The base URL of the document.</param>
        /// <returns>The constructed CSS stylesheet.</returns>
        public static ICssStyleSheet ParseCss(this IConfiguration configuration, String sourceCode, String url = null)
        {
            return DocumentBuilder.Css(sourceCode, configuration, url);
        }

        /// <summary>
        /// Builds a new CSSStyleSheet asynchronously by requesting the given (network) stream.
        /// </summary>
        /// <param name="configuration">Options to use for the document generation.</param>
        /// <param name="stream">The stream of chars to use as source code.</param>
        /// <param name="url">[Optional] The base URL of the document.</param>
        /// <returns>The task which constructs the CSS stylesheet.</returns>
        public static Task<ICssStyleSheet> ParseCssAsync(this IConfiguration configuration, Stream stream, String url = null)
        {
            return DocumentBuilder.CssAsync(stream, configuration, url);
        }

        /// <summary>
        /// Builds a new CSSStyleSheet asynchronously by requesting the given URL.
        /// </summary>
        /// <param name="configuration">Options to use for the document generation.</param>
        /// <param name="url">The URL which points to the address containing the source code.</param>
        /// <returns>The task which constructs the CSS stylesheet.</returns>
        public static Task<ICssStyleSheet> ParseCssAsync(this IConfiguration configuration, Uri url)
        {
            return DocumentBuilder.CssAsync(url, configuration);
        }

        #endregion
    }
}
