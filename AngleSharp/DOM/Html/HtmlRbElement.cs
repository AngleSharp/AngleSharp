namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// The rb HTML element.
    /// </summary>
    sealed class HtmlRbElement : HtmlElement
    {
        public HtmlRbElement(Document owner)
            : base(owner, Tags.Rb, NodeFlags.ImplicitelyClosed | NodeFlags.ImpliedEnd)
        {
        }
    }
}
