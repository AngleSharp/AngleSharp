namespace AngleSharp.Css.Parser.Tokens
{
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents a CSS string token.
    /// </summary>
    sealed class CssStringToken : CssToken
    {
        #region Fields

        private readonly Boolean _bad;
        private readonly Char _quote;

        #endregion

        #region ctor

        public CssStringToken(String data, Boolean bad, Char quote, TextPosition position)
            : base(CssTokenType.String, data, position)
        {
            _bad = bad;
            _quote = quote;
        }

        #endregion

        #region Properties

        public Boolean IsBad
        {
            get { return _bad; }
        }

        public Char Quote
        {
            get { return _quote; }
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
