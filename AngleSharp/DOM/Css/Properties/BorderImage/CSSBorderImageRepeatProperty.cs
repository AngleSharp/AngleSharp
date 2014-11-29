namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/border-image-repeat
    /// </summary>
    sealed class CSSBorderImageRepeatProperty : CSSProperty, ICssBorderImageRepeatProperty
    {
        #region Fields

        BorderRepeat _horizontal;
        BorderRepeat _vertical;

        #endregion

        #region ctor

        internal CSSBorderImageRepeatProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.BorderImageRepeat, rule)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the horizontal repeat value.
        /// </summary>
        public BorderRepeat Horizontal
        {
            get { return _horizontal; }
        }

        /// <summary>
        /// Gets the vertical repeat value.
        /// </summary>
        public BorderRepeat Vertical
        {
            get { return _vertical; }
        }

        #endregion

        #region Methods

        public void SetRepeat(BorderRepeat horizontal, BorderRepeat vertical)
        {
            _horizontal = horizontal;
            _vertical = vertical;
        }

        internal override void Reset()
        {
            _horizontal = BorderRepeat.Stretch;
            _vertical = BorderRepeat.Stretch;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            return TakeMany(From(Map.BorderRepeatModes)).Constraint(m => m.Length < 3).TryConvert(value, m => SetRepeat(m[0], m.Length == 2 ? m[1] : m[0]));
        }

        #endregion
    }
}
