namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// Represents the object for HTML td elements.
    /// </summary>
    sealed class HtmlTableDataCellElement : HtmlTableCellElement, IHtmlTableDataCellElement
    {
        #region ctor

        public HtmlTableDataCellElement(Document owner)
            : base(owner, Tags.Td)
        {
        }

        #endregion
    }
}
