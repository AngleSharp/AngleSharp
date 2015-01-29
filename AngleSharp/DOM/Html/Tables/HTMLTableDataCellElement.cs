namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// Represents the object for HTML td elements.
    /// </summary>
    sealed class HTMLTableDataCellElement : HTMLTableCellElement, IHtmlTableCellElement
    {
        #region ctor

        public HTMLTableDataCellElement(Document owner)
            : base(owner, Tags.Td)
        {
        }

        #endregion
    }
}
