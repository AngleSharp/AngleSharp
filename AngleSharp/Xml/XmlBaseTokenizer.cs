using System;

namespace AngleSharp.Xml
{
    /// <summary>
    /// An intermediate step to the Xml tokenizer.
    /// </summary>
    abstract class XmlBaseTokenizer : BaseTokenizer
    {
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
                stringBuffer.Clear();
                stringBuffer.Append(c);
                return ProcessingTarget(src.Next, XmlToken.Processing());
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
                    pi.Target = stringBuffer.ToString();
                    stringBuffer.Clear();
                    return ProcessingContent(src.Next, pi);
                }
                else if (c == Specification.QM)
                {
                    pi.Target = stringBuffer.ToString();
                    stringBuffer.Clear();
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

                stringBuffer.Append(c);
                c = src.Next;
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
                    c = src.Next;

                    if (c == Specification.GT)
                    {
                        pi.Content = stringBuffer.ToString();
                        return pi;
                    }

                    stringBuffer.Append(Specification.QM);
                }
                else if (c == Specification.EOF)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    return XmlToken.EOF;
                }
                else
                {
                    stringBuffer.Append(c);
                    c = src.Next;
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
            stringBuffer.Clear();

            if (c == Specification.MINUS)
                return CommentDashStart(src.Next);
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                stringBuffer.Append(Specification.REPLACEMENT);
                return Comment(src.Next);
            }
            else if (c == Specification.GT)
            {
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                return XmlToken.Comment(stringBuffer.ToString());
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                src.Back();
                return XmlToken.Comment(stringBuffer.ToString());
            }
            else
            {
                stringBuffer.Append(c);
                return Comment(src.Next);
            }
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#sec-comments.
        /// </summary>
        /// <param name="c">The next input character.</param>
        XmlToken CommentDashStart(Char c)
        {
            if (c == Specification.MINUS)
                return CommentEnd(src.Next);
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                stringBuffer.Append(Specification.MINUS);
                stringBuffer.Append(Specification.REPLACEMENT);
                return Comment(src.Next);
            }
            else if (c == Specification.GT)
            {
                RaiseErrorOccurred(ErrorCode.TagClosedWrong);
                return XmlToken.Comment(stringBuffer.ToString());
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                src.Back();
                return XmlToken.Comment(stringBuffer.ToString());
            }

            stringBuffer.Append(Specification.MINUS);
            stringBuffer.Append(c);
            return Comment(src.Next);
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
                    return CommentDashEnd(src.Next);
                else if (c == Specification.EOF)
                {
                    RaiseErrorOccurred(ErrorCode.EOF);
                    src.Back();
                    return XmlToken.Comment(stringBuffer.ToString());
                }
                else if (c == Specification.NULL)
                {
                    RaiseErrorOccurred(ErrorCode.NULL);
                    c = Specification.REPLACEMENT;
                }

                stringBuffer.Append(c);
                c = src.Next;
            }
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#sec-comments.
        /// </summary>
        /// <param name="c">The next input character.</param>
        XmlToken CommentDashEnd(Char c)
        {
            if (c == Specification.MINUS)
                return CommentEnd(src.Next);
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                src.Back();
                return XmlToken.Comment(stringBuffer.ToString());
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                c = Specification.REPLACEMENT;
            }

            stringBuffer.Append(Specification.MINUS);
            stringBuffer.Append(c);
            return Comment(src.Next);
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#sec-comments.
        /// </summary>
        /// <param name="c">The next input character.</param>
        XmlToken CommentEnd(Char c)
        {
            if (c == Specification.GT)
            {
                return XmlToken.Comment(stringBuffer.ToString());
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                stringBuffer.Append(Specification.MINUS);
                stringBuffer.Append(Specification.REPLACEMENT);
                return Comment(src.Next);
            }
            else if (c == Specification.EM)
            {
                RaiseErrorOccurred(ErrorCode.CommentEndedWithEM);
                return CommentBangEnd(src.Next);
            }
            else if (c == Specification.MINUS)
            {
                RaiseErrorOccurred(ErrorCode.CommentEndedWithDash);
                stringBuffer.Append(Specification.MINUS);
                return CommentEnd(src.Next);
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                src.Back();
                return XmlToken.Comment(stringBuffer.ToString());
            }

            RaiseErrorOccurred(ErrorCode.CommentEndedUnexpected);
            stringBuffer.Append(Specification.MINUS);
            stringBuffer.Append(Specification.MINUS);
            stringBuffer.Append(c);
            return Comment(src.Next);
        }

        /// <summary>
        /// More http://www.w3.org/TR/REC-xml/#sec-comments.
        /// </summary>
        /// <param name="c">The next input character.</param>
        XmlToken CommentBangEnd(Char c)
        {
            if (c == Specification.MINUS)
            {
                stringBuffer.Append(Specification.MINUS);
                stringBuffer.Append(Specification.MINUS);
                stringBuffer.Append(Specification.EM);
                return CommentDashEnd(src.Next);
            }
            else if (c == Specification.GT)
            {
                return XmlToken.Comment(stringBuffer.ToString());
            }
            else if (c == Specification.NULL)
            {
                RaiseErrorOccurred(ErrorCode.NULL);
                stringBuffer.Append(Specification.MINUS);
                stringBuffer.Append(Specification.MINUS);
                stringBuffer.Append(Specification.EM);
                stringBuffer.Append(Specification.REPLACEMENT);
                return Comment(src.Next);
            }
            else if (c == Specification.EOF)
            {
                RaiseErrorOccurred(ErrorCode.EOF);
                src.Back();
                return XmlToken.Comment(stringBuffer.ToString());
            }

            stringBuffer.Append(Specification.MINUS);
            stringBuffer.Append(Specification.MINUS);
            stringBuffer.Append(Specification.EM);
            stringBuffer.Append(c);
            return Comment(src.Next);
        }

        #endregion
    }
}
