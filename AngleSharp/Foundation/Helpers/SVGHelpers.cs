using System;

namespace AngleSharp
{
    /// <summary>
    /// A collection of useful helpers when working with SVG.
    /// </summary>
    static class SVGHelpers
    {
        /// <summary>
        /// Adjusts the tag name to the correct capitalization.
        /// </summary>
        /// <param name="tagName">The name of adjust.</param>
        /// <returns>The name with the correct capitalization.</returns>
        public static String AdjustTagName(String tagName)
        {
            switch (tagName)
            {
                case "altglyph": return "altGlyph";
                case "altglyphdef": return "altGlyphDef";
                case "altglyphitem": return "altGlyphItem";
                case "animatecolor": return "animateColor";
                case "animatemotion": return "animateMotion";
                case "animatetransform": return "animateTransform";
                case "clippath": return "clipPath";
                case "feblend": return "feBlend";
                case "fecolormatrix": return "feColorMatrix";
                case "fecomponenttransfer": return "feComponentTransfer";
                case "fecomposite": return "feComposite";
                case "feconvolvematrix": return "feConvolveMatrix";
                case "fediffuselighting": return "feDiffuseLighting";
                case "fedisplacementmap": return "feDisplacementMap";
                case "fedistantlight": return "feDistantLight";
                case "feflood": return "feFlood";
                case "fefunca": return "feFuncA";
                case "fefuncb": return "feFuncB";
                case "fefuncg": return "feFuncG";
                case "fefuncr": return "feFuncR";
                case "fegaussianblur": return "feGaussianBlur";
                case "feimage": return "feImage";
                case "femerge": return "feMerge";
                case "femergenode": return "feMergeNode";
                case "femorphology": return "feMorphology";
                case "feoffset": return "feOffset";
                case "fepointlight": return "fePointLight";
                case "fespecularlighting": return "feSpecularLighting";
                case "fespotlight": return "feSpotLight";
                case "fetile": return "feTile";
                case "feturbulence": return "feTurbulence";
                case "foreignobject": return "foreignObject";
                case "glyphref": return "glyphRef";
                case "lineargradient": return "linearGradient";
                case "radialgradient": return "radialGradient";
                case "textpath": return "textPath";
                default: return tagName;
            }
        }

        /// <summary>
        /// Adjusts the attribute name to the correct capitalization.
        /// </summary>
        /// <param name="attributeName">The name of adjust.</param>
        /// <returns>The name with the correct capitalization.</returns>
        public static String AdjustAttributeName(String attributeName)
        {
            switch (attributeName)
            {
                case "attributename": return "attributeName";
                case "attributetype": return "attributeType";
                case "basefrequency": return "baseFrequency";
                case "baseprofile": return "baseProfile";
                case "calcmode": return "calcMode";
                case "clippathunits": return "clipPathUnits";
                case "contentscripttype": return "contentScriptType";
                case "contentstyletype": return "contentStyleType";
                case "diffuseconstant": return "diffuseConstant";
                case "edgemode": return "edgeMode";
                case "externalresourcesrequired": return "externalResourcesRequired";
                case "filterres": return "filterRes";
                case "filterunits": return "filterUnits";
                case "glyphref": return "glyphRef";
                case "gradienttransform": return "gradientTransform";
                case "gradientunits": return "gradientUnits";
                case "kernelmatrix": return "kernelMatrix";
                case "kernelunitlength": return "kernelUnitLength";
                case "keypoints": return "keyPoints";
                case "keysplines": return "keySplines";
                case "keytimes": return "keyTimes";
                case "lengthadjust": return "lengthAdjust";
                case "limitingconeangle": return "limitingConeAngle";
                case "markerheight": return "markerHeight";
                case "markerunits": return "markerUnits";
                case "markerwidth": return "markerWidth";
                case "maskcontentunits": return "maskContentUnits";
                case "maskunits": return "maskUnits";
                case "numoctaves": return "numOctaves";
                case "pathlength": return "pathLength";
                case "patterncontentunits": return "patternContentUnits";
                case "patterntransform": return "patternTransform";
                case "patternunits": return "patternUnits";
                case "pointsatx": return "pointsAtX";
                case "pointsaty": return "pointsAtY";
                case "pointsatz": return "pointsAtZ";
                case "preservealpha": return "preserveAlpha";
                case "preserveaspectratio": return "preserveAspectRatio";
                case "primitiveunits": return "primitiveUnits";
                case "refx": return "refX";
                case "refy": return "refY";
                case "repeatcount": return "repeatCount";
                case "repeatdur": return "repeatDur";
                case "requiredextensions": return "requiredExtensions";
                case "requiredfeatures": return "requiredFeatures";
                case "specularconstant": return "specularConstant";
                case "specularexponent": return "specularExponent";
                case "spreadmethod": return "spreadMethod";
                case "startoffset": return "startOffset";
                case "stddeviation": return "stdDeviation";
                case "stitchtiles": return "stitchTiles";
                case "surfacescale": return "surfaceScale";
                case "systemlanguage": return "systemLanguage";
                case "tablevalues": return "tableValues";
                case "targetx": return "targetX";
                case "targety": return "targetY";
                case "textlength": return "textLength";
                case "viewbox": return "viewBox";
                case "viewtarget": return "viewTarget";
                case "xchannelselector": return "xChannelSelector";
                case "ychannelselector": return "yChannelSelector";
                case "zoomandpan": return "zoomAndPan";
                default: return attributeName;
            }
        }
    }
}
