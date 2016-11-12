namespace AngleSharp.Css.Parser.Tokens
{
    using System;

    /// <summary>
    /// The base class token for the CSS parser.
    /// </summary>
    class CssToken
    {
        #region Fields

        private readonly CssTokenType _type;
        private readonly String _data;

        public static readonly CssToken Whitespace = new CssToken(CssTokenType.Whitespace, " ");

        #endregion

        #region ctor

        public CssToken(CssTokenType type, String data)
        {
            _type = type;
            _data = data;
        }

        #endregion

        #region Properties

        public CssTokenType Type
        {
            get { return _type; }
        }

        public String Data
        {
            get { return _data; }
        }

        #endregion
    }
}
