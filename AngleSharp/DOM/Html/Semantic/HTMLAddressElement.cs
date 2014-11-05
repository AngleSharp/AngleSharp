namespace AngleSharp.DOM.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// The address HTML element.
    /// </summary>
    sealed class HTMLAddressElement : HTMLElement
    {
        internal HTMLAddressElement()
            : base(Tags.Address, NodeFlags.Special)
        {
        }
    }
}
