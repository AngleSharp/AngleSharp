namespace AngleSharp.Dom.Html
{
    using System;
    using AngleSharp.Attributes;

    /// <summary>
    /// Represents the HTML bgsound element.
    /// </summary>
    [DomHistorical]
    sealed class HtmlBgsoundElement : HtmlElement
    {
        public HtmlBgsoundElement(Document owner, String prefix = null)
            : base(owner, TagNames.Bgsound, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
        {
        }
    }
}
