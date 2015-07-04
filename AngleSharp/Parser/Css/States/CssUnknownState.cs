namespace AngleSharp.Parser.Css.States
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Css;
    using System.Collections.Generic;

    sealed class CssUnknownState : CssParseState
    {
        public CssUnknownState(CssTokenizer tokenizer, CssParserOptions options)
            : base(tokenizer, options)
        {
        }

        public override CssRule Create(CssToken current)
        {
            if (_options.IsIncludingUnknownRules)
            {
                var unknown = new CssUnknownRule(current.Data);
                _tokenizer.State = CssParseMode.Text;
                unknown.Prelude = _tokenizer.Get().Data;
                _tokenizer.State = CssParseMode.Data;

                if (_tokenizer.Get().Type == CssTokenType.CurlyBracketOpen)
                    FillRules(unknown);

                return unknown;
            }
            else
            {
                RaiseErrorOccurred(CssParseError.UnknownAtRule, current);
                _tokenizer.SkipUnknownRule();
                return null;
            }
        }

        public CssValue CreateValue(ref CssToken token)
        {
            var important = false;
            return CreateValue(ref token, out important);
        }

        public List<CssMedium> CreateMedia(ref CssToken token)
        {
            var list = new List<CssMedium>();

            while (token.Type != CssTokenType.Eof)
            {
                var medium = CreateMedium(ref token);

                if (medium == null || token.IsNot(CssTokenType.Comma, CssTokenType.Eof))
                    throw new DomException(DomError.Syntax);

                list.Add(medium);
                token = _tokenizer.Get();
            }

            return list;
        }
    }
}
