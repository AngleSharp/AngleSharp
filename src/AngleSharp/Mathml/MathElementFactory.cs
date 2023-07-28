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
            // NOTE: When adding cases where the constant in TagNames is mixed-case, make sure to add a mixed-case pattern matching case, e.g.:
            // var tagName when tagName.Equals(TagNames._MixedCaseConstant, StringComparison.OrdinalIgnoreCase) => ...
            return localName.ToLowerInvariant() switch
            {
                TagNames._Mn => new MathNumberElement(document, prefix),
                TagNames._Mo => new MathOperatorElement(document, prefix),
                TagNames._Mi => new MathIdentifierElement(document, prefix),
                TagNames._Ms => new MathStringElement(document, prefix),
                TagNames._Mtext => new MathTextElement(document, prefix),
                TagNames._AnnotationXml => new MathAnnotationXmlElement(document, prefix),
                _ => new MathElement(document, localName, prefix, flags)
            };
        }
    }
}