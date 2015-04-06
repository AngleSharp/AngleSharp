namespace AngleSharp.Factories
{
    using System;
    using System.Collections.Generic;
    using AngleSharp.Dom;
    using AngleSharp.Dom.Svg;
    using AngleSharp.Html;

    /// <summary>
    /// Provides string to SVGElement instance creation mappings.
    /// </summary>
    sealed class SvgElementFactory
    {
        delegate SvgElement Creator(Document owner, String prefix);

        readonly Dictionary<String, Creator> creators = new Dictionary<String, Creator>(StringComparer.OrdinalIgnoreCase)
        {
            { Tags.Svg, (document, prefix) => new SvgSvgElement(document, prefix) },
            { Tags.Circle, (document, prefix) => new SvgCircleElement(document, prefix) },
            { Tags.Desc, (document, prefix) => new SvgDescElement(document, prefix) },
            { Tags.ForeignObject, (document, prefix) => new SvgForeignObjectElement(document, prefix) },
            { Tags.Title, (document, prefix) => new SvgTitleElement(document, prefix) }
        };

        readonly Dictionary<String, String> adjustedTagNames = new Dictionary<String, String>(StringComparer.Ordinal)
        {
             { "altglyph", "altGlyph" },
             { "altglyphdef", "altGlyphDef" },
             { "altglyphitem", "altGlyphItem" },
             { "animatecolor", "animateColor" },
             { "animatemotion", "animateMotion" },
             { "animatetransform", "animateTransform" },
             { "clippath", "clipPath" },
             { "feblend", "feBlend" },
             { "fecolormatrix", "feColorMatrix" },
             { "fecomponenttransfer", "feComponentTransfer" },
             { "fecomposite", "feComposite" },
             { "feconvolvematrix", "feConvolveMatrix" },
             { "fediffuselighting", "feDiffuseLighting" },
             { "fedisplacementmap", "feDisplacementMap" },
             { "fedistantlight", "feDistantLight" },
             { "feflood", "feFlood" },
             { "fefunca", "feFuncA" },
             { "fefuncb", "feFuncB" },
             { "fefuncg", "feFuncG" },
             { "fefuncr", "feFuncR" },
             { "fegaussianblur", "feGaussianBlur" },
             { "feimage", "feImage" },
             { "femerge", "feMerge" },
             { "femergenode", "feMergeNode" },
             { "femorphology", "feMorphology" },
             { "feoffset", "feOffset" },
             { "fepointlight", "fePointLight" },
             { "fespecularlighting", "feSpecularLighting" },
             { "fespotlight", "feSpotLight" },
             { "fetile", "feTile" },
             { "feturbulence", "feTurbulence" },
             { "foreignobject", "foreignObject" },
             { "glyphref", "glyphRef" },
             { "lineargradient", "linearGradient" },
             { "radialgradient", "radialGradient" },
             { "textpath", "textPath" }
        };

        /// <summary>
        /// Returns a specialized SVGElement instance for the given tag name.
        /// </summary>
        /// <param name="document">The document that owns the element.</param>
        /// <param name="localName">The given tag name.</param>
        /// <param name="prefix">The prefix of the element, if any.</param>
        /// <returns>The specialized SVGElement instance.</returns>
        public SvgElement Create(Document document, String localName, String prefix = null)
        {
            Creator creator;

            if (creators.TryGetValue(localName, out creator))
                return creator(document, prefix);

            return new SvgElement(document, localName);
        }

        /// <summary>
        /// Returns a specialized SVGElement instance for the given tag name.
        /// </summary>
        /// <param name="document">The document that owns the element.</param>
        /// <param name="localName">The name to be sanatized.</param>
        /// <returns>The specialized SVGElement instance.</returns>
        public SvgElement CreateSanatized(Document document, String localName)
        {
            var newTag = SanatizeTag(localName);
            return Create(document, newTag);
        }

        /// <summary>
        /// Adjusts the tag name to the correct capitalization.
        /// </summary>
        /// <param name="localName">The name of adjust.</param>
        /// <returns>The name with the correct capitalization.</returns>
        String SanatizeTag(String localName)
        {
            String adjustedTagName;

            if (adjustedTagNames.TryGetValue(localName, out adjustedTagName))
                return adjustedTagName;

            return localName;
        }
    }
}
