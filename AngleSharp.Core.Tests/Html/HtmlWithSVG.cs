using System;
using AngleSharp.Dom;
using AngleSharp.Extensions;
using NUnit.Framework;

namespace AngleSharp.Core.Tests
{
    /// <summary>
    /// Tests from https://github.com/html5lib/html5lib-tests:
    /// tree-construction/tests10.dat
    /// tree-construction/tests11.dat
    /// </summary>
    [TestFixture]
    public class HtmlWithSVGTests
    {
        static IDocument Html(String code)
        {
            return code.ToHtmlDocument();
        }

        [Test]
        public void SvgCheckAttributesCaseNormalUnchanged()
        {
            var doc = Html(@"<!DOCTYPE html><body><svg attributeName='' attributeType='' baseFrequency='' baseProfile='' calcMode='' clipPathUnits='' contentScriptType='' contentStyleType='' diffuseConstant='' edgeMode='' externalResourcesRequired='' filterRes='' filterUnits='' glyphRef='' gradientTransform='' gradientUnits='' kernelMatrix='' kernelUnitLength='' keyPoints='' keySplines='' keyTimes='' lengthAdjust='' limitingConeAngle='' markerHeight='' markerUnits='' markerWidth='' maskContentUnits='' maskUnits='' numOctaves='' pathLength='' patternContentUnits='' patternTransform='' patternUnits='' pointsAtX='' pointsAtY='' pointsAtZ='' preserveAlpha='' preserveAspectRatio='' primitiveUnits='' refX='' refY='' repeatCount='' repeatDur='' requiredExtensions='' requiredFeatures='' specularConstant='' specularExponent='' spreadMethod='' startOffset='' stdDeviation='' stitchTiles='' surfaceScale='' systemLanguage='' tableValues='' targetX='' targetY='' textLength='' viewBox='' viewTarget='' xChannelSelector='' yChannelSelector='' zoomAndPan=''></svg>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(62, dochtml1body1svg0.Attributes.Count);
            Assert.AreEqual("svg", dochtml1body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("attributeName"));
            Assert.AreEqual("attributeName", dochtml1body1svg0.Attributes.Get("attributeName").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("attributeName").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("attributeType"));
            Assert.AreEqual("attributeType", dochtml1body1svg0.Attributes.Get("attributeType").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("attributeType").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("baseFrequency"));
            Assert.AreEqual("baseFrequency", dochtml1body1svg0.Attributes.Get("baseFrequency").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("baseFrequency").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("baseProfile"));
            Assert.AreEqual("baseProfile", dochtml1body1svg0.Attributes.Get("baseProfile").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("baseProfile").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("calcMode"));
            Assert.AreEqual("calcMode", dochtml1body1svg0.Attributes.Get("calcMode").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("calcMode").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("clipPathUnits"));
            Assert.AreEqual("clipPathUnits", dochtml1body1svg0.Attributes.Get("clipPathUnits").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("clipPathUnits").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("contentScriptType"));
            Assert.AreEqual("contentScriptType", dochtml1body1svg0.Attributes.Get("contentScriptType").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("contentScriptType").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("contentStyleType"));
            Assert.AreEqual("contentStyleType", dochtml1body1svg0.Attributes.Get("contentStyleType").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("contentStyleType").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("diffuseConstant"));
            Assert.AreEqual("diffuseConstant", dochtml1body1svg0.Attributes.Get("diffuseConstant").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("diffuseConstant").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("edgeMode"));
            Assert.AreEqual("edgeMode", dochtml1body1svg0.Attributes.Get("edgeMode").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("edgeMode").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("externalResourcesRequired"));
            Assert.AreEqual("externalResourcesRequired", dochtml1body1svg0.Attributes.Get("externalResourcesRequired").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("externalResourcesRequired").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("filterRes"));
            Assert.AreEqual("filterRes", dochtml1body1svg0.Attributes.Get("filterRes").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("filterRes").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("filterUnits"));
            Assert.AreEqual("filterUnits", dochtml1body1svg0.Attributes.Get("filterUnits").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("filterUnits").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("glyphRef"));
            Assert.AreEqual("glyphRef", dochtml1body1svg0.Attributes.Get("glyphRef").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("glyphRef").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("gradientTransform"));
            Assert.AreEqual("gradientTransform", dochtml1body1svg0.Attributes.Get("gradientTransform").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("gradientTransform").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("gradientUnits"));
            Assert.AreEqual("gradientUnits", dochtml1body1svg0.Attributes.Get("gradientUnits").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("gradientUnits").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("kernelMatrix"));
            Assert.AreEqual("kernelMatrix", dochtml1body1svg0.Attributes.Get("kernelMatrix").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("kernelMatrix").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("kernelUnitLength"));
            Assert.AreEqual("kernelUnitLength", dochtml1body1svg0.Attributes.Get("kernelUnitLength").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("kernelUnitLength").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("keyPoints"));
            Assert.AreEqual("keyPoints", dochtml1body1svg0.Attributes.Get("keyPoints").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("keyPoints").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("keySplines"));
            Assert.AreEqual("keySplines", dochtml1body1svg0.Attributes.Get("keySplines").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("keySplines").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("keyTimes"));
            Assert.AreEqual("keyTimes", dochtml1body1svg0.Attributes.Get("keyTimes").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("keyTimes").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("lengthAdjust"));
            Assert.AreEqual("lengthAdjust", dochtml1body1svg0.Attributes.Get("lengthAdjust").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("lengthAdjust").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("limitingConeAngle"));
            Assert.AreEqual("limitingConeAngle", dochtml1body1svg0.Attributes.Get("limitingConeAngle").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("limitingConeAngle").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("markerHeight"));
            Assert.AreEqual("markerHeight", dochtml1body1svg0.Attributes.Get("markerHeight").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("markerHeight").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("markerUnits"));
            Assert.AreEqual("markerUnits", dochtml1body1svg0.Attributes.Get("markerUnits").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("markerUnits").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("markerWidth"));
            Assert.AreEqual("markerWidth", dochtml1body1svg0.Attributes.Get("markerWidth").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("markerWidth").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("maskContentUnits"));
            Assert.AreEqual("maskContentUnits", dochtml1body1svg0.Attributes.Get("maskContentUnits").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("maskContentUnits").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("maskUnits"));
            Assert.AreEqual("maskUnits", dochtml1body1svg0.Attributes.Get("maskUnits").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("maskUnits").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("numOctaves"));
            Assert.AreEqual("numOctaves", dochtml1body1svg0.Attributes.Get("numOctaves").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("numOctaves").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("pathLength"));
            Assert.AreEqual("pathLength", dochtml1body1svg0.Attributes.Get("pathLength").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("pathLength").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("patternContentUnits"));
            Assert.AreEqual("patternContentUnits", dochtml1body1svg0.Attributes.Get("patternContentUnits").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("patternContentUnits").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("patternTransform"));
            Assert.AreEqual("patternTransform", dochtml1body1svg0.Attributes.Get("patternTransform").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("patternTransform").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("patternUnits"));
            Assert.AreEqual("patternUnits", dochtml1body1svg0.Attributes.Get("patternUnits").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("patternUnits").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("pointsAtX"));
            Assert.AreEqual("pointsAtX", dochtml1body1svg0.Attributes.Get("pointsAtX").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("pointsAtX").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("pointsAtY"));
            Assert.AreEqual("pointsAtY", dochtml1body1svg0.Attributes.Get("pointsAtY").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("pointsAtY").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("pointsAtZ"));
            Assert.AreEqual("pointsAtZ", dochtml1body1svg0.Attributes.Get("pointsAtZ").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("pointsAtZ").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("preserveAlpha"));
            Assert.AreEqual("preserveAlpha", dochtml1body1svg0.Attributes.Get("preserveAlpha").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("preserveAlpha").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("preserveAspectRatio"));
            Assert.AreEqual("preserveAspectRatio", dochtml1body1svg0.Attributes.Get("preserveAspectRatio").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("preserveAspectRatio").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("primitiveUnits"));
            Assert.AreEqual("primitiveUnits", dochtml1body1svg0.Attributes.Get("primitiveUnits").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("primitiveUnits").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("refX"));
            Assert.AreEqual("refX", dochtml1body1svg0.Attributes.Get("refX").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("refX").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("refY"));
            Assert.AreEqual("refY", dochtml1body1svg0.Attributes.Get("refY").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("refY").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("repeatCount"));
            Assert.AreEqual("repeatCount", dochtml1body1svg0.Attributes.Get("repeatCount").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("repeatCount").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("repeatDur"));
            Assert.AreEqual("repeatDur", dochtml1body1svg0.Attributes.Get("repeatDur").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("repeatDur").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("requiredExtensions"));
            Assert.AreEqual("requiredExtensions", dochtml1body1svg0.Attributes.Get("requiredExtensions").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("requiredExtensions").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("requiredFeatures"));
            Assert.AreEqual("requiredFeatures", dochtml1body1svg0.Attributes.Get("requiredFeatures").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("requiredFeatures").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("specularConstant"));
            Assert.AreEqual("specularConstant", dochtml1body1svg0.Attributes.Get("specularConstant").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("specularConstant").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("specularExponent"));
            Assert.AreEqual("specularExponent", dochtml1body1svg0.Attributes.Get("specularExponent").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("specularExponent").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("spreadMethod"));
            Assert.AreEqual("spreadMethod", dochtml1body1svg0.Attributes.Get("spreadMethod").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("spreadMethod").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("startOffset"));
            Assert.AreEqual("startOffset", dochtml1body1svg0.Attributes.Get("startOffset").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("startOffset").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("stdDeviation"));
            Assert.AreEqual("stdDeviation", dochtml1body1svg0.Attributes.Get("stdDeviation").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("stdDeviation").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("stitchTiles"));
            Assert.AreEqual("stitchTiles", dochtml1body1svg0.Attributes.Get("stitchTiles").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("stitchTiles").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("surfaceScale"));
            Assert.AreEqual("surfaceScale", dochtml1body1svg0.Attributes.Get("surfaceScale").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("surfaceScale").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("systemLanguage"));
            Assert.AreEqual("systemLanguage", dochtml1body1svg0.Attributes.Get("systemLanguage").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("systemLanguage").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("tableValues"));
            Assert.AreEqual("tableValues", dochtml1body1svg0.Attributes.Get("tableValues").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("tableValues").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("targetX"));
            Assert.AreEqual("targetX", dochtml1body1svg0.Attributes.Get("targetX").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("targetX").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("targetY"));
            Assert.AreEqual("targetY", dochtml1body1svg0.Attributes.Get("targetY").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("targetY").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("textLength"));
            Assert.AreEqual("textLength", dochtml1body1svg0.Attributes.Get("textLength").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("textLength").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("viewBox"));
            Assert.AreEqual("viewBox", dochtml1body1svg0.Attributes.Get("viewBox").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("viewBox").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("viewTarget"));
            Assert.AreEqual("viewTarget", dochtml1body1svg0.Attributes.Get("viewTarget").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("viewTarget").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("xChannelSelector"));
            Assert.AreEqual("xChannelSelector", dochtml1body1svg0.Attributes.Get("xChannelSelector").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("xChannelSelector").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("yChannelSelector"));
            Assert.AreEqual("yChannelSelector", dochtml1body1svg0.Attributes.Get("yChannelSelector").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("yChannelSelector").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("zoomAndPan"));
            Assert.AreEqual("zoomAndPan", dochtml1body1svg0.Attributes.Get("zoomAndPan").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("zoomAndPan").Value);
        }

        [Test]
        public void SvgCheckAttributesCaseUppercaseModified()
        {
            var doc = Html(@"<!DOCTYPE html><BODY><SVG ATTRIBUTENAME='' ATTRIBUTETYPE='' BASEFREQUENCY='' BASEPROFILE='' CALCMODE='' CLIPPATHUNITS='' CONTENTSCRIPTTYPE='' CONTENTSTYLETYPE='' DIFFUSECONSTANT='' EDGEMODE='' EXTERNALRESOURCESREQUIRED='' FILTERRES='' FILTERUNITS='' GLYPHREF='' GRADIENTTRANSFORM='' GRADIENTUNITS='' KERNELMATRIX='' KERNELUNITLENGTH='' KEYPOINTS='' KEYSPLINES='' KEYTIMES='' LENGTHADJUST='' LIMITINGCONEANGLE='' MARKERHEIGHT='' MARKERUNITS='' MARKERWIDTH='' MASKCONTENTUNITS='' MASKUNITS='' NUMOCTAVES='' PATHLENGTH='' PATTERNCONTENTUNITS='' PATTERNTRANSFORM='' PATTERNUNITS='' POINTSATX='' POINTSATY='' POINTSATZ='' PRESERVEALPHA='' PRESERVEASPECTRATIO='' PRIMITIVEUNITS='' REFX='' REFY='' REPEATCOUNT='' REPEATDUR='' REQUIREDEXTENSIONS='' REQUIREDFEATURES='' SPECULARCONSTANT='' SPECULAREXPONENT='' SPREADMETHOD='' STARTOFFSET='' STDDEVIATION='' STITCHTILES='' SURFACESCALE='' SYSTEMLANGUAGE='' TABLEVALUES='' TARGETX='' TARGETY='' TEXTLENGTH='' VIEWBOX='' VIEWTARGET='' XCHANNELSELECTOR='' YCHANNELSELECTOR='' ZOOMANDPAN=''></SVG>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(62, dochtml1body1svg0.Attributes.Count);
            Assert.AreEqual("svg", dochtml1body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("attributeName"));
            Assert.AreEqual("attributeName", dochtml1body1svg0.Attributes.Get("attributeName").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("attributeName").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("attributeType"));
            Assert.AreEqual("attributeType", dochtml1body1svg0.Attributes.Get("attributeType").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("attributeType").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("baseFrequency"));
            Assert.AreEqual("baseFrequency", dochtml1body1svg0.Attributes.Get("baseFrequency").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("baseFrequency").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("baseProfile"));
            Assert.AreEqual("baseProfile", dochtml1body1svg0.Attributes.Get("baseProfile").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("baseProfile").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("calcMode"));
            Assert.AreEqual("calcMode", dochtml1body1svg0.Attributes.Get("calcMode").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("calcMode").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("clipPathUnits"));
            Assert.AreEqual("clipPathUnits", dochtml1body1svg0.Attributes.Get("clipPathUnits").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("clipPathUnits").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("contentScriptType"));
            Assert.AreEqual("contentScriptType", dochtml1body1svg0.Attributes.Get("contentScriptType").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("contentScriptType").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("contentStyleType"));
            Assert.AreEqual("contentStyleType", dochtml1body1svg0.Attributes.Get("contentStyleType").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("contentStyleType").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("diffuseConstant"));
            Assert.AreEqual("diffuseConstant", dochtml1body1svg0.Attributes.Get("diffuseConstant").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("diffuseConstant").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("edgeMode"));
            Assert.AreEqual("edgeMode", dochtml1body1svg0.Attributes.Get("edgeMode").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("edgeMode").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("externalResourcesRequired"));
            Assert.AreEqual("externalResourcesRequired", dochtml1body1svg0.Attributes.Get("externalResourcesRequired").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("externalResourcesRequired").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("filterRes"));
            Assert.AreEqual("filterRes", dochtml1body1svg0.Attributes.Get("filterRes").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("filterRes").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("filterUnits"));
            Assert.AreEqual("filterUnits", dochtml1body1svg0.Attributes.Get("filterUnits").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("filterUnits").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("glyphRef"));
            Assert.AreEqual("glyphRef", dochtml1body1svg0.Attributes.Get("glyphRef").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("glyphRef").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("gradientTransform"));
            Assert.AreEqual("gradientTransform", dochtml1body1svg0.Attributes.Get("gradientTransform").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("gradientTransform").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("gradientUnits"));
            Assert.AreEqual("gradientUnits", dochtml1body1svg0.Attributes.Get("gradientUnits").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("gradientUnits").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("kernelMatrix"));
            Assert.AreEqual("kernelMatrix", dochtml1body1svg0.Attributes.Get("kernelMatrix").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("kernelMatrix").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("kernelUnitLength"));
            Assert.AreEqual("kernelUnitLength", dochtml1body1svg0.Attributes.Get("kernelUnitLength").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("kernelUnitLength").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("keyPoints"));
            Assert.AreEqual("keyPoints", dochtml1body1svg0.Attributes.Get("keyPoints").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("keyPoints").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("keySplines"));
            Assert.AreEqual("keySplines", dochtml1body1svg0.Attributes.Get("keySplines").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("keySplines").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("keyTimes"));
            Assert.AreEqual("keyTimes", dochtml1body1svg0.Attributes.Get("keyTimes").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("keyTimes").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("lengthAdjust"));
            Assert.AreEqual("lengthAdjust", dochtml1body1svg0.Attributes.Get("lengthAdjust").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("lengthAdjust").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("limitingConeAngle"));
            Assert.AreEqual("limitingConeAngle", dochtml1body1svg0.Attributes.Get("limitingConeAngle").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("limitingConeAngle").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("markerHeight"));
            Assert.AreEqual("markerHeight", dochtml1body1svg0.Attributes.Get("markerHeight").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("markerHeight").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("markerUnits"));
            Assert.AreEqual("markerUnits", dochtml1body1svg0.Attributes.Get("markerUnits").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("markerUnits").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("markerWidth"));
            Assert.AreEqual("markerWidth", dochtml1body1svg0.Attributes.Get("markerWidth").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("markerWidth").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("maskContentUnits"));
            Assert.AreEqual("maskContentUnits", dochtml1body1svg0.Attributes.Get("maskContentUnits").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("maskContentUnits").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("maskUnits"));
            Assert.AreEqual("maskUnits", dochtml1body1svg0.Attributes.Get("maskUnits").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("maskUnits").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("numOctaves"));
            Assert.AreEqual("numOctaves", dochtml1body1svg0.Attributes.Get("numOctaves").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("numOctaves").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("pathLength"));
            Assert.AreEqual("pathLength", dochtml1body1svg0.Attributes.Get("pathLength").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("pathLength").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("patternContentUnits"));
            Assert.AreEqual("patternContentUnits", dochtml1body1svg0.Attributes.Get("patternContentUnits").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("patternContentUnits").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("patternTransform"));
            Assert.AreEqual("patternTransform", dochtml1body1svg0.Attributes.Get("patternTransform").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("patternTransform").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("patternUnits"));
            Assert.AreEqual("patternUnits", dochtml1body1svg0.Attributes.Get("patternUnits").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("patternUnits").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("pointsAtX"));
            Assert.AreEqual("pointsAtX", dochtml1body1svg0.Attributes.Get("pointsAtX").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("pointsAtX").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("pointsAtY"));
            Assert.AreEqual("pointsAtY", dochtml1body1svg0.Attributes.Get("pointsAtY").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("pointsAtY").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("pointsAtZ"));
            Assert.AreEqual("pointsAtZ", dochtml1body1svg0.Attributes.Get("pointsAtZ").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("pointsAtZ").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("preserveAlpha"));
            Assert.AreEqual("preserveAlpha", dochtml1body1svg0.Attributes.Get("preserveAlpha").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("preserveAlpha").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("preserveAspectRatio"));
            Assert.AreEqual("preserveAspectRatio", dochtml1body1svg0.Attributes.Get("preserveAspectRatio").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("preserveAspectRatio").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("primitiveUnits"));
            Assert.AreEqual("primitiveUnits", dochtml1body1svg0.Attributes.Get("primitiveUnits").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("primitiveUnits").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("refX"));
            Assert.AreEqual("refX", dochtml1body1svg0.Attributes.Get("refX").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("refX").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("refY"));
            Assert.AreEqual("refY", dochtml1body1svg0.Attributes.Get("refY").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("refY").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("repeatCount"));
            Assert.AreEqual("repeatCount", dochtml1body1svg0.Attributes.Get("repeatCount").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("repeatCount").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("repeatDur"));
            Assert.AreEqual("repeatDur", dochtml1body1svg0.Attributes.Get("repeatDur").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("repeatDur").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("requiredExtensions"));
            Assert.AreEqual("requiredExtensions", dochtml1body1svg0.Attributes.Get("requiredExtensions").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("requiredExtensions").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("requiredFeatures"));
            Assert.AreEqual("requiredFeatures", dochtml1body1svg0.Attributes.Get("requiredFeatures").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("requiredFeatures").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("specularConstant"));
            Assert.AreEqual("specularConstant", dochtml1body1svg0.Attributes.Get("specularConstant").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("specularConstant").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("specularExponent"));
            Assert.AreEqual("specularExponent", dochtml1body1svg0.Attributes.Get("specularExponent").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("specularExponent").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("spreadMethod"));
            Assert.AreEqual("spreadMethod", dochtml1body1svg0.Attributes.Get("spreadMethod").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("spreadMethod").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("startOffset"));
            Assert.AreEqual("startOffset", dochtml1body1svg0.Attributes.Get("startOffset").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("startOffset").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("stdDeviation"));
            Assert.AreEqual("stdDeviation", dochtml1body1svg0.Attributes.Get("stdDeviation").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("stdDeviation").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("stitchTiles"));
            Assert.AreEqual("stitchTiles", dochtml1body1svg0.Attributes.Get("stitchTiles").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("stitchTiles").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("surfaceScale"));
            Assert.AreEqual("surfaceScale", dochtml1body1svg0.Attributes.Get("surfaceScale").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("surfaceScale").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("systemLanguage"));
            Assert.AreEqual("systemLanguage", dochtml1body1svg0.Attributes.Get("systemLanguage").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("systemLanguage").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("tableValues"));
            Assert.AreEqual("tableValues", dochtml1body1svg0.Attributes.Get("tableValues").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("tableValues").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("targetX"));
            Assert.AreEqual("targetX", dochtml1body1svg0.Attributes.Get("targetX").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("targetX").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("targetY"));
            Assert.AreEqual("targetY", dochtml1body1svg0.Attributes.Get("targetY").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("targetY").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("textLength"));
            Assert.AreEqual("textLength", dochtml1body1svg0.Attributes.Get("textLength").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("textLength").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("viewBox"));
            Assert.AreEqual("viewBox", dochtml1body1svg0.Attributes.Get("viewBox").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("viewBox").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("viewTarget"));
            Assert.AreEqual("viewTarget", dochtml1body1svg0.Attributes.Get("viewTarget").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("viewTarget").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("xChannelSelector"));
            Assert.AreEqual("xChannelSelector", dochtml1body1svg0.Attributes.Get("xChannelSelector").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("xChannelSelector").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("yChannelSelector"));
            Assert.AreEqual("yChannelSelector", dochtml1body1svg0.Attributes.Get("yChannelSelector").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("yChannelSelector").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("zoomAndPan"));
            Assert.AreEqual("zoomAndPan", dochtml1body1svg0.Attributes.Get("zoomAndPan").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("zoomAndPan").Value);
        }

        [Test]
        public void SvgCheckAttributesCaseLowercaseModified()
        {
            var doc = Html(@"<!DOCTYPE html><body><svg attributename='' attributetype='' basefrequency='' baseprofile='' calcmode='' clippathunits='' contentscripttype='' contentstyletype='' diffuseconstant='' edgemode='' externalresourcesrequired='' filterres='' filterunits='' glyphref='' gradienttransform='' gradientunits='' kernelmatrix='' kernelunitlength='' keypoints='' keysplines='' keytimes='' lengthadjust='' limitingconeangle='' markerheight='' markerunits='' markerwidth='' maskcontentunits='' maskunits='' numoctaves='' pathlength='' patterncontentunits='' patterntransform='' patternunits='' pointsatx='' pointsaty='' pointsatz='' preservealpha='' preserveaspectratio='' primitiveunits='' refx='' refy='' repeatcount='' repeatdur='' requiredextensions='' requiredfeatures='' specularconstant='' specularexponent='' spreadmethod='' startoffset='' stddeviation='' stitchtiles='' surfacescale='' systemlanguage='' tablevalues='' targetx='' targety='' textlength='' viewbox='' viewtarget='' xchannelselector='' ychannelselector='' zoomandpan=''></svg>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(62, dochtml1body1svg0.Attributes.Count);
            Assert.AreEqual("svg", dochtml1body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("attributeName"));
            Assert.AreEqual("attributeName", dochtml1body1svg0.Attributes.Get("attributeName").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("attributeName").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("attributeType"));
            Assert.AreEqual("attributeType", dochtml1body1svg0.Attributes.Get("attributeType").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("attributeType").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("baseFrequency"));
            Assert.AreEqual("baseFrequency", dochtml1body1svg0.Attributes.Get("baseFrequency").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("baseFrequency").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("baseProfile"));
            Assert.AreEqual("baseProfile", dochtml1body1svg0.Attributes.Get("baseProfile").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("baseProfile").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("calcMode"));
            Assert.AreEqual("calcMode", dochtml1body1svg0.Attributes.Get("calcMode").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("calcMode").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("clipPathUnits"));
            Assert.AreEqual("clipPathUnits", dochtml1body1svg0.Attributes.Get("clipPathUnits").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("clipPathUnits").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("contentScriptType"));
            Assert.AreEqual("contentScriptType", dochtml1body1svg0.Attributes.Get("contentScriptType").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("contentScriptType").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("contentStyleType"));
            Assert.AreEqual("contentStyleType", dochtml1body1svg0.Attributes.Get("contentStyleType").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("contentStyleType").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("diffuseConstant"));
            Assert.AreEqual("diffuseConstant", dochtml1body1svg0.Attributes.Get("diffuseConstant").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("diffuseConstant").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("edgeMode"));
            Assert.AreEqual("edgeMode", dochtml1body1svg0.Attributes.Get("edgeMode").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("edgeMode").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("externalResourcesRequired"));
            Assert.AreEqual("externalResourcesRequired", dochtml1body1svg0.Attributes.Get("externalResourcesRequired").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("externalResourcesRequired").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("filterRes"));
            Assert.AreEqual("filterRes", dochtml1body1svg0.Attributes.Get("filterRes").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("filterRes").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("filterUnits"));
            Assert.AreEqual("filterUnits", dochtml1body1svg0.Attributes.Get("filterUnits").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("filterUnits").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("glyphRef"));
            Assert.AreEqual("glyphRef", dochtml1body1svg0.Attributes.Get("glyphRef").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("glyphRef").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("gradientTransform"));
            Assert.AreEqual("gradientTransform", dochtml1body1svg0.Attributes.Get("gradientTransform").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("gradientTransform").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("gradientUnits"));
            Assert.AreEqual("gradientUnits", dochtml1body1svg0.Attributes.Get("gradientUnits").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("gradientUnits").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("kernelMatrix"));
            Assert.AreEqual("kernelMatrix", dochtml1body1svg0.Attributes.Get("kernelMatrix").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("kernelMatrix").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("kernelUnitLength"));
            Assert.AreEqual("kernelUnitLength", dochtml1body1svg0.Attributes.Get("kernelUnitLength").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("kernelUnitLength").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("keyPoints"));
            Assert.AreEqual("keyPoints", dochtml1body1svg0.Attributes.Get("keyPoints").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("keyPoints").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("keySplines"));
            Assert.AreEqual("keySplines", dochtml1body1svg0.Attributes.Get("keySplines").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("keySplines").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("keyTimes"));
            Assert.AreEqual("keyTimes", dochtml1body1svg0.Attributes.Get("keyTimes").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("keyTimes").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("lengthAdjust"));
            Assert.AreEqual("lengthAdjust", dochtml1body1svg0.Attributes.Get("lengthAdjust").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("lengthAdjust").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("limitingConeAngle"));
            Assert.AreEqual("limitingConeAngle", dochtml1body1svg0.Attributes.Get("limitingConeAngle").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("limitingConeAngle").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("markerHeight"));
            Assert.AreEqual("markerHeight", dochtml1body1svg0.Attributes.Get("markerHeight").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("markerHeight").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("markerUnits"));
            Assert.AreEqual("markerUnits", dochtml1body1svg0.Attributes.Get("markerUnits").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("markerUnits").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("markerWidth"));
            Assert.AreEqual("markerWidth", dochtml1body1svg0.Attributes.Get("markerWidth").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("markerWidth").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("maskContentUnits"));
            Assert.AreEqual("maskContentUnits", dochtml1body1svg0.Attributes.Get("maskContentUnits").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("maskContentUnits").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("maskUnits"));
            Assert.AreEqual("maskUnits", dochtml1body1svg0.Attributes.Get("maskUnits").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("maskUnits").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("numOctaves"));
            Assert.AreEqual("numOctaves", dochtml1body1svg0.Attributes.Get("numOctaves").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("numOctaves").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("pathLength"));
            Assert.AreEqual("pathLength", dochtml1body1svg0.Attributes.Get("pathLength").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("pathLength").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("patternContentUnits"));
            Assert.AreEqual("patternContentUnits", dochtml1body1svg0.Attributes.Get("patternContentUnits").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("patternContentUnits").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("patternTransform"));
            Assert.AreEqual("patternTransform", dochtml1body1svg0.Attributes.Get("patternTransform").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("patternTransform").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("patternUnits"));
            Assert.AreEqual("patternUnits", dochtml1body1svg0.Attributes.Get("patternUnits").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("patternUnits").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("pointsAtX"));
            Assert.AreEqual("pointsAtX", dochtml1body1svg0.Attributes.Get("pointsAtX").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("pointsAtX").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("pointsAtY"));
            Assert.AreEqual("pointsAtY", dochtml1body1svg0.Attributes.Get("pointsAtY").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("pointsAtY").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("pointsAtZ"));
            Assert.AreEqual("pointsAtZ", dochtml1body1svg0.Attributes.Get("pointsAtZ").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("pointsAtZ").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("preserveAlpha"));
            Assert.AreEqual("preserveAlpha", dochtml1body1svg0.Attributes.Get("preserveAlpha").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("preserveAlpha").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("preserveAspectRatio"));
            Assert.AreEqual("preserveAspectRatio", dochtml1body1svg0.Attributes.Get("preserveAspectRatio").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("preserveAspectRatio").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("primitiveUnits"));
            Assert.AreEqual("primitiveUnits", dochtml1body1svg0.Attributes.Get("primitiveUnits").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("primitiveUnits").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("refX"));
            Assert.AreEqual("refX", dochtml1body1svg0.Attributes.Get("refX").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("refX").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("refY"));
            Assert.AreEqual("refY", dochtml1body1svg0.Attributes.Get("refY").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("refY").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("repeatCount"));
            Assert.AreEqual("repeatCount", dochtml1body1svg0.Attributes.Get("repeatCount").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("repeatCount").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("repeatDur"));
            Assert.AreEqual("repeatDur", dochtml1body1svg0.Attributes.Get("repeatDur").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("repeatDur").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("requiredExtensions"));
            Assert.AreEqual("requiredExtensions", dochtml1body1svg0.Attributes.Get("requiredExtensions").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("requiredExtensions").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("requiredFeatures"));
            Assert.AreEqual("requiredFeatures", dochtml1body1svg0.Attributes.Get("requiredFeatures").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("requiredFeatures").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("specularConstant"));
            Assert.AreEqual("specularConstant", dochtml1body1svg0.Attributes.Get("specularConstant").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("specularConstant").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("specularExponent"));
            Assert.AreEqual("specularExponent", dochtml1body1svg0.Attributes.Get("specularExponent").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("specularExponent").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("spreadMethod"));
            Assert.AreEqual("spreadMethod", dochtml1body1svg0.Attributes.Get("spreadMethod").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("spreadMethod").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("startOffset"));
            Assert.AreEqual("startOffset", dochtml1body1svg0.Attributes.Get("startOffset").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("startOffset").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("stdDeviation"));
            Assert.AreEqual("stdDeviation", dochtml1body1svg0.Attributes.Get("stdDeviation").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("stdDeviation").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("stitchTiles"));
            Assert.AreEqual("stitchTiles", dochtml1body1svg0.Attributes.Get("stitchTiles").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("stitchTiles").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("surfaceScale"));
            Assert.AreEqual("surfaceScale", dochtml1body1svg0.Attributes.Get("surfaceScale").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("surfaceScale").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("systemLanguage"));
            Assert.AreEqual("systemLanguage", dochtml1body1svg0.Attributes.Get("systemLanguage").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("systemLanguage").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("tableValues"));
            Assert.AreEqual("tableValues", dochtml1body1svg0.Attributes.Get("tableValues").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("tableValues").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("targetX"));
            Assert.AreEqual("targetX", dochtml1body1svg0.Attributes.Get("targetX").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("targetX").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("targetY"));
            Assert.AreEqual("targetY", dochtml1body1svg0.Attributes.Get("targetY").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("targetY").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("textLength"));
            Assert.AreEqual("textLength", dochtml1body1svg0.Attributes.Get("textLength").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("textLength").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("viewBox"));
            Assert.AreEqual("viewBox", dochtml1body1svg0.Attributes.Get("viewBox").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("viewBox").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("viewTarget"));
            Assert.AreEqual("viewTarget", dochtml1body1svg0.Attributes.Get("viewTarget").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("viewTarget").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("xChannelSelector"));
            Assert.AreEqual("xChannelSelector", dochtml1body1svg0.Attributes.Get("xChannelSelector").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("xChannelSelector").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("yChannelSelector"));
            Assert.AreEqual("yChannelSelector", dochtml1body1svg0.Attributes.Get("yChannelSelector").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("yChannelSelector").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.Get("zoomAndPan"));
            Assert.AreEqual("zoomAndPan", dochtml1body1svg0.Attributes.Get("zoomAndPan").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.Get("zoomAndPan").Value);

        }

        [Test]
        public void SvgCheckTagCaseNormalUnchanged()
        {
            var doc = Html(@"<!DOCTYPE html><body><svg><altGlyph /><altGlyphDef /><altGlyphItem /><animateColor /><animateMotion /><animateTransform /><clipPath /><feBlend /><feColorMatrix /><feComponentTransfer /><feComposite /><feConvolveMatrix /><feDiffuseLighting /><feDisplacementMap /><feDistantLight /><feFlood /><feFuncA /><feFuncB /><feFuncG /><feFuncR /><feGaussianBlur /><feImage /><feMerge /><feMergeNode /><feMorphology /><feOffset /><fePointLight /><feSpecularLighting /><feSpotLight /><feTile /><feTurbulence /><foreignObject /><glyphRef /><linearGradient /><radialGradient /><textPath /></svg>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(36, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0.Attributes.Count);
            Assert.AreEqual("svg", dochtml1body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);

            var dochtml1body1svg0altGlyph0 = dochtml1body1svg0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1svg0altGlyph0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0altGlyph0.Attributes.Count);
            Assert.AreEqual("altGlyph", dochtml1body1svg0altGlyph0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0altGlyph0.NodeType);

            var dochtml1body1svg0altGlyphDef1 = dochtml1body1svg0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1svg0altGlyphDef1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0altGlyphDef1.Attributes.Count);
            Assert.AreEqual("altGlyphDef", dochtml1body1svg0altGlyphDef1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0altGlyphDef1.NodeType);

            var dochtml1body1svg0altGlyphItem2 = dochtml1body1svg0.ChildNodes[2] as Element;
            Assert.AreEqual(0, dochtml1body1svg0altGlyphItem2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0altGlyphItem2.Attributes.Count);
            Assert.AreEqual("altGlyphItem", dochtml1body1svg0altGlyphItem2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0altGlyphItem2.NodeType);

            var dochtml1body1svg0animateColor3 = dochtml1body1svg0.ChildNodes[3] as Element;
            Assert.AreEqual(0, dochtml1body1svg0animateColor3.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0animateColor3.Attributes.Count);
            Assert.AreEqual("animateColor", dochtml1body1svg0animateColor3.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0animateColor3.NodeType);

            var dochtml1body1svg0animateMotion4 = dochtml1body1svg0.ChildNodes[4] as Element;
            Assert.AreEqual(0, dochtml1body1svg0animateMotion4.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0animateMotion4.Attributes.Count);
            Assert.AreEqual("animateMotion", dochtml1body1svg0animateMotion4.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0animateMotion4.NodeType);

            var dochtml1body1svg0animateTransform5 = dochtml1body1svg0.ChildNodes[5] as Element;
            Assert.AreEqual(0, dochtml1body1svg0animateTransform5.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0animateTransform5.Attributes.Count);
            Assert.AreEqual("animateTransform", dochtml1body1svg0animateTransform5.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0animateTransform5.NodeType);

            var dochtml1body1svg0clipPath6 = dochtml1body1svg0.ChildNodes[6] as Element;
            Assert.AreEqual(0, dochtml1body1svg0clipPath6.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0clipPath6.Attributes.Count);
            Assert.AreEqual("clipPath", dochtml1body1svg0clipPath6.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0clipPath6.NodeType);

            var dochtml1body1svg0feBlend7 = dochtml1body1svg0.ChildNodes[7] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feBlend7.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feBlend7.Attributes.Count);
            Assert.AreEqual("feBlend", dochtml1body1svg0feBlend7.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feBlend7.NodeType);

            var dochtml1body1svg0feColorMatrix8 = dochtml1body1svg0.ChildNodes[8] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feColorMatrix8.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feColorMatrix8.Attributes.Count);
            Assert.AreEqual("feColorMatrix", dochtml1body1svg0feColorMatrix8.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feColorMatrix8.NodeType);

            var dochtml1body1svg0feComponentTransfer9 = dochtml1body1svg0.ChildNodes[9] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feComponentTransfer9.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feComponentTransfer9.Attributes.Count);
            Assert.AreEqual("feComponentTransfer", dochtml1body1svg0feComponentTransfer9.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feComponentTransfer9.NodeType);

            var dochtml1body1svg0feComposite10 = dochtml1body1svg0.ChildNodes[10] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feComposite10.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feComposite10.Attributes.Count);
            Assert.AreEqual("feComposite", dochtml1body1svg0feComposite10.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feComposite10.NodeType);

            var dochtml1body1svg0feConvolveMatrix11 = dochtml1body1svg0.ChildNodes[11] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feConvolveMatrix11.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feConvolveMatrix11.Attributes.Count);
            Assert.AreEqual("feConvolveMatrix", dochtml1body1svg0feConvolveMatrix11.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feConvolveMatrix11.NodeType);

            var dochtml1body1svg0feDiffuseLighting12 = dochtml1body1svg0.ChildNodes[12] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feDiffuseLighting12.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feDiffuseLighting12.Attributes.Count);
            Assert.AreEqual("feDiffuseLighting", dochtml1body1svg0feDiffuseLighting12.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feDiffuseLighting12.NodeType);

            var dochtml1body1svg0feDisplacementMap13 = dochtml1body1svg0.ChildNodes[13] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feDisplacementMap13.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feDisplacementMap13.Attributes.Count);
            Assert.AreEqual("feDisplacementMap", dochtml1body1svg0feDisplacementMap13.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feDisplacementMap13.NodeType);

            var dochtml1body1svg0feDistantLight14 = dochtml1body1svg0.ChildNodes[14] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feDistantLight14.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feDistantLight14.Attributes.Count);
            Assert.AreEqual("feDistantLight", dochtml1body1svg0feDistantLight14.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feDistantLight14.NodeType);

            var dochtml1body1svg0feFlood15 = dochtml1body1svg0.ChildNodes[15] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feFlood15.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feFlood15.Attributes.Count);
            Assert.AreEqual("feFlood", dochtml1body1svg0feFlood15.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feFlood15.NodeType);

            var dochtml1body1svg0feFuncA16 = dochtml1body1svg0.ChildNodes[16] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feFuncA16.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feFuncA16.Attributes.Count);
            Assert.AreEqual("feFuncA", dochtml1body1svg0feFuncA16.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feFuncA16.NodeType);

            var dochtml1body1svg0feFuncB17 = dochtml1body1svg0.ChildNodes[17] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feFuncB17.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feFuncB17.Attributes.Count);
            Assert.AreEqual("feFuncB", dochtml1body1svg0feFuncB17.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feFuncB17.NodeType);

            var dochtml1body1svg0feFuncG18 = dochtml1body1svg0.ChildNodes[18] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feFuncG18.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feFuncG18.Attributes.Count);
            Assert.AreEqual("feFuncG", dochtml1body1svg0feFuncG18.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feFuncG18.NodeType);

            var dochtml1body1svg0feFuncR19 = dochtml1body1svg0.ChildNodes[19] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feFuncR19.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feFuncR19.Attributes.Count);
            Assert.AreEqual("feFuncR", dochtml1body1svg0feFuncR19.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feFuncR19.NodeType);

            var dochtml1body1svg0feGaussianBlur20 = dochtml1body1svg0.ChildNodes[20] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feGaussianBlur20.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feGaussianBlur20.Attributes.Count);
            Assert.AreEqual("feGaussianBlur", dochtml1body1svg0feGaussianBlur20.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feGaussianBlur20.NodeType);

            var dochtml1body1svg0feImage21 = dochtml1body1svg0.ChildNodes[21] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feImage21.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feImage21.Attributes.Count);
            Assert.AreEqual("feImage", dochtml1body1svg0feImage21.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feImage21.NodeType);

            var dochtml1body1svg0feMerge22 = dochtml1body1svg0.ChildNodes[22] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feMerge22.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feMerge22.Attributes.Count);
            Assert.AreEqual("feMerge", dochtml1body1svg0feMerge22.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feMerge22.NodeType);

            var dochtml1body1svg0feMergeNode23 = dochtml1body1svg0.ChildNodes[23] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feMergeNode23.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feMergeNode23.Attributes.Count);
            Assert.AreEqual("feMergeNode", dochtml1body1svg0feMergeNode23.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feMergeNode23.NodeType);

            var dochtml1body1svg0feMorphology24 = dochtml1body1svg0.ChildNodes[24] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feMorphology24.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feMorphology24.Attributes.Count);
            Assert.AreEqual("feMorphology", dochtml1body1svg0feMorphology24.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feMorphology24.NodeType);

            var dochtml1body1svg0feOffset25 = dochtml1body1svg0.ChildNodes[25] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feOffset25.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feOffset25.Attributes.Count);
            Assert.AreEqual("feOffset", dochtml1body1svg0feOffset25.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feOffset25.NodeType);

            var dochtml1body1svg0fePointLight26 = dochtml1body1svg0.ChildNodes[26] as Element;
            Assert.AreEqual(0, dochtml1body1svg0fePointLight26.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0fePointLight26.Attributes.Count);
            Assert.AreEqual("fePointLight", dochtml1body1svg0fePointLight26.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0fePointLight26.NodeType);

            var dochtml1body1svg0feSpecularLighting27 = dochtml1body1svg0.ChildNodes[27] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feSpecularLighting27.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feSpecularLighting27.Attributes.Count);
            Assert.AreEqual("feSpecularLighting", dochtml1body1svg0feSpecularLighting27.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feSpecularLighting27.NodeType);

            var dochtml1body1svg0feSpotLight28 = dochtml1body1svg0.ChildNodes[28] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feSpotLight28.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feSpotLight28.Attributes.Count);
            Assert.AreEqual("feSpotLight", dochtml1body1svg0feSpotLight28.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feSpotLight28.NodeType);

            var dochtml1body1svg0feTile29 = dochtml1body1svg0.ChildNodes[29] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feTile29.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feTile29.Attributes.Count);
            Assert.AreEqual("feTile", dochtml1body1svg0feTile29.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feTile29.NodeType);

            var dochtml1body1svg0feTurbulence30 = dochtml1body1svg0.ChildNodes[30] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feTurbulence30.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feTurbulence30.Attributes.Count);
            Assert.AreEqual("feTurbulence", dochtml1body1svg0feTurbulence30.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feTurbulence30.NodeType);

            var dochtml1body1svg0foreignObject31 = dochtml1body1svg0.ChildNodes[31] as Element;
            Assert.AreEqual(0, dochtml1body1svg0foreignObject31.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0foreignObject31.Attributes.Count);
            Assert.AreEqual("foreignObject", dochtml1body1svg0foreignObject31.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0foreignObject31.NodeType);

            var dochtml1body1svg0glyphRef32 = dochtml1body1svg0.ChildNodes[32] as Element;
            Assert.AreEqual(0, dochtml1body1svg0glyphRef32.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0glyphRef32.Attributes.Count);
            Assert.AreEqual("glyphRef", dochtml1body1svg0glyphRef32.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0glyphRef32.NodeType);

            var dochtml1body1svg0linearGradient33 = dochtml1body1svg0.ChildNodes[33] as Element;
            Assert.AreEqual(0, dochtml1body1svg0linearGradient33.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0linearGradient33.Attributes.Count);
            Assert.AreEqual("linearGradient", dochtml1body1svg0linearGradient33.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0linearGradient33.NodeType);

            var dochtml1body1svg0radialGradient34 = dochtml1body1svg0.ChildNodes[34] as Element;
            Assert.AreEqual(0, dochtml1body1svg0radialGradient34.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0radialGradient34.Attributes.Count);
            Assert.AreEqual("radialGradient", dochtml1body1svg0radialGradient34.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0radialGradient34.NodeType);

            var dochtml1body1svg0textPath35 = dochtml1body1svg0.ChildNodes[35] as Element;
            Assert.AreEqual(0, dochtml1body1svg0textPath35.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0textPath35.Attributes.Count);
            Assert.AreEqual("textPath", dochtml1body1svg0textPath35.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0textPath35.NodeType);
        }

        [Test]
        public void SvgCheckTagCaseLowercaseModified()
        {
            var doc = Html(@"<!DOCTYPE html><body><svg><altglyph /><altglyphdef /><altglyphitem /><animatecolor /><animatemotion /><animatetransform /><clippath /><feblend /><fecolormatrix /><fecomponenttransfer /><fecomposite /><feconvolvematrix /><fediffuselighting /><fedisplacementmap /><fedistantlight /><feflood /><fefunca /><fefuncb /><fefuncg /><fefuncr /><fegaussianblur /><feimage /><femerge /><femergenode /><femorphology /><feoffset /><fepointlight /><fespecularlighting /><fespotlight /><fetile /><feturbulence /><foreignobject /><glyphref /><lineargradient /><radialgradient /><textpath /></svg>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(36, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0.Attributes.Count);
            Assert.AreEqual("svg", dochtml1body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);

            var dochtml1body1svg0altGlyph0 = dochtml1body1svg0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1svg0altGlyph0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0altGlyph0.Attributes.Count);
            Assert.AreEqual("altGlyph", dochtml1body1svg0altGlyph0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0altGlyph0.NodeType);

            var dochtml1body1svg0altGlyphDef1 = dochtml1body1svg0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1svg0altGlyphDef1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0altGlyphDef1.Attributes.Count);
            Assert.AreEqual("altGlyphDef", dochtml1body1svg0altGlyphDef1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0altGlyphDef1.NodeType);

            var dochtml1body1svg0altGlyphItem2 = dochtml1body1svg0.ChildNodes[2] as Element;
            Assert.AreEqual(0, dochtml1body1svg0altGlyphItem2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0altGlyphItem2.Attributes.Count);
            Assert.AreEqual("altGlyphItem", dochtml1body1svg0altGlyphItem2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0altGlyphItem2.NodeType);

            var dochtml1body1svg0animateColor3 = dochtml1body1svg0.ChildNodes[3] as Element;
            Assert.AreEqual(0, dochtml1body1svg0animateColor3.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0animateColor3.Attributes.Count);
            Assert.AreEqual("animateColor", dochtml1body1svg0animateColor3.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0animateColor3.NodeType);

            var dochtml1body1svg0animateMotion4 = dochtml1body1svg0.ChildNodes[4] as Element;
            Assert.AreEqual(0, dochtml1body1svg0animateMotion4.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0animateMotion4.Attributes.Count);
            Assert.AreEqual("animateMotion", dochtml1body1svg0animateMotion4.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0animateMotion4.NodeType);

            var dochtml1body1svg0animateTransform5 = dochtml1body1svg0.ChildNodes[5] as Element;
            Assert.AreEqual(0, dochtml1body1svg0animateTransform5.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0animateTransform5.Attributes.Count);
            Assert.AreEqual("animateTransform", dochtml1body1svg0animateTransform5.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0animateTransform5.NodeType);

            var dochtml1body1svg0clipPath6 = dochtml1body1svg0.ChildNodes[6] as Element;
            Assert.AreEqual(0, dochtml1body1svg0clipPath6.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0clipPath6.Attributes.Count);
            Assert.AreEqual("clipPath", dochtml1body1svg0clipPath6.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0clipPath6.NodeType);

            var dochtml1body1svg0feBlend7 = dochtml1body1svg0.ChildNodes[7] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feBlend7.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feBlend7.Attributes.Count);
            Assert.AreEqual("feBlend", dochtml1body1svg0feBlend7.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feBlend7.NodeType);

            var dochtml1body1svg0feColorMatrix8 = dochtml1body1svg0.ChildNodes[8] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feColorMatrix8.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feColorMatrix8.Attributes.Count);
            Assert.AreEqual("feColorMatrix", dochtml1body1svg0feColorMatrix8.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feColorMatrix8.NodeType);

            var dochtml1body1svg0feComponentTransfer9 = dochtml1body1svg0.ChildNodes[9] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feComponentTransfer9.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feComponentTransfer9.Attributes.Count);
            Assert.AreEqual("feComponentTransfer", dochtml1body1svg0feComponentTransfer9.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feComponentTransfer9.NodeType);

            var dochtml1body1svg0feComposite10 = dochtml1body1svg0.ChildNodes[10] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feComposite10.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feComposite10.Attributes.Count);
            Assert.AreEqual("feComposite", dochtml1body1svg0feComposite10.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feComposite10.NodeType);

            var dochtml1body1svg0feConvolveMatrix11 = dochtml1body1svg0.ChildNodes[11] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feConvolveMatrix11.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feConvolveMatrix11.Attributes.Count);
            Assert.AreEqual("feConvolveMatrix", dochtml1body1svg0feConvolveMatrix11.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feConvolveMatrix11.NodeType);

            var dochtml1body1svg0feDiffuseLighting12 = dochtml1body1svg0.ChildNodes[12] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feDiffuseLighting12.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feDiffuseLighting12.Attributes.Count);
            Assert.AreEqual("feDiffuseLighting", dochtml1body1svg0feDiffuseLighting12.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feDiffuseLighting12.NodeType);

            var dochtml1body1svg0feDisplacementMap13 = dochtml1body1svg0.ChildNodes[13] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feDisplacementMap13.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feDisplacementMap13.Attributes.Count);
            Assert.AreEqual("feDisplacementMap", dochtml1body1svg0feDisplacementMap13.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feDisplacementMap13.NodeType);

            var dochtml1body1svg0feDistantLight14 = dochtml1body1svg0.ChildNodes[14] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feDistantLight14.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feDistantLight14.Attributes.Count);
            Assert.AreEqual("feDistantLight", dochtml1body1svg0feDistantLight14.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feDistantLight14.NodeType);

            var dochtml1body1svg0feFlood15 = dochtml1body1svg0.ChildNodes[15] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feFlood15.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feFlood15.Attributes.Count);
            Assert.AreEqual("feFlood", dochtml1body1svg0feFlood15.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feFlood15.NodeType);

            var dochtml1body1svg0feFuncA16 = dochtml1body1svg0.ChildNodes[16] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feFuncA16.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feFuncA16.Attributes.Count);
            Assert.AreEqual("feFuncA", dochtml1body1svg0feFuncA16.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feFuncA16.NodeType);

            var dochtml1body1svg0feFuncB17 = dochtml1body1svg0.ChildNodes[17] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feFuncB17.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feFuncB17.Attributes.Count);
            Assert.AreEqual("feFuncB", dochtml1body1svg0feFuncB17.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feFuncB17.NodeType);

            var dochtml1body1svg0feFuncG18 = dochtml1body1svg0.ChildNodes[18] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feFuncG18.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feFuncG18.Attributes.Count);
            Assert.AreEqual("feFuncG", dochtml1body1svg0feFuncG18.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feFuncG18.NodeType);

            var dochtml1body1svg0feFuncR19 = dochtml1body1svg0.ChildNodes[19] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feFuncR19.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feFuncR19.Attributes.Count);
            Assert.AreEqual("feFuncR", dochtml1body1svg0feFuncR19.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feFuncR19.NodeType);

            var dochtml1body1svg0feGaussianBlur20 = dochtml1body1svg0.ChildNodes[20] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feGaussianBlur20.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feGaussianBlur20.Attributes.Count);
            Assert.AreEqual("feGaussianBlur", dochtml1body1svg0feGaussianBlur20.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feGaussianBlur20.NodeType);

            var dochtml1body1svg0feImage21 = dochtml1body1svg0.ChildNodes[21] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feImage21.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feImage21.Attributes.Count);
            Assert.AreEqual("feImage", dochtml1body1svg0feImage21.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feImage21.NodeType);

            var dochtml1body1svg0feMerge22 = dochtml1body1svg0.ChildNodes[22] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feMerge22.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feMerge22.Attributes.Count);
            Assert.AreEqual("feMerge", dochtml1body1svg0feMerge22.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feMerge22.NodeType);

            var dochtml1body1svg0feMergeNode23 = dochtml1body1svg0.ChildNodes[23] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feMergeNode23.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feMergeNode23.Attributes.Count);
            Assert.AreEqual("feMergeNode", dochtml1body1svg0feMergeNode23.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feMergeNode23.NodeType);

            var dochtml1body1svg0feMorphology24 = dochtml1body1svg0.ChildNodes[24] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feMorphology24.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feMorphology24.Attributes.Count);
            Assert.AreEqual("feMorphology", dochtml1body1svg0feMorphology24.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feMorphology24.NodeType);

            var dochtml1body1svg0feOffset25 = dochtml1body1svg0.ChildNodes[25] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feOffset25.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feOffset25.Attributes.Count);
            Assert.AreEqual("feOffset", dochtml1body1svg0feOffset25.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feOffset25.NodeType);

            var dochtml1body1svg0fePointLight26 = dochtml1body1svg0.ChildNodes[26] as Element;
            Assert.AreEqual(0, dochtml1body1svg0fePointLight26.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0fePointLight26.Attributes.Count);
            Assert.AreEqual("fePointLight", dochtml1body1svg0fePointLight26.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0fePointLight26.NodeType);

            var dochtml1body1svg0feSpecularLighting27 = dochtml1body1svg0.ChildNodes[27] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feSpecularLighting27.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feSpecularLighting27.Attributes.Count);
            Assert.AreEqual("feSpecularLighting", dochtml1body1svg0feSpecularLighting27.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feSpecularLighting27.NodeType);

            var dochtml1body1svg0feSpotLight28 = dochtml1body1svg0.ChildNodes[28] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feSpotLight28.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feSpotLight28.Attributes.Count);
            Assert.AreEqual("feSpotLight", dochtml1body1svg0feSpotLight28.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feSpotLight28.NodeType);

            var dochtml1body1svg0feTile29 = dochtml1body1svg0.ChildNodes[29] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feTile29.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feTile29.Attributes.Count);
            Assert.AreEqual("feTile", dochtml1body1svg0feTile29.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feTile29.NodeType);

            var dochtml1body1svg0feTurbulence30 = dochtml1body1svg0.ChildNodes[30] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feTurbulence30.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feTurbulence30.Attributes.Count);
            Assert.AreEqual("feTurbulence", dochtml1body1svg0feTurbulence30.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feTurbulence30.NodeType);

            var dochtml1body1svg0foreignObject31 = dochtml1body1svg0.ChildNodes[31] as Element;
            Assert.AreEqual(0, dochtml1body1svg0foreignObject31.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0foreignObject31.Attributes.Count);
            Assert.AreEqual("foreignObject", dochtml1body1svg0foreignObject31.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0foreignObject31.NodeType);

            var dochtml1body1svg0glyphRef32 = dochtml1body1svg0.ChildNodes[32] as Element;
            Assert.AreEqual(0, dochtml1body1svg0glyphRef32.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0glyphRef32.Attributes.Count);
            Assert.AreEqual("glyphRef", dochtml1body1svg0glyphRef32.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0glyphRef32.NodeType);

            var dochtml1body1svg0linearGradient33 = dochtml1body1svg0.ChildNodes[33] as Element;
            Assert.AreEqual(0, dochtml1body1svg0linearGradient33.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0linearGradient33.Attributes.Count);
            Assert.AreEqual("linearGradient", dochtml1body1svg0linearGradient33.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0linearGradient33.NodeType);

            var dochtml1body1svg0radialGradient34 = dochtml1body1svg0.ChildNodes[34] as Element;
            Assert.AreEqual(0, dochtml1body1svg0radialGradient34.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0radialGradient34.Attributes.Count);
            Assert.AreEqual("radialGradient", dochtml1body1svg0radialGradient34.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0radialGradient34.NodeType);

            var dochtml1body1svg0textPath35 = dochtml1body1svg0.ChildNodes[35] as Element;
            Assert.AreEqual(0, dochtml1body1svg0textPath35.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0textPath35.Attributes.Count);
            Assert.AreEqual("textPath", dochtml1body1svg0textPath35.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0textPath35.NodeType);
        }

        [Test]
        public void SvgCheckTagCaseUppercaseModified()
        {
            var doc = Html(@"<!DOCTYPE html><BODY><SVG><ALTGLYPH /><ALTGLYPHDEF /><ALTGLYPHITEM /><ANIMATECOLOR /><ANIMATEMOTION /><ANIMATETRANSFORM /><CLIPPATH /><FEBLEND /><FECOLORMATRIX /><FECOMPONENTTRANSFER /><FECOMPOSITE /><FECONVOLVEMATRIX /><FEDIFFUSELIGHTING /><FEDISPLACEMENTMAP /><FEDISTANTLIGHT /><FEFLOOD /><FEFUNCA /><FEFUNCB /><FEFUNCG /><FEFUNCR /><FEGAUSSIANBLUR /><FEIMAGE /><FEMERGE /><FEMERGENODE /><FEMORPHOLOGY /><FEOFFSET /><FEPOINTLIGHT /><FESPECULARLIGHTING /><FESPOTLIGHT /><FETILE /><FETURBULENCE /><FOREIGNOBJECT /><GLYPHREF /><LINEARGRADIENT /><RADIALGRADIENT /><TEXTPATH /></SVG>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(36, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0.Attributes.Count);
            Assert.AreEqual("svg", dochtml1body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);

            var dochtml1body1svg0altGlyph0 = dochtml1body1svg0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1svg0altGlyph0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0altGlyph0.Attributes.Count);
            Assert.AreEqual("altGlyph", dochtml1body1svg0altGlyph0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0altGlyph0.NodeType);

            var dochtml1body1svg0altGlyphDef1 = dochtml1body1svg0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1svg0altGlyphDef1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0altGlyphDef1.Attributes.Count);
            Assert.AreEqual("altGlyphDef", dochtml1body1svg0altGlyphDef1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0altGlyphDef1.NodeType);

            var dochtml1body1svg0altGlyphItem2 = dochtml1body1svg0.ChildNodes[2] as Element;
            Assert.AreEqual(0, dochtml1body1svg0altGlyphItem2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0altGlyphItem2.Attributes.Count);
            Assert.AreEqual("altGlyphItem", dochtml1body1svg0altGlyphItem2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0altGlyphItem2.NodeType);

            var dochtml1body1svg0animateColor3 = dochtml1body1svg0.ChildNodes[3] as Element;
            Assert.AreEqual(0, dochtml1body1svg0animateColor3.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0animateColor3.Attributes.Count);
            Assert.AreEqual("animateColor", dochtml1body1svg0animateColor3.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0animateColor3.NodeType);

            var dochtml1body1svg0animateMotion4 = dochtml1body1svg0.ChildNodes[4] as Element;
            Assert.AreEqual(0, dochtml1body1svg0animateMotion4.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0animateMotion4.Attributes.Count);
            Assert.AreEqual("animateMotion", dochtml1body1svg0animateMotion4.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0animateMotion4.NodeType);

            var dochtml1body1svg0animateTransform5 = dochtml1body1svg0.ChildNodes[5] as Element;
            Assert.AreEqual(0, dochtml1body1svg0animateTransform5.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0animateTransform5.Attributes.Count);
            Assert.AreEqual("animateTransform", dochtml1body1svg0animateTransform5.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0animateTransform5.NodeType);

            var dochtml1body1svg0clipPath6 = dochtml1body1svg0.ChildNodes[6] as Element;
            Assert.AreEqual(0, dochtml1body1svg0clipPath6.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0clipPath6.Attributes.Count);
            Assert.AreEqual("clipPath", dochtml1body1svg0clipPath6.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0clipPath6.NodeType);

            var dochtml1body1svg0feBlend7 = dochtml1body1svg0.ChildNodes[7] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feBlend7.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feBlend7.Attributes.Count);
            Assert.AreEqual("feBlend", dochtml1body1svg0feBlend7.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feBlend7.NodeType);

            var dochtml1body1svg0feColorMatrix8 = dochtml1body1svg0.ChildNodes[8] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feColorMatrix8.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feColorMatrix8.Attributes.Count);
            Assert.AreEqual("feColorMatrix", dochtml1body1svg0feColorMatrix8.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feColorMatrix8.NodeType);

            var dochtml1body1svg0feComponentTransfer9 = dochtml1body1svg0.ChildNodes[9] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feComponentTransfer9.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feComponentTransfer9.Attributes.Count);
            Assert.AreEqual("feComponentTransfer", dochtml1body1svg0feComponentTransfer9.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feComponentTransfer9.NodeType);

            var dochtml1body1svg0feComposite10 = dochtml1body1svg0.ChildNodes[10] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feComposite10.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feComposite10.Attributes.Count);
            Assert.AreEqual("feComposite", dochtml1body1svg0feComposite10.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feComposite10.NodeType);

            var dochtml1body1svg0feConvolveMatrix11 = dochtml1body1svg0.ChildNodes[11] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feConvolveMatrix11.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feConvolveMatrix11.Attributes.Count);
            Assert.AreEqual("feConvolveMatrix", dochtml1body1svg0feConvolveMatrix11.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feConvolveMatrix11.NodeType);

            var dochtml1body1svg0feDiffuseLighting12 = dochtml1body1svg0.ChildNodes[12] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feDiffuseLighting12.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feDiffuseLighting12.Attributes.Count);
            Assert.AreEqual("feDiffuseLighting", dochtml1body1svg0feDiffuseLighting12.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feDiffuseLighting12.NodeType);

            var dochtml1body1svg0feDisplacementMap13 = dochtml1body1svg0.ChildNodes[13] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feDisplacementMap13.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feDisplacementMap13.Attributes.Count);
            Assert.AreEqual("feDisplacementMap", dochtml1body1svg0feDisplacementMap13.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feDisplacementMap13.NodeType);

            var dochtml1body1svg0feDistantLight14 = dochtml1body1svg0.ChildNodes[14] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feDistantLight14.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feDistantLight14.Attributes.Count);
            Assert.AreEqual("feDistantLight", dochtml1body1svg0feDistantLight14.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feDistantLight14.NodeType);

            var dochtml1body1svg0feFlood15 = dochtml1body1svg0.ChildNodes[15] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feFlood15.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feFlood15.Attributes.Count);
            Assert.AreEqual("feFlood", dochtml1body1svg0feFlood15.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feFlood15.NodeType);

            var dochtml1body1svg0feFuncA16 = dochtml1body1svg0.ChildNodes[16] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feFuncA16.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feFuncA16.Attributes.Count);
            Assert.AreEqual("feFuncA", dochtml1body1svg0feFuncA16.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feFuncA16.NodeType);

            var dochtml1body1svg0feFuncB17 = dochtml1body1svg0.ChildNodes[17] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feFuncB17.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feFuncB17.Attributes.Count);
            Assert.AreEqual("feFuncB", dochtml1body1svg0feFuncB17.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feFuncB17.NodeType);

            var dochtml1body1svg0feFuncG18 = dochtml1body1svg0.ChildNodes[18] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feFuncG18.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feFuncG18.Attributes.Count);
            Assert.AreEqual("feFuncG", dochtml1body1svg0feFuncG18.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feFuncG18.NodeType);

            var dochtml1body1svg0feFuncR19 = dochtml1body1svg0.ChildNodes[19] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feFuncR19.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feFuncR19.Attributes.Count);
            Assert.AreEqual("feFuncR", dochtml1body1svg0feFuncR19.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feFuncR19.NodeType);

            var dochtml1body1svg0feGaussianBlur20 = dochtml1body1svg0.ChildNodes[20] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feGaussianBlur20.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feGaussianBlur20.Attributes.Count);
            Assert.AreEqual("feGaussianBlur", dochtml1body1svg0feGaussianBlur20.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feGaussianBlur20.NodeType);

            var dochtml1body1svg0feImage21 = dochtml1body1svg0.ChildNodes[21] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feImage21.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feImage21.Attributes.Count);
            Assert.AreEqual("feImage", dochtml1body1svg0feImage21.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feImage21.NodeType);

            var dochtml1body1svg0feMerge22 = dochtml1body1svg0.ChildNodes[22] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feMerge22.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feMerge22.Attributes.Count);
            Assert.AreEqual("feMerge", dochtml1body1svg0feMerge22.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feMerge22.NodeType);

            var dochtml1body1svg0feMergeNode23 = dochtml1body1svg0.ChildNodes[23] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feMergeNode23.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feMergeNode23.Attributes.Count);
            Assert.AreEqual("feMergeNode", dochtml1body1svg0feMergeNode23.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feMergeNode23.NodeType);

            var dochtml1body1svg0feMorphology24 = dochtml1body1svg0.ChildNodes[24] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feMorphology24.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feMorphology24.Attributes.Count);
            Assert.AreEqual("feMorphology", dochtml1body1svg0feMorphology24.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feMorphology24.NodeType);

            var dochtml1body1svg0feOffset25 = dochtml1body1svg0.ChildNodes[25] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feOffset25.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feOffset25.Attributes.Count);
            Assert.AreEqual("feOffset", dochtml1body1svg0feOffset25.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feOffset25.NodeType);

            var dochtml1body1svg0fePointLight26 = dochtml1body1svg0.ChildNodes[26] as Element;
            Assert.AreEqual(0, dochtml1body1svg0fePointLight26.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0fePointLight26.Attributes.Count);
            Assert.AreEqual("fePointLight", dochtml1body1svg0fePointLight26.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0fePointLight26.NodeType);

            var dochtml1body1svg0feSpecularLighting27 = dochtml1body1svg0.ChildNodes[27] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feSpecularLighting27.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feSpecularLighting27.Attributes.Count);
            Assert.AreEqual("feSpecularLighting", dochtml1body1svg0feSpecularLighting27.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feSpecularLighting27.NodeType);

            var dochtml1body1svg0feSpotLight28 = dochtml1body1svg0.ChildNodes[28] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feSpotLight28.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feSpotLight28.Attributes.Count);
            Assert.AreEqual("feSpotLight", dochtml1body1svg0feSpotLight28.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feSpotLight28.NodeType);

            var dochtml1body1svg0feTile29 = dochtml1body1svg0.ChildNodes[29] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feTile29.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feTile29.Attributes.Count);
            Assert.AreEqual("feTile", dochtml1body1svg0feTile29.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feTile29.NodeType);

            var dochtml1body1svg0feTurbulence30 = dochtml1body1svg0.ChildNodes[30] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feTurbulence30.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feTurbulence30.Attributes.Count);
            Assert.AreEqual("feTurbulence", dochtml1body1svg0feTurbulence30.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feTurbulence30.NodeType);

            var dochtml1body1svg0foreignObject31 = dochtml1body1svg0.ChildNodes[31] as Element;
            Assert.AreEqual(0, dochtml1body1svg0foreignObject31.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0foreignObject31.Attributes.Count);
            Assert.AreEqual("foreignObject", dochtml1body1svg0foreignObject31.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0foreignObject31.NodeType);

            var dochtml1body1svg0glyphRef32 = dochtml1body1svg0.ChildNodes[32] as Element;
            Assert.AreEqual(0, dochtml1body1svg0glyphRef32.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0glyphRef32.Attributes.Count);
            Assert.AreEqual("glyphRef", dochtml1body1svg0glyphRef32.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0glyphRef32.NodeType);

            var dochtml1body1svg0linearGradient33 = dochtml1body1svg0.ChildNodes[33] as Element;
            Assert.AreEqual(0, dochtml1body1svg0linearGradient33.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0linearGradient33.Attributes.Count);
            Assert.AreEqual("linearGradient", dochtml1body1svg0linearGradient33.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0linearGradient33.NodeType);

            var dochtml1body1svg0radialGradient34 = dochtml1body1svg0.ChildNodes[34] as Element;
            Assert.AreEqual(0, dochtml1body1svg0radialGradient34.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0radialGradient34.Attributes.Count);
            Assert.AreEqual("radialGradient", dochtml1body1svg0radialGradient34.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0radialGradient34.NodeType);

            var dochtml1body1svg0textPath35 = dochtml1body1svg0.ChildNodes[35] as Element;
            Assert.AreEqual(0, dochtml1body1svg0textPath35.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0textPath35.Attributes.Count);
            Assert.AreEqual("textPath", dochtml1body1svg0textPath35.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0textPath35.NodeType);
        }

        [Test]
        public void SvgSingleNodeInBody()
        {
            var doc = Html(@"<!DOCTYPE html><body><svg><solidColor /></svg>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0.Attributes.Count);
            Assert.AreEqual("svg", dochtml1body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);

            var dochtml1body1svg0solidcolor0 = dochtml1body1svg0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1svg0solidcolor0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0solidcolor0.Attributes.Count);
            Assert.AreEqual("solidcolor", dochtml1body1svg0solidcolor0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0solidcolor0.NodeType);
        }

        [Test]
        public void SvgSingleElement()
        {
            var doc = Html(@"<!DOCTYPE html><svg></svg>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0.Attributes.Count);
            Assert.AreEqual("svg", dochtml1body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);
        }

        [Test]
        public void SvgSingleElementFollowedByCdata()
        {
            var doc = Html(@"<!DOCTYPE html><svg></svg><![CDATA[a]]>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0.Attributes.Count);
            Assert.AreEqual("svg", dochtml1body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);

            var dochtml1body1Comment1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Comment, dochtml1body1Comment1.NodeType);
            Assert.AreEqual(@"[CDATA[a]]", dochtml1body1Comment1.TextContent);
        }

        [Test]
        public void SvgSingleElementInBody()
        {
            var doc = Html(@"<!DOCTYPE html><body><svg></svg>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0.Attributes.Count);
            Assert.AreEqual("svg", dochtml1body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);
        }

        [Test]
        public void SvgSingleElementInSelect()
        {
            var doc = Html(@"<!DOCTYPE html><body><select><svg></svg></select>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1select0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1select0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1select0.Attributes.Count);
            Assert.AreEqual("select", dochtml1body1select0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1select0.NodeType);
        }

        [Test]
        public void SvgSingleElementInOptionInSelect()
        {
            var doc = Html(@"<!DOCTYPE html><body><select><option><svg></svg></option></select>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1select0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1select0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1select0.Attributes.Count);
            Assert.AreEqual("select", dochtml1body1select0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1select0.NodeType);

            var dochtml1body1select0option0 = dochtml1body1select0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1select0option0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1select0option0.Attributes.Count);
            Assert.AreEqual("option", dochtml1body1select0option0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1select0option0.NodeType);
        }

        [Test]
        public void SvgSingleElementInTable()
        {
            var doc = Html(@"<!DOCTYPE html><body><table><svg></svg></table>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0.Attributes.Count);
            Assert.AreEqual("svg", dochtml1body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);

            var dochtml1body1table1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table1.Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table1.NodeType);
        }

        [Test]
        public void SvgElementWithGroupInTable()
        {
            var doc = Html(@"<!DOCTYPE html><body><table><svg><g>foo</g></svg></table>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0.Attributes.Count);
            Assert.AreEqual("svg", dochtml1body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);

            var dochtml1body1svg0g0 = dochtml1body1svg0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1svg0g0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0g0.Attributes.Count);
            Assert.AreEqual("g", dochtml1body1svg0g0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0g0.NodeType);

            var dochtml1body1svg0g0Text0 = dochtml1body1svg0g0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1svg0g0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1svg0g0Text0.TextContent);

            var dochtml1body1table1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table1.Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table1.NodeType);
        }

        [Test]
        public void SvgElementWithGroupAndTextInTable()
        {
            var doc = Html(@"<!DOCTYPE html><body><table><svg><g>foo</g><g>bar</g></svg></table>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0.Attributes.Count);
            Assert.AreEqual("svg", dochtml1body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);

            var dochtml1body1svg0g0 = dochtml1body1svg0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1svg0g0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0g0.Attributes.Count);
            Assert.AreEqual("g", dochtml1body1svg0g0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0g0.NodeType);

            var dochtml1body1svg0g0Text0 = dochtml1body1svg0g0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1svg0g0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1svg0g0Text0.TextContent);

            var dochtml1body1svg0g1 = dochtml1body1svg0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1svg0g1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0g1.Attributes.Count);
            Assert.AreEqual("g", dochtml1body1svg0g1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0g1.NodeType);

            var dochtml1body1svg0g1Text0 = dochtml1body1svg0g1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1svg0g1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1svg0g1Text0.TextContent);

            var dochtml1body1table1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table1.Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table1.NodeType);

        }

        [Test]
        public void SvgElementWithGroupInTbody()
        {
            var doc = Html(@"<!DOCTYPE html><body><table><tbody><svg><g>foo</g><g>bar</g></svg></tbody></table>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0.Attributes.Count);
            Assert.AreEqual("svg", dochtml1body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);

            var dochtml1body1svg0g0 = dochtml1body1svg0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1svg0g0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0g0.Attributes.Count);
            Assert.AreEqual("g", dochtml1body1svg0g0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0g0.NodeType);

            var dochtml1body1svg0g0Text0 = dochtml1body1svg0g0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1svg0g0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1svg0g0Text0.TextContent);

            var dochtml1body1svg0g1 = dochtml1body1svg0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1svg0g1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0g1.Attributes.Count);
            Assert.AreEqual("g", dochtml1body1svg0g1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0g1.NodeType);

            var dochtml1body1svg0g1Text0 = dochtml1body1svg0g1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1svg0g1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1svg0g1Text0.TextContent);

            var dochtml1body1table1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table1.Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table1.NodeType);

            var dochtml1body1table1tbody0 = dochtml1body1table1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1table1tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table1tbody0.Attributes.Count);
            Assert.AreEqual("tbody", dochtml1body1table1tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table1tbody0.NodeType);
        }

        [Test]
        public void SvgElementWithGroupAndTextInTbody()
        {
            var doc = Html(@"<!DOCTYPE html><body><table><tbody><tr><svg><g>foo</g><g>bar</g></svg></tr></tbody></table>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0.Attributes.Count);
            Assert.AreEqual("svg", dochtml1body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);

            var dochtml1body1svg0g0 = dochtml1body1svg0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1svg0g0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0g0.Attributes.Count);
            Assert.AreEqual("g", dochtml1body1svg0g0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0g0.NodeType);

            var dochtml1body1svg0g0Text0 = dochtml1body1svg0g0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1svg0g0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1svg0g0Text0.TextContent);

            var dochtml1body1svg0g1 = dochtml1body1svg0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1svg0g1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0g1.Attributes.Count);
            Assert.AreEqual("g", dochtml1body1svg0g1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0g1.NodeType);

            var dochtml1body1svg0g1Text0 = dochtml1body1svg0g1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1svg0g1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1svg0g1Text0.TextContent);

            var dochtml1body1table1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table1.Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table1.NodeType);

            var dochtml1body1table1tbody0 = dochtml1body1table1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table1tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table1tbody0.Attributes.Count);
            Assert.AreEqual("tbody", dochtml1body1table1tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table1tbody0.NodeType);

            var dochtml1body1table1tbody0tr0 = dochtml1body1table1tbody0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1table1tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table1tbody0tr0.Attributes.Count);
            Assert.AreEqual("tr", dochtml1body1table1tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table1tbody0tr0.NodeType);
        }

        [Test]
        public void SvgElementWithGroupInTableCell()
        {
            var doc = Html(@"<!DOCTYPE html><body><table><tbody><tr><td><svg><g>foo</g><g>bar</g></svg></td></tr></tbody></table>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1table0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0.Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0.NodeType);

            var dochtml1body1table0tbody0 = dochtml1body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0.Attributes.Count);
            Assert.AreEqual("tbody", dochtml1body1table0tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0.NodeType);

            var dochtml1body1table0tbody0tr0 = dochtml1body1table0tbody0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0.Attributes.Count);
            Assert.AreEqual("tr", dochtml1body1table0tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0.NodeType);

            var dochtml1body1table0tbody0tr0td0 = dochtml1body1table0tbody0tr0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0.Attributes.Count);
            Assert.AreEqual("td", dochtml1body1table0tbody0tr0td0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0.NodeType);

            var dochtml1body1table0tbody0tr0td0svg0 = dochtml1body1table0tbody0tr0td0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1table0tbody0tr0td0svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0svg0.Attributes.Count);
            Assert.AreEqual("svg", dochtml1body1table0tbody0tr0td0svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0svg0.NodeType);

            var dochtml1body1table0tbody0tr0td0svg0g0 = dochtml1body1table0tbody0tr0td0svg0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0svg0g0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0svg0g0.Attributes.Count);
            Assert.AreEqual("g", dochtml1body1table0tbody0tr0td0svg0g0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0svg0g0.NodeType);

            var dochtml1body1table0tbody0tr0td0svg0g0Text0 = dochtml1body1table0tbody0tr0td0svg0g0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0tbody0tr0td0svg0g0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1table0tbody0tr0td0svg0g0Text0.TextContent);

            var dochtml1body1table0tbody0tr0td0svg0g1 = dochtml1body1table0tbody0tr0td0svg0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0svg0g1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0svg0g1.Attributes.Count);
            Assert.AreEqual("g", dochtml1body1table0tbody0tr0td0svg0g1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0svg0g1.NodeType);

            var dochtml1body1table0tbody0tr0td0svg0g1Text0 = dochtml1body1table0tbody0tr0td0svg0g1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0tbody0tr0td0svg0g1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1table0tbody0tr0td0svg0g1Text0.TextContent);
        }

        [Test]
        public void SvgElementWithGroupAndTextInTableCell()
        {
            var doc = Html(@"<!DOCTYPE html><body><table><tbody><tr><td><svg><g>foo</g><g>bar</g></svg><p>baz</td></tr></tbody></table>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1table0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0.Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0.NodeType);

            var dochtml1body1table0tbody0 = dochtml1body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0.Attributes.Count);
            Assert.AreEqual("tbody", dochtml1body1table0tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0.NodeType);

            var dochtml1body1table0tbody0tr0 = dochtml1body1table0tbody0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0.Attributes.Count);
            Assert.AreEqual("tr", dochtml1body1table0tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0.NodeType);

            var dochtml1body1table0tbody0tr0td0 = dochtml1body1table0tbody0tr0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1table0tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0.Attributes.Count);
            Assert.AreEqual("td", dochtml1body1table0tbody0tr0td0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0.NodeType);

            var dochtml1body1table0tbody0tr0td0svg0 = dochtml1body1table0tbody0tr0td0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1table0tbody0tr0td0svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0svg0.Attributes.Count);
            Assert.AreEqual("svg", dochtml1body1table0tbody0tr0td0svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0svg0.NodeType);

            var dochtml1body1table0tbody0tr0td0svg0g0 = dochtml1body1table0tbody0tr0td0svg0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0svg0g0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0svg0g0.Attributes.Count);
            Assert.AreEqual("g", dochtml1body1table0tbody0tr0td0svg0g0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0svg0g0.NodeType);

            var dochtml1body1table0tbody0tr0td0svg0g0Text0 = dochtml1body1table0tbody0tr0td0svg0g0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0tbody0tr0td0svg0g0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1table0tbody0tr0td0svg0g0Text0.TextContent);

            var dochtml1body1table0tbody0tr0td0svg0g1 = dochtml1body1table0tbody0tr0td0svg0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0svg0g1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0svg0g1.Attributes.Count);
            Assert.AreEqual("g", dochtml1body1table0tbody0tr0td0svg0g1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0svg0g1.NodeType);

            var dochtml1body1table0tbody0tr0td0svg0g1Text0 = dochtml1body1table0tbody0tr0td0svg0g1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0tbody0tr0td0svg0g1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1table0tbody0tr0td0svg0g1Text0.TextContent);

            var dochtml1body1table0tbody0tr0td0p1 = dochtml1body1table0tbody0tr0td0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0p1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0p1.Attributes.Count);
            Assert.AreEqual("p", dochtml1body1table0tbody0tr0td0p1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0p1.NodeType);

            var dochtml1body1table0tbody0tr0td0p1Text0 = dochtml1body1table0tbody0tr0td0p1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0tbody0tr0td0p1Text0.NodeType);
            Assert.AreEqual("baz", dochtml1body1table0tbody0tr0td0p1Text0.TextContent);
        }

        [Test]
        public void SvgElementWithGroupAndTextInTableCaption()
        {
            var doc = Html(@"<!DOCTYPE html><body><table><caption><svg><g>foo</g><g>bar</g></svg><p>baz</caption></table>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1table0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0.Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0.NodeType);

            var dochtml1body1table0caption0 = dochtml1body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1table0caption0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0.Attributes.Count);
            Assert.AreEqual("caption", dochtml1body1table0caption0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0.NodeType);

            var dochtml1body1table0caption0svg0 = dochtml1body1table0caption0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1table0caption0svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0svg0.Attributes.Count);
            Assert.AreEqual("svg", dochtml1body1table0caption0svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0svg0.NodeType);

            var dochtml1body1table0caption0svg0g0 = dochtml1body1table0caption0svg0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0caption0svg0g0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0svg0g0.Attributes.Count);
            Assert.AreEqual("g", dochtml1body1table0caption0svg0g0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0svg0g0.NodeType);

            var dochtml1body1table0caption0svg0g0Text0 = dochtml1body1table0caption0svg0g0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0caption0svg0g0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1table0caption0svg0g0Text0.TextContent);

            var dochtml1body1table0caption0svg0g1 = dochtml1body1table0caption0svg0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1table0caption0svg0g1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0svg0g1.Attributes.Count);
            Assert.AreEqual("g", dochtml1body1table0caption0svg0g1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0svg0g1.NodeType);

            var dochtml1body1table0caption0svg0g1Text0 = dochtml1body1table0caption0svg0g1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0caption0svg0g1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1table0caption0svg0g1Text0.TextContent);

            var dochtml1body1table0caption0p1 = dochtml1body1table0caption0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1table0caption0p1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0p1.Attributes.Count);
            Assert.AreEqual("p", dochtml1body1table0caption0p1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0p1.NodeType);

            var dochtml1body1table0caption0p1Text0 = dochtml1body1table0caption0p1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0caption0p1Text0.NodeType);
            Assert.AreEqual("baz", dochtml1body1table0caption0p1Text0.TextContent);
        }

        [Test]
        public void SvgElementWithGroupInTableCaption()
        {
            var doc = Html(@"<!DOCTYPE html><body><table><caption><svg><g>foo</g><g>bar</g><p>baz</table><p>quux");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1table0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0.Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0.NodeType);

            var dochtml1body1table0caption0 = dochtml1body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1table0caption0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0.Attributes.Count);
            Assert.AreEqual("caption", dochtml1body1table0caption0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0.NodeType);

            var dochtml1body1table0caption0svg0 = dochtml1body1table0caption0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1table0caption0svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0svg0.Attributes.Count);
            Assert.AreEqual("svg", dochtml1body1table0caption0svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0svg0.NodeType);

            var dochtml1body1table0caption0svg0g0 = dochtml1body1table0caption0svg0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0caption0svg0g0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0svg0g0.Attributes.Count);
            Assert.AreEqual("g", dochtml1body1table0caption0svg0g0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0svg0g0.NodeType);

            var dochtml1body1table0caption0svg0g0Text0 = dochtml1body1table0caption0svg0g0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0caption0svg0g0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1table0caption0svg0g0Text0.TextContent);

            var dochtml1body1table0caption0svg0g1 = dochtml1body1table0caption0svg0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1table0caption0svg0g1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0svg0g1.Attributes.Count);
            Assert.AreEqual("g", dochtml1body1table0caption0svg0g1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0svg0g1.NodeType);

            var dochtml1body1table0caption0svg0g1Text0 = dochtml1body1table0caption0svg0g1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0caption0svg0g1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1table0caption0svg0g1Text0.TextContent);

            var dochtml1body1table0caption0p1 = dochtml1body1table0caption0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1table0caption0p1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0p1.Attributes.Count);
            Assert.AreEqual("p", dochtml1body1table0caption0p1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0p1.NodeType);

            var dochtml1body1table0caption0p1Text0 = dochtml1body1table0caption0p1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0caption0p1Text0.NodeType);
            Assert.AreEqual("baz", dochtml1body1table0caption0p1Text0.TextContent);

            var dochtml1body1p1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1p1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1p1.Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p1.NodeType);

            var dochtml1body1p1Text0 = dochtml1body1p1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1p1Text0.NodeType);
            Assert.AreEqual("quux", dochtml1body1p1Text0.TextContent);
        }

        [Test]
        public void SvgElementInCaptionWithMisclosedEnding()
        {
            var doc = Html(@"<!DOCTYPE html><body><table><caption><svg><g>foo</g><g>bar</g>baz</table><p>quux");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1table0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0.Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0.NodeType);

            var dochtml1body1table0caption0 = dochtml1body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0caption0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0.Attributes.Count);
            Assert.AreEqual("caption", dochtml1body1table0caption0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0.NodeType);

            var dochtml1body1table0caption0svg0 = dochtml1body1table0caption0.ChildNodes[0] as Element;
            Assert.AreEqual(3, dochtml1body1table0caption0svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0svg0.Attributes.Count);
            Assert.AreEqual("svg", dochtml1body1table0caption0svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0svg0.NodeType);

            var dochtml1body1table0caption0svg0g0 = dochtml1body1table0caption0svg0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0caption0svg0g0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0svg0g0.Attributes.Count);
            Assert.AreEqual("g", dochtml1body1table0caption0svg0g0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0svg0g0.NodeType);

            var dochtml1body1table0caption0svg0g0Text0 = dochtml1body1table0caption0svg0g0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0caption0svg0g0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1table0caption0svg0g0Text0.TextContent);

            var dochtml1body1table0caption0svg0g1 = dochtml1body1table0caption0svg0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1table0caption0svg0g1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0svg0g1.Attributes.Count);
            Assert.AreEqual("g", dochtml1body1table0caption0svg0g1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0svg0g1.NodeType);

            var dochtml1body1table0caption0svg0g1Text0 = dochtml1body1table0caption0svg0g1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0caption0svg0g1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1table0caption0svg0g1Text0.TextContent);

            var dochtml1body1table0caption0svg0Text2 = dochtml1body1table0caption0svg0.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0caption0svg0Text2.NodeType);
            Assert.AreEqual("baz", dochtml1body1table0caption0svg0Text2.TextContent);

            var dochtml1body1p1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1p1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1p1.Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p1.NodeType);

            var dochtml1body1p1Text0 = dochtml1body1p1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1p1Text0.NodeType);
            Assert.AreEqual("quux", dochtml1body1p1Text0.TextContent);
        }

        [Test]
        public void SvgElementInColgroupWithMisclosedEnding()
        {
            var doc = Html(@"<!DOCTYPE html><body><table><colgroup><svg><g>foo</g><g>bar</g><p>baz</table><p>quux");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(4, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0.Attributes.Count);
            Assert.AreEqual("svg", dochtml1body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);

            var dochtml1body1svg0g0 = dochtml1body1svg0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1svg0g0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0g0.Attributes.Count);
            Assert.AreEqual("g", dochtml1body1svg0g0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0g0.NodeType);

            var dochtml1body1svg0g0Text0 = dochtml1body1svg0g0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1svg0g0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1svg0g0Text0.TextContent);

            var dochtml1body1svg0g1 = dochtml1body1svg0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1svg0g1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0g1.Attributes.Count);
            Assert.AreEqual("g", dochtml1body1svg0g1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0g1.NodeType);

            var dochtml1body1svg0g1Text0 = dochtml1body1svg0g1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1svg0g1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1svg0g1Text0.TextContent);

            var dochtml1body1p1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1p1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1p1.Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p1.NodeType);

            var dochtml1body1p1Text0 = dochtml1body1p1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1p1Text0.NodeType);
            Assert.AreEqual("baz", dochtml1body1p1Text0.TextContent);

            var dochtml1body1table2 = dochtml1body1.ChildNodes[2] as Element;
            Assert.AreEqual(1, dochtml1body1table2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table2.Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table2.NodeType);

            var dochtml1body1table2colgroup0 = dochtml1body1table2.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1table2colgroup0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table2colgroup0.Attributes.Count);
            Assert.AreEqual("colgroup", dochtml1body1table2colgroup0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table2colgroup0.NodeType);

            var dochtml1body1p3 = dochtml1body1.ChildNodes[3] as Element;
            Assert.AreEqual(1, dochtml1body1p3.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1p3.Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p3.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p3.NodeType);

            var dochtml1body1p3Text0 = dochtml1body1p3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1p3Text0.NodeType);
            Assert.AreEqual("quux", dochtml1body1p3Text0.TextContent);
        }

        [Test]
        public void SvgElementWithGroupInSelectMisclosed()
        {
            var doc = Html(@"<!DOCTYPE html><body><table><tr><td><select><svg><g>foo</g><g>bar</g><p>baz</table><p>quux");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1table0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0.Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0.NodeType);

            var dochtml1body1table0tbody0 = dochtml1body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0.Attributes.Count);
            Assert.AreEqual("tbody", dochtml1body1table0tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0.NodeType);

            var dochtml1body1table0tbody0tr0 = dochtml1body1table0tbody0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0.Attributes.Count);
            Assert.AreEqual("tr", dochtml1body1table0tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0.NodeType);

            var dochtml1body1table0tbody0tr0td0 = dochtml1body1table0tbody0tr0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0.Attributes.Count);
            Assert.AreEqual("td", dochtml1body1table0tbody0tr0td0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0.NodeType);

            var dochtml1body1table0tbody0tr0td0select0 = dochtml1body1table0tbody0tr0td0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0select0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0select0.Attributes.Count);
            Assert.AreEqual("select", dochtml1body1table0tbody0tr0td0select0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0select0.NodeType);

            var dochtml1body1table0tbody0tr0td0select0Text0 = dochtml1body1table0tbody0tr0td0select0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0tbody0tr0td0select0Text0.NodeType);
            Assert.AreEqual("foobarbaz", dochtml1body1table0tbody0tr0td0select0Text0.TextContent);

            var dochtml1body1p1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1p1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1p1.Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p1.NodeType);

            var dochtml1body1p1Text0 = dochtml1body1p1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1p1Text0.NodeType);
            Assert.AreEqual("quux", dochtml1body1p1Text0.TextContent);
        }

        [Test]
        public void SvgElementWithGroupInSelectAndTableMisclosed()
        {
            var doc = Html(@"<!DOCTYPE html><body><table><select><svg><g>foo</g><g>bar</g><p>baz</table><p>quux");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(3, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1select0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1select0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1select0.Attributes.Count);
            Assert.AreEqual("select", dochtml1body1select0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1select0.NodeType);

            var dochtml1body1select0Text0 = dochtml1body1select0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1select0Text0.NodeType);
            Assert.AreEqual("foobarbaz", dochtml1body1select0Text0.TextContent);

            var dochtml1body1table1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table1.Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table1.NodeType);

            var dochtml1body1p2 = dochtml1body1.ChildNodes[2] as Element;
            Assert.AreEqual(1, dochtml1body1p2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1p2.Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p2.NodeType);

            var dochtml1body1p2Text0 = dochtml1body1p2.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1p2Text0.NodeType);
            Assert.AreEqual("quux", dochtml1body1p2Text0.TextContent);
        }

        [Test]
        public void SvgElementOutsideDocumentRoot()
        {
            var doc = Html(@"<!DOCTYPE html><body></body></html><svg><g>foo</g><g>bar</g><p>baz");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0.Attributes.Count);
            Assert.AreEqual("svg", dochtml1body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);

            var dochtml1body1svg0g0 = dochtml1body1svg0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1svg0g0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0g0.Attributes.Count);
            Assert.AreEqual("g", dochtml1body1svg0g0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0g0.NodeType);

            var dochtml1body1svg0g0Text0 = dochtml1body1svg0g0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1svg0g0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1svg0g0Text0.TextContent);

            var dochtml1body1svg0g1 = dochtml1body1svg0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1svg0g1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0g1.Attributes.Count);
            Assert.AreEqual("g", dochtml1body1svg0g1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0g1.NodeType);

            var dochtml1body1svg0g1Text0 = dochtml1body1svg0g1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1svg0g1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1svg0g1Text0.TextContent);

            var dochtml1body1p1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1p1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1p1.Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p1.NodeType);

            var dochtml1body1p1Text0 = dochtml1body1p1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1p1Text0.NodeType);
            Assert.AreEqual("baz", dochtml1body1p1Text0.TextContent);
        }

        [Test]
        public void SvgElementOutsideBody()
        {
            var doc = Html(@"<!DOCTYPE html><body></body><svg><g>foo</g><g>bar</g><p>baz");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0.Attributes.Count);
            Assert.AreEqual("svg", dochtml1body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);

            var dochtml1body1svg0g0 = dochtml1body1svg0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1svg0g0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0g0.Attributes.Count);
            Assert.AreEqual("g", dochtml1body1svg0g0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0g0.NodeType);

            var dochtml1body1svg0g0Text0 = dochtml1body1svg0g0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1svg0g0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1svg0g0Text0.TextContent);

            var dochtml1body1svg0g1 = dochtml1body1svg0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1svg0g1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0g1.Attributes.Count);
            Assert.AreEqual("g", dochtml1body1svg0g1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0g1.NodeType);

            var dochtml1body1svg0g1Text0 = dochtml1body1svg0g1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1svg0g1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1svg0g1Text0.TextContent);

            var dochtml1body1p1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1p1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1p1.Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p1.NodeType);

            var dochtml1body1p1Text0 = dochtml1body1p1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1p1Text0.NodeType);
            Assert.AreEqual("baz", dochtml1body1p1Text0.TextContent);
        }

        [Test]
        public void SvgElementInFrameset()
        {
            var doc = Html(@"<!DOCTYPE html><frameset><svg><g></g><g></g><p><span>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1frameset1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1frameset1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1frameset1.Attributes.Count);
            Assert.AreEqual("frameset", dochtml1frameset1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1frameset1.NodeType);
        }

        [Test]
        public void SvgElementOutsideFrameset()
        {
            var doc = Html(@"<!DOCTYPE html><frameset></frameset><svg><g></g><g></g><p><span>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1frameset1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1frameset1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1frameset1.Attributes.Count);
            Assert.AreEqual("frameset", dochtml1frameset1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1frameset1.NodeType);
        }

        [Test]
        public void SvgElementInBodyWithXlinkAttribute()
        {
            var doc = Html(@"<!DOCTYPE html><body xlink:href=foo><svg xlink:href=foo></svg>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
            Assert.IsNotNull(dochtml1body1.Attributes.Get("xlink:href"));
            Assert.AreEqual("xlink:href", dochtml1body1.Attributes.Get("xlink:href").Name);
            Assert.AreEqual("foo", dochtml1body1.Attributes.Get("xlink:href").Value);

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml1body1svg0.Attributes.Count);
            Assert.AreEqual("svg", dochtml1body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);

            var attr = dochtml1body1svg0.Attributes.Get("href");
            Assert.IsNotNull(attr);
            Assert.AreEqual("foo", attr.Value);
            Assert.AreEqual(null, attr.Prefix);
            Assert.AreEqual("http://www.w3.org/1999/xlink", attr.NamespaceUri);
        }

        [Test]
        public void SvgElementWithGroupThatHasXlinkAttribute()
        {
            var doc = Html(@"<!DOCTYPE html><body xlink:href=foo xml:lang=en><svg><g xml:lang=en xlink:href=foo></g></svg>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(2, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
            Assert.IsNotNull(dochtml1body1.Attributes.Get("xlink:href"));
            Assert.AreEqual("xlink:href", dochtml1body1.Attributes.Get("xlink:href").Name);
            Assert.AreEqual("foo", dochtml1body1.Attributes.Get("xlink:href").Value);
            Assert.IsNotNull(dochtml1body1.Attributes.Get("xml:lang"));
            Assert.AreEqual("xml:lang", dochtml1body1.Attributes.Get("xml:lang").Name);
            Assert.AreEqual("en", dochtml1body1.Attributes.Get("xml:lang").Value);

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0.Attributes.Count);
            Assert.AreEqual("svg", dochtml1body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);

            var dochtml1body1svg0g0 = dochtml1body1svg0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1svg0g0.ChildNodes.Length);
            Assert.AreEqual(2, dochtml1body1svg0g0.Attributes.Count);
            Assert.AreEqual("g", dochtml1body1svg0g0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0g0.NodeType);

            var attr1 = dochtml1body1svg0g0.Attributes.Get("href");
            Assert.IsNotNull(attr1);
            Assert.AreEqual("foo", attr1.Value);
            Assert.AreEqual(null, attr1.Prefix);
            Assert.AreEqual("http://www.w3.org/1999/xlink", attr1.NamespaceUri);

            var attr2 = dochtml1body1svg0g0.Attributes.Get("xml:lang");
            Assert.IsNotNull(attr2);
            Assert.AreEqual("en", attr2.Value);
            Assert.AreEqual("xml", attr2.Prefix);
            Assert.AreEqual("http://www.w3.org/XML/1998/namespace", attr2.NamespaceUri);
        }

        [Test]
        public void SvgElementWithGroupThatHasNamespacedAttributes()
        {
            var doc = Html(@"<!DOCTYPE html><body xlink:href=foo xml:lang=en><svg><g xml:lang=en xlink:href=foo /></svg>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(2, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
            Assert.IsNotNull(dochtml1body1.Attributes.Get("xlink:href"));
            Assert.AreEqual("xlink:href", dochtml1body1.Attributes.Get("xlink:href").Name);
            Assert.AreEqual("foo", dochtml1body1.Attributes.Get("xlink:href").Value);
            Assert.IsNotNull(dochtml1body1.Attributes.Get("xml:lang"));
            Assert.AreEqual("xml:lang", dochtml1body1.Attributes.Get("xml:lang").Name);
            Assert.AreEqual("en", dochtml1body1.Attributes.Get("xml:lang").Value);

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0.Attributes.Count);
            Assert.AreEqual("svg", dochtml1body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);

            var dochtml1body1svg0g0 = dochtml1body1svg0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1svg0g0.ChildNodes.Length);
            Assert.AreEqual(2, dochtml1body1svg0g0.Attributes.Count);
            Assert.AreEqual("g", dochtml1body1svg0g0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0g0.NodeType);

            var attr1 = dochtml1body1svg0g0.Attributes.Get("href");
            Assert.IsNotNull(attr1);
            Assert.AreEqual("foo", attr1.Value);
            Assert.AreEqual(null, attr1.Prefix);
            Assert.AreEqual("http://www.w3.org/1999/xlink", attr1.NamespaceUri);

            var attr2 = dochtml1body1svg0g0.Attributes.Get("xml:lang");
            Assert.IsNotNull(attr2);
            Assert.AreEqual("en", attr2.Value);
            Assert.AreEqual("xml", attr2.Prefix);
            Assert.AreEqual("http://www.w3.org/XML/1998/namespace", attr2.NamespaceUri);
        }

        [Test]
        public void SvgElementWithSelfClosingGroup()
        {
            var doc = Html(@"<!DOCTYPE html><body xlink:href=foo xml:lang=en><svg><g xml:lang=en xlink:href=foo />bar</svg>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(2, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
            Assert.IsNotNull(dochtml1body1.Attributes.Get("xlink:href"));
            Assert.AreEqual("xlink:href", dochtml1body1.Attributes.Get("xlink:href").Name);
            Assert.AreEqual("foo", dochtml1body1.Attributes.Get("xlink:href").Value);
            Assert.IsNotNull(dochtml1body1.Attributes.Get("xml:lang"));
            Assert.AreEqual("xml:lang", dochtml1body1.Attributes.Get("xml:lang").Name);
            Assert.AreEqual("en", dochtml1body1.Attributes.Get("xml:lang").Value);

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0.Attributes.Count);
            Assert.AreEqual("svg", dochtml1body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);

            var dochtml1body1svg0g0 = dochtml1body1svg0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1svg0g0.ChildNodes.Length);
            Assert.AreEqual(2, dochtml1body1svg0g0.Attributes.Count);
            Assert.AreEqual("g", dochtml1body1svg0g0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0g0.NodeType);

            var attr1 = dochtml1body1svg0g0.Attributes.Get("href");
            Assert.IsNotNull(attr1);
            Assert.AreEqual("foo", attr1.Value);
            Assert.AreEqual(null, attr1.Prefix);
            Assert.AreEqual("http://www.w3.org/1999/xlink", attr1.NamespaceUri);

            var attr2 = dochtml1body1svg0g0.Attributes.Get("xml:lang");
            Assert.IsNotNull(attr2);
            Assert.AreEqual("en", attr2.Value);
            Assert.AreEqual("xml", attr2.Prefix);
            Assert.AreEqual("http://www.w3.org/XML/1998/namespace", attr2.NamespaceUri);

            var dochtml1body1svg0Text1 = dochtml1body1svg0.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml1body1svg0Text1.NodeType);
            Assert.AreEqual("bar", dochtml1body1svg0Text1.TextContent);
        }

        [Test]
        public void SvgElementWithMisclosedPath()
        {
            var doc = Html(@"<svg></path>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1svg0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1svg0.Attributes.Count);
            Assert.AreEqual("svg", dochtml0body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0.NodeType);
        }

        [Test]
        public void SvgElementInDivMisclosed()
        {
            var doc = Html(@"<div><svg></div>a");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1div0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);

            var dochtml0body1div0svg0 = dochtml0body1div0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1div0svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0svg0.Attributes.Count);
            Assert.AreEqual("svg", dochtml0body1div0svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0svg0.NodeType);


            var dochtml0body1Text1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text1.NodeType);
            Assert.AreEqual("a", dochtml0body1Text1.TextContent);
        }

        [Test]
        public void SvgElementWithPathInDivMisclosed()
        {
            var doc = Html(@"<div><svg><path></div>a");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1div0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);

            var dochtml0body1div0svg0 = dochtml0body1div0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1div0svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0svg0.Attributes.Count);
            Assert.AreEqual("svg", dochtml0body1div0svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0svg0.NodeType);

            var dochtml0body1div0svg0path0 = dochtml0body1div0svg0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1div0svg0path0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0svg0path0.Attributes.Count);
            Assert.AreEqual("path", dochtml0body1div0svg0path0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0svg0path0.NodeType);

            var dochtml0body1Text1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text1.NodeType);
            Assert.AreEqual("a", dochtml0body1Text1.TextContent);
        }

        [Test]
        public void SvgElementWithMisclosedPathInDiv()
        {
            var doc = Html(@"<div><svg><path></svg><path>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1div0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);

            var dochtml0body1div0svg0 = dochtml0body1div0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1div0svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0svg0.Attributes.Count);
            Assert.AreEqual("svg", dochtml0body1div0svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0svg0.NodeType);

            var dochtml0body1div0svg0path0 = dochtml0body1div0svg0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1div0svg0path0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0svg0path0.Attributes.Count);
            Assert.AreEqual("path", dochtml0body1div0svg0path0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0svg0path0.NodeType);

            var dochtml0body1div0path1 = dochtml0body1div0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1div0path1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0path1.Attributes.Count);
            Assert.AreEqual("path", dochtml0body1div0path1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0path1.NodeType);
        }

        [Test]
        public void SvgElementWithPathAndMathInDiv()
        {
            var doc = Html(@"<div><svg><path><foreignObject><math></div>a");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1div0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);

            var dochtml0body1div0svg0 = dochtml0body1div0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1div0svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0svg0.Attributes.Count);
            Assert.AreEqual("svg", dochtml0body1div0svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0svg0.NodeType);

            var dochtml0body1div0svg0path0 = dochtml0body1div0svg0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1div0svg0path0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0svg0path0.Attributes.Count);
            Assert.AreEqual("path", dochtml0body1div0svg0path0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0svg0path0.NodeType);

            var dochtml0body1div0svg0path0foreignObject0 = dochtml0body1div0svg0path0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1div0svg0path0foreignObject0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0svg0path0foreignObject0.Attributes.Count);
            Assert.AreEqual("foreignObject", dochtml0body1div0svg0path0foreignObject0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0svg0path0foreignObject0.NodeType);

            var dochtml0body1div0svg0path0foreignObject0math0 = dochtml0body1div0svg0path0foreignObject0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1div0svg0path0foreignObject0math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0svg0path0foreignObject0math0.Attributes.Count);
            Assert.AreEqual("math", dochtml0body1div0svg0path0foreignObject0math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0svg0path0foreignObject0math0.NodeType);

            var dochtml0body1div0svg0path0foreignObject0math0Text0 = dochtml0body1div0svg0path0foreignObject0math0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0svg0path0foreignObject0math0Text0.NodeType);
            Assert.AreEqual("a", dochtml0body1div0svg0path0foreignObject0math0Text0.TextContent);
        }

        [Test]
        public void SvgElementWithPathAndForeignObjectInDiv()
        {
            var doc = Html(@"<div><svg><path><foreignObject><p></div>a");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1div0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);

            var dochtml0body1div0svg0 = dochtml0body1div0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1div0svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0svg0.Attributes.Count);
            Assert.AreEqual("svg", dochtml0body1div0svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0svg0.NodeType);

            var dochtml0body1div0svg0path0 = dochtml0body1div0svg0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1div0svg0path0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0svg0path0.Attributes.Count);
            Assert.AreEqual("path", dochtml0body1div0svg0path0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0svg0path0.NodeType);

            var dochtml0body1div0svg0path0foreignObject0 = dochtml0body1div0svg0path0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1div0svg0path0foreignObject0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0svg0path0foreignObject0.Attributes.Count);
            Assert.AreEqual("foreignObject", dochtml0body1div0svg0path0foreignObject0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0svg0path0foreignObject0.NodeType);

            var dochtml0body1div0svg0path0foreignObject0p0 = dochtml0body1div0svg0path0foreignObject0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1div0svg0path0foreignObject0p0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0svg0path0foreignObject0p0.Attributes.Count);
            Assert.AreEqual("p", dochtml0body1div0svg0path0foreignObject0p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0svg0path0foreignObject0p0.NodeType);

            var dochtml0body1div0svg0path0foreignObject0p0Text0 = dochtml0body1div0svg0path0foreignObject0p0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0svg0path0foreignObject0p0Text0.NodeType);
            Assert.AreEqual("a", dochtml0body1div0svg0path0foreignObject0p0Text0.TextContent);
        }

        [Test]
        public void SvgElementWithDescDivAndAnotherSvg()
        {
            var doc = Html(@"<!DOCTYPE html><svg><desc><div><svg><ul>a");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0.Attributes.Count);
            Assert.AreEqual("svg", dochtml1body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);

            var dochtml1body1svg0desc0 = dochtml1body1svg0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1svg0desc0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0desc0.Attributes.Count);
            Assert.AreEqual("desc", dochtml1body1svg0desc0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0desc0.NodeType);

            var dochtml1body1svg0desc0div0 = dochtml1body1svg0desc0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1svg0desc0div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0desc0div0.Attributes.Count);
            Assert.AreEqual("div", dochtml1body1svg0desc0div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0desc0div0.NodeType);

            var dochtml1body1svg0desc0div0svg0 = dochtml1body1svg0desc0div0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1svg0desc0div0svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0desc0div0svg0.Attributes.Count);
            Assert.AreEqual("svg", dochtml1body1svg0desc0div0svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0desc0div0svg0.NodeType);

            var dochtml1body1svg0desc0div0ul1 = dochtml1body1svg0desc0div0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1svg0desc0div0ul1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0desc0div0ul1.Attributes.Count);
            Assert.AreEqual("ul", dochtml1body1svg0desc0div0ul1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0desc0div0ul1.NodeType);

            var dochtml1body1svg0desc0div0ul1Text0 = dochtml1body1svg0desc0div0ul1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1svg0desc0div0ul1Text0.NodeType);
            Assert.AreEqual("a", dochtml1body1svg0desc0div0ul1Text0.TextContent);
        }

        [Test]
        public void SvgElementWithDescAndAnotherSvgElement()
        {
            var doc = Html(@"<!DOCTYPE html><svg><desc><svg><ul>a");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0.Attributes.Count);
            Assert.AreEqual("svg", dochtml1body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);

            var dochtml1body1svg0desc0 = dochtml1body1svg0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1svg0desc0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0desc0.Attributes.Count);
            Assert.AreEqual("desc", dochtml1body1svg0desc0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0desc0.NodeType);

            var dochtml1body1svg0desc0svg0 = dochtml1body1svg0desc0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1svg0desc0svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0desc0svg0.Attributes.Count);
            Assert.AreEqual("svg", dochtml1body1svg0desc0svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0desc0svg0.NodeType);

            var dochtml1body1svg0desc0ul1 = dochtml1body1svg0desc0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1svg0desc0ul1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0desc0ul1.Attributes.Count);
            Assert.AreEqual("ul", dochtml1body1svg0desc0ul1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0desc0ul1.NodeType);

            var dochtml1body1svg0desc0ul1Text0 = dochtml1body1svg0desc0ul1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1svg0desc0ul1Text0.NodeType);
            Assert.AreEqual("a", dochtml1body1svg0desc0ul1Text0.TextContent);
        }

        [Test]
        public void SvgElementInParagraph()
        {
            var doc = Html(@"<!DOCTYPE html><p><svg><desc><p>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1p0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1p0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1p0.Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0.NodeType);

            var dochtml1body1p0svg0 = dochtml1body1p0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1p0svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1p0svg0.Attributes.Count);
            Assert.AreEqual("svg", dochtml1body1p0svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0svg0.NodeType);

            var dochtml1body1p0svg0desc0 = dochtml1body1p0svg0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1p0svg0desc0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1p0svg0desc0.Attributes.Count);
            Assert.AreEqual("desc", dochtml1body1p0svg0desc0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0svg0desc0.NodeType);

            var dochtml1body1p0svg0desc0p0 = dochtml1body1p0svg0desc0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1p0svg0desc0p0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1p0svg0desc0p0.Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p0svg0desc0p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0svg0desc0p0.NodeType);
        }

        [Test]
        public void SvgElementWithTitleInSvgNamespace()
        {
            var doc = Html(@"<!DOCTYPE html><p><svg><title><p>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1p0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1p0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1p0.Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0.NodeType);

            var dochtml1body1p0svg0 = dochtml1body1p0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1p0svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1p0svg0.Attributes.Count);
            Assert.AreEqual("svg", dochtml1body1p0svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0svg0.NodeType);

            var dochtml1body1p0svg0title0 = dochtml1body1p0svg0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1p0svg0title0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1p0svg0title0.Attributes.Count);
            Assert.AreEqual("title", dochtml1body1p0svg0title0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0svg0title0.NodeType);

            var dochtml1body1p0svg0title0p0 = dochtml1body1p0svg0title0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1p0svg0title0p0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1p0svg0title0p0.Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p0svg0title0p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0svg0title0p0.NodeType);
        }

        [Test]
        public void SvgElementInDivWithForeignObject()
        {
            var doc = Html(@"<div><svg><path><foreignObject><p></foreignObject><p>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1div0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);

            var dochtml0body1div0svg0 = dochtml0body1div0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1div0svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0svg0.Attributes.Count);
            Assert.AreEqual("svg", dochtml0body1div0svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0svg0.NodeType);

            var dochtml0body1div0svg0path0 = dochtml0body1div0svg0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1div0svg0path0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0svg0path0.Attributes.Count);
            Assert.AreEqual("path", dochtml0body1div0svg0path0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0svg0path0.NodeType);

            var dochtml0body1div0svg0path0foreignObject0 = dochtml0body1div0svg0path0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0body1div0svg0path0foreignObject0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0svg0path0foreignObject0.Attributes.Count);
            Assert.AreEqual("foreignObject", dochtml0body1div0svg0path0foreignObject0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0svg0path0foreignObject0.NodeType);

            var dochtml0body1div0svg0path0foreignObject0p0 = dochtml0body1div0svg0path0foreignObject0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1div0svg0path0foreignObject0p0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0svg0path0foreignObject0p0.Attributes.Count);
            Assert.AreEqual("p", dochtml0body1div0svg0path0foreignObject0p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0svg0path0foreignObject0p0.NodeType);

            var dochtml0body1div0svg0path0foreignObject0p1 = dochtml0body1div0svg0path0foreignObject0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1div0svg0path0foreignObject0p1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0svg0path0foreignObject0p1.Attributes.Count);
            Assert.AreEqual("p", dochtml0body1div0svg0path0foreignObject0p1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0svg0path0foreignObject0p1.NodeType);
        }

        [Test]
        public void SvgWithScriptAndPathElement()
        {
            var doc = Html(@"<svg><script></script><path>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1svg0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1svg0.Attributes.Count);
            Assert.AreEqual("svg", dochtml0body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0.NodeType);

            var dochtml0body1svg0script0 = dochtml0body1svg0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1svg0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1svg0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0body1svg0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0script0.NodeType);

            var dochtml0body1svg0path1 = dochtml0body1svg0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1svg0path1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1svg0path1.Attributes.Count);
            Assert.AreEqual("path", dochtml0body1svg0path1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0path1.NodeType);
        }

        [Test]
        public void SvgInsideTableWithRow()
        {
            var doc = Html(@"<table><svg></svg><tr>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1svg0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1svg0.Attributes.Count);
            Assert.AreEqual("svg", dochtml0body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0.NodeType);

            var dochtml0body1table1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table1.Attributes.Count);
            Assert.AreEqual("table", dochtml0body1table1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table1.NodeType);

            var dochtml0body1table1tbody0 = dochtml0body1table1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table1tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table1tbody0.Attributes.Count);
            Assert.AreEqual("tbody", dochtml0body1table1tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table1tbody0.NodeType);

            var dochtml0body1table1tbody0tr0 = dochtml0body1table1tbody0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table1tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table1tbody0tr0.Attributes.Count);
            Assert.AreEqual("tr", dochtml0body1table1tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table1tbody0tr0.NodeType);
        }

        [Test]
        public void SvgInsideMathMLWithAnnotationXml()
        {
            var doc = Html(@"<math><annotation-xml><svg></svg></annotation-xml><mi>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1math0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml0body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0.NodeType);

            var dochtml0body1math0annotationxml0 = dochtml0body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0annotationxml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0annotationxml0.Attributes.Count);
            Assert.AreEqual("annotation-xml", dochtml0body1math0annotationxml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0annotationxml0.NodeType);

            var dochtml0body1math0annotationxml0svg0 = dochtml0body1math0annotationxml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1math0annotationxml0svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0annotationxml0svg0.Attributes.Count);
            Assert.AreEqual("svg", dochtml0body1math0annotationxml0svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0annotationxml0svg0.NodeType);

            var dochtml0body1math0mi1 = dochtml0body1math0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1math0mi1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mi1.Attributes.Count);
            Assert.AreEqual("mi", dochtml0body1math0mi1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mi1.NodeType);
        }

        [Test]
        public void SvgInsideMathMLWithAnnotationXmlAndForeignObject()
        {
            var doc = Html(@"<math><annotation-xml><svg><foreignObject><div><math><mi></mi></math><span></span></div></foreignObject><path></path></svg></annotation-xml><mi>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1math0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml0body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0.NodeType);

            var dochtml0body1math0annotationxml0 = dochtml0body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0annotationxml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0annotationxml0.Attributes.Count);
            Assert.AreEqual("annotation-xml", dochtml0body1math0annotationxml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0annotationxml0.NodeType);

            var dochtml0body1math0annotationxml0svg0 = dochtml0body1math0annotationxml0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0body1math0annotationxml0svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0annotationxml0svg0.Attributes.Count);
            Assert.AreEqual("svg", dochtml0body1math0annotationxml0svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0annotationxml0svg0.NodeType);

            var dochtml0body1math0annotationxml0svg0foreignObject0 = dochtml0body1math0annotationxml0svg0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0annotationxml0svg0foreignObject0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0annotationxml0svg0foreignObject0.Attributes.Count);
            Assert.AreEqual("foreignObject", dochtml0body1math0annotationxml0svg0foreignObject0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0annotationxml0svg0foreignObject0.NodeType);

            var dochtml0body1math0annotationxml0svg0foreignObject0div0 = dochtml0body1math0annotationxml0svg0foreignObject0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0body1math0annotationxml0svg0foreignObject0div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0annotationxml0svg0foreignObject0div0.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1math0annotationxml0svg0foreignObject0div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0annotationxml0svg0foreignObject0div0.NodeType);

            var dochtml0body1math0annotationxml0svg0foreignObject0div0math0 = dochtml0body1math0annotationxml0svg0foreignObject0div0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0annotationxml0svg0foreignObject0div0math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0annotationxml0svg0foreignObject0div0math0.Attributes.Count);
            Assert.AreEqual("math", dochtml0body1math0annotationxml0svg0foreignObject0div0math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0annotationxml0svg0foreignObject0div0math0.NodeType);

            var dochtml0body1math0annotationxml0svg0foreignObject0div0math0mi0 = dochtml0body1math0annotationxml0svg0foreignObject0div0math0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1math0annotationxml0svg0foreignObject0div0math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0annotationxml0svg0foreignObject0div0math0mi0.Attributes.Count);
            Assert.AreEqual("mi", dochtml0body1math0annotationxml0svg0foreignObject0div0math0mi0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0annotationxml0svg0foreignObject0div0math0mi0.NodeType);

            var dochtml0body1math0annotationxml0svg0foreignObject0div0span1 = dochtml0body1math0annotationxml0svg0foreignObject0div0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1math0annotationxml0svg0foreignObject0div0span1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0annotationxml0svg0foreignObject0div0span1.Attributes.Count);
            Assert.AreEqual("span", dochtml0body1math0annotationxml0svg0foreignObject0div0span1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0annotationxml0svg0foreignObject0div0span1.NodeType);

            var dochtml0body1math0annotationxml0svg0path1 = dochtml0body1math0annotationxml0svg0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1math0annotationxml0svg0path1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0annotationxml0svg0path1.Attributes.Count);
            Assert.AreEqual("path", dochtml0body1math0annotationxml0svg0path1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0annotationxml0svg0path1.NodeType);

            var dochtml0body1math0mi1 = dochtml0body1math0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1math0mi1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mi1.Attributes.Count);
            Assert.AreEqual("mi", dochtml0body1math0mi1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mi1.NodeType);
        }

        [Test]
        public void SvgInsideMathMLWithAnnotationXmlAndOthers()
        {
            var doc = Html(@"<math><annotation-xml><svg><foreignObject><math><mi><svg></svg></mi><mo></mo></math><span></span></foreignObject><path></path></svg></annotation-xml><mi>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1math0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml0body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0.NodeType);

            var dochtml0body1math0annotationxml0 = dochtml0body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0annotationxml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0annotationxml0.Attributes.Count);
            Assert.AreEqual("annotation-xml", dochtml0body1math0annotationxml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0annotationxml0.NodeType);

            var dochtml0body1math0annotationxml0svg0 = dochtml0body1math0annotationxml0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0body1math0annotationxml0svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0annotationxml0svg0.Attributes.Count);
            Assert.AreEqual("svg", dochtml0body1math0annotationxml0svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0annotationxml0svg0.NodeType);

            var dochtml0body1math0annotationxml0svg0foreignObject0 = dochtml0body1math0annotationxml0svg0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0body1math0annotationxml0svg0foreignObject0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0annotationxml0svg0foreignObject0.Attributes.Count);
            Assert.AreEqual("foreignObject", dochtml0body1math0annotationxml0svg0foreignObject0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0annotationxml0svg0foreignObject0.NodeType);

            var dochtml0body1math0annotationxml0svg0foreignObject0math0 = dochtml0body1math0annotationxml0svg0foreignObject0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0body1math0annotationxml0svg0foreignObject0math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0annotationxml0svg0foreignObject0math0.Attributes.Count);
            Assert.AreEqual("math", dochtml0body1math0annotationxml0svg0foreignObject0math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0annotationxml0svg0foreignObject0math0.NodeType);

            var dochtml0body1math0annotationxml0svg0foreignObject0math0mi0 = dochtml0body1math0annotationxml0svg0foreignObject0math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0annotationxml0svg0foreignObject0math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0annotationxml0svg0foreignObject0math0mi0.Attributes.Count);
            Assert.AreEqual("mi", dochtml0body1math0annotationxml0svg0foreignObject0math0mi0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0annotationxml0svg0foreignObject0math0mi0.NodeType);

            var dochtml0body1math0annotationxml0svg0foreignObject0math0mi0svg0 = dochtml0body1math0annotationxml0svg0foreignObject0math0mi0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1math0annotationxml0svg0foreignObject0math0mi0svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0annotationxml0svg0foreignObject0math0mi0svg0.Attributes.Count);
            Assert.AreEqual("svg", dochtml0body1math0annotationxml0svg0foreignObject0math0mi0svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0annotationxml0svg0foreignObject0math0mi0svg0.NodeType);

            var dochtml0body1math0annotationxml0svg0foreignObject0math0mo1 = dochtml0body1math0annotationxml0svg0foreignObject0math0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1math0annotationxml0svg0foreignObject0math0mo1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0annotationxml0svg0foreignObject0math0mo1.Attributes.Count);
            Assert.AreEqual("mo", dochtml0body1math0annotationxml0svg0foreignObject0math0mo1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0annotationxml0svg0foreignObject0math0mo1.NodeType);

            var dochtml0body1math0annotationxml0svg0foreignObject0span1 = dochtml0body1math0annotationxml0svg0foreignObject0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1math0annotationxml0svg0foreignObject0span1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0annotationxml0svg0foreignObject0span1.Attributes.Count);
            Assert.AreEqual("span", dochtml0body1math0annotationxml0svg0foreignObject0span1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0annotationxml0svg0foreignObject0span1.NodeType);

            var dochtml0body1math0annotationxml0svg0path1 = dochtml0body1math0annotationxml0svg0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1math0annotationxml0svg0path1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0annotationxml0svg0path1.Attributes.Count);
            Assert.AreEqual("path", dochtml0body1math0annotationxml0svg0path1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0annotationxml0svg0path1.NodeType);

            var dochtml0body1math0mi1 = dochtml0body1math0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1math0mi1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mi1.Attributes.Count);
            Assert.AreEqual("mi", dochtml0body1math0mi1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mi1.NodeType);
        }
    }
}
