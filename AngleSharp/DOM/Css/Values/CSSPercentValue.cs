namespace AngleSharp.DOM.Css
{
    using System;
    using System.Globalization;

    /// <summary>
    /// More information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/percentage
    /// </summary>
    sealed class CSSPercentValue : CSSPrimitiveValue
    {
        #region Fields

        Single _value;

        #endregion

        #region ctor

        public CSSPercentValue(Single value)
        {
            _text = value.ToString(CultureInfo.InvariantCulture) + "%";
            _value = value;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value of the CSS percentage.
        /// </summary>
        public Single Value
        {
            get { return _value; }
        }

        #endregion
    }
}
