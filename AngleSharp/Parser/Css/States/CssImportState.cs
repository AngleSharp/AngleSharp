namespace AngleSharp.Parser.Css.States
{
    using AngleSharp.Dom.Css;

    sealed class CssImportState : CssParseState
    {
        public CssImportState(CssTokenizer tokenizer, CssParserOptions options)
            : base(tokenizer, options)
        {
        }

        public override CssRule Create(CssToken current)
        {
            var token = _tokenizer.Get();
            var rule = new CssImportRule(_options);

            if (token.Is(CssTokenType.String, CssTokenType.Url))
            {
                rule.Href = token.Data;
                token = _tokenizer.Get();

                if (token.Type != CssTokenType.Semicolon)
                    FillMediaList(rule.Media, ref token);
            }

            _tokenizer.JumpToNextSemicolon();
            return rule;
        }
    }
}
