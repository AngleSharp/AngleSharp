namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Represents an string in CSS.
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/string
    /// </summary>
    sealed class CSSStringValue : CSSPrimitiveValue
    {
        #region Fields

        String _value;

        #endregion

        #region ctor

        public CSSStringValue(String value)
        {
            _text = String.Concat("'", value, "'");
            _value = value;

        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value.
        /// </summary>
        public String Value
        {
            get { return _value; }
        }

        #endregion
    }
}
