namespace AngleSharp.Dom.Html
{
    using System;
    using AngleSharp.Html;

    /// <summary>
    /// Represents the object for HTML td elements.
    /// </summary>
    sealed class HtmlTableDataCellElement : HtmlTableCellElement, IHtmlTableDataCellElement
    {
        #region ctor

        public HtmlTableDataCellElement(Document owner, String prefix = null)
            : base(owner, Tags.Td, prefix)
        {
        }

        #endregion
    }
}
