namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// Represents the object for HTML th elements.
    /// </summary>
    sealed class HtmlTableHeaderCellElement : HtmlTableCellElement, IHtmlTableHeaderCellElement
    {
        #region ctor

        public HtmlTableHeaderCellElement(Document owner)
            : base(owner, Tags.Th)
        {
        }

        #endregion
    }
}
