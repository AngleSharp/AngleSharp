namespace AngleSharp.DOM.Html
{
    using System;

    [DomName("HTMLScriptElement")]
    interface IHtmlScriptElement : IHtmlElement
    {
        [DomName("src")]
        String Source { get; set; }

        [DomName("async")]
        Boolean IsAsync { get; set; }

        [DomName("defer")]
        Boolean IsDeferred { get; set; }

        [DomName("type")]
        String Type { get; set; }

        [DomName("charset")]
        String CharacterSet { get; set; }

        [DomName("text")]
        String Text { get; set; }
    }
}
