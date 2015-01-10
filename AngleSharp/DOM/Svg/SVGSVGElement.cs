namespace AngleSharp.DOM.Svg
{
    using AngleSharp.Html;

    /// <summary>
    /// Represents the svg element of the SVG DOM.
    /// </summary>
    sealed class SvgSvgElement : SvgElement
    {
        public SvgSvgElement(Document owner)
            : base(owner, Tags.Svg)
        {
        }
    }
}
