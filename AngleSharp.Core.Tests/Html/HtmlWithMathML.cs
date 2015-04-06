using System;
using AngleSharp.Dom;
using AngleSharp.Extensions;
using NUnit.Framework;

namespace AngleSharp.Core.Tests
{
    /// <summary>
    /// Tests from https://github.com/html5lib/html5lib-tests:
    /// tree-construction/tests9.dat
    /// </summary>
    [TestFixture]
    public class HtmlWithMathMLTests
    {
        static IDocument Html(String code)
        {
            return code.ToHtmlDocument();
        }

        [Test]
        public void MathMLCheckAttributesCaseNormalUnchanged()
        {
            var doc = Html(@"<!DOCTYPE html><body><math attributeName='' attributeType='' baseFrequency='' baseProfile='' calcMode='' clipPathUnits='' contentScriptType='' contentStyleType='' diffuseConstant='' edgeMode='' externalResourcesRequired='' filterRes='' filterUnits='' glyphRef='' gradientTransform='' gradientUnits='' kernelMatrix='' kernelUnitLength='' keyPoints='' keySplines='' keyTimes='' lengthAdjust='' limitingConeAngle='' markerHeight='' markerUnits='' markerWidth='' maskContentUnits='' maskUnits='' numOctaves='' pathLength='' patternContentUnits='' patternTransform='' patternUnits='' pointsAtX='' pointsAtY='' pointsAtZ='' preserveAlpha='' preserveAspectRatio='' primitiveUnits='' refX='' refY='' repeatCount='' repeatDur='' requiredExtensions='' requiredFeatures='' specularConstant='' specularExponent='' spreadMethod='' startOffset='' stdDeviation='' stitchTiles='' surfaceScale='' systemLanguage='' tableValues='' targetX='' targetY='' textLength='' viewBox='' viewTarget='' xChannelSelector='' yChannelSelector='' zoomAndPan=''></math>");

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

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(62, dochtml1body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml1body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("attributename"));
            Assert.AreEqual("attributename", dochtml1body1math0.Attributes.Get("attributename").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("attributename").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("attributetype"));
            Assert.AreEqual("attributetype", dochtml1body1math0.Attributes.Get("attributetype").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("attributetype").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("basefrequency"));
            Assert.AreEqual("basefrequency", dochtml1body1math0.Attributes.Get("basefrequency").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("basefrequency").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("baseprofile"));
            Assert.AreEqual("baseprofile", dochtml1body1math0.Attributes.Get("baseprofile").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("baseprofile").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("calcmode"));
            Assert.AreEqual("calcmode", dochtml1body1math0.Attributes.Get("calcmode").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("calcmode").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("clippathunits"));
            Assert.AreEqual("clippathunits", dochtml1body1math0.Attributes.Get("clippathunits").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("clippathunits").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("contentscripttype"));
            Assert.AreEqual("contentscripttype", dochtml1body1math0.Attributes.Get("contentscripttype").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("contentscripttype").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("contentstyletype"));
            Assert.AreEqual("contentstyletype", dochtml1body1math0.Attributes.Get("contentstyletype").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("contentstyletype").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("diffuseconstant"));
            Assert.AreEqual("diffuseconstant", dochtml1body1math0.Attributes.Get("diffuseconstant").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("diffuseconstant").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("edgemode"));
            Assert.AreEqual("edgemode", dochtml1body1math0.Attributes.Get("edgemode").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("edgemode").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("externalresourcesrequired"));
            Assert.AreEqual("externalresourcesrequired", dochtml1body1math0.Attributes.Get("externalresourcesrequired").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("externalresourcesrequired").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("filterres"));
            Assert.AreEqual("filterres", dochtml1body1math0.Attributes.Get("filterres").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("filterres").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("filterunits"));
            Assert.AreEqual("filterunits", dochtml1body1math0.Attributes.Get("filterunits").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("filterunits").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("glyphref"));
            Assert.AreEqual("glyphref", dochtml1body1math0.Attributes.Get("glyphref").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("glyphref").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("gradienttransform"));
            Assert.AreEqual("gradienttransform", dochtml1body1math0.Attributes.Get("gradienttransform").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("gradienttransform").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("gradientunits"));
            Assert.AreEqual("gradientunits", dochtml1body1math0.Attributes.Get("gradientunits").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("gradientunits").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("kernelmatrix"));
            Assert.AreEqual("kernelmatrix", dochtml1body1math0.Attributes.Get("kernelmatrix").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("kernelmatrix").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("kernelunitlength"));
            Assert.AreEqual("kernelunitlength", dochtml1body1math0.Attributes.Get("kernelunitlength").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("kernelunitlength").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("keypoints"));
            Assert.AreEqual("keypoints", dochtml1body1math0.Attributes.Get("keypoints").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("keypoints").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("keysplines"));
            Assert.AreEqual("keysplines", dochtml1body1math0.Attributes.Get("keysplines").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("keysplines").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("keytimes"));
            Assert.AreEqual("keytimes", dochtml1body1math0.Attributes.Get("keytimes").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("keytimes").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("lengthadjust"));
            Assert.AreEqual("lengthadjust", dochtml1body1math0.Attributes.Get("lengthadjust").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("lengthadjust").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("limitingconeangle"));
            Assert.AreEqual("limitingconeangle", dochtml1body1math0.Attributes.Get("limitingconeangle").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("limitingconeangle").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("markerheight"));
            Assert.AreEqual("markerheight", dochtml1body1math0.Attributes.Get("markerheight").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("markerheight").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("markerunits"));
            Assert.AreEqual("markerunits", dochtml1body1math0.Attributes.Get("markerunits").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("markerunits").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("markerwidth"));
            Assert.AreEqual("markerwidth", dochtml1body1math0.Attributes.Get("markerwidth").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("markerwidth").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("maskcontentunits"));
            Assert.AreEqual("maskcontentunits", dochtml1body1math0.Attributes.Get("maskcontentunits").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("maskcontentunits").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("maskunits"));
            Assert.AreEqual("maskunits", dochtml1body1math0.Attributes.Get("maskunits").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("maskunits").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("numoctaves"));
            Assert.AreEqual("numoctaves", dochtml1body1math0.Attributes.Get("numoctaves").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("numoctaves").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("pathlength"));
            Assert.AreEqual("pathlength", dochtml1body1math0.Attributes.Get("pathlength").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("pathlength").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("patterncontentunits"));
            Assert.AreEqual("patterncontentunits", dochtml1body1math0.Attributes.Get("patterncontentunits").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("patterncontentunits").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("patterntransform"));
            Assert.AreEqual("patterntransform", dochtml1body1math0.Attributes.Get("patterntransform").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("patterntransform").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("patternunits"));
            Assert.AreEqual("patternunits", dochtml1body1math0.Attributes.Get("patternunits").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("patternunits").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("pointsatx"));
            Assert.AreEqual("pointsatx", dochtml1body1math0.Attributes.Get("pointsatx").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("pointsatx").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("pointsaty"));
            Assert.AreEqual("pointsaty", dochtml1body1math0.Attributes.Get("pointsaty").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("pointsaty").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("pointsatz"));
            Assert.AreEqual("pointsatz", dochtml1body1math0.Attributes.Get("pointsatz").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("pointsatz").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("preservealpha"));
            Assert.AreEqual("preservealpha", dochtml1body1math0.Attributes.Get("preservealpha").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("preservealpha").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("preserveaspectratio"));
            Assert.AreEqual("preserveaspectratio", dochtml1body1math0.Attributes.Get("preserveaspectratio").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("preserveaspectratio").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("primitiveunits"));
            Assert.AreEqual("primitiveunits", dochtml1body1math0.Attributes.Get("primitiveunits").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("primitiveunits").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("refx"));
            Assert.AreEqual("refx", dochtml1body1math0.Attributes.Get("refx").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("refx").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("refy"));
            Assert.AreEqual("refy", dochtml1body1math0.Attributes.Get("refy").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("refy").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("repeatcount"));
            Assert.AreEqual("repeatcount", dochtml1body1math0.Attributes.Get("repeatcount").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("repeatcount").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("repeatdur"));
            Assert.AreEqual("repeatdur", dochtml1body1math0.Attributes.Get("repeatdur").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("repeatdur").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("requiredextensions"));
            Assert.AreEqual("requiredextensions", dochtml1body1math0.Attributes.Get("requiredextensions").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("requiredextensions").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("requiredfeatures"));
            Assert.AreEqual("requiredfeatures", dochtml1body1math0.Attributes.Get("requiredfeatures").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("requiredfeatures").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("specularconstant"));
            Assert.AreEqual("specularconstant", dochtml1body1math0.Attributes.Get("specularconstant").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("specularconstant").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("specularexponent"));
            Assert.AreEqual("specularexponent", dochtml1body1math0.Attributes.Get("specularexponent").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("specularexponent").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("spreadmethod"));
            Assert.AreEqual("spreadmethod", dochtml1body1math0.Attributes.Get("spreadmethod").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("spreadmethod").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("startoffset"));
            Assert.AreEqual("startoffset", dochtml1body1math0.Attributes.Get("startoffset").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("startoffset").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("stddeviation"));
            Assert.AreEqual("stddeviation", dochtml1body1math0.Attributes.Get("stddeviation").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("stddeviation").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("stitchtiles"));
            Assert.AreEqual("stitchtiles", dochtml1body1math0.Attributes.Get("stitchtiles").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("stitchtiles").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("surfacescale"));
            Assert.AreEqual("surfacescale", dochtml1body1math0.Attributes.Get("surfacescale").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("surfacescale").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("systemlanguage"));
            Assert.AreEqual("systemlanguage", dochtml1body1math0.Attributes.Get("systemlanguage").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("systemlanguage").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("tablevalues"));
            Assert.AreEqual("tablevalues", dochtml1body1math0.Attributes.Get("tablevalues").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("tablevalues").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("targetx"));
            Assert.AreEqual("targetx", dochtml1body1math0.Attributes.Get("targetx").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("targetx").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("targety"));
            Assert.AreEqual("targety", dochtml1body1math0.Attributes.Get("targety").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("targety").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("textlength"));
            Assert.AreEqual("textlength", dochtml1body1math0.Attributes.Get("textlength").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("textlength").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("viewbox"));
            Assert.AreEqual("viewbox", dochtml1body1math0.Attributes.Get("viewbox").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("viewbox").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("viewtarget"));
            Assert.AreEqual("viewtarget", dochtml1body1math0.Attributes.Get("viewtarget").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("viewtarget").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("xchannelselector"));
            Assert.AreEqual("xchannelselector", dochtml1body1math0.Attributes.Get("xchannelselector").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("xchannelselector").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("ychannelselector"));
            Assert.AreEqual("ychannelselector", dochtml1body1math0.Attributes.Get("ychannelselector").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("ychannelselector").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.Get("zoomandpan"));
            Assert.AreEqual("zoomandpan", dochtml1body1math0.Attributes.Get("zoomandpan").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.Get("zoomandpan").Value);
        }

        [Test]
        public void MathMLCheckTagCaseNormalUnchanged()
        {
            var doc = Html(@"<!DOCTYPE html><body><math><altGlyph /><altGlyphDef /><altGlyphItem /><animateColor /><animateMotion /><animateTransform /><clipPath /><feBlend /><feColorMatrix /><feComponentTransfer /><feComposite /><feConvolveMatrix /><feDiffuseLighting /><feDisplacementMap /><feDistantLight /><feFlood /><feFuncA /><feFuncB /><feFuncG /><feFuncR /><feGaussianBlur /><feImage /><feMerge /><feMergeNode /><feMorphology /><feOffset /><fePointLight /><feSpecularLighting /><feSpotLight /><feTile /><feTurbulence /><foreignObject /><glyphRef /><linearGradient /><radialGradient /><textPath /></math>");

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

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(36, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml1body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

            var dochtml1body1math0altglyph0 = dochtml1body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1math0altglyph0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0altglyph0.Attributes.Count);
            Assert.AreEqual("altglyph", dochtml1body1math0altglyph0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0altglyph0.NodeType);

            var dochtml1body1math0altglyphdef1 = dochtml1body1math0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1math0altglyphdef1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0altglyphdef1.Attributes.Count);
            Assert.AreEqual("altglyphdef", dochtml1body1math0altglyphdef1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0altglyphdef1.NodeType);

            var dochtml1body1math0altglyphitem2 = dochtml1body1math0.ChildNodes[2] as Element;
            Assert.AreEqual(0, dochtml1body1math0altglyphitem2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0altglyphitem2.Attributes.Count);
            Assert.AreEqual("altglyphitem", dochtml1body1math0altglyphitem2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0altglyphitem2.NodeType);

            var dochtml1body1math0animatecolor3 = dochtml1body1math0.ChildNodes[3] as Element;
            Assert.AreEqual(0, dochtml1body1math0animatecolor3.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0animatecolor3.Attributes.Count);
            Assert.AreEqual("animatecolor", dochtml1body1math0animatecolor3.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0animatecolor3.NodeType);

            var dochtml1body1math0animatemotion4 = dochtml1body1math0.ChildNodes[4] as Element;
            Assert.AreEqual(0, dochtml1body1math0animatemotion4.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0animatemotion4.Attributes.Count);
            Assert.AreEqual("animatemotion", dochtml1body1math0animatemotion4.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0animatemotion4.NodeType);

            var dochtml1body1math0animatetransform5 = dochtml1body1math0.ChildNodes[5] as Element;
            Assert.AreEqual(0, dochtml1body1math0animatetransform5.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0animatetransform5.Attributes.Count);
            Assert.AreEqual("animatetransform", dochtml1body1math0animatetransform5.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0animatetransform5.NodeType);

            var dochtml1body1math0clippath6 = dochtml1body1math0.ChildNodes[6] as Element;
            Assert.AreEqual(0, dochtml1body1math0clippath6.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0clippath6.Attributes.Count);
            Assert.AreEqual("clippath", dochtml1body1math0clippath6.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0clippath6.NodeType);

            var dochtml1body1math0feblend7 = dochtml1body1math0.ChildNodes[7] as Element;
            Assert.AreEqual(0, dochtml1body1math0feblend7.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0feblend7.Attributes.Count);
            Assert.AreEqual("feblend", dochtml1body1math0feblend7.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0feblend7.NodeType);

            var dochtml1body1math0fecolormatrix8 = dochtml1body1math0.ChildNodes[8] as Element;
            Assert.AreEqual(0, dochtml1body1math0fecolormatrix8.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0fecolormatrix8.Attributes.Count);
            Assert.AreEqual("fecolormatrix", dochtml1body1math0fecolormatrix8.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0fecolormatrix8.NodeType);

            var dochtml1body1math0fecomponenttransfer9 = dochtml1body1math0.ChildNodes[9] as Element;
            Assert.AreEqual(0, dochtml1body1math0fecomponenttransfer9.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0fecomponenttransfer9.Attributes.Count);
            Assert.AreEqual("fecomponenttransfer", dochtml1body1math0fecomponenttransfer9.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0fecomponenttransfer9.NodeType);

            var dochtml1body1math0fecomposite10 = dochtml1body1math0.ChildNodes[10] as Element;
            Assert.AreEqual(0, dochtml1body1math0fecomposite10.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0fecomposite10.Attributes.Count);
            Assert.AreEqual("fecomposite", dochtml1body1math0fecomposite10.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0fecomposite10.NodeType);

            var dochtml1body1math0feconvolvematrix11 = dochtml1body1math0.ChildNodes[11] as Element;
            Assert.AreEqual(0, dochtml1body1math0feconvolvematrix11.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0feconvolvematrix11.Attributes.Count);
            Assert.AreEqual("feconvolvematrix", dochtml1body1math0feconvolvematrix11.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0feconvolvematrix11.NodeType);

            var dochtml1body1math0fediffuselighting12 = dochtml1body1math0.ChildNodes[12] as Element;
            Assert.AreEqual(0, dochtml1body1math0fediffuselighting12.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0fediffuselighting12.Attributes.Count);
            Assert.AreEqual("fediffuselighting", dochtml1body1math0fediffuselighting12.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0fediffuselighting12.NodeType);

            var dochtml1body1math0fedisplacementmap13 = dochtml1body1math0.ChildNodes[13] as Element;
            Assert.AreEqual(0, dochtml1body1math0fedisplacementmap13.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0fedisplacementmap13.Attributes.Count);
            Assert.AreEqual("fedisplacementmap", dochtml1body1math0fedisplacementmap13.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0fedisplacementmap13.NodeType);

            var dochtml1body1math0fedistantlight14 = dochtml1body1math0.ChildNodes[14] as Element;
            Assert.AreEqual(0, dochtml1body1math0fedistantlight14.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0fedistantlight14.Attributes.Count);
            Assert.AreEqual("fedistantlight", dochtml1body1math0fedistantlight14.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0fedistantlight14.NodeType);

            var dochtml1body1math0feflood15 = dochtml1body1math0.ChildNodes[15] as Element;
            Assert.AreEqual(0, dochtml1body1math0feflood15.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0feflood15.Attributes.Count);
            Assert.AreEqual("feflood", dochtml1body1math0feflood15.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0feflood15.NodeType);

            var dochtml1body1math0fefunca16 = dochtml1body1math0.ChildNodes[16] as Element;
            Assert.AreEqual(0, dochtml1body1math0fefunca16.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0fefunca16.Attributes.Count);
            Assert.AreEqual("fefunca", dochtml1body1math0fefunca16.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0fefunca16.NodeType);

            var dochtml1body1math0fefuncb17 = dochtml1body1math0.ChildNodes[17] as Element;
            Assert.AreEqual(0, dochtml1body1math0fefuncb17.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0fefuncb17.Attributes.Count);
            Assert.AreEqual("fefuncb", dochtml1body1math0fefuncb17.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0fefuncb17.NodeType);

            var dochtml1body1math0fefuncg18 = dochtml1body1math0.ChildNodes[18] as Element;
            Assert.AreEqual(0, dochtml1body1math0fefuncg18.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0fefuncg18.Attributes.Count);
            Assert.AreEqual("fefuncg", dochtml1body1math0fefuncg18.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0fefuncg18.NodeType);

            var dochtml1body1math0fefuncr19 = dochtml1body1math0.ChildNodes[19] as Element;
            Assert.AreEqual(0, dochtml1body1math0fefuncr19.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0fefuncr19.Attributes.Count);
            Assert.AreEqual("fefuncr", dochtml1body1math0fefuncr19.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0fefuncr19.NodeType);

            var dochtml1body1math0fegaussianblur20 = dochtml1body1math0.ChildNodes[20] as Element;
            Assert.AreEqual(0, dochtml1body1math0fegaussianblur20.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0fegaussianblur20.Attributes.Count);
            Assert.AreEqual("fegaussianblur", dochtml1body1math0fegaussianblur20.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0fegaussianblur20.NodeType);

            var dochtml1body1math0feimage21 = dochtml1body1math0.ChildNodes[21] as Element;
            Assert.AreEqual(0, dochtml1body1math0feimage21.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0feimage21.Attributes.Count);
            Assert.AreEqual("feimage", dochtml1body1math0feimage21.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0feimage21.NodeType);

            var dochtml1body1math0femerge22 = dochtml1body1math0.ChildNodes[22] as Element;
            Assert.AreEqual(0, dochtml1body1math0femerge22.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0femerge22.Attributes.Count);
            Assert.AreEqual("femerge", dochtml1body1math0femerge22.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0femerge22.NodeType);

            var dochtml1body1math0femergenode23 = dochtml1body1math0.ChildNodes[23] as Element;
            Assert.AreEqual(0, dochtml1body1math0femergenode23.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0femergenode23.Attributes.Count);
            Assert.AreEqual("femergenode", dochtml1body1math0femergenode23.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0femergenode23.NodeType);

            var dochtml1body1math0femorphology24 = dochtml1body1math0.ChildNodes[24] as Element;
            Assert.AreEqual(0, dochtml1body1math0femorphology24.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0femorphology24.Attributes.Count);
            Assert.AreEqual("femorphology", dochtml1body1math0femorphology24.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0femorphology24.NodeType);

            var dochtml1body1math0feoffset25 = dochtml1body1math0.ChildNodes[25] as Element;
            Assert.AreEqual(0, dochtml1body1math0feoffset25.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0feoffset25.Attributes.Count);
            Assert.AreEqual("feoffset", dochtml1body1math0feoffset25.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0feoffset25.NodeType);

            var dochtml1body1math0fepointlight26 = dochtml1body1math0.ChildNodes[26] as Element;
            Assert.AreEqual(0, dochtml1body1math0fepointlight26.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0fepointlight26.Attributes.Count);
            Assert.AreEqual("fepointlight", dochtml1body1math0fepointlight26.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0fepointlight26.NodeType);

            var dochtml1body1math0fespecularlighting27 = dochtml1body1math0.ChildNodes[27] as Element;
            Assert.AreEqual(0, dochtml1body1math0fespecularlighting27.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0fespecularlighting27.Attributes.Count);
            Assert.AreEqual("fespecularlighting", dochtml1body1math0fespecularlighting27.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0fespecularlighting27.NodeType);

            var dochtml1body1math0fespotlight28 = dochtml1body1math0.ChildNodes[28] as Element;
            Assert.AreEqual(0, dochtml1body1math0fespotlight28.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0fespotlight28.Attributes.Count);
            Assert.AreEqual("fespotlight", dochtml1body1math0fespotlight28.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0fespotlight28.NodeType);

            var dochtml1body1math0fetile29 = dochtml1body1math0.ChildNodes[29] as Element;
            Assert.AreEqual(0, dochtml1body1math0fetile29.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0fetile29.Attributes.Count);
            Assert.AreEqual("fetile", dochtml1body1math0fetile29.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0fetile29.NodeType);

            var dochtml1body1math0feturbulence30 = dochtml1body1math0.ChildNodes[30] as Element;
            Assert.AreEqual(0, dochtml1body1math0feturbulence30.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0feturbulence30.Attributes.Count);
            Assert.AreEqual("feturbulence", dochtml1body1math0feturbulence30.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0feturbulence30.NodeType);

            var dochtml1body1math0foreignobject31 = dochtml1body1math0.ChildNodes[31] as Element;
            Assert.AreEqual(0, dochtml1body1math0foreignobject31.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0foreignobject31.Attributes.Count);
            Assert.AreEqual("foreignobject", dochtml1body1math0foreignobject31.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0foreignobject31.NodeType);

            var dochtml1body1math0glyphref32 = dochtml1body1math0.ChildNodes[32] as Element;
            Assert.AreEqual(0, dochtml1body1math0glyphref32.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0glyphref32.Attributes.Count);
            Assert.AreEqual("glyphref", dochtml1body1math0glyphref32.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0glyphref32.NodeType);

            var dochtml1body1math0lineargradient33 = dochtml1body1math0.ChildNodes[33] as Element;
            Assert.AreEqual(0, dochtml1body1math0lineargradient33.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0lineargradient33.Attributes.Count);
            Assert.AreEqual("lineargradient", dochtml1body1math0lineargradient33.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0lineargradient33.NodeType);

            var dochtml1body1math0radialgradient34 = dochtml1body1math0.ChildNodes[34] as Element;
            Assert.AreEqual(0, dochtml1body1math0radialgradient34.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0radialgradient34.Attributes.Count);
            Assert.AreEqual("radialgradient", dochtml1body1math0radialgradient34.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0radialgradient34.NodeType);

            var dochtml1body1math0textpath35 = dochtml1body1math0.ChildNodes[35] as Element;
            Assert.AreEqual(0, dochtml1body1math0textpath35.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0textpath35.Attributes.Count);
            Assert.AreEqual("textpath", dochtml1body1math0textpath35.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0textpath35.NodeType);
        }

        [Test]
        public void MathMLSingleElement()
        {
            var doc = Html(@"<!DOCTYPE html><math></math>");

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

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml1body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);
        }

        [Test]
        public void MathMLSingleElementInBody()
        {
            var doc = Html(@"<!DOCTYPE html><body><math></math>");

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

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml1body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);
        }

        [Test]
        public void MathMLElementWithDivAndObjectElements()
        {
            var doc = Html(@"<math><mi><div><object><div><span></span></div></object></div></mi><mi>");

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

            var dochtml0body1math0mi0 = dochtml0body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mi0.Attributes.Count);
            Assert.AreEqual("mi", dochtml0body1math0mi0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mi0.NodeType);

            var dochtml0body1math0mi0div0 = dochtml0body1math0mi0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0mi0div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mi0div0.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1math0mi0div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mi0div0.NodeType);

            var dochtml0body1math0mi0div0object0 = dochtml0body1math0mi0div0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0mi0div0object0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mi0div0object0.Attributes.Count);
            Assert.AreEqual("object", dochtml0body1math0mi0div0object0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mi0div0object0.NodeType);

            var dochtml0body1math0mi0div0object0div0 = dochtml0body1math0mi0div0object0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0mi0div0object0div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mi0div0object0div0.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1math0mi0div0object0div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mi0div0object0div0.NodeType);

            var dochtml0body1math0mi0div0object0div0span0 = dochtml0body1math0mi0div0object0div0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1math0mi0div0object0div0span0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mi0div0object0div0span0.Attributes.Count);
            Assert.AreEqual("span", dochtml0body1math0mi0div0object0div0span0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mi0div0object0div0span0.NodeType);

            var dochtml0body1math0mi1 = dochtml0body1math0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1math0mi1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mi1.Attributes.Count);
            Assert.AreEqual("mi", dochtml0body1math0mi1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mi1.NodeType);
        }

        [Test]
        public void MathMLElementWithSvgChild()
        {
            var doc = Html(@"<math><mi><svg><foreignObject><div><div></div></div></foreignObject></svg></mi><mi>");

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

            var dochtml0body1math0mi0 = dochtml0body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mi0.Attributes.Count);
            Assert.AreEqual("mi", dochtml0body1math0mi0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mi0.NodeType);

            var dochtml0body1math0mi0svg0 = dochtml0body1math0mi0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0mi0svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mi0svg0.Attributes.Count);
            Assert.AreEqual("svg", dochtml0body1math0mi0svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mi0svg0.NodeType);

            var dochtml0body1math0mi0svg0foreignObject0 = dochtml0body1math0mi0svg0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0mi0svg0foreignObject0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mi0svg0foreignObject0.Attributes.Count);
            Assert.AreEqual("foreignObject", dochtml0body1math0mi0svg0foreignObject0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mi0svg0foreignObject0.NodeType);

            var dochtml0body1math0mi0svg0foreignObject0div0 = dochtml0body1math0mi0svg0foreignObject0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0mi0svg0foreignObject0div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mi0svg0foreignObject0div0.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1math0mi0svg0foreignObject0div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mi0svg0foreignObject0div0.NodeType);

            var dochtml0body1math0mi0svg0foreignObject0div0div0 = dochtml0body1math0mi0svg0foreignObject0div0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1math0mi0svg0foreignObject0div0div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mi0svg0foreignObject0div0div0.Attributes.Count);
            Assert.AreEqual("div", dochtml0body1math0mi0svg0foreignObject0div0div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mi0svg0foreignObject0div0div0.NodeType);

            var dochtml0body1math0mi1 = dochtml0body1math0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1math0mi1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mi1.Attributes.Count);
            Assert.AreEqual("mi", dochtml0body1math0mi1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mi1.NodeType);
        }

        [Test]
        public void MathMLSingleElementWithChild()
        {
            var doc = Html(@"<!DOCTYPE html><math><mi>");

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

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml1body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

            var dochtml1body1math0mi0 = dochtml1body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mi0.Attributes.Count);
            Assert.AreEqual("mi", dochtml1body1math0mi0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi0.NodeType);
        }

        [Test]
        public void MathMLWithMiAndMglyphElements()
        {
            var doc = Html(@"<math><mi><mglyph>");

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
            Assert.AreEqual(1, dochtml0body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml0body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0.NodeType);

            var dochtml0body1math0mi0 = dochtml0body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mi0.Attributes.Count);
            Assert.AreEqual("mi", dochtml0body1math0mi0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mi0.NodeType);

            var dochtml0body1math0mi0mglyph0 = dochtml0body1math0mi0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1math0mi0mglyph0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mi0mglyph0.Attributes.Count);
            Assert.AreEqual("mglyph", dochtml0body1math0mi0mglyph0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mi0mglyph0.NodeType);
        }

        [Test]
        public void MathMLWithMiAndMalignmarkElements()
        {
            var doc = Html(@"<math><mi><malignmark>");

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
            Assert.AreEqual(1, dochtml0body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml0body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0.NodeType);

            var dochtml0body1math0mi0 = dochtml0body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mi0.Attributes.Count);
            Assert.AreEqual("mi", dochtml0body1math0mi0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mi0.NodeType);

            var dochtml0body1math0mi0malignmark0 = dochtml0body1math0mi0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1math0mi0malignmark0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mi0malignmark0.Attributes.Count);
            Assert.AreEqual("malignmark", dochtml0body1math0mi0malignmark0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mi0malignmark0.NodeType);
        }

        [Test]
        public void MathMLWithMoAndMglyphElements()
        {
            var doc = Html(@"<math><mo><mglyph>");

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
            Assert.AreEqual(1, dochtml0body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml0body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0.NodeType);

            var dochtml0body1math0mo0 = dochtml0body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0mo0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mo0.Attributes.Count);
            Assert.AreEqual("mo", dochtml0body1math0mo0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mo0.NodeType);

            var dochtml0body1math0mo0mglyph0 = dochtml0body1math0mo0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1math0mo0mglyph0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mo0mglyph0.Attributes.Count);
            Assert.AreEqual("mglyph", dochtml0body1math0mo0mglyph0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mo0mglyph0.NodeType);
        }

        [Test]
        public void MathMLWithMoAndMalignmarkElements()
        {
            var doc = Html(@"<math><mo><malignmark>");

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
            Assert.AreEqual(1, dochtml0body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml0body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0.NodeType);

            var dochtml0body1math0mo0 = dochtml0body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0mo0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mo0.Attributes.Count);
            Assert.AreEqual("mo", dochtml0body1math0mo0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mo0.NodeType);

            var dochtml0body1math0mo0malignmark0 = dochtml0body1math0mo0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1math0mo0malignmark0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mo0malignmark0.Attributes.Count);
            Assert.AreEqual("malignmark", dochtml0body1math0mo0malignmark0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mo0malignmark0.NodeType);
        }

        [Test]
        public void MathMLWithMnAndMglyphElements()
        {
            var doc = Html(@"<math><mn><mglyph>");

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
            Assert.AreEqual(1, dochtml0body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml0body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0.NodeType);

            var dochtml0body1math0mn0 = dochtml0body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0mn0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mn0.Attributes.Count);
            Assert.AreEqual("mn", dochtml0body1math0mn0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mn0.NodeType);

            var dochtml0body1math0mn0mglyph0 = dochtml0body1math0mn0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1math0mn0mglyph0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mn0mglyph0.Attributes.Count);
            Assert.AreEqual("mglyph", dochtml0body1math0mn0mglyph0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mn0mglyph0.NodeType);
        }

        [Test]
        public void MathMLWithMnAndMalignmarkElements()
        {
            var doc = Html(@"<math><mn><malignmark>");

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
            Assert.AreEqual(1, dochtml0body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml0body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0.NodeType);

            var dochtml0body1math0mn0 = dochtml0body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0mn0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mn0.Attributes.Count);
            Assert.AreEqual("mn", dochtml0body1math0mn0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mn0.NodeType);

            var dochtml0body1math0mn0malignmark0 = dochtml0body1math0mn0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1math0mn0malignmark0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mn0malignmark0.Attributes.Count);
            Assert.AreEqual("malignmark", dochtml0body1math0mn0malignmark0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mn0malignmark0.NodeType);
        }

        [Test]
        public void MathMLWithMsAndMglyphElements()
        {
            var doc = Html(@"<math><ms><mglyph>");

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
            Assert.AreEqual(1, dochtml0body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml0body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0.NodeType);

            var dochtml0body1math0ms0 = dochtml0body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0ms0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0ms0.Attributes.Count);
            Assert.AreEqual("ms", dochtml0body1math0ms0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0ms0.NodeType);

            var dochtml0body1math0ms0mglyph0 = dochtml0body1math0ms0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1math0ms0mglyph0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0ms0mglyph0.Attributes.Count);
            Assert.AreEqual("mglyph", dochtml0body1math0ms0mglyph0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0ms0mglyph0.NodeType);
        }

        [Test]
        public void MathMLWithMsAndMalignmarkElements()
        {
            var doc = Html(@"<math><ms><malignmark>");

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
            Assert.AreEqual(1, dochtml0body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml0body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0.NodeType);

            var dochtml0body1math0ms0 = dochtml0body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0ms0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0ms0.Attributes.Count);
            Assert.AreEqual("ms", dochtml0body1math0ms0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0ms0.NodeType);

            var dochtml0body1math0ms0malignmark0 = dochtml0body1math0ms0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1math0ms0malignmark0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0ms0malignmark0.Attributes.Count);
            Assert.AreEqual("malignmark", dochtml0body1math0ms0malignmark0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0ms0malignmark0.NodeType);
        }

        [Test]
        public void MathMLWithMtextAndMglyphElements()
        {
            var doc = Html(@"<math><mtext><mglyph>");

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
            Assert.AreEqual(1, dochtml0body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml0body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0.NodeType);

            var dochtml0body1math0mtext0 = dochtml0body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0mtext0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mtext0.Attributes.Count);
            Assert.AreEqual("mtext", dochtml0body1math0mtext0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mtext0.NodeType);

            var dochtml0body1math0mtext0mglyph0 = dochtml0body1math0mtext0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1math0mtext0mglyph0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mtext0mglyph0.Attributes.Count);
            Assert.AreEqual("mglyph", dochtml0body1math0mtext0mglyph0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mtext0mglyph0.NodeType);
        }

        [Test]
        public void MathMLWithMtextAndMalignmarkElements()
        {
            var doc = Html(@"<math><mtext><malignmark>");

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
            Assert.AreEqual(1, dochtml0body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml0body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0.NodeType);

            var dochtml0body1math0mtext0 = dochtml0body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0mtext0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mtext0.Attributes.Count);
            Assert.AreEqual("mtext", dochtml0body1math0mtext0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mtext0.NodeType);

            var dochtml0body1math0mtext0malignmark0 = dochtml0body1math0mtext0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1math0mtext0malignmark0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mtext0malignmark0.Attributes.Count);
            Assert.AreEqual("malignmark", dochtml0body1math0mtext0malignmark0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mtext0malignmark0.NodeType);
        }

        [Test]
        public void MathMLAnnotationXmlWithSvgInside()
        {
            var doc = Html(@"<!DOCTYPE html><math><annotation-xml><svg><u>");

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

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml1body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);
            var dochtml1body1math0annotationxml0 = dochtml1body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1math0annotationxml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0annotationxml0.Attributes.Count);
            Assert.AreEqual("annotation-xml", dochtml1body1math0annotationxml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0annotationxml0.NodeType);

            var dochtml1body1math0annotationxml0svg0 = dochtml1body1math0annotationxml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1math0annotationxml0svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0annotationxml0svg0.Attributes.Count);
            Assert.AreEqual("svg", dochtml1body1math0annotationxml0svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0annotationxml0svg0.NodeType);

            var dochtml1body1u1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1u1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1u1.Attributes.Count);
            Assert.AreEqual("u", dochtml1body1u1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1u1.NodeType);
        }

        [Test]
        public void MathMLElementInSelect()
        {
            var doc = Html(@"<!DOCTYPE html><body><select><math></math></select>");

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
        public void MathMLInOptionOfSelect()
        {
            var doc = Html(@"<!DOCTYPE html><body><select><option><math></math></option></select>");

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
        public void MathMLInTable()
        {
            var doc = Html(@"<!DOCTYPE html><body><table><math></math></table>");

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

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml1body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

            var dochtml1body1table1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table1.Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table1.NodeType);
        }

        [Test]
        public void MathMLWithChildInTable()
        {
            var doc = Html(@"<!DOCTYPE html><body><table><math><mi>foo</mi></math></table>");

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

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml1body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

            var dochtml1body1math0mi0 = dochtml1body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mi0.Attributes.Count);
            Assert.AreEqual("mi", dochtml1body1math0mi0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi0.NodeType);

            var dochtml1body1math0mi0Text0 = dochtml1body1math0mi0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1math0mi0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1math0mi0Text0.TextContent);

            var dochtml1body1table1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table1.Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table1.NodeType);
        }

        [Test]
        public void MathMLWithChildrenInTable()
        {
            var doc = Html(@"<!DOCTYPE html><body><table><math><mi>foo</mi><mi>bar</mi></math></table>");

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

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml1body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

            var dochtml1body1math0mi0 = dochtml1body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mi0.Attributes.Count);
            Assert.AreEqual("mi", dochtml1body1math0mi0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi0.NodeType);

            var dochtml1body1math0mi0Text0 = dochtml1body1math0mi0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1math0mi0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1math0mi0Text0.TextContent);

            var dochtml1body1math0mi1 = dochtml1body1math0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1math0mi1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mi1.Attributes.Count);
            Assert.AreEqual("mi", dochtml1body1math0mi1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi1.NodeType);

            var dochtml1body1math0mi1Text0 = dochtml1body1math0mi1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1math0mi1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1math0mi1Text0.TextContent);

            var dochtml1body1table1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table1.Attributes.Count);
            Assert.AreEqual("table", dochtml1body1table1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table1.NodeType);
        }

        [Test]
        public void MathMLInTBodySectionOfTable()
        {
            var doc = Html(@"<!DOCTYPE html><body><table><tbody><math><mi>foo</mi><mi>bar</mi></math></tbody></table>");

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

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml1body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

            var dochtml1body1math0mi0 = dochtml1body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mi0.Attributes.Count);
            Assert.AreEqual("mi", dochtml1body1math0mi0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi0.NodeType);

            var dochtml1body1math0mi0Text0 = dochtml1body1math0mi0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1math0mi0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1math0mi0Text0.TextContent);

            var dochtml1body1math0mi1 = dochtml1body1math0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1math0mi1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mi1.Attributes.Count);
            Assert.AreEqual("mi", dochtml1body1math0mi1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi1.NodeType);

            var dochtml1body1math0mi1Text0 = dochtml1body1math0mi1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1math0mi1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1math0mi1Text0.TextContent);

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
        public void MathMLInRowOfTable()
        {
            var doc = Html(@"<!DOCTYPE html><body><table><tbody><tr><math><mi>foo</mi><mi>bar</mi></math></tr></tbody></table>");

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

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml1body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

            var dochtml1body1math0mi0 = dochtml1body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mi0.Attributes.Count);
            Assert.AreEqual("mi", dochtml1body1math0mi0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi0.NodeType);

            var dochtml1body1math0mi0Text0 = dochtml1body1math0mi0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1math0mi0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1math0mi0Text0.TextContent);

            var dochtml1body1math0mi1 = dochtml1body1math0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1math0mi1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mi1.Attributes.Count);
            Assert.AreEqual("mi", dochtml1body1math0mi1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi1.NodeType);

            var dochtml1body1math0mi1Text0 = dochtml1body1math0mi1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1math0mi1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1math0mi1Text0.TextContent);

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
        public void MathMLInCellOfTable()
        {
            var doc = Html(@"<!DOCTYPE html><body><table><tbody><tr><td><math><mi>foo</mi><mi>bar</mi></math></td></tr></tbody></table>");

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

            var dochtml1body1table0tbody0tr0td0math0 = dochtml1body1table0tbody0tr0td0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1table0tbody0tr0td0math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0math0.Attributes.Count);
            Assert.AreEqual("math", dochtml1body1table0tbody0tr0td0math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0math0.NodeType);

            var dochtml1body1table0tbody0tr0td0math0mi0 = dochtml1body1table0tbody0tr0td0math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0math0mi0.Attributes.Count);
            Assert.AreEqual("mi", dochtml1body1table0tbody0tr0td0math0mi0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0math0mi0.NodeType);

            var dochtml1body1table0tbody0tr0td0math0mi0Text0 = dochtml1body1table0tbody0tr0td0math0mi0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0tbody0tr0td0math0mi0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1table0tbody0tr0td0math0mi0Text0.TextContent);

            var dochtml1body1table0tbody0tr0td0math0mi1 = dochtml1body1table0tbody0tr0td0math0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0math0mi1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0math0mi1.Attributes.Count);
            Assert.AreEqual("mi", dochtml1body1table0tbody0tr0td0math0mi1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0math0mi1.NodeType);

            var dochtml1body1table0tbody0tr0td0math0mi1Text0 = dochtml1body1table0tbody0tr0td0math0mi1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0tbody0tr0td0math0mi1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1table0tbody0tr0td0math0mi1Text0.TextContent);
        }

        [Test]
        public void MathMLCompleteExampleInTable()
        {
            var doc = Html(@"<!DOCTYPE html><body><table><tbody><tr><td><math><mi>foo</mi><mi>bar</mi></math><p>baz</td></tr></tbody></table>");

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

            var dochtml1body1table0tbody0tr0td0math0 = dochtml1body1table0tbody0tr0td0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1table0tbody0tr0td0math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0math0.Attributes.Count);
            Assert.AreEqual("math", dochtml1body1table0tbody0tr0td0math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0math0.NodeType);

            var dochtml1body1table0tbody0tr0td0math0mi0 = dochtml1body1table0tbody0tr0td0math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0math0mi0.Attributes.Count);
            Assert.AreEqual("mi", dochtml1body1table0tbody0tr0td0math0mi0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0math0mi0.NodeType);

            var dochtml1body1table0tbody0tr0td0math0mi0Text0 = dochtml1body1table0tbody0tr0td0math0mi0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0tbody0tr0td0math0mi0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1table0tbody0tr0td0math0mi0Text0.TextContent);

            var dochtml1body1table0tbody0tr0td0math0mi1 = dochtml1body1table0tbody0tr0td0math0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0math0mi1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0math0mi1.Attributes.Count);
            Assert.AreEqual("mi", dochtml1body1table0tbody0tr0td0math0mi1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0math0mi1.NodeType);

            var dochtml1body1table0tbody0tr0td0math0mi1Text0 = dochtml1body1table0tbody0tr0td0math0mi1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0tbody0tr0td0math0mi1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1table0tbody0tr0td0math0mi1Text0.TextContent);

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
        public void MathMLInCaptionOfTable()
        {
            var doc = Html(@"<!DOCTYPE html><body><table><caption><math><mi>foo</mi><mi>bar</mi></math><p>baz</caption></table>");

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

            var dochtml1body1table0caption0math0 = dochtml1body1table0caption0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1table0caption0math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0math0.Attributes.Count);
            Assert.AreEqual("math", dochtml1body1table0caption0math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0math0.NodeType);

            var dochtml1body1table0caption0math0mi0 = dochtml1body1table0caption0math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0caption0math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0math0mi0.Attributes.Count);
            Assert.AreEqual("mi", dochtml1body1table0caption0math0mi0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0math0mi0.NodeType);

            var dochtml1body1table0caption0math0mi0Text0 = dochtml1body1table0caption0math0mi0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0caption0math0mi0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1table0caption0math0mi0Text0.TextContent);

            var dochtml1body1table0caption0math0mi1 = dochtml1body1table0caption0math0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1table0caption0math0mi1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0math0mi1.Attributes.Count);
            Assert.AreEqual("mi", dochtml1body1table0caption0math0mi1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0math0mi1.NodeType);

            var dochtml1body1table0caption0math0mi1Text0 = dochtml1body1table0caption0math0mi1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0caption0math0mi1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1table0caption0math0mi1Text0.TextContent);

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
        public void MathMLImplicitlyClosedInTable()
        {
            var doc = Html(@"<!DOCTYPE html><body><table><caption><math><mi>foo</mi><mi>bar</mi><p>baz</table><p>quux");

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

            var dochtml1body1table0caption0math0 = dochtml1body1table0caption0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1table0caption0math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0math0.Attributes.Count);
            Assert.AreEqual("math", dochtml1body1table0caption0math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0math0.NodeType);

            var dochtml1body1table0caption0math0mi0 = dochtml1body1table0caption0math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0caption0math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0math0mi0.Attributes.Count);
            Assert.AreEqual("mi", dochtml1body1table0caption0math0mi0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0math0mi0.NodeType);

            var dochtml1body1table0caption0math0mi0Text0 = dochtml1body1table0caption0math0mi0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0caption0math0mi0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1table0caption0math0mi0Text0.TextContent);

            var dochtml1body1table0caption0math0mi1 = dochtml1body1table0caption0math0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1table0caption0math0mi1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0math0mi1.Attributes.Count);
            Assert.AreEqual("mi", dochtml1body1table0caption0math0mi1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0math0mi1.NodeType);

            var dochtml1body1table0caption0math0mi1Text0 = dochtml1body1table0caption0math0mi1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0caption0math0mi1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1table0caption0math0mi1Text0.TextContent);

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
        public void MathMLInCaptionImplicitlyClosed()
        {
            var doc = Html(@"<!DOCTYPE html><body><table><caption><math><mi>foo</mi><mi>bar</mi>baz</table><p>quux");

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

            var dochtml1body1table0caption0math0 = dochtml1body1table0caption0.ChildNodes[0] as Element;
            Assert.AreEqual(3, dochtml1body1table0caption0math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0math0.Attributes.Count);
            Assert.AreEqual("math", dochtml1body1table0caption0math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0math0.NodeType);

            var dochtml1body1table0caption0math0mi0 = dochtml1body1table0caption0math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0caption0math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0math0mi0.Attributes.Count);
            Assert.AreEqual("mi", dochtml1body1table0caption0math0mi0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0math0mi0.NodeType);

            var dochtml1body1table0caption0math0mi0Text0 = dochtml1body1table0caption0math0mi0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0caption0math0mi0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1table0caption0math0mi0Text0.TextContent);

            var dochtml1body1table0caption0math0mi1 = dochtml1body1table0caption0math0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1table0caption0math0mi1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0math0mi1.Attributes.Count);
            Assert.AreEqual("mi", dochtml1body1table0caption0math0mi1.GetTagName());
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
            Assert.AreEqual("p", dochtml1body1p1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p1.NodeType);

            var dochtml1body1p1Text0 = dochtml1body1p1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1p1Text0.NodeType);
            Assert.AreEqual("quux", dochtml1body1p1Text0.TextContent);
        }

        [Test]
        public void MathMLInColgroupOfTable()
        {
            var doc = Html(@"<!DOCTYPE html><body><table><colgroup><math><mi>foo</mi><mi>bar</mi><p>baz</table><p>quux");

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

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml1body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

            var dochtml1body1math0mi0 = dochtml1body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mi0.Attributes.Count);
            Assert.AreEqual("mi", dochtml1body1math0mi0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi0.NodeType);

            var dochtml1body1math0mi0Text0 = dochtml1body1math0mi0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1math0mi0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1math0mi0Text0.TextContent);

            var dochtml1body1math0mi1 = dochtml1body1math0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1math0mi1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mi1.Attributes.Count);
            Assert.AreEqual("mi", dochtml1body1math0mi1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi1.NodeType);

            var dochtml1body1math0mi1Text0 = dochtml1body1math0mi1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1math0mi1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1math0mi1Text0.TextContent);

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
        public void MathMLInSelectInTable()
        {
            var doc = Html(@"<!DOCTYPE html><body><table><tr><td><select><math><mi>foo</mi><mi>bar</mi><p>baz</table><p>quux");

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
        public void MathMLInSelectInTableImplicitlyClosed()
        {
            var doc = Html(@"<!DOCTYPE html><body><table><select><math><mi>foo</mi><mi>bar</mi><p>baz</table><p>quux");

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
        public void MathMLOutsideDocumentRoot()
        {
            var doc = Html(@"<!DOCTYPE html><body></body></html><math><mi>foo</mi><mi>bar</mi><p>baz");

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

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml1body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

            var dochtml1body1math0mi0 = dochtml1body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mi0.Attributes.Count);
            Assert.AreEqual("mi", dochtml1body1math0mi0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi0.NodeType);

            var dochtml1body1math0mi0Text0 = dochtml1body1math0mi0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1math0mi0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1math0mi0Text0.TextContent);

            var dochtml1body1math0mi1 = dochtml1body1math0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1math0mi1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mi1.Attributes.Count);
            Assert.AreEqual("mi", dochtml1body1math0mi1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi1.NodeType);

            var dochtml1body1math0mi1Text0 = dochtml1body1math0mi1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1math0mi1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1math0mi1Text0.TextContent);

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
        public void MathMLOutsideDocumentImplicitlyClosed()
        {
            var doc = Html(@"<!DOCTYPE html><body></body><math><mi>foo</mi><mi>bar</mi><p>baz");

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

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml1body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

            var dochtml1body1math0mi0 = dochtml1body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mi0.Attributes.Count);
            Assert.AreEqual("mi", dochtml1body1math0mi0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi0.NodeType);

            var dochtml1body1math0mi0Text0 = dochtml1body1math0mi0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1math0mi0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1math0mi0Text0.TextContent);

            var dochtml1body1math0mi1 = dochtml1body1math0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1math0mi1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mi1.Attributes.Count);
            Assert.AreEqual("mi", dochtml1body1math0mi1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi1.NodeType);

            var dochtml1body1math0mi1Text0 = dochtml1body1math0mi1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1math0mi1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1math0mi1Text0.TextContent);

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
        public void MathMLInFrameset()
        {
            var doc = Html(@"<!DOCTYPE html><frameset><math><mi></mi><mi></mi><p><span>");

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
        public void MathMLOutsideFrameset()
        {
            var doc = Html(@"<!DOCTYPE html><frameset></frameset><math><mi></mi><mi></mi><p><span>");

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
        public void MathMLWithXLinkAttributes()
        {
            var doc = Html(@"<!DOCTYPE html><body xlink:href=foo><math xlink:href=foo></math>");

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
            Assert.AreEqual("foo", dochtml1body1.GetAttribute("xlink:href"));

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml1body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml1body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

            var attr = dochtml1body1math0.Attributes.Get("href");
            Assert.IsNotNull(attr);
            Assert.AreEqual("foo", attr.Value);
            Assert.AreEqual(null, attr.Prefix);
            Assert.AreEqual("http://www.w3.org/1999/xlink", attr.NamespaceUri);
        }

        [Test]
        public void MathMLInBodyWithLangAttribute()
        {
            var doc = Html(@"<!DOCTYPE html><body xlink:href=foo xml:lang=en><math><mi xml:lang=en xlink:href=foo></mi></math>");

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
            Assert.AreEqual("foo", dochtml1body1.GetAttribute("xlink:href"));
            Assert.AreEqual("en", dochtml1body1.GetAttribute("xml:lang"));

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml1body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

            var dochtml1body1math0mi0 = dochtml1body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1math0mi0.ChildNodes.Length);
            Assert.AreEqual(2, dochtml1body1math0mi0.Attributes.Count);
            Assert.AreEqual("mi", dochtml1body1math0mi0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi0.NodeType);

            var attr1 = dochtml1body1math0mi0.Attributes.Get("href");
            Assert.IsNotNull(attr1);
            Assert.AreEqual("foo", attr1.Value);
            Assert.AreEqual(null, attr1.Prefix);
            Assert.AreEqual("http://www.w3.org/1999/xlink", attr1.NamespaceUri);

            var attr2 = dochtml1body1math0mi0.Attributes.Get("xml:lang");
            Assert.IsNotNull(attr2);
            Assert.AreEqual("en", attr2.Value);
            Assert.AreEqual("xml", attr2.Prefix);
            Assert.AreEqual("http://www.w3.org/XML/1998/namespace", attr2.NamespaceUri);
        }

        [Test]
        public void MathMLWithMiChild()
        {
            var doc = Html(@"<!DOCTYPE html><body xlink:href=foo xml:lang=en><math><mi xml:lang=en xlink:href=foo /></math>");

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
            Assert.AreEqual("foo", dochtml1body1.GetAttribute("xlink:href"));
            Assert.AreEqual("en", dochtml1body1.GetAttribute("xml:lang"));

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml1body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

            var dochtml1body1math0mi0 = dochtml1body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1math0mi0.ChildNodes.Length);
            Assert.AreEqual(2, dochtml1body1math0mi0.Attributes.Count);
            Assert.AreEqual("mi", dochtml1body1math0mi0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi0.NodeType);

            var attr1 = dochtml1body1math0mi0.Attributes.Get("href");
            Assert.IsNotNull(attr1);
            Assert.AreEqual("foo", attr1.Value);
            Assert.AreEqual(null, attr1.Prefix);
            Assert.AreEqual("http://www.w3.org/1999/xlink", attr1.NamespaceUri);

            var attr2 = dochtml1body1math0mi0.Attributes.Get("xml:lang");
            Assert.IsNotNull(attr2);
            Assert.AreEqual("en", attr2.Value);
            Assert.AreEqual("xml", attr2.Prefix);
            Assert.AreEqual("http://www.w3.org/XML/1998/namespace", attr2.NamespaceUri);
        }

        [Test]
        public void MathMLWithTextNode()
        {
            var doc = Html(@"<!DOCTYPE html><body xlink:href=foo xml:lang=en><math><mi xml:lang=en xlink:href=foo />bar</math>");

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
            Assert.AreEqual("foo", dochtml1body1.GetAttribute("xlink:href"));
            Assert.AreEqual("en", dochtml1body1.GetAttribute("xml:lang"));

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Count);
            Assert.AreEqual("math", dochtml1body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

            var dochtml1body1math0mi0 = dochtml1body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1math0mi0.ChildNodes.Length);
            Assert.AreEqual(2, dochtml1body1math0mi0.Attributes.Count);
            Assert.AreEqual("mi", dochtml1body1math0mi0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi0.NodeType);

            var attr1 = dochtml1body1math0mi0.Attributes.Get("href");
            Assert.IsNotNull(attr1);
            Assert.AreEqual("foo", attr1.Value);
            Assert.AreEqual(null, attr1.Prefix);
            Assert.AreEqual("http://www.w3.org/1999/xlink", attr1.NamespaceUri);

            var attr2 = dochtml1body1math0mi0.Attributes.Get("xml:lang");
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
