namespace AngleSharp.Dom.Html
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents the data HTML element.
    /// </summary>
    [DomName("HTMLDataElement")]
    public interface IHtmlDataElement : IHtmlElement
    {
        /// <summary>
        /// Gets or sets the machine readable value.
        /// </summary>
        [DomName("value")]
        String Value { get; set; }
    }
}
