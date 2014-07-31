namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// The rp HTML element.
    /// </summary>
    sealed class HTMLRPElement : HTMLElement
    {
        internal HTMLRPElement()
            : base(Tags.Rp, NodeFlags.ImplicitelyClosed | NodeFlags.ImpliedEnd)
        {
        }
    }
}
