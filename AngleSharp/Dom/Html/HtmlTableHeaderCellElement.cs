namespace AngleSharp.Dom.Html
{
    using System;
    using AngleSharp.Html;

    /// <summary>
    /// Represents the object for HTML th elements.
    /// </summary>
    sealed class HtmlTableHeaderCellElement : HtmlTableCellElement, IHtmlTableHeaderCellElement
    {
        #region ctor

        public HtmlTableHeaderCellElement(Document owner, String prefix = null)
            : base(owner, Tags.Th, prefix)
        {
        }

        #endregion
    }
}
