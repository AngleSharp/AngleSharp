namespace AngleSharp.Extensions
{
    using AngleSharp.Dom.Events;
    using AngleSharp.Html;
    using AngleSharp.Parser.Html;
    using AngleSharp.Services;
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
        /// <returns>A stream of consumed tokens.</returns>
        public static IEnumerable<HtmlToken> Tokenize(this TextSource source, IEntityProvider provider = null, EventHandler<HtmlErrorEvent> errorHandler = null)
        {
            var resolver = provider ?? HtmlEntityService.Resolver;
            var htmlTokenizer = new HtmlTokenizer(source, resolver);
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
