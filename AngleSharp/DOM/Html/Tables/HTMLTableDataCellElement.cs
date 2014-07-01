namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the object for HTML td elements.
    /// </summary>
    sealed class HTMLTableDataCellElement : HTMLTableCellElement, IHtmlTableCellElement
    {
        #region ctor

        internal HTMLTableDataCellElement()
        {
            _name = Tags.Td;
        }

        #endregion
    }
}
