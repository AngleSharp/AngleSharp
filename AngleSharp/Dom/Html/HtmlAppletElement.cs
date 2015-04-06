namespace AngleSharp.Dom.Html
{
    using System;
    using AngleSharp.Attributes;
    using AngleSharp.Html;

    /// <summary>
    /// Represents the HTML applet element.
    /// </summary>
    [DomHistorical]
    sealed class HtmlAppletElement : HtmlElement
    {
        public HtmlAppletElement(Document owner, String prefix = null)
            : base(owner, Tags.Applet, prefix, NodeFlags.Special | NodeFlags.Scoped)
        {
        }
    }
}
