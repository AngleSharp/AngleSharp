namespace AngleSharp.Factories
{
    using System;
    using System.Collections.Generic;
    using AngleSharp.Dom;
    using AngleSharp.Dom.Mathml;
    using AngleSharp.Html;

    /// <summary>
    /// Provides string to MathElement instance creation mappings.
    /// </summary>
    sealed class MathElementFactory
    {
        delegate MathElement Creator(Document owner, String prefix);

        readonly Dictionary<String, Creator> creators = new Dictionary<String, Creator>(StringComparer.OrdinalIgnoreCase)
        {
            { Tags.Mn, (document, prefix) => new MathNumberElement(document, prefix) },
            { Tags.Mo, (document, prefix) => new MathOperatorElement(document, prefix) },
            { Tags.Mi, (document, prefix) => new MathIdentifierElement(document, prefix) },
            { Tags.Ms, (document, prefix) => new MathStringElement(document, prefix) },
            { Tags.Mtext, (document, prefix) => new MathTextElement(document, prefix) },
            { Tags.AnnotationXml, (document, prefix) => new MathAnnotationXmlElement(document, prefix) }
        };

        /// <summary>
        /// Returns a specialized MathMLElement instance for the given tag.
        /// </summary>
        /// <param name="document">The document that owns the element.</param>
        /// <param name="localName">The given tag name.</param>
        /// <param name="prefix">The prefix of the element, if any.</param>
        /// <returns>The specialized MathMLElement instance.</returns>
        public MathElement Create(Document document, String localName, String prefix = null)
        {
            Creator creator;

            if (creators.TryGetValue(localName, out creator))
                return creator(document, prefix);

            return new MathElement(document, localName, prefix);

        }
    }
}
