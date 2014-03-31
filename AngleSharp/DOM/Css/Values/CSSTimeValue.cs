namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents a time in CSS.
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/time
    /// </summary>
    sealed class CSSTimeValue : CSSPrimitiveValue
    {
        #region Fields

        Time _value;

        #endregion

        #region ctor

        public CSSTimeValue(Time value)
        {
            _text = value.ToString();
            _value = value;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value of the CSS time.
        /// </summary>
        public Time Time
        {
            get { return _value; }
        }

        #endregion
    }
}
