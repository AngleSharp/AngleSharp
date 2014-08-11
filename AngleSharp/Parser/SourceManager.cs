namespace AngleSharp.Parser
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents the source code manager.
    /// </summary>
    [DebuggerStepThrough]
    abstract class SourceManager : IDisposable
    {
        #region Fields

        readonly Stack<Int32> _collengths;
        readonly ITextSource _reader;

        Int32 _column;
        Int32 _row;
        Char _current;

        #endregion

        #region ctor

        /// <summary>
        /// Prepares everything.
        /// </summary>
        SourceManager()
        {
            _current = Specification.Null;
            _collengths = new Stack<Int32>();
            _column = 0;
            _row = 1;
        }

        /// <summary>
        /// Constructs a new instance of the source code manager.
        /// </summary>
        /// <param name="reader">The underlying text stream to read.</param>
        public SourceManager(ITextSource reader)
            : this()
        {
            _reader = reader;
        }

        /// <summary>
        /// Constructs a new instance of the source code manager.
        /// </summary>
        /// <param name="source">The source code to manage.</param>
        internal SourceManager(String source)
            : this(new TextSource(source))
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the insertion point.
        /// </summary>
        public Int32 InsertionPoint
        {
            get { return _reader.Index; }
            set
            {
                var delta = _reader.Index - value;

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
        public Int32 Line
        {
            get { return _row; }
        }

        /// <summary>
        /// Gets the current column within the source code.
        /// </summary>
        public Int32 Column
        {
            get { return _column; }
        }

        /// <summary>
        /// Gets the status of reading the source code, are we beyond the stream?
        /// </summary>
        public Boolean IsEnded
        {
            get { return _current == Specification.EndOfFile; }
        }

        /// <summary>
        /// Gets the current character.
        /// </summary>
        protected Char Current
        {
            get { return _current; }
        }

        /// <summary>
        /// Gets the next character (by advancing and returning the current character).
        /// </summary>
        protected Char Next
        {
            get { Advance(); return _current; }
        }

        /// <summary>
        /// Gets the previous character (by rewinding and returning the current character).
        /// </summary>
        protected Char Previous
        {
            get { Back(); return _current; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Resets the insertion point to the end of the buffer.
        /// </summary>
        public void ResetInsertionPoint()
        {
            InsertionPoint = _reader.Length;
        }

        /// <summary>
        /// Advances one character in the source code.
        /// </summary>
        protected void Advance()
        {
            if (!IsEnded)
                AdvanceUnsafe();
        }

        /// <summary>
        /// Advances n characters in the source code.
        /// </summary>
        /// <param name="n">The number of characters to advance.</param>
        protected void Advance(Int32 n)
        {
            while (n-- > 0 && !IsEnded)
                AdvanceUnsafe();
        }

        /// <summary>
        /// Advances one character in the source code.
        /// </summary>
        /// <param name="cancelToken">The cancellation token to use.</param>
        /// <returns>The task to await.</returns>
        protected async Task Advance(CancellationToken cancelToken)
        {
            if (!IsEnded)
                await AdvanceUnsafeAsync(cancelToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Advances n characters in the source code.
        /// </summary>
        /// <param name="n">The number of characters to advance.</param>
        /// <param name="cancelToken">The cancellation token to use.</param>
        /// <returns>The task to await.</returns>
        protected async Task Advance(Int32 n, CancellationToken cancelToken)
        {
            while (n-- > 0 && !IsEnded)
                await AdvanceUnsafeAsync(cancelToken).ConfigureAwait(false);
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

        /// <summary>
        /// Looks if the current character / next characters match a certain string.
        /// </summary>
        /// <param name="s">The string to compare to.</param>
        /// <param name="ignoreCase">Optional flag to set the case sensitivity.</param>
        /// <returns>The status of the check.</returns>
        protected Boolean ContinuesWith(String s, Boolean ignoreCase = true)
        {
            var mark = _reader.Index;
            _reader.Index--;
            var content = _reader.ReadCharacters(s.Length);
            _reader.Index = mark;
            return content.Length == s.Length && content.Equals(s, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
        }

        /// <summary>
        /// Looks if the current character / next characters match a certain string.
        /// </summary>
        /// <param name="s">The string to compare to.</param>
        /// <param name="cancelToken">The cancellation token to use.</param>
        /// <param name="ignoreCase">Optional flag to set the case sensitivity.</param>
        /// <returns>A task that results in the status of the check.</returns>
        protected async Task<Boolean> ContinuesWithAsync(String s, CancellationToken cancelToken, Boolean ignoreCase = true)
        {
            var mark = _reader.Index;
            _reader.Index--;
            var content = await _reader.ReadCharactersAsync(s.Length, cancelToken).ConfigureAwait(false);
            _reader.Index = mark;
            return content.Length == s.Length && content.Equals(s, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal);
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
                _collengths.Push(_column);
                _column = 1;
                _row++;
            }
            else
                _column++;

            _current = _reader.ReadCharacter();
        }

        /// <summary>
        /// Just advances one character without checking
        /// if the end is already reached.
        /// </summary>
        /// <param name="cancelToken">The cancellation token to use.</param>
        /// <returns>The task to await.</returns>
        async Task AdvanceUnsafeAsync(CancellationToken cancelToken)
        {
            if (_current.IsLineBreak())
            {
                _collengths.Push(_column);
                _column = 1;
                _row++;
            }
            else
                _column++;

            _current = await _reader.ReadCharacterAsync(cancelToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Just goes back one character without checking
        /// if the beginning is already reached.
        /// </summary>
        void BackUnsafe()
        {
            _reader.Index -= 1;

            if (_reader.Index == 0)
            {
                _column = 0;
                _current = Specification.Null;
                return;
            }

            _current = _reader[_reader.Index - 1];

            if (_current.IsLineBreak())
            {
                _column = _collengths.Count != 0 ? _collengths.Pop() : 1;
                _row--;
            }
            else
                _column--;
        }

        #endregion

        #region IDisposable Implementation

        /// <summary>
        /// Disposes all disposable objects.
        /// </summary>
        public void Dispose()
        {
            var disposable = _reader as IDisposable;

            if (disposable != null)
                disposable.Dispose();
        }

        #endregion
    }
}
