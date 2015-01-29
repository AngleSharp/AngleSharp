namespace AngleSharp.Parser.Css
{
    using AngleSharp.Dom.Css;
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
        /// Creates a new CSS delimiter token.
        /// </summary>
        /// <param name="c">The delim char.</param>
        /// <returns>The created token.</returns>
        [DebuggerStepThrough]
        public static CssDelimToken Delim(Char c)
        {
            return new CssDelimToken(c);
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

        /// <summary>
        /// Converts the data to an identifier value. Uses inherit for inherit.
        /// </summary>
        /// <returns>The created value.</returns>
        public ICssValue ToIdentifier()
        {
            if (_data.Equals(Keywords.Inherit, StringComparison.OrdinalIgnoreCase))
                return CssValue.Inherit;
            else if (_data.Equals(Keywords.Initial, StringComparison.OrdinalIgnoreCase))
                return CssValue.Initial;

            return new CssIdentifier(_data);
        }

        #endregion
    }
}
