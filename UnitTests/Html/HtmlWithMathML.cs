using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AngleSharp;
using AngleSharp.DOM;

namespace UnitTests
{
    [TestClass]
    public class HtmlWithMathMLTests
    {
        [TestMethod]
        public void MathMLCheckAttributesCaseNormalUnchanged()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body><math attributeName='' attributeType='' baseFrequency='' baseProfile='' calcMode='' clipPathUnits='' contentScriptType='' contentStyleType='' diffuseConstant='' edgeMode='' externalResourcesRequired='' filterRes='' filterUnits='' glyphRef='' gradientTransform='' gradientUnits='' kernelMatrix='' kernelUnitLength='' keyPoints='' keySplines='' keyTimes='' lengthAdjust='' limitingConeAngle='' markerHeight='' markerUnits='' markerWidth='' maskContentUnits='' maskUnits='' numOctaves='' pathLength='' patternContentUnits='' patternTransform='' patternUnits='' pointsAtX='' pointsAtY='' pointsAtZ='' preserveAlpha='' preserveAspectRatio='' primitiveUnits='' refX='' refY='' repeatCount='' repeatDur='' requiredExtensions='' requiredFeatures='' specularConstant='' specularExponent='' spreadMethod='' startOffset='' stdDeviation='' stitchTiles='' surfaceScale='' systemLanguage='' tableValues='' targetX='' targetY='' textLength='' viewBox='' viewTarget='' xChannelSelector='' yChannelSelector='' zoomAndPan=''></math>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(62, dochtml1body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml1body1math0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);
            Assert.IsNotNull(dochtml1body1math0.Attributes["attributename"]);
            Assert.AreEqual("attributename", dochtml1body1math0.Attributes["attributename"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["attributename"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["attributetype"]);
            Assert.AreEqual("attributetype", dochtml1body1math0.Attributes["attributetype"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["attributetype"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["basefrequency"]);
            Assert.AreEqual("basefrequency", dochtml1body1math0.Attributes["basefrequency"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["basefrequency"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["baseprofile"]);
            Assert.AreEqual("baseprofile", dochtml1body1math0.Attributes["baseprofile"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["baseprofile"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["calcmode"]);
            Assert.AreEqual("calcmode", dochtml1body1math0.Attributes["calcmode"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["calcmode"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["clippathunits"]);
            Assert.AreEqual("clippathunits", dochtml1body1math0.Attributes["clippathunits"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["clippathunits"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["contentscripttype"]);
            Assert.AreEqual("contentscripttype", dochtml1body1math0.Attributes["contentscripttype"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["contentscripttype"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["contentstyletype"]);
            Assert.AreEqual("contentstyletype", dochtml1body1math0.Attributes["contentstyletype"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["contentstyletype"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["diffuseconstant"]);
            Assert.AreEqual("diffuseconstant", dochtml1body1math0.Attributes["diffuseconstant"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["diffuseconstant"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["edgemode"]);
            Assert.AreEqual("edgemode", dochtml1body1math0.Attributes["edgemode"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["edgemode"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["externalresourcesrequired"]);
            Assert.AreEqual("externalresourcesrequired", dochtml1body1math0.Attributes["externalresourcesrequired"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["externalresourcesrequired"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["filterres"]);
            Assert.AreEqual("filterres", dochtml1body1math0.Attributes["filterres"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["filterres"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["filterunits"]);
            Assert.AreEqual("filterunits", dochtml1body1math0.Attributes["filterunits"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["filterunits"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["glyphref"]);
            Assert.AreEqual("glyphref", dochtml1body1math0.Attributes["glyphref"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["glyphref"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["gradienttransform"]);
            Assert.AreEqual("gradienttransform", dochtml1body1math0.Attributes["gradienttransform"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["gradienttransform"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["gradientunits"]);
            Assert.AreEqual("gradientunits", dochtml1body1math0.Attributes["gradientunits"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["gradientunits"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["kernelmatrix"]);
            Assert.AreEqual("kernelmatrix", dochtml1body1math0.Attributes["kernelmatrix"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["kernelmatrix"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["kernelunitlength"]);
            Assert.AreEqual("kernelunitlength", dochtml1body1math0.Attributes["kernelunitlength"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["kernelunitlength"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["keypoints"]);
            Assert.AreEqual("keypoints", dochtml1body1math0.Attributes["keypoints"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["keypoints"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["keysplines"]);
            Assert.AreEqual("keysplines", dochtml1body1math0.Attributes["keysplines"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["keysplines"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["keytimes"]);
            Assert.AreEqual("keytimes", dochtml1body1math0.Attributes["keytimes"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["keytimes"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["lengthadjust"]);
            Assert.AreEqual("lengthadjust", dochtml1body1math0.Attributes["lengthadjust"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["lengthadjust"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["limitingconeangle"]);
            Assert.AreEqual("limitingconeangle", dochtml1body1math0.Attributes["limitingconeangle"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["limitingconeangle"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["markerheight"]);
            Assert.AreEqual("markerheight", dochtml1body1math0.Attributes["markerheight"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["markerheight"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["markerunits"]);
            Assert.AreEqual("markerunits", dochtml1body1math0.Attributes["markerunits"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["markerunits"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["markerwidth"]);
            Assert.AreEqual("markerwidth", dochtml1body1math0.Attributes["markerwidth"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["markerwidth"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["maskcontentunits"]);
            Assert.AreEqual("maskcontentunits", dochtml1body1math0.Attributes["maskcontentunits"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["maskcontentunits"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["maskunits"]);
            Assert.AreEqual("maskunits", dochtml1body1math0.Attributes["maskunits"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["maskunits"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["numoctaves"]);
            Assert.AreEqual("numoctaves", dochtml1body1math0.Attributes["numoctaves"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["numoctaves"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["pathlength"]);
            Assert.AreEqual("pathlength", dochtml1body1math0.Attributes["pathlength"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["pathlength"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["patterncontentunits"]);
            Assert.AreEqual("patterncontentunits", dochtml1body1math0.Attributes["patterncontentunits"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["patterncontentunits"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["patterntransform"]);
            Assert.AreEqual("patterntransform", dochtml1body1math0.Attributes["patterntransform"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["patterntransform"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["patternunits"]);
            Assert.AreEqual("patternunits", dochtml1body1math0.Attributes["patternunits"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["patternunits"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["pointsatx"]);
            Assert.AreEqual("pointsatx", dochtml1body1math0.Attributes["pointsatx"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["pointsatx"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["pointsaty"]);
            Assert.AreEqual("pointsaty", dochtml1body1math0.Attributes["pointsaty"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["pointsaty"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["pointsatz"]);
            Assert.AreEqual("pointsatz", dochtml1body1math0.Attributes["pointsatz"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["pointsatz"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["preservealpha"]);
            Assert.AreEqual("preservealpha", dochtml1body1math0.Attributes["preservealpha"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["preservealpha"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["preserveaspectratio"]);
            Assert.AreEqual("preserveaspectratio", dochtml1body1math0.Attributes["preserveaspectratio"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["preserveaspectratio"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["primitiveunits"]);
            Assert.AreEqual("primitiveunits", dochtml1body1math0.Attributes["primitiveunits"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["primitiveunits"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["refx"]);
            Assert.AreEqual("refx", dochtml1body1math0.Attributes["refx"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["refx"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["refy"]);
            Assert.AreEqual("refy", dochtml1body1math0.Attributes["refy"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["refy"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["repeatcount"]);
            Assert.AreEqual("repeatcount", dochtml1body1math0.Attributes["repeatcount"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["repeatcount"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["repeatdur"]);
            Assert.AreEqual("repeatdur", dochtml1body1math0.Attributes["repeatdur"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["repeatdur"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["requiredextensions"]);
            Assert.AreEqual("requiredextensions", dochtml1body1math0.Attributes["requiredextensions"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["requiredextensions"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["requiredfeatures"]);
            Assert.AreEqual("requiredfeatures", dochtml1body1math0.Attributes["requiredfeatures"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["requiredfeatures"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["specularconstant"]);
            Assert.AreEqual("specularconstant", dochtml1body1math0.Attributes["specularconstant"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["specularconstant"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["specularexponent"]);
            Assert.AreEqual("specularexponent", dochtml1body1math0.Attributes["specularexponent"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["specularexponent"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["spreadmethod"]);
            Assert.AreEqual("spreadmethod", dochtml1body1math0.Attributes["spreadmethod"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["spreadmethod"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["startoffset"]);
            Assert.AreEqual("startoffset", dochtml1body1math0.Attributes["startoffset"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["startoffset"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["stddeviation"]);
            Assert.AreEqual("stddeviation", dochtml1body1math0.Attributes["stddeviation"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["stddeviation"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["stitchtiles"]);
            Assert.AreEqual("stitchtiles", dochtml1body1math0.Attributes["stitchtiles"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["stitchtiles"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["surfacescale"]);
            Assert.AreEqual("surfacescale", dochtml1body1math0.Attributes["surfacescale"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["surfacescale"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["systemlanguage"]);
            Assert.AreEqual("systemlanguage", dochtml1body1math0.Attributes["systemlanguage"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["systemlanguage"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["tablevalues"]);
            Assert.AreEqual("tablevalues", dochtml1body1math0.Attributes["tablevalues"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["tablevalues"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["targetx"]);
            Assert.AreEqual("targetx", dochtml1body1math0.Attributes["targetx"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["targetx"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["targety"]);
            Assert.AreEqual("targety", dochtml1body1math0.Attributes["targety"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["targety"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["textlength"]);
            Assert.AreEqual("textlength", dochtml1body1math0.Attributes["textlength"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["textlength"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["viewbox"]);
            Assert.AreEqual("viewbox", dochtml1body1math0.Attributes["viewbox"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["viewbox"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["viewtarget"]);
            Assert.AreEqual("viewtarget", dochtml1body1math0.Attributes["viewtarget"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["viewtarget"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["xchannelselector"]);
            Assert.AreEqual("xchannelselector", dochtml1body1math0.Attributes["xchannelselector"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["xchannelselector"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["ychannelselector"]);
            Assert.AreEqual("ychannelselector", dochtml1body1math0.Attributes["ychannelselector"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["ychannelselector"].Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes["zoomandpan"]);
            Assert.AreEqual("zoomandpan", dochtml1body1math0.Attributes["zoomandpan"].Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes["zoomandpan"].Value);
        }

        [TestMethod]
        public void MathMLCheckTagCaseNormalUnchanged()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body><math><altGlyph /><altGlyphDef /><altGlyphItem /><animateColor /><animateMotion /><animateTransform /><clipPath /><feBlend /><feColorMatrix /><feComponentTransfer /><feComposite /><feConvolveMatrix /><feDiffuseLighting /><feDisplacementMap /><feDistantLight /><feFlood /><feFuncA /><feFuncB /><feFuncG /><feFuncR /><feGaussianBlur /><feImage /><feMerge /><feMergeNode /><feMorphology /><feOffset /><fePointLight /><feSpecularLighting /><feSpotLight /><feTile /><feTurbulence /><foreignObject /><glyphRef /><linearGradient /><radialGradient /><textPath /></math>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(36, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml1body1math0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

            var dochtml1body1math0altglyph0 = dochtml1body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1math0altglyph0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0altglyph0.Attributes.Count);
            Assert.AreEqual("altglyph", dochtml1body1math0altglyph0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0altglyph0.NodeType);

            var dochtml1body1math0altglyphdef1 = dochtml1body1math0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1math0altglyphdef1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0altglyphdef1.Attributes.Count);
            Assert.AreEqual("altglyphdef", dochtml1body1math0altglyphdef1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0altglyphdef1.NodeType);

            var dochtml1body1math0altglyphitem2 = dochtml1body1math0.ChildNodes[2] as Element;
            Assert.AreEqual(0, dochtml1body1math0altglyphitem2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0altglyphitem2.Attributes.Count);
            Assert.AreEqual("altglyphitem", dochtml1body1math0altglyphitem2.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0altglyphitem2.NodeType);

            var dochtml1body1math0animatecolor3 = dochtml1body1math0.ChildNodes[3] as Element;
            Assert.AreEqual(0, dochtml1body1math0animatecolor3.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0animatecolor3.Attributes.Count);
            Assert.AreEqual("animatecolor", dochtml1body1math0animatecolor3.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0animatecolor3.NodeType);

            var dochtml1body1math0animatemotion4 = dochtml1body1math0.ChildNodes[4] as Element;
            Assert.AreEqual(0, dochtml1body1math0animatemotion4.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0animatemotion4.Attributes.Count);
            Assert.AreEqual("animatemotion", dochtml1body1math0animatemotion4.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0animatemotion4.NodeType);

            var dochtml1body1math0animatetransform5 = dochtml1body1math0.ChildNodes[5] as Element;
            Assert.AreEqual(0, dochtml1body1math0animatetransform5.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0animatetransform5.Attributes.Count);
            Assert.AreEqual("animatetransform", dochtml1body1math0animatetransform5.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0animatetransform5.NodeType);

            var dochtml1body1math0clippath6 = dochtml1body1math0.ChildNodes[6] as Element;
            Assert.AreEqual(0, dochtml1body1math0clippath6.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0clippath6.Attributes.Count);
            Assert.AreEqual("clippath", dochtml1body1math0clippath6.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0clippath6.NodeType);

            var dochtml1body1math0feblend7 = dochtml1body1math0.ChildNodes[7] as Element;
            Assert.AreEqual(0, dochtml1body1math0feblend7.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0feblend7.Attributes.Count);
            Assert.AreEqual("feblend", dochtml1body1math0feblend7.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0feblend7.NodeType);

            var dochtml1body1math0fecolormatrix8 = dochtml1body1math0.ChildNodes[8] as Element;
            Assert.AreEqual(0, dochtml1body1math0fecolormatrix8.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0fecolormatrix8.Attributes.Count);
            Assert.AreEqual("fecolormatrix", dochtml1body1math0fecolormatrix8.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0fecolormatrix8.NodeType);

            var dochtml1body1math0fecomponenttransfer9 = dochtml1body1math0.ChildNodes[9] as Element;
            Assert.AreEqual(0, dochtml1body1math0fecomponenttransfer9.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0fecomponenttransfer9.Attributes.Count);
            Assert.AreEqual("fecomponenttransfer", dochtml1body1math0fecomponenttransfer9.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0fecomponenttransfer9.NodeType);

            var dochtml1body1math0fecomposite10 = dochtml1body1math0.ChildNodes[10] as Element;
            Assert.AreEqual(0, dochtml1body1math0fecomposite10.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0fecomposite10.Attributes.Count);
            Assert.AreEqual("fecomposite", dochtml1body1math0fecomposite10.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0fecomposite10.NodeType);

            var dochtml1body1math0feconvolvematrix11 = dochtml1body1math0.ChildNodes[11] as Element;
            Assert.AreEqual(0, dochtml1body1math0feconvolvematrix11.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0feconvolvematrix11.Attributes.Count);
            Assert.AreEqual("feconvolvematrix", dochtml1body1math0feconvolvematrix11.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0feconvolvematrix11.NodeType);

            var dochtml1body1math0fediffuselighting12 = dochtml1body1math0.ChildNodes[12] as Element;
            Assert.AreEqual(0, dochtml1body1math0fediffuselighting12.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0fediffuselighting12.Attributes.Count);
            Assert.AreEqual("fediffuselighting", dochtml1body1math0fediffuselighting12.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0fediffuselighting12.NodeType);

            var dochtml1body1math0fedisplacementmap13 = dochtml1body1math0.ChildNodes[13] as Element;
            Assert.AreEqual(0, dochtml1body1math0fedisplacementmap13.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0fedisplacementmap13.Attributes.Count);
            Assert.AreEqual("fedisplacementmap", dochtml1body1math0fedisplacementmap13.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0fedisplacementmap13.NodeType);

            var dochtml1body1math0fedistantlight14 = dochtml1body1math0.ChildNodes[14] as Element;
            Assert.AreEqual(0, dochtml1body1math0fedistantlight14.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0fedistantlight14.Attributes.Count);
            Assert.AreEqual("fedistantlight", dochtml1body1math0fedistantlight14.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0fedistantlight14.NodeType);

            var dochtml1body1math0feflood15 = dochtml1body1math0.ChildNodes[15] as Element;
            Assert.AreEqual(0, dochtml1body1math0feflood15.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0feflood15.Attributes.Count);
            Assert.AreEqual("feflood", dochtml1body1math0feflood15.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0feflood15.NodeType);

            var dochtml1body1math0fefunca16 = dochtml1body1math0.ChildNodes[16] as Element;
            Assert.AreEqual(0, dochtml1body1math0fefunca16.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0fefunca16.Attributes.Count);
            Assert.AreEqual("fefunca", dochtml1body1math0fefunca16.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0fefunca16.NodeType);

            var dochtml1body1math0fefuncb17 = dochtml1body1math0.ChildNodes[17] as Element;
            Assert.AreEqual(0, dochtml1body1math0fefuncb17.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0fefuncb17.Attributes.Count);
            Assert.AreEqual("fefuncb", dochtml1body1math0fefuncb17.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0fefuncb17.NodeType);

            var dochtml1body1math0fefuncg18 = dochtml1body1math0.ChildNodes[18] as Element;
            Assert.AreEqual(0, dochtml1body1math0fefuncg18.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0fefuncg18.Attributes.Count);
            Assert.AreEqual("fefuncg", dochtml1body1math0fefuncg18.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0fefuncg18.NodeType);

            var dochtml1body1math0fefuncr19 = dochtml1body1math0.ChildNodes[19] as Element;
            Assert.AreEqual(0, dochtml1body1math0fefuncr19.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0fefuncr19.Attributes.Count);
            Assert.AreEqual("fefuncr", dochtml1body1math0fefuncr19.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0fefuncr19.NodeType);

            var dochtml1body1math0fegaussianblur20 = dochtml1body1math0.ChildNodes[20] as Element;
            Assert.AreEqual(0, dochtml1body1math0fegaussianblur20.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0fegaussianblur20.Attributes.Count);
            Assert.AreEqual("fegaussianblur", dochtml1body1math0fegaussianblur20.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0fegaussianblur20.NodeType);

            var dochtml1body1math0feimage21 = dochtml1body1math0.ChildNodes[21] as Element;
            Assert.AreEqual(0, dochtml1body1math0feimage21.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0feimage21.Attributes.Count);
            Assert.AreEqual("feimage", dochtml1body1math0feimage21.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0feimage21.NodeType);

            var dochtml1body1math0femerge22 = dochtml1body1math0.ChildNodes[22] as Element;
            Assert.AreEqual(0, dochtml1body1math0femerge22.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0femerge22.Attributes.Count);
            Assert.AreEqual("femerge", dochtml1body1math0femerge22.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0femerge22.NodeType);

            var dochtml1body1math0femergenode23 = dochtml1body1math0.ChildNodes[23] as Element;
            Assert.AreEqual(0, dochtml1body1math0femergenode23.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0femergenode23.Attributes.Count);
            Assert.AreEqual("femergenode", dochtml1body1math0femergenode23.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0femergenode23.NodeType);

            var dochtml1body1math0femorphology24 = dochtml1body1math0.ChildNodes[24] as Element;
            Assert.AreEqual(0, dochtml1body1math0femorphology24.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0femorphology24.Attributes.Count);
            Assert.AreEqual("femorphology", dochtml1body1math0femorphology24.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0femorphology24.NodeType);

            var dochtml1body1math0feoffset25 = dochtml1body1math0.ChildNodes[25] as Element;
            Assert.AreEqual(0, dochtml1body1math0feoffset25.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0feoffset25.Attributes.Count);
            Assert.AreEqual("feoffset", dochtml1body1math0feoffset25.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0feoffset25.NodeType);

            var dochtml1body1math0fepointlight26 = dochtml1body1math0.ChildNodes[26] as Element;
            Assert.AreEqual(0, dochtml1body1math0fepointlight26.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0fepointlight26.Attributes.Count);
            Assert.AreEqual("fepointlight", dochtml1body1math0fepointlight26.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0fepointlight26.NodeType);

            var dochtml1body1math0fespecularlighting27 = dochtml1body1math0.ChildNodes[27] as Element;
            Assert.AreEqual(0, dochtml1body1math0fespecularlighting27.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0fespecularlighting27.Attributes.Count);
            Assert.AreEqual("fespecularlighting", dochtml1body1math0fespecularlighting27.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0fespecularlighting27.NodeType);

            var dochtml1body1math0fespotlight28 = dochtml1body1math0.ChildNodes[28] as Element;
            Assert.AreEqual(0, dochtml1body1math0fespotlight28.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0fespotlight28.Attributes.Count);
            Assert.AreEqual("fespotlight", dochtml1body1math0fespotlight28.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0fespotlight28.NodeType);

            var dochtml1body1math0fetile29 = dochtml1body1math0.ChildNodes[29] as Element;
            Assert.AreEqual(0, dochtml1body1math0fetile29.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0fetile29.Attributes.Count);
            Assert.AreEqual("fetile", dochtml1body1math0fetile29.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0fetile29.NodeType);

            var dochtml1body1math0feturbulence30 = dochtml1body1math0.ChildNodes[30] as Element;
            Assert.AreEqual(0, dochtml1body1math0feturbulence30.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0feturbulence30.Attributes.Count);
            Assert.AreEqual("feturbulence", dochtml1body1math0feturbulence30.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0feturbulence30.NodeType);

            var dochtml1body1math0foreignobject31 = dochtml1body1math0.ChildNodes[31] as Element;
            Assert.AreEqual(0, dochtml1body1math0foreignobject31.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0foreignobject31.Attributes.Count);
            Assert.AreEqual("foreignobject", dochtml1body1math0foreignobject31.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0foreignobject31.NodeType);

            var dochtml1body1math0glyphref32 = dochtml1body1math0.ChildNodes[32] as Element;
            Assert.AreEqual(0, dochtml1body1math0glyphref32.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0glyphref32.Attributes.Count);
            Assert.AreEqual("glyphref", dochtml1body1math0glyphref32.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0glyphref32.NodeType);

            var dochtml1body1math0lineargradient33 = dochtml1body1math0.ChildNodes[33] as Element;
            Assert.AreEqual(0, dochtml1body1math0lineargradient33.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0lineargradient33.Attributes.Count);
            Assert.AreEqual("lineargradient", dochtml1body1math0lineargradient33.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0lineargradient33.NodeType);

            var dochtml1body1math0radialgradient34 = dochtml1body1math0.ChildNodes[34] as Element;
            Assert.AreEqual(0, dochtml1body1math0radialgradient34.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0radialgradient34.Attributes.Count);
            Assert.AreEqual("radialgradient", dochtml1body1math0radialgradient34.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0radialgradient34.NodeType);

            var dochtml1body1math0textpath35 = dochtml1body1math0.ChildNodes[35] as Element;
            Assert.AreEqual(0, dochtml1body1math0textpath35.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0textpath35.Attributes.Count);
            Assert.AreEqual("textpath", dochtml1body1math0textpath35.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0textpath35.NodeType);
        }

        [TestMethod]
        public void MathMLSingleElement()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><math></math>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml1body1math0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);
        }

        [TestMethod]
        public void MathMLSingleElementInBody()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body><math></math>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml1body1math0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);
        }

        [TestMethod]
        public void MathMLElementWithDivAndObjectElements()
        {
            var doc = DocumentBuilder.Html(@"<math><mi><div><object><div><span></span></div></object></div></mi><mi>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1math0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml0body1math0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1math0.NodeType);

            var dochtml0body1math0mi0 = dochtml0body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mi0.Attributes.Count);
            Assert.AreEqual("mi", dochtml0body1math0mi0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mi0.NodeType);

            var dochtml0body1math0mi0div0 = dochtml0body1math0mi0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0mi0div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mi0div0.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1math0mi0div0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mi0div0.NodeType);

            var dochtml0body1math0mi0div0object0 = dochtml0body1math0mi0div0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0mi0div0object0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mi0div0object0.Attributes.Count);
            Assert.AreEqual("object", dochtml0body1math0mi0div0object0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mi0div0object0.NodeType);

            var dochtml0body1math0mi0div0object0div0 = dochtml0body1math0mi0div0object0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0mi0div0object0div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mi0div0object0div0.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1math0mi0div0object0div0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mi0div0object0div0.NodeType);

            var dochtml0body1math0mi0div0object0div0span0 = dochtml0body1math0mi0div0object0div0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1math0mi0div0object0div0span0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mi0div0object0div0span0.Attributes.Count);
            Assert.AreEqual("span", dochtml0body1math0mi0div0object0div0span0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mi0div0object0div0span0.NodeType);

            var dochtml0body1math0mi1 = dochtml0body1math0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1math0mi1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mi1.Attributes.Count);
            Assert.AreEqual("mi", dochtml0body1math0mi1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mi1.NodeType);
        }

        [TestMethod]
        public void MathMLElementWithSvgChild()
        {
            var doc = DocumentBuilder.Html(@"<math><mi><svg><foreignObject><div><div></div></div></foreignObject></svg></mi><mi>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1math0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml0body1math0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1math0.NodeType);

            var dochtml0body1math0mi0 = dochtml0body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mi0.Attributes.Count);
            Assert.AreEqual("mi", dochtml0body1math0mi0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mi0.NodeType);

            var dochtml0body1math0mi0svg0 = dochtml0body1math0mi0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0mi0svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mi0svg0.Attributes.Count);
            Assert.AreEqual("svg", dochtml0body1math0mi0svg0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mi0svg0.NodeType);

            var dochtml0body1math0mi0svg0foreignObject0 = dochtml0body1math0mi0svg0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0mi0svg0foreignObject0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mi0svg0foreignObject0.Attributes.Count);
            Assert.AreEqual("foreignObject", dochtml0body1math0mi0svg0foreignObject0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mi0svg0foreignObject0.NodeType);

            var dochtml0body1math0mi0svg0foreignObject0div0 = dochtml0body1math0mi0svg0foreignObject0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0mi0svg0foreignObject0div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mi0svg0foreignObject0div0.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1math0mi0svg0foreignObject0div0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mi0svg0foreignObject0div0.NodeType);

            var dochtml0body1math0mi0svg0foreignObject0div0div0 = dochtml0body1math0mi0svg0foreignObject0div0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1math0mi0svg0foreignObject0div0div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mi0svg0foreignObject0div0div0.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1math0mi0svg0foreignObject0div0div0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mi0svg0foreignObject0div0div0.NodeType);

            var dochtml0body1math0mi1 = dochtml0body1math0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1math0mi1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mi1.Attributes.Count);
            Assert.AreEqual("mi", dochtml0body1math0mi1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mi1.NodeType);
        }

        [TestMethod]
        public void MathMLSingleElementWithChild()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><math><mi>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml1body1math0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

            var dochtml1body1math0mi0 = dochtml1body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mi0.Attributes.Count);
            Assert.AreEqual("mi", dochtml1body1math0mi0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi0.NodeType);
        }

        [TestMethod]
        public void MathMLWithMiAndMglyphElements()
        {
            var doc = DocumentBuilder.Html(@"<math><mi><mglyph>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1math0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml0body1math0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1math0.NodeType);

            var dochtml0body1math0mi0 = dochtml0body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mi0.Attributes.Count);
            Assert.AreEqual("mi", dochtml0body1math0mi0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mi0.NodeType);

            var dochtml0body1math0mi0mglyph0 = dochtml0body1math0mi0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1math0mi0mglyph0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mi0mglyph0.Attributes.Count);
            Assert.AreEqual("mglyph", dochtml0body1math0mi0mglyph0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mi0mglyph0.NodeType);
        }

        [TestMethod]
        public void MathMLWithMiAndMalignmarkElements()
        {
            var doc = DocumentBuilder.Html(@"<math><mi><malignmark>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1math0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml0body1math0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1math0.NodeType);

            var dochtml0body1math0mi0 = dochtml0body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mi0.Attributes.Count);
            Assert.AreEqual("mi", dochtml0body1math0mi0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mi0.NodeType);

            var dochtml0body1math0mi0malignmark0 = dochtml0body1math0mi0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1math0mi0malignmark0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mi0malignmark0.Attributes.Count);
            Assert.AreEqual("malignmark", dochtml0body1math0mi0malignmark0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mi0malignmark0.NodeType);
        }

        [TestMethod]
        public void MathMLWithMoAndMglyphElements()
        {
            var doc = DocumentBuilder.Html(@"<math><mo><mglyph>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1math0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml0body1math0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1math0.NodeType);

            var dochtml0body1math0mo0 = dochtml0body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0mo0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mo0.Attributes.Count);
            Assert.AreEqual("mo", dochtml0body1math0mo0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mo0.NodeType);

            var dochtml0body1math0mo0mglyph0 = dochtml0body1math0mo0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1math0mo0mglyph0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mo0mglyph0.Attributes.Count);
            Assert.AreEqual("mglyph", dochtml0body1math0mo0mglyph0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mo0mglyph0.NodeType);
        }

        [TestMethod]
        public void MathMLWithMoAndMalignmarkElements()
        {
            var doc = DocumentBuilder.Html(@"<math><mo><malignmark>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1math0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml0body1math0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1math0.NodeType);

            var dochtml0body1math0mo0 = dochtml0body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0mo0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mo0.Attributes.Count);
            Assert.AreEqual("mo", dochtml0body1math0mo0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mo0.NodeType);

            var dochtml0body1math0mo0malignmark0 = dochtml0body1math0mo0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1math0mo0malignmark0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mo0malignmark0.Attributes.Count);
            Assert.AreEqual("malignmark", dochtml0body1math0mo0malignmark0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mo0malignmark0.NodeType);
        }

        [TestMethod]
        public void MathMLWithMnAndMglyphElements()
        {
            var doc = DocumentBuilder.Html(@"<math><mn><mglyph>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1math0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml0body1math0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1math0.NodeType);

            var dochtml0body1math0mn0 = dochtml0body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0mn0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mn0.Attributes.Count);
            Assert.AreEqual("mn", dochtml0body1math0mn0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mn0.NodeType);

            var dochtml0body1math0mn0mglyph0 = dochtml0body1math0mn0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1math0mn0mglyph0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mn0mglyph0.Attributes.Count);
            Assert.AreEqual("mglyph", dochtml0body1math0mn0mglyph0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mn0mglyph0.NodeType);
        }

        [TestMethod]
        public void MathMLWithMnAndMalignmarkElements()
        {
            var doc = DocumentBuilder.Html(@"<math><mn><malignmark>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1math0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml0body1math0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1math0.NodeType);

            var dochtml0body1math0mn0 = dochtml0body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0mn0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mn0.Attributes.Count);
            Assert.AreEqual("mn", dochtml0body1math0mn0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mn0.NodeType);

            var dochtml0body1math0mn0malignmark0 = dochtml0body1math0mn0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1math0mn0malignmark0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mn0malignmark0.Attributes.Count);
            Assert.AreEqual("malignmark", dochtml0body1math0mn0malignmark0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mn0malignmark0.NodeType);
        }

        [TestMethod]
        public void MathMLWithMsAndMglyphElements()
        {
            var doc = DocumentBuilder.Html(@"<math><ms><mglyph>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1math0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml0body1math0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1math0.NodeType);

            var dochtml0body1math0ms0 = dochtml0body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0ms0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0ms0.Attributes.Count);
            Assert.AreEqual("ms", dochtml0body1math0ms0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1math0ms0.NodeType);

            var dochtml0body1math0ms0mglyph0 = dochtml0body1math0ms0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1math0ms0mglyph0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0ms0mglyph0.Attributes.Count);
            Assert.AreEqual("mglyph", dochtml0body1math0ms0mglyph0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1math0ms0mglyph0.NodeType);
        }

        [TestMethod]
        public void MathMLWithMsAndMalignmarkElements()
        {
            var doc = DocumentBuilder.Html(@"<math><ms><malignmark>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1math0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml0body1math0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1math0.NodeType);

            var dochtml0body1math0ms0 = dochtml0body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0ms0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0ms0.Attributes.Count);
            Assert.AreEqual("ms", dochtml0body1math0ms0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1math0ms0.NodeType);

            var dochtml0body1math0ms0malignmark0 = dochtml0body1math0ms0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1math0ms0malignmark0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0ms0malignmark0.Attributes.Count);
            Assert.AreEqual("malignmark", dochtml0body1math0ms0malignmark0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1math0ms0malignmark0.NodeType);
        }

        [TestMethod]
        public void MathMLWithMtextAndMglyphElements()
        {
            var doc = DocumentBuilder.Html(@"<math><mtext><mglyph>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1math0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml0body1math0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1math0.NodeType);

            var dochtml0body1math0mtext0 = dochtml0body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0mtext0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mtext0.Attributes.Count);
            Assert.AreEqual("mtext", dochtml0body1math0mtext0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mtext0.NodeType);

            var dochtml0body1math0mtext0mglyph0 = dochtml0body1math0mtext0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1math0mtext0mglyph0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mtext0mglyph0.Attributes.Count);
            Assert.AreEqual("mglyph", dochtml0body1math0mtext0mglyph0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mtext0mglyph0.NodeType);
        }

        [TestMethod]
        public void MathMLWithMtextAndMalignmarkElements()
        {
            var doc = DocumentBuilder.Html(@"<math><mtext><malignmark>");

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1math0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml0body1math0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1math0.NodeType);

            var dochtml0body1math0mtext0 = dochtml0body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0mtext0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mtext0.Attributes.Count);
            Assert.AreEqual("mtext", dochtml0body1math0mtext0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mtext0.NodeType);

            var dochtml0body1math0mtext0malignmark0 = dochtml0body1math0mtext0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1math0mtext0malignmark0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mtext0malignmark0.Attributes.Count);
            Assert.AreEqual("malignmark", dochtml0body1math0mtext0malignmark0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mtext0malignmark0.NodeType);
        }

        [TestMethod]
        public void MathMLAnnotationXmlWithSvgInside()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><math><annotation-xml><svg><u>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml1body1math0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);
            var dochtml1body1math0annotationxml0 = dochtml1body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1math0annotationxml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0annotationxml0.Attributes.Count);
            Assert.AreEqual("annotation-xml", dochtml1body1math0annotationxml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0annotationxml0.NodeType);

            var dochtml1body1math0annotationxml0svg0 = dochtml1body1math0annotationxml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1math0annotationxml0svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0annotationxml0svg0.Attributes.Count);
            Assert.AreEqual("svg", dochtml1body1math0annotationxml0svg0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0annotationxml0svg0.NodeType);

            var dochtml1body1u1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1u1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1u1.Attributes.Count);
            Assert.AreEqual("u", dochtml1body1u1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1u1.NodeType);
        }

        [TestMethod]
        public void MathMLElementInSelect()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body><select><math></math></select>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1select0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1select0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1select0.Attributes.Count);
            Assert.AreEqual("select", dochtml1body1select0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1select0.NodeType);
        }

        [TestMethod]
        public void MathMLInOptionOfSelect()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body><select><option><math></math></option></select>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1select0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1select0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1select0.Attributes.Count);
            Assert.AreEqual("select", dochtml1body1select0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1select0.NodeType);

            var dochtml1body1select0option0 = dochtml1body1select0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1select0option0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1select0option0.Attributes.Count);
            Assert.AreEqual("option", dochtml1body1select0option0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1select0option0.NodeType);
        }

        [TestMethod]
        public void MathMLInTable()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body><table><math></math></table>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml1body1math0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

            var dochtml1body1table1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table1.Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table1.NodeType);
        }

        [TestMethod]
        public void MathMLWithChildInTable()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body><table><math><mi>foo</mi></math></table>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml1body1math0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

            var dochtml1body1math0mi0 = dochtml1body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mi0.Attributes.Count);
            Assert.AreEqual("mi", dochtml1body1math0mi0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi0.NodeType);

            var dochtml1body1math0mi0Text0 = dochtml1body1math0mi0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1math0mi0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1math0mi0Text0.TextContent);

            var dochtml1body1table1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table1.Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table1.NodeType);
        }

        [TestMethod]
        public void MathMLWithChildrenInTable()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body><table><math><mi>foo</mi><mi>bar</mi></math></table>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml1body1math0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

            var dochtml1body1math0mi0 = dochtml1body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mi0.Attributes.Count);
            Assert.AreEqual("mi", dochtml1body1math0mi0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi0.NodeType);

            var dochtml1body1math0mi0Text0 = dochtml1body1math0mi0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1math0mi0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1math0mi0Text0.TextContent);

            var dochtml1body1math0mi1 = dochtml1body1math0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1math0mi1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mi1.Attributes.Count);
            Assert.AreEqual("mi", dochtml1body1math0mi1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi1.NodeType);

            var dochtml1body1math0mi1Text0 = dochtml1body1math0mi1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1math0mi1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1math0mi1Text0.TextContent);

            var dochtml1body1table1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table1.Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table1.NodeType);
        }

        [TestMethod]
        public void MathMLInTBodySectionOfTable()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body><table><tbody><math><mi>foo</mi><mi>bar</mi></math></tbody></table>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml1body1math0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

            var dochtml1body1math0mi0 = dochtml1body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mi0.Attributes.Count);
            Assert.AreEqual("mi", dochtml1body1math0mi0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi0.NodeType);

            var dochtml1body1math0mi0Text0 = dochtml1body1math0mi0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1math0mi0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1math0mi0Text0.TextContent);

            var dochtml1body1math0mi1 = dochtml1body1math0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1math0mi1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mi1.Attributes.Count);
            Assert.AreEqual("mi", dochtml1body1math0mi1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi1.NodeType);

            var dochtml1body1math0mi1Text0 = dochtml1body1math0mi1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1math0mi1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1math0mi1Text0.TextContent);

            var dochtml1body1table1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table1.Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table1.NodeType);

            var dochtml1body1table1tbody0 = dochtml1body1table1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1table1tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table1tbody0.Attributes.Count);
            Assert.AreEqual("tbody", dochtml1body1table1tbody0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table1tbody0.NodeType);
        }

        [TestMethod]
        public void MathMLInRowOfTable()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body><table><tbody><tr><math><mi>foo</mi><mi>bar</mi></math></tr></tbody></table>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml1body1math0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

            var dochtml1body1math0mi0 = dochtml1body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mi0.Attributes.Count);
            Assert.AreEqual("mi", dochtml1body1math0mi0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi0.NodeType);

            var dochtml1body1math0mi0Text0 = dochtml1body1math0mi0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1math0mi0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1math0mi0Text0.TextContent);

            var dochtml1body1math0mi1 = dochtml1body1math0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1math0mi1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mi1.Attributes.Count);
            Assert.AreEqual("mi", dochtml1body1math0mi1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi1.NodeType);

            var dochtml1body1math0mi1Text0 = dochtml1body1math0mi1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1math0mi1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1math0mi1Text0.TextContent);

            var dochtml1body1table1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table1.Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table1.NodeType);

            var dochtml1body1table1tbody0 = dochtml1body1table1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table1tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table1tbody0.Attributes.Count);
            Assert.AreEqual("tbody", dochtml1body1table1tbody0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table1tbody0.NodeType);

            var dochtml1body1table1tbody0tr0 = dochtml1body1table1tbody0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1table1tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table1tbody0tr0.Attributes.Count);
            Assert.AreEqual("tr", dochtml1body1table1tbody0tr0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table1tbody0tr0.NodeType);
        }

        [TestMethod]
        public void MathMLInCellOfTable()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body><table><tbody><tr><td><math><mi>foo</mi><mi>bar</mi></math></td></tr></tbody></table>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1table0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0.Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0.NodeType);

            var dochtml1body1table0tbody0 = dochtml1body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0.Attributes.Count);
            Assert.AreEqual("tbody", dochtml1body1table0tbody0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0.NodeType);

            var dochtml1body1table0tbody0tr0 = dochtml1body1table0tbody0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0.Attributes.Count);
            Assert.AreEqual("tr", dochtml1body1table0tbody0tr0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0.NodeType);

            var dochtml1body1table0tbody0tr0td0 = dochtml1body1table0tbody0tr0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0.Attributes.Count);
            Assert.AreEqual("td", dochtml1body1table0tbody0tr0td0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0.NodeType);

            var dochtml1body1table0tbody0tr0td0math0 = dochtml1body1table0tbody0tr0td0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1table0tbody0tr0td0math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0math0.Attributes.Count);
            Assert.AreEqual("math", dochtml1body1table0tbody0tr0td0math0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0math0.NodeType);

            var dochtml1body1table0tbody0tr0td0math0mi0 = dochtml1body1table0tbody0tr0td0math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0math0mi0.Attributes.Count);
            Assert.AreEqual("mi", dochtml1body1table0tbody0tr0td0math0mi0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0math0mi0.NodeType);

            var dochtml1body1table0tbody0tr0td0math0mi0Text0 = dochtml1body1table0tbody0tr0td0math0mi0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0tbody0tr0td0math0mi0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1table0tbody0tr0td0math0mi0Text0.TextContent);

            var dochtml1body1table0tbody0tr0td0math0mi1 = dochtml1body1table0tbody0tr0td0math0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0math0mi1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0math0mi1.Attributes.Count);
            Assert.AreEqual("mi", dochtml1body1table0tbody0tr0td0math0mi1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0math0mi1.NodeType);

            var dochtml1body1table0tbody0tr0td0math0mi1Text0 = dochtml1body1table0tbody0tr0td0math0mi1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0tbody0tr0td0math0mi1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1table0tbody0tr0td0math0mi1Text0.TextContent);
        }

        [TestMethod]
        public void MathMLCompleteExampleInTable()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body><table><tbody><tr><td><math><mi>foo</mi><mi>bar</mi></math><p>baz</td></tr></tbody></table>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1table0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0.Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0.NodeType);

            var dochtml1body1table0tbody0 = dochtml1body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0.Attributes.Count);
            Assert.AreEqual("tbody", dochtml1body1table0tbody0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0.NodeType);

            var dochtml1body1table0tbody0tr0 = dochtml1body1table0tbody0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0.Attributes.Count);
            Assert.AreEqual("tr", dochtml1body1table0tbody0tr0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0.NodeType);

            var dochtml1body1table0tbody0tr0td0 = dochtml1body1table0tbody0tr0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1table0tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0.Attributes.Count);
            Assert.AreEqual("td", dochtml1body1table0tbody0tr0td0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0.NodeType);

            var dochtml1body1table0tbody0tr0td0math0 = dochtml1body1table0tbody0tr0td0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1table0tbody0tr0td0math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0math0.Attributes.Count);
            Assert.AreEqual("math", dochtml1body1table0tbody0tr0td0math0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0math0.NodeType);

            var dochtml1body1table0tbody0tr0td0math0mi0 = dochtml1body1table0tbody0tr0td0math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0math0mi0.Attributes.Count);
            Assert.AreEqual("mi", dochtml1body1table0tbody0tr0td0math0mi0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0math0mi0.NodeType);

            var dochtml1body1table0tbody0tr0td0math0mi0Text0 = dochtml1body1table0tbody0tr0td0math0mi0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0tbody0tr0td0math0mi0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1table0tbody0tr0td0math0mi0Text0.TextContent);

            var dochtml1body1table0tbody0tr0td0math0mi1 = dochtml1body1table0tbody0tr0td0math0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0math0mi1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0math0mi1.Attributes.Count);
            Assert.AreEqual("mi", dochtml1body1table0tbody0tr0td0math0mi1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0math0mi1.NodeType);

            var dochtml1body1table0tbody0tr0td0math0mi1Text0 = dochtml1body1table0tbody0tr0td0math0mi1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0tbody0tr0td0math0mi1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1table0tbody0tr0td0math0mi1Text0.TextContent);

            var dochtml1body1table0tbody0tr0td0p1 = dochtml1body1table0tbody0tr0td0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0p1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0p1.Attributes.Count);
            Assert.AreEqual("p", dochtml1body1table0tbody0tr0td0p1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0p1.NodeType);

            var dochtml1body1table0tbody0tr0td0p1Text0 = dochtml1body1table0tbody0tr0td0p1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0tbody0tr0td0p1Text0.NodeType);
            Assert.AreEqual("baz", dochtml1body1table0tbody0tr0td0p1Text0.TextContent);
        }

        [TestMethod]
        public void MathMLInCaptionOfTable()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body><table><caption><math><mi>foo</mi><mi>bar</mi></math><p>baz</caption></table>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1table0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0.Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0.NodeType);

            var dochtml1body1table0caption0 = dochtml1body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1table0caption0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0.Attributes.Count);
            Assert.AreEqual("caption", dochtml1body1table0caption0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0.NodeType);

            var dochtml1body1table0caption0math0 = dochtml1body1table0caption0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1table0caption0math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0math0.Attributes.Count);
            Assert.AreEqual("math", dochtml1body1table0caption0math0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0math0.NodeType);

            var dochtml1body1table0caption0math0mi0 = dochtml1body1table0caption0math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0caption0math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0math0mi0.Attributes.Count);
            Assert.AreEqual("mi", dochtml1body1table0caption0math0mi0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0math0mi0.NodeType);

            var dochtml1body1table0caption0math0mi0Text0 = dochtml1body1table0caption0math0mi0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0caption0math0mi0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1table0caption0math0mi0Text0.TextContent);

            var dochtml1body1table0caption0math0mi1 = dochtml1body1table0caption0math0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1table0caption0math0mi1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0math0mi1.Attributes.Count);
            Assert.AreEqual("mi", dochtml1body1table0caption0math0mi1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0math0mi1.NodeType);

            var dochtml1body1table0caption0math0mi1Text0 = dochtml1body1table0caption0math0mi1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0caption0math0mi1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1table0caption0math0mi1Text0.TextContent);

            var dochtml1body1table0caption0p1 = dochtml1body1table0caption0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1table0caption0p1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0p1.Attributes.Count);
            Assert.AreEqual("p", dochtml1body1table0caption0p1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0p1.NodeType);

            var dochtml1body1table0caption0p1Text0 = dochtml1body1table0caption0p1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0caption0p1Text0.NodeType);
            Assert.AreEqual("baz", dochtml1body1table0caption0p1Text0.TextContent);
        }

        [TestMethod]
        public void MathMLImplicitlyClosedInTable()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body><table><caption><math><mi>foo</mi><mi>bar</mi><p>baz</table><p>quux");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1table0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0.Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0.NodeType);

            var dochtml1body1table0caption0 = dochtml1body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1table0caption0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0.Attributes.Count);
            Assert.AreEqual("caption", dochtml1body1table0caption0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0.NodeType);

            var dochtml1body1table0caption0math0 = dochtml1body1table0caption0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1table0caption0math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0math0.Attributes.Count);
            Assert.AreEqual("math", dochtml1body1table0caption0math0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0math0.NodeType);

            var dochtml1body1table0caption0math0mi0 = dochtml1body1table0caption0math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0caption0math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0math0mi0.Attributes.Count);
            Assert.AreEqual("mi", dochtml1body1table0caption0math0mi0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0math0mi0.NodeType);

            var dochtml1body1table0caption0math0mi0Text0 = dochtml1body1table0caption0math0mi0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0caption0math0mi0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1table0caption0math0mi0Text0.TextContent);

            var dochtml1body1table0caption0math0mi1 = dochtml1body1table0caption0math0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1table0caption0math0mi1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0math0mi1.Attributes.Count);
            Assert.AreEqual("mi", dochtml1body1table0caption0math0mi1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0math0mi1.NodeType);

            var dochtml1body1table0caption0math0mi1Text0 = dochtml1body1table0caption0math0mi1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0caption0math0mi1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1table0caption0math0mi1Text0.TextContent);

            var dochtml1body1table0caption0p1 = dochtml1body1table0caption0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1table0caption0p1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0p1.Attributes.Count);
            Assert.AreEqual("p", dochtml1body1table0caption0p1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0p1.NodeType);

            var dochtml1body1table0caption0p1Text0 = dochtml1body1table0caption0p1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0caption0p1Text0.NodeType);
            Assert.AreEqual("baz", dochtml1body1table0caption0p1Text0.TextContent);

            var dochtml1body1p1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1p1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1p1.Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1p1.NodeType);

            var dochtml1body1p1Text0 = dochtml1body1p1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1p1Text0.NodeType);
            Assert.AreEqual("quux", dochtml1body1p1Text0.TextContent);
        }

        [TestMethod]
        public void MathMLInCaptionImplicitlyClosed()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body><table><caption><math><mi>foo</mi><mi>bar</mi>baz</table><p>quux");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1table0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0.Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0.NodeType);

            var dochtml1body1table0caption0 = dochtml1body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0caption0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0.Attributes.Count);
            Assert.AreEqual("caption", dochtml1body1table0caption0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0.NodeType);

            var dochtml1body1table0caption0math0 = dochtml1body1table0caption0.ChildNodes[0] as Element;
            Assert.AreEqual(3, dochtml1body1table0caption0math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0math0.Attributes.Count);
            Assert.AreEqual("math", dochtml1body1table0caption0math0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0math0.NodeType);

            var dochtml1body1table0caption0math0mi0 = dochtml1body1table0caption0math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0caption0math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0math0mi0.Attributes.Count);
            Assert.AreEqual("mi", dochtml1body1table0caption0math0mi0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0math0mi0.NodeType);

            var dochtml1body1table0caption0math0mi0Text0 = dochtml1body1table0caption0math0mi0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0caption0math0mi0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1table0caption0math0mi0Text0.TextContent);

            var dochtml1body1table0caption0math0mi1 = dochtml1body1table0caption0math0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1table0caption0math0mi1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0math0mi1.Attributes.Count);
            Assert.AreEqual("mi", dochtml1body1table0caption0math0mi1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0math0mi1.NodeType);

            var dochtml1body1table0caption0math0mi1Text0 = dochtml1body1table0caption0math0mi1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0caption0math0mi1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1table0caption0math0mi1Text0.TextContent);

            var dochtml1body1table0caption0math0Text2 = dochtml1body1table0caption0math0.ChildNodes[2];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0caption0math0Text2.NodeType);
            Assert.AreEqual("baz", dochtml1body1table0caption0math0Text2.TextContent);

            var dochtml1body1p1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1p1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1p1.Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1p1.NodeType);

            var dochtml1body1p1Text0 = dochtml1body1p1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1p1Text0.NodeType);
            Assert.AreEqual("quux", dochtml1body1p1Text0.TextContent);
        }

        [TestMethod]
        public void MathMLInColgroupOfTable()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body><table><colgroup><math><mi>foo</mi><mi>bar</mi><p>baz</table><p>quux");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(4, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml1body1math0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

            var dochtml1body1math0mi0 = dochtml1body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mi0.Attributes.Count);
            Assert.AreEqual("mi", dochtml1body1math0mi0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi0.NodeType);

            var dochtml1body1math0mi0Text0 = dochtml1body1math0mi0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1math0mi0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1math0mi0Text0.TextContent);

            var dochtml1body1math0mi1 = dochtml1body1math0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1math0mi1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mi1.Attributes.Count);
            Assert.AreEqual("mi", dochtml1body1math0mi1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi1.NodeType);

            var dochtml1body1math0mi1Text0 = dochtml1body1math0mi1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1math0mi1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1math0mi1Text0.TextContent);

            var dochtml1body1p1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1p1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1p1.Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1p1.NodeType);

            var dochtml1body1p1Text0 = dochtml1body1p1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1p1Text0.NodeType);
            Assert.AreEqual("baz", dochtml1body1p1Text0.TextContent);

            var dochtml1body1table2 = dochtml1body1.ChildNodes[2] as Element;
            Assert.AreEqual(1, dochtml1body1table2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table2.Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table2.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table2.NodeType);

            var dochtml1body1table2colgroup0 = dochtml1body1table2.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1table2colgroup0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table2colgroup0.Attributes.Count);
            Assert.AreEqual("colgroup", dochtml1body1table2colgroup0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table2colgroup0.NodeType);

            var dochtml1body1p3 = dochtml1body1.ChildNodes[3] as Element;
            Assert.AreEqual(1, dochtml1body1p3.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1p3.Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p3.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1p3.NodeType);

            var dochtml1body1p3Text0 = dochtml1body1p3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1p3Text0.NodeType);
            Assert.AreEqual("quux", dochtml1body1p3Text0.TextContent);
        }

        [TestMethod]
        public void MathMLInSelectInTable()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body><table><tr><td><select><math><mi>foo</mi><mi>bar</mi><p>baz</table><p>quux");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1table0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0.Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0.NodeType);

            var dochtml1body1table0tbody0 = dochtml1body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0.Attributes.Count);
            Assert.AreEqual("tbody", dochtml1body1table0tbody0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0.NodeType);

            var dochtml1body1table0tbody0tr0 = dochtml1body1table0tbody0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0.Attributes.Count);
            Assert.AreEqual("tr", dochtml1body1table0tbody0tr0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0.NodeType);

            var dochtml1body1table0tbody0tr0td0 = dochtml1body1table0tbody0tr0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0.Attributes.Count);
            Assert.AreEqual("td", dochtml1body1table0tbody0tr0td0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0.NodeType);

            var dochtml1body1table0tbody0tr0td0select0 = dochtml1body1table0tbody0tr0td0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0select0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0select0.Attributes.Count);
            Assert.AreEqual("select", dochtml1body1table0tbody0tr0td0select0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0select0.NodeType);

            var dochtml1body1table0tbody0tr0td0select0Text0 = dochtml1body1table0tbody0tr0td0select0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0tbody0tr0td0select0Text0.NodeType);
            Assert.AreEqual("foobarbaz", dochtml1body1table0tbody0tr0td0select0Text0.TextContent);

            var dochtml1body1p1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1p1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1p1.Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1p1.NodeType);

            var dochtml1body1p1Text0 = dochtml1body1p1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1p1Text0.NodeType);
            Assert.AreEqual("quux", dochtml1body1p1Text0.TextContent);
        }

        [TestMethod]
        public void MathMLInSelectInTableImplicitlyClosed()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body><table><select><math><mi>foo</mi><mi>bar</mi><p>baz</table><p>quux");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(3, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1select0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1select0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1select0.Attributes.Count);
            Assert.AreEqual("select", dochtml1body1select0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1select0.NodeType);

            var dochtml1body1select0Text0 = dochtml1body1select0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1select0Text0.NodeType);
            Assert.AreEqual("foobarbaz", dochtml1body1select0Text0.TextContent);

            var dochtml1body1table1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table1.Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1table1.NodeType);

            var dochtml1body1p2 = dochtml1body1.ChildNodes[2] as Element;
            Assert.AreEqual(1, dochtml1body1p2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1p2.Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p2.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1p2.NodeType);

            var dochtml1body1p2Text0 = dochtml1body1p2.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1p2Text0.NodeType);
            Assert.AreEqual("quux", dochtml1body1p2Text0.TextContent);
        }

        [TestMethod]
        public void MathMLOutsideDocumentRoot()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body></body></html><math><mi>foo</mi><mi>bar</mi><p>baz");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml1body1math0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

            var dochtml1body1math0mi0 = dochtml1body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mi0.Attributes.Count);
            Assert.AreEqual("mi", dochtml1body1math0mi0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi0.NodeType);

            var dochtml1body1math0mi0Text0 = dochtml1body1math0mi0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1math0mi0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1math0mi0Text0.TextContent);

            var dochtml1body1math0mi1 = dochtml1body1math0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1math0mi1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mi1.Attributes.Count);
            Assert.AreEqual("mi", dochtml1body1math0mi1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi1.NodeType);

            var dochtml1body1math0mi1Text0 = dochtml1body1math0mi1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1math0mi1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1math0mi1Text0.TextContent);

            var dochtml1body1p1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1p1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1p1.Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1p1.NodeType);

            var dochtml1body1p1Text0 = dochtml1body1p1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1p1Text0.NodeType);
            Assert.AreEqual("baz", dochtml1body1p1Text0.TextContent);
        }

        [TestMethod]
        public void MathMLOutsideDocumentImplicitlyClosed()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body></body><math><mi>foo</mi><mi>bar</mi><p>baz");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml1body1math0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

            var dochtml1body1math0mi0 = dochtml1body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mi0.Attributes.Count);
            Assert.AreEqual("mi", dochtml1body1math0mi0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi0.NodeType);

            var dochtml1body1math0mi0Text0 = dochtml1body1math0mi0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1math0mi0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1math0mi0Text0.TextContent);

            var dochtml1body1math0mi1 = dochtml1body1math0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1math0mi1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mi1.Attributes.Count);
            Assert.AreEqual("mi", dochtml1body1math0mi1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi1.NodeType);

            var dochtml1body1math0mi1Text0 = dochtml1body1math0mi1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1math0mi1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1math0mi1Text0.TextContent);

            var dochtml1body1p1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1p1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1p1.Attributes.Count);
            Assert.AreEqual("p", dochtml1body1p1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1p1.NodeType);

            var dochtml1body1p1Text0 = dochtml1body1p1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1p1Text0.NodeType);
            Assert.AreEqual("baz", dochtml1body1p1Text0.TextContent);
        }

        [TestMethod]
        public void MathMLInFrameset()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><frameset><math><mi></mi><mi></mi><p><span>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1frameset1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1frameset1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1frameset1.Attributes.Count);
            Assert.AreEqual("frameset", dochtml1frameset1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1frameset1.NodeType);
        }

        [TestMethod]
        public void MathMLOutsideFrameset()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><frameset></frameset><math><mi></mi><mi></mi><p><span>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1frameset1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1frameset1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1frameset1.Attributes.Count);
            Assert.AreEqual("frameset", dochtml1frameset1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1frameset1.NodeType);
        }

        [TestMethod]
        public void MathMLWithXLinkAttributes()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body xlink:href=foo><math xlink:href=foo></math>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
            Assert.AreEqual("foo", dochtml1body1.Attributes["xlink:href"].Value);

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml1body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml1body1math0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

            var attr = dochtml1body1math0.Attributes["href"];
            Assert.IsNotNull(attr);
            Assert.AreEqual("foo", attr.Value);
            Assert.AreEqual(null, attr.Prefix);
            Assert.AreEqual("http://www.w3.org/1999/xlink", attr.NamespaceUri);
        }

        [TestMethod]
        public void MathMLInBodyWithLangAttribute()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body xlink:href=foo xml:lang=en><math><mi xml:lang=en xlink:href=foo></mi></math>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(2, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
            Assert.AreEqual("foo", dochtml1body1.Attributes["xlink:href"].Value);
            Assert.AreEqual("en", dochtml1body1.Attributes["xml:lang"].Value);

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml1body1math0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

            var dochtml1body1math0mi0 = dochtml1body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1math0mi0.ChildNodes.Length);
            Assert.AreEqual(2, dochtml1body1math0mi0.Attributes.Count);
            Assert.AreEqual("mi", dochtml1body1math0mi0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi0.NodeType);

            var attr1 = dochtml1body1math0mi0.Attributes["href"];
            Assert.IsNotNull(attr1);
            Assert.AreEqual("foo", attr1.Value);
            Assert.AreEqual(null, attr1.Prefix);
            Assert.AreEqual("http://www.w3.org/1999/xlink", attr1.NamespaceUri);

            var attr2 = dochtml1body1math0mi0.Attributes["xml:lang"];
            Assert.IsNotNull(attr2);
            Assert.AreEqual("en", attr2.Value);
            Assert.AreEqual("xml", attr2.Prefix);
            Assert.AreEqual("http://www.w3.org/XML/1998/namespace", attr2.NamespaceUri);
        }

        [TestMethod]
        public void MathMLWithMiChild()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body xlink:href=foo xml:lang=en><math><mi xml:lang=en xlink:href=foo /></math>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(2, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
            Assert.AreEqual("foo", dochtml1body1.Attributes["xlink:href"].Value);
            Assert.AreEqual("en", dochtml1body1.Attributes["xml:lang"].Value);

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml1body1math0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

            var dochtml1body1math0mi0 = dochtml1body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1math0mi0.ChildNodes.Length);
            Assert.AreEqual(2, dochtml1body1math0mi0.Attributes.Count);
            Assert.AreEqual("mi", dochtml1body1math0mi0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi0.NodeType);

            var attr1 = dochtml1body1math0mi0.Attributes["href"];
            Assert.IsNotNull(attr1);
            Assert.AreEqual("foo", attr1.Value);
            Assert.AreEqual(null, attr1.Prefix);
            Assert.AreEqual("http://www.w3.org/1999/xlink", attr1.NamespaceUri);

            var attr2 = dochtml1body1math0mi0.Attributes["xml:lang"];
            Assert.IsNotNull(attr2);
            Assert.AreEqual("en", attr2.Value);
            Assert.AreEqual("xml", attr2.Prefix);
            Assert.AreEqual("http://www.w3.org/XML/1998/namespace", attr2.NamespaceUri);
        }

        [TestMethod]
        public void MathMLWithTextNode()
        {
            var doc = DocumentBuilder.Html(@"<!DOCTYPE html><body xlink:href=foo xml:lang=en><math><mi xml:lang=en xlink:href=foo />bar</math>");

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(2, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
            Assert.AreEqual("foo", dochtml1body1.Attributes["xlink:href"].Value);
            Assert.AreEqual("en", dochtml1body1.Attributes["xml:lang"].Value);

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml1body1math0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

            var dochtml1body1math0mi0 = dochtml1body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1math0mi0.ChildNodes.Length);
            Assert.AreEqual(2, dochtml1body1math0mi0.Attributes.Count);
            Assert.AreEqual("mi", dochtml1body1math0mi0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi0.NodeType);

            var attr1 = dochtml1body1math0mi0.Attributes["href"];
            Assert.IsNotNull(attr1);
            Assert.AreEqual("foo", attr1.Value);
            Assert.AreEqual(null, attr1.Prefix);
            Assert.AreEqual("http://www.w3.org/1999/xlink", attr1.NamespaceUri);

            var attr2 = dochtml1body1math0mi0.Attributes["xml:lang"];
            Assert.IsNotNull(attr2);
            Assert.AreEqual("en", attr2.Value);
            Assert.AreEqual("xml", attr2.Prefix);
            Assert.AreEqual("http://www.w3.org/XML/1998/namespace", attr2.NamespaceUri);

            var dochtml1body1math0Text1 = dochtml1body1math0.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml1body1math0Text1.NodeType);
            Assert.AreEqual("bar", dochtml1body1math0Text1.TextContent);
        }
    }
}
