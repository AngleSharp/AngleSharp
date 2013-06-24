using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML iframe element.
    /// </summary>
    public sealed class HTMLIFrameElement : HTMLFrameElementBase
    {
        #region Constant

        /// <summary>
        /// The iframe tag.
        /// </summary>
        internal const string Tag = "iframe";

        #endregion

        #region ctor

        internal HTMLIFrameElement()
        {
            _name = Tag;
        }

        #endregion

        #region Internal properties

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override bool IsSpecial
        {
            get { return true; }
        }

        #endregion
    }
}
