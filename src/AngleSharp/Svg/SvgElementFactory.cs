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
            // NOTE: When adding cases where the constant in TagNames is mixed-case, make sure to add a mixed-case pattern matching case, e.g.:
            // var tagName when tagName.Equals(TagNames._MixedCaseConstant, StringComparison.OrdinalIgnoreCase) => ...
            return localName.ToLowerInvariant() switch
            {
                TagNames._Svg => new SvgSvgElement(document, prefix),
                TagNames._Circle => new SvgCircleElement(document, prefix),
                TagNames._Desc => new SvgDescElement(document, prefix),
                TagNames._Title => new SvgTitleElement(document, prefix),
                var tagName when tagName.Equals(TagNames._ForeignObject, StringComparison.OrdinalIgnoreCase) => new SvgForeignObjectElement(document, prefix),
                _ => new SvgElement(document, localName, prefix, flags)
            };
        }
    }
}