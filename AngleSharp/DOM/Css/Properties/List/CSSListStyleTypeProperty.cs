namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/list-style-type
    /// </summary>
    sealed class CSSListStyleTypeProperty : CSSProperty, ICssListStyleTypeProperty
    {
        #region Fields

        ListStyle _style;

        #endregion

        #region ctor

        internal CSSListStyleTypeProperty()
            : base(PropertyNames.ListStyleType, PropertyFlags.Inherited)
        {
            _style = ListStyle.Disc;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected style for the list.
        /// </summary>
        public ListStyle Style
        {
            get { return _style; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            var position = value.ToListStyle();

            if (position.HasValue)
                _style = position.Value;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
