namespace AngleSharp.DOM.Css
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Represents a CSS primitive value.
    /// </summary>
    sealed class CSSPrimitiveValue : CSSValue
    {
        #region Fields

        Object _value;
        CssUnit _unit;

        #endregion

        #region ctor

        internal CSSPrimitiveValue(CssUnit unit, String value)
        {
            _type = CssValueType.PrimitiveValue;
            SetStringValue(unit, value);
        }

        internal CSSPrimitiveValue(CssUnit unit, Single value)
        {
            _type = CssValueType.PrimitiveValue;
            SetFloatValue(unit, value);
        }

        internal CSSPrimitiveValue(String unit, Single value)
        {
            _type = CssValueType.PrimitiveValue;
            var unitType = ConvertStringToUnitType(unit);
            SetFloatValue(unitType, value);
        }

        internal CSSPrimitiveValue(CSSColor value)
        {
            _text = value.ToCss();
            _type = CssValueType.PrimitiveValue;
            _unit = CssUnit.Rgbcolor;
            _value = value;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the unit type of the value.
        /// </summary>
        public CssUnit PrimitiveType
        {
            get { return _unit; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the primitive value to the given number.
        /// </summary>
        /// <param name="unit">The unit of the number.</param>
        /// <param name="value">The value of the number.</param>
        /// <returns>The CSS primitive value instance.</returns>
        public CSSPrimitiveValue SetFloatValue(CssUnit unit, Single value)
        {
            _text = value.ToString(CultureInfo.InvariantCulture) + ConvertUnitTypeToString(unit);
            _unit = unit;
            _value = value;
            return this;
        }

        /// <summary>
        /// Gets the primitive value's number if any.
        /// </summary>
        /// <param name="unit">The unit of the number.</param>
        /// <returns>The value of the number if any.</returns>
        public Single? GetFloatValue(CssUnit unit)
        {
            if (_value is Single)
            {
                var qty = (Single)_value;
                //TODO Convert

                switch (unit)
                {
                    case CssUnit.Percentage:
                        qty = qty / 100f;
                        break;
                }

                return qty;
            }

            return null;
        }

        /// <summary>
        /// Sets the primitive value to the given string.
        /// </summary>
        /// <param name="unit">The unit of the string.</param>
        /// <param name="value">The value of the string.</param>
        /// <returns>The CSS primitive value instance.</returns>
        public CSSPrimitiveValue SetStringValue(CssUnit unit, String value)
        {
            switch (unit)
            {
                case CssUnit.String:
                    _text = "'" + value + "'";
                    break;
                case CssUnit.Uri:
                    _text = "url('" + value + "')";
                    break;
                default:
                    _text = value;
                    break;
            }

            _unit = unit;
            _value = value;
            return this;
        }

        /// <summary>
        /// Gets the primitive value's string if any.
        /// </summary>
        /// <returns>The value of the string if any.</returns>
        public String GetStringValue()
        {
            if (_value is String)
            {
                var qty = (String)_value;
                //TODO Convert
                return qty;
            }

            return null;
        }

        /// <summary>
        /// Gets the primitive value's counter if any.
        /// </summary>
        /// <returns>The value of the counter if any.</returns>
        public Counter GetCounterValue()
        {
            return _value as Counter;
        }

        /// <summary>
        /// Gets the primitive value's rectangle if any.
        /// </summary>
        /// <returns>The value of the rectangle if any.</returns>
        public CSSRect GetRectValue()
        {
            return _value as CSSRect;
        }

        /// <summary>
        /// Gets the primitive value's RGB color if any.
        /// </summary>
        /// <returns>The value of the RGB color if any.</returns>
        public CSSColor? GetRGBColorValue()
        {
            if(_unit == CssUnit.Rgbcolor)
                return (CSSColor)_value;

            return null;
        }

        #endregion

        #region Helpers

        internal static CssUnit ConvertStringToUnitType(String unit)
        {
            switch (unit)
            {
                case "%": return CssUnit.Percentage;
                case "em": return CssUnit.Ems;
                case "cm": return CssUnit.Cm;
                case "deg": return CssUnit.Deg;
                case "grad": return CssUnit.Grad;
                case "rad": return CssUnit.Rad;
                case "turn": return CssUnit.Turn;
                case "ex": return CssUnit.Exs;
                case "hz": return CssUnit.Hz;
                case "in": return CssUnit.In;
                case "khz": return CssUnit.Khz;
                case "mm": return CssUnit.Mm;
                case "ms": return CssUnit.Ms;
                case "s": return CssUnit.S;
                case "pc": return CssUnit.Pc;
                case "pt": return CssUnit.Pt;
                case "px": return CssUnit.Px;
                case "vw": return CssUnit.Vw;
                case "vh": return CssUnit.Vh;
                case "vmin": return CssUnit.Vmin;
                case "vmax": return CssUnit.Vmax;
            }

            return CssUnit.Unknown;
        }

        internal static String ConvertUnitTypeToString(CssUnit unit)
        {
            switch (unit)
            {
                case CssUnit.Percentage: return "%";
                case CssUnit.Ems: return "em";
                case CssUnit.Cm: return "cm";
                case CssUnit.Deg: return "deg";
                case CssUnit.Grad: return "grad";
                case CssUnit.Rad: return "rad";
                case CssUnit.Turn: return "turn";
                case CssUnit.Exs: return "ex";
                case CssUnit.Hz: return "hz";
                case CssUnit.In: return "in";
                case CssUnit.Khz: return "khz";
                case CssUnit.Mm: return "mm";
                case CssUnit.Ms: return "ms";
                case CssUnit.S: return "s";
                case CssUnit.Pc: return "pc";
                case CssUnit.Pt: return "pt";
                case CssUnit.Px: return "px";
                case CssUnit.Vw: return "vw";
                case CssUnit.Vh: return "vh";
                case CssUnit.Vmin: return "vmin";
                case CssUnit.Vmax: return "vmax";
            }

            return String.Empty;
        }

        #endregion
    }
}
