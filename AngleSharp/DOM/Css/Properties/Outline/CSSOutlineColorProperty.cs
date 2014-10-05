namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/outline-color
    /// </summary>
    sealed class CSSOutlineColorProperty : CSSProperty, ICssOutlineColorProperty
    {
        #region Fields

        static readonly InvertColorMode _invert = new InvertColorMode();
        ColorMode _mode;

        #endregion

        #region ctor

        internal CSSOutlineColorProperty()
            : base(PropertyNames.OutlineColor, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the color of the outline.
        /// </summary>
        public Color Color
        {
            get { return _mode.ComputeColor(); }
        }

        #endregion

        #region Methods

        protected override void Reset()
        {
            _mode = _invert;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            var color = value.ToColor();

            if (color.HasValue)
                _mode = new SolidColorMode(color.Value);
            else if (value.Is(Keywords.Invert))
                _mode = _invert;
            else
                return false;

            return true;
        }

        #endregion

        #region Color Modes

        abstract class ColorMode
        {
            public abstract Color ComputeColor();
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

            public override Color ComputeColor()
            {
                return _color;
            }
        }

        /// <summary>
        /// To ensure the outline is visible, performs a color inversion of the
        /// background. This makes the focus border more salient, regardless of
        /// the color in the background.
        /// </summary>
        sealed class InvertColorMode : ColorMode
        {
            public override Color ComputeColor()
            {
                //TODO
                return Color.Transparent;
            }
        }

        #endregion
    }
}
