namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the object for HTML th elements.
    /// </summary>
    sealed class HTMLTableHeaderCellElement : HTMLTableCellElement, IHtmlTableHeaderCellElement
    {
        #region ctor

        internal HTMLTableHeaderCellElement()
            : base(Tags.Th)
        {
        }

        #endregion
    }
}
