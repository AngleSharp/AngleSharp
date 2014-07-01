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

        #region Properties

        /// <summary>
        /// Gets or sets the value of the scope attribute. The allowed
        /// values are col, row, colgroup or rowgroup.
        /// </summary>
        public String Scope 
        {
            get { return GetAttribute(AttributeNames.Scope); }
            set { SetAttribute(AttributeNames.Scope, value); } 
        }

        #endregion
    }
}
