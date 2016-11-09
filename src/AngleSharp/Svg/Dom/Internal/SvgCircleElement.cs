namespace AngleSharp.Svg.Dom
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// Represents the circle element of the SVG DOM.
    /// </summary>
    sealed class SvgCircleElement : SvgElement, ISvgCircleElement
    {
        public SvgCircleElement(Document owner, String prefix = null)
            : base(owner, TagNames.Circle, prefix)
        {
        }
    }
}
