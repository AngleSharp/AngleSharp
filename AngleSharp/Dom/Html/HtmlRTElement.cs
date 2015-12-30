namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// The rt element.
    /// </summary>
    sealed class HtmlRtElement : HtmlElement
    {
        public HtmlRtElement(Document owner, String prefix = null)
            : base(owner, TagNames.Rt, prefix, NodeFlags.ImplicitelyClosed | NodeFlags.ImpliedEnd)
        {
        }
    }
}
