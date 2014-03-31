namespace AngleSharp.DOM.Css
{
    using System;
    using System.Globalization;

    /// <summary>
    /// More information available
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/length
    /// </summary>
    class CSSUnitValue : CSSPrimitiveValue
    {
        #region Fields

        Single _value;
        CssUnit _unit;

        #endregion

        #region ctor

        CSSUnitValue(Single value, CssUnit unit)
        {
            _text = value.ToString(CultureInfo.InvariantCulture) + unit.ToCssString();
            _value = value;
            _unit = unit;
        }

        #endregion

        #region Static Methods

        public static CSSUnitValue FromUnit(Single value, CssUnit unit)
        {
            switch (unit)
            {
                case CssUnit.Px:
                case CssUnit.Cm:
                case CssUnit.Mm:
                case CssUnit.In:
                case CssUnit.Pt:
                case CssUnit.Vh:
                case CssUnit.Vw:
                case CssUnit.Vmin:
                case CssUnit.Vmax:
                case CssUnit.Exs:
                case CssUnit.Ems:
                case CssUnit.Pc:
                case CssUnit.Ch:
                case CssUnit.Rems:
                    return new Length(value, unit);

                case CssUnit.Deg:
                case CssUnit.Grad:
                case CssUnit.Rad:
                case CssUnit.Turn:
                    return new Angle(value, unit);

                case CssUnit.S:
                case CssUnit.Ms:
                    return new Time(value, unit);

                case CssUnit.Dpi:
                case CssUnit.Dpcm:
                case CssUnit.Dppx:
                    return new Resolution(value, unit);

                case CssUnit.Hz:
                case CssUnit.Khz:
                    return new Frequency(value, unit);
            }

            return new CSSUnitValue(value, unit);
        }

        public static CSSUnitValue FromString(Single value, String unit)
        {
            return FromUnit(value, unit.ToCssUnit());
        }

        #endregion

        #region Properties

        #region Properties

        /// <summary>
        /// Gets the unit type of the value.
        /// </summary>
        public CssUnit Unit
        {
            get { return _unit; }
        }

        #endregion

        /// <summary>
        /// Gets the value of the CSS length.
        /// </summary>
        public Single Value
        {
            get { return _value; }
        }

        #endregion

        #region Nested

        /// <summary>
        /// Represents a length:
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/length
        /// </summary>
        public class Length : CSSUnitValue
        {
            public Length(Single value, CssUnit unit)
                : base(value, unit)
            {
            }
        }

        /// <summary>
        /// Represents a resolution:
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/resolution
        /// </summary>
        public class Resolution : CSSUnitValue
        {
            public Resolution(Single value, CssUnit unit)
                : base(value, unit)
            {
            }
        }

        /// <summary>
        /// Represents a time:
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/time
        /// </summary>
        public class Time : CSSUnitValue
        {
            public Time(Single value, CssUnit unit)
                : base(value, unit)
            {
            }
        }

        /// <summary>
        /// Represents an angle:
        /// https://developer.mozilla.org/en-US/docs/Web/CSS/angle
        /// </summary>
        public class Angle : CSSUnitValue
        {
            public Angle(Single value, CssUnit unit)
                : base(value, unit)
            {
            }
        }

        /// <summary>
        /// Represents a frequency.
        /// </summary>
        public class Frequency : CSSUnitValue
        {
            public Frequency(Single value, CssUnit unit)
                : base(value, unit)
            {
            }
        }

        #endregion
    }
}
