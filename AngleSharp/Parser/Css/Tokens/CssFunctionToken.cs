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

        public CssFunctionToken(String data, TextPosition position)
            : base(CssTokenType.Function, data, position)
        {
            _arguments = new List<CssToken>();
        }

        #endregion

        #region Properties

        public IEnumerable<CssToken> ArgumentTokens
        {
            get
            {
                var final = _arguments.Count - 1;

                if (final >= 0 && _arguments[final].Type == CssTokenType.RoundBracketClose)
                {
                    final--;
                }

                return _arguments.Take(1 + final);
            }
        }

        #endregion

        #region Methods

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

        public override String ToValue()
        {
            return String.Concat(Data, "(", _arguments.ToText());
        }

        #endregion
    }
}
