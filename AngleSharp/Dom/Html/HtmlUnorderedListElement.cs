namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// The DOM Object representing the unordered list.
    /// </summary>
    sealed class HtmlUnorderedListElement : HtmlElement, IHtmlUnorderedListElement
    {
        public HtmlUnorderedListElement(Document owner, String prefix = null)
            : base(owner, TagNames.Ul, prefix, NodeFlags.Special | NodeFlags.HtmlListScoped)
        {
        }
    }
}
