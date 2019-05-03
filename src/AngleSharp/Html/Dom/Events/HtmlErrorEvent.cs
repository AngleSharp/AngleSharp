namespace AngleSharp.Html.Dom.Events
{
    using AngleSharp.Common;
    using AngleSharp.Dom;
    using AngleSharp.Dom.Events;
    using AngleSharp.Html.Parser;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// The event that is published in case of an HTML parse error.
    /// </summary>
    public class HtmlErrorEvent : Event
    {
        #region Fields

        private readonly HtmlParseError _code;
        private readonly TextPosition _position;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HtmlParseErrorEvent event.
        /// </summary>
        /// <param name="code">The provided error code.</param>
        /// <param name="position">The position in the source.</param>
        /// 
        public HtmlErrorEvent(HtmlParseError code, TextPosition position)
            : base(EventNames.Error)
        {
            _code = code;
            _position = position;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the position of the error.
        /// </summary>
        public TextPosition Position => _position;

        /// <summary>
        /// Gets the provided error code.
        /// </summary>
        public Int32 Code => _code.GetCode();

        /// <summary>
        /// Gets the associated error message.
        /// </summary>
        public String Message => _code.GetMessage();

        #endregion
    }
}
