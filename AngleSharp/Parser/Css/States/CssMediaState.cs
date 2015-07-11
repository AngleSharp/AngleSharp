namespace AngleSharp.Parser.Css.States
{
    using AngleSharp.Dom.Css;

    sealed class CssMediaState : CssParseState
    {
        public CssMediaState(CssTokenizer tokenizer, CssParserOptions options)
            : base(tokenizer, options)
        {
        }

        public override CssRule Create(CssToken current)
        {
            var token = _tokenizer.Get();
            var rule = new CssMediaRule(_options);
            FillMediaList(rule.Media, ref token);

            if (token.Type != CssTokenType.CurlyBracketOpen)
                return SkipDeclarations(token);

            FillRules(rule);
            return rule;
        }
    }
}
