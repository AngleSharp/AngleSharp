namespace AngleSharp.Html.Dom
{
    using AngleSharp.Attributes;
    using AngleSharp.Dom;

    /// <summary>
    /// Represents the datalist HTML element.
    /// </summary>
    [DomName("HTMLDataListElement")]
    public interface IHtmlDataListElement : IHtmlElement
    {
        /// <summary>
        /// Gets the associated options.
        /// </summary>
        [DomName("options")]
        IHtmlCollection<IHtmlOptionElement> Options { get; }
    }
}
