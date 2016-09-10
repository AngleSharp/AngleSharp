namespace AngleSharp.Services.Default
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Mathml;
    using AngleSharp.Html;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides string to MathElement instance creation mappings.
    /// </summary>
    sealed class MathElementFactory : IElementFactory<MathElement>
    {
        private delegate MathElement Creator(Document owner, String prefix);

        private readonly Dictionary<String, Creator> creators = new Dictionary<String, Creator>(StringComparer.OrdinalIgnoreCase)
        {
            { TagNames.Mn, (document, prefix) => new MathNumberElement(document, prefix) },
            { TagNames.Mo, (document, prefix) => new MathOperatorElement(document, prefix) },
            { TagNames.Mi, (document, prefix) => new MathIdentifierElement(document, prefix) },
            { TagNames.Ms, (document, prefix) => new MathStringElement(document, prefix) },
            { TagNames.Mtext, (document, prefix) => new MathTextElement(document, prefix) },
            { TagNames.AnnotationXml, (document, prefix) => new MathAnnotationXmlElement(document, prefix) }
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
            var creator = default(Creator);

            if (creators.TryGetValue(localName, out creator))
            {
                return creator(document, prefix);
            }

            return new MathElement(document, localName, prefix);

        }
    }
}
