namespace AngleSharp.Html.Dom
{
    using AngleSharp.Attributes;
    using AngleSharp.Dom;
    using System;

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
