namespace AngleSharp.Dom.Css
{
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a CSS value.
    /// </summary>
    sealed class CssValue : IEnumerable<CssToken>, IStyleFormattable
    {
        #region Fields

        readonly List<CssToken> _tokens;

        public static CssValue Initial = CssValue.FromString(Keywords.Initial);
        public static CssValue Empty = new CssValue(Enumerable.Empty<CssToken>());

        #endregion

        #region ctor

        private CssValue(CssToken token)
        {
            _tokens = new List<CssToken>();
            _tokens.Add(token);
        }

        /// <summary>
        /// Creates a new CSS value.
        /// </summary>
        /// <param name="tokens">The tokens to use.</param>
        public CssValue(IEnumerable<CssToken> tokens)
        {
            _tokens = new List<CssToken>(tokens);
        }

        /// <summary>
        /// Creates a new CSS value with the given text and type.
        /// </summary>
        /// <param name="text">The text to convert.</param>
        /// <returns>The new value.</returns>
        public static CssValue FromString(String text)
        {
            var token = new CssToken(CssTokenType.Ident, text, TextPosition.Empty);
            return new CssValue(token);
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
        /// Gets or sets a string representation of the current value.
        /// </summary>
        public String CssText
        {
            get { return ToCss(); }
        }

        #endregion

        #region String Representation

        public String ToCss()
        {
            return _tokens.ToText();
        }

        public String ToCss(IStyleFormatter formatter)
        {
            return ToCss();
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
