using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;

namespace AngleSharp
{
    /// <summary>
    /// Represents the source code manager.
    /// </summary>
    [DebuggerStepThrough]
    sealed class SourceManager
    {
        #region Members

        Int32 _column;
        Int32 _row;
        Int32 _insertion;
        Stack<Int32> _collengths;
        Char _current;
        TextReader _reader;
        StringBuilder _buffer;
        Boolean _ended;
        Boolean _lwcr;
        Encoding _encoding;

        #endregion

        #region ctor

        /// <summary>
        /// Prepares everything.
        /// </summary>
        SourceManager()
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
        /// <param name="source">The source code string to manage.</param>
        public SourceManager(String source)
            : this()
        {
            _reader = new StringReader(source);
            ReadCurrent();
        }

        /// <summary>
        /// Constructs a new instance of the source code manager.
        /// </summary>
        /// <param name="stream">The source code stream to manage.</param>
        public SourceManager(Stream stream)
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
        public Boolean IsBeginning 
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
        public Int32 InsertionPoint
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
            get { return _ended; }
        }

        /// <summary>
        /// Gets the status of reading the source code, is the EOF currently given?
        /// </summary>
        public Boolean IsEnding
        {
            get { return _current == Specification.EOF; }
        }

        /// <summary>
        /// Gets the current character.
        /// </summary>
        public Char Current
        {
            get { return _current; }
        }

        /// <summary>
        /// Gets the next character (by advancing and returning the current character).
        /// </summary>
        [DebuggerHidden]
        public Char Next
        {
            get { Advance(); return _current; }
        }

        /// <summary>
        /// Gets the previous character (by rewinding and returning the current character).
        /// </summary>
        [DebuggerHidden]
        public Char Previous
        {
            get { Back(); return _current; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Resets the insertion point to the end of the buffer.
        /// </summary>
        [DebuggerStepThrough]
        public void ResetInsertionPoint()
        {
            InsertionPoint = _buffer.Length;
        }

        /// <summary>
        /// Advances one character in the source code.
        /// </summary>
        /// <returns>The current source manager.</returns>
        [DebuggerStepThrough]
        public void Advance()
        {
            if (!IsEnding)
                AdvanceUnsafe();
            else if (!_ended)
                _ended = true;
        }

        /// <summary>
        /// Advances n characters in the source code.
        /// </summary>
        /// <param name="n">The number of characters to advance.</param>
        [DebuggerStepThrough]
        public void Advance(Int32 n)
        {
            while(n-- > 0 && !IsEnding)
                AdvanceUnsafe();
        }

        /// <summary>
        /// Moves back one character in the source code.
        /// </summary>
        [DebuggerStepThrough]
        public void Back()
        {
            _ended = false;

            if (!IsBeginning)
                BackUnsafe();
        }

        /// <summary>
        /// Moves back n characters in the source code.
        /// </summary>
        /// <param name="n">The number of characters to rewind.</param>
        [DebuggerStepThrough]
        public void Back(Int32 n)
        {
            _ended = false;

            while (n-- > 0 && !IsBeginning)
                BackUnsafe();
        }

        /// <summary>
        /// Looks if the current character / next characters match a certain string.
        /// </summary>
        /// <param name="s">The string to compare to.</param>
        /// <param name="ignoreCase">Optional flag to unignore the case sensitivity.</param>
        /// <returns>The status of the check.</returns>
        [DebuggerStepThrough]
        public bool ContinuesWith(String s, Boolean ignoreCase = true)
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
        [DebuggerStepThrough]
        void ReadCurrent()
        {
            if (_insertion < _buffer.Length)
            {
                _current = _buffer[_insertion];
                _insertion++;
                return;
            }

            var tmp = _reader.Read();
            _current = tmp == -1 ? Specification.EOF : (Char)tmp;

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
        [DebuggerStepThrough]
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
        [DebuggerStepThrough]
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
