namespace AngleSharp.DOM.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// The DOM Object representing the unordered list.
    /// </summary>
    sealed class HTMLUListElement : HTMLElement, IHtmlUnorderedListElement
    {
        #region ctor

        public HTMLUListElement(Document owner)
            : base(Tags.Ul, NodeFlags.Special | NodeFlags.HtmlListScoped)
        {
            Owner = owner;
        }

        #endregion
    }
}
