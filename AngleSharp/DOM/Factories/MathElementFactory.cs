namespace AngleSharp.DOM.Mathml
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Provides string to MathElement instance creation mappings.
    /// </summary>
    sealed class MathElementFactory : ElementFactory<MathElement>
    {
        static readonly MathElementFactory Instance = new MathElementFactory();

        MathElementFactory()
        {
            creators.Add(Tags.Mn, document => new MathNumberElement { Owner = document });
            creators.Add(Tags.Mo, document => new MathOperatorElement { Owner = document });
            creators.Add(Tags.Mi, document => new MathIdentifierElement { Owner = document });
            creators.Add(Tags.Ms, document => new MathStringElement { Owner = document });
            creators.Add(Tags.Mtext, document => new MathTextElement { Owner = document });
            creators.Add(Tags.AnnotationXml, document => new MathAnnotationXmlElement { Owner = document });
        }

        protected override MathElement CreateDefault(String name, Document document)
        {
            return new MathElement(name) { Owner = document };
        }

        /// <summary>
        /// Returns a specialized MathMLElement instance for the given tag name.
        /// </summary>
        /// <param name="tagName">The given tag name.</param>
        /// <param name="document">The document that owns the element.</param>
        /// <returns>The specialized MathMLElement instance.</returns>
        public static MathElement Create(String tagName, Document document)
        {
            return Instance.CreateSpecific(tagName, document);

        }
    }
}
