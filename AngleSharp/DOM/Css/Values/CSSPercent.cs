namespace AngleSharp.DOM.Css
{
    using System;
    using System.Globalization;

    sealed class CSSPercent : CSSPrimitiveValue
    {
        #region Fields

        Single _value;

        #endregion

        #region ctor

        public CSSPercent(Single value)
            : base(CssUnit.Percentage)
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
