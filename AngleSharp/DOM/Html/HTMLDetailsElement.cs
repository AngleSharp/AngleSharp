namespace AngleSharp.DOM.Html
{
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
        internal HTMLDetailsElement()
        {
            _name = Tags.Details;
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
