namespace AngleSharp.Dom.Html
{
    using System;
    using AngleSharp.Html;

    /// <summary>
    /// The address HTML element.
    /// </summary>
    sealed class HtmlAddressElement : HtmlElement
    {
        public HtmlAddressElement(Document owner, String prefix = null)
            : base(owner, Tags.Address, prefix, NodeFlags.Special)
        {
        }
    }
}
