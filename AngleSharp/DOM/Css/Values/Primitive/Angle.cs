namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using System;
    using System.Globalization;

    /// <summary>
    /// Represents an angle value.
    /// </summary>
    public struct Angle : IEquatable<Angle>, IComparable<Angle>, IFormattable, ICssValue
    {
        #region Basic angles

        /// <summary>
        /// The zero angle.
        /// </summary>
        public static readonly Angle Zero = new Angle();

        /// <summary>
        /// The 45° angle.
        /// </summary>
        public static readonly Angle HalfQuarter = new Angle(45f, Angle.Unit.Deg);

        /// <summary>
        /// The 90° angle.
        /// </summary>
        public static readonly Angle Quarter = new Angle(90f, Angle.Unit.Deg);

        /// <summary>
        /// The 135° angle.
        /// </summary>
        public static readonly Angle TripleHalfQuarter = new Angle(135f, Angle.Unit.Deg);

        /// <summary>
        /// The 180° angle.
        /// </summary>
        public static readonly Angle Half = new Angle(180f, Angle.Unit.Deg);

        #endregion

        #region Fields

        readonly Single _value;
        readonly Unit _unit;

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
        /// Gets the value of angle in radians.
        /// </summary>
        public Single Value
        {
            get
            {
                switch (_unit)
                {
                    case Unit.Deg:
                        return (Single)(Math.PI / 180.0 * _value);

                    case Unit.Grad:
                        return (Single)(Math.PI / 200.0 * _value);

                    case Unit.Turn:
                        return (Single)(2.0 * Math.PI * _value);

                    default:
                        return _value;
                }
            }
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
                    case Unit.Deg:
                        return Units.Deg;

                    case Unit.Grad:
                        return Units.Grad;

                    case Unit.Turn:
                        return Units.Turn;

                    case Unit.Rad:
                        return Units.Rad;

                    default:
                        return String.Empty;
                }
            }
        }

        #endregion

        #region Casts

        /// <summary>
        /// Converts the angle to a number representing radians.
        /// </summary>
        /// <param name="angle">The angle to convert.</param>
        /// <returns>The number of radians.</returns>
        public static explicit operator Single(Angle angle)
        {
            return angle.Value;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Computes the tangent of the given angle.
        /// </summary>
        /// <returns>The tangent.</returns>
        public Single Tan()
        {
            return (Single)Math.Tan(Value);
        }

        /// <summary>
        /// Computes the cosine of the given angle.
        /// </summary>
        /// <returns>The cosine.</returns>
        public Single Cos()
        {
            return (Single)Math.Cos(Value);
        }

        /// <summary>
        /// Computes the sine of the given angle.
        /// </summary>
        /// <returns>The sine.</returns>
        public Single Sin()
        {
            return (Single)Math.Sin(Value);
        }

        /// <summary>
        /// Checks for equality with the other angle.
        /// </summary>
        /// <param name="other">The angle to compare with.</param>
        /// <returns>True if both represent the same angle in rad.</returns>
        public Boolean Equals(Angle other)
        {
            return Value == other.Value;
        }

        #endregion

        #region Units

        /// <summary>
        /// An enumeration of angle representations.
        /// </summary>
        public enum Unit : ushort
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

        #region Equality

        /// <summary>
        /// Compares the current angle against the given one.
        /// </summary>
        /// <param name="other">The angle to compare to.</param>
        /// <returns>The result of the comparison.</returns>
        public Int32 CompareTo(Angle other)
        {
            return Value.CompareTo(other.Value);
        }

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

        #endregion

        #region String Representation

        /// <summary>
        /// Returns a string representing the angle.
        /// </summary>
        /// <returns>The unit string.</returns>
        public override String ToString()
        {
            return String.Concat(_value.ToString(), UnitString);
        }

        /// <summary>
        /// Returns a formatted string representing the angle.
        /// </summary>
        /// <param name="format">The format of the number.</param>
        /// <param name="formatProvider">The provider to use.</param>
        /// <returns>The unit string.</returns>
        public String ToString(String format, IFormatProvider formatProvider)
        {
            return String.Concat(_value.ToString(format, formatProvider), UnitString);
        }

        #endregion

        #region CSS Value

        CssValueType ICssValue.Type
        {
            get { return CssValueType.Primitive; }
        }

        String ICssValue.CssText
        {
            get { return String.Concat(_value.ToString(CultureInfo.InvariantCulture), UnitString); }
        }

        #endregion
    }
}
