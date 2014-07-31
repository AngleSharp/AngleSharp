namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// The DOM Object representing the unordered list.
    /// </summary>
    sealed class HTMLUListElement : HTMLElement, IHtmlUnorderedListElement
    {
        #region ctor

        internal HTMLUListElement()
            : base(Tags.Ul, NodeFlags.Special | NodeFlags.HtmlListScoped)
        {
        }

        #endregion
    }
}
