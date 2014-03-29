namespace AngleSharp.DOM.Css
{
    using System;
    using System.Globalization;

    sealed class CSSLength : CSSPrimitiveValue
    {
        #region Fields

        Single _value;

        #endregion

        #region ctor

        CSSLength(Single value, CssUnit unit)
            : base(unit)
        {
            _text = value.ToString(CultureInfo.InvariantCulture) + unit.ToCssString();
            _value = value;
        }

        #endregion

        #region Static Methods

        public static CSSValue FromString(Single value, String unit)
        {
            return new CSSLength(value, unit.ToCssUnit());
        }

        public static CSSLength Pixel(Single value)
        {
            return new CSSLength(value, CssUnit.Px);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value of the CSS length.
        /// </summary>
        public Single Value
        {
            get { return _value; }
        }

        #endregion
    }
}
