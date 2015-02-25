namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Basis for all elementary margin properties.
    /// </summary>
    abstract class CssMarginPartProperty : CssProperty
    {
        #region Fields

        internal static readonly Length Default = Length.Zero;
        internal static readonly IValueConverter<Length?> Converter = Converters.AutoLengthOrPercentConverter;
        Length? _margin;

        #endregion

        #region ctor

        internal CssMarginPartProperty(String name, CssStyleDeclaration rule)
            : base(name, rule, PropertyFlags.Unitless | PropertyFlags.Animatable)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the margin is automatically determined.
        /// </summary>
        public Boolean IsAuto
        {
            get { return _margin == null; }
        }

        /// <summary>
        /// Gets the margin relative to the width of the containing block or
        /// a fixed width, if any.
        /// </summary>
        internal Length? Margin
        {
            get { return _margin; }
        }

        #endregion

        #region Methods

        void SetMargin(Length? margin)
        {
            _margin = margin;
        }

        internal override void Reset()
        {
            _margin = Default;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, SetMargin);
        }

        #endregion
    }
}
