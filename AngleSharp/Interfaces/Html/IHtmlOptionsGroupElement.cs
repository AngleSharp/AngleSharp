namespace AngleSharp.Dom.Html
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents the optgroup HTML element.
    /// </summary>
    [DomName("HTMLOptGroupElement")]
    public interface IHtmlOptionsGroupElement : IHtmlElement
    {
        /// <summary>
        /// Gets or sets if the optgroup is enabled or disabled.
        /// </summary>
        [DomName("disabled")]
        Boolean IsDisabled { get; set; }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        [DomName("label")]
        String Label { get; set; }
    }
}
