namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the HTML ordered list (ol) element.
    /// </summary>
    sealed class HTMLOListElement : HTMLElement, IListScopeElement, IHtmlOrderedListElement
    {
        #region ctor

        internal HTMLOListElement()
        {
            _name = Tags.Ol;
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

        #region Properties

        /// <summary>
        /// Gets or sets if the order is reversed.
        /// </summary>
        public Boolean IsReversed
        {
            get { return GetAttribute(AttributeNames.Reversed) != null; }
            set { SetAttribute(AttributeNames.Reversed, value ? String.Empty : null); }
        }

        /// <summary>
        /// Gets or sets the start of the numbering.
        /// </summary>
        public Int32 Start
        {
            get { return GetAttribute(AttributeNames.Start).ToInteger(1); }
            set { SetAttribute(AttributeNames.Start, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets a value within [ 1, a, A, i, I ].
        /// </summary>
        public String Type
        {
            get { return GetAttribute(AttributeNames.Type); }
            set { SetAttribute(AttributeNames.Type, value); }
        }

        #endregion
    }
}
