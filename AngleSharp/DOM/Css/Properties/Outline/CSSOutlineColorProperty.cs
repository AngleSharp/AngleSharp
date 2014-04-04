namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/outline-color
    /// </summary>
    sealed class CSSOutlineColorProperty : CSSProperty
    {
        #region Fields

        static readonly InvertColorMode _invert = new InvertColorMode();
        ColorMode _mode;

        #endregion

        #region ctor

        public CSSOutlineColorProperty()
            : base(PropertyNames.OutlineColor)
        {
            _inherited = false;
            _mode = _invert;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            var color = value.ToColor();

            if (color.HasValue)
                _mode = new SolidColorMode(color.Value);
            else if (value is CSSIdentifierValue && ((CSSIdentifierValue)value).Value.Equals("invert", StringComparison.OrdinalIgnoreCase))
                _mode = _invert;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion

        #region Color Modes

        abstract class ColorMode
        {
            //TODO add members that make sense
        }

        /// <summary>
        /// Draws a solid outline with the given color.
        /// </summary>
        sealed class SolidColorMode : ColorMode
        {
            Color _color;

            public SolidColorMode(Color color)
            {
                _color = color;
            }
        }

        /// <summary>
        /// To ensure the outline is visible, performs a color inversion of the
        /// background. This makes the focus border more salient, regardless of
        /// the color in the background.
        /// </summary>
        sealed class InvertColorMode : ColorMode
        {
        }

        #endregion
    }
}
