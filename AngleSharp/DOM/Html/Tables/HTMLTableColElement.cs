namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the HTML column (col / colgroup) element.
    /// </summary>
    sealed class HTMLTableColElement : HTMLElement, IHtmlTableColumnElement
    {
        #region ctor

        internal HTMLTableColElement(String name)
            : base(name, NodeFlags.Special)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value of the horizontal alignment attribute.
        /// </summary>
        public HorizontalAlignment Align
        {
            get { return GetAttribute(AttributeNames.Align).ToEnum(HorizontalAlignment.Center); }
            set { SetAttribute(AttributeNames.Align, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the number of columns in a group or affected by a grouping.
        /// </summary>
        public Int32 Span
        {
            get { return GetAttribute(AttributeNames.Span).ToInteger(0); }
            set { SetAttribute(AttributeNames.Span, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the value of the vertical alignment attribute.
        /// </summary>
        public VerticalAlignment VAlign
        {
            get { return GetAttribute(AttributeNames.Valign).ToEnum(VerticalAlignment.Middle); }
            set { SetAttribute(AttributeNames.Valign, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the value of the width attribute.
        /// </summary>
        public String Width
        {
            get { return GetAttribute(AttributeNames.Width); }
            set { SetAttribute(AttributeNames.Width, value); }
        }

        #endregion
    }
}
