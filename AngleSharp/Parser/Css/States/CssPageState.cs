namespace AngleSharp.Parser.Css.States
{
    using AngleSharp.Dom.Css;

    sealed class CssPageState : CssParseState
    {
        public CssPageState(CssTokenizer tokenizer, CssParser parser)
            : base(tokenizer, parser)
        {
        }

        public override CssRule Create(CssToken current)
        {
            var token = _tokenizer.Get();
            var rule = new CssPageRule(_parser);
            rule.Selector = CreateSelector(ref token);

            if (token.Type != CssTokenType.CurlyBracketOpen)
                return SkipDeclarations(token);

            FillDeclarations(rule.Style);
            return rule;
        }
    }
}
