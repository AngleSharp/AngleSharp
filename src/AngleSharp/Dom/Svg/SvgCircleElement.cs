namespace AngleSharp.Dom.Svg
{
    using AngleSharp.Html;
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
