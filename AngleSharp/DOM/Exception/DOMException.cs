using System;

namespace AngleSharp.DOM
{
    /// <summary>
    /// Represents a DOM exception.
    /// </summary>
    public sealed class DOMException : Exception
    {
        /// <summary>
        /// Creates a new DOMException.
        /// </summary>
        /// <param name="code">The error code.</param>
        internal DOMException(ErrorCode code)
            : base(Errors.GetError(code))
        {
            Code = (Int32)code;
            Name = code.ToString();
        }

        /// <summary>
        /// Creates a new DOMException.
        /// </summary>
        /// <param name="code">The error code.</param>
        public DOMException(Int32 code)
            : base(Errors.GetError((ErrorCode)code))
        {
            Code = code;
            Name = ((ErrorCode)code).ToString();
        }

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
    }
}
