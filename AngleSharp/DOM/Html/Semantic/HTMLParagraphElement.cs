using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML paragraph element.
    /// </summary>
    public sealed class HTMLParagraphElement : HTMLElement
    {
        #region Constant

        /// <summary>
        /// The p tag.
        /// </summary>
        internal const String Tag = "p";

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML paragraph element.
        /// </summary>
        internal HTMLParagraphElement()
        {
            _name = Tag;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value of the alignment attribute.
        /// </summary>
        [DOM("align")]
        public HorizontalAlignment Align
        {
            get { return ToEnum(GetAttribute("align"), HorizontalAlignment.Left); }
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
    }
}
