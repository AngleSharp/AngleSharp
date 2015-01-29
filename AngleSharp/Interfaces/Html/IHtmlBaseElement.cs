namespace AngleSharp.Dom.Html
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents the base HTML element.
    /// </summary>
    [DomName("HTMLBaseElement")]
    public interface IHtmlBaseElement : IHtmlElement
    {
        /// <summary>
        /// Gets or sets the hyperreference to the base URL.
        /// </summary>
        [DomName("href")]
        String Href { get; set; }

        /// <summary>
        /// Gets or sets the base target.
        /// </summary>
        [DomName("Target")]
        String Target { get; set; }
    }
}
