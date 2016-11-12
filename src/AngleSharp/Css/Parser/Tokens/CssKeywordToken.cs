namespace AngleSharp.Css.Parser.Tokens
{
    using System;

    /// <summary>
    /// Represents a CSS keyword token.
    /// </summary>
    sealed class CssKeywordToken : CssToken
    {
        #region ctor

        public CssKeywordToken(CssTokenType type, String data)
            : base(type, data)
        {
        }

        #endregion

        #region String representation

        public override String ToValue()
        {
            switch (Type)
            {
                case CssTokenType.Hash:
                    return "#" + Data;
                default:
                    return Data;
            }
        }

        #endregion
    }
}
