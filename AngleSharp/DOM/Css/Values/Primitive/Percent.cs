namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using System;
    using System.Globalization;

    /// <summary>
    /// Represents a percentage value.
    /// </summary>
    public struct Percent : IEquatable<Percent>, IComparable<Percent>, IFormattable, ICssValue
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

        #region Casts

        /// <summary>
        /// Converts the percent value to its probability representation.
        /// </summary>
        /// <param name="number">The percent number to convert.</param>
        /// <returns>The number between 0 and 1.</returns>
        public static explicit operator Single(Percent number)
        {
            return number.NormalizedValue;
        }

        /// <summary>
        /// Converts the percent value to its common representation.
        /// </summary>
        /// <param name="number">The percent number to convert.</param>
        /// <returns>The integer between 0 and 100.</returns>
        public static explicit operator Int32(Percent number)
        {
            return (Int32)number._value;
        }

        #endregion

        #region Operators

        /// <summary>
        /// Checks the equality of the two given percentages.
        /// </summary>
        /// <param name="a">The left percentage.</param>
        /// <param name="b">The right percentage.</param>
        /// <returns>True if both percentages are equal, otherwise false.</returns>
        public static Boolean operator ==(Percent a, Percent b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// Checks the inequality of the two given percentages.
        /// </summary>
        /// <param name="a">The left percentage.</param>
        /// <param name="b">The right percentage.</param>
        /// <returns>True if both percentages are not equal, otherwise false.</returns>
        public static Boolean operator !=(Percent a, Percent b)
        {
            return !a.Equals(b);
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
            throw new NotImplementedException();
        }

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
        /// Compares the current percentage against the given one.
        /// </summary>
        /// <param name="other">The percentage to compare to.</param>
        /// <returns>The result of the comparison.</returns>
        public Int32 CompareTo(Percent other)
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
            if (obj is Percent)
                return this.Equals((Percent)obj);

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
            return NormalizedValue.ToString();
        }

        /// <summary>
        /// Returns a formatted string representing the percentage.
        /// </summary>
        /// <param name="format">The format of the number.</param>
        /// <param name="formatProvider">The provider to use.</param>
        /// <returns>The unit string.</returns>
        public String ToString(String format, IFormatProvider formatProvider)
        {
            return NormalizedValue.ToString(format, formatProvider);
        }

        #endregion

        #region CSS Value

        CssValueType ICssValue.Type
        {
            get { return CssValueType.Primitive; }
        }

        String ICssValue.CssText
        {
            get { return String.Concat(_value.ToString(CultureInfo.InvariantCulture), Units.Percent); }
        }

        #endregion
    }
}
