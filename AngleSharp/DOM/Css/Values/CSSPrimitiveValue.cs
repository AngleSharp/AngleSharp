using System;
using System.Globalization;

namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents a CSS primitive value.
    /// </summary>
    sealed class CSSPrimitiveValue : CSSValue
    {
        #region Members

        Object data;
        UnitType unit;

        #endregion

        #region ctor

        internal CSSPrimitiveValue(UnitType unitType, String value)
        {
            _type = CssValue.PrimitiveValue;
            SetStringValue(unitType, value);
        }

        internal CSSPrimitiveValue(UnitType unitType, Single value)
        {
            _type = CssValue.PrimitiveValue;
            SetFloatValue(unitType, value);
        }

        internal CSSPrimitiveValue(String unit, Single value)
        {
            _type = CssValue.PrimitiveValue;
            var unitType = ConvertStringToUnitType(unit);
            SetFloatValue(unitType, value);
        }

        internal CSSPrimitiveValue(HtmlColor value)
        {
            _text = value.ToCss();
            _type = CssValue.PrimitiveValue;
            unit = UnitType.Rgbcolor;
            data = value;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the unit type of the value.
        /// </summary>
        public UnitType PrimitiveType
        {
            get { return unit; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the primitive value to the given number.
        /// </summary>
        /// <param name="unitType">The unit of the number.</param>
        /// <param name="value">The value of the number.</param>
        /// <returns>The CSS primitive value instance.</returns>
        public CSSPrimitiveValue SetFloatValue(UnitType unitType, Single value)
        {
            _text = value.ToString(CultureInfo.InvariantCulture) + ConvertUnitTypeToString(unitType);
            unit = unitType;
            data = value;
            return this;
        }

        /// <summary>
        /// Gets the primitive value's number if any.
        /// </summary>
        /// <param name="unitType">The unit of the number.</param>
        /// <returns>The value of the number if any.</returns>
        public Single? GetFloatValue(UnitType unitType)
        {
            if (data is Single)
            {
                var value = (Single)data;
                //TODO Convert
                return value;
            }

            return null;
        }

        /// <summary>
        /// Sets the primitive value to the given string.
        /// </summary>
        /// <param name="unitType">The unit of the string.</param>
        /// <param name="value">The value of the string.</param>
        /// <returns>The CSS primitive value instance.</returns>
        public CSSPrimitiveValue SetStringValue(UnitType unitType, String value)
        {
            switch (unitType)
            {
                case UnitType.String:
                    _text = "'" + value + "'";
                    break;
                case UnitType.Uri:
                    _text = "url('" + value + "')";
                    break;
                default:
                    _text = value;
                    break;
            }

            unit = unitType;
            data = value;
            return this;
        }

        /// <summary>
        /// Gets the primitive value's string if any.
        /// </summary>
        /// <returns>The value of the string if any.</returns>
        public String GetStringValue()
        {
            if (data is String)
            {
                var value = (String)data;
                //TODO Convert
                return value;
            }

            return null;
        }

        /// <summary>
        /// Gets the primitive value's counter if any.
        /// </summary>
        /// <returns>The value of the counter if any.</returns>
        public Counter GetCounterValue()
        {
            return data as Counter;
        }

        /// <summary>
        /// Gets the primitive value's rectangle if any.
        /// </summary>
        /// <returns>The value of the rectangle if any.</returns>
        public Rect GetRectValue()
        {
            return data as Rect;
        }

        /// <summary>
        /// Gets the primitive value's RGB color if any.
        /// </summary>
        /// <returns>The value of the RGB color if any.</returns>
        public HtmlColor? GetRGBColorValue()
        {
            if(unit == UnitType.Rgbcolor)
                return (HtmlColor)data;

            return null;
        }

        #endregion

        #region Helpers

        internal static UnitType ConvertStringToUnitType(String unit)
        {
            switch (unit)
            {
                case "%": return UnitType.Percentage;
                case "em": return UnitType.Ems;
                case "cm": return UnitType.Cm;
                case "deg": return UnitType.Deg;
                case "grad": return UnitType.Grad;
                case "rad": return UnitType.Rad;
                case "turn": return UnitType.Turn;
                case "ex": return UnitType.Exs;
                case "hz": return UnitType.Hz;
                case "in": return UnitType.In;
                case "khz": return UnitType.Khz;
                case "mm": return UnitType.Mm;
                case "ms": return UnitType.Ms;
                case "s": return UnitType.S;
                case "pc": return UnitType.Pc;
                case "pt": return UnitType.Pt;
                case "px": return UnitType.Px;
                case "vw": return UnitType.Vw;
                case "vh": return UnitType.Vh;
                case "vmin": return UnitType.Vmin;
                case "vmax": return UnitType.Vmax;
            }

            return UnitType.Unknown;
        }

        internal static String ConvertUnitTypeToString(UnitType unit)
        {
            switch (unit)
            {
                case UnitType.Percentage: return "%";
                case UnitType.Ems: return "em";
                case UnitType.Cm: return "cm";
                case UnitType.Deg: return "deg";
                case UnitType.Grad: return "grad";
                case UnitType.Rad: return "rad";
                case UnitType.Turn: return "turn";
                case UnitType.Exs: return "ex";
                case UnitType.Hz: return "hz";
                case UnitType.In: return "in";
                case UnitType.Khz: return "khz";
                case UnitType.Mm: return "mm";
                case UnitType.Ms: return "ms";
                case UnitType.S: return "s";
                case UnitType.Pc: return "pc";
                case UnitType.Pt: return "pt";
                case UnitType.Px: return "px";
                case UnitType.Vw: return "vw";
                case UnitType.Vh: return "vh";
                case UnitType.Vmin: return "vmin";
                case UnitType.Vmax: return "vmax";
            }

            return String.Empty;
        }

        #endregion
    }
}
