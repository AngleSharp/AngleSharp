namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the HTML optgroup element.
    /// </summary>
    sealed class HTMLOptGroupElement : HTMLElement, ISelectScopeElement, IImpliedEnd, IHtmlOptionsGroupElement
    {
        #region ctor

        /// <summary>
        /// Creates a new HTML optgroup element.
        /// </summary>
        internal HTMLOptGroupElement()
        {
            _name = Tags.Optgroup;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        public String Label
        {
            get { return GetAttribute(AttributeNames.Label); }
            set { SetAttribute(AttributeNames.Label, value); }
        }

        /// <summary>
        /// Gets or sets if the optgroup is enabled or disabled.
        /// </summary>
        public Boolean IsDisabled
        {
            get { return GetAttribute(AttributeNames.Disabled) != null; }
            set { SetAttribute(AttributeNames.Disabled, value ? String.Empty : null); }
        }

        #endregion

        #region Internal properties

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override Boolean IsSpecial
        {
            get { return false; }
        }

        #endregion
    }
}
