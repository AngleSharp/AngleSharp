namespace AngleSharp.Common
{
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Common methods and variables of all tokenizers.
    /// </summary>
    public abstract class BaseTokenizer: IDisposable
    {
        #region Fields

        private readonly Stack<UInt16> _columns;

        private readonly IReadOnlyTextSource _source;
        private readonly WritableTextSource? _wts;
        private readonly CharArrayTextSource? _cats;

        private StringBuilder _stringBuilder;
        private IMutableCharBuffer _charBuffer;
        private readonly ArrayPoolBuffer? _apb;
        private readonly StringBuilderBuffer? _sbb;

        private UInt16 _column;
        private UInt16 _row;
        private Char _current;
        private Boolean _normalized;
        private Boolean _disableElementPositionTracking;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new instance of the base tokenizer.
        /// </summary>
        /// <param name="source">The source to tokenize.</param>
        public BaseTokenizer(TextSource source)
        {
            _stringBuilder = StringBuilderPool.Obtain();

            if (source.TryGetContentLength(out var length))
            {
                _charBuffer = _apb = new ArrayPoolBuffer(length);
            }
            else
            {
                _charBuffer = _sbb = new StringBuilderBuffer();
            }

            _source = source.GetUnderlyingTextSource();

            if (_source is WritableTextSource wts)
            {
                _wts = wts;
            }
            else if (_source is CharArrayTextSource cats)
            {
                _cats = cats;
            }

            _current = Symbols.Null;
            _column = 0;
            _row = 1;
            _columns = new Stack<UInt16>();
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
        protected StringBuilder StringBuffer => _stringBuilder;

        /// <summary>
        /// Gets the allocated string buffer.
        /// </summary>
        private protected IMutableCharBuffer CharBuffer => _charBuffer;

        /// <summary>
        /// Gets if the current index has been normalized (CRLF -> LF).
        /// </summary>
        protected Boolean IsNormalized => _normalized;

        /// <summary>
        ///
        /// </summary>
        public Boolean DisableElementPositionTracking
        {
            get => _disableElementPositionTracking;
            set => _disableElementPositionTracking = value;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Flushes the buffer.
        /// </summary>
        /// <returns>The content of the buffer.</returns>
        public String FlushBuffer()
        {
            var result = StringBuffer.ToString();
            StringBuffer.Clear();
            return result;
        }

        /// <summary>
        /// Flushes the buffer. Will return the reference to the memory without creating a new string if possible.
        /// </summary>
        /// <returns></returns>
        internal StringOrMemory FlushBufferFast()
        {
            return _charBuffer.GetDataAndClear();
        }

        internal StringOrMemory FlushBufferFast(Func<IMutableCharBuffer, String?> stringResolver)
        {
            var resolved = stringResolver(CharBuffer);
            if (resolved != null)
            {
                _charBuffer.Discard();
                return new StringOrMemory(resolved);
            }

            return _charBuffer.GetDataAndClear();
        }

        /// <summary>
        /// Disposes the tokenizer by releasing the buffer.
        /// </summary>
        public void Dispose()
        {
            var isDisposed = _charBuffer is null;
            if (!isDisposed)
            {
                _source.Dispose();

                _stringBuilder.Clear();
                _stringBuilder.ReturnToPool();
                _stringBuilder = null!;

                _charBuffer!.Discard();
                _charBuffer.Dispose();
                _charBuffer = null!;
            }
        }

        /// <summary>
        /// Gets the current text position in the source.
        /// </summary>
        /// <returns>The (row, col) position.</returns>
        public TextPosition GetCurrentPosition() => new(_row, _column, Position);

        /// <summary>
        /// Checks if the source continues with the given string.
        /// The comparison is case-insensitive.
        /// </summary>
        /// <param name="s">The string to compare to.</param>
        /// <returns>True if the source continues with the given string.</returns>
        protected Boolean ContinuesWithInsensitive(String s)
        {
            var content = PeekStringFast(s.Length);
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
            var content = PeekStringFast(s.Length);
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
        /// Will try to get the reference to the memory or will create new string formed by the next characters.
        /// </summary>
        /// <param name="length">The length of the string.</param>
        /// <returns>The upcoming string.</returns>
        protected StringOrMemory PeekStringFast(Int32 length)
        {
            var mark = _source.Index;
            _source.Index--;
            var content = _source.ReadMemory(length);
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

        /// <summary>
        /// Appends the given character to the buffer.
        /// </summary>
        private protected void Append(Char c)
        {
            if (_sbb != null)
            {
                _sbb._sb.Append(c);
            }
            else
            {
                _apb!.Append(c);
            }
        }

        private protected void Append(Char a, Char b)
        {
            if (_sbb != null)
            {
                _sbb._sb.Append(a).Append(b);
            }
            else
            {
                _apb!.Append(a);
                _apb!.Append(b);
            }
        }

        private protected void Append(Char a, Char b, Char c)
        {
            if (_sbb != null)
            {
                _sbb._sb.Append(a).Append(b).Append(c);
            }
            else
            {
                _apb!.Append(a);
                _apb!.Append(b);
                _apb!.Append(c);
            }
        }

        private protected void Append(Char a, Char b, Char c, Char d)
        {
            if (_sbb != null)
            {
                _sbb._sb.Append(a).Append(b).Append(c).Append(d);
            }
            else
            {
                _apb!.Append(a);
                _apb!.Append(b);
                _apb!.Append(c);
                _apb!.Append(d);
            }
        }

        #endregion

        #region Helpers

        private void AdvanceUnsafe()
        {
            if (!_disableElementPositionTracking) Track();

            var c = ReadCharFromSource();
            _current = NormalizeForward(c);

            void Track()
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
            }
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
            _current = c;

            if (!_disableElementPositionTracking)
            {
                if (c == Symbols.LineFeed)
                {
                    _column = _columns.Count != 0 ? _columns.Pop() : (UInt16)1;
                    _row--;
                }
                else if (c != Symbols.Null)
                {
                    _column--;
                }
            }
        }

        private Char NormalizeForward(Char p)
        {
            if (p != Symbols.CarriageReturn)
            {
                _normalized = false;
                return p;
            }
            else if (ReadCharFromSource() != Symbols.LineFeed)
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

        private Char ReadCharFromSource()
        {
            if (_wts != null)
            {
                return _wts.ReadCharacter();
            }

            if (_cats != null)
            {
                return _cats.ReadCharacter();
            }

            return _source.ReadCharacter();
        }

        #endregion
    }
}
