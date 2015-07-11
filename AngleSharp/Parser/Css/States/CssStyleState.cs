namespace AngleSharp.Parser.Css.States
{
    using AngleSharp.Dom.Css;

    sealed class CssStyleState : CssParseState
    {
        public CssStyleState(CssTokenizer tokenizer, CssParserOptions options)
            : base(tokenizer, options)
        {
        }

        public override CssRule Create(CssToken current)
        {
            var rule = new CssStyleRule(_options);
            rule.Selector = CreateSelector(ref current);
            FillDeclarations(rule.Style);
            return rule.Selector != null ? rule : null;
        }
    }
}
