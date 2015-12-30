namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the object for HTML th elements.
    /// </summary>
    sealed class HtmlTableHeaderCellElement : HtmlTableCellElement, IHtmlTableHeaderCellElement
    {
        public HtmlTableHeaderCellElement(Document owner, String prefix = null)
            : base(owner, TagNames.Th, prefix)
        {
        }
    }
}
