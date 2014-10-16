namespace AngleSharp.Infrastructure
{
    using AngleSharp.DOM;
    using AngleSharp.DOM.Css;
    using AngleSharp.Network;
    using AngleSharp.Parser.Css;
    using System;

    /// <summary>
    /// The CSS style engine for creating CSSStyleSheet instances.
    /// </summary>
    public class CssStyleEngine : IStyleEngine
    {
        /// <summary>
        /// Gets the type for the CSS style engine.
        /// </summary>
        public String Type
        {
            get { return MimeTypes.Css; }
        }

        /// <summary>
        /// Creates a style sheet for the given source.
        /// </summary>
        /// <param name="source">The source code describing the style sheet.</param>
        /// <param name="options">The options with the parameters for evaluating the style.</param>
        /// <returns>The created style sheet.</returns>
        public IStyleSheet Parse(String source, StyleOptions options)
        {
            var style = new CSSStyleSheet(source) { OwnerNode = options.Element };
            var parser = new CssParser(style);
            parser.Parse();
            return style;
        }

        /// <summary>
        /// Creates a style sheet for the given response from a request.
        /// </summary>
        /// <param name="response">The response with the stream representing the source of the stylesheet.</param>
        /// <param name="options">The options with the parameters for evaluating the style.</param>
        /// <returns>The created style sheet.</returns>
        public IStyleSheet Parse(IResponse response, StyleOptions options)
        {
            var style = new CSSStyleSheet(new TextSource(response.Content)) { Href = response.Address.Href, OwnerNode = options.Element };
            var parser = new CssParser(style);
            parser.Parse();
            return style;
        }
    }
}
