namespace AngleSharp.Dom.Html
{
    using System;
    using AngleSharp.Html;

    /// <summary>
    /// The DOM Object representing the unordered list.
    /// </summary>
    sealed class HtmlUnorderedListElement : HtmlElement, IHtmlUnorderedListElement
    {
        #region ctor

        public HtmlUnorderedListElement(Document owner, String prefix = null)
            : base(owner, Tags.Ul, prefix, NodeFlags.Special | NodeFlags.HtmlListScoped)
        {
        }

        #endregion
    }
}
