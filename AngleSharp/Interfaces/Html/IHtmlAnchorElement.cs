namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the a HTML element.
    /// </summary>
    [DomName("HTMLAnchorElement")]
    public interface IHtmlAnchorElement : IHtmlElement, IUrlUtilities
    {
        [DomName("target")]
        String Target { get; set; }

        [DomName("download")]
        String Download { get; set; }

        [DomName("ping")]
        ISettableTokenList Ping { get; }

        [DomName("rel")]
        String Rel { get; set; }

        [DomName("relList")]
        ITokenList RelList { get; }

        [DomName("hreflang")]
        String HrefLang { get; set; }

        [DomName("type")]
        String Type { get; }

        [DomName("text")]
        String Text { get; }
    }
}
