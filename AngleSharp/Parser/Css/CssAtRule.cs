namespace AngleSharp.Parser.Css
{
    using AngleSharp.Css;
    using AngleSharp.Dom.Css;
    using AngleSharp.Parser.Css.States;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>
    /// Collects all possible @-rules for easy access.
    /// </summary>
    [DebuggerStepThrough]
    static class CssAtRule
    {
        delegate CssParseState Creator(CssTokenizer tokenizer, CssParser parser);

        static readonly Dictionary<String, Creator> creators = new Dictionary<String, Creator>();

        static CssAtRule()
        {
            creators.Add(RuleNames.Charset, (tokenizer, parser) => new CssCharsetState(tokenizer, parser));
            creators.Add(RuleNames.Page, (tokenizer, parser) => new CssPageState(tokenizer, parser));
            creators.Add(RuleNames.Import, (tokenizer, parser) => new CssImportState(tokenizer, parser));
            creators.Add(RuleNames.FontFace, (tokenizer, parser) => new CssFontFaceState(tokenizer, parser));
            creators.Add(RuleNames.Media, (tokenizer, parser) => new CssMediaState(tokenizer, parser));
            creators.Add(RuleNames.Namespace, (tokenizer, parser) => new CssNamespaceState(tokenizer, parser));
            creators.Add(RuleNames.Supports, (tokenizer, parser) => new CssSupportsState(tokenizer, parser));
            creators.Add(RuleNames.Keyframes, (tokenizer, parser) => new CssKeyframesState(tokenizer, parser));
            creators.Add(RuleNames.Document, (tokenizer, parser) => new CssDocumentState(tokenizer, parser));
        }

        /// <summary>
        /// Parses an @-rule with the given name, if there is any.
        /// </summary>
        public static CssRule CreateAtRule(this CssTokenizer tokenizer, CssToken token, CssParser parser)
        {
            Creator creator;

            if (creators.TryGetValue(token.Data, out creator))
                return creator(tokenizer, parser).Create(token);

            return new CssUnknownState(tokenizer, parser).Create(token);
        }

        /// <summary>
        /// Creates a rule with the enumeration of tokens.
        /// </summary>
        public static CssRule CreateRule(this CssTokenizer tokenizer, CssToken token, CssParser parser)
        {
            switch (token.Type)
            {
                case CssTokenType.AtKeyword:
                    return tokenizer.CreateAtRule(token, parser);

                case CssTokenType.CurlyBracketOpen:
                    tokenizer.RaiseErrorOccurred(CssParseError.InvalidBlockStart, token.Position);
                    tokenizer.SkipUnknownRule();
                    return null;

                case CssTokenType.String:
                case CssTokenType.Url:
                case CssTokenType.CurlyBracketClose:
                case CssTokenType.RoundBracketClose:
                case CssTokenType.SquareBracketClose:
                    tokenizer.RaiseErrorOccurred(CssParseError.InvalidToken, token.Position);
                    tokenizer.SkipUnknownRule();
                    return null;

                default:
                    return new CssStyleState(tokenizer, parser).Create(token);
            }
        }
    }
}
