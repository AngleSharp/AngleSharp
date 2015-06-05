namespace AngleSharp.Parser.Css
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using AngleSharp.Css;
    using AngleSharp.Dom.Css;
    using AngleSharp.Parser.Css.States;

    /// <summary>
    /// Collects all possible @-rules for easy access.
    /// </summary>
    [DebuggerStepThrough]
    static class CssAtRule
    {
        delegate CssParseState Creator(CssTokenizer parser);

        static readonly Dictionary<String, Creator> creators = new Dictionary<String, Creator>();

        static CssAtRule()
        {
            creators.Add(RuleNames.Charset, (tokenizer) => new CssCharsetState(tokenizer));
            creators.Add(RuleNames.Page, (tokenizer) => new CssPageState(tokenizer));
            creators.Add(RuleNames.Import, (tokenizer) => new CssImportState(tokenizer));
            creators.Add(RuleNames.FontFace, (tokenizer) => new CssFontFaceState(tokenizer));
            creators.Add(RuleNames.Media, (tokenizer) => new CssMediaState(tokenizer));
            creators.Add(RuleNames.Namespace, (tokenizer) => new CssNamespaceState(tokenizer));
            creators.Add(RuleNames.Supports, (tokenizer) => new CssSupportsState(tokenizer));
            creators.Add(RuleNames.Keyframes, (tokenizer) => new CssKeyframesState(tokenizer));
            creators.Add(RuleNames.Document, (tokenizer) => new CssDocumentState(tokenizer));
        }

        /// <summary>
        /// Parses an @-rule with the given name, if there is any.
        /// </summary>
        /// <param name="tokenizer">The currently active tokenizer.</param>
        /// <param name="token">The name of the @-rule.</param>
        /// <returns>
        /// The created rule or null, if no rule could be created.
        /// </returns>
        public static CssRule CreateAtRule(this CssTokenizer tokenizer, CssToken token)
        {
            Creator creator;

            if (creators.TryGetValue(token.Data, out creator))
                return creator(tokenizer).Create(token);

            return new CssUnknownState(tokenizer).Create(token);
        }

        /// <summary>
        /// Creates a rule with the enumeration of tokens.
        /// </summary>
        public static CssRule CreateRule(this CssTokenizer tokenizer, CssToken token)
        {
            switch (token.Type)
            {
                case CssTokenType.AtKeyword:
                    return tokenizer.CreateAtRule(token);

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
                    return new CssStyleState(tokenizer).Create(token);
            }
        }
    }
}
