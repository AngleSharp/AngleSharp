namespace AngleSharp.Parser.Css.States
{
    using System.Collections.Generic;
    using AngleSharp.Dom;
    using AngleSharp.Dom.Css;

    sealed class CssUnknownState : CssParseState
    {
        public CssUnknownState(CssTokenizer tokenizer)
            : base(tokenizer)
        {
        }

        public override CssRule Create(CssToken current)
        {
            RaiseErrorOccurred(CssParseError.UnknownAtRule, current);
            _tokenizer.SkipUnknownRule();
            return null;
        }

        public CssValue CreateValue(ref CssToken token)
        {
            var important = false;
            return CreateValue(ref token, out important);
        }

        public List<CssMedium> CreateMediaList(ref CssToken token)
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
