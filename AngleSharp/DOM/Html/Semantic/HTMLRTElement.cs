namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// The rt element.
    /// </summary>
    sealed class HTMLRTElement : HTMLElement
    {
        public HTMLRTElement(Document owner)
            : base(owner, Tags.Rt, NodeFlags.ImplicitelyClosed | NodeFlags.ImpliedEnd)
        {
        }
    }
}
