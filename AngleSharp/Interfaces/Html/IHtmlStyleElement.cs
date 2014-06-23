namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents a style HTML element.
    /// </summary>
    [DomName("HTMLStyleElement")]
    public interface IHtmlStyleElement : IHtmlElement
    {
        [DomName("disabled")]
        Boolean Disabled { get; set; }

        [DomName("media")]
        String Media { get; set; }

        [DomName("type")]
        String Type { get; set; }

        [DomName("scoped")]
        Boolean Scoped { get; set; }
    }
}
