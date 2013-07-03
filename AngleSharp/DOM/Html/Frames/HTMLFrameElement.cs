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
        internal const String Tag = "frame";

        #endregion

        #region ctor

        internal HTMLFrameElement()
        {
            _name = Tag;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets if the frame cannot be resized.
        /// </summary>
        [DOM("noResize")]
        public Boolean NoResize
        {
            get { return ToBoolean(GetAttribute("noresize"), false); }
            set { SetAttribute("noresize", value.ToString()); }
        }

        #endregion

        #region Internal Properties

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
