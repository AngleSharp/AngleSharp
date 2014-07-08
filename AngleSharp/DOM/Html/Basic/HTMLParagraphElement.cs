namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the HTML paragraph element.
    /// </summary>
    sealed class HTMLParagraphElement : HTMLElement, IImpliedEnd, IHtmlParagraphElement
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
        public HorizontalAlignment Align
        {
            get { return GetAttribute(AttributeNames.Align).ToEnum(HorizontalAlignment.Left); }
            set { SetAttribute(AttributeNames.Align, value.ToString()); }
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
