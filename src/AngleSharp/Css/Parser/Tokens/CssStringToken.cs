namespace AngleSharp.Css.Parser.Tokens
{
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents a CSS string token.
    /// </summary>
    sealed class CssStringToken : CssToken
    {
        #region ctor

        public CssStringToken(String data)
            : base(CssTokenType.String, data)
        {
        }

        #endregion

        #region String representation

        public override String ToValue()
        {
            return Data.CssString();
        }

        #endregion
    }
}
