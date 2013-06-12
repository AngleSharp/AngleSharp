using System;

namespace AngleSharp.Css
{
    /// <summary>
    /// The column token that contains a column (||).
    /// </summary>
    sealed class CssColumnToken : CssToken
    {
        #region Static instance

        readonly static CssColumnToken token;

        #endregion

        #region ctor

        static CssColumnToken()
        {
            token = new CssColumnToken();
        }

        /// <summary>
        /// Creates a new CSS column token.
        /// </summary>
        CssColumnToken()
        {
            _type = CssTokenType.Column;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the token.
        /// </summary>
        public static CssColumnToken Token
        {
            get { return token; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a string which represents the original value.
        /// </summary>
        /// <returns>The original value.</returns>
        public override String ToValue()
        {
            return "||";
        }

        #endregion
    }
}
