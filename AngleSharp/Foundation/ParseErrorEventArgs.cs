using System;

namespace AngleSharp
{
    /// <summary>
    /// The ParseErrorEventArgs package.
    /// </summary>
    public sealed class ParseErrorEventArgs : EventArgs
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
        /// Gets or sets the line within the document.
        /// </summary>
        public int Line
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the column within the document.
        /// </summary>
        public int Column
        {
            get;
            set;
        }

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
