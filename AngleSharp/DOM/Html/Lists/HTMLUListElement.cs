namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// The DOM Object representing the unordered list.
    /// </summary>
    sealed class HTMLUListElement : HTMLElement, IHtmlUnorderedListElement
    {
        #region ctor

        public HTMLUListElement(Document owner)
            : base(owner, Tags.Ul, NodeFlags.Special | NodeFlags.HtmlListScoped)
        {
        }

        #endregion
    }
}
