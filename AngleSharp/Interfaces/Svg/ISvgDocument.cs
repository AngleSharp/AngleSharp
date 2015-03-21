namespace AngleSharp.Dom.Svg
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Serves as an entry point to the content of an SVG document.
    /// </summary>
    [DomName("SVGDocument ")]
    public interface ISvgDocument : IDocument
    {
        /// <summary>
        /// Gets the root svg element in the document hierachy.
        /// </summary>
        [DomName("rootElement")]
        ISvgSvgElement RootElement { get; }

        /// <summary>
        /// Loads the given url as a SvgDocument. Clears the current document
        /// structure and re-fills it with the contents from the provided url.
        /// </summary>
        /// <param name="url">The url to get the new DOM from.</param>
        [DomName("load")]
        void LoadSvg(String url);
    }
}
