using System;

namespace AngleSharp
{
    /// <summary>
    /// The ParseErrorEventArgs package.
    /// </summary>
    public class ParseErrorEventArgs : ParserEventArgs
    {
        #region ctor

        /// <summary>
        /// Creates a new ErrorEventArgs package.
        /// </summary>
        /// <param name="code">The provided error code.</param>
        /// <param name="msg">The associated error message.</param>
        public ParseErrorEventArgs(int code, string msg)
        {
            ErrorMessage = msg;
            ErrorCode = code;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the provided error code.
        /// </summary>
        public int ErrorCode
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the associated error message.
        /// </summary>
        public string ErrorMessage
        {
            get;
            private set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a string containing the relevant information.
        /// </summary>
        /// <returns>The string containing the error message, error 
        /// code as well as line and column.</returns>
        public override string ToString()
        {
            return string.Format("Ln {0}, Col {1}: ERR{2} ({3}).", Line, Column, ErrorCode, ErrorMessage);
        }

        #endregion
    }
}
