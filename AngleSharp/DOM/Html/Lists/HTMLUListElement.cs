namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// The DOM Object representing the unordered list.
    /// </summary>
    sealed class HTMLUListElement : HTMLElement, IListScopeElement, IHtmlUnorderedListElement
    {
        #region ctor

        internal HTMLUListElement()
            : base(Tags.Ul, NodeFlags.Special)
        {
        }

        #endregion
    }
}
