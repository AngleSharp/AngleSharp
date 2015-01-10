namespace AngleSharp.DOM.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// The rp HTML element.
    /// </summary>
    sealed class HTMLRPElement : HTMLElement
    {
        public HTMLRPElement(Document owner)
            : base(Tags.Rp, NodeFlags.ImplicitelyClosed | NodeFlags.ImpliedEnd)
        {
            Owner = owner;
        }
    }
}
