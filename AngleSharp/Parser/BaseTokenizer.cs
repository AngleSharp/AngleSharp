namespace AngleSharp.Parser
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Text;
    using AngleSharp.Events;
    using AngleSharp.Extensions;

    /// <summary>
    /// Common methods and variables of all tokenizers.
    /// </summary>
    [DebuggerStepThrough]
    abstract class BaseTokenizer : IDisposable
    {
        #region Fields

        protected readonly StringBuilder _stringBuffer;
        protected readonly IEventAggregator _events;
        readonly Stack<UInt16> _columns;
        readonly TextSource _source;

        UInt16 _column;
        UInt16 _row;
        Char _current;

        #endregion

        #region ctor

        public BaseTokenizer(TextSource source, IEventAggregator events)
        {
            _stringBuffer = Pool.NewStringBuilder();
            _events = events;
            _columns = new Stack<UInt16>();
            _source = source;
            _current = Symbols.Null;
            _column = 0;
            _row = 1;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the insertion point.
        /// </summary>
        public Int32 InsertionPoint
        {
            get { return _source.Index; }
            set
            {
                var delta = _source.Index - value;

                while (delta > 0)
                {
                    BackUnsafe();
                    delta--;
                }

                while (delta < 0)
                {
                    AdvanceUnsafe();
                    delta++;
                }
            }
        }

        /// <summary>
        /// Gets the current line within the source code.
        /// </summary>
        public UInt16 Line
        {
            get { return _row; }
        }

        /// <summary>
        /// Gets the current column within the source code.
        /// </summary>
        public UInt16 Column
        {
            get { return _column; }
        }

        /// <summary>
        /// Gets the current position within the source code.
        /// </summary>
        public Int32 Position
        {
            get { return _source.Index; }
        }

        /// <summary>
        /// Gets the current character.
        /// </summary>
        protected Char Current
        {
            get { return _current; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Disposes all disposable objects.
        /// </summary>
        public void Dispose()
        {
            var disposable = _source as IDisposable;

            if (disposable != null)
                disposable.Dispose();

            _stringBuffer.ToPool();
        }

        /// <summary>
        /// Gets the current position.
        /// </summary>
        /// <returns>A new text position.</returns>
        public TextPosition GetCurrentPosition()
        {
            return new TextPosition(_row, _column, Position);
        }

        /// <summary>
        /// Looks if the current character / next characters match a certain string.
        /// </summary>
        /// <param name="s">The string to compare to.</param>
        /// <param name="ignoreCase">Optional flag to set the case sensitivity.</param>
        /// <returns>The status of the check.</returns>
        protected Boolean ContinuesWith(String s, Boolean ignoreCase = true)
        {
            var mark = _source.Index;
            _source.Index--;
            var content = _source.ReadCharacters(s.Length);
            _source.Index = mark;
            return content.Length == s.Length && content.Equals(s, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
        }

        /// <summary>
        /// Resets the insertion point to the end of the buffer.
        /// </summary>
        public void ResetInsertionPoint()
        {
            InsertionPoint = _source.Length;
        }

        /// <summary>
        /// Advances to the next non-space character.
        /// </summary>
        /// <returns>The next non-space character.</returns>
        protected Char SkipSpaces()
        {
            var c = GetNext();

            while (c.IsSpaceCharacter())
                c = GetNext();

            return c;
        }

        #endregion

        #region Source Management

        /// <summary>
        /// Gets the next character (by advancing and returning the current character).
        /// </summary>
        protected Char GetNext()
        {
            Advance();
            return _current;
        }

        /// <summary>
        /// Gets the previous character (by rewinding and returning the current character).
        /// </summary>
        /// <returns>The previous character.</returns>
        protected Char GetPrevious()
        {
            Back();
            return _current;
        }

        /// <summary>
        /// Advances one character in the source code.
        /// </summary>
        protected void Advance()
        {
            if (_current != Symbols.EndOfFile)
                AdvanceUnsafe();
        }

        /// <summary>
        /// Advances n characters in the source code.
        /// </summary>
        /// <param name="n">The number of characters to advance.</param>
        protected void Advance(Int32 n)
        {
            while (n-- > 0 && _current != Symbols.EndOfFile)
                AdvanceUnsafe();
        }

        /// <summary>
        /// Moves back one character in the source code.
        /// </summary>
        protected void Back()
        {
            if (InsertionPoint > 0)
                BackUnsafe();
        }

        /// <summary>
        /// Moves back n characters in the source code.
        /// </summary>
        /// <param name="n">The number of characters to rewind.</param>
        protected void Back(Int32 n)
        {
            while (n-- > 0 && InsertionPoint > 0)
                BackUnsafe();
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Just advances one character without checking
        /// if the end is already reached.
        /// </summary>
        void AdvanceUnsafe()
        {
            if (_current.IsLineBreak())
            {
                _columns.Push(_column);
                _column = 0;
                _row++;
            }

            _column++;
            _current = _source.ReadCharacter();

            if (_current == Symbols.CarriageReturn)
            {
                _current = _source.ReadCharacter();
            }
        }

        /// <summary>
        /// Just goes back one character without checking
        /// if the beginning is already reached.
        /// </summary>
        void BackUnsafe()
        {
            _source.Index -= 1;

            if (_source.Index == 0)
            {
                _column = 0;
                _current = Symbols.Null;
                return;
            }

            _current = _source[_source.Index - 1];

            if (_current == Symbols.CarriageReturn)
            {
                BackUnsafe();
                return;
            }

            if (_current.IsLineBreak())
            {
                _column = _columns.Count != 0 ? _columns.Pop() : (UInt16)1;
                _row--;
            }
            else
            {
                _column--;
            }
        }

        #endregion
    }
}
