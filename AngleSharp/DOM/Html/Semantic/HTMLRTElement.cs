namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// The rt element.
    /// </summary>
    sealed class HTMLRTElement : HTMLElement
    {
        internal HTMLRTElement()
            : base(Tags.Rt, NodeFlags.ImplicitelyClosed | NodeFlags.ImpliedEnd)
        {
        }
    }
}
