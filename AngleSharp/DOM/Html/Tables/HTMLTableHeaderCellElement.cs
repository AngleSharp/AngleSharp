namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// Represents the object for HTML th elements.
    /// </summary>
    sealed class HTMLTableHeaderCellElement : HTMLTableCellElement, IHtmlTableHeaderCellElement
    {
        #region ctor

        public HTMLTableHeaderCellElement(Document owner)
            : base(owner, Tags.Th)
        {
        }

        #endregion
    }
}
