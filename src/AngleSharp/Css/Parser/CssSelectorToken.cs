namespace AngleSharp.Css.Parser
{
    using System;

    /// <summary>
    /// The CSS selector token.
    /// </summary>
    struct CssSelectorToken
    {
        #region Fields

        private readonly CssTokenType _type;
        private readonly String _data;

        public static readonly CssSelectorToken Whitespace = new CssSelectorToken(CssTokenType.Whitespace, " ");

        #endregion

        #region ctor

        public CssSelectorToken(CssTokenType type, String data)
        {
            _type = type;
            _data = data;
        }

        #endregion

        #region Properties

        public CssTokenType Type => _type;

        public String Data => _data;

        #endregion
    }
}
