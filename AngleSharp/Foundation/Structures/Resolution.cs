namespace AngleSharp.DOM
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Represents a resolution value.
    /// </summary>
    public struct Resolution : IEquatable<Resolution>, ICssObject
    {
        #region Fields

        Single _value;
        Unit _unit;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new resolution value.
        /// </summary>
        /// <param name="value">The value of the resolution.</param>
        /// <param name="unit">The unit of the resolution.</param>
        public Resolution(Single value, Unit unit)
        {
            _value = value;
            _unit = unit;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value of resolution.
        /// </summary>
        public Single Value
        {
            get { return _value; }
        }

        #endregion

        #region Methods

        public Boolean Equals(Resolution other)
        {
            return _value == other._value && _unit == other._unit;
        }

        #endregion

        #region Units

        public enum Unit
        {
            /// <summary>
            /// The value is a resolution (dots per in).
            /// </summary>
            Dpi,
            /// <summary>
            /// The value is a resolution (dots per cm).
            /// </summary>
            Dpcm,
            /// <summary>
            /// The value is a resolution (dots per px).
            /// </summary>
            Dppx,
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
            if (obj is Resolution)
                return this.Equals((Resolution)obj);

            return false;
        }

        /// <summary>
        /// Returns a hash code that defines the current resolution.
        /// </summary>
        /// <returns>The integer value of the hashcode.</returns>
        public override Int32 GetHashCode()
        {
            return (Int32)_value;
        }

        #endregion

        #region String Representation

        /// <summary>
        /// Returns a string representing the resolution.
        /// </summary>
        /// <returns>The unit string.</returns>
        public override String ToString()
        {
            return String.Concat(_value.ToString(), _unit.ToString().ToLower());
        }

        /// <summary>
        /// Returns a CSS representation of the resolution.
        /// </summary>
        /// <returns>The CSS value string.</returns>
        public String ToCss()
        {
            return String.Concat(_value.ToString(CultureInfo.InvariantCulture), _unit.ToString().ToLower());
        }

        #endregion
    }
}
