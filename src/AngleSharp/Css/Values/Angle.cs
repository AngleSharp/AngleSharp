namespace AngleSharp.Css.Values
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Represents an angle value.
    /// </summary>
    public struct Angle : IEquatable<Angle>, IComparable<Angle>, IFormattable
    {
        #region Basic angles

        /// <summary>
        /// The zero angle.
        /// </summary>
        public static readonly Angle Zero = new Angle(0f, Angle.Unit.Rad);

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
        /// Gets the value of the angle.
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
                    case Unit.Deg:
                        return UnitNames.Deg;

                    case Unit.Grad:
                        return UnitNames.Grad;

                    case Unit.Turn:
                        return UnitNames.Turn;

                    case Unit.Rad:
                        return UnitNames.Rad;

                    default:
                        return String.Empty;
                }
            }
        }

        #endregion

        #region Comparison

        /// <summary>
        /// Compares the magnitude of two angles.
        /// </summary>
        public static Boolean operator >=(Angle a, Angle b)
        {
            var result = a.CompareTo(b);
            return result == 0 || result == 1;
        }

        /// <summary>
        /// Compares the magnitude of two angles.
        /// </summary>
        public static Boolean operator >(Angle a, Angle b)
        {
            return a.CompareTo(b) == 1;
        }

        /// <summary>
        /// Compares the magnitude of two angles.
        /// </summary>
        public static Boolean operator <=(Angle a, Angle b)
        {
            var result = a.CompareTo(b);
            return result == 0 || result == -1;
        }

        /// <summary>
        /// Compares the magnitude of two angles.
        /// </summary>
        public static Boolean operator <(Angle a, Angle b)
        {
            return a.CompareTo(b) == -1;
        }

        /// <summary>
        /// Compares the current angle against the given one.
        /// </summary>
        /// <param name="other">The angle to compare to.</param>
        /// <returns>The result of the comparison.</returns>
        public Int32 CompareTo(Angle other)
        {
            return ToRadian().CompareTo(other.ToRadian());
        }

        #endregion

        #region Methods

        /// <summary>
        /// Tries to convert the given string to an Angle.
        /// </summary>
        /// <param name="s">The string to convert.</param>
        /// <param name="result">The reference to the result.</param>
        /// <returns>True if successful, otherwise false.</returns>
        public static Boolean TryParse(String s, out Angle result)
        {
            var value = default(Single);
            var unit = GetUnit(s.CssUnit(out value));

            if (unit != Unit.None)
            {
                result = new Angle(value, unit);
                return true;
            }

            result = default(Angle);
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
                case "deg": return Unit.Deg;
                case "grad": return Unit.Grad;
                case "turn": return Unit.Turn;
                case "rad": return Unit.Rad;
                default: return Unit.None;
            }
        }

        /// <summary>
        /// Converts the contained value to rad.
        /// </summary>
        /// <returns>The value in rad.</returns>
        public Single ToRadian()
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

        /// <summary>
        /// Converts the contained value to turns.
        /// </summary>
        /// <returns>The value in turns.</returns>
        public Single ToTurns()
        {
            switch (_unit)
            {
                case Unit.Deg:
                    return (Single)(_value / 360.0);

                case Unit.Grad:
                    return (Single)(_value / 400.0);

                case Unit.Rad:
                    return (Single)(_value / (2.0 * Math.PI));

                default:
                    return _value;
            }
        }

        /// <summary>
        /// Checks for equality with the other angle.
        /// </summary>
        /// <param name="other">The angle to compare with.</param>
        /// <returns>True if both represent the same angle in rad.</returns>
        public Boolean Equals(Angle other)
        {
            return ToRadian() == other.ToRadian();
        }

        #endregion

        #region Units

        /// <summary>
        /// An enumeration of angle representations.
        /// </summary>
        public enum Unit : byte
        {
            /// <summary>
            /// No valid unit.
            /// </summary>
            None,
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
        /// Checks for equality of two angles.
        /// </summary>
        public static Boolean operator ==(Angle a, Angle b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// Checks for inequality of two angles.
        /// </summary>
        public static Boolean operator !=(Angle a, Angle b)
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
            var other = obj as Angle?;

            if (other != null)
            {
                return Equals(other.Value);
            }

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
    }
}
