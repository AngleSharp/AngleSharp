namespace AngleSharp.Factories
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Svg;
    using AngleSharp.Html;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides string to SVGElement instance creation mappings.
    /// </summary>
    sealed class SvgElementFactory
    {
        readonly Dictionary<String, Func<Document, SvgElement>> creators = new Dictionary<String, Func<Document, SvgElement>>(StringComparer.OrdinalIgnoreCase)
        {
            { Tags.Svg, document => new SvgSvgElement(document) },
            { Tags.Circle, document => new SvgCircleElement(document) },
            { Tags.Desc, document => new SvgDescElement(document) },
            { Tags.ForeignObject, document => new SvgForeignObjectElement(document) },
            { Tags.Title, document => new SvgTitleElement(document) }
        };

        /// <summary>
        /// Returns a specialized SVGElement instance for the given tag name.
        /// </summary>
        /// <param name="tag">The given tag name.</param>
        /// <param name="document">The document that owns the element.</param>
        /// <returns>The specialized SVGElement instance.</returns>
        public SvgElement Create(String tag, Document document)
        {
            Func<Document, SvgElement> creator;

            if (creators.TryGetValue(tag, out creator))
                return creator(document);

            return new SvgElement(document, tag);
        }
    }
}
