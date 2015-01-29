namespace AngleSharp.Dom.Html
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents the li HTML element.
    /// </summary>
    [DomName("HTMLLIElement")]
    public interface IHtmlListItemElement : IHtmlElement
    {
        /// <summary>
        /// Gets or sets the value in an ordered list.
        /// </summary>
        [DomName("value")]
        Int32? Value { get; set; }
    }
}
