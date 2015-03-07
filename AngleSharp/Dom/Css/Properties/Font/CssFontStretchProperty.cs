namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/font-stretch
    /// </summary>
    sealed class CssFontStretchProperty : CssProperty
    {
        #region Fields

        internal static readonly FontStretch Default = FontStretch.Normal;
        internal static readonly IValueConverter<FontStretch> Converter = Map.FontStretches.ToConverter();
        FontStretch _stretch;

        #endregion

        #region ctor

        internal CssFontStretchProperty(CssStyleDeclaration rule)
            : base(PropertyNames.FontStretch, rule, PropertyFlags.Inherited | PropertyFlags.Animatable)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected font stretch setting.
        /// </summary>
        public FontStretch Stretch
        {
            get { return _stretch; }
        }

        #endregion

        #region Methods

        void SetStretch(FontStretch stretch)
        {
            _stretch = stretch;
        }

        internal override void Reset()
        {
            _stretch = Default;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, SetStretch);
        }

        #endregion
    }
}
