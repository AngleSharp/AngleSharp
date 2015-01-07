namespace AngleSharp.DOM.Svg
{
    using AngleSharp.Html;

    /// <summary>
    /// Represents the foreign object element of the SVG DOM.
    /// </summary>
    sealed class SvgForeignObjectElement : SvgElement
    {
        internal SvgForeignObjectElement()
            : base(Tags.ForeignObject, NodeFlags.HtmlTip | NodeFlags.Special | NodeFlags.Scoped)
        {
        }
    }
}
