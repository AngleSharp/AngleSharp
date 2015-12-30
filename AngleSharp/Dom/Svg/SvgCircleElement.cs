namespace AngleSharp.Dom.Svg
{
    using System;
    using AngleSharp.Html;

    /// <summary>
    /// Represents the circle element of the SVG DOM.
    /// </summary>
    sealed class SvgCircleElement : SvgElement
    {
        public SvgCircleElement(Document owner, String prefix = null)
            : base(owner, TagNames.Circle, prefix)
        {
        }
    }
}
