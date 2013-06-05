using System;
using System.Collections.Generic;

namespace AngleSharp
{
    /// <summary>
    /// Represents the CSS source code manager.
    /// </summary>
    sealed class CssSource
    {
        #region Members

        char[] _chars;
        int _index;
        char _current;
        int _line;
        int _column;
        Stack<int> _columns;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS source code manager.
        /// </summary>
        /// <param name="source">The source code to manage.</param>
        public CssSource(string source)
        {
            _chars = source.ToCharArray();
            _columns = new Stack<int>();
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the current line.
        /// </summary>
        public int Line
        {
            get { return _line; }
        }

        /// <summary>
        /// Gets the current column.
        /// </summary>
        public int Column
        {
            get { return _column; }
        }

        /// <summary>
        /// Gets the current character.
        /// </summary>
        public char Current
        {
            get { return _current; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Reads the next character.
        /// </summary>
        /// <returns>The current character</returns>
        public char Next()
        {
            if (Specification.IsLineBreak(_current))
            {
                if (_chars[_index] == Specification.CR && _chars.Length > _index + 1 && _chars[_index + 1] == Specification.LF)
                    _index++;

                _line++;
                _columns.Push(_column);
                _column = 1;
            }
            else
                _column++;

            _index++;

            if (_index > _chars.Length)
                _index = _chars.Length;

            if (_index == _chars.Length)
                _current = Specification.EOF;
            else if (_chars[_index] == Specification.CR)
                _current = Specification.LF;
            else
                _current = _chars[_index];

            return _current;
        }

        /// <summary>
        /// Reads the previous character.
        /// </summary>
        /// <returns>The current character</returns>
        public char Previous()
        {
            _index--;

            if (_index < 0)
                _current = Specification.NULL;
            else
                _current = _chars[_index];

            if (Specification.IsLineBreak(_current))
            {
                if (_chars[_index] == Specification.LF && _index - 1 >= 0 && _chars[_index - 1] == Specification.CR)
                    _index--;

                _line--;
                _column = _columns.Pop();
            }
            else
                _column--;

            return _current;
        }

        /// <summary>
        /// Resets the source code manager.
        /// </summary>
        public void Reset()
        {
            _columns.Clear();
            _index = -1;
            _line = 1;
            _column = 0;
        }

        #endregion
    }
}
