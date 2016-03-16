namespace AngleSharp.Css.Values
{
    using System;

    /// <summary>
    /// Represents a percentage value.
    /// </summary>
    public struct Percent : IEquatable<Percent>, IComparable<Percent>, IFormattable
    {
        #region Basic values

        /// <summary>
        /// Gets a zero percent value.
        /// </summary>
        public static readonly Percent Zero = new Percent(0f);

        /// <summary>
        /// Gets a fifty percent value.
        /// </summary>
        public static readonly Percent Fifty = new Percent(50f);

        /// <summary>
        /// Gets a hundred percent value.
        /// </summary>
        public static readonly Percent Hundred = new Percent(100f);

        #endregion

        #region Fields

        readonly Single _value;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new percentage value.
        /// </summary>
        /// <param name="value">The value in percent (0 to 100).</param>
        public Percent(Single value)
        {
            _value = value;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the normalized value (0 to 1).
        /// </summary>
        public Single NormalizedValue
        {
            get { return _value * 0.01f; }
        }

        /// <summary>
        /// Gets the usual value (0 to 100).
        /// </summary>
        public Single Value
        {
            get { return _value; }
        }

        #endregion

        #region Comparison

        /// <summary>
        /// Compares the magnitude of two percents.
        /// </summary>
        public static Boolean operator >=(Percent a, Percent b)
        {
            return a._value >= b._value;
        }

        /// <summary>
        /// Compares the magnitude of two percents.
        /// </summary>
        public static Boolean operator >(Percent a, Percent b)
        {
            return a._value > b._value;
        }

        /// <summary>
        /// Compares the magnitude of two percents.
        /// </summary>
        public static Boolean operator <=(Percent a, Percent b)
        {
            return a._value <= b._value;
        }

        /// <summary>
        /// Compares the magnitude of two percents.
        /// </summary>
        public static Boolean operator <(Percent a, Percent b)
        {
            return a._value < b._value;
        }

        /// <summary>
        /// Compares the current percentage against the given one.
        /// </summary>
        /// <param name="other">The percentage to compare to.</param>
        /// <returns>The result of the comparison.</returns>
        public Int32 CompareTo(Percent other)
        {
            return _value.CompareTo(other._value);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the given percent value is equal to the current one.
        /// </summary>
        /// <param name="other">The other percent value.</param>
        /// <returns>True if both have the same value.</returns>
        public Boolean Equals(Percent other)
        {
            return _value == other._value;
        }

        #endregion

        #region Equality

        /// <summary>
        /// Checks for equality of two percents.
        /// </summary>
        public static Boolean operator ==(Percent a, Percent b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// Checks for inequality of two percents.
        /// </summary>
        public static Boolean operator !=(Percent a, Percent b)
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
            var other = obj as Percent?;

            if (other != null)
            {
                return Equals(other.Value);
            }

            return false;
        }

        /// <summary>
        /// Returns a hash code that defines the current percentage.
        /// </summary>
        /// <returns>The integer value of the hashcode.</returns>
        public override Int32 GetHashCode()
        {
            return _value.GetHashCode();
        }

        #endregion

        #region String Representation

        /// <summary>
        /// Returns a string representing the percentage.
        /// </summary>
        /// <returns>The string.</returns>
        public override String ToString()
        {
            return _value.ToString() + "%";
        }

        /// <summary>
        /// Returns a formatted string representing the percentage.
        /// </summary>
        /// <param name="format">The format of the number.</param>
        /// <param name="formatProvider">The provider to use.</param>
        /// <returns>The unit string.</returns>
        public String ToString(String format, IFormatProvider formatProvider)
        {
            return _value.ToString(format, formatProvider) + "%";
        }

        #endregion
    }
}
