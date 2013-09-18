using System;
using AngleSharp.Xml;
using System.Collections.Generic;
using System.Diagnostics;

namespace AngleSharp.DTD
{
    /// <summary>
    /// The base tokenizer class for Document Type Definitions.
    /// </summary>
    [DebuggerStepThrough]
    class DtdPlainTokenizer : BaseTokenizer
    {
        #region Members

        protected Boolean _external;
        protected IntermediateStream _stream;
        protected DtdContainer _container;

        #endregion

        #region ctor
        
        /// <summary>
        /// Creates a new DTD tokenizer with the given source and container.
        /// </summary>
        /// <param name="container">The container to use.</param>
        /// <param name="src">The source to inspect.</param>
        public DtdPlainTokenizer(DtdContainer container, SourceManager src)
            : base(src)
        {
            _container = container;
            _external = true;
            _stream = new IntermediateStream(src);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Scans the DTD in the doctype as specified in the
        /// official XML spec and (in German) here:
        /// http://www.stefanheymann.de/xml/dtdxml.htm
        /// </summary>
        public DtdToken Get()
        {
            var c = _stream.Current;
            var element = GetElement(c);

            if (element != DtdToken.EOF)
                SkipSpaces(c);

            return element;
        }

        #endregion

        #region States

        protected virtual DtdToken GetElement(Char c)
        {
            if (c == Specification.LT && _stream.ContinuesWith("<?xml"))
            {
                _stream.Advance(4);
                return TextDecl(_stream.Next);
            }
            else if (c != Specification.EOF)
            {
                var s = ScanString(c, Specification.EOF);
                return new DtdCommentToken { Data = s };
            }

            return DtdToken.EOF;
        }

        #endregion

        #region Text Declaration

        /// <summary>
        /// The text declaration for external DTDs.
        /// </summary>
        /// <param name="c">The character.</param>
        /// <returns>The token.</returns>
        protected DtdToken TextDecl(Char c)
        {
            if (_external)
            {
                var token = new DtdDeclToken();

                if (c.IsSpaceCharacter())
                {
                    c = SkipSpaces(c);

                    if (_stream.ContinuesWith(AttributeNames.VERSION))
                    {
                        _stream.Advance(6);
                        return TextDeclVersion(_stream.Next, token);
                    }
                    else if (_stream.ContinuesWith(AttributeNames.ENCODING))
                    {
                        _stream.Advance(7);
                        return TextDeclEncoding(_stream.Next, token);
                    }
                }
            }

            throw Errors.Xml(ErrorCode.DtdTextDeclInvalid);
        }

        /// <summary>
        /// Gets the version specified in the text declaration.
        /// </summary>
        /// <param name="c">The character.</param>
        /// <param name="decl">The current declaration.</param>
        /// <returns>The token.</returns>
        DtdToken TextDeclVersion(Char c, DtdDeclToken decl)
        {
            if (c == Specification.EQ)
            {
                var q = _stream.Next;

                if (q == Specification.DQ || q == Specification.SQ)
                {
                    _stringBuffer.Clear();
                    c = _stream.Next;

                    while (c.IsDigit() || c == Specification.DOT)
                    {
                        _stringBuffer.Append(c);
                        c = _stream.Next;
                    }

                    if (c == q)
                    {
                        decl.Version = _stringBuffer.ToString();
                        return TextDeclBetween(_stream.Next, decl);
                    }
                }
            }

            throw Errors.Xml(ErrorCode.DtdTextDeclInvalid);
        }

        /// <summary>
        /// Between the version and the encoding in the text declaration.
        /// </summary>
        /// <param name="c">The character.</param>
        /// <param name="decl">The current declaration.</param>
        /// <returns>The token.</returns>
        DtdToken TextDeclBetween(Char c, DtdDeclToken decl)
        {
            if (c.IsSpaceCharacter())
            {
                while (c.IsSpaceCharacter())
                    c = _stream.Next;

                if (_stream.ContinuesWith(AttributeNames.ENCODING))
                {
                    _stream.Advance(7);
                    return TextDeclEncoding(_stream.Next, decl);
                }
            }

            throw Errors.Xml(ErrorCode.DtdTextDeclInvalid);
        }

        /// <summary>
        /// Gets the encoding specified in the text declaration.
        /// </summary>
        /// <param name="c">The character.</param>
        /// <param name="decl">The current declaration.</param>
        /// <returns>The token.</returns>
        DtdToken TextDeclEncoding(Char c, DtdDeclToken decl)
        {
            if (c == Specification.EQ)
            {
                var q = _stream.Next;

                if (q == Specification.DQ || q == Specification.SQ)
                {
                    _stringBuffer.Clear();
                    c = _stream.Next;

                    if (c.IsLetter())
                    {
                        do
                        {
                            _stringBuffer.Append(c);
                            c = _stream.Next;
                        }
                        while (c.IsAlphanumericAscii() || c == Specification.DOT || c == Specification.UNDERSCORE || c == Specification.MINUS);
                    }

                    if (c == q)
                    {
                        decl.Encoding = _stringBuffer.ToString();
                        return TextDeclAfter(_stream.Next, decl);
                    }
                }
            }

            throw Errors.Xml(ErrorCode.DtdTextDeclInvalid);
        }

        /// <summary>
        /// After the declaration specified in the text declaration.
        /// </summary>
        /// <param name="c">The character.</param>
        /// <param name="decl">The current declaration.</param>
        /// <returns>The token.</returns>
        DtdToken TextDeclAfter(Char c, DtdDeclToken decl)
        {
            while (c.IsSpaceCharacter())
                c = _stream.Next;

            if (c == Specification.QM)
                return TextDeclEnd(_stream.Next, decl);

            throw Errors.Xml(ErrorCode.DtdTextDeclInvalid);
        }

        /// <summary>
        /// Checks if the text declaration ended correctly.
        /// </summary>
        /// <param name="c">The character.</param>
        /// <param name="decl">The current declaration.</param>
        /// <returns>The token.</returns>
        DtdToken TextDeclEnd(Char c, DtdDeclToken decl)
        {
            if (c == Specification.GT)
                return decl;

            throw Errors.Xml(ErrorCode.DtdTextDeclInvalid);
        }

        #endregion

        #region Intermediate Stream

        protected sealed class IntermediateStream
        {
            #region Members

            Stack<String> _params;
            Stack<Int32> _pos;
            Stack<String> _texts;
            SourceManager _base;
            Int32 _start;
            Int32 _end;

            #endregion

            #region ctor

            public IntermediateStream(SourceManager src)
            {
                _start = src.InsertionPoint - 1;
                _pos = new Stack<Int32>();
                _params = new Stack<String>();
                _texts = new Stack<String>();
                _base = src;
            }

            #endregion

            #region Properties

            /// <summary>
            /// The content (of the original stream).
            /// </summary>
            public String Content
            {
                get { return _base.Copy(_start, _end); }
            }

            /// <summary>
            /// The next character.
            /// </summary>
            public Char Next
            {
                get
                {
                    if (_texts.Count == 0)
                    {
                        _end = _base.InsertionPoint;
                        return _base.Next;
                    }
                    else
                    {
                        _pos.Push(_pos.Pop() + 1);

                        if (_pos.Peek() == _texts.Peek().Length)
                        {
                            _pos.Pop();
                            _texts.Pop();
                            _params.Pop();
                        }
                    }

                    return Current;
                }
            }

            /// <summary>
            /// The current character.
            /// </summary>
            public Char Current
            {
                get { return _texts.Count == 0 ? _base.Current : _texts.Peek()[_pos.Peek()]; }
            }

            #endregion

            #region Methods

            /// <summary>
            /// Pushes the the param entity with its text onto the stack.
            /// </summary>
            /// <param name="param">The param entity to use.</param>
            /// <param name="text">The text to insert.</param>
            public void Push(String param, String text)
            {
                if (_params.Contains(param))
                    throw Errors.Xml(ErrorCode.DtdPEReferenceRecursion);

                Advance();

                if (String.IsNullOrEmpty(text))
                    return;

                _params.Push(param);
                _pos.Push(0);
                _texts.Push(text);
            }

            /// <summary>
            /// Advances by one character.
            /// </summary>
            public void Advance()
            {
                if (_texts.Count != 0)
                {
                    _pos.Push(_pos.Pop() + 1);

                    if (_pos.Peek() == _texts.Peek().Length)
                    {
                        _pos.Pop();
                        _texts.Pop();
                        _params.Pop();
                    }
                }
                else
                    _base.Advance();
            }

            /// <summary>
            /// Advances by n characters.
            /// </summary>
            /// <param name="n">The number of characters to skip.</param>
            public void Advance(Int32 n)
            {
                for (int i = 0; i < n; i++)
                    Advance();
            }

            /// <summary>
            /// Checks if the stream continues with the given word.
            /// </summary>
            /// <param name="word">The word to check for.</param>
            /// <returns>True if it continues, otherwise false.</returns>
            public Boolean ContinuesWith(String word)
            {
                if (_texts.Count == 0)
                    return _base.ContinuesWith(word, false);

                var pos = _pos.Peek();
                var text = _texts.Peek();

                if (text.Length - pos < word.Length)
                    return false;

                for (int i = 0; i < word.Length; i++)
                {
                    if (text[i + pos] != word[i])
                        return false;
                }

                return true;
            }

            #endregion
        }

        #endregion

        #region References

        protected void PEReference(Char c, Boolean use = true)
        {
            var buffer = Pool.NewStringBuilder();

            if (c.IsXmlNameStart())
            {
                do
                {
                    buffer.Append(c);
                    c = _stream.Next;
                }
                while (c.IsXmlName());

                var temp = buffer.ToPool();

                if (c == Specification.SC)
                {
                    var p = _container.GetParameter(temp);

                    if (p != null)
                    {
                        if (use)
                        {
                            _stream.Push(temp, p.NodeValue);
                            return;
                        }
                        else
                            throw Errors.Xml(ErrorCode.DtdPEReferenceInvalid);
                    }
                }
            }

            if (use)
                throw Errors.Xml(ErrorCode.DtdPEReferenceInvalid);

            _stringBuffer.Append(Specification.PERCENT).Append(buffer.ToString());
        }

        protected String EReference(Char c)
        {
            var buffer = Pool.NewStringBuilder();

            if (c.IsXmlNameStart())
            {
                do
                {
                    buffer.Append(c);
                    c = _stream.Next;
                }
                while (c.IsXmlName());

                var temp = buffer.ToPool();

                if (temp.Length > 0 && c == Specification.SC)
                {
                    var p = _container.GetEntity(temp);

                    if (p != null)
                        return p.NodeValue;
                }
            }
            else if (c == Specification.NUM)
            {
                c = _src.Next;
                var hex = c == 'x' || c == 'X';

                if (hex)
                    c = _stream.Next;

                while (hex ? c.IsHex() : c.IsDigit())
                {
                    buffer.Append(c);
                    c = _src.Next;
                }

                var temp = buffer.ToPool();

                if (temp.Length > 0 && c == Specification.SC)
                {
                    var num = hex ? temp.FromHex() : temp.FromDec();

                    if (num.IsValidAsCharRef())
                        return Char.ConvertFromUtf32(num);

                    throw Errors.Xml(ErrorCode.CharacterReferenceInvalidNumber);
                }
            }

            throw Errors.Xml(ErrorCode.CharacterReferenceNotTerminated);
        }

        #endregion

        #region Helper

        protected String ScanString(Char c, Char end)
        {
            var buffer = Pool.NewStringBuilder();

            while (c != end)
            {
                if (c == Specification.EOF)
                {
                    buffer.ToPool();
                    throw Errors.Xml(ErrorCode.EOF);
                }
                else if (c == Specification.PERCENT)
                {
                    PEReference(_stream.Next, _external);
                    c = _stream.Current;
                    continue;
                }
                else if (c == Specification.LT && _stream.ContinuesWith("<![CDATA["))
                {
                    _src.Advance(8);
                    c = _stream.Next;

                    while (true)
                    {
                        if (c == Specification.EOF)
                            throw Errors.Xml(ErrorCode.EOF);

                        if (c == Specification.SBC && _stream.ContinuesWith("]]>"))
                        {
                            _stream.Advance(2);
                            break;
                        }

                        buffer.Append(c);
                        c = _src.Next;
                    }
                }
                else if (c == Specification.AMPERSAND)
                    buffer.Append(EReference(_stream.Next));
                else
                    buffer.Append(c);

                c = _stream.Next;
            }

            return buffer.ToPool();
        }

        protected Char SkipSpaces(Char c)
        {
            do
            {
                c = _stream.Next;
            }
            while (c.IsSpaceCharacter());

            return c;
        }

        #endregion
    }
}
