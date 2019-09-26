namespace AngleSharp.Svg
{
    using AngleSharp.Dom;
    using AngleSharp.Svg.Dom;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides string to SVGElement instance creation mappings.
    /// </summary>
    sealed class SvgElementFactory : IElementFactory<Document, SvgElement>
    {
        private delegate SvgElement Creator(Document owner, String prefix);

        private readonly Dictionary<String, Creator> creators = new Dictionary<String, Creator>(StringComparer.OrdinalIgnoreCase)
        {
            { TagNames.Svg, (document, prefix) => new SvgSvgElement(document, prefix) },
            { TagNames.Circle, (document, prefix) => new SvgCircleElement(document, prefix) },
            { TagNames.Desc, (document, prefix) => new SvgDescElement(document, prefix) },
            { TagNames.ForeignObject, (document, prefix) => new SvgForeignObjectElement(document, prefix) },
            { TagNames.Title, (document, prefix) => new SvgTitleElement(document, prefix) },
        };

        /// <summary>
        /// Returns a specialized SVGElement instance for the given tag name.
        /// </summary>
        /// <param name="document">The document that owns the element.</param>
        /// <param name="localName">The given tag name.</param>
        /// <param name="prefix">The prefix of the element, if any.</param>
        /// <param name="flags">The optional flags, if any.</param>
        /// <returns>The specialized SVGElement instance.</returns>
        public SvgElement Create(Document document, String localName, String prefix = null, NodeFlags flags = NodeFlags.None)
        {
            if (creators.TryGetValue(localName, out var creator))
            {
                return creator.Invoke(document, prefix);
            }

            return new SvgElement(document, localName, prefix, flags);
        }
    }
}
