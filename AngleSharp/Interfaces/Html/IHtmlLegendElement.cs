namespace AngleSharp.Dom.Html
{
    using AngleSharp.Attributes;

    /// <summary>
    /// Represents the legend HTML element.
    /// </summary>
    [DomName("HTMLLegendElement")]
    public interface IHtmlLegendElement : IHtmlElement
    {
        /// <summary>
        /// Gets the associated form element, if any.
        /// </summary>
        [DomName("form")]
        IHtmlFormElement Form { get; }
    }
}
