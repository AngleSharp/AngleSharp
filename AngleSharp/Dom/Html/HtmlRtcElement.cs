namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// The rtc HTML element.
    /// </summary>
    sealed class HtmlRtcElement : HtmlElement
    {
        public HtmlRtcElement(Document owner)
            : base(owner, Tags.Rtc, NodeFlags.ImplicitelyClosed | NodeFlags.ImpliedEnd)
        {
        }
    }
}
