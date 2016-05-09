namespace AngleSharp.Dom.Svg
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the foreign object element of the SVG DOM.
    /// </summary>
    sealed class SvgForeignObjectElement : SvgElement, ISvgForeignObjectElement
    {
        public SvgForeignObjectElement(Document owner, String prefix = null)
            : base(owner, TagNames.ForeignObject, prefix, NodeFlags.HtmlTip | NodeFlags.Special | NodeFlags.Scoped)
        {
        }
    }
}
