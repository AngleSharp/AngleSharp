namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents a param element.
    /// </summary>
    sealed class HTMLParamElement : HTMLElement, IHtmlParamElement
    {
        #region ctor

        /// <summary>
        /// Creates a new HTML param element.
        /// </summary>
        internal HTMLParamElement()
        {
            _name = Tags.Param;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value of the value attribute.
        /// </summary>
        public String Value
        {
            get { return GetAttribute(AttributeNames.Value); }
            set { SetAttribute(AttributeNames.Value, value); }
        }

        /// <summary>
        /// Gets or sets the value of the name attribute.
        /// </summary>
        public String Name
        {
            get { return GetAttribute(AttributeNames.Name); }
            set { SetAttribute(AttributeNames.Name, value); }
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
