namespace AngleSharp.DOM.Css
{
    using System;
    using System.Globalization;

    sealed class CSSNumber : CSSValue
    {
        #region Fields

        Single _value;

        #endregion

        #region ctor

        public CSSNumber(Single value)
        {
            _type = CssValueType.PrimitiveValue;
            _text = value.ToString(CultureInfo.InvariantCulture);
            _value = value;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value of the CSS number.
        /// </summary>
        public Single Value
        {
            get { return _value; }
        }

        /// <summary>
        /// Gets the unit type of the value.
        /// </summary>
        public CssUnit PrimitiveType
        {
            get { return CssUnit.Number; }
        }

        #endregion
    }
}
