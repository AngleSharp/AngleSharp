namespace AngleSharp.DOM.Svg
{
    using AngleSharp.Html;

    /// <summary>
    /// Represents the desc element of the SVG DOM.
    /// </summary>
    sealed class SVGDescElement : SVGElement
    {
        internal SVGDescElement()
            : base(Tags.Desc, NodeFlags.HtmlTip | NodeFlags.Special | NodeFlags.Scoped)
        {
        }
    }
}
