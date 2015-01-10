namespace AngleSharp.DOM.Svg
{
    using AngleSharp.Html;

    /// <summary>
    /// Represents the circle element of the SVG DOM.
    /// </summary>
    sealed class SvgCircleElement : SvgElement
    {
        public SvgCircleElement(Document owner)
            : base(owner, Tags.Circle)
        {
        }
    }
}
