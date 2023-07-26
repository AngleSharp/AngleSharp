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
            // REVIEW: is ToLowerInvariant() the right approach here? are TagNames always lowercase?
            return localName.ToLowerInvariant() switch
            {
                TagNames.Svg => new SvgSvgElement(document, prefix),
                TagNames.Circle => new SvgCircleElement(document, prefix),
                TagNames.Desc => new SvgDescElement(document, prefix),
                var tagName when tagName.Equals(TagNames.ForeignObject, StringComparison.OrdinalIgnoreCase) => new SvgForeignObjectElement(document, prefix),
                TagNames.Title => new SvgTitleElement(document, prefix),
                _ => new SvgElement(document, localName, prefix, flags)
            };
        }
    }
}