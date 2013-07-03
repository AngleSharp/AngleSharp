using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML frameset element.
    /// </summary>
    public sealed class HTMLFrameSetElement : HTMLElement
    {
        #region Constant

        /// <summary>
        /// The frameset tag.
        /// </summary>
        internal const String Tag = "frameset";

        #endregion

        #region ctor

        internal HTMLFrameSetElement()
        {
            _name = Tag;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the number of columns of frames in the frameset. .
        /// </summary>
        [DOM("cols")]
        public UInt32 Cols
        {
            get { return ToInteger(GetAttribute("cols"), 1u); }
            set { SetAttribute("cols", value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the number of rows of frames in the frameset.
        /// </summary>
        [DOM("rows")]
        public UInt32 Rows
        {
            get { return ToInteger(GetAttribute("rows"), 1u); }
            set { SetAttribute("rows", value.ToString()); }
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
