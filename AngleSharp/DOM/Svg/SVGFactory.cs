namespace AngleSharp.DOM.Svg
{
    using System;

    internal class SVGFactory
    {
        /// <summary>
        /// Returns a specialized SVGElement instance for the given tag name.
        /// </summary>
        /// <param name="tagName">The given tag name.</param>
        /// <param name="document">The document that owns the element.</param>
        /// <returns>The specialized SVGElement instance.</returns>
        public static SVGElement Create(String tagName, Document document)
        {
            switch (tagName)
            {
                case Tags.SVG:            return new SVGSVGElement { OwnerDocument = document };
                case Tags.CIRCLE:         return new SVGCircleElement { OwnerDocument = document };
                case Tags.DESC:           return new SVGDescElement { OwnerDocument = document };
                case Tags.FOREIGNOBJECT:  return new SVGForeignObjectElement { OwnerDocument = document };
                case Tags.TITLE:          return new SVGTitleElement { OwnerDocument = document };
                default:                  return new SVGElement { NodeName = tagName, OwnerDocument = document };
            }
        }
    }
}
