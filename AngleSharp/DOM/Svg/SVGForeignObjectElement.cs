namespace AngleSharp.DOM.Svg
{
    using AngleSharp.Html;

    /// <summary>
    /// Represents the foreign object element of the SVG DOM.
    /// </summary>
    sealed class SvgForeignObjectElement : SvgElement
    {
        public SvgForeignObjectElement(Document owner)
            : base(owner, Tags.ForeignObject, NodeFlags.HtmlTip | NodeFlags.Special | NodeFlags.Scoped)
        {
        }
    }
}
