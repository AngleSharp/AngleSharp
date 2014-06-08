namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the HTML frameset element.
    /// </summary>
    [DomName("HTMLFrameSetElement")]
    public sealed class HTMLFrameSetElement : HTMLElement
    {
        #region ctor

        internal HTMLFrameSetElement()
        {
            _name = Tags.Frameset;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the number of columns of frames in the frameset. .
        /// </summary>
        [DomName("cols")]
        public UInt32 Cols
        {
            get { return ToInteger(GetAttribute(AttributeNames.Cols), 1u); }
            set { SetAttribute(AttributeNames.Cols, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the number of rows of frames in the frameset.
        /// </summary>
        [DomName("rows")]
        public UInt32 Rows
        {
            get { return ToInteger(GetAttribute(AttributeNames.Rows), 1u); }
            set { SetAttribute(AttributeNames.Rows, value.ToString()); }
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
