namespace AngleSharp.DOM.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// The address HTML element.
    /// </summary>
    sealed class HTMLAddressElement : HTMLElement
    {
        public HTMLAddressElement(Document owner)
            : base(owner, Tags.Address, NodeFlags.Special)
        {
        }
    }
}
