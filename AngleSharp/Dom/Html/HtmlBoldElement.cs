namespace AngleSharp.Dom.Html
{
    using System;
    using AngleSharp.Html;

    /// <summary>
    /// The bold HTML element.
    /// </summary>
    sealed class HtmlBoldElement : HtmlElement
    {
        public HtmlBoldElement(Document owner, String prefix = null)
            : base(owner, Tags.B, prefix, NodeFlags.HtmlFormatting)
        {
        }
    }
}
