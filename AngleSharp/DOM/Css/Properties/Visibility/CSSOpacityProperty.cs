namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/opacity
    /// </summary>
    sealed class CSSOpacityProperty : CSSProperty, ICssOpacityProperty
    {
        #region Fields

        Single _opacity;

        #endregion

        #region ctor

        internal CSSOpacityProperty(CSSStyleDeclaration rule)
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
            _opacity = 1f;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            return this.WithNumber().TryConvert(value, SetOpacity);
        }

        #endregion
    }
}
