namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents a resolution in CSS.
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/resolution
    /// </summary>
    sealed class CSSResolutionValue : CSSPrimitiveValue
    {
        #region Fields

        Resolution _value;

        #endregion

        #region ctor

        public CSSResolutionValue(Resolution value)
        {
            _text = value.ToString();
            _value = value;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value of the CSS resolution.
        /// </summary>
        public Resolution Resolution
        {
            get { return _value; }
        }

        #endregion
    }
}
