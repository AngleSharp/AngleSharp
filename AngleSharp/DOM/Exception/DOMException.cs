using System;
using System.Runtime.Serialization;

namespace AngleSharp.DOM
{
    /// <summary>
    /// Represents a DOM exception.
    /// </summary>
    [Serializable]
    public class DOMException : Exception
    {
        /// <summary>
        /// Creates a new DOMException.
        /// </summary>
        /// <param name="code">The error code.</param>
        internal DOMException(ErrorCode code)
            : base(Errors.GetError(code))
        {
            Code = (int)code;
            Name = code.ToString();
        }

        /// <summary>
        /// Creates a new DOMException.
        /// </summary>
        /// <param name="code">The error code.</param>
        public DOMException(int code)
            : base(Errors.GetError((ErrorCode)code))
        {
            Code = code;
            Name = ((ErrorCode)code).ToString();
        }

        /// <summary>
        /// Gets the name of the error.
        /// </summary>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the error code for this exception.
        /// </summary>
        public int Code
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the object data in a serialization scenario.
        /// </summary>
        /// <param name="info">The serialization info.</param>
        /// <param name="context">The streaming context.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Name", Name);
            info.AddValue("Code", Code);
        }
    }
}
