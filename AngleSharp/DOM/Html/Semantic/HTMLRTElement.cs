namespace AngleSharp.DOM.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// The rt element.
    /// </summary>
    sealed class HTMLRTElement : HTMLElement
    {
        public HTMLRTElement(Document owner)
            : base(Tags.Rt, NodeFlags.ImplicitelyClosed | NodeFlags.ImpliedEnd)
        {
            Owner = owner;
        }
    }
}
