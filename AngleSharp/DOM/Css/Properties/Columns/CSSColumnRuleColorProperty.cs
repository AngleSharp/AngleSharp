namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/column-rule-color
    /// </summary>
    sealed class CssColumnRuleColorProperty : CssProperty, ICssColumnRuleColorProperty
    {
        #region Fields

        internal static readonly Color Default = Color.Transparent;
        internal static readonly IValueConverter<Color> Converter = Converters.ColorConverter;
        /// <summary>
        /// Represents the color of the rule separating columns.
        /// </summary>
        Color _color;

        #endregion

        #region ctor

        internal CssColumnRuleColorProperty(CssStyleDeclaration rule)
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
            _color = Default;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, SetColor);
        }

        #endregion
    }
}
