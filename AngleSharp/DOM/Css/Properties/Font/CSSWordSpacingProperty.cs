namespace AngleSharp.DOM.Css
{
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/word-spacing
    /// </summary>
    sealed class CSSWordSpacingProperty : CSSProperty, ICssWordSpacingProperty
    {
        #region Fields

        Length? _spacing;

        #endregion

        #region ctor

        internal CSSWordSpacingProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.WordSpacing, rule, PropertyFlags.Inherited | PropertyFlags.Unitless | PropertyFlags.Animatable)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if normal inter-word space, as defined by the current
        /// font and/or the browser, is active.
        /// </summary>
        public Boolean IsNormal
        {
            get { return _spacing.HasValue == false; }
        }

        /// <summary>
        /// Gets the defined custom spacing, if any.
        /// </summary>
        public Length? Spacing
        {
            get { return _spacing; }
        }

        #endregion

        #region Methods

        internal override void Reset()
        {
            _spacing = null;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            if (value.Is(Keywords.Normal))
                _spacing = null;
            else if (value.ToLength().HasValue)
                _spacing = value.ToLength();
            else
                return false;

            return true;
        }

        #endregion
    }
}
