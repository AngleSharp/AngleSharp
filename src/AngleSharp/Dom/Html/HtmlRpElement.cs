namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// The rp HTML element.
    /// </summary>
    sealed class HtmlRpElement : HtmlElement
    {
        public HtmlRpElement(Document owner, String prefix = null)
            : base(owner, TagNames.Rp, prefix, NodeFlags.ImplicitelyClosed | NodeFlags.ImpliedEnd)
        {
        }
    }
}
