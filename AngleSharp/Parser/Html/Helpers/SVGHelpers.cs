namespace AngleSharp.Parser.Html
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A collection of useful helpers when working with SVG.
    /// </summary>
    static class SvgHelpers
    {
        #region Fields and Initialization

        static readonly Dictionary<String, String> tagNames = new Dictionary<String, String>();
        static readonly Dictionary<String, String> attributeNames = new Dictionary<String, String>();

        static SvgHelpers()
        {
            tagNames.Add("altglyph", "altGlyph");
            tagNames.Add("altglyphdef", "altGlyphDef");
            tagNames.Add("altglyphitem", "altGlyphItem");
            tagNames.Add("animatecolor", "animateColor");
            tagNames.Add("animatemotion", "animateMotion");
            tagNames.Add("animatetransform", "animateTransform");
            tagNames.Add("clippath", "clipPath");
            tagNames.Add("feblend", "feBlend");
            tagNames.Add("fecolormatrix", "feColorMatrix");
            tagNames.Add("fecomponenttransfer", "feComponentTransfer");
            tagNames.Add("fecomposite", "feComposite");
            tagNames.Add("feconvolvematrix", "feConvolveMatrix");
            tagNames.Add("fediffuselighting", "feDiffuseLighting");
            tagNames.Add("fedisplacementmap", "feDisplacementMap");
            tagNames.Add("fedistantlight", "feDistantLight");
            tagNames.Add("feflood", "feFlood");
            tagNames.Add("fefunca", "feFuncA");
            tagNames.Add("fefuncb", "feFuncB");
            tagNames.Add("fefuncg", "feFuncG");
            tagNames.Add("fefuncr", "feFuncR");
            tagNames.Add("fegaussianblur", "feGaussianBlur");
            tagNames.Add("feimage", "feImage");
            tagNames.Add("femerge", "feMerge");
            tagNames.Add("femergenode", "feMergeNode");
            tagNames.Add("femorphology", "feMorphology");
            tagNames.Add("feoffset", "feOffset");
            tagNames.Add("fepointlight", "fePointLight");
            tagNames.Add("fespecularlighting", "feSpecularLighting");
            tagNames.Add("fespotlight", "feSpotLight");
            tagNames.Add("fetile", "feTile");
            tagNames.Add("feturbulence", "feTurbulence");
            tagNames.Add("foreignobject", "foreignObject");
            tagNames.Add("glyphref", "glyphRef");
            tagNames.Add("lineargradient", "linearGradient");
            tagNames.Add("radialgradient", "radialGradient");
            tagNames.Add("textpath", "textPath");
            
            attributeNames.Add("attributename", "attributeName");
            attributeNames.Add("attributetype", "attributeType");
            attributeNames.Add("basefrequency", "baseFrequency");
            attributeNames.Add("baseprofile", "baseProfile");
            attributeNames.Add("calcmode", "calcMode");
            attributeNames.Add("clippathunits", "clipPathUnits");
            attributeNames.Add("contentscripttype", "contentScriptType");
            attributeNames.Add("contentstyletype", "contentStyleType");
            attributeNames.Add("diffuseconstant", "diffuseConstant");
            attributeNames.Add("edgemode", "edgeMode");
            attributeNames.Add("externalresourcesrequired", "externalResourcesRequired");
            attributeNames.Add("filterres", "filterRes");
            attributeNames.Add("filterunits", "filterUnits");
            attributeNames.Add("glyphref", "glyphRef");
            attributeNames.Add("gradienttransform", "gradientTransform");
            attributeNames.Add("gradientunits", "gradientUnits");
            attributeNames.Add("kernelmatrix", "kernelMatrix");
            attributeNames.Add("kernelunitlength", "kernelUnitLength");
            attributeNames.Add("keypoints", "keyPoints");
            attributeNames.Add("keysplines", "keySplines");
            attributeNames.Add("keytimes", "keyTimes");
            attributeNames.Add("lengthadjust", "lengthAdjust");
            attributeNames.Add("limitingconeangle", "limitingConeAngle");
            attributeNames.Add("markerheight", "markerHeight");
            attributeNames.Add("markerunits", "markerUnits");
            attributeNames.Add("markerwidth", "markerWidth");
            attributeNames.Add("maskcontentunits", "maskContentUnits");
            attributeNames.Add("maskunits", "maskUnits");
            attributeNames.Add("numoctaves", "numOctaves");
            attributeNames.Add("pathlength", "pathLength");
            attributeNames.Add("patterncontentunits", "patternContentUnits");
            attributeNames.Add("patterntransform", "patternTransform");
            attributeNames.Add("patternunits", "patternUnits");
            attributeNames.Add("pointsatx", "pointsAtX");
            attributeNames.Add("pointsaty", "pointsAtY");
            attributeNames.Add("pointsatz", "pointsAtZ");
            attributeNames.Add("preservealpha", "preserveAlpha");
            attributeNames.Add("preserveaspectratio", "preserveAspectRatio");
            attributeNames.Add("primitiveunits", "primitiveUnits");
            attributeNames.Add("refx", "refX");
            attributeNames.Add("refy", "refY");
            attributeNames.Add("repeatcount", "repeatCount");
            attributeNames.Add("repeatdur", "repeatDur");
            attributeNames.Add("requiredextensions", "requiredExtensions");
            attributeNames.Add("requiredfeatures", "requiredFeatures");
            attributeNames.Add("specularconstant", "specularConstant");
            attributeNames.Add("specularexponent", "specularExponent");
            attributeNames.Add("spreadmethod", "spreadMethod");
            attributeNames.Add("startoffset", "startOffset");
            attributeNames.Add("stddeviation", "stdDeviation");
            attributeNames.Add("stitchtiles", "stitchTiles");
            attributeNames.Add("surfacescale", "surfaceScale");
            attributeNames.Add("systemlanguage", "systemLanguage");
            attributeNames.Add("tablevalues", "tableValues");
            attributeNames.Add("targetx", "targetX");
            attributeNames.Add("targety", "targetY");
            attributeNames.Add("textlength", "textLength");
            attributeNames.Add("viewbox", "viewBox");
            attributeNames.Add("viewtarget", "viewTarget");
            attributeNames.Add("xchannelselector", "xChannelSelector");
            attributeNames.Add("ychannelselector", "yChannelSelector");
            attributeNames.Add("zoomandpan", "zoomAndPan");
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adjusts the tag name to the correct capitalization.
        /// </summary>
        /// <param name="tagName">The name of adjust.</param>
        /// <returns>The name with the correct capitalization.</returns>
        public static String AdjustSvgTagName(this String tagName)
        {
            String adjustedTagName;

            if (tagNames.TryGetValue(tagName, out adjustedTagName))
                return adjustedTagName;

            return tagName;
        }

        /// <summary>
        /// Adjusts the attribute name to the correct capitalization.
        /// </summary>
        /// <param name="attributeName">The name of adjust.</param>
        /// <returns>The name with the correct capitalization.</returns>
        public static String AdjustSvgAttributeName(this String attributeName)
        {
            String adjustedAttributeName;

            if (attributeNames.TryGetValue(attributeName, out adjustedAttributeName))
                return adjustedAttributeName;

            return attributeName;
        }

        #endregion
    }
}
