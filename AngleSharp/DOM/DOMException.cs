namespace AngleSharp.DOM
{
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
        internal DomException(ErrorCode code)
            : base(code.GetErrorMessage())
        {
            Code = (Int32)code;
            Name = code.ToString();
        }

        /// <summary>
        /// Creates a new DOMException.
        /// </summary>
        /// <param name="code">The error code.</param>
        internal DomException(Int32 code)
            : this((ErrorCode)code)
        {
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
