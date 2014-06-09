namespace AngleSharp.DOM.Mathml
{
    using System;
    using System.Collections.Generic;

    internal class MathFactory
    {
        static readonly Dictionary<String, Func<Document, MathElement>> creators = new Dictionary<String, Func<Document, MathElement>>(StringComparer.OrdinalIgnoreCase);

        static MathFactory()
        {
            creators.Add(Tags.Mn, doc => new MathNumberElement { Owner = doc });
            creators.Add(Tags.Mo, doc => new MathOperatorElement { Owner = doc });
            creators.Add(Tags.Mi, doc => new MathIdentifierElement { Owner = doc });
            creators.Add(Tags.Ms, doc => new MathStringElement { Owner = doc });
            creators.Add(Tags.Mtext, doc => new MathTextElement { Owner = doc });
            creators.Add(Tags.AnnotationXml, doc => new MathAnnotationXmlElement { Owner = doc });
        }

        /// <summary>
        /// Returns a specialized MathMLElement instance for the given tag name.
        /// </summary>
        /// <param name="tagName">The given tag name.</param>
        /// <param name="document">The document that owns the element.</param>
        /// <returns>The specialized MathMLElement instance.</returns>
        public static MathElement Create(String tagName, Document document)
        {
            Func<Document, MathElement> creator;

            if (creators.TryGetValue(tagName, out creator))
                return creator(document);

            return new MathElement { NodeName = tagName, Owner = document };
        }
    }
}
