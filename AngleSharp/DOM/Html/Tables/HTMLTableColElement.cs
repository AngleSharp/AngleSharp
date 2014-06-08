namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the HTML column (col / colgroup) element.
    /// </summary>
    [DomName("HTMLTableColElement")]
    public sealed class HTMLTableColElement : HTMLElement
    {
        #region ctor

        internal HTMLTableColElement()
        {
            _name = Tags.Col;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value of the horizontal alignment attribute.
        /// </summary>
        [DomName("align")]
        public HorizontalAlignment Align
        {
            get { return ToEnum(GetAttribute("align"), HorizontalAlignment.Center); }
            set { SetAttribute("align", value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the number of columns in a group or affected by a grouping.
        /// </summary>
        [DomName("span")]
        public UInt32 Span
        {
            get { return ToInteger(GetAttribute("span"), 0u); }
            set { SetAttribute("span", value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the value of the vertical alignment attribute.
        /// </summary>
        [DomName("vAlign")]
        public VerticalAlignment VAlign
        {
            get { return ToEnum(GetAttribute("valign"), VerticalAlignment.Middle); }
            set { SetAttribute("valign", value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the value of the width attribute.
        /// </summary>
        [DomName("width")]
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
