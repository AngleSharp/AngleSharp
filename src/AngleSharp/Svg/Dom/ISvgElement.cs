namespace AngleSharp.Svg.Dom
{
    using AngleSharp.Attributes;
    using AngleSharp.Dom;

    /// <summary>
    /// The SVGElement interface represents any SVG element. Some elements directly
    /// implement this interface, other implement it via an interface that inherit it.
    /// </summary>
    [DomName("SVGElement")]
    public interface ISvgElement : IElement
    {
    }
}
