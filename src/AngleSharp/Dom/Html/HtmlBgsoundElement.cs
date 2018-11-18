namespace AngleSharp.Dom.Html
{
    using System;
    using AngleSharp.Attributes;
    using AngleSharp.Html;

    /// <summary>
    /// Represents the HTML bgsound element.
    /// </summary>
    [DomHistorical]
    sealed class HtmlBgsoundElement : HtmlElement, IHtmlBgsoundElement
    {
        public HtmlBgsoundElement(Document owner, String prefix = null)
            : base(owner, TagNames.Bgsound, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
        {
        }
    }
}
