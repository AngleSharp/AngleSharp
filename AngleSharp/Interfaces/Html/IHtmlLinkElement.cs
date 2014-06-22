namespace AngleSharp.DOM.Html
{
    using System;

    [DomName("HTMLLinkElement")]
    public interface IHtmlLinkElement : IHtmlElement
    {
        [DomName("disabled")]
        Boolean Disabled { get; set; }

        [DomName("href")]
        String Href { get; set; }

        [DomName("rel")]
        String Rel { get; set; }

        [DomName("relList")]
        ITokenList RelList { get; }

        [DomName("media")]
        String Media { get; set; }

        [DomName("hreflang")]
        String Hreflang { get; set; }

        [DomName("type")]
        String Type { get; set; }

        [DomName("sizes")]
        ISettableTokenList Sizes { get; }
    }
}
