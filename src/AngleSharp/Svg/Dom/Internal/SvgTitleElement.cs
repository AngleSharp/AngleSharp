namespace AngleSharp.Svg.Dom
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// Represents the title element of the SVG DOM.
    /// </summary>
    sealed class SvgTitleElement : SvgElement, ISvgTitleElement
    {
        public SvgTitleElement(Document owner, String prefix = null)
            : base(owner, TagNames.Title, prefix, NodeFlags.HtmlTip | NodeFlags.Special | NodeFlags.Scoped)
        {
        }
    }
}
