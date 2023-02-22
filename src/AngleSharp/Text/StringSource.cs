namespace AngleSharp.Text
{
    using System;

    /// <summary>
    /// A string abstraction for micro parsers.
    /// </summary>
    public sealed class StringSource
    {
        #region Fields

        private readonly Int32 _last;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new string source from the given content.
        /// </summary>
        public StringSource(String content)
        {
            Content = content ?? String.Empty;
            _last = Content.Length - 1;
            Index = 0;
            Current = _last == -1 ? Symbols.EndOfFile : content![0];
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the current character.
        /// </summary>
        public Char Current { get; private set; }

        /// <summary>
        /// Gets if the content has been fully scanned.
        /// </summary>
        public Boolean IsDone => Current == Symbols.EndOfFile;

        /// <summary>
        /// Gets the current index.
        /// </summary>
        public Int32 Index { get; private set; }

        /// <summary>
        /// Gets the underlying content.
        /// </summary>
        public String Content { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Advances by one character and returns the character.
        /// </summary>
        /// <returns>The next character.</returns>
        public Char Next()
        {
            if (Index == _last)
            {
                Current = Symbols.EndOfFile;
                Index = Content.Length;
            }
            else if (Index < Content.Length)
            {
                Current = Content[++Index];
            }

            return Current;
        }

        /// <summary>
        /// Goes back by one character and returns the character.
        /// </summary>
        /// <returns>The previous character.</returns>
        public Char Back()
        {
            if (Index > 0)
            {
                Current = Content[--Index];
            }

            return Current;
        }

        #endregion
    }
}
