namespace AngleSharp.Parser
{
    using System;
    using System.Diagnostics;

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
        /// <param name="start">The start position.</param>
        /// <param name="end">The end position.</param>
        internal ParseErrorEventArgs(Int32 code, String msg, TextPosition start, TextPosition end)
        {
            ErrorMessage = msg;
            ErrorCode = code;
            Start = start;
            End = end;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the start position of the error.
        /// </summary>
        internal TextPosition Start
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the end position of the error.
        /// </summary>
        internal TextPosition End
        {
            get;
            private set;
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
            return String.Format("Ln {0}, Col {1}: ERR{2} ({3}).", Start.Line.ToString(), Start.Column.ToString(), ErrorCode.ToString(), ErrorMessage);
        }

        #endregion
    }
}
