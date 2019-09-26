namespace AngleSharp.Mathml
{
    using AngleSharp.Dom;
    using AngleSharp.Mathml.Dom;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides string to MathElement instance creation mappings.
    /// </summary>
    sealed class MathElementFactory : IElementFactory<Document, MathElement>
    {
        private delegate MathElement Creator(Document owner, String prefix);

        private readonly Dictionary<String, Creator> creators = new Dictionary<String, Creator>(StringComparer.OrdinalIgnoreCase)
        {
            { TagNames.Mn, (document, prefix) => new MathNumberElement(document, prefix) },
            { TagNames.Mo, (document, prefix) => new MathOperatorElement(document, prefix) },
            { TagNames.Mi, (document, prefix) => new MathIdentifierElement(document, prefix) },
            { TagNames.Ms, (document, prefix) => new MathStringElement(document, prefix) },
            { TagNames.Mtext, (document, prefix) => new MathTextElement(document, prefix) },
            { TagNames.AnnotationXml, (document, prefix) => new MathAnnotationXmlElement(document, prefix) },
        };

        /// <summary>
        /// Returns a specialized MathMLElement instance for the given tag.
        /// </summary>
        /// <param name="document">The document that owns the element.</param>
        /// <param name="localName">The given tag name.</param>
        /// <param name="prefix">The prefix of the element, if any.</param>
        /// <param name="flags">The optional flags, if any.</param>
        /// <returns>The specialized MathMLElement instance.</returns>
        public MathElement Create(Document document, String localName, String prefix = null, NodeFlags flags = NodeFlags.None)
        {
            if (creators.TryGetValue(localName, out var creator))
            {
                return creator.Invoke(document, prefix);
            }

            return new MathElement(document, localName, prefix, flags);

        }
    }
}
