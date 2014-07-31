namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML paragraph element.
    /// </summary>
    sealed class HTMLParagraphElement : HTMLElement, IHtmlParagraphElement
    {
        #region ctor

        /// <summary>
        /// Creates a new HTML paragraph element.
        /// </summary>
        internal HTMLParagraphElement()
            : base(Tags.P, NodeFlags.Special | NodeFlags.ImplicitelyClosed | NodeFlags.ImpliedEnd)
        {
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
    }
}
