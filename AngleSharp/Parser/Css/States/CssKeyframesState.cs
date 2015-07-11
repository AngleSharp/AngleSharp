namespace AngleSharp.Parser.Css.States
{
    using AngleSharp.Css.Values;
    using AngleSharp.Dom.Css;
    using System.Collections.Generic;

    sealed class CssKeyframesState : CssParseState
    {
        public CssKeyframesState(CssTokenizer tokenizer, CssParserOptions options)
            : base(tokenizer, options)
        {
        }

        public override CssRule Create(CssToken current)
        {
            var token = _tokenizer.Get();
            var rule = new CssKeyframesRule(_options);
            rule.Name = GetRuleName(ref token);

            if (token.Type != CssTokenType.CurlyBracketOpen)
                return SkipDeclarations(token);

            FillKeyframeRules(rule);
            return rule;
        }

        /// <summary>
        /// Before the curly bracket of an @keyframes rule has been seen.
        /// </summary>
        public CssKeyframeRule CreateKeyframeRule(CssToken token)
        {
            var rule = new CssKeyframeRule(_options);
            rule.Key = CreateKeyframeSelector(ref token);

            if (rule.Key == null)
            {
                _tokenizer.JumpToEndOfDeclaration();
                return null;
            }

            FillDeclarations(rule.Style);
            return rule;
        }

        /// <summary>
        /// Called in the text for a frame in the @keyframes rule.
        /// </summary>
        public KeyframeSelector CreateKeyframeSelector(ref CssToken token)
        {
            var keys = new List<Percent>();

            while (token.Type != CssTokenType.Eof)
            {
                if (keys.Count > 0)
                {
                    if (token.Type == CssTokenType.CurlyBracketOpen)
                        break;
                    else if (token.Type != CssTokenType.Comma)
                        return null;

                    token = _tokenizer.Get();
                }

                if (token.Type == CssTokenType.Percentage)
                    keys.Add(new Percent(((CssUnitToken)token).Value));
                else if (token.Type == CssTokenType.Ident && token.Data.Equals(Keywords.From))
                    keys.Add(Percent.Zero);
                else if (token.Type == CssTokenType.Ident && token.Data.Equals(Keywords.To))
                    keys.Add(Percent.Hundred);
                else
                    return null;

                token = _tokenizer.Get();
            }

            return new KeyframeSelector(keys);
        }

        /// <summary>
        /// Fills the given keyframe rule with rules given by the tokens.
        /// </summary>
        void FillKeyframeRules(CssKeyframesRule parentRule)
        {
            var token = _tokenizer.Get();

            while (token.IsNot(CssTokenType.Eof, CssTokenType.CurlyBracketClose))
            {
                var rule = CreateKeyframeRule(token);

                if (rule != null)
                    parentRule.Rules.Add(rule, parentRule.Owner, parentRule);

                token = _tokenizer.Get();
            }
        }
    }
}
