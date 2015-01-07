namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/column-rule-width
    /// </summary>
    sealed class CSSColumnRuleWidthProperty : CSSProperty, ICssColumnRuleWidthProperty
    {
        #region Fields

        internal static readonly Length Default = Length.Medium;
        internal static readonly IValueConverter<Length> Converter = Converters.LineWidthConverter;
        /// <summary>
        /// Describes the width of the rule separating two columns.
        /// </summary>
        Length _width;

        #endregion

        #region ctor

        internal CSSColumnRuleWidthProperty(CssStyleDeclaration rule)
            : base(PropertyNames.ColumnRuleWidth, rule, PropertyFlags.Animatable)
        {
            Reset();
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

        public void SetWidth(Length width)
        {
            _width = width;
        }

        internal override void Reset()
        {
            _width = Default;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, SetWidth);
        }

        #endregion
    }
}
