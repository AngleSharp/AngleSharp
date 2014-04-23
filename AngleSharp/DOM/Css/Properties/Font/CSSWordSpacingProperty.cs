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
            : base(PropertyNames.WordSpacing)
        {
            _inherited = true;
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
