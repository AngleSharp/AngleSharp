namespace AngleSharp.DOM.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// Represents the object for HTML td elements.
    /// </summary>
    sealed class HTMLTableDataCellElement : HTMLTableCellElement, IHtmlTableCellElement
    {
        #region ctor

        internal HTMLTableDataCellElement()
            : base(Tags.Td)
        {
        }

        #endregion
    }
}
