namespace AngleSharp.Dom.Html
{
    using System;
    using AngleSharp.Attributes;
    using AngleSharp.Extensions;
    using AngleSharp.Html;

    /// <summary>
    /// Represents the HTML frameset element.
    /// Obsolete since HTML 4.01.
    /// </summary>
    [DomHistorical]
    sealed class HtmlFrameSetElement : HtmlElement
    {
        #region ctor

        public HtmlFrameSetElement(Document owner, String prefix = null)
            : base(owner, Tags.Frameset, prefix, NodeFlags.Special)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the number of columns of frames in the frameset. .
        /// </summary>
        public Int32 Columns
        {
            get { return GetOwnAttribute(AttributeNames.Cols).ToInteger(1); }
            set { SetOwnAttribute(AttributeNames.Cols, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the number of rows of frames in the frameset.
        /// </summary>
        public Int32 Rows
        {
            get { return GetOwnAttribute(AttributeNames.Rows).ToInteger(1); }
            set { SetOwnAttribute(AttributeNames.Rows, value.ToString()); }
        }

        #endregion
    }
}
