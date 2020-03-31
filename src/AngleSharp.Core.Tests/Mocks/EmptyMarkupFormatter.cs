using AngleSharp.Dom;
using System;

namespace AngleSharp.Core.Tests.Mocks
{
    class EmptyMarkupFormatter : IMarkupFormatter
    {
        string IMarkupFormatter.Comment(IComment comment) => string.Empty;

        string IMarkupFormatter.Doctype(IDocumentType doctype) => string.Empty;

        string IMarkupFormatter.Processing(IProcessingInstruction processing) => string.Empty;

        string IMarkupFormatter.Text(ICharacterData text) => string.Empty;

        string IMarkupFormatter.LiteralText(ICharacterData text) => string.Empty;

        string IMarkupFormatter.OpenTag(IElement element, Boolean selfClosing) => string.Empty;

        string IMarkupFormatter.CloseTag(IElement element, Boolean selfClosing) => string.Empty;
    }
}
