namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using System;
    using System.Globalization;

    /// <summary>
    /// Represents an absolute length value.
    /// </summary>
    public struct Length : IEquatable<Length>, IComparable<Length>, IFormattable, ICssValue
    {
        #region Basic lengths

        /// <summary>
        /// Gets a zero pixel length value.
        /// </summary>
        public static readonly Length Zero = new Length();

        /// <summary>
        /// Gets the half relative length, i.e. 50%.
        /// </summary>
        public static readonly Length Half = new Length(50f, Unit.Percent);

        /// <summary>
        /// Gets the full relative length, i.e. 100%.
        /// </summary>
        public static readonly Length Full = new Length(100f, Unit.Percent);

        /// <summary>
        /// Gets a thin length value.
        /// </summary>
        public static readonly Length Thin = new Length(1f, Unit.Px);

        /// <summary>
        /// Gets a medium length value.
        /// </summary>
        public static readonly Length Medium = new Length(3f, Unit.Px);

        /// <summary>
        /// Gets a thick length value.
        /// </summary>
        public static readonly Length Thick = new Length(5f, Unit.Px);

        /// <summary>
        /// Gets the missing value.
        /// </summary>
        public static readonly Length Missing = new Length(-1f, Unit.Ch);

        #endregion

        #region Fields

        readonly Single _value;
        readonly Unit _unit;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new length value.
        /// </summary>
        /// <param name="value">The value of the length.</param>
        /// <param name="unit">The unit of the length.</param>
        public Length(Single value, Unit unit)
        {
            _value = value;
            _unit = unit;
        }

        #endregion

        #region Operators

        /// <summary>
        /// Checks the equality of the two given lengths.
        /// </summary>
        /// <param name="a">The left length.</param>
        /// <param name="b">The right length.</param>
        /// <returns>True if both lengths are equal, otherwise false.</returns>
        public static Boolean operator ==(Length a, Length b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// Checks the inequality of the two given lengths.
        /// </summary>
        /// <param name="a">The left length.</param>
        /// <param name="b">The right length.</param>
        /// <returns>True if both lengths are not equal, otherwise false.</returns>
        public static Boolean operator !=(Length a, Length b)
        {
            return !a.Equals(b);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the length is given in absolute units.
        /// Such a length may be converted to pixels.
        /// </summary>
        public Boolean IsAbsolute
        {
            get { return _unit == Unit.In || _unit == Unit.Mm || _unit == Unit.Pc || _unit == Unit.Px || _unit == Unit.Pt || _unit == Unit.Cm; }
        }

        /// <summary>
        /// Gets if the length is given in relative units.
        /// Such a length cannot be converted to pixels.
        /// </summary>
        public Boolean IsRelative
        {
            get { return !IsAbsolute; }
        }

        /// <summary>
        /// Gets the type of the length.
        /// </summary>
        public Unit Type
        {
            get { return _unit; }
        }

        /// <summary>
        /// Gets the value of the length.
        /// </summary>
        public Single Value
        {
            get { return _value; }
        }

        /// <summary>
        /// Gets the representation of the unit as a string.
        /// </summary>
        public String UnitString
        {
            get
            {
                switch (_unit)
                {
                    case Unit.Px: return Units.Px;
                    case Unit.Em: return Units.Em;
                    case Unit.Ex: return Units.Ex;
                    case Unit.Cm: return Units.Cm;
                    case Unit.Mm: return Units.Mm;
                    case Unit.In: return Units.In;
                    case Unit.Pt: return Units.Pt;
                    case Unit.Pc: return Units.Pc;
                    case Unit.Ch: return Units.Ch;
                    case Unit.Rem: return Units.Rem;
                    case Unit.Vw: return Units.Vw;
                    case Unit.Vh: return Units.Vh;
                    case Unit.Vmin: return Units.Vmin;
                    case Unit.Vmax: return Units.Vmax;
                    case Unit.Percent: return Units.Percent;
                    default: return String.Empty;
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Converts the length to a number of pixels, if possible. If the
        /// current unit is relative, then an exception will be thrown.
        /// </summary>
        /// <returns>The number of pixels represented by the current length.</returns>
        public Single ToPixel()
        {
            switch (_unit)
            {
                case Unit.In: // 1 in = 2.54 cm
                    return _value * 96f;
                case Unit.Mm: // 1 mm = 0.1 cm
                    return _value * 5f * 96f / 127f;
                case Unit.Pc: // 1 pc = 12 pt
                    return _value * 12f * 96f / 72f;
                case Unit.Pt: // 1 pt = 1/72 in
                    return _value * 96f / 72f;
                case Unit.Cm: // 1 cm = 50/127 in
                    return _value * 50f * 96f / 127f;
                case Unit.Px: // 1 px = 1/96 in
                    return _value;
                default:
                    throw new InvalidOperationException("A relative unit cannot be converted.");
            }
        }

        /// <summary>
        /// Converts the length to the given unit, if possible. If the current
        /// or given unit is relative, then an exception will be thrown.
        /// </summary>
        /// <param name="unit">The unit to convert to.</param>
        /// <returns>The value in the given unit of the current length.</returns>
        public Single To(Unit unit)
        {
            var value = ToPixel();

            switch (unit)
            {
                case Unit.In: // 1 in = 2.54 cm
                    return value / 96f;
                case Unit.Mm: // 1 mm = 0.1 cm
                    return value * 127f / (5f * 96f);
                case Unit.Pc: // 1 pc = 12 pt
                    return value * 72f / (12f * 96f);
                case Unit.Pt: // 1 pt = 1/72 in
                    return value * 72f / 96f;
                case Unit.Cm: // 1 cm = 50/127 in
                    return value * 127f / (50f * 96f);
                case Unit.Px: // 1 px = 1/96 in
                    return value;
                default:
                    throw new InvalidOperationException("An absolute unit cannot be converted to a relative one.");
            }
        }

        /// <summary>
        /// Checks if both lengths are actually equal.
        /// </summary>
        /// <param name="other">The other length to compare to.</param>
        /// <returns>True if both lengths are equal, otherwise false.</returns>
        public Boolean Equals(Length other)
        {
            return _value == other._value && _unit == other._unit;
        }

        #endregion

        #region Units

        /// <summary>
        /// An enumeration of length units.
        /// </summary>
        public enum Unit : ushort
        {
            /// <summary>
            /// The value is a length (px).
            /// </summary>
            Px,
            /// <summary>
            /// The value is a length (em).
            /// </summary>
            Em,
            /// <summary>
            /// The value is a length (ex). Usually about 0.5em for most fonts.
            /// </summary>
            Ex,
            /// <summary>
            /// The value is a length (cm).
            /// </summary>
            Cm,
            /// <summary>
            /// The value is a length (mm).
            /// </summary>
            Mm,
            /// <summary>
            /// The value is a length (in).
            /// </summary>
            In,
            /// <summary>
            /// The value is a length (pt).
            /// </summary>
            Pt,
            /// <summary>
            /// The value is a length (pc).
            /// </summary>
            Pc,
            /// <summary>
            /// The value is a length (width of the 0-character).
            /// </summary>
            Ch,
            /// <summary>
            /// The value is the relative to the font-size of the root element (in em).
            /// </summary>
            Rem,
            /// <summary>
            /// The value is relative to the viewport width.
            /// 1vw = 1/100 of the viewport width.
            /// </summary>
            Vw,
            /// <summary>
            /// The value is relative to the viewport height.
            /// 1vh = 1/100 of the viewport height.
            /// </summary>
            Vh,
            /// <summary>
            /// The value is relative to the minimum of viewport width and height.
            /// 1vmin = 1/100 of the minimum viewport dimension.
            /// </summary>
            Vmin,
            /// <summary>
            /// The value is relative to the maximum of viewport width and height.
            /// 1vmax = 1/100 of the maximum viewport dimension.
            /// </summary>
            Vmax,
            /// <summary>
            /// The value is relative to a fixed (external) value, that is context
            /// dependent. 1% = 1/100 of the external value.
            /// </summary>
            Percent
        }

        #endregion

        #region Equality

        /// <summary>
        /// Compares the current length against the given one.
        /// </summary>
        /// <param name="other">The length to compare to.</param>
        /// <returns>The result of the comparison.</returns>
        public Int32 CompareTo(Length other)
        {
            if (IsAbsolute && other.IsAbsolute)
                return ToPixel().CompareTo(other.ToPixel());
            else if (_unit == other._unit)
                return _value.CompareTo(other._value);

            return 0;
        }

        /// <summary>
        /// Tests if another object is equal to this object.
        /// </summary>
        /// <param name="obj">The object to test with.</param>
        /// <returns>True if the two objects are equal, otherwise false.</returns>
        public override Boolean Equals(Object obj)
        {
            if (obj is Length)
                return this.Equals((Length)obj);

            return false;
        }

        /// <summary>
        /// Returns a hash code that defines the current length.
        /// </summary>
        /// <returns>The integer value of the hashcode.</returns>
        public override Int32 GetHashCode()
        {
            return _value.GetHashCode();
        }

        #endregion

        #region String Representation

        /// <summary>
        /// Returns a string representing the length.
        /// </summary>
        /// <returns>The unit string.</returns>
        public override String ToString()
        {
            return String.Concat(_value.ToString(), UnitString);
        }

        /// <summary>
        /// Returns a formatted string representing the length.
        /// </summary>
        /// <param name="format">The format of the number.</param>
        /// <param name="formatProvider">The provider to use.</param>
        /// <returns>The unit string.</returns>
        public String ToString(String format, IFormatProvider formatProvider)
        {
            return String.Concat(_value.ToString(format, formatProvider), UnitString);
        }

        #endregion

        #region CSS Value

        CssValueType ICssValue.Type
        {
            get { return CssValueType.Primitive; }
        }

        String ICssValue.CssText
        {
            get
            {
                if (_value == 0f)
                    return _value.ToString(CultureInfo.InvariantCulture);

                return String.Concat(_value.ToString(CultureInfo.InvariantCulture), UnitString);
            }
        }

        #endregion
    }
}
