namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// The DOM Object representing the unordered list.
    /// </summary>
    sealed class HtmlUnorderedListElement : HtmlElement, IHtmlUnorderedListElement
    {
        #region ctor

        public HtmlUnorderedListElement(Document owner)
            : base(owner, Tags.Ul, NodeFlags.Special | NodeFlags.HtmlListScoped)
        {
        }

        #endregion
    }
}
