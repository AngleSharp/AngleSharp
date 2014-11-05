namespace AngleSharp.DOM.Svg
{
    using AngleSharp.Html;

    /// <summary>
    /// Represents the foreign object element of the SVG DOM.
    /// </summary>
    sealed class SVGForeignObjectElement : SVGElement
    {
        internal SVGForeignObjectElement()
            : base(Tags.ForeignObject, NodeFlags.HtmlTip | NodeFlags.Special | NodeFlags.Scoped)
        {
        }
    }
}
