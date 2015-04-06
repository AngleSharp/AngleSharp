namespace AngleSharp.Dom.Svg
{
    using System;
    using AngleSharp.Html;

    /// <summary>
    /// Represents the foreign object element of the SVG DOM.
    /// </summary>
    sealed class SvgForeignObjectElement : SvgElement
    {
        public SvgForeignObjectElement(Document owner, String prefix = null)
            : base(owner, Tags.ForeignObject, prefix, NodeFlags.HtmlTip | NodeFlags.Special | NodeFlags.Scoped)
        {
        }
    }
}
