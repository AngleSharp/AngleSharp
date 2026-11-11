namespace AngleSharp.Common
{
    using AngleSharp.Text;
    using System;
    using System.Buffers;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Text;

    /// <summary>
    /// Common methods and variables of all tokenizers.
    /// </summary>
    public abstract class BaseTokenizer: IDisposable
    {
        #region Fields

        private readonly Stack<UInt16> _columns;

        private readonly IReadOnlyTextSource _source;
        private readonly CharArrayTextSource? _cats;
        private readonly ReadOnlyMemoryTextSource? _roms;

        private StringBuilder _stringBuilder;
        private IMutableCharBuffer _charBuffer;
        private readonly ArrayPoolBuffer? _apb;
        private readonly StringBuilderBuffer? _sbb;

        private UInt16 _column;
        private UInt16 _row;
        private Char _current;
        private Boolean _normalized;
        private Boolean _disableElementPositionTracking;
        private Boolean _disposed;

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

            if (_source is CharArrayTextSource cats)
            {
                _cats = cats;
            }
            else if (_source is ReadOnlyMemoryTextSource roms)
            {
                _roms = roms;
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
        /// Gets the character buffer if it is the pooled one, which keeps what has been
        /// appended in one block of memory - there to be looked at without copying it out.
        /// </summary>
        private protected ArrayPoolBuffer? PooledBuffer => _apb;

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
            if (!_disposed)
            {
                _disposed = true;
                _source.Dispose();
                ReleaseBuffers();
            }
        }

        /// <summary>
        /// Gives the buffers back to their pools without disposing the source. For a tokenizer
        /// that has delivered its last token while its owner lives on: the character buffer is
        /// as long as the source, and the next tokenizer can have it right away instead of a
        /// newly allocated one.
        /// </summary>
        /// <remarks>
        /// Every token text handed out so far is a slice of that buffer, so this is only for a
        /// caller that knows none of them is kept. No token can be read afterwards.
        /// </remarks>
        internal void ReleaseBuffers()
        {
            if (_charBuffer is not null)
            {
                _stringBuilder.Clear();
                _stringBuilder.ReturnToPool();
                _stringBuilder = null!;

                _charBuffer.Discard();
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

        /// <summary>
        /// Drops everything the character buffer has accumulated so far.
        /// </summary>
        /// <remarks>
        /// The pooled buffer is sized from the source's character count, which is only enough for
        /// a single pass: every token committed to it advances a write position that is never
        /// rewound. A parser that restarts over the same source -- which an encoding declaration
        /// makes it do -- therefore has to say so, or the second pass appends behind the tokens of
        /// the abandoned one and runs off the end of the rented array.
        /// </remarks>
        internal void ResetBuffer() => _charBuffer.Reset();

        /// <summary>
        /// Re-reads the source's character count and grows the character buffer to match.
        /// </summary>
        /// <remarks>
        /// The count is only fixed for as long as the encoding is. Re-decoding the same bytes with
        /// a single-byte encoding turns every multi-byte sequence into that many characters, so a
        /// source can be substantially longer after an encoding declaration than the buffer was
        /// rented for at the start of the parse.
        /// </remarks>
        internal void GrowBufferToSource()
        {
            if (_source.TryGetContentLength(out var length))
            {
                _charBuffer.EnsureCapacity(length);
            }
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

        /// <summary>
        /// The kinds of plain text a tokenizer reads in bulk, by what ends a run of it. A line
        /// break and a NUL end every one of them: the first needs its row counted and a CR
        /// normalized, the second is replaced.
        /// </summary>
        private protected enum TextRun : Byte
        {
            /// <summary>Ended by &lt; and &amp;: data and RCDATA.</summary>
            Data,
            /// <summary>Ended by &lt;: RAWTEXT and script data.</summary>
            RawText,
            /// <summary>Ended by &lt; and -: escaped script data.</summary>
            EscapedScript,
            /// <summary>Ended by " and &amp;: a double-quoted attribute value.</summary>
            DoubleQuoted,
            /// <summary>Ended by ' and &amp;: a single-quoted attribute value.</summary>
            SingleQuoted,
            /// <summary>Ended by -: a comment.</summary>
            Comment,
            /// <summary>Ended by nothing else: PLAINTEXT.</summary>
            Plaintext,
            /// <summary>
            /// Ended by white space, / &gt; = " ' &lt; and the upper-case letters: what a tag
            /// name or an attribute name takes over as it is written.
            /// </summary>
            Name,
        }

#if NET8_0_OR_GREATER
        // Indexed by TextRun.
        private static readonly SearchValues<Char>[] RunTerminators =
        [
            SearchValues.Create("<&\0\r\n"),
            SearchValues.Create("<\0\r\n"),
            SearchValues.Create("<-\0\r\n"),
            SearchValues.Create("\"&\0\r\n"),
            SearchValues.Create("'&\0\r\n"),
            SearchValues.Create("-\0\r\n"),
            SearchValues.Create("\0\r\n"),
            SearchValues.Create("\t\n\f\r \"'/<=>\0ABCDEFGHIJKLMNOPQRSTUVWXYZ"),
        ];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Int32 IndexOfTerminator(ReadOnlySpan<Char> text, TextRun run) =>
            text.IndexOfAny(RunTerminators[(Int32)run]);
#else
        // Without SearchValues there is no single search for five characters, so this is one
        // for the terminators of the content model and one for the line breaks in front of
        // them. Each of the two is only ever let loose on a window: on all that is left of the
        // source, the one whose characters are rare would run to the end of it again for every
        // hit of the other - quadratic for a long script without a "<", or a long text
        // without a line break.
        private static Int32 IndexOfTerminator(ReadOnlySpan<Char> text, TextRun run)
        {
            const Int32 window = 256;

            if (run == TextRun.Name)
            {
                return IndexOfNameEnd(text);
            }

            for (var offset = 0; offset < text.Length; offset += window)
            {
                var part = text.Slice(offset, Math.Min(window, text.Length - offset));
                Int32 found;

                switch (run)
                {
                    case TextRun.Data:
                        found = part.IndexOfAny(Symbols.LessThan, Symbols.Ampersand, Symbols.Null);
                        break;
                    case TextRun.RawText:
                        found = part.IndexOfAny(Symbols.LessThan, Symbols.Null);
                        break;
                    case TextRun.EscapedScript:
                        found = part.IndexOfAny(Symbols.LessThan, Symbols.Minus, Symbols.Null);
                        break;
                    case TextRun.DoubleQuoted:
                        found = part.IndexOfAny(Symbols.DoubleQuote, Symbols.Ampersand, Symbols.Null);
                        break;
                    case TextRun.SingleQuoted:
                        found = part.IndexOfAny(Symbols.SingleQuote, Symbols.Ampersand, Symbols.Null);
                        break;
                    case TextRun.Comment:
                        found = part.IndexOfAny(Symbols.Minus, Symbols.Null);
                        break;
                    default:
                        found = part.IndexOf(Symbols.Null);
                        break;
                }

                var line = (found < 0 ? part : part.Slice(0, found)).IndexOfAny(Symbols.CarriageReturn, Symbols.LineFeed);

                if (line >= 0)
                {
                    return offset + line;
                }
                else if (found >= 0)
                {
                    return offset + found;
                }
            }

            return -1;
        }

        // The characters that end a run of TextRun.Name, as one bit each: those below 64 and
        // the upper-case letters, which all lie in the 64 above.
        private const UInt64 NameEndLow =
            (1UL << Symbols.Null) | (1UL << Symbols.Tab) | (1UL << Symbols.LineFeed) | (1UL << Symbols.FormFeed) |
            (1UL << Symbols.CarriageReturn) | (1UL << Symbols.Space) | (1UL << Symbols.DoubleQuote) |
            (1UL << Symbols.SingleQuote) | (1UL << Symbols.Solidus) | (1UL << Symbols.LessThan) |
            (1UL << Symbols.Equality) | (1UL << Symbols.GreaterThan);
        private const UInt64 NameEndHigh = ((1UL << 26) - 1) << ('A' - 64);

        // Names are a handful of characters long, which a plain loop is good for; there is
        // nothing in IndexOfAny for a set of this size anyway.
        private static Int32 IndexOfNameEnd(ReadOnlySpan<Char> text)
        {
            for (var i = 0; i < text.Length; i++)
            {
                var c = text[i];

                if (c < 128 && (((c < 64 ? NameEndLow >> c : NameEndHigh >> (c - 64)) & 1UL) != 0UL))
                {
                    return i;
                }
            }

            return -1;
        }
#endif

        /// <summary>
        /// Reads on from the current position: appends the run of plain text that follows, if
        /// there is one, in a single copy, and returns the character after it - the one that
        /// needs the attention of the state machine. Where the source cannot be read in bulk
        /// this is <see cref="GetNext"/>, so a state calls it wherever it would have asked for
        /// the next character only to append that as well.
        /// </summary>
        /// <param name="run">The kind of text, i.e., what ends a run of it.</param>
        private protected Char ScanText(TextRun run)
        {
            ReadOnlySpan<Char> remaining;
            Int32 index;

            if (_roms is not null)
            {
                index = _roms.Index;

                if (index >= _roms.Length)
                {
                    return GetNext();
                }

                remaining = _roms.Remaining;
            }
            else if (_cats is not null)
            {
                index = _cats.Index;

                if (index >= _cats.Length)
                {
                    return GetNext();
                }

                remaining = _cats.Array.AsSpan(index, _cats.Length - index);
            }
            else
            {
                return GetNext();
            }

            var found = IndexOfTerminator(remaining, run);
            var runLength = found < 0 ? remaining.Length : found;

            if (runLength > 0)
            {
                var text = remaining.Slice(0, runLength);

                if (_apb != null)
                {
                    _apb.Append(text);
                }
                else
                {
#if NETSTANDARD2_0 || NET462 || NET472
                    for (var i = 0; i < text.Length; i++)
                    {
                        _sbb!._sb.Append(text[i]);
                    }
#else
                    _sbb!._sb.Append(text);
#endif
                }

                if (!_disableElementPositionTracking)
                {
                    if (_current == Symbols.LineFeed)
                    {
                        _columns.Push(_column);
                        _column = (UInt16)runLength;
                        _row++;
                    }
                    else
                    {
                        _column += (UInt16)runLength;
                    }
                }

                _current = text[runLength - 1];

                if (_roms is not null)
                {
                    _roms.Index = index + runLength;
                }
                else
                {
                    _cats!.Index = index + runLength;
                }
            }

            return GetNext();
        }

        #endregion

        #region Helpers

        private void AdvanceUnsafe()
        {
            if (!_disableElementPositionTracking)
            {
                Track();
            }

            var c = ReadCharFromSource();
            _current = NormalizeForward(c);
        }

        private void Track()
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
                BackUnsafe();
                return _current;
            }
            else
            {
                _normalized = true;
            }

            return Symbols.LineFeed;
        }

        // The two sources a string or a buffer is parsed from are sealed, so their reads are
        // direct calls rather than one through the interface for every character.
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Char ReadCharFromSource()
        {
            if (_roms is not null)
            {
                return _roms.ReadCharacter();
            }
            else if (_cats is not null)
            {
                return _cats.ReadCharacter();
            }

            return _source.ReadCharacter();
        }

        #endregion
    }
}
