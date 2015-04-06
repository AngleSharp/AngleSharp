namespace AngleSharp.Dom.Html
{
    using System;
    using AngleSharp.Attributes;
    using AngleSharp.Html;

    /// <summary>
    /// Represents the HTML bgsound element.
    /// </summary>
    [DomHistorical]
    sealed class HtmlBgsoundElement : HtmlElement
    {
        public HtmlBgsoundElement(Document owner, String prefix = null)
            : base(owner, Tags.Bgsound, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
        {
        }
    }
}
