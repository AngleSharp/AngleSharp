namespace AngleSharp.Css.Values
{
    using System;

    /// <summary>
    /// Represents a float value.
    /// </summary>
    public struct Number : IEquatable<Number>, IComparable<Number>, IFormattable
    {
        #region Basic numbers

        /// <summary>
        /// Gets a zero value.
        /// </summary>
        public static readonly Number Zero = new Number(0f, Unit.Integer);

        /// <summary>
        /// Gets the positive infinite value.
        /// </summary>
        public static readonly Number Infinite = new Number(Single.PositiveInfinity, Unit.Float);

        /// <summary>
        /// Gets the neutral element.
        /// </summary>
        public static readonly Number One = new Number(1f, Unit.Integer);

        #endregion

        #region Fields

        readonly Single _value;
        readonly Unit _unit;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new number value.
        /// </summary>
        /// <param name="value">The value of the number.</param>
        /// <param name="unit">The kind of number.</param>
        public Number(Single value, Unit unit)
        {
            _value = value;
            _unit = unit;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value.
        /// </summary>
        public Single Value
        {
            get { return _value; }
        }

        /// <summary>
        /// Gets if the stored value is an integer number.
        /// </summary>
        public Boolean IsInteger
        {
            get { return _unit == Unit.Integer; }
        }

        #endregion

        #region Comparison

        /// <summary>
        /// Compares the magnitude of two numbers.
        /// </summary>
        public static Boolean operator >=(Number a, Number b)
        {
            return a._value >= b._value;
        }

        /// <summary>
        /// Compares the magnitude of two numbers.
        /// </summary>
        public static Boolean operator >(Number a, Number b)
        {
            return a._value > b._value;
        }

        /// <summary>
        /// Compares the magnitude of two numbers.
        /// </summary>
        public static Boolean operator <=(Number a, Number b)
        {
            return a._value <= b._value;
        }

        /// <summary>
        /// Compares the magnitude of two numbers.
        /// </summary>
        public static Boolean operator <(Number a, Number b)
        {
            return a._value < b._value;
        }

        /// <summary>
        /// Compares the current number against the given one.
        /// </summary>
        /// <param name="other">The number to compare to.</param>
        /// <returns>The result of the comparison.</returns>
        public Int32 CompareTo(Number other)
        {
            return _value.CompareTo(other._value);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks for equality with a given number.
        /// </summary>
        /// <param name="other">The number to compare to.</param>
        /// <returns>True if both numbers are equal, otherwise false.</returns>
        public Boolean Equals(Number other)
        {
            return _value == other._value && _unit == other._unit;
        }

        #endregion

        #region Units

        /// <summary>
        /// An enumeration of angle representations.
        /// </summary>
        public enum Unit : byte
        {
            /// <summary>
            /// The value has been given as an integer.
            /// </summary>
            Integer,
            /// <summary>
            /// The value has been given in a floating point notation.
            /// </summary>
            Float,
        }

        #endregion

        #region Equality

        /// <summary>
        /// Checks for equality of two numbers.
        /// </summary>
        public static Boolean operator ==(Number a, Number b)
        {
            return a._value == b._value;
        }

        /// <summary>
        /// Checks for inequality of two numbers.
        /// </summary>
        public static Boolean operator !=(Number a, Number b)
        {
            return a._value != b._value;
        }

        /// <summary>
        /// Tests if another object is equal to this object.
        /// </summary>
        /// <param name="obj">The object to test with.</param>
        /// <returns>True if the two objects are equal, otherwise false.</returns>
        public override Boolean Equals(Object obj)
        {
            var other = obj as Number?;

            if (other != null)
            {
                return Equals(other.Value);
            }

            return false;
        }

        /// <summary>
        /// Returns a hash code that defines the current number.
        /// </summary>
        /// <returns>The integer value of the hashcode.</returns>
        public override Int32 GetHashCode()
        {
            return _value.GetHashCode();
        }

        #endregion

        #region String Representation

        /// <summary>
        /// Returns a string representing the number.
        /// </summary>
        /// <returns>The string.</returns>
        public override String ToString()
        {
            return _value.ToString();
        }

        /// <summary>
        /// Returns a formatted string representing the number.
        /// </summary>
        /// <param name="format">The format of the number.</param>
        /// <param name="formatProvider">The provider to use.</param>
        /// <returns>The unit string.</returns>
        public String ToString(String format, IFormatProvider formatProvider)
        {
            return _value.ToString(format, formatProvider);
        }

        #endregion
    }
}
