namespace AngleSharp.Html.Dom
{
    using AngleSharp.Dom;
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
