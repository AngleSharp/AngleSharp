namespace AngleSharp.Parser.Css
{
    using AngleSharp.Extensions;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a CSS function token.
    /// </summary>
    sealed class CssFunctionToken : CssToken, IEnumerable<CssToken>
    {
        #region Fields

        readonly List<CssToken> _arguments;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS function token.
        /// </summary>
        /// <param name="data">The data to use.</param>
        /// <param name="position">The token's position.</param>
        public CssFunctionToken(String data, TextPosition position)
            : base(CssTokenType.Function, data, position)
        {
            _arguments = new List<CssToken>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the tokens stored for the arguments.
        /// </summary>
        public IEnumerable<CssToken> ArgumentTokens
        {
            get
            {
                var final = _arguments.Count - 1;

                if (final >= 0 && _arguments[final].Type == CssTokenType.RoundBracketClose)
                    final--;

                return _arguments.Take(1 + final);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Uses the provided token as an argument token.
        /// </summary>
        public void AddArgumentToken(CssToken token)
        {
            _arguments.Add(token);
        }

        public IEnumerator<CssToken> GetEnumerator()
        {
            return _arguments.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region String representation

        /// <summary>
        /// Gets a string which represents the original value.
        /// </summary>
        /// <returns>The original value.</returns>
        public override String ToValue()
        {
            return String.Concat(Data, "(", _arguments.ToText());
        }

        #endregion
    }
}
