using System;

namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents a CSS primitive value.
    /// </summary>
    public sealed class CSSPrimitiveValue : CSSValue
    {
        #region ctor

        internal CSSPrimitiveValue()
        {
        }

        internal CSSPrimitiveValue(UnitType unitType, String value)
        {
            SetStringValue(unitType, value);
        }

        internal CSSPrimitiveValue(UnitType unitType, Single value)
        {
            SetFloatValue(unitType, value);
        }

        internal CSSPrimitiveValue(String unit, Single value)
        {
            var unitType = ConvertStringToUnitType(unit);
            SetFloatValue(unitType, value);
        }

        #endregion

        #region Properties 

        /// <summary>
        /// Gets the type of the value as defined by the CssValue constants.
        /// </summary>
        public CssValue PrimitiveType
        {
            get;
            private set;
        }

        #endregion

        #region Methods

        internal static UnitType ConvertStringToUnitType(String unit)
        {
            switch (unit)
            {
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

        public CSSPrimitiveValue SetFloatValue(UnitType unitType, Single value)
        {
            //TODO
            return this;
        }

        public Single GetFloatValue(UnitType unitType)
        {
            //TODO
            return 0f;
        }

        public CSSPrimitiveValue SetStringValue(UnitType unitType, String value)
        {
            //TODO
            return this;
        }

        public String GetStringValue()
        {
            //TODO
            return string.Empty;
        }

        public Counter GetCounterValue()
        {
            //TODO
            throw new NotImplementedException();
        }

        public Rect GetRectValue()
        {
            //TODO
            throw new NotImplementedException();
        }

        public RGBColor GetRGBColorValue()
        {
            //TODO
            throw new NotImplementedException();
        }

        #endregion
    }
}
