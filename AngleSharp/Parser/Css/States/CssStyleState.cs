namespace AngleSharp.Parser.Css.States
{
    using AngleSharp.Dom.Css;

    sealed class CssStyleState : CssParseState
    {
        public CssStyleState(CssTokenizer tokenizer, CssParser parser)
            : base(tokenizer, parser)
        {
        }

        public override CssRule Create(CssToken current)
        {
            var rule = new CssStyleRule(_parser);
            rule.Selector = CreateSelector(ref current);
            FillDeclarations(rule.Style);
            return rule.Selector != null ? rule : null;
        }
    }
}
