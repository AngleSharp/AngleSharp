namespace AngleSharp.DOM.Html
{
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Represents the HTML caption element.
    /// </summary>
    sealed class HTMLTableCaptionElement : HTMLElement, IHtmlTableCaptionElement
    {
        #region ctor

        public HTMLTableCaptionElement()
            : base(Tags.Caption, NodeFlags.Special | NodeFlags.Scoped)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value of the alignment attribute.
        /// </summary>
        public CaptionAlignment Align
        {
            get { return GetAttribute(AttributeNames.Align).ToEnum(CaptionAlignment.Top); }
            set { SetAttribute(AttributeNames.Align, value.ToString()); }
        }

        #endregion

        #region Enumeration

        /// <summary>
        /// Specifies the position of the caption with respect to the table.
        /// </summary>
        public enum CaptionAlignment
        {
            /// <summary>
            /// The caption is at the top of the table. This is the default value.
            /// </summary>
            Top,
            /// <summary>
            /// The caption is at the bottom of the table.
            /// </summary>
            Bottom,
            /// <summary>
            /// The caption is at the left of the table.
            /// </summary>
            Left,
            /// <summary>
            /// The caption is at the rigzt of the table.
            /// </summary>
            Right
        }

        #endregion
    }
}
