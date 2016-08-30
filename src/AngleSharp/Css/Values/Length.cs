namespace AngleSharp.Css.Values
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Represents an absolute length value.
    /// </summary>
    public struct Length : IEquatable<Length>, IComparable<Length>, IFormattable
    {
        #region Basic lengths

        /// <summary>
        /// Gets a zero pixel length value.
        /// </summary>
        public static readonly Length Zero = new Length(0f, Unit.Px);

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
                    case Unit.Px: return UnitNames.Px;
                    case Unit.Em: return UnitNames.Em;
                    case Unit.Ex: return UnitNames.Ex;
                    case Unit.Cm: return UnitNames.Cm;
                    case Unit.Mm: return UnitNames.Mm;
                    case Unit.In: return UnitNames.In;
                    case Unit.Pt: return UnitNames.Pt;
                    case Unit.Pc: return UnitNames.Pc;
                    case Unit.Ch: return UnitNames.Ch;
                    case Unit.Rem: return UnitNames.Rem;
                    case Unit.Vw: return UnitNames.Vw;
                    case Unit.Vh: return UnitNames.Vh;
                    case Unit.Vmin: return UnitNames.Vmin;
                    case Unit.Vmax: return UnitNames.Vmax;
                    case Unit.Percent: return UnitNames.Percent;
                    default: return String.Empty;
                }
            }
        }

        #endregion

        #region Comparison

        /// <summary>
        /// Compares the magnitude of two lengths.
        /// </summary>
        public static Boolean operator >=(Length a, Length b)
        {
            var result = a.CompareTo(b);
            return result == 0 || result == 1;
        }

        /// <summary>
        /// Compares the magnitude of two lengths.
        /// </summary>
        public static Boolean operator >(Length a, Length b)
        {
            return a.CompareTo(b) == 1;
        }

        /// <summary>
        /// Compares the magnitude of two lengths.
        /// </summary>
        public static Boolean operator <=(Length a, Length b)
        {
            var result = a.CompareTo(b);
            return result == 0 || result == -1;
        }

        /// <summary>
        /// Compares the magnitude of two lengths.
        /// </summary>
        public static Boolean operator <(Length a, Length b)
        {
            return a.CompareTo(b) == -1;
        }

        /// <summary>
        /// Compares the current length against the given one.
        /// </summary>
        /// <param name="other">The length to compare to.</param>
        /// <returns>The result of the comparison.</returns>
        public Int32 CompareTo(Length other)
        {
            if (_unit == other._unit)
            {
                return _value.CompareTo(other._value);
            }
            else if (IsAbsolute && other.IsAbsolute)
            {
                return ToPixel().CompareTo(other.ToPixel());
            }

            return 0;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Tries to convert the given string to a Length.
        /// </summary>
        /// <param name="s">The string to convert.</param>
        /// <param name="result">The reference to the result.</param>
        /// <returns>True if successful, otherwise false.</returns>
        public static Boolean TryParse(String s, out Length result)
        {
            var value = default(Single);
            var unitString = s.CssUnit(out value);
            var unit = GetUnit(unitString);

            if (unit != Unit.None)
            {
                result = new Length(value, unit);
                return true;
            }
            else if (value == 0f)
            {
                result = Length.Zero;
                return true;
            }

            result = default(Length);
            return false;
        }

        /// <summary>
        /// Gets the unit from the enumeration for the provided string.
        /// </summary>
        /// <param name="s">The string to convert.</param>
        /// <returns>A valid CSS unit or None.</returns>
        public static Unit GetUnit(String s)
        {
            switch (s)
            {
                case "ch": return Unit.Ch;
                case "cm": return Unit.Cm;
                case "em": return Unit.Em;
                case "ex": return Unit.Ex;
                case "in": return Unit.In;
                case "mm": return Unit.Mm;
                case "pc": return Unit.Pc;
                case "pt": return Unit.Pt;
                case "px": return Unit.Px;
                case "rem": return Unit.Rem;
                case "vh": return Unit.Vh;
                case "vmax": return Unit.Vmax;
                case "vmin": return Unit.Vmin;
                case "vw": return Unit.Vw;
                case "%": return Unit.Percent;
                default: return Unit.None;
            }
        }

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
        public enum Unit : byte
        {
            /// <summary>
            /// No valid unit.
            /// </summary>
            None,
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

        /// <summary>
        /// Tests if another object is equal to this object.
        /// </summary>
        /// <param name="obj">The object to test with.</param>
        /// <returns>True if the two objects are equal, otherwise false.</returns>
        public override Boolean Equals(Object obj)
        {
            var other = obj as Length?;

            if (other != null)
            {
                return Equals(other.Value);
            }

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
            var unit = _value == 0f ? String.Empty : UnitString;
            return String.Concat(_value.ToString(), unit);
        }

        /// <summary>
        /// Returns a formatted string representing the length.
        /// </summary>
        /// <param name="format">The format of the number.</param>
        /// <param name="formatProvider">The provider to use.</param>
        /// <returns>The unit string.</returns>
        public String ToString(String format, IFormatProvider formatProvider)
        {
            var unit = _value == 0f ? String.Empty : UnitString;
            return String.Concat(_value.ToString(format, formatProvider), unit);
        }

        #endregion
    }
}
