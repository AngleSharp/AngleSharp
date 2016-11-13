namespace AngleSharp.Text
{
    using System;

    /// <summary>
    /// A string abstraction for micro parsers.
    /// </summary>
    public sealed class StringSource
    {
        private readonly String _content;
        private readonly Int32 _last;
        private Int32 _index;
        private Char _current;

        /// <summary>
        /// Creates a new string source from the given content.
        /// </summary>
        public StringSource(String content)
        {
            _content = content ?? String.Empty;
            _last = _content.Length - 1;
            _index = 0;
            _current = _last == -1 ? Symbols.EndOfFile : content[0];
        }

        /// <summary>
        /// Gets the current character.
        /// </summary>
        public Char Current
        {
            get { return _current; }
        }

        /// <summary>
        /// Gets the current index.
        /// </summary>
        public Int32 Index
        {
            get { return _index; }
        }

        /// <summary>
        /// Gets the underlying content.
        /// </summary>
        public String Content
        {
            get { return _content; }
        }

        /// <summary>
        /// Advances by one character and returns the character.
        /// </summary>
        /// <returns>The next character.</returns>
        public Char Next()
        {
            if (_index == _last)
            {
                _current = Symbols.EndOfFile;
                _index = _content.Length;
            }
            else if (_index < _content.Length)
            {
                _current = _content[++_index];
            }

            return _current;
        }

        /// <summary>
        /// Goes back by one character and returns the character.
        /// </summary>
        /// <returns>The previous character.</returns>
        public Char Back()
        {
            if (_index > 0)
            {
                _current = _content[--_index];
            }

            return _current;
        }
    }
}
