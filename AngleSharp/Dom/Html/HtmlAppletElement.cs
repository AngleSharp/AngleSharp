namespace AngleSharp.Dom.Html
{
    using AngleSharp.Attributes;
    using AngleSharp.Html;

    /// <summary>
    /// Represents the HTML applet element.
    /// </summary>
    [DomHistorical]
    sealed class HtmlAppletElement : HtmlElement
    {
        public HtmlAppletElement(Document owner)
            : base(owner, Tags.Applet, NodeFlags.Special | NodeFlags.Scoped)
        {
        }
    }
}
