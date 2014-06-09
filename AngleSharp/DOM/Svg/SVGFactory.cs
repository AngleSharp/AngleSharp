namespace AngleSharp.DOM.Svg
{
    using System;
    using System.Collections.Generic;

    internal class SVGFactory
    {
        static readonly Dictionary<String, Func<Document, SVGElement>> creators = new Dictionary<String, Func<Document, SVGElement>>(StringComparer.OrdinalIgnoreCase);

        static SVGFactory()
        {
            creators.Add(Tags.Svg, doc => new SVGSVGElement { Owner = doc });
            creators.Add(Tags.Circle, doc => new SVGCircleElement { Owner = doc });
            creators.Add(Tags.Desc, doc => new SVGDescElement { Owner = doc });
            creators.Add(Tags.ForeignObject, doc => new SVGForeignObjectElement { Owner = doc });
            creators.Add(Tags.Title, doc => new SVGTitleElement { Owner = doc });
        }

        /// <summary>
        /// Returns a specialized SVGElement instance for the given tag name.
        /// </summary>
        /// <param name="tagName">The given tag name.</param>
        /// <param name="document">The document that owns the element.</param>
        /// <returns>The specialized SVGElement instance.</returns>
        public static SVGElement Create(String tagName, Document document)
        {
            Func<Document, SVGElement> creator;

            if (creators.TryGetValue(tagName, out creator))
                return creator(document);

            return new SVGElement { NodeName = tagName, Owner = document };
        }
    }
}
