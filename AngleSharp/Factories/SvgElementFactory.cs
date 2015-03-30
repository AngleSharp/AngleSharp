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

        /// <summary>
        /// Returns a specialized SVGElement instance for the given tag name.
        /// </summary>
        /// <param name="tag">The given tag name, which is sanatized.</param>
        /// <param name="document">The document that owns the element.</param>
        /// <returns>The specialized SVGElement instance.</returns>
        public SvgElement CreateSanatized(String tag, Document document)
        {
            var newTag = SanatizeTag(tag);
            return Create(newTag, document);
        }

        /// <summary>
        /// Adjusts the tag name to the correct capitalization.
        /// </summary>
        /// <param name="tagName">The name of adjust.</param>
        /// <returns>The name with the correct capitalization.</returns>
        String SanatizeTag(String tagName)
        {
            String adjustedTagName;

            if (adjustedTagNames.TryGetValue(tagName, out adjustedTagName))
                return adjustedTagName;

            return tagName;
        }
    }
}
