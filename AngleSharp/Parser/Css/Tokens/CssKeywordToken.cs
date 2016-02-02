namespace AngleSharp.Parser.Css
{
    using System;

    /// <summary>
    /// Represents a CSS keyword token.
    /// </summary>
    sealed class CssKeywordToken : CssToken
    {
        #region ctor

        public CssKeywordToken(CssTokenType type, String data, TextPosition position)
            : base(type, data, position)
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
                case CssTokenType.AtKeyword:
                    return "@" + Data;
                case CssTokenType.Function:
                    return Data + "(";
                default:
                    return Data;
            }
        }

        #endregion
    }
}
