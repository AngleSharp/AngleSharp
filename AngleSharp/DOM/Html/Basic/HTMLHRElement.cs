using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the hr element.
    /// </summary>
    [DOM("HTMLHRElement")]
    public sealed class HTMLHRElement : HTMLElement
    {
        #region Constant

        /// <summary>
        /// The hr tag.
        /// </summary>
        internal const String Tag = "hr";

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new hr element.
        /// </summary>
        internal HTMLHRElement()
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
