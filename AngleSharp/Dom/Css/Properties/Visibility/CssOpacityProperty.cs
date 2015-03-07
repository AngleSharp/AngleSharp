namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/opacity
    /// </summary>
    sealed class CssOpacityProperty : CssProperty
    {
        #region Fields

        internal static readonly IValueConverter<Single> Converter = Converters.NumberConverter;
        internal static readonly Single Default = 1f;
        Single _opacity;

        #endregion

        #region ctor

        internal CssOpacityProperty(CssStyleDeclaration rule)
            : base(PropertyNames.Opacity, rule, PropertyFlags.Animatable)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value that should be used for the opacity.
        /// </summary>
        public Single Opacity
        {
            get { return _opacity; }
        }

        #endregion

        #region Methods

        public void SetOpacity(Single opacity)
        {
            _opacity = opacity;
        }

        internal override void Reset()
        {
            _opacity = Default;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, SetOpacity);
        }

        #endregion
    }
}
