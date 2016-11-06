namespace AngleSharp.Html.Dom
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents the details HTML element.
    /// </summary>
    [DomName("HTMLDetailsElement")]
    public interface IHtmlDetailsElement : IHtmlElement
    {
        /// <summary>
        /// Gets or sets if the element is opened.
        /// </summary>
        [DomName("open")]
        Boolean IsOpen { get; set; }
    }
}
