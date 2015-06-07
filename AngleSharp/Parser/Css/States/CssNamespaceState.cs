namespace AngleSharp.Parser.Css.States
{
    using AngleSharp.Dom.Css;

    sealed class CssNamespaceState : CssParseState
    {
        public CssNamespaceState(CssTokenizer tokenizer)
            : base(tokenizer)
        {
        }

        public override CssRule Create(CssToken current)
        {
            var token = _tokenizer.Get();
            var rule = new CssNamespaceRule();
            rule.Prefix = GetRuleName(ref token);

            if (token.Type == CssTokenType.Url)
                rule.NamespaceUri = token.Data;

            _tokenizer.JumpToNextSemicolon();
            return rule;
        }
    }
}
