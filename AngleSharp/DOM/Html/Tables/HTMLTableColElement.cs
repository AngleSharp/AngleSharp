using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML column (col / colgroup) element.
    /// </summary>
    public sealed class HTMLTableColElement : HTMLElement
    {
        #region Constants

        /// <summary>
        /// The col tag.
        /// </summary>
        internal const String ColTag = "col";

        /// <summary>
        /// The colgroup tag.
        /// </summary>
        internal const String ColgroupTag = "colgroup";

        #endregion

        #region ctor

        internal HTMLTableColElement()
        {
            _name = ColTag;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value of the horizontal alignment attribute.
        /// </summary>
        [DOM("align")]
        public HorizontalAlignment Align
        {
            get { return ToEnum(GetAttribute("align"), HorizontalAlignment.Center); }
            set { SetAttribute("align", value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the number of columns in a group or affected by a grouping.
        /// </summary>
        [DOM("span")]
        public UInt32 Span
        {
            get { return ToInteger(GetAttribute("span"), 0u); }
            set { SetAttribute("span", value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the value of the vertical alignment attribute.
        /// </summary>
        [DOM("vAlign")]
        public VerticalAlignment VAlign
        {
            get { return ToEnum(GetAttribute("valign"), VerticalAlignment.Middle); }
            set { SetAttribute("valign", value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the value of the width attribute.
        /// </summary>
        [DOM("width")]
        public String Width
        {
            get { return GetAttribute("width"); }
            set { SetAttribute("width", value); }
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
