namespace AngleSharp.Dom.Html
{
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the HTML column group element.
    /// </summary>
    sealed class HtmlTableColgroupElement : HtmlElement, IHtmlTableColumnElement
    {
        #region ctor

        public HtmlTableColgroupElement(Document owner, String prefix = null)
            : base(owner, Tags.Colgroup, prefix, NodeFlags.Special)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value of the horizontal alignment attribute.
        /// </summary>
        public HorizontalAlignment Align
        {
            get { return GetOwnAttribute(AttributeNames.Align).ToEnum(HorizontalAlignment.Center); }
            set { SetOwnAttribute(AttributeNames.Align, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the number of columns in a group or affected by a grouping.
        /// </summary>
        public Int32 Span
        {
            get { return GetOwnAttribute(AttributeNames.Span).ToInteger(0); }
            set { SetOwnAttribute(AttributeNames.Span, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the value of the vertical alignment attribute.
        /// </summary>
        public VerticalAlignment VAlign
        {
            get { return GetOwnAttribute(AttributeNames.Valign).ToEnum(VerticalAlignment.Middle); }
            set { SetOwnAttribute(AttributeNames.Valign, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the value of the width attribute.
        /// </summary>
        public String Width
        {
            get { return GetOwnAttribute(AttributeNames.Width); }
            set { SetOwnAttribute(AttributeNames.Width, value); }
        }

        #endregion
    }
}
