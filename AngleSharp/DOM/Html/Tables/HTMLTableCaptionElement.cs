using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML caption element.
    /// </summary>
    public sealed class HTMLTableCaptionElement : HTMLElement
    {
        #region Constant

        /// <summary>
        /// The caption tag.
        /// </summary>
        internal const String Tag = "caption";

        #endregion

        #region ctor

        internal HTMLTableCaptionElement()
        {
            _name = Tag;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value of the alignment attribute.
        /// </summary>
        [DOM("align")]
        public CaptionAlignment Align
        {
            get { return ToEnum(GetAttribute("align"), CaptionAlignment.Top); }
            set { SetAttribute("align", value.ToString()); }
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
