namespace AngleSharp.Dom
{
    using AngleSharp.Common;
    using System;

    /// <summary>
    /// Represents a DOM exception.
    /// </summary>
    public sealed class DomException : Exception, IDomException
    {
        #region ctor

        /// <summary>
        /// Creates a new DOMException.
        /// </summary>
        /// <param name="code">The error code.</param>
        public DomException(DomError code)
            : base(code.GetMessage())
        {
            Code = (Int32)code;
            Name = code.ToString();
        }

        /// <summary>
        /// Creates a new DOMException with a custom message.
        /// </summary>
        /// <param name="message">The message to transport.</param>
        public DomException(String message)
        {
            Code = 0;
            Name = message;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of the error.
        /// </summary>
        public String Name
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the error code for this exception.
        /// </summary>
        public Int32 Code
        {
            get;
            private set;
        }

        #endregion
    }
}
