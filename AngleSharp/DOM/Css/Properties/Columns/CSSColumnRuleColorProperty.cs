namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/column-rule-color
    /// </summary>
    sealed class CSSColumnRuleColorProperty : CSSProperty, ICssColumnRuleColorProperty
    {
        #region Fields

        /// <summary>
        /// Represents the color of the rule separating columns.
        /// </summary>
        Color _color;

        #endregion

        #region ctor

        internal CSSColumnRuleColorProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.ColumnRuleColor, rule, PropertyFlags.Animatable)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the color for the vertical column rule.
        /// </summary>
        public Color Color
        {
            get { return _color; }
        }

        #endregion

        #region Methods

        internal override void Reset()
        {
            _color = Color.Transparent;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            var color = value.ToColor();

            if (color.HasValue)
            {
                _color = color.Value;
                return true;
            }
            
            return false;
        }

        #endregion
    }
}
