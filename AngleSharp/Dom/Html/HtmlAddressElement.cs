namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// The address HTML element.
    /// </summary>
    sealed class HtmlAddressElement : HtmlElement
    {
        public HtmlAddressElement(Document owner)
            : base(owner, Tags.Address, NodeFlags.Special)
        {
        }
    }
}
