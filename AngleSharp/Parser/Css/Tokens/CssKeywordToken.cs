namespace AngleSharp.Parser.Css
{
    using System;

    /// <summary>
    /// Represents a CSS keyword token.
    /// </summary>
    sealed class CssKeywordToken : CssToken
    {
        #region ctor

        /// <summary>
        /// Creates a new CSS keyword token.
        /// </summary>
        /// <param name="type">The exact type.</param>
        /// <param name="data">The data to use.</param>
        CssKeywordToken(CssTokenType type, String data)
            : base(type, data)
        {
        }

        #endregion

        #region Static constructors

        /// <summary>
        /// Creates a new CSS keyword token for a function.
        /// </summary>
        /// <param name="name">The name of the function.</param>
        /// <returns>The created token.</returns>
        public static CssKeywordToken Function(String name)
        {
            return new CssKeywordToken(CssTokenType.Function, name);
        }

        /// <summary>
        /// Creates a new CSS keyword token for an identifier.
        /// </summary>
        /// <param name="identifier">The name of the identifier.</param>
        /// <returns>The created token.</returns>
        public static CssKeywordToken Ident(String identifier)
        {
            return new CssKeywordToken(CssTokenType.Ident, identifier);
        }

        /// <summary>
        /// Creates a new CSS keyword token for an at-keyword.
        /// </summary>
        /// <param name="name">The name of the @-rule.</param>
        /// <returns>The created token.</returns>
        public static CssKeywordToken At(String name)
        {
            return new CssKeywordToken(CssTokenType.AtKeyword, name);
        }

        /// <summary>
        /// Creates a new CSS keyword token for a hash token.
        /// </summary>
        /// <param name="characters">The contained characters.</param>
        /// <returns>The created token.</returns>
        public static CssKeywordToken Hash(String characters)
        {
            return new CssKeywordToken(CssTokenType.Hash, characters);
        }

        #endregion

        #region String representation

        /// <summary>
        /// Gets a string which represents the original value.
        /// </summary>
        /// <returns>The original value.</returns>
        public override String ToValue()
        {
            switch (Type)
            {
                case CssTokenType.Hash:
                    return "#" + Data;
                case CssTokenType.AtKeyword:
                    return "@" + Data;
                default:
                    return Data;
            }
        }

        #endregion
    }
}
