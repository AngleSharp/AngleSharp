using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML pre element.
    /// </summary>
    [DOM("HTMLPreElement")]
    public sealed class HTMLPreElement : HTMLElement
    {
        #region ctor

        internal HTMLPreElement()
        {
            _name = Tags.PRE;
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
