namespace AngleSharp.Services
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Html;
    using System;

    interface IHtmlElementFactory
    {
        HtmlElement Create(Document document, String localName, String prefix = null);
    }
}
