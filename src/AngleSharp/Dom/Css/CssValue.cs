namespace AngleSharp.Dom.Css
{
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Represents a CSS value.
    /// </summary>
    sealed class CssValue : CssNode, IEnumerable<CssToken>
    {
        #region Fields

        readonly List<CssToken> _tokens;

        public static CssValue Initial = CssValue.FromString(Keywords.Initial);
        public static CssValue Empty = new CssValue(Enumerable.Empty<CssToken>());

        #endregion

        #region ctor

        private CssValue(CssToken token)
        {
            _tokens = new List<CssToken> { token };
        }

        public CssValue(IEnumerable<CssToken> tokens)
        {
            _tokens = new List<CssToken>(tokens);
        }

        public static CssValue FromString(String text)
        {
            var token = new CssToken(CssTokenType.Ident, text, TextPosition.Empty);
            return new CssValue(token);
        }

        #endregion

        #region Properties

        public CssToken this[Int32 index]
        {
            get { return _tokens[index]; }
        }

        public Int32 Count
        {
            get { return _tokens.Count; }
        }

        public String CssText
        {
            get { return this.ToCss(); }
        }

        #endregion

        #region Methods

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            writer.Write(_tokens.ToText());
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
