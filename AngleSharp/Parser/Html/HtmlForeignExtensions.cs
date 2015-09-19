namespace AngleSharp.Parser.Html
{
    using AngleSharp.Dom;
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

        #endregion

        #region Methods

        /// <summary>
        /// Adds the attribute with the adjusted prefix, namespace and name.
        /// </summary>
        /// <param name="element">The element to host the attribute.</param>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="value">The value of the attribute.</param>
        public static void AdjustAttribute(this Element element, String name, String value)
        {
            if (IsXLinkAttribute(name))
                element.SetAttribute(Namespaces.XLinkUri, name.Substring(name.IndexOf(Symbols.Colon) + 1), value);
            else if (IsXmlAttribute(name))
                element.SetAttribute(Namespaces.XmlUri, name, value);
            else if (IsXmlNamespaceAttribute(name))
                element.SetAttribute(Namespaces.XmlNsUri, name, value);
            else
                element.SetAttribute(name, value);
        }

        /// <summary>
        /// Adjusts the attribute name to the correct capitalization.
        /// </summary>
        /// <param name="attributeName">The name of adjust.</param>
        /// <returns>The name with the correct capitalization.</returns>
        public static String AdjustToMathAttribute(this String attributeName)
        {
            return attributeName.Is("definitionurl") ? "definitionURL" : attributeName;
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
                return adjustedAttributeName;

            return attributeName;
        }

        #endregion

        #region Helpers

        static Boolean IsXmlNamespaceAttribute(String name)
        {
            return name.Length > 4 && (name.Is(Namespaces.XmlNsPrefix) || name.Is("xmlns:xlink"));
        }

        static Boolean IsXmlAttribute(String name)
        {
            return (name.Length > 7 && "xml:".EqualsSubset(name, 0, 4)) &&
                (Tags.Base.EqualsSubset(name, 4, 4) || AttributeNames.Lang.EqualsSubset(name, 4, 4) ||
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
