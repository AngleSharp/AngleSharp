namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using System;
    using System.Globalization;

    /// <summary>
    /// Represents a time value.
    /// </summary>
    public struct Frequency : IEquatable<Frequency>, IComparable<Frequency>, IFormattable, ICssValue
    {
        #region Fields

        readonly Single _value;
        readonly Unit _unit;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new frequency value.
        /// </summary>
        /// <param name="value">The value of the frequency.</param>
        /// <param name="unit">The unit of the frequency.</param>
        public Frequency(Single value, Unit unit)
        {
            _value = value;
            _unit = unit;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value of frequency in Hz.
        /// </summary>
        public Single Value
        {
            get { return _unit == Unit.Khz ? _value * 1000f : _value; }
        }

        /// <summary>
        /// Gets the type of the length.
        /// </summary>
        public Unit Type
        {
            get { return _unit; }
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
                    case Unit.Khz:
                        return Units.Khz;

                    case Unit.Hz:
                        return Units.Hz;

                    default:
                        return String.Empty;
                }
            }
        }

        #endregion

        #region Casts

        /// <summary>
        /// Converts the frequency to a single floating point.
        /// </summary>
        /// <param name="frequency">The frequency.</param>
        /// <returns>The float value.</returns>
        public static explicit operator Single(Frequency frequency)
        {
            return frequency.Value;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks for equality with the other frequency.
        /// </summary>
        /// <param name="other">The frequency to compare to.</param>
        /// <returns>True if both frequencies are equal, otherwise false.</returns>
        public Boolean Equals(Frequency other)
        {
            return _value == other._value && _unit == other._unit;
        }

        #endregion

        #region Units

        /// <summary>
        /// The various frequency units.
        /// </summary>
        public enum Unit
        {
            /// <summary>
            /// The value is a frequency (Hz).
            /// </summary>
            Hz,
            /// <summary>
            /// The value is a frequency (kHz).
            /// </summary>
            Khz,
        }

        #endregion

        #region Equality

        /// <summary>
        /// Compares the current frequency against the given one.
        /// </summary>
        /// <param name="other">The frequency to compare to.</param>
        /// <returns>The result of the comparison.</returns>
        public Int32 CompareTo(Frequency other)
        {
            return Value.CompareTo(other.Value);
        }

        /// <summary>
        /// Tests if another object is equal to this object.
        /// </summary>
        /// <param name="obj">The object to test with.</param>
        /// <returns>True if the two objects are equal, otherwise false.</returns>
        public override Boolean Equals(Object obj)
        {
            if (obj is Frequency)
                return this.Equals((Frequency)obj);

            return false;
        }

        /// <summary>
        /// Returns a hash code that defines the current frequency.
        /// </summary>
        /// <returns>The integer value of the hashcode.</returns>
        public override Int32 GetHashCode()
        {
            return _value.GetHashCode();
        }

        #endregion

        #region String Representation

        /// <summary>
        /// Returns a string representing the frequency.
        /// </summary>
        /// <returns>The unit string.</returns>
        public override String ToString()
        {
            return String.Concat(_value.ToString(), UnitString);
        }

        /// <summary>
        /// Returns a formatted string representing the frequency.
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
            get { return String.Concat(_value.ToString(CultureInfo.InvariantCulture), UnitString); }
        }

        #endregion
    }
}
