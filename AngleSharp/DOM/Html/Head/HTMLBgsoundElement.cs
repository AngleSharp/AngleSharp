namespace AngleSharp.DOM.Html
{
    using AngleSharp.Attributes;
    using AngleSharp.Html;

    /// <summary>
    /// Represents the HTML bgsound element.
    /// </summary>
    [DomHistorical]
    sealed class HTMLBgsoundElement : HTMLElement
    {
        public HTMLBgsoundElement(Document owner)
            : base(Tags.Bgsound, NodeFlags.Special | NodeFlags.SelfClosing)
        {
            Owner = owner;
        }
    }
}
