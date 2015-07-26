namespace AngleSharp.Parser.Html
{
    using AngleSharp.Dom;
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
            if (attributeName.Equals("definitionurl", StringComparison.Ordinal))
                return "definitionURL";

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
                return adjustedAttributeName;

            return attributeName;
        }

        #endregion

        #region Helpers

        static Boolean IsXmlNamespaceAttribute(String name)
        {
            return name.Length > 4 &&
                (name.Equals(Namespaces.XmlNsPrefix, StringComparison.Ordinal) ||
                    name.Equals("xmlns:xlink", StringComparison.Ordinal));
        }

        static Boolean IsXmlAttribute(String name)
        {
            return (name.Length > 4 && String.Compare("xml:", 0, name, 0, 4, StringComparison.Ordinal) == 0) &&
                (String.Compare(Tags.Base, 0, name, 4, 4, StringComparison.Ordinal) == 0 ||
                    String.Compare(AttributeNames.Lang, 0, name, 4, 4, StringComparison.Ordinal) == 0 ||
                    String.Compare(AttributeNames.Space, 0, name, 4, 5, StringComparison.Ordinal) == 0);
        }

        static Boolean IsXLinkAttribute(String name)
        {
            return (name.Length > 6 && String.Compare("xlink:", 0, name, 0, 6, StringComparison.Ordinal) == 0) &&
                (String.Compare(AttributeNames.Actuate, 0, name, 6, 7, StringComparison.Ordinal) == 0 ||
                    String.Compare(AttributeNames.Arcrole, 0, name, 6, 7, StringComparison.Ordinal) == 0 ||
                    String.Compare(AttributeNames.Href, 0, name, 6, 4, StringComparison.Ordinal) == 0 ||
                    String.Compare(AttributeNames.Role, 0, name, 6, 4, StringComparison.Ordinal) == 0 ||
                    String.Compare(AttributeNames.Show, 0, name, 6, 4, StringComparison.Ordinal) == 0 ||
                    String.Compare(AttributeNames.Type, 0, name, 6, 4, StringComparison.Ordinal) == 0 ||
                    String.Compare(AttributeNames.Title, 0, name, 6, 5, StringComparison.Ordinal) == 0);
        }

        #endregion
    }
}
