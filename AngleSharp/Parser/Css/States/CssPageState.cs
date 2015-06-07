namespace AngleSharp.Parser.Css.States
{
    using AngleSharp.Dom.Css;

    sealed class CssPageState : CssParseState
    {
        public CssPageState(CssTokenizer tokenizer)
            : base(tokenizer)
        {
        }

        public override CssRule Create(CssToken current)
        {
            var token = _tokenizer.Get();
            var rule = new CssPageRule();
            rule.Selector = CreateSelector(ref token);

            if (token.Type != CssTokenType.CurlyBracketOpen)
                return SkipDeclarations(token);

            FillDeclarations(rule.Style);
            return rule;
        }
    }
}
