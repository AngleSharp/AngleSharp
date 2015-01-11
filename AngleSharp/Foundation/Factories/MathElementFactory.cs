namespace AngleSharp.Factories
{
    using AngleSharp.DOM;
    using AngleSharp.DOM.Mathml;
    using AngleSharp.Html;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides string to MathElement instance creation mappings.
    /// </summary>
    sealed class MathElementFactory
    {
        readonly Dictionary<String, Func<Document, MathElement>> creators = new Dictionary<String, Func<Document, MathElement>>(StringComparer.OrdinalIgnoreCase)
        {
            { Tags.Mn, document => new MathNumberElement(document) },
            { Tags.Mo, document => new MathOperatorElement(document) },
            { Tags.Mi, document => new MathIdentifierElement(document) },
            { Tags.Ms, document => new MathStringElement(document) },
            { Tags.Mtext, document => new MathTextElement(document) },
            { Tags.AnnotationXml, document => new MathAnnotationXmlElement(document) }
        };

        /// <summary>
        /// Returns a specialized MathMLElement instance for the given tag name.
        /// </summary>
        /// <param name="tag">The given tag name.</param>
        /// <param name="document">The document that owns the element.</param>
        /// <returns>The specialized MathMLElement instance.</returns>
        public MathElement Create(String tag, Document document)
        {
            Func<Document, MathElement> creator;

            if (creators.TryGetValue(tag, out creator))
                return creator(document);

            return new MathElement(document, tag);

        }
    }
}
