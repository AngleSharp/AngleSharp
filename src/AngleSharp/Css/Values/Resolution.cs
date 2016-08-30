namespace AngleSharp.Css.Values
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Represents a resolution value.
    /// </summary>
    public struct Resolution : IEquatable<Resolution>, IComparable<Resolution>, IFormattable
    {
        #region Fields

        readonly Single _value;
        readonly Unit _unit;

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

        /// <summary>
        /// Gets the type of the length.
        /// </summary>
        public Unit Type
        {
            get { return _unit; }
        }

        /// <summary>
        /// Gets the representation of the unit as a string.
        /// </summary>
        public String UnitString
        {
            get
            {
                switch (_unit)
                {
                    case Unit.Dpcm:
                        return UnitNames.Dpcm;

                    case Unit.Dpi:
                        return UnitNames.Dpi;

                    case Unit.Dppx:
                        return UnitNames.Dppx;

                    default:
                        return String.Empty;
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Tries to convert the given string to a Resolution.
        /// </summary>
        /// <param name="s">The string to convert.</param>
        /// <param name="result">The reference to the result.</param>
        /// <returns>True if successful, otherwise false.</returns>
        public static Boolean TryParse(String s, out Resolution result)
        {
            var value = default(Single);
            var unit = GetUnit(s.CssUnit(out value));

            if (unit != Unit.None)
            {
                result = new Resolution(value, unit);
                return true;
            }

            result = default(Resolution);
            return false;
        }

        /// <summary>
        /// Gets the unit from the enumeration for the provided string.
        /// </summary>
        /// <param name="s">The string to convert.</param>
        /// <returns>A valid CSS unit or None.</returns>
        public static Unit GetUnit(String s)
        {
            switch (s)
            {
                case "dpcm": return Unit.Dpcm;
                case "dpi": return Unit.Dpi;
                case "dppx": return Unit.Dppx;
                default: return Unit.None;
            }
        }

        /// <summary>
        /// Converts the resolution to a per pixel density.
        /// </summary>
        /// <returns>The density in dots per pixels.</returns>
        public Single ToDotsPerPixel()
        {
            if (_unit == Unit.Dpi)
            {
                return _value / 96f;
            }
            else if (_unit == Unit.Dpcm)
            {
                return _value * 127f / (50f * 96f);
            }

            return _value;
        }

        /// <summary>
        /// Converts the resolution to the given unit.
        /// </summary>
        /// <param name="unit">The unit to convert to.</param>
        /// <returns>The density in the given unit.</returns>
        public Single To(Unit unit)
        {
            var value = ToDotsPerPixel();

            if (unit == Unit.Dpi)
            {
                return value * 96f;
            }
            else if (unit == Unit.Dpcm)
            {
                return value * 50f * 96f / 127f;
            }

            return value;
        }

        /// <summary>
        /// Checks if the current resolution equals the given one.
        /// </summary>
        /// <param name="other">The given resolution to check for equality.</param>
        /// <returns>True if both are equal, otherwise false.</returns>
        public Boolean Equals(Resolution other)
        {
            return _value == other._value && _unit == other._unit;
        }

        #endregion

        #region Units

        /// <summary>
        /// The various resolution units.
        /// </summary>
        public enum Unit : byte
        {
            /// <summary>
            /// No valid unit.
            /// </summary>
            None,
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
        /// Compares the current resolution against the given one.
        /// </summary>
        /// <param name="other">The resolution to compare to.</param>
        /// <returns>The result of the comparison.</returns>
        public Int32 CompareTo(Resolution other)
        {
            return ToDotsPerPixel().CompareTo(other.ToDotsPerPixel());
        }

        /// <summary>
        /// Tests if another object is equal to this object.
        /// </summary>
        /// <param name="obj">The object to test with.</param>
        /// <returns>True if the two objects are equal, otherwise false.</returns>
        public override Boolean Equals(Object obj)
        {
            var other = obj as Resolution?;

            if (other != null)
            {
                return Equals(other.Value);
            }

            return false;
        }

        /// <summary>
        /// Returns a hash code that defines the current resolution.
        /// </summary>
        /// <returns>The integer value of the hashcode.</returns>
        public override Int32 GetHashCode()
        {
            return _value.GetHashCode();
        }

        #endregion

        #region String Representation

        /// <summary>
        /// Returns a string representing the resolution.
        /// </summary>
        /// <returns>The unit string.</returns>
        public override String ToString()
        {
            return String.Concat(_value.ToString(), UnitString);
        }

        /// <summary>
        /// Returns a formatted string representing the resolution.
        /// </summary>
        /// <param name="format">The format of the number.</param>
        /// <param name="formatProvider">The provider to use.</param>
        /// <returns>The unit string.</returns>
        public String ToString(String format, IFormatProvider formatProvider)
        {
            return String.Concat(_value.ToString(format, formatProvider), UnitString);
        }

        #endregion
    }
}
