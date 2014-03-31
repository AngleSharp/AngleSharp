namespace AngleSharp
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Represents a length value.
    /// </summary>
    struct Length : IEquatable<Length>
    {
        #region Fields

        /// <summary>
        /// Gets a zero pixel length value.
        /// </summary>
        public static readonly Length Zero = new Length();

        Single _value;
        Unit _unit;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new length value.
        /// </summary>
        /// <param name="value">The value of the length.</param>
        /// <param name="unit">The unit of the length.</param>
        public Length(Single value, Unit unit)
        {
            _value = value;
            _unit = unit;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value of length.
        /// </summary>
        public Single Value
        {
            get { return _value; }
        }

        #endregion

        #region Methods

        public Boolean Equals(Length other)
        {
            return _value == other._value && _unit == other._unit;
        }

        #endregion

        #region Units

        public enum Unit
        {
            /// <summary>
            /// The value is a length (px).
            /// </summary>
            Px,
            /// <summary>
            /// The value is a length (em).
            /// </summary>
            Em,
            /// <summary>
            /// The value is a length (ex). Usually about 0.5em for most fonts.
            /// </summary>
            Ex,
            /// <summary>
            /// The value is a length (cm).
            /// </summary>
            Cm,
            /// <summary>
            /// The value is a length (mm).
            /// </summary>
            Mm,
            /// <summary>
            /// The value is a length (in).
            /// </summary>
            In,
            /// <summary>
            /// The value is a length (pt).
            /// </summary>
            Pt,
            /// <summary>
            /// The value is a length (pc).
            /// </summary>
            Pc,
            /// <summary>
            /// The value is a length (width of the 0-character).
            /// </summary>
            Ch,
            /// <summary>
            /// The value is the relative to the font-size of the root element (in em).
            /// </summary>
            Rem,
            /// <summary>
            /// The value is relative to the viewport width.
            /// </summary>
            Vw,
            /// <summary>
            /// The value is relative to the viewport height.
            /// </summary>
            Vh,
            /// <summary>
            /// The value is relative to the minimum of viewport width and height.
            /// </summary>
            Vmin,
            /// <summary>
            /// The value is relative to the maximum of viewport width and height.
            /// </summary>
            Vmax,
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
            if (obj is Length)
                return this.Equals((Length)obj);

            return false;
        }

        /// <summary>
        /// Returns a hash code that defines the current length.
        /// </summary>
        /// <returns>The integer value of the hashcode.</returns>
        public override Int32 GetHashCode()
        {
            return (Int32)_value;
        }

        /// <summary>
        /// Returns a string representing the length.
        /// </summary>
        /// <returns>The unit string.</returns>
        public override String ToString()
        {
            return String.Concat(_value.ToString(CultureInfo.InvariantCulture), _unit.ToString().ToLower());
        }

        #endregion
    }
}
