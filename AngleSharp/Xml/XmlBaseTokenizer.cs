using System;

namespace AngleSharp.Xml
{
    /// <summary>
    /// An intermediate step to the Xml tokenizer.
    /// </summary>
    abstract class XmlBaseTokenizer : BaseTokenizer
    {
        #region Constants

        protected const String PUBLIC = "PUBLIC";
        protected const String SYSTEM = "SYSTEM";

        #endregion

        #region ctor

        public XmlBaseTokenizer(SourceManager src)
            : base(src)
        {
        }

        #endregion

        #region Processing Instruction

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#sec-pi.
        /// </summary>
        /// <param name="c">The next input character.</param>
        protected XmlToken ProcessingStart(Char c)
        {
            if (c.IsLetter())
            {
                _stringBuffer.Clear();
                _stringBuffer.Append(c);
                return ProcessingTarget(_src.Next, XmlToken.Processing());
            }
            else
            {
                RaiseErrorOccurred(ErrorCode.AmbiguousOpenTag);
                throw new ArgumentException("Invalid processing instruction.");
            }
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#sec-pi.
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="pi">The processing instruction token.</param>
        protected XmlToken ProcessingTarget(Char c, XmlPIToken pi)
        {
            while (true)
            {
                if (c.IsSpaceCharacter())
                {
                    pi.Target = _stringBuffer.ToString();
                    _stringBuffer.Clear();
                    return ProcessingContent(_src.Next, pi);
                }
                else if (c == Specification.QM)
                {
                    pi.Target = _stringBuffer.ToString();
                    _stringBuffer.Clear();
                    return ProcessingContent(c, pi);
                }
                else if (c == Specification.NULL)
                {
                    RaiseErrorOccurred(ErrorCode.NULL);
                    c = Specification.REPLACEMENT;
                }
                else if (c == Specification.EOF)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    return XmlToken.EOF;
                }

                _stringBuffer.Append(c);
                c = _src.Next;
            }
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#sec-pi.
        /// </summary>
        /// <param name="c">The next input character.</param>
        /// <param name="pi">The processing instruction token.</param>
        XmlToken ProcessingContent(Char c, XmlPIToken pi)
        {
            while (true)
            {
                if (c == Specification.QM)
                {
                    c = _src.Next;

                    if (c == Specification.GT)
                    {
                        pi.Content = _stringBuffer.ToString();
                        return pi;
                    }

                    _stringBuffer.Append(Specification.QM);
                }
                else if (c == Specification.EOF)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    return XmlToken.EOF;
                }
                else
                {
                    _stringBuffer.Append(c);
                    c = _src.Next;
                }
            }
        }

        #endregion

        #region Comments

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#sec-comments.
        /// </summary>
        /// <param name="c">The next input character.</param>
        protected XmlToken CommentStart(Char c)
        {
            _stringBuffer.Clear();

            if (c == Specification.MINUS)
                return CommentDashStart(_src.Next);
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                _stringBuffer.Append(Specification.REPLACEMENT);
                return Comment(_src.Next);
            }
            else if (c == Specification.GT)
            {
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                return XmlToken.Comment(_stringBuffer.ToString());
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                _src.Back();
                return XmlToken.Comment(_stringBuffer.ToString());
            }
            else
            {
                _stringBuffer.Append(c);
                return Comment(_src.Next);
            }
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#sec-comments.
        /// </summary>
        /// <param name="c">The next input character.</param>
        XmlToken CommentDashStart(Char c)
        {
            if (c == Specification.MINUS)
                return CommentEnd(_src.Next);
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                _stringBuffer.Append(Specification.MINUS);
                _stringBuffer.Append(Specification.REPLACEMENT);
                return Comment(_src.Next);
            }
            else if (c == Specification.GT)
            {
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                return XmlToken.Comment(_stringBuffer.ToString());
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                _src.Back();
                return XmlToken.Comment(_stringBuffer.ToString());
            }

            _stringBuffer.Append(Specification.MINUS);
            _stringBuffer.Append(c);
            return Comment(_src.Next);
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#sec-comments.
        /// </summary>
        /// <param name="c">The next input character.</param>
        XmlToken Comment(Char c)
        {
            while (true)
            {
                if (c == Specification.MINUS)
                    return CommentDashEnd(_src.Next);
                else if (c == Specification.EOF)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    _src.Back();
                    return XmlToken.Comment(_stringBuffer.ToString());
                }
                else if (c == Specification.NULL)
                {
                    RaiseErrorOccurred(ErrorCode.NULL);
                    c = Specification.REPLACEMENT;
                }

                _stringBuffer.Append(c);
                c = _src.Next;
            }
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#sec-comments.
        /// </summary>
        /// <param name="c">The next input character.</param>
        XmlToken CommentDashEnd(Char c)
        {
            if (c == Specification.MINUS)
                return CommentEnd(_src.Next);
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                _src.Back();
                return XmlToken.Comment(_stringBuffer.ToString());
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                c = Specification.REPLACEMENT;
            }

            _stringBuffer.Append(Specification.MINUS);
            _stringBuffer.Append(c);
            return Comment(_src.Next);
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#sec-comments.
        /// </summary>
        /// <param name="c">The next input character.</param>
        XmlToken CommentEnd(Char c)
        {
            if (c == Specification.GT)
            {
                return XmlToken.Comment(_stringBuffer.ToString());
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                _stringBuffer.Append(Specification.MINUS);
                _stringBuffer.Append(Specification.REPLACEMENT);
                return Comment(_src.Next);
            }
            else if (c == Specification.EM)
            {
                RaiseErrorOccurred(ErrorCode.CommentEndedWithEM);
                return CommentBangEnd(_src.Next);
            }
            else if (c == Specification.MINUS)
            {
                RaiseErrorOccurred(ErrorCode.CommentEndedWithDash);
                _stringBuffer.Append(Specification.MINUS);
                return CommentEnd(_src.Next);
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                _src.Back();
                return XmlToken.Comment(_stringBuffer.ToString());
            }

            RaiseErrorOccurred(ErrorCode.CommentEndedUnexpected);
            _stringBuffer.Append(Specification.MINUS);
            _stringBuffer.Append(Specification.MINUS);
            _stringBuffer.Append(c);
            return Comment(_src.Next);
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#sec-comments.
        /// </summary>
        /// <param name="c">The next input character.</param>
        XmlToken CommentBangEnd(Char c)
        {
            if (c == Specification.MINUS)
            {
                _stringBuffer.Append(Specification.MINUS);
                _stringBuffer.Append(Specification.MINUS);
                _stringBuffer.Append(Specification.EM);
                return CommentDashEnd(_src.Next);
            }
            else if (c == Specification.GT)
            {
                return XmlToken.Comment(_stringBuffer.ToString());
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                _stringBuffer.Append(Specification.MINUS);
                _stringBuffer.Append(Specification.MINUS);
                _stringBuffer.Append(Specification.EM);
                _stringBuffer.Append(Specification.REPLACEMENT);
                return Comment(_src.Next);
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                _src.Back();
                return XmlToken.Comment(_stringBuffer.ToString());
            }

            _stringBuffer.Append(Specification.MINUS);
            _stringBuffer.Append(Specification.MINUS);
            _stringBuffer.Append(Specification.EM);
            _stringBuffer.Append(c);
            return Comment(_src.Next);
        }

        #endregion
    }
}
