namespace AngleSharp.DOM.Html
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the HTML details element.
    /// </summary>
    sealed class HTMLDetailsElement : HTMLElement, IHtmlDetailsElement
    {
        #region ctor

        /// <summary>
        /// Creates a new HTML details element.
        /// </summary>
        public HTMLDetailsElement(Document owner)
            : base(Tags.Details, NodeFlags.Special)
        {
            Owner = owner;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets if the details element is open.
        /// </summary>
        public Boolean IsOpen
        {
            get { return GetAttribute(AttributeNames.Open) != null; }
            set { SetAttribute(AttributeNames.Open, value ? String.Empty : null); }
        }

        #endregion
    }
}
