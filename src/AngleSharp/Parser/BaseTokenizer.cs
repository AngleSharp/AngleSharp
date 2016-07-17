namespace AngleSharp.Parser
{
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Common methods and variables of all tokenizers.
    /// </summary>
    abstract class BaseTokenizer : IDisposable
    {
        #region Fields

        readonly Stack<UInt16> _columns;
        readonly TextSource _source;

        UInt16 _column;
        UInt16 _row;
        Char _current;

        #endregion

        #region ctor

        public BaseTokenizer(TextSource source)
        {
            StringBuffer = Pool.NewStringBuilder();
            _columns = new Stack<UInt16>();
            _source = source;
            _current = Symbols.Null;
            _column = 0;
            _row = 1;
        }

        #endregion

        #region Properties

        protected StringBuilder StringBuffer 
        { 
            get; 
            private set; 
        }

        public TextSource Source
        {
            get { return _source; }
        }

        public Int32 InsertionPoint
        {
            get { return _source.Index; }
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

        public UInt16 Line
        {
            get { return _row; }
        }

        public UInt16 Column
        {
            get { return _column; }
        }

        public Int32 Position
        {
            get { return _source.Index; }
        }

        protected Char Current
        {
            get { return _current; }
        }

        #endregion

        #region Methods

        public String FlushBuffer()
        {
            var content = StringBuffer.ToString();
            StringBuffer.Clear();
            return content;
        }

        public void Dispose()
        {
            var isDisposed = StringBuffer == null;

            if (!isDisposed)
            {
                var disposable = _source as IDisposable;
                disposable?.Dispose();
                StringBuffer.Clear().ToPool();
                StringBuffer = null;
            }
        }

        public TextPosition GetCurrentPosition()
        {
            return new TextPosition(_row, _column, Position);
        }

        protected Boolean ContinuesWithInsensitive(String s)
        {
            var content = PeekString(s.Length);
            return content.Length == s.Length && content.Isi(s);
        }

        protected Boolean ContinuesWithSensitive(String s)
        {
            var content = PeekString(s.Length);
            return content.Length == s.Length && content.Isi(s);
        }

        protected String PeekString(Int32 length)
        {
            var mark = _source.Index;
            _source.Index--;
            var content = _source.ReadCharacters(length);
            _source.Index = mark;
            return content;
        }

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

        protected Char GetNext()
        {
            Advance();
            return _current;
        }

        protected Char GetPrevious()
        {
            Back();
            return _current;
        }

        protected void Advance()
        {
            if (_current != Symbols.EndOfFile)
            {
                AdvanceUnsafe();
            }
        }

        protected void Advance(Int32 n)
        {
            while (n-- > 0 && _current != Symbols.EndOfFile)
            {
                AdvanceUnsafe();
            }
        }

        protected void Back()
        {
            if (InsertionPoint > 0)
            {
                BackUnsafe();
            }
        }

        protected void Back(Int32 n)
        {
            while (n-- > 0 && InsertionPoint > 0)
            {
                BackUnsafe();
            }
        }

        #endregion

        #region Helpers

        void AdvanceUnsafe()
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

        void BackUnsafe()
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

        Char NormalizeForward(Char p)
        {
            if (p != Symbols.CarriageReturn)
            {
                return p;
            }
            else if (_source.ReadCharacter() != Symbols.LineFeed)
            {
                _source.Index--;
            }
            
            return Symbols.LineFeed;
        }

        Char NormalizeBackward(Char p)
        {
            if (p != Symbols.CarriageReturn)
            {
                return p;
            }
            else if (_source.Index < _source.Length && _source[_source.Index] == Symbols.LineFeed)
            {
                BackUnsafe();
                return Symbols.Null;
            }

            return Symbols.LineFeed;
        }

        #endregion
    }
}
