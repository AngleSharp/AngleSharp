namespace AngleSharp.Parser.Css.States
{
    using AngleSharp.Dom.Css;

    sealed class CssMediaState : CssParseState
    {
        public CssMediaState(CssTokenizer tokenizer)
            : base(tokenizer)
        {
        }

        public override CssRule Create(CssToken current)
        {
            var token = _tokenizer.Get();
            var list = CreateMediaList(ref token);
            var rule = new CssMediaRule(list);

            if (token.Type != CssTokenType.CurlyBracketOpen)
                return SkipDeclarations(token);

            FillRules(rule);
            return rule;
        }
    }
}
