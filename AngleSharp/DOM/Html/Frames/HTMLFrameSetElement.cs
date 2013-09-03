using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML frameset element.
    /// </summary>
    [DOM("HTMLFrameSetElement")]
    public sealed class HTMLFrameSetElement : HTMLElement
    {
        #region ctor

        internal HTMLFrameSetElement()
        {
            _name = Tags.FRAMESET;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the number of columns of frames in the frameset. .
        /// </summary>
        [DOM("cols")]
        public UInt32 Cols
        {
            get { return ToInteger(GetAttribute(AttributeNames.COLS), 1u); }
            set { SetAttribute(AttributeNames.COLS, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the number of rows of frames in the frameset.
        /// </summary>
        [DOM("rows")]
        public UInt32 Rows
        {
            get { return ToInteger(GetAttribute(AttributeNames.ROWS), 1u); }
            set { SetAttribute(AttributeNames.ROWS, value.ToString()); }
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
