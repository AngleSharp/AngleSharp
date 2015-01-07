namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-spacing
    /// </summary>
    sealed class CSSBorderSpacingProperty : CSSProperty, ICssBorderSpacingProperty
    {
        #region Fields

        internal static readonly Length Default = Length.Zero;
        internal static readonly IValueConverter<Length[]> Converter = Converters.LengthConverter.Many(1, 2);
        Length _h;
        Length _v;

        #endregion

        #region ctor

        internal CSSBorderSpacingProperty(CssStyleDeclaration rule)
            : base(PropertyNames.BorderSpacing, rule, PropertyFlags.Inherited)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the horizontal spacing between cells, that is the space
        /// between cells in adjacent columns.
        /// </summary>
        public Length Horizontal
        {
            get { return _h; }
        }

        /// <summary>
        /// Gets the vertical spacing between cells, that is the space
        /// between cells in adjacent rows.
        /// </summary>
        public Length Vertical
        {
            get { return _v; }
        }

        #endregion

        #region Methods

        public void SetSpacing(Length horizontal, Length vertical)
        {
            _h = horizontal;
            _v = vertical;
        }

        internal override void Reset()
        {
            _h = Default;
            _v = Default;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, m => SetSpacing(m[0], m.Length == 2 ? m[1] : m[0]));
        }

        #endregion
    }
}
