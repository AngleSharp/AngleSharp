using System;

namespace AngleSharp.Css
{
    /// <summary>
    /// The match token that contains part of a selector.
    /// </summary>
    class CssMatchToken : CssToken
    {
        readonly static CssMatchToken include = new CssMatchToken { _type = CssTokenType.IncludeMatch };
        readonly static CssMatchToken dash = new CssMatchToken { _type = CssTokenType.DashMatch };
        readonly static CssToken prefix = new CssMatchToken { _type = CssTokenType.PrefixMatch };
        readonly static CssToken substring = new CssMatchToken { _type = CssTokenType.SubstringMatch };
        readonly static CssToken suffix = new CssMatchToken { _type = CssTokenType.SuffixMatch };
        readonly static CssToken not = new CssMatchToken { _type = CssTokenType.NotMatch };

        /// <summary>
        /// Creates a new CSS match token.
        /// </summary>
        private CssMatchToken()
        {
        }

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

        /// <summary>
        /// Gets a string which represents the original value.
        /// </summary>
        /// <returns>The original value.</returns>
        public override string ToValue()
        {
            switch (_type)
            {
                case CssTokenType.SubstringMatch:
                    return "*=";

                case CssTokenType.SuffixMatch:
                    return "$=";

                case CssTokenType.PrefixMatch:
                    return "^=";

                case CssTokenType.IncludeMatch:
                    return "~=";

                case CssTokenType.DashMatch:
                    return "|=";

                case CssTokenType.NotMatch:
                    return "!=";
            }

            return string.Empty;
        }
    }
}
