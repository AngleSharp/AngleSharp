namespace AngleSharp.DOM
{
    using System;

    /// <summary>
    /// Represents a DOM exception.
    /// </summary>
    [DOM("DOMException")]
    public sealed class DOMException : Exception
    {
        #region ctor

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
        internal DOMException(Int32 code)
            : base(Errors.GetError((ErrorCode)code))
        {
            Code = code;
            Name = ((ErrorCode)code).ToString();
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
        [DOM("code")]
        public Int32 Code
        {
            get;
            private set;
        }

        #endregion
    }
}
