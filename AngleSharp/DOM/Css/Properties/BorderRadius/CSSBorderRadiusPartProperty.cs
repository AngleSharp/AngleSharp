namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Basis for all elementary border-radius properties.
    /// </summary>
    abstract class CSSBorderRadiusPartProperty : CSSProperty
    {
        #region Fields

        internal static readonly Length Default = Length.Zero;
        internal static readonly IValueConverter<Length> SingleConverter = Converters.LengthOrPercentConverter;
        internal static readonly IValueConverter<Tuple<Length, Length?>> Converter = Converters.WithOrder(
            SingleConverter.Required(), 
            SingleConverter.ToNullable().Option(null));
        Length _horizontal;
        Length _vertical;

        #endregion

        #region ctor

        internal CSSBorderRadiusPartProperty(String name, CSSStyleDeclaration rule)
            : base(name, rule, PropertyFlags.Animatable)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the horizontal radius of the ellipse.
        /// </summary>
        public IDistance HorizontalRadius
        {
            get { return _horizontal; }
        }

        /// <summary>
        /// Gets if the horizontal radius is the same as the vertical one.
        /// </summary>
        public Boolean IsCircle
        {
            get { return _horizontal.Equals(_vertical); }
        }

        /// <summary>
        /// Gets the vertical radius of the ellipse.
        /// </summary>
        public IDistance VerticalRadius
        {
            get { return _vertical; }
        }

        #endregion

        #region Methods

        public void SetRadius(Length horizontal, Length vertical)
        {
            _horizontal = horizontal;
            _vertical = vertical;
        }

        internal override void Reset()
        {
            _horizontal = Default;
            _vertical = Default;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, m => SetRadius(m.Item1, m.Item2 ?? m.Item1));
        }

        #endregion
    }
}
