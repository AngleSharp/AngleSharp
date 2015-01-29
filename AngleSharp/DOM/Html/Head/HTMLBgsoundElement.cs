namespace AngleSharp.Dom.Html
{
    using AngleSharp.Attributes;
    using AngleSharp.Html;

    /// <summary>
    /// Represents the HTML bgsound element.
    /// </summary>
    [DomHistorical]
    sealed class HTMLBgsoundElement : HtmlElement
    {
        public HTMLBgsoundElement(Document owner)
            : base(owner, Tags.Bgsound, NodeFlags.Special | NodeFlags.SelfClosing)
        {
        }
    }
}
