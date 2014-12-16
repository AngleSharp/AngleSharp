namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/max-height
    /// </summary>
    sealed class CSSMaxHeightProperty : CSSProperty, ICssMaxHeightProperty
    {
        #region Fields

        internal static readonly Length? Default = null;
        internal static readonly IValueConverter<Length?> Converter = Converters.LengthOrPercentConverter.ToNullable().Or(Keywords.None, Default);
        Length? _mode;

        #endregion

        #region ctor

        internal CSSMaxHeightProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.MaxHeight, rule, PropertyFlags.Animatable)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the specified max-height of the element. A percentage is calculated
        /// with respect to the height of the containing block. If the height of the
        /// containing block is not specified explicitly, the percentage value is
        /// treated as none.
        /// </summary>
        public Length? Limit
        {
            get { return _mode; }
        }

        #endregion

        #region Methods

        public void SetLimit(Length? mode)
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
