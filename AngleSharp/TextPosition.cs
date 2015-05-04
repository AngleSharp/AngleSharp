namespace AngleSharp
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// The position in the source code.
    /// </summary>
    [DebuggerStepThrough]
    public struct TextPosition : IEquatable<TextPosition>, IComparable<TextPosition>
    {
        #region Fields

        /// <summary>
        /// An empty position (0, 0, 0).
        /// </summary>
        public static readonly TextPosition Empty = new TextPosition();

        readonly UInt16 _line;
        readonly UInt16 _column;
        readonly Int32 _position;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new text position.
        /// </summary>
        /// <param name="line">The line of the character.</param>
        /// <param name="column">The column of the character.</param>
        /// <param name="position">The index of the character.</param>
        public TextPosition(UInt16 line, UInt16 column, Int32 position)
        {
            _line = line;
            _column = column;
            _position = position;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the line within the document.
        /// </summary>
        public Int32 Line
        {
            get { return _line; }
        }

        /// <summary>
        /// Gets the column within the document.
        /// </summary>
        public Int32 Column
        {
            get { return _column; }
        }

        /// <summary>
        /// Gets the position within the source.
        /// </summary>
        public Int32 Position
        {
            get { return _position; }
        }

        #endregion

        #region Comparison

        /// <summary>
        /// Returns a string representation of the position in the text.
        /// </summary>
        /// <returns>
        /// A string that contains the contained line and column.
        /// </returns>
        public override String ToString()
        {
            return String.Format("Ln {0}, Col {1}, Pos {2}", _line, _column, _position);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        /// An integer that is the hash code for this instance.
        /// </returns>
        public override Int32 GetHashCode()
        {
            return _position ^ (_line | _column) + _line;
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
            if (obj is TextPosition)
                return Equals((TextPosition)obj);

            return false;
        }

        /// <summary>
        /// Indicates whether the current position is equal to the given
        /// position.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// True if the given position has the same values, otherwise false.
        /// </returns>
        public Boolean Equals(TextPosition other)
        {
            return this._position == other._position && this._column == other._column && this._line == other._line;
        }

        /// <summary>
        /// Compares the two positions by their absolute positions in the text
        /// source.
        /// </summary>
        /// <param name="a">The first operand.</param>
        /// <param name="b">The second operand.</param>
        /// <returns>
        /// True if the position of the first operand is greater than the
        /// second operand.
        /// </returns>
        public static Boolean operator >(TextPosition a, TextPosition b)
        {
            return a._position > b._position;
        }

        /// <summary>
        /// Compares the two positions by their absolute positions in the text
        /// source.
        /// </summary>
        /// <param name="a">The first operand.</param>
        /// <param name="b">The second operand.</param>
        /// <returns>
        /// True if the position of the first operand is less than the second
        /// operand.
        /// </returns>
        public static Boolean operator <(TextPosition a, TextPosition b)
        {
            return a._position < b._position;
        }

        /// <summary>
        /// Compares the current position with another position.
        /// </summary>
        /// <param name="other">The position to compare to.</param>
        /// <returns>
        /// A mathematical representation of the relation (1 = greater, -1 =
        /// less, 0 = equal).
        /// </returns>
        public Int32 CompareTo(TextPosition other)
        {
            return this.Equals(other) ? 0 : (this > other ? 1 : -1);
        }

        #endregion
    }
}
