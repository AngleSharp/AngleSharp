namespace AngleSharp.Dom.Html
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents the html HTML element.
    /// </summary>
    [DomName("HTMLHtmlElement")]
    public interface IHtmlHtmlElement : IHtmlElement
    {
        /// <summary>
        /// Gets or sets the value of the manifest attribute.
        /// </summary>
        [DomName("manifest")]
        String Manifest { get; set; }
    }
}
