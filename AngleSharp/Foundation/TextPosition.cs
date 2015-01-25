namespace AngleSharp
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// The position in the source code.
    /// </summary>
    [DebuggerStepThrough]
    public struct TextPosition
    {
        #region Fields

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
    }
}
