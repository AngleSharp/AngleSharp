namespace AngleSharp.Css.Parser.Tokens
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a CSS function token.
    /// </summary>
    sealed class CssFunctionToken : CssToken, IEnumerable<CssToken>
    {
        #region Fields

        private readonly List<CssToken> _arguments;

        #endregion

        #region ctor

        public CssFunctionToken(String data)
            : base(CssTokenType.Function, data)
        {
            _arguments = new List<CssToken>();
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
