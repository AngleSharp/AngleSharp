namespace AngleSharp.Dom.Html
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents the menu HTML element.
    /// </summary>
    [DomName("HTMLMenuElement")]
    public interface IHtmlMenuElement : IHtmlElement
    {
        /// <summary>
        /// Gets or sets the text label of the menu element.
        /// </summary>
        [DomName("label")]
        String Label { get; set; }

        /// <summary>
        /// Gets or sets the type of the menu element.
        /// </summary>
        [DomName("type")]
        String Type { get; set; }
    }
}
