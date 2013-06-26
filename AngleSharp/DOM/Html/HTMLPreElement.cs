using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML pre element.
    /// </summary>
    public sealed class HTMLPreElement : HTMLElement
    {
        #region Constant

        /// <summary>
        /// The pre tag.
        /// </summary>
        internal const String Tag = "pre";

        #endregion

        #region ctor

        internal HTMLPreElement()
        {
            _name = Tag;
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
