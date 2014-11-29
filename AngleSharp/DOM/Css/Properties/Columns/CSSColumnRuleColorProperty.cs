namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
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

        public void SetColor(Color color)
        {
            _color = color;
        }

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
            return WithColor().TryConvert(value, SetColor);
        }

        #endregion
    }
}
