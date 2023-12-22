namespace AngleSharp.Html.Parser
{
    using AngleSharp.Dom;
    using Dom;
    using System;
    using Common;
    using Construction;

    sealed class HtmlDomBuilder : HtmlDomBuilder<HtmlDocument, Element>
    {
        public HtmlDomBuilder(
            IHtmlElementFactory<HtmlDocument, Element> elementFactory,
            HtmlDocument document,
            HtmlTokenizerOptions? maybeOptions = null,
            String? stopAt = null)
            : base(
                elementFactory: elementFactory,
                document: document,
                maybeOptions: maybeOptions,
                emitWhitespaceTextNodes: true,
                shouldEnd: stopAt != null ? e => e.Prefix.Length == 0 && e.LocalName.Is(stopAt) : null)
        {
        }
    }
}
