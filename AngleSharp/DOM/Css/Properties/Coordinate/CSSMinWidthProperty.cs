namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/min-width
    /// </summary>
    sealed class CssMinWidthProperty : CssProperty, ICssMinWidthProperty
    {
        #region Fields

        internal static readonly Length Default = Length.Zero;
        internal static readonly IValueConverter<Length> Converter = Converters.LengthOrPercentConverter;
        Length _mode;

        #endregion

        #region ctor

        internal CssMinWidthProperty(CssStyleDeclaration rule)
            : base(PropertyNames.MinWidth, rule, PropertyFlags.Animatable)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the minimum height of the element.
        /// </summary>
        public Length? Limit
        {
            get { return _mode; }
        }

        #endregion

        #region Methods

        public void SetLimit(Length mode)
        {
            _mode = mode;
        }

        internal override void Reset()
        {
            _mode = Default;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, SetLimit);
        }

        #endregion
    }
}
