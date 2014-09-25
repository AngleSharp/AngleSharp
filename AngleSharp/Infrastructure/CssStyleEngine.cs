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
        /// <param name="owner">The owner of the style sheet, if any.</param>
        /// <returns>The created style sheet.</returns>
        public IStyleSheet CreateStyleSheetFor(String source, IElement owner = null)
        {
            var style = new CSSStyleSheet(source) { OwnerNode = owner };
            var parser = new CssParser(style);
            parser.Parse();
            return style;
        }

        /// <summary>
        /// Creates a style sheet for the given stream.
        /// </summary>
        /// <param name="source">The stream with the source describing the style sheet.</param>
        /// <param name="owner">The owner of the style sheet, if any.</param>
        /// <returns>The created style sheet.</returns>
        public IStyleSheet CreateStyleSheetFor(Stream source, IElement owner = null)
        {
            var style = new CSSStyleSheet(new TextSource(source)) { OwnerNode = owner };
            var parser = new CssParser(style);
            parser.Parse();
            return style;
        }
    }
}
