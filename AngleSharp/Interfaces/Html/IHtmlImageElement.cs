namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the image HTML element.
    /// </summary>
    [DomName("HTMLImageElement")]
    public interface IHtmlImageElement : IHtmlElement
    {
        [DomName("alt")]
        String alt { get; set; }

        [DomName("src")]
        String src { get; set; }

        [DomName("crossOrigin")]
        String crossOrigin { get; set; }

        [DomName("useMap")]
        String useMap { get; set; }

        [DomName("isMap")]
        Boolean isMap { get; set; }

        [DomName("width")]
        Int32 width { get; set; }

        [DomName("height")]
        Int32 height { get; set; }

        [DomName("naturalWidth")]
        Int32 naturalWidth { get; }

        [DomName("naturalHeight")]
        Int32 naturalHeight { get; }

        [DomName("complete")]
        Boolean complete { get; }
    }
}
