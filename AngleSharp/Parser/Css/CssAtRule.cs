namespace AngleSharp.Parser.Css
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using AngleSharp.Css;
    using AngleSharp.Dom.Css;

    /// <summary>
    /// Collects all possible @-rules for easy access.
    /// </summary>
    [DebuggerStepThrough]
    static class CssAtRule
    {
        delegate CssRule Creator(CssParser parser);

        static readonly Dictionary<String, Creator> creators = new Dictionary<String, Creator>();

        static CssAtRule()
        {
            creators.Add(RuleNames.Charset, (parser) => parser.CreateCharsetRule());
            creators.Add(RuleNames.Page, (parser) => parser.CreatePageRule());
            creators.Add(RuleNames.Import, (parser) => parser.CreateImportRule());
            creators.Add(RuleNames.FontFace, (parser) => parser.CreateFontFaceRule());
            creators.Add(RuleNames.Media, (parser) => parser.CreateMediaRule());
            creators.Add(RuleNames.Namespace, (parser) => parser.CreateNamespaceRule());
            creators.Add(RuleNames.Supports, (parser) => parser.CreateSupportsRule());
            creators.Add(RuleNames.Keyframes, (parser) => parser.CreateKeyframesRule());
            creators.Add(RuleNames.Document, (parser) => parser.CreateDocumentRule());
        }

        /// <summary>
        /// Parses an @-rule with the given name, if there is any.
        /// </summary>
        /// <param name="parser">The currently active parser.</param>
        /// <param name="name">The name of the @-rule.</param>
        /// <returns>The created rule or null, if no rule could be created.</returns>
        public static CssRule CreateAtRule(this CssParser parser, String name)
        {
            Creator creator;

            if (creators.TryGetValue(name, out creator))
                return creator(parser);

            return null;
        }
    }
}
