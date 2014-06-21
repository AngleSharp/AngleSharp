namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the title HTML element.
    /// </summary>
    [DomName("HTMLTitleElement")]
    interface IHtmlTitleElement : IHtmlElement
    {
        /// <summary>
        /// Gets or sets the text of the title.
        /// </summary>
        [DomName("text")]
        String Text { get; set; }
    }
}
