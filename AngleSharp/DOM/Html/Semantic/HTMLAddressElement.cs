namespace AngleSharp.DOM.Html
{
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
