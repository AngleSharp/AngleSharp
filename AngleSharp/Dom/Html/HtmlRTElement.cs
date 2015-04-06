namespace AngleSharp.Dom.Html
{
    using System;
    using AngleSharp.Html;

    /// <summary>
    /// The rt element.
    /// </summary>
    sealed class HtmlRtElement : HtmlElement
    {
        public HtmlRtElement(Document owner, String prefix = null)
            : base(owner, Tags.Rt, prefix, NodeFlags.ImplicitelyClosed | NodeFlags.ImpliedEnd)
        {
        }
    }
}
