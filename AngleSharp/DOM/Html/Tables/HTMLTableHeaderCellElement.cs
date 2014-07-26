namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the object for HTML th elements.
    /// </summary>
    sealed class HTMLTableHeaderCellElement : HTMLTableCellElement, IHtmlTableHeaderCellElement
    {
        #region ctor

        internal HTMLTableHeaderCellElement()
        {
            _name = Tags.Th;
        }

        #endregion
    }
}
