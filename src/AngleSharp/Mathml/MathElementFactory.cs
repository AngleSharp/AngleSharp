namespace AngleSharp.Mathml
{
    using AngleSharp.Dom;
    using AngleSharp.Mathml.Dom;
    using System;

    /// <summary>
    /// Provides string to MathElement instance creation mappings.
    /// </summary>
    sealed class MathElementFactory : IElementFactory<Document, MathElement>
    {
        /// <summary>
        /// Returns a specialized MathMLElement instance for the given tag.
        /// </summary>
        /// <param name="document">The document that owns the element.</param>
        /// <param name="localName">The given tag name.</param>
        /// <param name="prefix">The prefix of the element, if any.</param>
        /// <param name="flags">The optional flags, if any.</param>
        /// <returns>The specialized MathMLElement instance.</returns>
        public MathElement Create(Document document, String localName, String? prefix = null, NodeFlags flags = NodeFlags.None)
        {
            // REVIEW: is ToLowerInvariant() the right approach here? are TagNames always lowercase?
            return localName.ToLowerInvariant() switch
            {
                TagNames.Mn => new MathNumberElement(document, prefix),
                TagNames.Mo => new MathOperatorElement(document, prefix),
                TagNames.Mi => new MathIdentifierElement(document, prefix),
                TagNames.Ms => new MathStringElement(document, prefix),
                TagNames.Mtext => new MathTextElement(document, prefix),
                TagNames.AnnotationXml => new MathAnnotationXmlElement(document, prefix),
                _ => new MathElement(document, localName, prefix, flags)
            };
        }
    }
}