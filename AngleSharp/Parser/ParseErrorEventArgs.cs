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
        /// <param name="position">The position in the source.</param>
        public ParseErrorEventArgs(Int32 code, String msg, TextPosition position)
        {
            ErrorMessage = msg;
            ErrorCode = code;
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
            return String.Format("Ln {0}, Col {1}: ERR{2} ({3}).", Position.Line.ToString(), Position.Column.ToString(), ErrorCode.ToString(), ErrorMessage);
        }

        #endregion
    }
}
