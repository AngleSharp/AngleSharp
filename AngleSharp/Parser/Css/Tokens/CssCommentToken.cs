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

        /// <summary>
        /// Creates a new CSS comment token.
        /// </summary>
        /// <param name="data">The string data.</param>
        /// <param name="bad">If the string was bad (optional).</param>
        /// <param name="position">The token's position.</param>
        public CssCommentToken(String data, Boolean bad, TextPosition position)
            : base(CssTokenType.Comment, data, position)
        {
            _bad = bad;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the data is bad.
        /// </summary>
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
