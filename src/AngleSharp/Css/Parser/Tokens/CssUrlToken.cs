namespace AngleSharp.Css.Parser.Tokens
{
    using AngleSharp.Extensions;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents a CSS URL token.
    /// </summary>
    sealed class CssUrlToken : CssToken
    {
        #region Fields

        private readonly Boolean _bad;
        private readonly String _functionName;

        #endregion

        #region ctor

        public CssUrlToken(String functionName, String data, Boolean bad, TextPosition position)
            : base(CssTokenType.Url, data, position)
        {
            _bad = bad;
            _functionName = functionName;
        }

        #endregion

        #region Properties

        public Boolean IsBad
        {
            get { return _bad; }
        }

        public String FunctionName
        {
            get { return _functionName; }
        }

        #endregion

        #region String representation

        public override String ToValue()
        {
            var url = Data.CssString();
            return _functionName.CssFunction(url);
        }

        #endregion
    }
}
