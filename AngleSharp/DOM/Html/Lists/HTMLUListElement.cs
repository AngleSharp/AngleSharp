namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// The DOM Object representing the unordered list.
    /// </summary>
    [DOM("HTMLUListElement")]
    public sealed class HTMLUListElement : HTMLElement, IListScopeElement
    {
        #region ctor

        internal HTMLUListElement()
        {
            _name = Tags.UL;
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
