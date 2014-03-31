namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents a length in CSS.
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/frequency
    /// </summary>
    sealed class CSSFrequencyValue : CSSPrimitiveValue
    {
        #region Fields

        Frequency _value;

        #endregion

        #region ctor

        public CSSFrequencyValue(Frequency value)
        {
            _text = value.ToString();
            _value = value;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value of the CSS frequency.
        /// </summary>
        public Frequency Frequency
        {
            get { return _value; }
        }

        #endregion
    }
}
