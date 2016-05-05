namespace AngleSharp.Dom.Events
{
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using AngleSharp.Parser.Css;
    using System;

    /// <summary>
    /// The event that is published in case of an CSS parse error.
    /// </summary>
    public class CssErrorEvent : Event
    {
        #region Fields

        private CssParseError _code;
        private TextPosition _position;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CssParseErrorEvent event.
        /// </summary>
        /// <param name="code">The provided error code.</param>
        /// <param name="position">The position in the source.</param>
        /// 
        public CssErrorEvent(CssParseError code, TextPosition position)
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
