namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background-color
    /// </summary>
    sealed class CSSBackgroundColorProperty : CSSProperty
    {
        #region Fields

        Color _color;

        #endregion

        #region ctor

        public CSSBackgroundColorProperty()
            : base(PropertyNames.BackgroundColor)
        {
            _color = Color.Transparent;
            _inherited = false;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the color of the background.
        /// </summary>
        /// <returns></returns>
        public Color Color
        {
            get { return _color; }
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            var color = value.ToColor();

            if (color.HasValue)
                _color = color.Value;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
