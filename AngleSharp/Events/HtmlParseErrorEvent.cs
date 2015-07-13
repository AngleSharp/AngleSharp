namespace AngleSharp.Events
{
    using System;

    /// <summary>
    /// The event that is published in case of an HTML parse error.
    /// </summary>
    public class HtmlParseErrorEvent
    {
        #region ctor
        
        /// <summary>
        /// Creates a new HtmlParseErrorEvent event.
        /// </summary>
        /// <param name="code">The provided error code.</param>
        /// <param name="message">The associated error message.</param>
        /// <param name="position">The position in the source.</param>
        /// 
        public HtmlParseErrorEvent(Int32 code, String message, TextPosition position)
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
