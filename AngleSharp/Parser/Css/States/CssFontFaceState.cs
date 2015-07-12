namespace AngleSharp.Parser.Css.States
{
    using AngleSharp.Css;
    using AngleSharp.Dom.Css;
    using System;

    sealed class CssFontFaceState : CssParseState
    {
        public CssFontFaceState(CssTokenizer tokenizer, CssParser parser)
            : base(tokenizer, parser)
        {
        }

        public override CssRule Create(CssToken current)
        {
            var token = _tokenizer.Get();
            var rule = new CssFontFaceRule(_parser);

            if (token.Type != CssTokenType.CurlyBracketOpen)
                return SkipDeclarations(token);

            FillFontFaceDeclarations(rule);
            return rule;
        }

        void FillFontFaceDeclarations(CssFontFaceRule rule)
        {
            var token = _tokenizer.Get();

            while (token.IsNot(CssTokenType.Eof, CssTokenType.CurlyBracketClose))
            {
                var property = CreateDeclarationWith(CreateProperty, ref token);

                if (property != null && property.HasValue)
                    rule.SetProperty(property);
            }
        }

        CssProperty CreateProperty(String propertyName)
        {
            if (propertyName.Equals(PropertyNames.Src, StringComparison.OrdinalIgnoreCase))
                return new CssSrcProperty();
            else if (propertyName.Equals(PropertyNames.UnicodeRange, StringComparison.OrdinalIgnoreCase))
                return new CssUnicodeRangeProperty();

            return Factory.Properties.Create(propertyName);
        }
    }
}
