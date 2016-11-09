namespace AngleSharp.Html.Dom
{
    using AngleSharp.Attributes;
    using AngleSharp.Dom;

    /// <summary>
    /// Represents the template HTML element.
    /// </summary>
    [DomName("HTMLTemplateElement")]
    public interface IHtmlTemplateElement : IHtmlElement
    {
        /// <summary>
        /// Gets the template's content for cloning.
        /// </summary>
        [DomName("content")]
        IDocumentFragment Content { get; }
    }
}
