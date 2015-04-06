namespace AngleSharp.Dom.Html
{
    using System;
    using AngleSharp.Html;

    /// <summary>
    /// The rtc HTML element.
    /// </summary>
    sealed class HtmlRtcElement : HtmlElement
    {
        public HtmlRtcElement(Document owner, String prefix = null)
            : base(owner, Tags.Rtc, prefix, NodeFlags.ImplicitelyClosed | NodeFlags.ImpliedEnd)
        {
        }
    }
}
