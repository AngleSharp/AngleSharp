namespace AngleSharp.Dom.Html
{
    using AngleSharp.Attributes;
    using AngleSharp.Html;

    /// <summary>
    /// Represents the HTML bgsound element.
    /// </summary>
    [DomHistorical]
    sealed class HtmlBgsoundElement : HtmlElement
    {
        public HtmlBgsoundElement(Document owner)
            : base(owner, Tags.Bgsound, NodeFlags.Special | NodeFlags.SelfClosing)
        {
        }
    }
}
