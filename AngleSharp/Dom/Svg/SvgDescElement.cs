namespace AngleSharp.Dom.Svg
{
    using System;
    using AngleSharp.Html;

    /// <summary>
    /// Represents the desc element of the SVG DOM.
    /// </summary>
    sealed class SvgDescElement : SvgElement
    {
        public SvgDescElement(Document owner, String prefix = null)
            : base(owner, Tags.Desc, prefix, NodeFlags.HtmlTip | NodeFlags.Special | NodeFlags.Scoped)
        {
        }
    }
}
