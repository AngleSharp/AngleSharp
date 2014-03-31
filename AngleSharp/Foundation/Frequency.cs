namespace AngleSharp
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Represents a time value.
    /// </summary>
    struct Frequency : IEquatable<Frequency>
    {
        #region Fields

        Single _value;
        Unit _unit;

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
        /// Gets the value of frequency.
        /// </summary>
        public Single Value
        {
            get { return _value; }
        }

        #endregion

        #region Methods

        public Boolean Equals(Frequency other)
        {
            return _value == other._value && _unit == other._unit;
        }

        #endregion

        #region Units

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

        #region Overrides

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
            return (Int32)_value;
        }

        /// <summary>
        /// Returns a string representing the frequency.
        /// </summary>
        /// <returns>The unit string.</returns>
        public override String ToString()
        {
            return String.Concat(_value.ToString(CultureInfo.InvariantCulture), _unit.ToString().ToLower());
        }

        #endregion
    }
}
