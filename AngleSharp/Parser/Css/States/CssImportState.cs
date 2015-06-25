namespace AngleSharp.Parser.Css.States
{
    using AngleSharp.Dom.Collections;
    using AngleSharp.Dom.Css;

    sealed class CssImportState : CssParseState
    {
        public CssImportState(CssTokenizer tokenizer)
            : base(tokenizer)
        {
        }

        public override CssRule Create(CssToken current)
        {
            var token = _tokenizer.Get();
            var rule = new CssImportRule();

            if (token.Is(CssTokenType.String, CssTokenType.Url))
            {
                rule.Href = token.Data;
                token = _tokenizer.Get();
                rule.Media = token.Type == CssTokenType.Semicolon ? new MediaList() : CreateMediaList(ref token);
            }

            _tokenizer.JumpToNextSemicolon();
            return rule;
        }
    }
}
