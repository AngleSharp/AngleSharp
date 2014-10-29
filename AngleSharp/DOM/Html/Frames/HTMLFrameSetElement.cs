namespace AngleSharp.DOM.Html
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents the HTML frameset element.
    /// Obsolete since HTML 4.01.
    /// </summary>
    [DomHistorical]
    sealed class HTMLFrameSetElement : HTMLElement
    {
        #region ctor

        public HTMLFrameSetElement()
            : base(Tags.Frameset, NodeFlags.Special)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the number of columns of frames in the frameset. .
        /// </summary>
        public Int32 Columns
        {
            get { return GetAttribute(AttributeNames.Cols).ToInteger(1); }
            set { SetAttribute(AttributeNames.Cols, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the number of rows of frames in the frameset.
        /// </summary>
        public Int32 Rows
        {
            get { return GetAttribute(AttributeNames.Rows).ToInteger(1); }
            set { SetAttribute(AttributeNames.Rows, value.ToString()); }
        }

        #endregion
    }
}
