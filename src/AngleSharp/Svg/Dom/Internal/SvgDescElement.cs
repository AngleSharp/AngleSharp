namespace AngleSharp.Svg.Dom
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// Represents the desc element of the SVG DOM.
    /// </summary>
    sealed class SvgDescElement : SvgElement, ISvgDescriptionElement
    {
        public SvgDescElement(Document owner, String prefix = null)
            : base(owner, TagNames.Desc, prefix, NodeFlags.HtmlTip | NodeFlags.Special | NodeFlags.Scoped)
        {
        }
    }
}
