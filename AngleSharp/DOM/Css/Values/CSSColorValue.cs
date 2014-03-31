namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Represents a color in CSS.
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/color_value
    /// </summary>
    sealed class CSSColorValue : CSSPrimitiveValue
    {
        #region Fields

        Color _value;

        #endregion

        #region ctor

        public CSSColorValue(Color value)
        {
            _text = value.ToString();
            _value = value;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value of the CSS color.
        /// </summary>
        public Color Color
        {
            get { return _value; }
        }

        #endregion
    }
}
