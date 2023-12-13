namespace AngleSharp.Html.Parser
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom.Events;
    using AngleSharp.Html.Parser.Tokens;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Extensions to access the underying tokenizer.
    /// </summary>
    public static class TokenizerExtensions
    {
        /// <summary>
        /// Performs the tokenization on the given text source.
        /// </summary>
        /// <param name="source">The source of the tokenization.</param>
        /// <param name="provider">The custom entity provider, if any.</param>
        /// <param name="errorHandler">The error handler to be used, if any.</param>
        /// <param name="options"></param>
        /// <returns>A stream of consumed tokens.</returns>
        public static IEnumerable<HtmlToken> Tokenize(
            this IReadOnlyTextSource source,
            IEntityProvider? provider = null,
            EventHandler<HtmlErrorEvent>? errorHandler = null,
            HtmlTokenizerOptions? options = null)
        {
            var resolver = provider ?? HtmlEntityProvider.Resolver;
            var htmlTokenizer = new HtmlTokenizer(source, resolver);

            if (options != null)
            {
                var ov = options.Value;

                htmlTokenizer.IsStrictMode = ov.IsStrictMode;
                htmlTokenizer.IsSupportingProcessingInstructions = ov.IsSupportingProcessingInstructions;
                htmlTokenizer.IsNotConsumingCharacterReferences = ov.IsNotConsumingCharacterReferences;
                htmlTokenizer.IsPreservingAttributeNames = ov.IsPreservingAttributeNames;

                htmlTokenizer.SkipRawText = ov.SkipRawText;
                htmlTokenizer.SkipScriptText = ov.SkipScriptText;
                htmlTokenizer.SkipDataText = ov.SkipDataText;
                htmlTokenizer.ShouldEmitAttribute = ov.ShouldEmitAttribute ?? (static (_, _) => true);
            }

            var token = default(HtmlToken);

            if (errorHandler != null)
            {
                htmlTokenizer.Error += errorHandler;
            }

            do
            {
                token = htmlTokenizer.Get();
                yield return token;
            }
            while (token.Type != HtmlTokenType.EndOfFile);
        }
    }
}
