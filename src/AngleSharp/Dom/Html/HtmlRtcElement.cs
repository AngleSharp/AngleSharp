namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// The rtc HTML element.
    /// </summary>
    sealed class HtmlRtcElement : HtmlElement, IHtmlRtcElement
    {
        public HtmlRtcElement(Document owner, String prefix = null)
            : base(owner, TagNames.Rtc, prefix, NodeFlags.ImplicitelyClosed | NodeFlags.ImpliedEnd)
        {
        }
    }
}
