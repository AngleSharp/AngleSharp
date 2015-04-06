namespace AngleSharp.Dom.Html
{
    using System;

    /// <summary>
    /// The class for an unknown HTML element.
    /// </summary>
    sealed class HtmlUnknownElement : HtmlElement, IHtmlUnknownElement
    {
        public HtmlUnknownElement(Document owner, String localName, String prefix)
            : base(owner, localName, prefix)
        {
        }
    }
}
