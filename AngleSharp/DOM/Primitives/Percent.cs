namespace AngleSharp.DOM
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Represents a percentage value.
    /// </summary>
    public struct Percent : IEquatable<Percent>, ICssObject
    {
        #region Fields

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

        Single _value;

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
        /// Gets the value (0 to 1).
        /// </summary>
        public Single Value
        {
            get { return _value * 0.01f; }
        }

        #endregion

        #region Casts

        public static explicit operator Single(Percent number)
        {
            return number.Value;
        }

        public static explicit operator Int32(Percent number)
        {
            return (Int32)number._value;
        }

        #endregion

        #region Methods

        public Boolean Equals(Percent other)
        {
            return _value == other._value;
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
            return (Int32)_value;
        }

        #endregion

        #region String Representation

        /// <summary>
        /// Returns a string representing the percentage.
        /// </summary>
        /// <returns>The string.</returns>
        public override String ToString()
        {
            return Value.ToString();
        }

        /// <summary>
        /// Returns a CSS representation of the percentage.
        /// </summary>
        /// <returns>The CSS value string.</returns>
        public String ToCss()
        {
            return String.Concat(_value.ToString(CultureInfo.InvariantCulture), "%");
        }

        #endregion
    }
}
