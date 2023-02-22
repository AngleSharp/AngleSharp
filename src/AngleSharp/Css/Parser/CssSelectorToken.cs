namespace AngleSharp.Css.Parser
{
    using System;

    /// <summary>
    /// The CSS selector token.
    /// </summary>
    readonly struct CssSelectorToken
    {
        #region Fields

        public static readonly CssSelectorToken Whitespace = new CssSelectorToken(CssTokenType.Whitespace, " ");

        #endregion

        #region ctor

        public CssSelectorToken(CssTokenType type, String data)
        {
            Type = type;
            Data = data;
        }

        #endregion

        #region Properties

        public CssTokenType Type { get; }

        public String Data { get; }

        #endregion
    }
}
