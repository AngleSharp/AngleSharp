namespace AngleSharp.Dom.Svg
{
    using System;
    using AngleSharp.Html;

    /// <summary>
    /// Represents the svg element of the SVG DOM.
    /// </summary>
    sealed class SvgSvgElement : SvgElement
    {
        public SvgSvgElement(Document owner, String prefix = null)
            : base(owner, prefix, Tags.Svg)
        {
        }
    }
}
