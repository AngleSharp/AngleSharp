namespace AngleSharp.Parser.Css
{
    using System;

    /// <summary>
    /// The match token that contains part of a selector.
    /// </summary>
    sealed class CssMatchToken : CssToken
    {
        #region Static instances

        readonly static CssMatchToken include;
        readonly static CssMatchToken dash;
        readonly static CssToken prefix;
        readonly static CssToken substring;
        readonly static CssToken suffix;
        readonly static CssToken not;

        #endregion

        #region ctor

        static CssMatchToken()
        {
            include = new CssMatchToken(CssTokenType.IncludeMatch, "~=");
            dash = new CssMatchToken(CssTokenType.DashMatch, "|=");
            prefix = new CssMatchToken(CssTokenType.PrefixMatch, "^=");
            substring = new CssMatchToken(CssTokenType.SubstringMatch, "*=");
            suffix = new CssMatchToken(CssTokenType.SuffixMatch, "$=");
            not = new CssMatchToken(CssTokenType.NotMatch, "!=");
        }

        /// <summary>
        /// Creates a new CSS match token.
        /// </summary>
        CssMatchToken(CssTokenType type, String data)
            : base(type, data)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a new CSS include-match token.
        /// </summary>
        public static CssMatchToken Include
        {
            get { return include; }
        }

        /// <summary>
        /// Gets a new CSS dash-match token.
        /// </summary>
        public static CssMatchToken Dash
        {
            get { return dash; }
        }

        /// <summary>
        /// Gets a new CSS prefix-match token.
        /// </summary>
        public static CssToken Prefix
        {
            get { return prefix; }
        }

        /// <summary>
        /// Gets a new CSS substring-match token.
        /// </summary>
        public static CssToken Substring
        {
            get { return substring; }
        }

        /// <summary>
        /// Gets a new CSS suffix-match token.
        /// </summary>
        public static CssToken Suffix
        {
            get { return suffix; }
        }

        /// <summary>
        /// Gets a new CSS not-match token.
        /// </summary>
        public static CssToken Not
        {
            get { return not; }
        }

        #endregion
    }
}
