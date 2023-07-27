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
            if (localName.Equals(TagNames.Mn, StringComparison.OrdinalIgnoreCase))
            {
                return new MathNumberElement(document, prefix);
            }

            if (localName.Equals(TagNames.Mo, StringComparison.OrdinalIgnoreCase))
            {
                return new MathOperatorElement(document, prefix);
            }

            if (localName.Equals(TagNames.Mi, StringComparison.OrdinalIgnoreCase))
            {
                return new MathIdentifierElement(document, prefix);
            }

            if (localName.Equals(TagNames.Ms, StringComparison.OrdinalIgnoreCase))
            {
                return new MathStringElement(document, prefix);
            }

            if (localName.Equals(TagNames.Mtext, StringComparison.OrdinalIgnoreCase))
            {
                return new MathTextElement(document, prefix);
            }

            if (localName.Equals(TagNames.AnnotationXml, StringComparison.OrdinalIgnoreCase))
            {
                return new MathAnnotationXmlElement(document, prefix);
            }

            return new MathElement(document, localName, prefix, flags);
        }
    }
}