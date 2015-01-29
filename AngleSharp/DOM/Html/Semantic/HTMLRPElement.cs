namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// The rp HTML element.
    /// </summary>
    sealed class HTMLRPElement : HtmlElement
    {
        public HTMLRPElement(Document owner)
            : base(owner, Tags.Rp, NodeFlags.ImplicitelyClosed | NodeFlags.ImpliedEnd)
        {
        }
    }
}
