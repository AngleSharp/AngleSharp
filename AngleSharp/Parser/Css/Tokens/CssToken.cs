namespace AngleSharp.Parser.Css
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// The base class token for the CSS parser.
    /// </summary>
    [DebuggerStepThrough]
    abstract class CssToken
    {
        #region Fields

        readonly CssTokenType _type;
        readonly String _data;

        #endregion

        #region ctor

        public CssToken(CssTokenType type, String data)
        {
            _type = type;
            _data = data;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the type of the token.
        /// </summary>
        public CssTokenType Type
        {
            get { return _type; }
        }

        /// <summary>
        /// Gets the data of the token.
        /// </summary>
        public String Data
        {
            get { return _data; }
        }

        #endregion

        #region Factory

        /// <summary>
        /// Gets the column token.
        /// </summary>
        public static CssColumnToken Column
        {
            get { return CssColumnToken.Token; }
        }

        /// <summary>
        /// Creates a new CSS delimiter token.
        /// </summary>
        /// <param name="c">The delim char.</param>
        /// <returns>The created token.</returns>
        [DebuggerStepThrough]
        public static CssDelimToken Delim(Char c)
        {
            return new CssDelimToken(c);
        }

        /// <summary>
        /// Creates a new CSS number token.
        /// </summary>
        /// <param name="value">The single precision number.</param>
        /// <returns>The created token.</returns>
        [DebuggerStepThrough]
        public static CssNumberToken Number(String value)
        {
            return new CssNumberToken(value);
        }

        /// <summary>
        /// Creates a new CSS range token.
        /// </summary>
        /// <param name="start">The start of the range.</param>
        /// <param name="end">The end of the range.</param>
        /// <returns>The created token.</returns>
        [DebuggerStepThrough]
        public static CssRangeToken Range(String start, String end)
        {
            return new CssRangeToken(start, end);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a string which represents the original value.
        /// </summary>
        /// <returns>The original value.</returns>
        public virtual String ToValue()
        {
            return _data;
        }

        #endregion
    }
}
