using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents an HTML li, dd or dt tag.
    /// </summary>
    [DOM("HTMLLIElement")]
    public sealed class HTMLLIElement : HTMLElement, IImplClosed
    {
        #region ctor

        /// <summary>
        /// Creates a new item tag.
        /// </summary>
        internal HTMLLIElement()
        {
            _name = Tags.LI;
        }

        #endregion

        #region Internal properties

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
