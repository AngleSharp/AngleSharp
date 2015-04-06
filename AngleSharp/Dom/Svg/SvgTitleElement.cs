namespace AngleSharp.Dom.Svg
{
    using System;
    using AngleSharp.Html;

    /// <summary>
    /// Represents the title element of the SVG DOM.
    /// </summary>
    sealed class SvgTitleElement : SvgElement, ISvgTitleElement
    {
        public SvgTitleElement(Document owner, String prefix = null)
            : base(owner, Tags.Title, prefix, NodeFlags.HtmlTip | NodeFlags.Special | NodeFlags.Scoped)
        {
        }
    }
}
