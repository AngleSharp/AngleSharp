namespace AngleSharp.Parser.Css
{
    using System;

    /// <summary>
    /// The match token that contains part of a selector.
    /// </summary>
    sealed class CssMatchToken : CssToken
    {
        #region ctor

        /// <summary>
        /// Creates a new CSS match token.
        /// </summary>
        CssMatchToken(CssTokenType type, String data, TextPosition position)
            : base(type, data, position)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Creates a new CSS include-match token.
        /// </summary>
        public static CssMatchToken Include(TextPosition position)
        {
            return new CssMatchToken(CssTokenType.IncludeMatch, "~=", position);
        }

        /// <summary>
        /// Creates a new CSS dash-match token.
        /// </summary>
        public static CssMatchToken Dash(TextPosition position)
        {
            return new CssMatchToken(CssTokenType.DashMatch, "|=", position);
        }

        /// <summary>
        /// Creates a new CSS prefix-match token.
        /// </summary>
        public static CssToken Prefix(TextPosition position)
        {
            return new CssMatchToken(CssTokenType.PrefixMatch, "^=", position);
        }

        /// <summary>
        /// Creates a new CSS substring-match token.
        /// </summary>
        public static CssToken Substring(TextPosition position)
        {
            return new CssMatchToken(CssTokenType.SubstringMatch, "*=", position);
        }

        /// <summary>
        /// Creates a new CSS suffix-match token.
        /// </summary>
        public static CssToken Suffix(TextPosition position)
        {
            return new CssMatchToken(CssTokenType.SuffixMatch, "$=", position);
        }

        /// <summary>
        /// Creates a new CSS not-match token.
        /// </summary>
        public static CssToken Not(TextPosition position)
        {
            return new CssMatchToken(CssTokenType.NotMatch, "!=", position);
        }

        #endregion
    }
}
