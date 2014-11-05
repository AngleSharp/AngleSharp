namespace AngleSharp.DOM.Html
{
    using AngleSharp.Html;

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
