namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// The rb HTML element.
    /// </summary>
    sealed class HtmlRbElement : HtmlElement
    {
        public HtmlRbElement(Document owner, String prefix = null)
            : base(owner, TagNames.Rb, prefix, NodeFlags.ImplicitelyClosed | NodeFlags.ImpliedEnd)
        {
        }
    }
}
