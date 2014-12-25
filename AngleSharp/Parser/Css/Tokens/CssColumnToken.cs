namespace AngleSharp.Parser.Css
{
    using System;

    /// <summary>
    /// The column token that contains a column (||).
    /// </summary>
    sealed class CssColumnToken : CssToken
    {
        #region Static instance

        readonly static CssColumnToken instance;

        #endregion

        #region ctor

        static CssColumnToken()
        {
            instance = new CssColumnToken();
        }

        /// <summary>
        /// Creates a new CSS column token.
        /// </summary>
        CssColumnToken()
            : base(CssTokenType.Column, "||")
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the token.
        /// </summary>
        public static CssColumnToken Instance
        {
            get { return instance; }
        }

        #endregion
    }
}
