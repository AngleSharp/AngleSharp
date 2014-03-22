namespace AngleSharp.DOM.Mathml
{
    using System;

    internal class MathFactory
    {
        /// <summary>
        /// Returns a specialized MathMLElement instance for the given tag name.
        /// </summary>
        /// <param name="tagName">The given tag name.</param>
        /// <param name="document">The document that owns the element.</param>
        /// <returns>The specialized MathMLElement instance.</returns>
        public static MathElement Create(String tagName, Document document)
        {
            switch (tagName)
            {
                case Tags.MN:              return new MathNumberElement { OwnerDocument = document };
                case Tags.MO:              return new MathOperatorElement { OwnerDocument = document };
                case Tags.MI:              return new MathIdentifierElement { OwnerDocument = document };
                case Tags.MS:              return new MathStringElement { OwnerDocument = document };
                case Tags.MTEXT:           return new MathTextElement { OwnerDocument = document };
                case Tags.ANNOTATION_XML:  return new MathAnnotationXmlElement { OwnerDocument = document };
                default:                   return new MathElement { NodeName = tagName, OwnerDocument = document };
            }
        }
    }
}
