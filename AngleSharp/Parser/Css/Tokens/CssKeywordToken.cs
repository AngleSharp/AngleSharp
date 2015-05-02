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
        /// <param name="position">The token's position.</param>
        CssKeywordToken(CssTokenType type, String data, TextPosition position)
            : base(type, data, position)
        {
        }

        #endregion

        #region Static constructors

        /// <summary>
        /// Creates a new CSS keyword token for a function.
        /// </summary>
        /// <param name="name">The name of the function.</param>
        /// <param name="position">The token's position.</param>
        /// <returns>The created token.</returns>
        public static CssKeywordToken Function(String name, TextPosition position)
        {
            return new CssKeywordToken(CssTokenType.Function, name, position);
        }

        /// <summary>
        /// Creates a new CSS keyword token for an identifier.
        /// </summary>
        /// <param name="identifier">The name of the identifier.</param>
        /// <param name="position">The token's position.</param>
        /// <returns>The created token.</returns>
        public static CssKeywordToken Ident(String identifier, TextPosition position)
        {
            return new CssKeywordToken(CssTokenType.Ident, identifier, position);
        }

        /// <summary>
        /// Creates a new CSS keyword token for an at-keyword.
        /// </summary>
        /// <param name="name">The name of the @-rule.</param>
        /// <param name="position">The token's position.</param>
        /// <returns>The created token.</returns>
        public static CssKeywordToken At(String name, TextPosition position)
        {
            return new CssKeywordToken(CssTokenType.AtKeyword, name, position);
        }

        /// <summary>
        /// Creates a new CSS keyword token for a hash token.
        /// </summary>
        /// <param name="characters">The contained characters.</param>
        /// <param name="position">The token's position.</param>
        /// <returns>The created token.</returns>
        public static CssKeywordToken Hash(String characters, TextPosition position)
        {
            return new CssKeywordToken(CssTokenType.Hash, characters, position);
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
