using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents an HTML element with only semantic meaning.
    /// </summary>
    sealed class HTMLSemanticElement : HTMLElement
    {
        #region Members

        Boolean _special;

        #endregion

        #region ctor

        internal HTMLSemanticElement(Boolean special = true)
        {
            _special = special;
        }

        #endregion

        #region Internal Properties

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override Boolean IsSpecial
        {
            get { return _special; }
        }

        #endregion
    }
}
