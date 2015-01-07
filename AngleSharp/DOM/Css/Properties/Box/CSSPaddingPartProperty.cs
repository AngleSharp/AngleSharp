namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using System;

    /// <summary>
    /// Basis for all elementary padding properties.
    /// </summary>
    abstract class CssPaddingPartProperty : CssProperty
    {
        #region Fields

        internal static readonly Length Default = Length.Zero;
        internal static readonly IValueConverter<Length> Converter = Converters.LengthOrPercentConverter;
        Length _padding;

        #endregion

        #region ctor

        internal CssPaddingPartProperty(String name, CssStyleDeclaration rule)
            : base(name, rule, PropertyFlags.Unitless | PropertyFlags.Animatable)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the padding relative to the width of the containing block or
        /// a fixed width.
        /// </summary>
        internal Length Padding
        {
            get { return _padding; }
        }

        #endregion

        #region Methods

        void SetPadding(Length padding)
        {
            _padding = padding;
        }

        internal override void Reset()
        {
            _padding = Default;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, SetPadding);
        }

        #endregion
    }
}
