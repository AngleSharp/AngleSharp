namespace AngleSharp.Svg
{
    using AngleSharp.Dom;
    using AngleSharp.Svg.Dom;
    using System;

    /// <summary>
    /// Provides string to SVGElement instance creation mappings.
    /// </summary>
    sealed class SvgElementFactory : IElementFactory<Document, SvgElement>
    {
        /// <summary>
        /// Returns a specialized SVGElement instance for the given tag name.
        /// </summary>
        /// <param name="document">The document that owns the element.</param>
        /// <param name="localName">The given tag name.</param>
        /// <param name="prefix">The prefix of the element, if any.</param>
        /// <param name="flags">The optional flags, if any.</param>
        /// <returns>The specialized SVGElement instance.</returns>
        public SvgElement Create(Document document, String localName, String? prefix = null, NodeFlags flags = NodeFlags.None)
        {
            if (localName.Equals(TagNames.Svg, StringComparison.OrdinalIgnoreCase))
            {
                return new SvgSvgElement(document, prefix);
            }

            if (localName.Equals(TagNames.Circle, StringComparison.OrdinalIgnoreCase))
            {
                return new SvgCircleElement(document, prefix);
            }

            if (localName.Equals(TagNames.Desc, StringComparison.OrdinalIgnoreCase))
            {
                return new SvgDescElement(document, prefix);
            }

            if (localName.Equals(TagNames.ForeignObject, StringComparison.OrdinalIgnoreCase))
            {
                return new SvgForeignObjectElement(document, prefix);
            }

            if (localName.Equals(TagNames.Title, StringComparison.OrdinalIgnoreCase))
            {
                return new SvgTitleElement(document, prefix);
            }

            return new SvgElement(document, localName, prefix, flags);
        }
    }
}