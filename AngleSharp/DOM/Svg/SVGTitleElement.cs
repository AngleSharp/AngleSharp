namespace AngleSharp.DOM.Svg
{
    using AngleSharp.Html;

    /// <summary>
    /// Represents the title element of the SVG DOM.
    /// </summary>
    sealed class SvgTitleElement : SvgElement, ISvgTitleElement
    {
        internal SvgTitleElement()
            : base(Tags.Title, NodeFlags.HtmlTip | NodeFlags.Special | NodeFlags.Scoped)
        {
        }
    }
}
