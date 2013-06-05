using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace AngleSharp
{
    /// <summary>
    /// Represents the HTML source code manager.
    /// </summary>
    sealed class HtmlSource
    {
        #region Members

        int _column;
        int _row;
        int _insertion;
        Stack<int> _collengths;
        char _current;
        TextReader _reader;
        StringBuilder _buffer;
        bool _ended;
        bool _lwcr;
        Encoding _encoding;

        #endregion

        #region Constructor

        /// <summary>
        /// Prepares everything
        /// </summary>
        private HtmlSource()
        {
            _encoding = HtmlEncoding.Suggest(LocalSettings.Language);
            _buffer = new StringBuilder();
            _collengths = new Stack<int>();
            _column = 1;
            _row = 1;
        }

        /// <summary>
        /// Constructs a new instance of the source code manager.
        /// </summary>
        /// <param name="html">The source code string to manage.</param>
        public HtmlSource(string html)
            : this()
        {
            _reader = new StringReader(html);
            ReadCurrent();
        }

        /// <summary>
        /// Constructs a new instance of the source code manager.
        /// </summary>
        /// <param name="stream">The source code stream to manage.</param>
        public HtmlSource(Stream stream)
            : this()
        {
            _reader = new StreamReader(stream, true);
            ReadCurrent();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the position is at the moment at the beginning.
        /// </summary>
        public bool IsBeginning 
        { 
            get { return _insertion < 2; } 
        }

        /// <summary>
        /// Gets or sets the encoding to use.
        /// </summary>
        public Encoding Encoding
        {
            get { return _encoding; }
            set
            {
                _encoding = value;

                if (_reader is StreamReader)
                {
                    var chars = _buffer.Length;
                    var stream = ((StreamReader)_reader).BaseStream;
                    _insertion = 0;
                    stream.Position = 0;
                    _buffer.Clear();
                    _reader = new StreamReader(stream, value);
                    Advance(chars);
                }
            }
        }

        /// <summary>
        /// Gets or sets the insertion point.
        /// </summary>
        public int InsertionPoint
        {
            get { return _insertion; }
            set 
            {
                if (value >= 0 && value <= _buffer.Length)
                {
                    var delta = _insertion - value;

                    if (delta > 0)
                    {
                        while (_insertion != value)
                            BackUnsafe();
                    }
                    else if (delta < 0)
                    {
                        while (_insertion != value)
                            AdvanceUnsafe();
                    }
                }
            }
        }

        /// <summary>
        /// Gets the current line within the source code.
        /// </summary>
        public int Line
        {
            get { return _row; }
        }

        /// <summary>
        /// Gets the current column within the source code.
        /// </summary>
        public int Column
        {
            get { return _column; }
        }

        /// <summary>
        /// Gets the status of reading the source code, are we beyond the stream?
        /// </summary>
        public bool IsEnded
        {
            get { return _ended; }
        }

        /// <summary>
        /// Gets the status of reading the source code, is the EOF currently given?
        /// </summary>
        public bool IsEnding
        {
            get { return _current == Specification.EOF; }
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
        /// Resets the insertion point to the end of the buffer.
        /// </summary>
        /// <returns></returns>
        public HtmlSource ResetInsertionPoint()
        {
            InsertionPoint = _buffer.Length;
            return this;
        }

        /// <summary>
        /// Advances one character in the source code.
        /// </summary>
        /// <returns>The current source manager.</returns>
        public HtmlSource Advance()
        {
            if (!IsEnding)
                AdvanceUnsafe();
            else if (!_ended)
                _ended = true;

            return this;
        }

        /// <summary>
        /// Advances n characters in the source code.
        /// </summary>
        /// <param name="n">The number of characters to advance.</param>
        /// <returns>The current source manager.</returns>
        public HtmlSource Advance(int n)
        {
            while(n-- > 0 && !IsEnding)
                AdvanceUnsafe();

            return this;
        }

        /// <summary>
        /// Moves back one character in the source code.
        /// </summary>
        /// <returns>The current source manager.</returns>
        public HtmlSource Back()
        {
            _ended = false;

            if (!IsBeginning)
                BackUnsafe();

            return this;
        }

        /// <summary>
        /// Moves back n characters in the source code.
        /// </summary>
        /// <param name="n">The number of characters to rewind.</param>
        /// <returns>The current source manager.</returns>
        public HtmlSource Back(int n)
        {
            _ended = false;

            while (n-- > 0 && !IsBeginning)
                BackUnsafe();

            return this;
        }

        /// <summary>
        /// Looks if the current character / next characters match a certain string.
        /// </summary>
        /// <param name="s">The string to compare to.</param>
        /// <param name="ignoreCase">Optional flag to unignore the case sensitivity.</param>
        /// <returns>The status of the check.</returns>
        public bool ContinuesWith(string s, bool ignoreCase = true)
        {
            for (var index = 0; index < s.Length; index++)
            {
                var chr = _current;

                if (ignoreCase && Specification.IsUppercaseAscii(chr))
                    chr = chr.ToLower();

                if (s[index] != chr)
                {
                    Back(index);
                    return false;
                }

                Advance();
            }

            Back(s.Length);
            return true;
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Reads the current character (from the stream or
        /// from the buffer).
        /// </summary>
        void ReadCurrent()
        {
            if (_insertion < _buffer.Length)
            {
                _current = _buffer[_insertion];
                _insertion++;
                return;
            }

            var tmp = _reader.Read();

            if (tmp == -1)
            {
                _current = Specification.EOF;
                return;
            }

            _current = (char)tmp;

            if (_current == Specification.CR)
            {
                _current = Specification.LF;
                _lwcr = true;
            }
            else if (_lwcr)
            {
                _lwcr = false;

                if (_current == Specification.LF)
                {
                    ReadCurrent();
                    return;
                }
            }

            _buffer.Append(_current);
            _insertion++;
        }

        /// <summary>
        /// Just advances one character without checking
        /// if the end is already reached.
        /// </summary>
        void AdvanceUnsafe()
        {
            if (Specification.IsLineBreak(_current))
            {
                _collengths.Push(_column);
                _column = 1;
                _row++;
            }
            else
                _column++;

            ReadCurrent();
        }

        /// <summary>
        /// Just goes back one character without checking
        /// if the beginning is already reached.
        /// </summary>
        void BackUnsafe()
        {
            _insertion--;

            if (_insertion == 0)
            {
                _column = 0;
                _current = Specification.NULL;
                return;
            }

            _current = _buffer[_insertion - 1];

            if (Specification.IsLineBreak(_current))
            {
                _column = _collengths.Count != 0 ? _collengths.Pop() : 1;
                _row--;
            }
            else
                _column--;
        }

        #endregion
    }
}
