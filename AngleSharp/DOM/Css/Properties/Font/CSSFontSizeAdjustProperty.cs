namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information:
    /// http://dev.w3.org/csswg/css-fonts/#propdef-font-size-adjust
    /// </summary>
    sealed class CssFontSizeAdjustProperty : CssProperty
    {
        #region Fields

        internal static readonly Single? Default = null;
        internal static readonly IValueConverter<Single?> Converter = Converters.NumberConverter.ToNullable().Or(Keywords.None, Default);
        Single? _aspectValue;

        #endregion

        #region ctor

        internal CssFontSizeAdjustProperty(CssStyleDeclaration rule)
            : base(PropertyNames.FontSizeAdjust, rule, PropertyFlags.Inherited | PropertyFlags.Animatable)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the aspect value specified by the property, if any.
        /// </summary>
        public Single? AspectValue
        {
            get { return _aspectValue; }
        }

        #endregion

        #region Methods

        void SetAspectValue(Single? aspectValue)
        {
            _aspectValue = aspectValue;
        }

        internal override void Reset()
        {
            _aspectValue = Default;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, SetAspectValue);
        }

        #endregion
    }
}
