namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/word-spacing
    /// </summary>
    sealed class CSSWordSpacingProperty : CSSProperty
    {
        #region Fields

        static readonly NormalWordSpacingMode _normal = new NormalWordSpacingMode();
        WordSpacingMode _mode;

        #endregion

        #region ctor

        public CSSWordSpacingProperty()
            : base(PropertyNames.WordSpacing)
        {
            _inherited = true;
            _mode = _normal;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            if (value.Is("normal"))
                _mode = _normal;
            else if (value.ToLength().HasValue)
                _mode = new CustomWordSpacingMode(value.ToLength().Value);
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion

        #region Modes

        abstract class WordSpacingMode
        {
            //TODO add members
        }

        /// <summary>
        /// The normal inter-word space, as defined by the current
        /// font and/or the browser.
        /// </summary>
        sealed class NormalWordSpacingMode : WordSpacingMode
        {

        }

        /// <summary>
        /// Sets a custom length for spacing between two words.
        /// </summary>
        sealed class CustomWordSpacingMode : WordSpacingMode
        {
            Length _spacing;

            public CustomWordSpacingMode(Length spacing)
            {
                _spacing = spacing;
            }
        }

        #endregion
    }
}
