namespace AngleSharp
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// The positional range in the source code.
    /// </summary>
    [DebuggerStepThrough]
    public struct TextRange : IEquatable<TextRange>, IComparable<TextRange>
    {
        #region Fields

        readonly TextPosition _start;
        readonly TextPosition _end;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new text range.
        /// </summary>
        /// <param name="start">The start of the range.</param>
        /// <param name="end">The end of the range.</param>
        public TextRange(TextPosition start, TextPosition end)
        {
            _start = start;
            _end = end;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the start position of the range.
        /// </summary>
        public TextPosition Start
        {
            get { return _start; }
        }

        /// <summary>
        /// Gets the end position of the range.
        /// </summary>
        public TextPosition End
        {
            get { return _end; }
        }

        #endregion

        #region Comparison

        /// <summary>
        /// Returns a string representation of the range in the text.
        /// </summary>
        /// <returns>
        /// A string that contains the start and end positions.
        /// </returns>
        public override String ToString()
        {
            return String.Format("({0}) -- ({1})", _start, _end);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        /// An integer that is the hash code for this instance.
        /// </returns>
        public override Int32 GetHashCode()
        {
            return _end.GetHashCode() ^ _start.GetHashCode();
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <param name="obj">
        /// The object to compare with the current instance.
        /// </param>
        /// <returns>
        /// True if the given object is a text position with the same values,
        /// otherwise false.
        /// </returns>
        public override Boolean Equals(Object obj)
        {
            if (obj is TextRange)
                return Equals((TextRange)obj);

            return false;
        }

        /// <summary>
        /// Indicates whether the current range is equal to the given range.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// True if the given range has the same start and end position,
        /// otherwise false.
        /// </returns>
        public Boolean Equals(TextRange other)
        {
            return this._start.Equals(other._start) && this._end.Equals(other._end);
        }

        /// <summary>
        /// Compares the two ranges by their positions in the text source.
        /// </summary>
        /// <param name="a">The first operand.</param>
        /// <param name="b">The second operand.</param>
        /// <returns>
        /// True if the start position of the first operand is greater than the
        /// end position of the second operand.
        /// </returns>
        public static Boolean operator >(TextRange a, TextRange b)
        {
            return a._start > b._end;
        }

        /// <summary>
        /// Compares the two ranges by their positions in the text source.
        /// </summary>
        /// <param name="a">The first operand.</param>
        /// <param name="b">The second operand.</param>
        /// <returns>
        /// True if the end position of the first operand is less than the
        /// start position of the second operand.
        /// </returns>
        public static Boolean operator <(TextRange a, TextRange b)
        {
            return a._end < b._start;
        }

        /// <summary>
        /// Compares the current range with another range.
        /// </summary>
        /// <param name="other">The range to compare to.</param>
        /// <returns>
        /// A mathematical representation of the relation (1 = greater, -1 =
        /// less, 0 = equal).
        /// </returns>
        public Int32 CompareTo(TextRange other)
        {
            if (this > other)
                return 1;
            else if (other > this)
                return -1;

            return 0;
        }

        #endregion
    }
}
