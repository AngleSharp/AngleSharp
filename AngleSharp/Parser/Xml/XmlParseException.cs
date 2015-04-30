namespace AngleSharp.Parser.Xml
{
    using System;

    /// <summary>
    /// Exception that is thrown if an ill-formatted XML document is parsed.
    /// </summary>
    public class XmlParseException : Exception
    {
        #region ctor

        /// <summary>
        /// Creates a new XmlParseException.
        /// </summary>
        /// <param name="code">The provided error code.</param>
        /// <param name="message">The associated error message.</param>
        /// <param name="position">The position in the source.</param>
        /// 
        public XmlParseException(Int32 code, String message, TextPosition position)
            : base(message)
        {
            Code = code;
            Position = position;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the position of the error.
        /// </summary>
        public TextPosition Position
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the provided error code.
        /// </summary>
        public Int32 Code
        {
            get;
            private set;
        }

        #endregion
    }
}
