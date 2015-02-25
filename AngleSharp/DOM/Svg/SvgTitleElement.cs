namespace AngleSharp.Dom.Svg
{
    using AngleSharp.Html;

    /// <summary>
    /// Represents the title element of the SVG DOM.
    /// </summary>
    sealed class SvgTitleElement : SvgElement, ISvgTitleElement
    {
        public SvgTitleElement(Document owner)
            : base(owner, Tags.Title, NodeFlags.HtmlTip | NodeFlags.Special | NodeFlags.Scoped)
        {
        }
    }
}
