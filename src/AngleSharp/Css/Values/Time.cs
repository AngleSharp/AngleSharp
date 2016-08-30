namespace AngleSharp.Css.Values
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Represents a time value.
    /// </summary>
    public struct Time : IEquatable<Time>, IComparable<Time>, IFormattable
    {
        #region Basic times

        /// <summary>
        /// Gets the zero time.
        /// </summary>
        public static readonly Time Zero = new Time(0f, Unit.Ms);

        #endregion

        #region Fields

        readonly Single _value;
        readonly Unit _unit;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new time value.
        /// </summary>
        /// <param name="value">The value of the time.</param>
        /// <param name="unit">The unit of the time.</param>
        public Time(Single value, Unit unit)
        {
            _value = value;
            _unit = unit;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value of time.
        /// </summary>
        public Single Value
        {
            get { return _value; }
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
                    case Unit.Ms:
                        return UnitNames.Ms;

                    case Unit.S:
                        return UnitNames.S;

                    default:
                        return String.Empty;
                }
            }
        }

        #endregion

        #region Comparison

        /// <summary>
        /// Compares the magnitude of two times.
        /// </summary>
        public static Boolean operator >=(Time a, Time b)
        {
            var result = a.CompareTo(b);
            return result == 0 || result == 1;
        }

        /// <summary>
        /// Compares the magnitude of two times.
        /// </summary>
        public static Boolean operator >(Time a, Time b)
        {
            return a.CompareTo(b) == 1;
        }

        /// <summary>
        /// Compares the magnitude of two times.
        /// </summary>
        public static Boolean operator <=(Time a, Time b)
        {
            var result = a.CompareTo(b);
            return result == 0 || result == -1;
        }

        /// <summary>
        /// Compares the magnitude of two times.
        /// </summary>
        public static Boolean operator <(Time a, Time b)
        {
            return a.CompareTo(b) == -1;
        }

        /// <summary>
        /// Compares the current time against the given one.
        /// </summary>
        /// <param name="other">The time to compare to.</param>
        /// <returns>The result of the comparison.</returns>
        public Int32 CompareTo(Time other)
        {
            return ToMilliseconds().CompareTo(other.ToMilliseconds());
        }

        #endregion

        #region Methods

        /// <summary>
        /// Tries to convert the given string to a Time.
        /// </summary>
        /// <param name="s">The string to convert.</param>
        /// <param name="result">The reference to the result.</param>
        /// <returns>True if successful, otherwise false.</returns>
        public static Boolean TryParse(String s, out Time result)
        {
            var value = default(Single);
            var unit = GetUnit(s.CssUnit(out value));

            if (unit != Unit.None)
            {
                result = new Time(value, unit);
                return true;
            }

            result = default(Time);
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
                case "s": return Unit.S;
                case "ms": return Unit.Ms;
                default: return Unit.None;
            }
        }

        /// <summary>
        /// Converts the value to milliseconds.
        /// </summary>
        /// <returns>The number of milliseconds.</returns>
        public Single ToMilliseconds()
        {
            return _unit == Unit.S ? _value * 1000f : _value;
        }

        /// <summary>
        /// Checks if the current time is equal to the other time.
        /// </summary>
        /// <param name="other">The time to compare to.</param>
        /// <returns>True if both represent the same value.</returns>
        public Boolean Equals(Time other)
        {
            return ToMilliseconds() == other.ToMilliseconds();
        }

        #endregion

        #region Units

        /// <summary>
        /// An enumeration of time units.
        /// </summary>
        public enum Unit : byte
        {
            /// <summary>
            /// No valid unit.
            /// </summary>
            None,
            /// <summary>
            /// The value is a time (ms).
            /// </summary>
            Ms,
            /// <summary>
            /// The value is a time (s).
            /// </summary>
            S,
        }

        #endregion

        #region Equality

        /// <summary>
        /// Checks for equality of two times.
        /// </summary>
        public static Boolean operator ==(Time a, Time b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// Checks for inequality of two times.
        /// </summary>
        public static Boolean operator !=(Time a, Time b)
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
            var other = obj as Time?;

            if (other != null)
            {
                return Equals(other.Value);
            }

            return false;
        }

        /// <summary>
        /// Returns a hash code that defines the current time.
        /// </summary>
        /// <returns>The integer value of the hashcode.</returns>
        public override Int32 GetHashCode()
        {
            return _value.GetHashCode();
        }

        #endregion

        #region String Representation

        /// <summary>
        /// Returns a string representing the time.
        /// </summary>
        /// <returns>The unit string.</returns>
        public override String ToString()
        {
            return String.Concat(_value.ToString(), UnitString);
        }

        /// <summary>
        /// Returns a formatted string representing the time.
        /// </summary>
        /// <param name="format">The format of the number.</param>
        /// <param name="formatProvider">The provider to use.</param>
        /// <returns>The unit string.</returns>
        public String ToString(String format, IFormatProvider formatProvider)
        {
            return String.Concat(_value.ToString(format, formatProvider), UnitString);
        }

        #endregion
    }
}
