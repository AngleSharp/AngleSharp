using System;
using System.Diagnostics;

namespace AngleSharp
{
    /// <summary>
    /// The ParseErrorEventArgs package.
    /// </summary>
    [DebuggerStepThrough]
    public sealed class ParseErrorEventArgs : EventArgs
    {
        #region ctor

        /// <summary>
        /// Creates a new ErrorEventArgs package.
        /// </summary>
        /// <param name="code">The provided error code.</param>
        /// <param name="msg">The associated error message.</param>
        public ParseErrorEventArgs(Int32 code, String msg)
        {
            ErrorMessage = msg;
            ErrorCode = code;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the line within the document.
        /// </summary>
        public Int32 Line
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the column within the document.
        /// </summary>
        public Int32 Column
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the provided error code.
        /// </summary>
        public Int32 ErrorCode
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the associated error message.
        /// </summary>
        public String ErrorMessage
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
        public override String ToString()
        {
            return String.Format("Ln {0}, Col {1}: ERR{2} ({3}).", Line, Column, ErrorCode, ErrorMessage);
        }

        #endregion
    }
}
