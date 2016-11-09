namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// The rtc HTML element.
    /// </summary>
    sealed class HtmlRtcElement : HtmlElement
    {
        public HtmlRtcElement(Document owner, String prefix = null)
            : base(owner, TagNames.Rtc, prefix, NodeFlags.ImplicitelyClosed | NodeFlags.ImpliedEnd)
        {
        }
    }
}
