namespace AngleSharp.Parser.Html
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Mathml;
    using AngleSharp.Dom.Svg;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A collection of useful helpers when working with foreign content.
    /// </summary>
    static class HtmlForeignExtensions
    {
        #region Fields

        static readonly Dictionary<String, String> svgAttributeNames = new Dictionary<String, String>(StringComparer.Ordinal)
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

        static readonly Dictionary<String, String> svgAdjustedTagNames = new Dictionary<String, String>(StringComparer.Ordinal)
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
        public static String SanatizeSvgTagName(this String localName)
        {
            var adjustedTagName = default(String);

            if (svgAdjustedTagNames.TryGetValue(localName, out adjustedTagName))
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
        public static MathElement Setup(this MathElement element, HtmlTagToken tag)
        {
            var count = tag.Attributes.Count;

            for (var i = 0; i < count; i++)
            {
                var name = tag.Attributes[i].Key;
                var value = tag.Attributes[i].Value;
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
        public static SvgElement Setup(this SvgElement element, HtmlTagToken tag)
        {
            var count = tag.Attributes.Count;

            for (var i = 0; i < count; i++)
            {
                var name = tag.Attributes[i].Key;
                var value = tag.Attributes[i].Value;
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
        public static void AdjustAttribute(this Element element, String name, String value)
        {
            if (IsXLinkAttribute(name))
            {
                element.SetAttribute(NamespaceNames.XLinkUri, name.Substring(name.IndexOf(Symbols.Colon) + 1), value);
            }
            else if (IsXmlAttribute(name))
            {
                element.SetAttribute(NamespaceNames.XmlUri, name, value);
            }
            else if (IsXmlNamespaceAttribute(name))
            {
                element.SetAttribute(NamespaceNames.XmlNsUri, name, value);
            }
            else
            {
                element.SetOwnAttribute(name, value);
            }
        }

        /// <summary>
        /// Adjusts the attribute name to the correct capitalization.
        /// </summary>
        /// <param name="attributeName">The name of adjust.</param>
        /// <returns>The name with the correct capitalization.</returns>
        public static String AdjustToMathAttribute(this String attributeName)
        {
            if (attributeName.Is("definitionurl"))
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
        public static String AdjustToSvgAttribute(this String attributeName)
        {
            var adjustedAttributeName = default(String);

            if (svgAttributeNames.TryGetValue(attributeName, out adjustedAttributeName))
            {
                return adjustedAttributeName;
            }

            return attributeName;
        }

        #endregion

        #region Helpers

        static Boolean IsXmlNamespaceAttribute(String name)
        {
            return name.Length > 4 && (name.Is(NamespaceNames.XmlNsPrefix) || name.Is("xmlns:xlink"));
        }

        static Boolean IsXmlAttribute(String name)
        {
            return (name.Length > 7 && "xml:".EqualsSubset(name, 0, 4)) &&
                (TagNames.Base.EqualsSubset(name, 4, 4) || AttributeNames.Lang.EqualsSubset(name, 4, 4) ||
                 AttributeNames.Space.EqualsSubset(name, 4, 5));
        }

        static Boolean IsXLinkAttribute(String name)
        {
            return (name.Length > 9 && "xlink:".EqualsSubset(name, 0, 6)) &&
                (AttributeNames.Actuate.EqualsSubset(name, 6, 7) || AttributeNames.Arcrole.EqualsSubset(name, 6, 7) ||
                 AttributeNames.Href.EqualsSubset(name, 6, 4) || AttributeNames.Role.EqualsSubset(name, 6, 4) ||
                 AttributeNames.Show.EqualsSubset(name, 6, 4) || AttributeNames.Type.EqualsSubset(name, 6, 4) ||
                 AttributeNames.Title.EqualsSubset(name, 6, 5));
        }

        static Boolean EqualsSubset(this String a, String b, Int32 index, Int32 length)
        {
            return String.Compare(a, 0, b, index, length, StringComparison.Ordinal) == 0;
        }

        #endregion
    }
}
