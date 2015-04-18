namespace AngleSharp.Events
{
    using System;
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;

    /// <summary>
    /// The event that is published in case of an CSS parse error.
    /// </summary>
    public class CssParseErrorEvent
    {
        #region ctor

        /// <summary>
        /// Creates a new CssParseErrorEvent event.
        /// </summary>
        /// <param name="error">The error object.</param>
        /// <param name="position">The position in the source.</param>
        internal CssParseErrorEvent(CssParseError error, TextPosition position)
            : this(error.GetCode(), error.GetMessage(), position)
        {
        }

        /// <summary>
        /// Creates a new CssParseErrorEvent event.
        /// </summary>
        /// <param name="code">The provided error code.</param>
        /// <param name="message">The associated error message.</param>
        /// <param name="position">The position in the source.</param>
        /// 
        public CssParseErrorEvent(Int32 code, String message, TextPosition position)
        {
            Code = code;
            Message = message;
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
        public Int32 Code
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the associated error message.
        /// </summary>
        public String Message
        {
            get;
            private set;
        }

        #endregion
    }
}
