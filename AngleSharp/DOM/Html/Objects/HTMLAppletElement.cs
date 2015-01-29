namespace AngleSharp.Dom.Html
{
    using AngleSharp.Attributes;
    using AngleSharp.Html;

    /// <summary>
    /// Represents the HTML applet element.
    /// </summary>
    [DomHistorical]
    sealed class HTMLAppletElement : HtmlElement
    {
        public HTMLAppletElement(Document owner)
            : base(owner, Tags.Applet, NodeFlags.Special | NodeFlags.Scoped)
        {
        }
    }
}
