using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML frame element.
    /// </summary>
    public sealed class HTMLFrameElement : HTMLFrameElementBase
    {
        #region Constant

        /// <summary>
        /// The frame tag.
        /// </summary>
        internal const string Tag = "frame";

        #endregion

        #region ctor

        internal HTMLFrameElement()
        {
            _name = Tag;
        }

        #endregion

        #region Internal Properties

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override bool IsSpecial
        {
            get { return true; }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the frame has a border.
        /// </summary>
        public Boolean HasFrameBorder
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets if the frame cannot be resized.
        /// </summary>
        public Boolean NoResize
        {
            get;
            private set;
        }

        #endregion
    }
}
