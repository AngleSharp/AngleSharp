namespace AngleSharp.DOM.Svg
{
    using AngleSharp.Html;

    /// <summary>
    /// Represents the desc element of the SVG DOM.
    /// </summary>
    sealed class SvgDescElement : SvgElement
    {
        internal SvgDescElement()
            : base(Tags.Desc, NodeFlags.HtmlTip | NodeFlags.Special | NodeFlags.Scoped)
        {
        }
    }
}
