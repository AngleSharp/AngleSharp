namespace AngleSharp
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Represents a time value.
    /// </summary>
    public struct Time : IEquatable<Time>, ICssObject
    {
        #region Fields

        /// <summary>
        /// Gets the zero time.
        /// </summary>
        public static readonly Time Zero = new Time(0f, Unit.S);

        Single _value;
        Unit _unit;

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
        /// Gets the value of time in ms.
        /// </summary>
        public Single Value
        {
            get { return _unit == Unit.S ? _value * 1000f : _value; }
        }

        #endregion

        #region Casts

        /// <summary>
        /// Converts the time to the number of milliseconds.
        /// </summary>
        /// <param name="time">The time to convert.</param>
        /// <returns>The number of milliseconds.</returns>
        public static explicit operator Single(Time time)
        {
            return time.Value;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the current time is equal to the other time.
        /// </summary>
        /// <param name="other">The time to compare to.</param>
        /// <returns>True if both represent the same value.</returns>
        public Boolean Equals(Time other)
        {
            return Value == other.Value;
        }

        #endregion

        #region Units

        /// <summary>
        /// An enumeration of time units.
        /// </summary>
        public enum Unit : ushort
        {
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
        /// Returns a hash code that defines the current time.
        /// </summary>
        /// <returns>The integer value of the hashcode.</returns>
        public override Int32 GetHashCode()
        {
            return (Int32)_value;
        }

        #endregion

        #region String Representation

        /// <summary>
        /// Returns a string representing the time.
        /// </summary>
        /// <returns>The unit string.</returns>
        public override String ToString()
        {
            return String.Concat(_value.ToString(), _unit.ToString().ToLower());
        }

        /// <summary>
        /// Returns a CSS representation of the time.
        /// </summary>
        /// <returns>The CSS value string.</returns>
        public String ToCss()
        {
            return String.Concat(_value.ToString(CultureInfo.InvariantCulture), _unit.ToString().ToLower());
        }

        #endregion
    }
}
