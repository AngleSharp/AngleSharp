namespace AngleSharp.Dom.Css
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;

    /// <summary>
    /// Represents a CSS value.
    /// </summary>
    sealed class CssValue : ICssValue, IEnumerable<CssToken>
    {
        #region Fields

        readonly CssValueType _type;
        readonly List<CssToken> _tokens;

        public static CssValue Initial = new CssValue(new []
        { 
            new CssKeywordToken(CssTokenType.Ident, "initial", TextPosition.Empty) 
        });

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS value.
        /// </summary>
        /// <param name="tokens">The tokens to use.</param>
        public CssValue(IEnumerable<CssToken> tokens)
        {
            _tokens = new List<CssToken>(tokens);
            _type = FindType(_tokens);
        }

        static CssValueType FindType(List<CssToken> tokens)
        {
            var type = CssValueType.Custom;
            var open = 0;

            if (tokens.Count == 1)
            {
                if (tokens[0].Data.Equals(Keywords.Initial, StringComparison.OrdinalIgnoreCase))
                    return CssValueType.Initial;
                else if (tokens[0].Data.Equals(Keywords.Inherit, StringComparison.OrdinalIgnoreCase))
                    return CssValueType.Inherit;
            }

            for (int i = 0; i < tokens.Count; i++)
            {
                if (type == CssValueType.Custom)
                    type = CssValueType.Primitive;

                if (open > 0 && tokens[i].Type == CssTokenType.RoundBracketClose)
                    open--;

                if (tokens[i].Type == CssTokenType.Function || tokens[i].Type == CssTokenType.RoundBracketOpen)
                    open++;

                if (open > 0)
                    continue;

                if (tokens[i].Type == CssTokenType.Whitespace || tokens[i].Type == CssTokenType.Comma ||
                    (tokens[i].Type == CssTokenType.Delim && tokens[i].Data == "/"))
                    type = CssValueType.List;
            }

            return type;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the token at the provided index.
        /// </summary>
        /// <param name="index">The index of the token.</param>
        /// <returns>The token at the index.</returns>
        public CssToken this[Int32 index]
        {
            get { return _tokens[index]; }
        }

        /// <summary>
        /// Gets the number of tokens for the current value.
        /// </summary>
        public Int32 Count
        {
            get { return _tokens.Count; }
        }

        /// <summary>
        /// Gets a code defining the type of the value as defined above.
		/// </summary>
        public CssValueType Type
        {
            get { return _type; }
        }

        /// <summary>
        /// Gets or sets a string representation of the current value.
        /// </summary>
        public String CssText
        {
            get { return _tokens.ToText(); }
        }

        #endregion

        #region IEnumerable

        public IEnumerator<CssToken> GetEnumerator()
        {
            return _tokens.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
