namespace AngleSharp.DOM.Html
{
    using AngleSharp.Html;

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
