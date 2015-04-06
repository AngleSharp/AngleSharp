namespace AngleSharp.Dom.Html
{
    using System;
    using AngleSharp.Html;

    /// <summary>
    /// The rp HTML element.
    /// </summary>
    sealed class HtmlRpElement : HtmlElement
    {
        public HtmlRpElement(Document owner, String prefix = null)
            : base(owner, Tags.Rp, prefix, NodeFlags.ImplicitelyClosed | NodeFlags.ImpliedEnd)
        {
        }
    }
}
