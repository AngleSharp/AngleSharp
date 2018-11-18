namespace AngleSharp.Dom.Html
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents the a HTML element.
    /// </summary>
    [DomName("HTMLAnchorElement")]
    public interface IHtmlAnchorElement : IHtmlUrlBaseElement
    {
        /// <summary>
        /// Gets the text of the anchor tag (same as TextContent).
        /// </summary>
        [DomName("text")]
        String Text { get; }
    }
}
