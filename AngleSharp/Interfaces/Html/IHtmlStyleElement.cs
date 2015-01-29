namespace AngleSharp.Dom.Html
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents a style HTML element.
    /// </summary>
    [DomName("HTMLStyleElement")]
    public interface IHtmlStyleElement : IHtmlElement, ILinkStyle
    {
        /// <summary>
        /// Gets or sets if the style is enabled or disabled.
        /// </summary>
        [DomName("disabled")]
        Boolean IsDisabled { get; set; }

        /// <summary>
        /// Gets or sets the use with one or more target media.
        /// </summary>
        [DomName("media")]
        String Media { get; set; }

        /// <summary>
        /// Gets or sets the content type of the style sheet language.
        /// </summary>
        [DomName("type")]
        String Type { get; set; }

        /// <summary>
        /// Gets or sets if the style is scoped.
        /// </summary>
        [DomName("scoped")]
        Boolean IsScoped { get; set; }
    }
}
