namespace AngleSharp.Common
{
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Common methods and variables of all tokenizers.
    /// </summary>
    public abstract class BaseTokenizer : IDisposable
    {
        #region Fields

        private readonly Stack<UInt16> _columns;
        private readonly TextSource _source;

        private UInt16 _column;
        private UInt16 _row;
        private Char _current;
        private StringBuilder _buffer;
        private Boolean _normalized;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new instance of the base tokenizer.
        /// </summary>
        /// <param name="source">The source to tokenize.</param>
        public BaseTokenizer(TextSource source)
        {
            _buffer = StringBuilderPool.Obtain();
            _columns = new Stack<UInt16>();
            _source = source;
            _current = Symbols.Null;
            _column = 0;
            _row = 1;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the current insertion point.
        /// </summary>
        public Int32 InsertionPoint
        {
            get => _source.Index;
            protected set
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
        /// Gets the current source index.
        /// </summary>
        public Int32 Position => _source.Index  - (_normalized ? 1 : 0);

        /// <summary>
        /// Gets the current character.
        /// </summary>
        protected Char Current => _current;

        /// <summary>
        /// Gets the allocated string buffer.
        /// </summary>
        protected StringBuilder StringBuffer => _buffer;

        /// <summary>
        /// Gets if the current index has been normalized (CRLF -> LF).
        /// </summary>
        protected Boolean IsNormalized => _normalized;

        #endregion

        #region Methods

        /// <summary>
        /// Flushes the buffer.
        /// </summary>
        /// <returns>The content of the buffer.</returns>
        public String FlushBuffer() => FlushBuffer(null);

        internal String FlushBuffer(Func<StringBuilder, String> stringResolver)
        {
            var content = stringResolver?.Invoke(StringBuffer) ?? StringBuffer.ToString();
            StringBuffer.Clear();
            return content;
        }

        /// <summary>
        /// Disposes the tokenizer by releasing the buffer.
        /// </summary>
        public void Dispose()
        {
            var isDisposed = StringBuffer == null;

            if (!isDisposed)
            {
                var disposable = _source as IDisposable;
                disposable?.Dispose();
                StringBuffer.Clear().ToPool();
                _buffer = null;
            }
        }

        /// <summary>
        /// Gets the current text position in the source.
        /// </summary>
        /// <returns>The (row, col) position.</returns>
        public TextPosition GetCurrentPosition() => new TextPosition(_row, _column, Position);

        /// <summary>
        /// Checks if the source continues with the given string.
        /// The comparison is case-insensitive.
        /// </summary>
        /// <param name="s">The string to compare to.</param>
        /// <returns>True if the source continues with the given string.</returns>
        protected Boolean ContinuesWithInsensitive(String s)
        {
            var content = PeekString(s.Length);
            return content.Length == s.Length && content.Isi(s);
        }

        /// <summary>
        /// Checks if the source continues with the given string.
        /// The comparison is case-sensitive.
        /// </summary>
        /// <param name="s">The string to compare to.</param>
        /// <returns>True if the source continues with the given string.</returns>
        protected Boolean ContinuesWithSensitive(String s)
        {
            var content = PeekString(s.Length);
            return content.Length == s.Length && content.Is(s);
        }

        /// <summary>
        /// Gets the string formed by the next characters.
        /// </summary>
        /// <param name="length">The length of the string.</param>
        /// <returns>The upcoming string.</returns>
        protected String PeekString(Int32 length)
        {
            var mark = _source.Index;
            _source.Index--;
            var content = _source.ReadCharacters(length);
            _source.Index = mark;
            return content;
        }

        /// <summary>
        /// Skips the next space character(s).
        /// </summary>
        /// <returns>The upcoming first non-space.</returns>
        protected Char SkipSpaces()
        {
            var c = GetNext();

            while (c.IsSpaceCharacter())
            {
                c = GetNext();
            }

            return c;
        }

        #endregion

        #region Source Management

        /// <summary>
        /// Gets the next character in the source by advancing.
        /// </summary>
        /// <returns>The next char.</returns>
        protected Char GetNext()
        {
            Advance();
            return _current;
        }

        /// <summary>
        /// Gets the previous character in the source by going back.
        /// </summary>
        /// <returns>The previous char.</returns>
        protected Char GetPrevious()
        {
            Back();
            return _current;
        }

        /// <summary>
        /// Advances in the source by 1 character if possible.
        /// </summary>
        protected void Advance()
        {
            if (_current != Symbols.EndOfFile)
            {
                AdvanceUnsafe();
            }
        }

        /// <summary>
        /// Advances in the source by n characters or less if possible.
        /// </summary>
        /// <param name="n">The positive number of characters.</param>
        protected void Advance(Int32 n)
        {
            while (n-- > 0 && _current != Symbols.EndOfFile)
            {
                AdvanceUnsafe();
            }
        }

        /// <summary>
        /// Goes back in the source by 1 character if possible.
        /// </summary>
        protected void Back()
        {
            if (InsertionPoint > 0)
            {
                BackUnsafe();
            }
        }

        /// <summary>
        /// Goes back in the source by n characters or less if possible.
        /// </summary>
        /// <param name="n">The positive number of characters.</param>
        protected void Back(Int32 n)
        {
            while (n-- > 0 && InsertionPoint > 0)
            {
                BackUnsafe();
            }
        }

        #endregion

        #region Helpers

        private void AdvanceUnsafe()
        {
            if (_current == Symbols.LineFeed)
            {
                _columns.Push(_column);
                _column = 1;
                _row++;
            }
            else
            {
                _column++;
            }

            _current = NormalizeForward(_source.ReadCharacter());
        }

        private void BackUnsafe()
        {
            _source.Index -= 1;

            if (_source.Index == 0)
            {
                _column = 0;
                _current = Symbols.Null;
                return;
            }

            var c = NormalizeBackward(_source[_source.Index - 1]);

            if (c == Symbols.LineFeed)
            {
                _column = _columns.Count != 0 ? _columns.Pop() : (UInt16)1;
                _row--;
                _current = c;
            }
            else if (c != Symbols.Null)
            {
                _current = c;
                _column--;
            }
        }

        private Char NormalizeForward(Char p)
        {
            if (p != Symbols.CarriageReturn)
            {
                _normalized = false;
                return p;
            }
            else if (_source.ReadCharacter() != Symbols.LineFeed)
            {
                _source.Index--;
            }
            else
            {
                _normalized = true;
            }
            
            return Symbols.LineFeed;
        }

        private Char NormalizeBackward(Char p)
        {
            if (p != Symbols.CarriageReturn)
            {
                _normalized = false;
                return p;
            }
            else if (_source.Index < _source.Length && _source[_source.Index] == Symbols.LineFeed)
            {
                _normalized = false;
                BackUnsafe();
                return Symbols.Null;
            }
            else
            {
                _normalized = true;
                return Symbols.LineFeed;
            }
        }

        #endregion
    }
}
