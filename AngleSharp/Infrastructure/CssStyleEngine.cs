namespace AngleSharp.Infrastructure
{
    using AngleSharp.DOM;
    using AngleSharp.DOM.Css;
    using AngleSharp.Parser.Css;
    using System;
    using System.IO;

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
        /// <returns>The created style sheet.</returns>
        public IStyleSheet CreateStyleSheetFor(String source)
        {
            var style = new CSSStyleSheet();
            var parser = new CssParser(style, source);
            parser.Parse();
            return style;
        }

        /// <summary>
        /// Creates a style sheet for the given stream.
        /// </summary>
        /// <param name="source">The stream with the source describing the style sheet.</param>
        /// <returns>The created style sheet.</returns>
        public IStyleSheet CreateStyleSheetFor(Stream source)
        {
            var style = new CSSStyleSheet();
            var parser = new CssParser(style, source);
            parser.Parse();
            return style;
        }
    }
}
