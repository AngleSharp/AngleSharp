using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML head element.
    /// </summary>
    [DOM("HTMLHeadElement")]
    public sealed class HTMLHeadElement : HTMLElement
    {
        #region ctor

        internal HTMLHeadElement()
        {
            _name = Tags.HEAD;
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