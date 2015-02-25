namespace AngleSharp.Dom.Svg
{
    using AngleSharp.Html;

    /// <summary>
    /// Represents the desc element of the SVG DOM.
    /// </summary>
    sealed class SvgDescElement : SvgElement
    {
        public SvgDescElement(Document owner)
            : base(owner, Tags.Desc, NodeFlags.HtmlTip | NodeFlags.Special | NodeFlags.Scoped)
        {
        }
    }
}
