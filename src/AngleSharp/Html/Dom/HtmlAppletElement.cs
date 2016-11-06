namespace AngleSharp.Html.Dom
{
    using AngleSharp.Attributes;
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// Represents the HTML applet element.
    /// </summary>
    [DomHistorical]
    sealed class HtmlAppletElement : HtmlElement
    {
        public HtmlAppletElement(Document owner, String prefix = null)
            : base(owner, TagNames.Applet, prefix, NodeFlags.Special | NodeFlags.Scoped)
        {
        }
    }
}
