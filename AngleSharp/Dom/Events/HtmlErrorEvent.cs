namespace AngleSharp.Dom.Events
{
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using AngleSharp.Parser.Html;
    using System;

    /// <summary>
    /// The event that is published in case of an HTML parse error.
    /// </summary>
    public class HtmlErrorEvent : Event
    {
        #region Fields

        readonly HtmlParseError _code;
        readonly TextPosition _position;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HtmlParseErrorEvent event.
        /// </summary>
        /// <param name="code">The provided error code.</param>
        /// <param name="position">The position in the source.</param>
        /// 
        public HtmlErrorEvent(HtmlParseError code, TextPosition position)
            : base(EventNames.ParseError)
        {
            _code = code;
            _position = position;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the position of the error.
        /// </summary>
        public TextPosition Position
        {
            get { return _position; }
        }

        /// <summary>
        /// Gets the provided error code.
        /// </summary>
        public Int32 Code
        {
            get { return _code.GetCode(); }
        }

        /// <summary>
        /// Gets the associated error message.
        /// </summary>
        public String Message
        {
            get { return _code.GetMessage(); }
        }

        #endregion
    }
}
