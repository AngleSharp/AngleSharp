namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the object for HTML td elements.
    /// </summary>
    sealed class HtmlTableDataCellElement : HtmlTableCellElement, IHtmlTableDataCellElement
    {
        public HtmlTableDataCellElement(Document owner, String prefix = null)
            : base(owner, TagNames.Td, prefix)
        {
        }
    }
}
