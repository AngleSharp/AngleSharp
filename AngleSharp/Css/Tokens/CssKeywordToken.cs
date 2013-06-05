using System;

namespace AngleSharp.Css
{
    /// <summary>
    /// Represents a CSS keyword token.
    /// </summary>
    class CssKeywordToken : CssToken
    {
        #region Members

        string _data;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS keyword token.
        /// </summary>
        /// <param name="type">The exact type.</param>
        private CssKeywordToken(CssTokenType type)
        {
            _type = type;
        }

        #endregion

        #region Static constructors

        /// <summary>
        /// Creates a new CSS keyword token for a function.
        /// </summary>
        /// <param name="name">The name of the function.</param>
        /// <returns>The created token.</returns>
        public static CssKeywordToken Function(string name)
        {
            return new CssKeywordToken(CssTokenType.Function) { _data = name };
        }

        /// <summary>
        /// Creates a new CSS keyword token for an identifier.
        /// </summary>
        /// <param name="name">The name of the identifier.</param>
        /// <returns>The created token.</returns>
        public static CssKeywordToken Ident(string identifier)
        {
            return new CssKeywordToken(CssTokenType.Ident) { _data = identifier };
        }

        /// <summary>
        /// Creates a new CSS keyword token for an at-keyword.
        /// </summary>
        /// <param name="name">The name of the @-rule.</param>
        /// <returns>The created token.</returns>
        public static CssKeywordToken At(string name)
        {
            return new CssKeywordToken(CssTokenType.AtKeyword) { _data = name };
        }

        /// <summary>
        /// Creates a new CSS keyword token for a hash token.
        /// </summary>
        /// <param name="name">The contained characters.</param>
        /// <returns>The created token.</returns>
        public static CssKeywordToken Hash(string characters)
        {
            return new CssKeywordToken(CssTokenType.Hash) { _data = characters };
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the contained data.
        /// </summary>
        public string Data
        {
            get { return _data; }
            set { _data = value; }
        }

        #endregion

        #region String representation

        /// <summary>
        /// Gets a string which represents the original value.
        /// </summary>
        /// <returns>The original value.</returns>
        public override string ToValue()
        {
            switch (_type)
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
