namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the iframe HTML element.
    /// </summary>
    [DomName("HTMLIFrameElement")]
    interface IHtmlInlineFrameElement : IHtmlElement
    {
        [DomName("src")]
        String Src { get; set; }

        [DomName("srcdoc")]
        String SrcDoc { get; set; }

        [DomName("name")]
        String Name { get; set; }

        [DomName("sandbox")]
        ISettableTokenList Sandbox { get; }

        [DomName("seamless")]
        Boolean Seamless { get; set; }

        [DomName("allowFullscreen")]
        Boolean AllowFullscreen { get; set; }

        [DomName("width")]
        String Width { get; set; }

        [DomName("height")]
        String Height { get; set; }

        [DomName("contentDocument")]
        IDocument ContentDocument { get; }

        [DomName("contentWindow")]
        IWindowProxy ContentWindow { get; }
    }
}
