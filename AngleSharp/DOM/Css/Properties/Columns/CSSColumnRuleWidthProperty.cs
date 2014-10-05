namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/column-rule-width
    /// </summary>
    sealed class CSSColumnRuleWidthProperty : CSSProperty, ICssColumnRuleWidthProperty
    {
        #region Fields

        /// <summary>
        /// Describes the width of the rule separating two columns.
        /// </summary>
        Length _width;

        #endregion

        #region ctor

        internal CSSColumnRuleWidthProperty()
            : base(PropertyNames.ColumnRuleWidth, PropertyFlags.Animatable)
        {
            _width = Length.Medium;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the width of the column-rule.
        /// </summary>
        public Length Width
        {
            get { return _width; }
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
            var width = value.ToBorderWidth();

            if (width.HasValue)
                _width = width.Value;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
