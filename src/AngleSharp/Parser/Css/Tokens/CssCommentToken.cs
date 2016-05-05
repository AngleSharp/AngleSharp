namespace AngleSharp.Parser.Css
{
    using System;

    /// <summary>
    /// Represents a CSS comment token.
    /// </summary>
    sealed class CssCommentToken : CssToken
    {
        #region Fields

        readonly Boolean _bad;

        #endregion

        #region ctor

        public CssCommentToken(String data, Boolean bad, TextPosition position)
            : base(CssTokenType.Comment, data, position)
        {
            _bad = bad;
        }

        #endregion

        #region Properties

        public Boolean IsBad
        {
            get { return _bad; }
        }

        #endregion

        #region String representation

        public override String ToValue()
        {
            var trailing = _bad ? String.Empty : "*/";
            return String.Concat("/*", Data, trailing);
        }

        #endregion
    }
}
