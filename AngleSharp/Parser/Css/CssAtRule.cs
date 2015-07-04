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
        delegate CssParseState Creator(CssTokenizer parser, CssParserOptions options);

        static readonly Dictionary<String, Creator> creators = new Dictionary<String, Creator>();

        static CssAtRule()
        {
            creators.Add(RuleNames.Charset, (tokenizer, options) => new CssCharsetState(tokenizer, options));
            creators.Add(RuleNames.Page, (tokenizer, options) => new CssPageState(tokenizer, options));
            creators.Add(RuleNames.Import, (tokenizer, options) => new CssImportState(tokenizer, options));
            creators.Add(RuleNames.FontFace, (tokenizer, options) => new CssFontFaceState(tokenizer, options));
            creators.Add(RuleNames.Media, (tokenizer, options) => new CssMediaState(tokenizer, options));
            creators.Add(RuleNames.Namespace, (tokenizer, options) => new CssNamespaceState(tokenizer, options));
            creators.Add(RuleNames.Supports, (tokenizer, options) => new CssSupportsState(tokenizer, options));
            creators.Add(RuleNames.Keyframes, (tokenizer, options) => new CssKeyframesState(tokenizer, options));
            creators.Add(RuleNames.Document, (tokenizer, options) => new CssDocumentState(tokenizer, options));
        }

        /// <summary>
        /// Parses an @-rule with the given name, if there is any.
        /// </summary>
        public static CssRule CreateAtRule(this CssTokenizer tokenizer, CssToken token, CssParserOptions options)
        {
            Creator creator;

            if (creators.TryGetValue(token.Data, out creator))
                return creator(tokenizer, options).Create(token);

            return new CssUnknownState(tokenizer, options).Create(token);
        }

        /// <summary>
        /// Creates a rule with the enumeration of tokens.
        /// </summary>
        public static CssRule CreateRule(this CssTokenizer tokenizer, CssToken token, CssParserOptions options)
        {
            switch (token.Type)
            {
                case CssTokenType.AtKeyword:
                    return tokenizer.CreateAtRule(token, options);

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
                    return new CssStyleState(tokenizer, options).Create(token);
            }
        }
    }
}
