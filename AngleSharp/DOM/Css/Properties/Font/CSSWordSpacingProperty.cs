namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/word-spacing
    /// </summary>
    public sealed class CSSWordSpacingProperty : CSSProperty
    {
        #region Fields

        Length? _spacing;

        #endregion

        #region ctor

        internal CSSWordSpacingProperty()
            : base(PropertyNames.WordSpacing, PropertyFlags.Inherited | PropertyFlags.Unitless)
        {
            _spacing = null;
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

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            if (value.Is("normal"))
                _spacing = null;
            else if (value.ToLength().HasValue)
                _spacing = value.ToLength();
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
