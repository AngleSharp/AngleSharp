namespace AngleSharp.DOM.Css
{
    using System;
    using System.Globalization;

    /// <summary>
    /// More information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/number
    /// </summary>
    sealed class CSSNumberValue : CSSPrimitiveValue
    {
        #region Fields

        public static readonly CSSNumberValue Zero = new CSSNumberValue(0f);
        Single _value;

        #endregion

        #region ctor

        public CSSNumberValue(Single value)
        {
            _text = value.ToString(CultureInfo.InvariantCulture);
            _value = value;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the status of the number. Can it be seen
        /// as a positive integer?
        /// </summary>
        public Boolean IsInteger
        {
            get { return _value >= 0 && Math.Floor(_value) == _value; }
        }

        /// <summary>
        /// Gets the value of the CSS number.
        /// </summary>
        public Single Value
        {
            get { return _value; }
        }

        #endregion
    }
}
