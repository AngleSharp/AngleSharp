namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-image-outset
    /// </summary>
    sealed class CssBorderImageOutsetProperty : CssProperty
    {
        #region Fields

        internal static readonly Length Default = Length.Zero;
        internal static readonly IValueConverter<Tuple<Length, Length, Length, Length>> Converter = Converters.LengthOrPercentConverter.Periodic();
        Length _top;
        Length _right;
        Length _bottom;
        Length _left;

        #endregion

        #region ctor

        internal CssBorderImageOutsetProperty(CssStyleDeclaration rule)
            : base(PropertyNames.BorderImageOutset, rule)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the length or percentage for the outset of the top border.
        /// </summary>
        public Length OutsetTop
        {
            get { return _top; }
        }

        /// <summary>
        /// Gets the length or percentage for the outset of the right border.
        /// </summary>
        public Length OutsetRight
        {
            get { return _right; }
        }

        /// <summary>
        /// Gets the length or percentage for the outset of the bottom border.
        /// </summary>
        public Length OutsetBottom
        {
            get { return _bottom; }
        }

        /// <summary>
        /// Gets the length or percentage for the outset of the left border.
        /// </summary>
        public Length OutsetLeft
        {
            get { return _left; }
        }

        #endregion

        #region Methods

        void SetOutset(Length top, Length right, Length bottom, Length left)
        {
            _top = top;
            _right = right;
            _bottom = bottom;
            _left = left;
        }

        internal override void Reset()
        {
            _top = Default;
            _right = Default;
            _bottom = Default;
            _left = Default;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, m => SetOutset(m.Item1, m.Item2, m.Item3, m.Item4));
        }

        #endregion
    }
}
