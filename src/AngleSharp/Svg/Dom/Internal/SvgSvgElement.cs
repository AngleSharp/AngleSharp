namespace AngleSharp.Svg.Dom
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// Represents the svg element of the SVG DOM.
    /// </summary>
    sealed class SvgSvgElement : SvgElement, ISvgSvgElement
    {
        public SvgSvgElement(Document owner, String prefix = null)
            : base(owner, TagNames.Svg, prefix)
        {
        }
    }
}
