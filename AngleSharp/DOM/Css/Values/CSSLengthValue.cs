namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents a length in CSS.
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/length
    /// </summary>
    sealed class CSSLengthValue : CSSPrimitiveValue
    {
        #region Fields

        Length _value;

        #endregion

        #region ctor

        public CSSLengthValue(Length value)
        {
            _text = value.ToString();
            _value = value;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value of the CSS length.
        /// </summary>
        public Length Length
        {
            get { return _value; }
        }

        #endregion
    }
}
