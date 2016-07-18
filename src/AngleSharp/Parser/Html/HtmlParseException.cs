namespace AngleSharp.Parser.Html
{
    using System;

    /// <summary>
    /// Exception that is thrown if an ill-formatted HTML document is parsed
    /// in strict mode.
    /// </summary>
    public class HtmlParseException : Exception
    {
        #region ctor

        /// <summary>
        /// Creates a new HtmlParseException.
        /// </summary>
        /// <param name="code">The provided error code.</param>
        /// <param name="message">The associated error message.</param>
        /// <param name="position">The position in the source.</param>
        /// 
        public HtmlParseException(Int32 code, String message, TextPosition position)
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
