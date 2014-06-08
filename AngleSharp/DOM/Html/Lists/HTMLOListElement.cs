namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the HTML ordered list (ol) element.
    /// </summary>
    [DomName("HTMLOListElement")]
    public sealed class HTMLOListElement : HTMLElement, IListScopeElement
    {
        #region ctor

        internal HTMLOListElement()
        {
            _name = Tags.Ol;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override Boolean IsSpecial
        {
            get { return true; }
        }

        #endregion
    }
}
