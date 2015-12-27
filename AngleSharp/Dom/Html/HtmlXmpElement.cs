namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// The xmp HTML element.
    /// </summary>
    sealed class HtmlXmpElement : HtmlElement
    {
        public HtmlXmpElement(Document owner, String prefix = null)
            : base(owner, TagNames.Xmp, prefix, NodeFlags.Special | NodeFlags.LiteralText)
        {
        }
    }
}
