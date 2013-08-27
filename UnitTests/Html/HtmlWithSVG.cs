using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AngleSharp;
using AngleSharp.DOM;

namespace UnitTests
{
    [TestClass]
    public class HtmlWithSVG
    {
        [TestMethod]
        public void SvgCheckAttributesCaseNormalUnchanged()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body><svg attributeName='' attributeType='' baseFrequency='' baseProfile='' calcMode='' clipPathUnits='' contentScriptType='' contentStyleType='' diffuseConstant='' edgeMode='' externalResourcesRequired='' filterRes='' filterUnits='' glyphRef='' gradientTransform='' gradientUnits='' kernelMatrix='' kernelUnitLength='' keyPoints='' keySplines='' keyTimes='' lengthAdjust='' limitingConeAngle='' markerHeight='' markerUnits='' markerWidth='' maskContentUnits='' maskUnits='' numOctaves='' pathLength='' patternContentUnits='' patternTransform='' patternUnits='' pointsAtX='' pointsAtY='' pointsAtZ='' preserveAlpha='' preserveAspectRatio='' primitiveUnits='' refX='' refY='' repeatCount='' repeatDur='' requiredExtensions='' requiredFeatures='' specularConstant='' specularExponent='' spreadMethod='' startOffset='' stdDeviation='' stitchTiles='' surfaceScale='' systemLanguage='' tableValues='' targetX='' targetY='' textLength='' viewBox='' viewTarget='' xChannelSelector='' yChannelSelector='' zoomAndPan=''></svg>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(62, dochtml1body1svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml1body1svg0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["attributeName"]);
            Assert.AreEqual("attributeName", dochtml1body1svg0.Attributes["attributeName"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["attributeName"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["attributeType"]);
            Assert.AreEqual("attributeType", dochtml1body1svg0.Attributes["attributeType"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["attributeType"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["baseFrequency"]);
            Assert.AreEqual("baseFrequency", dochtml1body1svg0.Attributes["baseFrequency"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["baseFrequency"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["baseProfile"]);
            Assert.AreEqual("baseProfile", dochtml1body1svg0.Attributes["baseProfile"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["baseProfile"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["calcMode"]);
            Assert.AreEqual("calcMode", dochtml1body1svg0.Attributes["calcMode"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["calcMode"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["clipPathUnits"]);
            Assert.AreEqual("clipPathUnits", dochtml1body1svg0.Attributes["clipPathUnits"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["clipPathUnits"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["contentScriptType"]);
            Assert.AreEqual("contentScriptType", dochtml1body1svg0.Attributes["contentScriptType"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["contentScriptType"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["contentStyleType"]);
            Assert.AreEqual("contentStyleType", dochtml1body1svg0.Attributes["contentStyleType"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["contentStyleType"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["diffuseConstant"]);
            Assert.AreEqual("diffuseConstant", dochtml1body1svg0.Attributes["diffuseConstant"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["diffuseConstant"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["edgeMode"]);
            Assert.AreEqual("edgeMode", dochtml1body1svg0.Attributes["edgeMode"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["edgeMode"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["externalResourcesRequired"]);
            Assert.AreEqual("externalResourcesRequired", dochtml1body1svg0.Attributes["externalResourcesRequired"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["externalResourcesRequired"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["filterRes"]);
            Assert.AreEqual("filterRes", dochtml1body1svg0.Attributes["filterRes"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["filterRes"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["filterUnits"]);
            Assert.AreEqual("filterUnits", dochtml1body1svg0.Attributes["filterUnits"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["filterUnits"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["glyphRef"]);
            Assert.AreEqual("glyphRef", dochtml1body1svg0.Attributes["glyphRef"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["glyphRef"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["gradientTransform"]);
            Assert.AreEqual("gradientTransform", dochtml1body1svg0.Attributes["gradientTransform"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["gradientTransform"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["gradientUnits"]);
            Assert.AreEqual("gradientUnits", dochtml1body1svg0.Attributes["gradientUnits"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["gradientUnits"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["kernelMatrix"]);
            Assert.AreEqual("kernelMatrix", dochtml1body1svg0.Attributes["kernelMatrix"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["kernelMatrix"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["kernelUnitLength"]);
            Assert.AreEqual("kernelUnitLength", dochtml1body1svg0.Attributes["kernelUnitLength"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["kernelUnitLength"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["keyPoints"]);
            Assert.AreEqual("keyPoints", dochtml1body1svg0.Attributes["keyPoints"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["keyPoints"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["keySplines"]);
            Assert.AreEqual("keySplines", dochtml1body1svg0.Attributes["keySplines"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["keySplines"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["keyTimes"]);
            Assert.AreEqual("keyTimes", dochtml1body1svg0.Attributes["keyTimes"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["keyTimes"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["lengthAdjust"]);
            Assert.AreEqual("lengthAdjust", dochtml1body1svg0.Attributes["lengthAdjust"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["lengthAdjust"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["limitingConeAngle"]);
            Assert.AreEqual("limitingConeAngle", dochtml1body1svg0.Attributes["limitingConeAngle"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["limitingConeAngle"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["markerHeight"]);
            Assert.AreEqual("markerHeight", dochtml1body1svg0.Attributes["markerHeight"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["markerHeight"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["markerUnits"]);
            Assert.AreEqual("markerUnits", dochtml1body1svg0.Attributes["markerUnits"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["markerUnits"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["markerWidth"]);
            Assert.AreEqual("markerWidth", dochtml1body1svg0.Attributes["markerWidth"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["markerWidth"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["maskContentUnits"]);
            Assert.AreEqual("maskContentUnits", dochtml1body1svg0.Attributes["maskContentUnits"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["maskContentUnits"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["maskUnits"]);
            Assert.AreEqual("maskUnits", dochtml1body1svg0.Attributes["maskUnits"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["maskUnits"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["numOctaves"]);
            Assert.AreEqual("numOctaves", dochtml1body1svg0.Attributes["numOctaves"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["numOctaves"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["pathLength"]);
            Assert.AreEqual("pathLength", dochtml1body1svg0.Attributes["pathLength"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["pathLength"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["patternContentUnits"]);
            Assert.AreEqual("patternContentUnits", dochtml1body1svg0.Attributes["patternContentUnits"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["patternContentUnits"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["patternTransform"]);
            Assert.AreEqual("patternTransform", dochtml1body1svg0.Attributes["patternTransform"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["patternTransform"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["patternUnits"]);
            Assert.AreEqual("patternUnits", dochtml1body1svg0.Attributes["patternUnits"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["patternUnits"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["pointsAtX"]);
            Assert.AreEqual("pointsAtX", dochtml1body1svg0.Attributes["pointsAtX"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["pointsAtX"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["pointsAtY"]);
            Assert.AreEqual("pointsAtY", dochtml1body1svg0.Attributes["pointsAtY"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["pointsAtY"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["pointsAtZ"]);
            Assert.AreEqual("pointsAtZ", dochtml1body1svg0.Attributes["pointsAtZ"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["pointsAtZ"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["preserveAlpha"]);
            Assert.AreEqual("preserveAlpha", dochtml1body1svg0.Attributes["preserveAlpha"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["preserveAlpha"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["preserveAspectRatio"]);
            Assert.AreEqual("preserveAspectRatio", dochtml1body1svg0.Attributes["preserveAspectRatio"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["preserveAspectRatio"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["primitiveUnits"]);
            Assert.AreEqual("primitiveUnits", dochtml1body1svg0.Attributes["primitiveUnits"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["primitiveUnits"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["refX"]);
            Assert.AreEqual("refX", dochtml1body1svg0.Attributes["refX"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["refX"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["refY"]);
            Assert.AreEqual("refY", dochtml1body1svg0.Attributes["refY"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["refY"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["repeatCount"]);
            Assert.AreEqual("repeatCount", dochtml1body1svg0.Attributes["repeatCount"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["repeatCount"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["repeatDur"]);
            Assert.AreEqual("repeatDur", dochtml1body1svg0.Attributes["repeatDur"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["repeatDur"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["requiredExtensions"]);
            Assert.AreEqual("requiredExtensions", dochtml1body1svg0.Attributes["requiredExtensions"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["requiredExtensions"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["requiredFeatures"]);
            Assert.AreEqual("requiredFeatures", dochtml1body1svg0.Attributes["requiredFeatures"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["requiredFeatures"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["specularConstant"]);
            Assert.AreEqual("specularConstant", dochtml1body1svg0.Attributes["specularConstant"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["specularConstant"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["specularExponent"]);
            Assert.AreEqual("specularExponent", dochtml1body1svg0.Attributes["specularExponent"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["specularExponent"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["spreadMethod"]);
            Assert.AreEqual("spreadMethod", dochtml1body1svg0.Attributes["spreadMethod"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["spreadMethod"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["startOffset"]);
            Assert.AreEqual("startOffset", dochtml1body1svg0.Attributes["startOffset"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["startOffset"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["stdDeviation"]);
            Assert.AreEqual("stdDeviation", dochtml1body1svg0.Attributes["stdDeviation"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["stdDeviation"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["stitchTiles"]);
            Assert.AreEqual("stitchTiles", dochtml1body1svg0.Attributes["stitchTiles"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["stitchTiles"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["surfaceScale"]);
            Assert.AreEqual("surfaceScale", dochtml1body1svg0.Attributes["surfaceScale"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["surfaceScale"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["systemLanguage"]);
            Assert.AreEqual("systemLanguage", dochtml1body1svg0.Attributes["systemLanguage"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["systemLanguage"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["tableValues"]);
            Assert.AreEqual("tableValues", dochtml1body1svg0.Attributes["tableValues"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["tableValues"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["targetX"]);
            Assert.AreEqual("targetX", dochtml1body1svg0.Attributes["targetX"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["targetX"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["targetY"]);
            Assert.AreEqual("targetY", dochtml1body1svg0.Attributes["targetY"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["targetY"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["textLength"]);
            Assert.AreEqual("textLength", dochtml1body1svg0.Attributes["textLength"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["textLength"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["viewBox"]);
            Assert.AreEqual("viewBox", dochtml1body1svg0.Attributes["viewBox"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["viewBox"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["viewTarget"]);
            Assert.AreEqual("viewTarget", dochtml1body1svg0.Attributes["viewTarget"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["viewTarget"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["xChannelSelector"]);
            Assert.AreEqual("xChannelSelector", dochtml1body1svg0.Attributes["xChannelSelector"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["xChannelSelector"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["yChannelSelector"]);
            Assert.AreEqual("yChannelSelector", dochtml1body1svg0.Attributes["yChannelSelector"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["yChannelSelector"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["zoomAndPan"]);
            Assert.AreEqual("zoomAndPan", dochtml1body1svg0.Attributes["zoomAndPan"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["zoomAndPan"].Value);
        }

        [TestMethod]
        public void SvgCheckAttributesCaseUppercaseModified()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><BODY><SVG ATTRIBUTENAME='' ATTRIBUTETYPE='' BASEFREQUENCY='' BASEPROFILE='' CALCMODE='' CLIPPATHUNITS='' CONTENTSCRIPTTYPE='' CONTENTSTYLETYPE='' DIFFUSECONSTANT='' EDGEMODE='' EXTERNALRESOURCESREQUIRED='' FILTERRES='' FILTERUNITS='' GLYPHREF='' GRADIENTTRANSFORM='' GRADIENTUNITS='' KERNELMATRIX='' KERNELUNITLENGTH='' KEYPOINTS='' KEYSPLINES='' KEYTIMES='' LENGTHADJUST='' LIMITINGCONEANGLE='' MARKERHEIGHT='' MARKERUNITS='' MARKERWIDTH='' MASKCONTENTUNITS='' MASKUNITS='' NUMOCTAVES='' PATHLENGTH='' PATTERNCONTENTUNITS='' PATTERNTRANSFORM='' PATTERNUNITS='' POINTSATX='' POINTSATY='' POINTSATZ='' PRESERVEALPHA='' PRESERVEASPECTRATIO='' PRIMITIVEUNITS='' REFX='' REFY='' REPEATCOUNT='' REPEATDUR='' REQUIREDEXTENSIONS='' REQUIREDFEATURES='' SPECULARCONSTANT='' SPECULAREXPONENT='' SPREADMETHOD='' STARTOFFSET='' STDDEVIATION='' STITCHTILES='' SURFACESCALE='' SYSTEMLANGUAGE='' TABLEVALUES='' TARGETX='' TARGETY='' TEXTLENGTH='' VIEWBOX='' VIEWTARGET='' XCHANNELSELECTOR='' YCHANNELSELECTOR='' ZOOMANDPAN=''></SVG>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(62, dochtml1body1svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml1body1svg0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["attributeName"]);
            Assert.AreEqual("attributeName", dochtml1body1svg0.Attributes["attributeName"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["attributeName"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["attributeType"]);
            Assert.AreEqual("attributeType", dochtml1body1svg0.Attributes["attributeType"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["attributeType"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["baseFrequency"]);
            Assert.AreEqual("baseFrequency", dochtml1body1svg0.Attributes["baseFrequency"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["baseFrequency"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["baseProfile"]);
            Assert.AreEqual("baseProfile", dochtml1body1svg0.Attributes["baseProfile"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["baseProfile"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["calcMode"]);
            Assert.AreEqual("calcMode", dochtml1body1svg0.Attributes["calcMode"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["calcMode"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["clipPathUnits"]);
            Assert.AreEqual("clipPathUnits", dochtml1body1svg0.Attributes["clipPathUnits"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["clipPathUnits"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["contentScriptType"]);
            Assert.AreEqual("contentScriptType", dochtml1body1svg0.Attributes["contentScriptType"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["contentScriptType"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["contentStyleType"]);
            Assert.AreEqual("contentStyleType", dochtml1body1svg0.Attributes["contentStyleType"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["contentStyleType"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["diffuseConstant"]);
            Assert.AreEqual("diffuseConstant", dochtml1body1svg0.Attributes["diffuseConstant"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["diffuseConstant"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["edgeMode"]);
            Assert.AreEqual("edgeMode", dochtml1body1svg0.Attributes["edgeMode"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["edgeMode"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["externalResourcesRequired"]);
            Assert.AreEqual("externalResourcesRequired", dochtml1body1svg0.Attributes["externalResourcesRequired"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["externalResourcesRequired"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["filterRes"]);
            Assert.AreEqual("filterRes", dochtml1body1svg0.Attributes["filterRes"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["filterRes"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["filterUnits"]);
            Assert.AreEqual("filterUnits", dochtml1body1svg0.Attributes["filterUnits"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["filterUnits"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["glyphRef"]);
            Assert.AreEqual("glyphRef", dochtml1body1svg0.Attributes["glyphRef"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["glyphRef"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["gradientTransform"]);
            Assert.AreEqual("gradientTransform", dochtml1body1svg0.Attributes["gradientTransform"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["gradientTransform"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["gradientUnits"]);
            Assert.AreEqual("gradientUnits", dochtml1body1svg0.Attributes["gradientUnits"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["gradientUnits"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["kernelMatrix"]);
            Assert.AreEqual("kernelMatrix", dochtml1body1svg0.Attributes["kernelMatrix"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["kernelMatrix"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["kernelUnitLength"]);
            Assert.AreEqual("kernelUnitLength", dochtml1body1svg0.Attributes["kernelUnitLength"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["kernelUnitLength"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["keyPoints"]);
            Assert.AreEqual("keyPoints", dochtml1body1svg0.Attributes["keyPoints"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["keyPoints"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["keySplines"]);
            Assert.AreEqual("keySplines", dochtml1body1svg0.Attributes["keySplines"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["keySplines"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["keyTimes"]);
            Assert.AreEqual("keyTimes", dochtml1body1svg0.Attributes["keyTimes"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["keyTimes"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["lengthAdjust"]);
            Assert.AreEqual("lengthAdjust", dochtml1body1svg0.Attributes["lengthAdjust"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["lengthAdjust"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["limitingConeAngle"]);
            Assert.AreEqual("limitingConeAngle", dochtml1body1svg0.Attributes["limitingConeAngle"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["limitingConeAngle"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["markerHeight"]);
            Assert.AreEqual("markerHeight", dochtml1body1svg0.Attributes["markerHeight"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["markerHeight"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["markerUnits"]);
            Assert.AreEqual("markerUnits", dochtml1body1svg0.Attributes["markerUnits"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["markerUnits"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["markerWidth"]);
            Assert.AreEqual("markerWidth", dochtml1body1svg0.Attributes["markerWidth"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["markerWidth"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["maskContentUnits"]);
            Assert.AreEqual("maskContentUnits", dochtml1body1svg0.Attributes["maskContentUnits"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["maskContentUnits"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["maskUnits"]);
            Assert.AreEqual("maskUnits", dochtml1body1svg0.Attributes["maskUnits"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["maskUnits"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["numOctaves"]);
            Assert.AreEqual("numOctaves", dochtml1body1svg0.Attributes["numOctaves"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["numOctaves"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["pathLength"]);
            Assert.AreEqual("pathLength", dochtml1body1svg0.Attributes["pathLength"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["pathLength"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["patternContentUnits"]);
            Assert.AreEqual("patternContentUnits", dochtml1body1svg0.Attributes["patternContentUnits"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["patternContentUnits"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["patternTransform"]);
            Assert.AreEqual("patternTransform", dochtml1body1svg0.Attributes["patternTransform"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["patternTransform"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["patternUnits"]);
            Assert.AreEqual("patternUnits", dochtml1body1svg0.Attributes["patternUnits"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["patternUnits"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["pointsAtX"]);
            Assert.AreEqual("pointsAtX", dochtml1body1svg0.Attributes["pointsAtX"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["pointsAtX"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["pointsAtY"]);
            Assert.AreEqual("pointsAtY", dochtml1body1svg0.Attributes["pointsAtY"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["pointsAtY"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["pointsAtZ"]);
            Assert.AreEqual("pointsAtZ", dochtml1body1svg0.Attributes["pointsAtZ"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["pointsAtZ"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["preserveAlpha"]);
            Assert.AreEqual("preserveAlpha", dochtml1body1svg0.Attributes["preserveAlpha"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["preserveAlpha"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["preserveAspectRatio"]);
            Assert.AreEqual("preserveAspectRatio", dochtml1body1svg0.Attributes["preserveAspectRatio"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["preserveAspectRatio"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["primitiveUnits"]);
            Assert.AreEqual("primitiveUnits", dochtml1body1svg0.Attributes["primitiveUnits"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["primitiveUnits"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["refX"]);
            Assert.AreEqual("refX", dochtml1body1svg0.Attributes["refX"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["refX"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["refY"]);
            Assert.AreEqual("refY", dochtml1body1svg0.Attributes["refY"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["refY"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["repeatCount"]);
            Assert.AreEqual("repeatCount", dochtml1body1svg0.Attributes["repeatCount"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["repeatCount"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["repeatDur"]);
            Assert.AreEqual("repeatDur", dochtml1body1svg0.Attributes["repeatDur"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["repeatDur"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["requiredExtensions"]);
            Assert.AreEqual("requiredExtensions", dochtml1body1svg0.Attributes["requiredExtensions"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["requiredExtensions"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["requiredFeatures"]);
            Assert.AreEqual("requiredFeatures", dochtml1body1svg0.Attributes["requiredFeatures"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["requiredFeatures"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["specularConstant"]);
            Assert.AreEqual("specularConstant", dochtml1body1svg0.Attributes["specularConstant"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["specularConstant"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["specularExponent"]);
            Assert.AreEqual("specularExponent", dochtml1body1svg0.Attributes["specularExponent"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["specularExponent"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["spreadMethod"]);
            Assert.AreEqual("spreadMethod", dochtml1body1svg0.Attributes["spreadMethod"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["spreadMethod"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["startOffset"]);
            Assert.AreEqual("startOffset", dochtml1body1svg0.Attributes["startOffset"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["startOffset"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["stdDeviation"]);
            Assert.AreEqual("stdDeviation", dochtml1body1svg0.Attributes["stdDeviation"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["stdDeviation"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["stitchTiles"]);
            Assert.AreEqual("stitchTiles", dochtml1body1svg0.Attributes["stitchTiles"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["stitchTiles"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["surfaceScale"]);
            Assert.AreEqual("surfaceScale", dochtml1body1svg0.Attributes["surfaceScale"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["surfaceScale"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["systemLanguage"]);
            Assert.AreEqual("systemLanguage", dochtml1body1svg0.Attributes["systemLanguage"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["systemLanguage"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["tableValues"]);
            Assert.AreEqual("tableValues", dochtml1body1svg0.Attributes["tableValues"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["tableValues"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["targetX"]);
            Assert.AreEqual("targetX", dochtml1body1svg0.Attributes["targetX"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["targetX"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["targetY"]);
            Assert.AreEqual("targetY", dochtml1body1svg0.Attributes["targetY"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["targetY"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["textLength"]);
            Assert.AreEqual("textLength", dochtml1body1svg0.Attributes["textLength"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["textLength"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["viewBox"]);
            Assert.AreEqual("viewBox", dochtml1body1svg0.Attributes["viewBox"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["viewBox"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["viewTarget"]);
            Assert.AreEqual("viewTarget", dochtml1body1svg0.Attributes["viewTarget"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["viewTarget"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["xChannelSelector"]);
            Assert.AreEqual("xChannelSelector", dochtml1body1svg0.Attributes["xChannelSelector"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["xChannelSelector"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["yChannelSelector"]);
            Assert.AreEqual("yChannelSelector", dochtml1body1svg0.Attributes["yChannelSelector"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["yChannelSelector"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["zoomAndPan"]);
            Assert.AreEqual("zoomAndPan", dochtml1body1svg0.Attributes["zoomAndPan"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["zoomAndPan"].Value);
        }

        [TestMethod]
        public void SvgCheckAttributesCaseLowercaseModified()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body><svg attributename='' attributetype='' basefrequency='' baseprofile='' calcmode='' clippathunits='' contentscripttype='' contentstyletype='' diffuseconstant='' edgemode='' externalresourcesrequired='' filterres='' filterunits='' glyphref='' gradienttransform='' gradientunits='' kernelmatrix='' kernelunitlength='' keypoints='' keysplines='' keytimes='' lengthadjust='' limitingconeangle='' markerheight='' markerunits='' markerwidth='' maskcontentunits='' maskunits='' numoctaves='' pathlength='' patterncontentunits='' patterntransform='' patternunits='' pointsatx='' pointsaty='' pointsatz='' preservealpha='' preserveaspectratio='' primitiveunits='' refx='' refy='' repeatcount='' repeatdur='' requiredextensions='' requiredfeatures='' specularconstant='' specularexponent='' spreadmethod='' startoffset='' stddeviation='' stitchtiles='' surfacescale='' systemlanguage='' tablevalues='' targetx='' targety='' textlength='' viewbox='' viewtarget='' xchannelselector='' ychannelselector='' zoomandpan=''></svg>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(62, dochtml1body1svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml1body1svg0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["attributeName"]);
            Assert.AreEqual("attributeName", dochtml1body1svg0.Attributes["attributeName"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["attributeName"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["attributeType"]);
            Assert.AreEqual("attributeType", dochtml1body1svg0.Attributes["attributeType"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["attributeType"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["baseFrequency"]);
            Assert.AreEqual("baseFrequency", dochtml1body1svg0.Attributes["baseFrequency"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["baseFrequency"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["baseProfile"]);
            Assert.AreEqual("baseProfile", dochtml1body1svg0.Attributes["baseProfile"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["baseProfile"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["calcMode"]);
            Assert.AreEqual("calcMode", dochtml1body1svg0.Attributes["calcMode"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["calcMode"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["clipPathUnits"]);
            Assert.AreEqual("clipPathUnits", dochtml1body1svg0.Attributes["clipPathUnits"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["clipPathUnits"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["contentScriptType"]);
            Assert.AreEqual("contentScriptType", dochtml1body1svg0.Attributes["contentScriptType"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["contentScriptType"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["contentStyleType"]);
            Assert.AreEqual("contentStyleType", dochtml1body1svg0.Attributes["contentStyleType"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["contentStyleType"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["diffuseConstant"]);
            Assert.AreEqual("diffuseConstant", dochtml1body1svg0.Attributes["diffuseConstant"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["diffuseConstant"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["edgeMode"]);
            Assert.AreEqual("edgeMode", dochtml1body1svg0.Attributes["edgeMode"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["edgeMode"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["externalResourcesRequired"]);
            Assert.AreEqual("externalResourcesRequired", dochtml1body1svg0.Attributes["externalResourcesRequired"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["externalResourcesRequired"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["filterRes"]);
            Assert.AreEqual("filterRes", dochtml1body1svg0.Attributes["filterRes"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["filterRes"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["filterUnits"]);
            Assert.AreEqual("filterUnits", dochtml1body1svg0.Attributes["filterUnits"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["filterUnits"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["glyphRef"]);
            Assert.AreEqual("glyphRef", dochtml1body1svg0.Attributes["glyphRef"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["glyphRef"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["gradientTransform"]);
            Assert.AreEqual("gradientTransform", dochtml1body1svg0.Attributes["gradientTransform"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["gradientTransform"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["gradientUnits"]);
            Assert.AreEqual("gradientUnits", dochtml1body1svg0.Attributes["gradientUnits"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["gradientUnits"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["kernelMatrix"]);
            Assert.AreEqual("kernelMatrix", dochtml1body1svg0.Attributes["kernelMatrix"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["kernelMatrix"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["kernelUnitLength"]);
            Assert.AreEqual("kernelUnitLength", dochtml1body1svg0.Attributes["kernelUnitLength"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["kernelUnitLength"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["keyPoints"]);
            Assert.AreEqual("keyPoints", dochtml1body1svg0.Attributes["keyPoints"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["keyPoints"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["keySplines"]);
            Assert.AreEqual("keySplines", dochtml1body1svg0.Attributes["keySplines"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["keySplines"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["keyTimes"]);
            Assert.AreEqual("keyTimes", dochtml1body1svg0.Attributes["keyTimes"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["keyTimes"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["lengthAdjust"]);
            Assert.AreEqual("lengthAdjust", dochtml1body1svg0.Attributes["lengthAdjust"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["lengthAdjust"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["limitingConeAngle"]);
            Assert.AreEqual("limitingConeAngle", dochtml1body1svg0.Attributes["limitingConeAngle"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["limitingConeAngle"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["markerHeight"]);
            Assert.AreEqual("markerHeight", dochtml1body1svg0.Attributes["markerHeight"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["markerHeight"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["markerUnits"]);
            Assert.AreEqual("markerUnits", dochtml1body1svg0.Attributes["markerUnits"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["markerUnits"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["markerWidth"]);
            Assert.AreEqual("markerWidth", dochtml1body1svg0.Attributes["markerWidth"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["markerWidth"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["maskContentUnits"]);
            Assert.AreEqual("maskContentUnits", dochtml1body1svg0.Attributes["maskContentUnits"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["maskContentUnits"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["maskUnits"]);
            Assert.AreEqual("maskUnits", dochtml1body1svg0.Attributes["maskUnits"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["maskUnits"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["numOctaves"]);
            Assert.AreEqual("numOctaves", dochtml1body1svg0.Attributes["numOctaves"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["numOctaves"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["pathLength"]);
            Assert.AreEqual("pathLength", dochtml1body1svg0.Attributes["pathLength"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["pathLength"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["patternContentUnits"]);
            Assert.AreEqual("patternContentUnits", dochtml1body1svg0.Attributes["patternContentUnits"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["patternContentUnits"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["patternTransform"]);
            Assert.AreEqual("patternTransform", dochtml1body1svg0.Attributes["patternTransform"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["patternTransform"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["patternUnits"]);
            Assert.AreEqual("patternUnits", dochtml1body1svg0.Attributes["patternUnits"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["patternUnits"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["pointsAtX"]);
            Assert.AreEqual("pointsAtX", dochtml1body1svg0.Attributes["pointsAtX"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["pointsAtX"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["pointsAtY"]);
            Assert.AreEqual("pointsAtY", dochtml1body1svg0.Attributes["pointsAtY"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["pointsAtY"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["pointsAtZ"]);
            Assert.AreEqual("pointsAtZ", dochtml1body1svg0.Attributes["pointsAtZ"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["pointsAtZ"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["preserveAlpha"]);
            Assert.AreEqual("preserveAlpha", dochtml1body1svg0.Attributes["preserveAlpha"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["preserveAlpha"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["preserveAspectRatio"]);
            Assert.AreEqual("preserveAspectRatio", dochtml1body1svg0.Attributes["preserveAspectRatio"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["preserveAspectRatio"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["primitiveUnits"]);
            Assert.AreEqual("primitiveUnits", dochtml1body1svg0.Attributes["primitiveUnits"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["primitiveUnits"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["refX"]);
            Assert.AreEqual("refX", dochtml1body1svg0.Attributes["refX"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["refX"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["refY"]);
            Assert.AreEqual("refY", dochtml1body1svg0.Attributes["refY"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["refY"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["repeatCount"]);
            Assert.AreEqual("repeatCount", dochtml1body1svg0.Attributes["repeatCount"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["repeatCount"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["repeatDur"]);
            Assert.AreEqual("repeatDur", dochtml1body1svg0.Attributes["repeatDur"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["repeatDur"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["requiredExtensions"]);
            Assert.AreEqual("requiredExtensions", dochtml1body1svg0.Attributes["requiredExtensions"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["requiredExtensions"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["requiredFeatures"]);
            Assert.AreEqual("requiredFeatures", dochtml1body1svg0.Attributes["requiredFeatures"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["requiredFeatures"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["specularConstant"]);
            Assert.AreEqual("specularConstant", dochtml1body1svg0.Attributes["specularConstant"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["specularConstant"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["specularExponent"]);
            Assert.AreEqual("specularExponent", dochtml1body1svg0.Attributes["specularExponent"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["specularExponent"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["spreadMethod"]);
            Assert.AreEqual("spreadMethod", dochtml1body1svg0.Attributes["spreadMethod"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["spreadMethod"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["startOffset"]);
            Assert.AreEqual("startOffset", dochtml1body1svg0.Attributes["startOffset"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["startOffset"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["stdDeviation"]);
            Assert.AreEqual("stdDeviation", dochtml1body1svg0.Attributes["stdDeviation"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["stdDeviation"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["stitchTiles"]);
            Assert.AreEqual("stitchTiles", dochtml1body1svg0.Attributes["stitchTiles"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["stitchTiles"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["surfaceScale"]);
            Assert.AreEqual("surfaceScale", dochtml1body1svg0.Attributes["surfaceScale"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["surfaceScale"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["systemLanguage"]);
            Assert.AreEqual("systemLanguage", dochtml1body1svg0.Attributes["systemLanguage"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["systemLanguage"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["tableValues"]);
            Assert.AreEqual("tableValues", dochtml1body1svg0.Attributes["tableValues"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["tableValues"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["targetX"]);
            Assert.AreEqual("targetX", dochtml1body1svg0.Attributes["targetX"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["targetX"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["targetY"]);
            Assert.AreEqual("targetY", dochtml1body1svg0.Attributes["targetY"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["targetY"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["textLength"]);
            Assert.AreEqual("textLength", dochtml1body1svg0.Attributes["textLength"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["textLength"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["viewBox"]);
            Assert.AreEqual("viewBox", dochtml1body1svg0.Attributes["viewBox"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["viewBox"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["viewTarget"]);
            Assert.AreEqual("viewTarget", dochtml1body1svg0.Attributes["viewTarget"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["viewTarget"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["xChannelSelector"]);
            Assert.AreEqual("xChannelSelector", dochtml1body1svg0.Attributes["xChannelSelector"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["xChannelSelector"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["yChannelSelector"]);
            Assert.AreEqual("yChannelSelector", dochtml1body1svg0.Attributes["yChannelSelector"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["yChannelSelector"].Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes["zoomAndPan"]);
            Assert.AreEqual("zoomAndPan", dochtml1body1svg0.Attributes["zoomAndPan"].Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes["zoomAndPan"].Value);

        }

        [TestMethod]
        public void SvgCheckTagCaseNormalUnchanged()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body><svg><altGlyph /><altGlyphDef /><altGlyphItem /><animateColor /><animateMotion /><animateTransform /><clipPath /><feBlend /><feColorMatrix /><feComponentTransfer /><feComposite /><feConvolveMatrix /><feDiffuseLighting /><feDisplacementMap /><feDistantLight /><feFlood /><feFuncA /><feFuncB /><feFuncG /><feFuncR /><feGaussianBlur /><feImage /><feMerge /><feMergeNode /><feMorphology /><feOffset /><fePointLight /><feSpecularLighting /><feSpotLight /><feTile /><feTurbulence /><foreignObject /><glyphRef /><linearGradient /><radialGradient /><textPath /></svg>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(36, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml1body1svg0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);

            var dochtml1body1svg0altGlyph0 = dochtml1body1svg0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1svg0altGlyph0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0altGlyph0.Attributes.Length);
            Assert.AreEqual("altGlyph", dochtml1body1svg0altGlyph0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0altGlyph0.NodeType);

            var dochtml1body1svg0altGlyphDef1 = dochtml1body1svg0.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1svg0altGlyphDef1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0altGlyphDef1.Attributes.Length);
            Assert.AreEqual("altGlyphDef", dochtml1body1svg0altGlyphDef1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0altGlyphDef1.NodeType);

            var dochtml1body1svg0altGlyphItem2 = dochtml1body1svg0.ChildNodes[2];
            Assert.AreEqual(0, dochtml1body1svg0altGlyphItem2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0altGlyphItem2.Attributes.Length);
            Assert.AreEqual("altGlyphItem", dochtml1body1svg0altGlyphItem2.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0altGlyphItem2.NodeType);

            var dochtml1body1svg0animateColor3 = dochtml1body1svg0.ChildNodes[3];
            Assert.AreEqual(0, dochtml1body1svg0animateColor3.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0animateColor3.Attributes.Length);
            Assert.AreEqual("animateColor", dochtml1body1svg0animateColor3.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0animateColor3.NodeType);

            var dochtml1body1svg0animateMotion4 = dochtml1body1svg0.ChildNodes[4];
            Assert.AreEqual(0, dochtml1body1svg0animateMotion4.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0animateMotion4.Attributes.Length);
            Assert.AreEqual("animateMotion", dochtml1body1svg0animateMotion4.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0animateMotion4.NodeType);

            var dochtml1body1svg0animateTransform5 = dochtml1body1svg0.ChildNodes[5];
            Assert.AreEqual(0, dochtml1body1svg0animateTransform5.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0animateTransform5.Attributes.Length);
            Assert.AreEqual("animateTransform", dochtml1body1svg0animateTransform5.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0animateTransform5.NodeType);

            var dochtml1body1svg0clipPath6 = dochtml1body1svg0.ChildNodes[6];
            Assert.AreEqual(0, dochtml1body1svg0clipPath6.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0clipPath6.Attributes.Length);
            Assert.AreEqual("clipPath", dochtml1body1svg0clipPath6.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0clipPath6.NodeType);

            var dochtml1body1svg0feBlend7 = dochtml1body1svg0.ChildNodes[7];
            Assert.AreEqual(0, dochtml1body1svg0feBlend7.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feBlend7.Attributes.Length);
            Assert.AreEqual("feBlend", dochtml1body1svg0feBlend7.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feBlend7.NodeType);

            var dochtml1body1svg0feColorMatrix8 = dochtml1body1svg0.ChildNodes[8];
            Assert.AreEqual(0, dochtml1body1svg0feColorMatrix8.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feColorMatrix8.Attributes.Length);
            Assert.AreEqual("feColorMatrix", dochtml1body1svg0feColorMatrix8.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feColorMatrix8.NodeType);

            var dochtml1body1svg0feComponentTransfer9 = dochtml1body1svg0.ChildNodes[9];
            Assert.AreEqual(0, dochtml1body1svg0feComponentTransfer9.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feComponentTransfer9.Attributes.Length);
            Assert.AreEqual("feComponentTransfer", dochtml1body1svg0feComponentTransfer9.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feComponentTransfer9.NodeType);

            var dochtml1body1svg0feComposite10 = dochtml1body1svg0.ChildNodes[10];
            Assert.AreEqual(0, dochtml1body1svg0feComposite10.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feComposite10.Attributes.Length);
            Assert.AreEqual("feComposite", dochtml1body1svg0feComposite10.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feComposite10.NodeType);

            var dochtml1body1svg0feConvolveMatrix11 = dochtml1body1svg0.ChildNodes[11];
            Assert.AreEqual(0, dochtml1body1svg0feConvolveMatrix11.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feConvolveMatrix11.Attributes.Length);
            Assert.AreEqual("feConvolveMatrix", dochtml1body1svg0feConvolveMatrix11.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feConvolveMatrix11.NodeType);

            var dochtml1body1svg0feDiffuseLighting12 = dochtml1body1svg0.ChildNodes[12];
            Assert.AreEqual(0, dochtml1body1svg0feDiffuseLighting12.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feDiffuseLighting12.Attributes.Length);
            Assert.AreEqual("feDiffuseLighting", dochtml1body1svg0feDiffuseLighting12.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feDiffuseLighting12.NodeType);

            var dochtml1body1svg0feDisplacementMap13 = dochtml1body1svg0.ChildNodes[13];
            Assert.AreEqual(0, dochtml1body1svg0feDisplacementMap13.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feDisplacementMap13.Attributes.Length);
            Assert.AreEqual("feDisplacementMap", dochtml1body1svg0feDisplacementMap13.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feDisplacementMap13.NodeType);

            var dochtml1body1svg0feDistantLight14 = dochtml1body1svg0.ChildNodes[14];
            Assert.AreEqual(0, dochtml1body1svg0feDistantLight14.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feDistantLight14.Attributes.Length);
            Assert.AreEqual("feDistantLight", dochtml1body1svg0feDistantLight14.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feDistantLight14.NodeType);

            var dochtml1body1svg0feFlood15 = dochtml1body1svg0.ChildNodes[15];
            Assert.AreEqual(0, dochtml1body1svg0feFlood15.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feFlood15.Attributes.Length);
            Assert.AreEqual("feFlood", dochtml1body1svg0feFlood15.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feFlood15.NodeType);

            var dochtml1body1svg0feFuncA16 = dochtml1body1svg0.ChildNodes[16];
            Assert.AreEqual(0, dochtml1body1svg0feFuncA16.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feFuncA16.Attributes.Length);
            Assert.AreEqual("feFuncA", dochtml1body1svg0feFuncA16.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feFuncA16.NodeType);

            var dochtml1body1svg0feFuncB17 = dochtml1body1svg0.ChildNodes[17];
            Assert.AreEqual(0, dochtml1body1svg0feFuncB17.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feFuncB17.Attributes.Length);
            Assert.AreEqual("feFuncB", dochtml1body1svg0feFuncB17.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feFuncB17.NodeType);

            var dochtml1body1svg0feFuncG18 = dochtml1body1svg0.ChildNodes[18];
            Assert.AreEqual(0, dochtml1body1svg0feFuncG18.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feFuncG18.Attributes.Length);
            Assert.AreEqual("feFuncG", dochtml1body1svg0feFuncG18.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feFuncG18.NodeType);

            var dochtml1body1svg0feFuncR19 = dochtml1body1svg0.ChildNodes[19];
            Assert.AreEqual(0, dochtml1body1svg0feFuncR19.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feFuncR19.Attributes.Length);
            Assert.AreEqual("feFuncR", dochtml1body1svg0feFuncR19.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feFuncR19.NodeType);

            var dochtml1body1svg0feGaussianBlur20 = dochtml1body1svg0.ChildNodes[20];
            Assert.AreEqual(0, dochtml1body1svg0feGaussianBlur20.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feGaussianBlur20.Attributes.Length);
            Assert.AreEqual("feGaussianBlur", dochtml1body1svg0feGaussianBlur20.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feGaussianBlur20.NodeType);

            var dochtml1body1svg0feImage21 = dochtml1body1svg0.ChildNodes[21];
            Assert.AreEqual(0, dochtml1body1svg0feImage21.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feImage21.Attributes.Length);
            Assert.AreEqual("feImage", dochtml1body1svg0feImage21.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feImage21.NodeType);

            var dochtml1body1svg0feMerge22 = dochtml1body1svg0.ChildNodes[22];
            Assert.AreEqual(0, dochtml1body1svg0feMerge22.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feMerge22.Attributes.Length);
            Assert.AreEqual("feMerge", dochtml1body1svg0feMerge22.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feMerge22.NodeType);

            var dochtml1body1svg0feMergeNode23 = dochtml1body1svg0.ChildNodes[23];
            Assert.AreEqual(0, dochtml1body1svg0feMergeNode23.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feMergeNode23.Attributes.Length);
            Assert.AreEqual("feMergeNode", dochtml1body1svg0feMergeNode23.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feMergeNode23.NodeType);

            var dochtml1body1svg0feMorphology24 = dochtml1body1svg0.ChildNodes[24];
            Assert.AreEqual(0, dochtml1body1svg0feMorphology24.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feMorphology24.Attributes.Length);
            Assert.AreEqual("feMorphology", dochtml1body1svg0feMorphology24.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feMorphology24.NodeType);

            var dochtml1body1svg0feOffset25 = dochtml1body1svg0.ChildNodes[25];
            Assert.AreEqual(0, dochtml1body1svg0feOffset25.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feOffset25.Attributes.Length);
            Assert.AreEqual("feOffset", dochtml1body1svg0feOffset25.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feOffset25.NodeType);

            var dochtml1body1svg0fePointLight26 = dochtml1body1svg0.ChildNodes[26];
            Assert.AreEqual(0, dochtml1body1svg0fePointLight26.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0fePointLight26.Attributes.Length);
            Assert.AreEqual("fePointLight", dochtml1body1svg0fePointLight26.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0fePointLight26.NodeType);

            var dochtml1body1svg0feSpecularLighting27 = dochtml1body1svg0.ChildNodes[27];
            Assert.AreEqual(0, dochtml1body1svg0feSpecularLighting27.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feSpecularLighting27.Attributes.Length);
            Assert.AreEqual("feSpecularLighting", dochtml1body1svg0feSpecularLighting27.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feSpecularLighting27.NodeType);

            var dochtml1body1svg0feSpotLight28 = dochtml1body1svg0.ChildNodes[28];
            Assert.AreEqual(0, dochtml1body1svg0feSpotLight28.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feSpotLight28.Attributes.Length);
            Assert.AreEqual("feSpotLight", dochtml1body1svg0feSpotLight28.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feSpotLight28.NodeType);

            var dochtml1body1svg0feTile29 = dochtml1body1svg0.ChildNodes[29];
            Assert.AreEqual(0, dochtml1body1svg0feTile29.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feTile29.Attributes.Length);
            Assert.AreEqual("feTile", dochtml1body1svg0feTile29.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feTile29.NodeType);

            var dochtml1body1svg0feTurbulence30 = dochtml1body1svg0.ChildNodes[30];
            Assert.AreEqual(0, dochtml1body1svg0feTurbulence30.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feTurbulence30.Attributes.Length);
            Assert.AreEqual("feTurbulence", dochtml1body1svg0feTurbulence30.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feTurbulence30.NodeType);

            var dochtml1body1svg0foreignObject31 = dochtml1body1svg0.ChildNodes[31];
            Assert.AreEqual(0, dochtml1body1svg0foreignObject31.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0foreignObject31.Attributes.Length);
            Assert.AreEqual("foreignObject", dochtml1body1svg0foreignObject31.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0foreignObject31.NodeType);

            var dochtml1body1svg0glyphRef32 = dochtml1body1svg0.ChildNodes[32];
            Assert.AreEqual(0, dochtml1body1svg0glyphRef32.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0glyphRef32.Attributes.Length);
            Assert.AreEqual("glyphRef", dochtml1body1svg0glyphRef32.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0glyphRef32.NodeType);

            var dochtml1body1svg0linearGradient33 = dochtml1body1svg0.ChildNodes[33];
            Assert.AreEqual(0, dochtml1body1svg0linearGradient33.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0linearGradient33.Attributes.Length);
            Assert.AreEqual("linearGradient", dochtml1body1svg0linearGradient33.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0linearGradient33.NodeType);

            var dochtml1body1svg0radialGradient34 = dochtml1body1svg0.ChildNodes[34];
            Assert.AreEqual(0, dochtml1body1svg0radialGradient34.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0radialGradient34.Attributes.Length);
            Assert.AreEqual("radialGradient", dochtml1body1svg0radialGradient34.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0radialGradient34.NodeType);

            var dochtml1body1svg0textPath35 = dochtml1body1svg0.ChildNodes[35];
            Assert.AreEqual(0, dochtml1body1svg0textPath35.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0textPath35.Attributes.Length);
            Assert.AreEqual("textPath", dochtml1body1svg0textPath35.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0textPath35.NodeType);
        }

        [TestMethod]
        public void SvgCheckTagCaseLowercaseModified()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body><svg><altglyph /><altglyphdef /><altglyphitem /><animatecolor /><animatemotion /><animatetransform /><clippath /><feblend /><fecolormatrix /><fecomponenttransfer /><fecomposite /><feconvolvematrix /><fediffuselighting /><fedisplacementmap /><fedistantlight /><feflood /><fefunca /><fefuncb /><fefuncg /><fefuncr /><fegaussianblur /><feimage /><femerge /><femergenode /><femorphology /><feoffset /><fepointlight /><fespecularlighting /><fespotlight /><fetile /><feturbulence /><foreignobject /><glyphref /><lineargradient /><radialgradient /><textpath /></svg>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(36, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml1body1svg0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);

            var dochtml1body1svg0altGlyph0 = dochtml1body1svg0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1svg0altGlyph0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0altGlyph0.Attributes.Length);
            Assert.AreEqual("altGlyph", dochtml1body1svg0altGlyph0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0altGlyph0.NodeType);

            var dochtml1body1svg0altGlyphDef1 = dochtml1body1svg0.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1svg0altGlyphDef1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0altGlyphDef1.Attributes.Length);
            Assert.AreEqual("altGlyphDef", dochtml1body1svg0altGlyphDef1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0altGlyphDef1.NodeType);

            var dochtml1body1svg0altGlyphItem2 = dochtml1body1svg0.ChildNodes[2];
            Assert.AreEqual(0, dochtml1body1svg0altGlyphItem2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0altGlyphItem2.Attributes.Length);
            Assert.AreEqual("altGlyphItem", dochtml1body1svg0altGlyphItem2.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0altGlyphItem2.NodeType);

            var dochtml1body1svg0animateColor3 = dochtml1body1svg0.ChildNodes[3];
            Assert.AreEqual(0, dochtml1body1svg0animateColor3.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0animateColor3.Attributes.Length);
            Assert.AreEqual("animateColor", dochtml1body1svg0animateColor3.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0animateColor3.NodeType);

            var dochtml1body1svg0animateMotion4 = dochtml1body1svg0.ChildNodes[4];
            Assert.AreEqual(0, dochtml1body1svg0animateMotion4.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0animateMotion4.Attributes.Length);
            Assert.AreEqual("animateMotion", dochtml1body1svg0animateMotion4.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0animateMotion4.NodeType);

            var dochtml1body1svg0animateTransform5 = dochtml1body1svg0.ChildNodes[5];
            Assert.AreEqual(0, dochtml1body1svg0animateTransform5.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0animateTransform5.Attributes.Length);
            Assert.AreEqual("animateTransform", dochtml1body1svg0animateTransform5.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0animateTransform5.NodeType);

            var dochtml1body1svg0clipPath6 = dochtml1body1svg0.ChildNodes[6];
            Assert.AreEqual(0, dochtml1body1svg0clipPath6.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0clipPath6.Attributes.Length);
            Assert.AreEqual("clipPath", dochtml1body1svg0clipPath6.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0clipPath6.NodeType);

            var dochtml1body1svg0feBlend7 = dochtml1body1svg0.ChildNodes[7];
            Assert.AreEqual(0, dochtml1body1svg0feBlend7.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feBlend7.Attributes.Length);
            Assert.AreEqual("feBlend", dochtml1body1svg0feBlend7.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feBlend7.NodeType);

            var dochtml1body1svg0feColorMatrix8 = dochtml1body1svg0.ChildNodes[8];
            Assert.AreEqual(0, dochtml1body1svg0feColorMatrix8.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feColorMatrix8.Attributes.Length);
            Assert.AreEqual("feColorMatrix", dochtml1body1svg0feColorMatrix8.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feColorMatrix8.NodeType);

            var dochtml1body1svg0feComponentTransfer9 = dochtml1body1svg0.ChildNodes[9];
            Assert.AreEqual(0, dochtml1body1svg0feComponentTransfer9.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feComponentTransfer9.Attributes.Length);
            Assert.AreEqual("feComponentTransfer", dochtml1body1svg0feComponentTransfer9.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feComponentTransfer9.NodeType);

            var dochtml1body1svg0feComposite10 = dochtml1body1svg0.ChildNodes[10];
            Assert.AreEqual(0, dochtml1body1svg0feComposite10.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feComposite10.Attributes.Length);
            Assert.AreEqual("feComposite", dochtml1body1svg0feComposite10.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feComposite10.NodeType);

            var dochtml1body1svg0feConvolveMatrix11 = dochtml1body1svg0.ChildNodes[11];
            Assert.AreEqual(0, dochtml1body1svg0feConvolveMatrix11.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feConvolveMatrix11.Attributes.Length);
            Assert.AreEqual("feConvolveMatrix", dochtml1body1svg0feConvolveMatrix11.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feConvolveMatrix11.NodeType);

            var dochtml1body1svg0feDiffuseLighting12 = dochtml1body1svg0.ChildNodes[12];
            Assert.AreEqual(0, dochtml1body1svg0feDiffuseLighting12.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feDiffuseLighting12.Attributes.Length);
            Assert.AreEqual("feDiffuseLighting", dochtml1body1svg0feDiffuseLighting12.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feDiffuseLighting12.NodeType);

            var dochtml1body1svg0feDisplacementMap13 = dochtml1body1svg0.ChildNodes[13];
            Assert.AreEqual(0, dochtml1body1svg0feDisplacementMap13.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feDisplacementMap13.Attributes.Length);
            Assert.AreEqual("feDisplacementMap", dochtml1body1svg0feDisplacementMap13.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feDisplacementMap13.NodeType);

            var dochtml1body1svg0feDistantLight14 = dochtml1body1svg0.ChildNodes[14];
            Assert.AreEqual(0, dochtml1body1svg0feDistantLight14.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feDistantLight14.Attributes.Length);
            Assert.AreEqual("feDistantLight", dochtml1body1svg0feDistantLight14.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feDistantLight14.NodeType);

            var dochtml1body1svg0feFlood15 = dochtml1body1svg0.ChildNodes[15];
            Assert.AreEqual(0, dochtml1body1svg0feFlood15.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feFlood15.Attributes.Length);
            Assert.AreEqual("feFlood", dochtml1body1svg0feFlood15.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feFlood15.NodeType);

            var dochtml1body1svg0feFuncA16 = dochtml1body1svg0.ChildNodes[16];
            Assert.AreEqual(0, dochtml1body1svg0feFuncA16.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feFuncA16.Attributes.Length);
            Assert.AreEqual("feFuncA", dochtml1body1svg0feFuncA16.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feFuncA16.NodeType);

            var dochtml1body1svg0feFuncB17 = dochtml1body1svg0.ChildNodes[17];
            Assert.AreEqual(0, dochtml1body1svg0feFuncB17.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feFuncB17.Attributes.Length);
            Assert.AreEqual("feFuncB", dochtml1body1svg0feFuncB17.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feFuncB17.NodeType);

            var dochtml1body1svg0feFuncG18 = dochtml1body1svg0.ChildNodes[18];
            Assert.AreEqual(0, dochtml1body1svg0feFuncG18.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feFuncG18.Attributes.Length);
            Assert.AreEqual("feFuncG", dochtml1body1svg0feFuncG18.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feFuncG18.NodeType);

            var dochtml1body1svg0feFuncR19 = dochtml1body1svg0.ChildNodes[19];
            Assert.AreEqual(0, dochtml1body1svg0feFuncR19.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feFuncR19.Attributes.Length);
            Assert.AreEqual("feFuncR", dochtml1body1svg0feFuncR19.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feFuncR19.NodeType);

            var dochtml1body1svg0feGaussianBlur20 = dochtml1body1svg0.ChildNodes[20];
            Assert.AreEqual(0, dochtml1body1svg0feGaussianBlur20.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feGaussianBlur20.Attributes.Length);
            Assert.AreEqual("feGaussianBlur", dochtml1body1svg0feGaussianBlur20.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feGaussianBlur20.NodeType);

            var dochtml1body1svg0feImage21 = dochtml1body1svg0.ChildNodes[21];
            Assert.AreEqual(0, dochtml1body1svg0feImage21.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feImage21.Attributes.Length);
            Assert.AreEqual("feImage", dochtml1body1svg0feImage21.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feImage21.NodeType);

            var dochtml1body1svg0feMerge22 = dochtml1body1svg0.ChildNodes[22];
            Assert.AreEqual(0, dochtml1body1svg0feMerge22.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feMerge22.Attributes.Length);
            Assert.AreEqual("feMerge", dochtml1body1svg0feMerge22.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feMerge22.NodeType);

            var dochtml1body1svg0feMergeNode23 = dochtml1body1svg0.ChildNodes[23];
            Assert.AreEqual(0, dochtml1body1svg0feMergeNode23.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feMergeNode23.Attributes.Length);
            Assert.AreEqual("feMergeNode", dochtml1body1svg0feMergeNode23.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feMergeNode23.NodeType);

            var dochtml1body1svg0feMorphology24 = dochtml1body1svg0.ChildNodes[24];
            Assert.AreEqual(0, dochtml1body1svg0feMorphology24.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feMorphology24.Attributes.Length);
            Assert.AreEqual("feMorphology", dochtml1body1svg0feMorphology24.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feMorphology24.NodeType);

            var dochtml1body1svg0feOffset25 = dochtml1body1svg0.ChildNodes[25];
            Assert.AreEqual(0, dochtml1body1svg0feOffset25.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feOffset25.Attributes.Length);
            Assert.AreEqual("feOffset", dochtml1body1svg0feOffset25.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feOffset25.NodeType);

            var dochtml1body1svg0fePointLight26 = dochtml1body1svg0.ChildNodes[26];
            Assert.AreEqual(0, dochtml1body1svg0fePointLight26.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0fePointLight26.Attributes.Length);
            Assert.AreEqual("fePointLight", dochtml1body1svg0fePointLight26.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0fePointLight26.NodeType);

            var dochtml1body1svg0feSpecularLighting27 = dochtml1body1svg0.ChildNodes[27];
            Assert.AreEqual(0, dochtml1body1svg0feSpecularLighting27.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feSpecularLighting27.Attributes.Length);
            Assert.AreEqual("feSpecularLighting", dochtml1body1svg0feSpecularLighting27.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feSpecularLighting27.NodeType);

            var dochtml1body1svg0feSpotLight28 = dochtml1body1svg0.ChildNodes[28];
            Assert.AreEqual(0, dochtml1body1svg0feSpotLight28.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feSpotLight28.Attributes.Length);
            Assert.AreEqual("feSpotLight", dochtml1body1svg0feSpotLight28.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feSpotLight28.NodeType);

            var dochtml1body1svg0feTile29 = dochtml1body1svg0.ChildNodes[29];
            Assert.AreEqual(0, dochtml1body1svg0feTile29.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feTile29.Attributes.Length);
            Assert.AreEqual("feTile", dochtml1body1svg0feTile29.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feTile29.NodeType);

            var dochtml1body1svg0feTurbulence30 = dochtml1body1svg0.ChildNodes[30];
            Assert.AreEqual(0, dochtml1body1svg0feTurbulence30.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feTurbulence30.Attributes.Length);
            Assert.AreEqual("feTurbulence", dochtml1body1svg0feTurbulence30.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feTurbulence30.NodeType);

            var dochtml1body1svg0foreignObject31 = dochtml1body1svg0.ChildNodes[31];
            Assert.AreEqual(0, dochtml1body1svg0foreignObject31.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0foreignObject31.Attributes.Length);
            Assert.AreEqual("foreignObject", dochtml1body1svg0foreignObject31.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0foreignObject31.NodeType);

            var dochtml1body1svg0glyphRef32 = dochtml1body1svg0.ChildNodes[32];
            Assert.AreEqual(0, dochtml1body1svg0glyphRef32.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0glyphRef32.Attributes.Length);
            Assert.AreEqual("glyphRef", dochtml1body1svg0glyphRef32.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0glyphRef32.NodeType);

            var dochtml1body1svg0linearGradient33 = dochtml1body1svg0.ChildNodes[33];
            Assert.AreEqual(0, dochtml1body1svg0linearGradient33.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0linearGradient33.Attributes.Length);
            Assert.AreEqual("linearGradient", dochtml1body1svg0linearGradient33.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0linearGradient33.NodeType);

            var dochtml1body1svg0radialGradient34 = dochtml1body1svg0.ChildNodes[34];
            Assert.AreEqual(0, dochtml1body1svg0radialGradient34.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0radialGradient34.Attributes.Length);
            Assert.AreEqual("radialGradient", dochtml1body1svg0radialGradient34.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0radialGradient34.NodeType);

            var dochtml1body1svg0textPath35 = dochtml1body1svg0.ChildNodes[35];
            Assert.AreEqual(0, dochtml1body1svg0textPath35.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0textPath35.Attributes.Length);
            Assert.AreEqual("textPath", dochtml1body1svg0textPath35.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0textPath35.NodeType);
        }

        [TestMethod]
        public void SvgCheckTagCaseUppercaseModified()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><BODY><SVG><ALTGLYPH /><ALTGLYPHDEF /><ALTGLYPHITEM /><ANIMATECOLOR /><ANIMATEMOTION /><ANIMATETRANSFORM /><CLIPPATH /><FEBLEND /><FECOLORMATRIX /><FECOMPONENTTRANSFER /><FECOMPOSITE /><FECONVOLVEMATRIX /><FEDIFFUSELIGHTING /><FEDISPLACEMENTMAP /><FEDISTANTLIGHT /><FEFLOOD /><FEFUNCA /><FEFUNCB /><FEFUNCG /><FEFUNCR /><FEGAUSSIANBLUR /><FEIMAGE /><FEMERGE /><FEMERGENODE /><FEMORPHOLOGY /><FEOFFSET /><FEPOINTLIGHT /><FESPECULARLIGHTING /><FESPOTLIGHT /><FETILE /><FETURBULENCE /><FOREIGNOBJECT /><GLYPHREF /><LINEARGRADIENT /><RADIALGRADIENT /><TEXTPATH /></SVG>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(36, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml1body1svg0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);

            var dochtml1body1svg0altGlyph0 = dochtml1body1svg0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1svg0altGlyph0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0altGlyph0.Attributes.Length);
            Assert.AreEqual("altGlyph", dochtml1body1svg0altGlyph0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0altGlyph0.NodeType);

            var dochtml1body1svg0altGlyphDef1 = dochtml1body1svg0.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1svg0altGlyphDef1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0altGlyphDef1.Attributes.Length);
            Assert.AreEqual("altGlyphDef", dochtml1body1svg0altGlyphDef1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0altGlyphDef1.NodeType);

            var dochtml1body1svg0altGlyphItem2 = dochtml1body1svg0.ChildNodes[2];
            Assert.AreEqual(0, dochtml1body1svg0altGlyphItem2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0altGlyphItem2.Attributes.Length);
            Assert.AreEqual("altGlyphItem", dochtml1body1svg0altGlyphItem2.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0altGlyphItem2.NodeType);

            var dochtml1body1svg0animateColor3 = dochtml1body1svg0.ChildNodes[3];
            Assert.AreEqual(0, dochtml1body1svg0animateColor3.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0animateColor3.Attributes.Length);
            Assert.AreEqual("animateColor", dochtml1body1svg0animateColor3.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0animateColor3.NodeType);

            var dochtml1body1svg0animateMotion4 = dochtml1body1svg0.ChildNodes[4];
            Assert.AreEqual(0, dochtml1body1svg0animateMotion4.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0animateMotion4.Attributes.Length);
            Assert.AreEqual("animateMotion", dochtml1body1svg0animateMotion4.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0animateMotion4.NodeType);

            var dochtml1body1svg0animateTransform5 = dochtml1body1svg0.ChildNodes[5];
            Assert.AreEqual(0, dochtml1body1svg0animateTransform5.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0animateTransform5.Attributes.Length);
            Assert.AreEqual("animateTransform", dochtml1body1svg0animateTransform5.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0animateTransform5.NodeType);

            var dochtml1body1svg0clipPath6 = dochtml1body1svg0.ChildNodes[6];
            Assert.AreEqual(0, dochtml1body1svg0clipPath6.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0clipPath6.Attributes.Length);
            Assert.AreEqual("clipPath", dochtml1body1svg0clipPath6.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0clipPath6.NodeType);

            var dochtml1body1svg0feBlend7 = dochtml1body1svg0.ChildNodes[7];
            Assert.AreEqual(0, dochtml1body1svg0feBlend7.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feBlend7.Attributes.Length);
            Assert.AreEqual("feBlend", dochtml1body1svg0feBlend7.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feBlend7.NodeType);

            var dochtml1body1svg0feColorMatrix8 = dochtml1body1svg0.ChildNodes[8];
            Assert.AreEqual(0, dochtml1body1svg0feColorMatrix8.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feColorMatrix8.Attributes.Length);
            Assert.AreEqual("feColorMatrix", dochtml1body1svg0feColorMatrix8.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feColorMatrix8.NodeType);

            var dochtml1body1svg0feComponentTransfer9 = dochtml1body1svg0.ChildNodes[9];
            Assert.AreEqual(0, dochtml1body1svg0feComponentTransfer9.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feComponentTransfer9.Attributes.Length);
            Assert.AreEqual("feComponentTransfer", dochtml1body1svg0feComponentTransfer9.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feComponentTransfer9.NodeType);

            var dochtml1body1svg0feComposite10 = dochtml1body1svg0.ChildNodes[10];
            Assert.AreEqual(0, dochtml1body1svg0feComposite10.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feComposite10.Attributes.Length);
            Assert.AreEqual("feComposite", dochtml1body1svg0feComposite10.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feComposite10.NodeType);

            var dochtml1body1svg0feConvolveMatrix11 = dochtml1body1svg0.ChildNodes[11];
            Assert.AreEqual(0, dochtml1body1svg0feConvolveMatrix11.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feConvolveMatrix11.Attributes.Length);
            Assert.AreEqual("feConvolveMatrix", dochtml1body1svg0feConvolveMatrix11.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feConvolveMatrix11.NodeType);

            var dochtml1body1svg0feDiffuseLighting12 = dochtml1body1svg0.ChildNodes[12];
            Assert.AreEqual(0, dochtml1body1svg0feDiffuseLighting12.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feDiffuseLighting12.Attributes.Length);
            Assert.AreEqual("feDiffuseLighting", dochtml1body1svg0feDiffuseLighting12.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feDiffuseLighting12.NodeType);

            var dochtml1body1svg0feDisplacementMap13 = dochtml1body1svg0.ChildNodes[13];
            Assert.AreEqual(0, dochtml1body1svg0feDisplacementMap13.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feDisplacementMap13.Attributes.Length);
            Assert.AreEqual("feDisplacementMap", dochtml1body1svg0feDisplacementMap13.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feDisplacementMap13.NodeType);

            var dochtml1body1svg0feDistantLight14 = dochtml1body1svg0.ChildNodes[14];
            Assert.AreEqual(0, dochtml1body1svg0feDistantLight14.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feDistantLight14.Attributes.Length);
            Assert.AreEqual("feDistantLight", dochtml1body1svg0feDistantLight14.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feDistantLight14.NodeType);

            var dochtml1body1svg0feFlood15 = dochtml1body1svg0.ChildNodes[15];
            Assert.AreEqual(0, dochtml1body1svg0feFlood15.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feFlood15.Attributes.Length);
            Assert.AreEqual("feFlood", dochtml1body1svg0feFlood15.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feFlood15.NodeType);

            var dochtml1body1svg0feFuncA16 = dochtml1body1svg0.ChildNodes[16];
            Assert.AreEqual(0, dochtml1body1svg0feFuncA16.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feFuncA16.Attributes.Length);
            Assert.AreEqual("feFuncA", dochtml1body1svg0feFuncA16.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feFuncA16.NodeType);

            var dochtml1body1svg0feFuncB17 = dochtml1body1svg0.ChildNodes[17];
            Assert.AreEqual(0, dochtml1body1svg0feFuncB17.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feFuncB17.Attributes.Length);
            Assert.AreEqual("feFuncB", dochtml1body1svg0feFuncB17.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feFuncB17.NodeType);

            var dochtml1body1svg0feFuncG18 = dochtml1body1svg0.ChildNodes[18];
            Assert.AreEqual(0, dochtml1body1svg0feFuncG18.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feFuncG18.Attributes.Length);
            Assert.AreEqual("feFuncG", dochtml1body1svg0feFuncG18.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feFuncG18.NodeType);

            var dochtml1body1svg0feFuncR19 = dochtml1body1svg0.ChildNodes[19];
            Assert.AreEqual(0, dochtml1body1svg0feFuncR19.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feFuncR19.Attributes.Length);
            Assert.AreEqual("feFuncR", dochtml1body1svg0feFuncR19.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feFuncR19.NodeType);

            var dochtml1body1svg0feGaussianBlur20 = dochtml1body1svg0.ChildNodes[20];
            Assert.AreEqual(0, dochtml1body1svg0feGaussianBlur20.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feGaussianBlur20.Attributes.Length);
            Assert.AreEqual("feGaussianBlur", dochtml1body1svg0feGaussianBlur20.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feGaussianBlur20.NodeType);

            var dochtml1body1svg0feImage21 = dochtml1body1svg0.ChildNodes[21];
            Assert.AreEqual(0, dochtml1body1svg0feImage21.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feImage21.Attributes.Length);
            Assert.AreEqual("feImage", dochtml1body1svg0feImage21.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feImage21.NodeType);

            var dochtml1body1svg0feMerge22 = dochtml1body1svg0.ChildNodes[22];
            Assert.AreEqual(0, dochtml1body1svg0feMerge22.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feMerge22.Attributes.Length);
            Assert.AreEqual("feMerge", dochtml1body1svg0feMerge22.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feMerge22.NodeType);

            var dochtml1body1svg0feMergeNode23 = dochtml1body1svg0.ChildNodes[23];
            Assert.AreEqual(0, dochtml1body1svg0feMergeNode23.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feMergeNode23.Attributes.Length);
            Assert.AreEqual("feMergeNode", dochtml1body1svg0feMergeNode23.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feMergeNode23.NodeType);

            var dochtml1body1svg0feMorphology24 = dochtml1body1svg0.ChildNodes[24];
            Assert.AreEqual(0, dochtml1body1svg0feMorphology24.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feMorphology24.Attributes.Length);
            Assert.AreEqual("feMorphology", dochtml1body1svg0feMorphology24.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feMorphology24.NodeType);

            var dochtml1body1svg0feOffset25 = dochtml1body1svg0.ChildNodes[25];
            Assert.AreEqual(0, dochtml1body1svg0feOffset25.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feOffset25.Attributes.Length);
            Assert.AreEqual("feOffset", dochtml1body1svg0feOffset25.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feOffset25.NodeType);

            var dochtml1body1svg0fePointLight26 = dochtml1body1svg0.ChildNodes[26];
            Assert.AreEqual(0, dochtml1body1svg0fePointLight26.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0fePointLight26.Attributes.Length);
            Assert.AreEqual("fePointLight", dochtml1body1svg0fePointLight26.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0fePointLight26.NodeType);

            var dochtml1body1svg0feSpecularLighting27 = dochtml1body1svg0.ChildNodes[27];
            Assert.AreEqual(0, dochtml1body1svg0feSpecularLighting27.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feSpecularLighting27.Attributes.Length);
            Assert.AreEqual("feSpecularLighting", dochtml1body1svg0feSpecularLighting27.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feSpecularLighting27.NodeType);

            var dochtml1body1svg0feSpotLight28 = dochtml1body1svg0.ChildNodes[28];
            Assert.AreEqual(0, dochtml1body1svg0feSpotLight28.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feSpotLight28.Attributes.Length);
            Assert.AreEqual("feSpotLight", dochtml1body1svg0feSpotLight28.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feSpotLight28.NodeType);

            var dochtml1body1svg0feTile29 = dochtml1body1svg0.ChildNodes[29];
            Assert.AreEqual(0, dochtml1body1svg0feTile29.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feTile29.Attributes.Length);
            Assert.AreEqual("feTile", dochtml1body1svg0feTile29.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feTile29.NodeType);

            var dochtml1body1svg0feTurbulence30 = dochtml1body1svg0.ChildNodes[30];
            Assert.AreEqual(0, dochtml1body1svg0feTurbulence30.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feTurbulence30.Attributes.Length);
            Assert.AreEqual("feTurbulence", dochtml1body1svg0feTurbulence30.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feTurbulence30.NodeType);

            var dochtml1body1svg0foreignObject31 = dochtml1body1svg0.ChildNodes[31];
            Assert.AreEqual(0, dochtml1body1svg0foreignObject31.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0foreignObject31.Attributes.Length);
            Assert.AreEqual("foreignObject", dochtml1body1svg0foreignObject31.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0foreignObject31.NodeType);

            var dochtml1body1svg0glyphRef32 = dochtml1body1svg0.ChildNodes[32];
            Assert.AreEqual(0, dochtml1body1svg0glyphRef32.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0glyphRef32.Attributes.Length);
            Assert.AreEqual("glyphRef", dochtml1body1svg0glyphRef32.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0glyphRef32.NodeType);

            var dochtml1body1svg0linearGradient33 = dochtml1body1svg0.ChildNodes[33];
            Assert.AreEqual(0, dochtml1body1svg0linearGradient33.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0linearGradient33.Attributes.Length);
            Assert.AreEqual("linearGradient", dochtml1body1svg0linearGradient33.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0linearGradient33.NodeType);

            var dochtml1body1svg0radialGradient34 = dochtml1body1svg0.ChildNodes[34];
            Assert.AreEqual(0, dochtml1body1svg0radialGradient34.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0radialGradient34.Attributes.Length);
            Assert.AreEqual("radialGradient", dochtml1body1svg0radialGradient34.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0radialGradient34.NodeType);

            var dochtml1body1svg0textPath35 = dochtml1body1svg0.ChildNodes[35];
            Assert.AreEqual(0, dochtml1body1svg0textPath35.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0textPath35.Attributes.Length);
            Assert.AreEqual("textPath", dochtml1body1svg0textPath35.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0textPath35.NodeType);
        }

        [TestMethod]
        public void SvgSingleNodeInBody()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body><svg><solidColor /></svg>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml1body1svg0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);

            var dochtml1body1svg0solidcolor0 = dochtml1body1svg0.ChildNodes[0];
            Assert.AreEqual(0, dochtml1body1svg0solidcolor0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0solidcolor0.Attributes.Length);
            Assert.AreEqual("solidcolor", dochtml1body1svg0solidcolor0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0solidcolor0.NodeType);
        }
    }
}
