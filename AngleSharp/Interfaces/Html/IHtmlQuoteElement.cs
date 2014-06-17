namespace AngleSharp.DOM.Html
{
    using System;

    [DomName("HTMLQuoteElement")]
    interface IHtmlQuoteElement : IHtmlElement
    {
        [DomName("cite")]
        String Citation { get; set; }
    }
}
