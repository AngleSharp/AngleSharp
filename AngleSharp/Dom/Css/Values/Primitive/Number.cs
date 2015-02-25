namespace AngleSharp.Dom.Css
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Represents a float value.
    /// </summary>
    public struct Number : IEquatable<Number>, IComparable<Number>, IFormattable, ICssValue
    {
        #region Basic numbers

        /// <summary>
        /// Gets a zero value.
        /// </summary>
        public static readonly Number Zero = new Number();

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

        #region Casts and Operators

        /// <summary>
        /// Compares two numbers for equality.
        /// </summary>
        /// <param name="a">The first number.</param>
        /// <param name="b">The second number.</param>
        /// <returns>True if both numbers are equal, otherwise false.</returns>
        public static Boolean operator ==(Number a, Number b)
        {
            return a._value == b._value;
        }

        /// <summary>
        /// Compares two numbers for inequality.
        /// </summary>
        /// <param name="a">The first number.</param>
        /// <param name="b">The second number.</param>
        /// <returns>True if both numbers are not equal, otherwise false.</returns>
        public static Boolean operator !=(Number a, Number b)
        {
            return a._value != b._value;
        }

        /// <summary>
        /// Compares two numbers if the first one is greater or equal to the second one.
        /// </summary>
        /// <param name="a">The first number.</param>
        /// <param name="b">The second number.</param>
        /// <returns>True if the first number is greater or equal, otherwise false.</returns>
        public static Boolean operator >=(Number a, Number b)
        {
            return a._value >= b._value;
        }

        /// <summary>
        /// Compares two numbers if the first one is greater than the second one.
        /// </summary>
        /// <param name="a">The first number.</param>
        /// <param name="b">The second number.</param>
        /// <returns>True if the first number is greater, otherwise false.</returns>
        public static Boolean operator >(Number a, Number b)
        {
            return a._value > b._value;
        }

        /// <summary>
        /// Compares two numbers if the second one is greater or equal to the second one.
        /// </summary>
        /// <param name="a">The first number.</param>
        /// <param name="b">The second number.</param>
        /// <returns>True if the second number is greater or equal, otherwise false.</returns>
        public static Boolean operator <=(Number a, Number b)
        {
            return a._value <= b._value;
        }

        /// <summary>
        /// Compares two numbers if the first one is smaller than the second one.
        /// </summary>
        /// <param name="a">The first number.</param>
        /// <param name="b">The second number.</param>
        /// <returns>True if the first number is smaller, otherwise false.</returns>
        public static Boolean operator <(Number a, Number b)
        {
            return a._value < b._value;
        }

        /// <summary>
        /// Converts the number to a floating point value.
        /// </summary>
        /// <param name="number">The number to convert.</param>
        /// <returns>The floating point value of the number.</returns>
        public static explicit operator Single(Number number)
        {
            return number._value;
        }

        /// <summary>
        /// Converts the number to an integer value.
        /// </summary>
        /// <param name="number">The number to convert.</param>
        /// <returns>The integer value of the number.</returns>
        public static explicit operator Int32(Number number)
        {
            return (Int32)number._value;
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
        public enum Unit : ushort
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
        /// Compares the current number against the given one.
        /// </summary>
        /// <param name="other">The number to compare to.</param>
        /// <returns>The result of the comparison.</returns>
        public Int32 CompareTo(Number other)
        {
            return _value.CompareTo(other._value);
        }

        /// <summary>
        /// Tests if another object is equal to this object.
        /// </summary>
        /// <param name="obj">The object to test with.</param>
        /// <returns>True if the two objects are equal, otherwise false.</returns>
        public override Boolean Equals(Object obj)
        {
            if (obj is Number)
                return this.Equals((Number)obj);

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

        #region CSS Value

        CssValueType ICssValue.Type
        {
            get { return CssValueType.Primitive; }
        }

        String ICssValue.CssText
        {
            get { return _value.ToString(CultureInfo.InvariantCulture); }
        }

        #endregion
    }
}
