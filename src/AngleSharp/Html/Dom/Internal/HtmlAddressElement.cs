namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// The address HTML element.
    /// </summary>
    sealed class HtmlAddressElement : HtmlElement
    {
        public HtmlAddressElement(Document owner, String prefix = null)
            : base(owner, TagNames.Address, prefix, NodeFlags.Special)
        {
        }
    }
}
