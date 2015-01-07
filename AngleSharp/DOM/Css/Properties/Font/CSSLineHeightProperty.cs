namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using System;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/line-height
    /// </summary>
    sealed class CssLineHeightProperty : CssProperty, ICssLineHeightProperty
    {
        #region Fields

        internal static readonly Length Default = new Length(120f, Length.Unit.Percent);
        internal static readonly IValueConverter<Length> Converter = Converters.LineHeightConverter;
        Length _height;

        #endregion

        #region ctor

        internal CssLineHeightProperty(CssStyleDeclaration rule)
            : base(PropertyNames.LineHeight, rule, PropertyFlags.Inherited | PropertyFlags.Animatable)
        {
            Reset();
        }

        #endregion

        #region Properties

        public Length Height
        {
            get { return _height; }
        }

        #endregion

        #region Methods

        void SetHeight(Length height)
        {
            _height = height;
        }

        internal override void Reset()
        {
            _height = Default;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, SetHeight);
        }

        #endregion
    }
}
