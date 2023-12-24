namespace AngleSharp.Html.Parser
{
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using Common;
    using Construction;
    using Tokens.Struct;

    /// <summary>
    /// A collection of useful helpers when working with foreign content.
    /// </summary>
    static class HtmlForeignExtensions
    {
        #region Fields
        private static readonly Dictionary<StringOrMemory, String> svgAttributeNames =
            new(OrdinalStringOrMemoryComparer.Instance)
        {
            { "attributename", "attributeName" },
            { "attributetype", "attributeType" },
            { "basefrequency", "baseFrequency" },
            { "baseprofile", "baseProfile" },
            { "calcmode", "calcMode" },
            { "clippathunits", "clipPathUnits" },
            { "contentscripttype", "contentScriptType" },
            { "contentstyletype", "contentStyleType" },
            { "diffuseconstant", "diffuseConstant" },
            { "edgemode", "edgeMode" },
            { "externalresourcesrequired", "externalResourcesRequired" },
            { "filterres", "filterRes" },
            { "filterunits", "filterUnits" },
            { "glyphref", "glyphRef" },
            { "gradienttransform", "gradientTransform" },
            { "gradientunits", "gradientUnits" },
            { "kernelmatrix", "kernelMatrix" },
            { "kernelunitlength", "kernelUnitLength" },
            { "keypoints", "keyPoints" },
            { "keysplines", "keySplines" },
            { "keytimes", "keyTimes" },
            { "lengthadjust", "lengthAdjust" },
            { "limitingconeangle", "limitingConeAngle" },
            { "markerheight", "markerHeight" },
            { "markerunits", "markerUnits" },
            { "markerwidth", "markerWidth" },
            { "maskcontentunits", "maskContentUnits" },
            { "maskunits", "maskUnits" },
            { "numoctaves", "numOctaves" },
            { "pathlength", "pathLength" },
            { "patterncontentunits", "patternContentUnits" },
            { "patterntransform", "patternTransform" },
            { "patternunits", "patternUnits" },
            { "pointsatx", "pointsAtX" },
            { "pointsaty", "pointsAtY" },
            { "pointsatz", "pointsAtZ" },
            { "preservealpha", "preserveAlpha" },
            { "preserveaspectratio", "preserveAspectRatio" },
            { "primitiveunits", "primitiveUnits" },
            { "refx", "refX" },
            { "refy", "refY" },
            { "repeatcount", "repeatCount" },
            { "repeatdur", "repeatDur" },
            { "requiredextensions", "requiredExtensions" },
            { "requiredfeatures", "requiredFeatures" },
            { "specularconstant", "specularConstant" },
            { "specularexponent", "specularExponent" },
            { "spreadmethod", "spreadMethod" },
            { "startoffset", "startOffset" },
            { "stddeviation", "stdDeviation" },
            { "stitchtiles", "stitchTiles" },
            { "surfacescale", "surfaceScale" },
            { "systemlanguage", "systemLanguage" },
            { "tablevalues", "tableValues" },
            { "targetx", "targetX" },
            { "targety", "targetY" },
            { "textlength", "textLength" },
            { "viewbox", "viewBox" },
            { "viewtarget", "viewTarget" },
            { "xchannelselector", "xChannelSelector" },
            { "ychannelselector", "yChannelSelector" },
            { "zoomandpan", "zoomAndPan" },
        };

        private static readonly Dictionary<StringOrMemory, String> svgAdjustedTagNames =
            new(OrdinalStringOrMemoryComparer.Instance)
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

        #endregion

        #region Methods

        /// <summary>
        /// Adjusts the tag name to the correct capitalization.
        /// </summary>
        /// <param name="localName">The name of adjust.</param>
        /// <returns>The name with the correct capitalization.</returns>
        public static StringOrMemory SanatizeSvgTagName(this StringOrMemory localName)
        {
            if (svgAdjustedTagNames.TryGetValue(localName, out var adjustedTagName))
            {
                return adjustedTagName;
            }

            return localName;
        }

        /// <summary>
        /// Setups a new math element with the attributes from the token.
        /// </summary>
        /// <param name="element">The element to setup.</param>
        /// <param name="tag">The tag token to use.</param>
        /// <returns>The finished element.</returns>
        public static IConstructableMathElement Setup(this IConstructableMathElement element, ref StructHtmlToken tag)
        {
            var count = tag.Attributes.Count;

            for (var i = 0; i < count; i++)
            {
                var attr = tag.Attributes[i];
                var name = attr.Name;
                var value = attr.Value;
                element.AdjustAttribute(name.AdjustToMathAttribute(), value);
            }

            return element;
        }

        /// <summary>
        /// Setups a new SVG element with the attributes from the token.
        /// </summary>
        /// <param name="element">The element to setup.</param>
        /// <param name="tag">The tag token to use.</param>
        /// <returns>The finished element.</returns>
        public static IConstructableSvgElement Setup(this IConstructableSvgElement element, ref StructHtmlToken tag)
        {
            var count = tag.Attributes.Count;

            for (var i = 0; i < count; i++)
            {
                var attr = tag.Attributes[i];
                var name = attr.Name;
                var value = attr.Value;
                element.AdjustAttribute(name.AdjustToSvgAttribute(), value);
            }

            return element;
        }

        /// <summary>
        /// Adds the attribute with the adjusted prefix, namespace and name.
        /// </summary>
        /// <param name="element">The element to host the attribute.</param>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="value">The value of the attribute.</param>
        public static void AdjustAttribute(this IConstructableElement element, StringOrMemory name, StringOrMemory value)
        {
            var ns = default(String);

            if (IsXLinkAttribute(name))
            {
                // var newName = name.Substring(name.IndexOf(Symbols.Colon) + 1);
                var newName = new StringOrMemory(name.Memory.Slice(name.Memory.Span.IndexOf(Symbols.Colon) + 1));

                if (newName.IsXmlName() && newName.IsQualifiedName())
                {
                    ns = NamespaceNames.XLinkUri;
                    name = newName;
                }
            }
            else if (IsXmlAttribute(name))
            {
                ns = NamespaceNames.XmlUri;
            }
            else if (IsXmlNamespaceAttribute(name))
            {
                ns = NamespaceNames.XmlNsUri;
            }

            if (ns is null)
            {
                element.SetOwnAttribute(name, value);
            }
            else
            {
                element.SetAttribute(ns, name, value);
            }
        }

        /// <summary>
        /// Adjusts the attribute name to the correct capitalization.
        /// </summary>
        /// <param name="attributeName">The name of adjust.</param>
        /// <returns>The name with the correct capitalization.</returns>
        public static StringOrMemory AdjustToMathAttribute(this StringOrMemory attributeName)
        {
            if (attributeName == "definitionurl")
            {
                return "definitionURL";
            }

            return attributeName;
        }

        /// <summary>
        /// Adjusts the attribute name to the correct capitalization.
        /// </summary>
        /// <param name="attributeName">The name of adjust.</param>
        /// <returns>The name with the correct capitalization.</returns>
        public static StringOrMemory AdjustToSvgAttribute(this StringOrMemory attributeName)
        {
            if (svgAttributeNames.TryGetValue(attributeName, out var adjustedAttributeName))
            {
                return adjustedAttributeName;
            }

            return attributeName;
        }

        #endregion

        #region Helpers

        private static Boolean IsXmlNamespaceAttribute(StringOrMemory name) =>
            name.Length > 4 && (name.Is(NamespaceNames.XmlNsPrefix) || name == "xmlns:xlink");

        private static Boolean IsXmlAttribute(StringOrMemory name) =>
            (name.Length > 7 && "xml:".EqualsSubset(name, 0, 4)) &&
            (TagNames.Base.EqualsSubset(name, 4, 4) || AttributeNames.Lang.EqualsSubset(name, 4, 4) ||
             AttributeNames.Space.EqualsSubset(name, 4, 5));

        private static Boolean IsXLinkAttribute(StringOrMemory name) =>
            (name.Length > 9 && "xlink:".EqualsSubset(name, 0, 6)) &&
            (AttributeNames.Actuate.EqualsSubset(name, 6, 7) || AttributeNames.Arcrole.EqualsSubset(name, 6, 7) ||
             AttributeNames.Href.EqualsSubset(name, 6, 4) || AttributeNames.Role.EqualsSubset(name, 6, 4) ||
             AttributeNames.Show.EqualsSubset(name, 6, 4) || AttributeNames.Type.EqualsSubset(name, 6, 4) ||
             AttributeNames.Title.EqualsSubset(name, 6, 5));

        private static Boolean EqualsSubset(this String a, StringOrMemory b, Int32 index, Int32 length)
        {
            if (length > a.Length)
            {
                return false;
            }

            if (length > b.Length - index)
            {
                return false;
            }

            return a.AsSpan().Slice(0, length).SequenceEqual(b.Memory.Span.Slice(index, length));
        }

        #endregion
    }
}
