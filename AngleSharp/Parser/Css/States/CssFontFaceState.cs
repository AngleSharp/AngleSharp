namespace AngleSharp.Parser.Css.States
{
    using AngleSharp.Dom.Css;

    sealed class CssFontFaceState : CssParseState
    {
        public CssFontFaceState(CssTokenizer tokenizer, CssParserOptions options)
            : base(tokenizer, new CssParserOptions { IsIncludingUnknownDeclarations = true, IsToleratingInvalidValues = options.IsToleratingInvalidValues })
        {
        }

        public override CssRule Create(CssToken current)
        {
            var token = _tokenizer.Get();
            var rule = new CssFontFaceRule(_options);

            if (token.Type != CssTokenType.CurlyBracketOpen)
                return SkipDeclarations(token);

            FillFontFaceDeclarations(rule);
            return rule;
        }

        void FillFontFaceDeclarations(CssFontFaceRule rule)
        {
            var token = _tokenizer.Get();

            while (token.IsNot(CssTokenType.Eof, CssTokenType.CurlyBracketClose))
            {
                var property = CreateDeclaration(ref token);

                if (property != null && property.HasValue)
                    rule.SetProperty(property);
            }
        }
    }
}
