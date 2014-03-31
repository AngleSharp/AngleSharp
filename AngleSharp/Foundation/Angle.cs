namespace AngleSharp
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Represents an angle value.
    /// </summary>
    struct Angle : IEquatable<Angle>
    {
        #region Fields

        Single _value;
        Unit _unit;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new angle value.
        /// </summary>
        /// <param name="value">The value of the angle.</param>
        /// <param name="unit">The unit of the angle.</param>
        public Angle(Single value, Unit unit)
        {
            _value = value;
            _unit = unit;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value of angle.
        /// </summary>
        public Single Value
        {
            get { return _value; }
        }

        #endregion

        #region Methods

        public Boolean Equals(Angle other)
        {
            return _value == other._value && _unit == other._unit;
        }

        #endregion

        #region Units

        public enum Unit
        {
            /// <summary>
            /// The value is an angle (deg). There are 360 degrees in a full circle.
            /// </summary>
            Deg,
            /// <summary>
            /// The value is an angle (rad). There are 2*pi radians in a full circle.
            /// </summary>
            Rad,
            /// <summary>
            /// The value is an angle (grad). There are 400 gradians in a full circle.
            /// </summary>
            Grad,
            /// <summary>
            /// The value is a turn. There is 1 turn in a full circle.
            /// </summary>
            Turn,
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
            if (obj is Angle)
                return this.Equals((Angle)obj);

            return false;
        }

        /// <summary>
        /// Returns a hash code that defines the current angle.
        /// </summary>
        /// <returns>The integer value of the hashcode.</returns>
        public override Int32 GetHashCode()
        {
            return (Int32)_value;
        }

        /// <summary>
        /// Returns a string representing the angle.
        /// </summary>
        /// <returns>The unit string.</returns>
        public override String ToString()
        {
            return String.Concat(_value.ToString(CultureInfo.InvariantCulture), _unit.ToString().ToLower());
        }

        #endregion
    }
}
