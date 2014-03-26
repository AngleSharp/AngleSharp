namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the HTML paragraph element.
    /// </summary>
    [DOM("HTMLParagraphElement")]
    public sealed class HTMLParagraphElement : HTMLElement, IImpliedEnd
    {
        #region ctor

        /// <summary>
        /// Creates a new HTML paragraph element.
        /// </summary>
        internal HTMLParagraphElement()
        {
            _name = Tags.P;
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
