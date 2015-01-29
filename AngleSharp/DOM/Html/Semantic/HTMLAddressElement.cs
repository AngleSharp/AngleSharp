namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// The address HTML element.
    /// </summary>
    sealed class HTMLAddressElement : HtmlElement
    {
        public HTMLAddressElement(Document owner)
            : base(owner, Tags.Address, NodeFlags.Special)
        {
        }
    }
}
