namespace AngleSharp.Dom.Svg
{
    using AngleSharp.Attributes;

    /// <summary>
    /// Serves as an entry point to the content of an SVG document.
    /// </summary>
    [DomName("SVGDocument")]
    public interface ISvgDocument : IDocument
    {
        /// <summary>
        /// Gets the root svg element in the document hierachy.
        /// </summary>
        [DomName("rootElement")]
        ISvgSvgElement RootElement { get; }
    }
}
