namespace AngleSharp.Core.Tests.Html
{
    using AngleSharp.Dom;
    using NUnit.Framework;

    /// <summary>
    /// Tests from https://github.com/html5lib/html5lib-tests:
    /// tree-construction/tests9.dat
    /// </summary>
    [TestFixture]
    public class HtmlWithMathMLTests
    {
        [Test]
        public void MathMLCheckAttributesCaseNormalUnchanged()
        {
            var doc = (@"<!DOCTYPE html><body><math attributeName='' attributeType='' baseFrequency='' baseProfile='' calcMode='' clipPathUnits='' contentScriptType='' contentStyleType='' diffuseConstant='' edgeMode='' externalResourcesRequired='' filterRes='' filterUnits='' glyphRef='' gradientTransform='' gradientUnits='' kernelMatrix='' kernelUnitLength='' keyPoints='' keySplines='' keyTimes='' lengthAdjust='' limitingConeAngle='' markerHeight='' markerUnits='' markerWidth='' maskContentUnits='' maskUnits='' numOctaves='' pathLength='' patternContentUnits='' patternTransform='' patternUnits='' pointsAtX='' pointsAtY='' pointsAtZ='' preserveAlpha='' preserveAspectRatio='' primitiveUnits='' refX='' refY='' repeatCount='' repeatDur='' requiredExtensions='' requiredFeatures='' specularConstant='' specularExponent='' spreadMethod='' startOffset='' stdDeviation='' stitchTiles='' surfaceScale='' systemLanguage='' tableValues='' targetX='' targetY='' textLength='' viewBox='' viewTarget='' xChannelSelector='' yChannelSelector='' zoomAndPan=''></math>").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(62, dochtml1body1math0.Attributes.Length);
            Assert.AreEqual("math", dochtml1body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("attributename"));
            Assert.AreEqual("attributename", dochtml1body1math0.Attributes.GetNamedItem("attributename").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("attributename").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("attributetype"));
            Assert.AreEqual("attributetype", dochtml1body1math0.Attributes.GetNamedItem("attributetype").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("attributetype").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("basefrequency"));
            Assert.AreEqual("basefrequency", dochtml1body1math0.Attributes.GetNamedItem("basefrequency").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("basefrequency").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("baseprofile"));
            Assert.AreEqual("baseprofile", dochtml1body1math0.Attributes.GetNamedItem("baseprofile").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("baseprofile").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("calcmode"));
            Assert.AreEqual("calcmode", dochtml1body1math0.Attributes.GetNamedItem("calcmode").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("calcmode").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("clippathunits"));
            Assert.AreEqual("clippathunits", dochtml1body1math0.Attributes.GetNamedItem("clippathunits").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("clippathunits").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("contentscripttype"));
            Assert.AreEqual("contentscripttype", dochtml1body1math0.Attributes.GetNamedItem("contentscripttype").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("contentscripttype").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("contentstyletype"));
            Assert.AreEqual("contentstyletype", dochtml1body1math0.Attributes.GetNamedItem("contentstyletype").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("contentstyletype").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("diffuseconstant"));
            Assert.AreEqual("diffuseconstant", dochtml1body1math0.Attributes.GetNamedItem("diffuseconstant").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("diffuseconstant").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("edgemode"));
            Assert.AreEqual("edgemode", dochtml1body1math0.Attributes.GetNamedItem("edgemode").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("edgemode").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("externalresourcesrequired"));
            Assert.AreEqual("externalresourcesrequired", dochtml1body1math0.Attributes.GetNamedItem("externalresourcesrequired").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("externalresourcesrequired").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("filterres"));
            Assert.AreEqual("filterres", dochtml1body1math0.Attributes.GetNamedItem("filterres").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("filterres").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("filterunits"));
            Assert.AreEqual("filterunits", dochtml1body1math0.Attributes.GetNamedItem("filterunits").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("filterunits").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("glyphref"));
            Assert.AreEqual("glyphref", dochtml1body1math0.Attributes.GetNamedItem("glyphref").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("glyphref").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("gradienttransform"));
            Assert.AreEqual("gradienttransform", dochtml1body1math0.Attributes.GetNamedItem("gradienttransform").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("gradienttransform").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("gradientunits"));
            Assert.AreEqual("gradientunits", dochtml1body1math0.Attributes.GetNamedItem("gradientunits").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("gradientunits").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("kernelmatrix"));
            Assert.AreEqual("kernelmatrix", dochtml1body1math0.Attributes.GetNamedItem("kernelmatrix").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("kernelmatrix").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("kernelunitlength"));
            Assert.AreEqual("kernelunitlength", dochtml1body1math0.Attributes.GetNamedItem("kernelunitlength").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("kernelunitlength").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("keypoints"));
            Assert.AreEqual("keypoints", dochtml1body1math0.Attributes.GetNamedItem("keypoints").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("keypoints").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("keysplines"));
            Assert.AreEqual("keysplines", dochtml1body1math0.Attributes.GetNamedItem("keysplines").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("keysplines").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("keytimes"));
            Assert.AreEqual("keytimes", dochtml1body1math0.Attributes.GetNamedItem("keytimes").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("keytimes").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("lengthadjust"));
            Assert.AreEqual("lengthadjust", dochtml1body1math0.Attributes.GetNamedItem("lengthadjust").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("lengthadjust").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("limitingconeangle"));
            Assert.AreEqual("limitingconeangle", dochtml1body1math0.Attributes.GetNamedItem("limitingconeangle").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("limitingconeangle").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("markerheight"));
            Assert.AreEqual("markerheight", dochtml1body1math0.Attributes.GetNamedItem("markerheight").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("markerheight").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("markerunits"));
            Assert.AreEqual("markerunits", dochtml1body1math0.Attributes.GetNamedItem("markerunits").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("markerunits").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("markerwidth"));
            Assert.AreEqual("markerwidth", dochtml1body1math0.Attributes.GetNamedItem("markerwidth").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("markerwidth").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("maskcontentunits"));
            Assert.AreEqual("maskcontentunits", dochtml1body1math0.Attributes.GetNamedItem("maskcontentunits").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("maskcontentunits").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("maskunits"));
            Assert.AreEqual("maskunits", dochtml1body1math0.Attributes.GetNamedItem("maskunits").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("maskunits").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("numoctaves"));
            Assert.AreEqual("numoctaves", dochtml1body1math0.Attributes.GetNamedItem("numoctaves").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("numoctaves").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("pathlength"));
            Assert.AreEqual("pathlength", dochtml1body1math0.Attributes.GetNamedItem("pathlength").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("pathlength").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("patterncontentunits"));
            Assert.AreEqual("patterncontentunits", dochtml1body1math0.Attributes.GetNamedItem("patterncontentunits").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("patterncontentunits").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("patterntransform"));
            Assert.AreEqual("patterntransform", dochtml1body1math0.Attributes.GetNamedItem("patterntransform").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("patterntransform").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("patternunits"));
            Assert.AreEqual("patternunits", dochtml1body1math0.Attributes.GetNamedItem("patternunits").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("patternunits").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("pointsatx"));
            Assert.AreEqual("pointsatx", dochtml1body1math0.Attributes.GetNamedItem("pointsatx").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("pointsatx").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("pointsaty"));
            Assert.AreEqual("pointsaty", dochtml1body1math0.Attributes.GetNamedItem("pointsaty").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("pointsaty").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("pointsatz"));
            Assert.AreEqual("pointsatz", dochtml1body1math0.Attributes.GetNamedItem("pointsatz").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("pointsatz").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("preservealpha"));
            Assert.AreEqual("preservealpha", dochtml1body1math0.Attributes.GetNamedItem("preservealpha").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("preservealpha").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("preserveaspectratio"));
            Assert.AreEqual("preserveaspectratio", dochtml1body1math0.Attributes.GetNamedItem("preserveaspectratio").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("preserveaspectratio").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("primitiveunits"));
            Assert.AreEqual("primitiveunits", dochtml1body1math0.Attributes.GetNamedItem("primitiveunits").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("primitiveunits").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("refx"));
            Assert.AreEqual("refx", dochtml1body1math0.Attributes.GetNamedItem("refx").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("refx").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("refy"));
            Assert.AreEqual("refy", dochtml1body1math0.Attributes.GetNamedItem("refy").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("refy").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("repeatcount"));
            Assert.AreEqual("repeatcount", dochtml1body1math0.Attributes.GetNamedItem("repeatcount").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("repeatcount").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("repeatdur"));
            Assert.AreEqual("repeatdur", dochtml1body1math0.Attributes.GetNamedItem("repeatdur").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("repeatdur").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("requiredextensions"));
            Assert.AreEqual("requiredextensions", dochtml1body1math0.Attributes.GetNamedItem("requiredextensions").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("requiredextensions").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("requiredfeatures"));
            Assert.AreEqual("requiredfeatures", dochtml1body1math0.Attributes.GetNamedItem("requiredfeatures").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("requiredfeatures").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("specularconstant"));
            Assert.AreEqual("specularconstant", dochtml1body1math0.Attributes.GetNamedItem("specularconstant").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("specularconstant").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("specularexponent"));
            Assert.AreEqual("specularexponent", dochtml1body1math0.Attributes.GetNamedItem("specularexponent").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("specularexponent").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("spreadmethod"));
            Assert.AreEqual("spreadmethod", dochtml1body1math0.Attributes.GetNamedItem("spreadmethod").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("spreadmethod").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("startoffset"));
            Assert.AreEqual("startoffset", dochtml1body1math0.Attributes.GetNamedItem("startoffset").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("startoffset").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("stddeviation"));
            Assert.AreEqual("stddeviation", dochtml1body1math0.Attributes.GetNamedItem("stddeviation").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("stddeviation").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("stitchtiles"));
            Assert.AreEqual("stitchtiles", dochtml1body1math0.Attributes.GetNamedItem("stitchtiles").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("stitchtiles").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("surfacescale"));
            Assert.AreEqual("surfacescale", dochtml1body1math0.Attributes.GetNamedItem("surfacescale").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("surfacescale").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("systemlanguage"));
            Assert.AreEqual("systemlanguage", dochtml1body1math0.Attributes.GetNamedItem("systemlanguage").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("systemlanguage").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("tablevalues"));
            Assert.AreEqual("tablevalues", dochtml1body1math0.Attributes.GetNamedItem("tablevalues").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("tablevalues").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("targetx"));
            Assert.AreEqual("targetx", dochtml1body1math0.Attributes.GetNamedItem("targetx").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("targetx").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("targety"));
            Assert.AreEqual("targety", dochtml1body1math0.Attributes.GetNamedItem("targety").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("targety").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("textlength"));
            Assert.AreEqual("textlength", dochtml1body1math0.Attributes.GetNamedItem("textlength").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("textlength").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("viewbox"));
            Assert.AreEqual("viewbox", dochtml1body1math0.Attributes.GetNamedItem("viewbox").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("viewbox").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("viewtarget"));
            Assert.AreEqual("viewtarget", dochtml1body1math0.Attributes.GetNamedItem("viewtarget").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("viewtarget").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("xchannelselector"));
            Assert.AreEqual("xchannelselector", dochtml1body1math0.Attributes.GetNamedItem("xchannelselector").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("xchannelselector").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("ychannelselector"));
            Assert.AreEqual("ychannelselector", dochtml1body1math0.Attributes.GetNamedItem("ychannelselector").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("ychannelselector").Value);
            Assert.IsNotNull(dochtml1body1math0.Attributes.GetNamedItem("zoomandpan"));
            Assert.AreEqual("zoomandpan", dochtml1body1math0.Attributes.GetNamedItem("zoomandpan").Name);
            Assert.AreEqual("", dochtml1body1math0.Attributes.GetNamedItem("zoomandpan").Value);
        }

        [Test]
        public void MathMLCheckTagCaseNormalUnchanged()
        {
            var doc = (@"<!DOCTYPE html><body><math><altGlyph /><altGlyphDef /><altGlyphItem /><animateColor /><animateMotion /><animateTransform /><clipPath /><feBlend /><feColorMatrix /><feComponentTransfer /><feComposite /><feConvolveMatrix /><feDiffuseLighting /><feDisplacementMap /><feDistantLight /><feFlood /><feFuncA /><feFuncB /><feFuncG /><feFuncR /><feGaussianBlur /><feImage /><feMerge /><feMergeNode /><feMorphology /><feOffset /><fePointLight /><feSpecularLighting /><feSpotLight /><feTile /><feTurbulence /><foreignObject /><glyphRef /><linearGradient /><radialGradient /><textPath /></math>").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(36, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Length);
            Assert.AreEqual("math", dochtml1body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

            var dochtml1body1math0altglyph0 = dochtml1body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1math0altglyph0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0altglyph0.Attributes.Length);
            Assert.AreEqual("altglyph", dochtml1body1math0altglyph0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0altglyph0.NodeType);

            var dochtml1body1math0altglyphdef1 = dochtml1body1math0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1math0altglyphdef1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0altglyphdef1.Attributes.Length);
            Assert.AreEqual("altglyphdef", dochtml1body1math0altglyphdef1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0altglyphdef1.NodeType);

            var dochtml1body1math0altglyphitem2 = dochtml1body1math0.ChildNodes[2] as Element;
            Assert.AreEqual(0, dochtml1body1math0altglyphitem2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0altglyphitem2.Attributes.Length);
            Assert.AreEqual("altglyphitem", dochtml1body1math0altglyphitem2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0altglyphitem2.NodeType);

            var dochtml1body1math0animatecolor3 = dochtml1body1math0.ChildNodes[3] as Element;
            Assert.AreEqual(0, dochtml1body1math0animatecolor3.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0animatecolor3.Attributes.Length);
            Assert.AreEqual("animatecolor", dochtml1body1math0animatecolor3.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0animatecolor3.NodeType);

            var dochtml1body1math0animatemotion4 = dochtml1body1math0.ChildNodes[4] as Element;
            Assert.AreEqual(0, dochtml1body1math0animatemotion4.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0animatemotion4.Attributes.Length);
            Assert.AreEqual("animatemotion", dochtml1body1math0animatemotion4.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0animatemotion4.NodeType);

            var dochtml1body1math0animatetransform5 = dochtml1body1math0.ChildNodes[5] as Element;
            Assert.AreEqual(0, dochtml1body1math0animatetransform5.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0animatetransform5.Attributes.Length);
            Assert.AreEqual("animatetransform", dochtml1body1math0animatetransform5.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0animatetransform5.NodeType);

            var dochtml1body1math0clippath6 = dochtml1body1math0.ChildNodes[6] as Element;
            Assert.AreEqual(0, dochtml1body1math0clippath6.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0clippath6.Attributes.Length);
            Assert.AreEqual("clippath", dochtml1body1math0clippath6.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0clippath6.NodeType);

            var dochtml1body1math0feblend7 = dochtml1body1math0.ChildNodes[7] as Element;
            Assert.AreEqual(0, dochtml1body1math0feblend7.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0feblend7.Attributes.Length);
            Assert.AreEqual("feblend", dochtml1body1math0feblend7.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0feblend7.NodeType);

            var dochtml1body1math0fecolormatrix8 = dochtml1body1math0.ChildNodes[8] as Element;
            Assert.AreEqual(0, dochtml1body1math0fecolormatrix8.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0fecolormatrix8.Attributes.Length);
            Assert.AreEqual("fecolormatrix", dochtml1body1math0fecolormatrix8.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0fecolormatrix8.NodeType);

            var dochtml1body1math0fecomponenttransfer9 = dochtml1body1math0.ChildNodes[9] as Element;
            Assert.AreEqual(0, dochtml1body1math0fecomponenttransfer9.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0fecomponenttransfer9.Attributes.Length);
            Assert.AreEqual("fecomponenttransfer", dochtml1body1math0fecomponenttransfer9.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0fecomponenttransfer9.NodeType);

            var dochtml1body1math0fecomposite10 = dochtml1body1math0.ChildNodes[10] as Element;
            Assert.AreEqual(0, dochtml1body1math0fecomposite10.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0fecomposite10.Attributes.Length);
            Assert.AreEqual("fecomposite", dochtml1body1math0fecomposite10.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0fecomposite10.NodeType);

            var dochtml1body1math0feconvolvematrix11 = dochtml1body1math0.ChildNodes[11] as Element;
            Assert.AreEqual(0, dochtml1body1math0feconvolvematrix11.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0feconvolvematrix11.Attributes.Length);
            Assert.AreEqual("feconvolvematrix", dochtml1body1math0feconvolvematrix11.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0feconvolvematrix11.NodeType);

            var dochtml1body1math0fediffuselighting12 = dochtml1body1math0.ChildNodes[12] as Element;
            Assert.AreEqual(0, dochtml1body1math0fediffuselighting12.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0fediffuselighting12.Attributes.Length);
            Assert.AreEqual("fediffuselighting", dochtml1body1math0fediffuselighting12.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0fediffuselighting12.NodeType);

            var dochtml1body1math0fedisplacementmap13 = dochtml1body1math0.ChildNodes[13] as Element;
            Assert.AreEqual(0, dochtml1body1math0fedisplacementmap13.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0fedisplacementmap13.Attributes.Length);
            Assert.AreEqual("fedisplacementmap", dochtml1body1math0fedisplacementmap13.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0fedisplacementmap13.NodeType);

            var dochtml1body1math0fedistantlight14 = dochtml1body1math0.ChildNodes[14] as Element;
            Assert.AreEqual(0, dochtml1body1math0fedistantlight14.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0fedistantlight14.Attributes.Length);
            Assert.AreEqual("fedistantlight", dochtml1body1math0fedistantlight14.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0fedistantlight14.NodeType);

            var dochtml1body1math0feflood15 = dochtml1body1math0.ChildNodes[15] as Element;
            Assert.AreEqual(0, dochtml1body1math0feflood15.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0feflood15.Attributes.Length);
            Assert.AreEqual("feflood", dochtml1body1math0feflood15.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0feflood15.NodeType);

            var dochtml1body1math0fefunca16 = dochtml1body1math0.ChildNodes[16] as Element;
            Assert.AreEqual(0, dochtml1body1math0fefunca16.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0fefunca16.Attributes.Length);
            Assert.AreEqual("fefunca", dochtml1body1math0fefunca16.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0fefunca16.NodeType);

            var dochtml1body1math0fefuncb17 = dochtml1body1math0.ChildNodes[17] as Element;
            Assert.AreEqual(0, dochtml1body1math0fefuncb17.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0fefuncb17.Attributes.Length);
            Assert.AreEqual("fefuncb", dochtml1body1math0fefuncb17.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0fefuncb17.NodeType);

            var dochtml1body1math0fefuncg18 = dochtml1body1math0.ChildNodes[18] as Element;
            Assert.AreEqual(0, dochtml1body1math0fefuncg18.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0fefuncg18.Attributes.Length);
            Assert.AreEqual("fefuncg", dochtml1body1math0fefuncg18.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0fefuncg18.NodeType);

            var dochtml1body1math0fefuncr19 = dochtml1body1math0.ChildNodes[19] as Element;
            Assert.AreEqual(0, dochtml1body1math0fefuncr19.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0fefuncr19.Attributes.Length);
            Assert.AreEqual("fefuncr", dochtml1body1math0fefuncr19.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0fefuncr19.NodeType);

            var dochtml1body1math0fegaussianblur20 = dochtml1body1math0.ChildNodes[20] as Element;
            Assert.AreEqual(0, dochtml1body1math0fegaussianblur20.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0fegaussianblur20.Attributes.Length);
            Assert.AreEqual("fegaussianblur", dochtml1body1math0fegaussianblur20.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0fegaussianblur20.NodeType);

            var dochtml1body1math0feimage21 = dochtml1body1math0.ChildNodes[21] as Element;
            Assert.AreEqual(0, dochtml1body1math0feimage21.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0feimage21.Attributes.Length);
            Assert.AreEqual("feimage", dochtml1body1math0feimage21.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0feimage21.NodeType);

            var dochtml1body1math0femerge22 = dochtml1body1math0.ChildNodes[22] as Element;
            Assert.AreEqual(0, dochtml1body1math0femerge22.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0femerge22.Attributes.Length);
            Assert.AreEqual("femerge", dochtml1body1math0femerge22.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0femerge22.NodeType);

            var dochtml1body1math0femergenode23 = dochtml1body1math0.ChildNodes[23] as Element;
            Assert.AreEqual(0, dochtml1body1math0femergenode23.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0femergenode23.Attributes.Length);
            Assert.AreEqual("femergenode", dochtml1body1math0femergenode23.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0femergenode23.NodeType);

            var dochtml1body1math0femorphology24 = dochtml1body1math0.ChildNodes[24] as Element;
            Assert.AreEqual(0, dochtml1body1math0femorphology24.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0femorphology24.Attributes.Length);
            Assert.AreEqual("femorphology", dochtml1body1math0femorphology24.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0femorphology24.NodeType);

            var dochtml1body1math0feoffset25 = dochtml1body1math0.ChildNodes[25] as Element;
            Assert.AreEqual(0, dochtml1body1math0feoffset25.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0feoffset25.Attributes.Length);
            Assert.AreEqual("feoffset", dochtml1body1math0feoffset25.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0feoffset25.NodeType);

            var dochtml1body1math0fepointlight26 = dochtml1body1math0.ChildNodes[26] as Element;
            Assert.AreEqual(0, dochtml1body1math0fepointlight26.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0fepointlight26.Attributes.Length);
            Assert.AreEqual("fepointlight", dochtml1body1math0fepointlight26.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0fepointlight26.NodeType);

            var dochtml1body1math0fespecularlighting27 = dochtml1body1math0.ChildNodes[27] as Element;
            Assert.AreEqual(0, dochtml1body1math0fespecularlighting27.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0fespecularlighting27.Attributes.Length);
            Assert.AreEqual("fespecularlighting", dochtml1body1math0fespecularlighting27.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0fespecularlighting27.NodeType);

            var dochtml1body1math0fespotlight28 = dochtml1body1math0.ChildNodes[28] as Element;
            Assert.AreEqual(0, dochtml1body1math0fespotlight28.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0fespotlight28.Attributes.Length);
            Assert.AreEqual("fespotlight", dochtml1body1math0fespotlight28.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0fespotlight28.NodeType);

            var dochtml1body1math0fetile29 = dochtml1body1math0.ChildNodes[29] as Element;
            Assert.AreEqual(0, dochtml1body1math0fetile29.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0fetile29.Attributes.Length);
            Assert.AreEqual("fetile", dochtml1body1math0fetile29.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0fetile29.NodeType);

            var dochtml1body1math0feturbulence30 = dochtml1body1math0.ChildNodes[30] as Element;
            Assert.AreEqual(0, dochtml1body1math0feturbulence30.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0feturbulence30.Attributes.Length);
            Assert.AreEqual("feturbulence", dochtml1body1math0feturbulence30.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0feturbulence30.NodeType);

            var dochtml1body1math0foreignobject31 = dochtml1body1math0.ChildNodes[31] as Element;
            Assert.AreEqual(0, dochtml1body1math0foreignobject31.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0foreignobject31.Attributes.Length);
            Assert.AreEqual("foreignobject", dochtml1body1math0foreignobject31.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0foreignobject31.NodeType);

            var dochtml1body1math0glyphref32 = dochtml1body1math0.ChildNodes[32] as Element;
            Assert.AreEqual(0, dochtml1body1math0glyphref32.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0glyphref32.Attributes.Length);
            Assert.AreEqual("glyphref", dochtml1body1math0glyphref32.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0glyphref32.NodeType);

            var dochtml1body1math0lineargradient33 = dochtml1body1math0.ChildNodes[33] as Element;
            Assert.AreEqual(0, dochtml1body1math0lineargradient33.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0lineargradient33.Attributes.Length);
            Assert.AreEqual("lineargradient", dochtml1body1math0lineargradient33.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0lineargradient33.NodeType);

            var dochtml1body1math0radialgradient34 = dochtml1body1math0.ChildNodes[34] as Element;
            Assert.AreEqual(0, dochtml1body1math0radialgradient34.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0radialgradient34.Attributes.Length);
            Assert.AreEqual("radialgradient", dochtml1body1math0radialgradient34.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0radialgradient34.NodeType);

            var dochtml1body1math0textpath35 = dochtml1body1math0.ChildNodes[35] as Element;
            Assert.AreEqual(0, dochtml1body1math0textpath35.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0textpath35.Attributes.Length);
            Assert.AreEqual("textpath", dochtml1body1math0textpath35.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0textpath35.NodeType);
        }

        [Test]
        public void MathMLSingleElement()
        {
            var doc = (@"<!DOCTYPE html><math></math>").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Length);
            Assert.AreEqual("math", dochtml1body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);
        }

        [Test]
        public void MathMLSingleElementInBody()
        {
            var doc = (@"<!DOCTYPE html><body><math></math>").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Length);
            Assert.AreEqual("math", dochtml1body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);
        }

        [Test]
        public void MathMLElementWithDivAndObjectElements()
        {
            var doc = (@"<math><mi><div><object><div><span></span></div></object></div></mi><mi>").ToHtmlDocument();

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1math0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0.Attributes.Length);
            Assert.AreEqual("math", dochtml0body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0.NodeType);

            var dochtml0body1math0mi0 = dochtml0body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mi0.Attributes.Length);
            Assert.AreEqual("mi", dochtml0body1math0mi0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mi0.NodeType);

            var dochtml0body1math0mi0div0 = dochtml0body1math0mi0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0mi0div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mi0div0.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1math0mi0div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mi0div0.NodeType);

            var dochtml0body1math0mi0div0object0 = dochtml0body1math0mi0div0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0mi0div0object0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mi0div0object0.Attributes.Length);
            Assert.AreEqual("object", dochtml0body1math0mi0div0object0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mi0div0object0.NodeType);

            var dochtml0body1math0mi0div0object0div0 = dochtml0body1math0mi0div0object0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0mi0div0object0div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mi0div0object0div0.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1math0mi0div0object0div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mi0div0object0div0.NodeType);

            var dochtml0body1math0mi0div0object0div0span0 = dochtml0body1math0mi0div0object0div0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1math0mi0div0object0div0span0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mi0div0object0div0span0.Attributes.Length);
            Assert.AreEqual("span", dochtml0body1math0mi0div0object0div0span0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mi0div0object0div0span0.NodeType);

            var dochtml0body1math0mi1 = dochtml0body1math0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1math0mi1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mi1.Attributes.Length);
            Assert.AreEqual("mi", dochtml0body1math0mi1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mi1.NodeType);
        }

        [Test]
        public void MathMLElementWithSvgChild()
        {
            var doc = (@"<math><mi><svg><foreignObject><div><div></div></div></foreignObject></svg></mi><mi>").ToHtmlDocument();

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1math0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0.Attributes.Length);
            Assert.AreEqual("math", dochtml0body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0.NodeType);

            var dochtml0body1math0mi0 = dochtml0body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mi0.Attributes.Length);
            Assert.AreEqual("mi", dochtml0body1math0mi0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mi0.NodeType);

            var dochtml0body1math0mi0svg0 = dochtml0body1math0mi0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0mi0svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mi0svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml0body1math0mi0svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mi0svg0.NodeType);

            var dochtml0body1math0mi0svg0foreignObject0 = dochtml0body1math0mi0svg0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0mi0svg0foreignObject0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mi0svg0foreignObject0.Attributes.Length);
            Assert.AreEqual("foreignObject", dochtml0body1math0mi0svg0foreignObject0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mi0svg0foreignObject0.NodeType);

            var dochtml0body1math0mi0svg0foreignObject0div0 = dochtml0body1math0mi0svg0foreignObject0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0mi0svg0foreignObject0div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mi0svg0foreignObject0div0.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1math0mi0svg0foreignObject0div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mi0svg0foreignObject0div0.NodeType);

            var dochtml0body1math0mi0svg0foreignObject0div0div0 = dochtml0body1math0mi0svg0foreignObject0div0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1math0mi0svg0foreignObject0div0div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mi0svg0foreignObject0div0div0.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1math0mi0svg0foreignObject0div0div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mi0svg0foreignObject0div0div0.NodeType);

            var dochtml0body1math0mi1 = dochtml0body1math0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1math0mi1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mi1.Attributes.Length);
            Assert.AreEqual("mi", dochtml0body1math0mi1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mi1.NodeType);
        }

        [Test]
        public void MathMLSingleElementWithChild()
        {
            var doc = (@"<!DOCTYPE html><math><mi>").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Length);
            Assert.AreEqual("math", dochtml1body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

            var dochtml1body1math0mi0 = dochtml1body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mi0.Attributes.Length);
            Assert.AreEqual("mi", dochtml1body1math0mi0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi0.NodeType);
        }

        [Test]
        public void MathMLWithMiAndMglyphElements()
        {
            var doc = (@"<math><mi><mglyph>").ToHtmlDocument();

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1math0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0.Attributes.Length);
            Assert.AreEqual("math", dochtml0body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0.NodeType);

            var dochtml0body1math0mi0 = dochtml0body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mi0.Attributes.Length);
            Assert.AreEqual("mi", dochtml0body1math0mi0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mi0.NodeType);

            var dochtml0body1math0mi0mglyph0 = dochtml0body1math0mi0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1math0mi0mglyph0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mi0mglyph0.Attributes.Length);
            Assert.AreEqual("mglyph", dochtml0body1math0mi0mglyph0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mi0mglyph0.NodeType);
        }

        [Test]
        public void MathMLWithMiAndMalignmarkElements()
        {
            var doc = (@"<math><mi><malignmark>").ToHtmlDocument();

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1math0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0.Attributes.Length);
            Assert.AreEqual("math", dochtml0body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0.NodeType);

            var dochtml0body1math0mi0 = dochtml0body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mi0.Attributes.Length);
            Assert.AreEqual("mi", dochtml0body1math0mi0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mi0.NodeType);

            var dochtml0body1math0mi0malignmark0 = dochtml0body1math0mi0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1math0mi0malignmark0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mi0malignmark0.Attributes.Length);
            Assert.AreEqual("malignmark", dochtml0body1math0mi0malignmark0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mi0malignmark0.NodeType);
        }

        [Test]
        public void MathMLWithMoAndMglyphElements()
        {
            var doc = (@"<math><mo><mglyph>").ToHtmlDocument();

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1math0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0.Attributes.Length);
            Assert.AreEqual("math", dochtml0body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0.NodeType);

            var dochtml0body1math0mo0 = dochtml0body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0mo0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mo0.Attributes.Length);
            Assert.AreEqual("mo", dochtml0body1math0mo0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mo0.NodeType);

            var dochtml0body1math0mo0mglyph0 = dochtml0body1math0mo0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1math0mo0mglyph0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mo0mglyph0.Attributes.Length);
            Assert.AreEqual("mglyph", dochtml0body1math0mo0mglyph0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mo0mglyph0.NodeType);
        }

        [Test]
        public void MathMLWithMoAndMalignmarkElements()
        {
            var doc = (@"<math><mo><malignmark>").ToHtmlDocument();

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1math0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0.Attributes.Length);
            Assert.AreEqual("math", dochtml0body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0.NodeType);

            var dochtml0body1math0mo0 = dochtml0body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0mo0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mo0.Attributes.Length);
            Assert.AreEqual("mo", dochtml0body1math0mo0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mo0.NodeType);

            var dochtml0body1math0mo0malignmark0 = dochtml0body1math0mo0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1math0mo0malignmark0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mo0malignmark0.Attributes.Length);
            Assert.AreEqual("malignmark", dochtml0body1math0mo0malignmark0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mo0malignmark0.NodeType);
        }

        [Test]
        public void MathMLWithMnAndMglyphElements()
        {
            var doc = (@"<math><mn><mglyph>").ToHtmlDocument();

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1math0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0.Attributes.Length);
            Assert.AreEqual("math", dochtml0body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0.NodeType);

            var dochtml0body1math0mn0 = dochtml0body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0mn0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mn0.Attributes.Length);
            Assert.AreEqual("mn", dochtml0body1math0mn0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mn0.NodeType);

            var dochtml0body1math0mn0mglyph0 = dochtml0body1math0mn0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1math0mn0mglyph0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mn0mglyph0.Attributes.Length);
            Assert.AreEqual("mglyph", dochtml0body1math0mn0mglyph0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mn0mglyph0.NodeType);
        }

        [Test]
        public void MathMLWithMnAndMalignmarkElements()
        {
            var doc = (@"<math><mn><malignmark>").ToHtmlDocument();

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1math0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0.Attributes.Length);
            Assert.AreEqual("math", dochtml0body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0.NodeType);

            var dochtml0body1math0mn0 = dochtml0body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0mn0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mn0.Attributes.Length);
            Assert.AreEqual("mn", dochtml0body1math0mn0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mn0.NodeType);

            var dochtml0body1math0mn0malignmark0 = dochtml0body1math0mn0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1math0mn0malignmark0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mn0malignmark0.Attributes.Length);
            Assert.AreEqual("malignmark", dochtml0body1math0mn0malignmark0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mn0malignmark0.NodeType);
        }

        [Test]
        public void MathMLWithMsAndMglyphElements()
        {
            var doc = (@"<math><ms><mglyph>").ToHtmlDocument();

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1math0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0.Attributes.Length);
            Assert.AreEqual("math", dochtml0body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0.NodeType);

            var dochtml0body1math0ms0 = dochtml0body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0ms0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0ms0.Attributes.Length);
            Assert.AreEqual("ms", dochtml0body1math0ms0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0ms0.NodeType);

            var dochtml0body1math0ms0mglyph0 = dochtml0body1math0ms0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1math0ms0mglyph0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0ms0mglyph0.Attributes.Length);
            Assert.AreEqual("mglyph", dochtml0body1math0ms0mglyph0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0ms0mglyph0.NodeType);
        }

        [Test]
        public void MathMLWithMsAndMalignmarkElements()
        {
            var doc = (@"<math><ms><malignmark>").ToHtmlDocument();

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1math0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0.Attributes.Length);
            Assert.AreEqual("math", dochtml0body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0.NodeType);

            var dochtml0body1math0ms0 = dochtml0body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0ms0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0ms0.Attributes.Length);
            Assert.AreEqual("ms", dochtml0body1math0ms0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0ms0.NodeType);

            var dochtml0body1math0ms0malignmark0 = dochtml0body1math0ms0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1math0ms0malignmark0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0ms0malignmark0.Attributes.Length);
            Assert.AreEqual("malignmark", dochtml0body1math0ms0malignmark0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0ms0malignmark0.NodeType);
        }

        [Test]
        public void MathMLWithMtextAndMglyphElements()
        {
            var doc = (@"<math><mtext><mglyph>").ToHtmlDocument();

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1math0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0.Attributes.Length);
            Assert.AreEqual("math", dochtml0body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0.NodeType);

            var dochtml0body1math0mtext0 = dochtml0body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0mtext0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mtext0.Attributes.Length);
            Assert.AreEqual("mtext", dochtml0body1math0mtext0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mtext0.NodeType);

            var dochtml0body1math0mtext0mglyph0 = dochtml0body1math0mtext0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1math0mtext0mglyph0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mtext0mglyph0.Attributes.Length);
            Assert.AreEqual("mglyph", dochtml0body1math0mtext0mglyph0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mtext0mglyph0.NodeType);
        }

        [Test]
        public void MathMLWithMtextAndMalignmarkElements()
        {
            var doc = (@"<math><mtext><malignmark>").ToHtmlDocument();

            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);

            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);

            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1math0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0.Attributes.Length);
            Assert.AreEqual("math", dochtml0body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0.NodeType);

            var dochtml0body1math0mtext0 = dochtml0body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0mtext0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mtext0.Attributes.Length);
            Assert.AreEqual("mtext", dochtml0body1math0mtext0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mtext0.NodeType);

            var dochtml0body1math0mtext0malignmark0 = dochtml0body1math0mtext0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1math0mtext0malignmark0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mtext0malignmark0.Attributes.Length);
            Assert.AreEqual("malignmark", dochtml0body1math0mtext0malignmark0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mtext0malignmark0.NodeType);
        }

        [Test]
        public void MathMLAnnotationXmlWithSvgInside()
        {
            var doc = (@"<!DOCTYPE html><math><annotation-xml><svg><u>").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Length);
            Assert.AreEqual("math", dochtml1body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);
            var dochtml1body1math0annotationxml0 = dochtml1body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1math0annotationxml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0annotationxml0.Attributes.Length);
            Assert.AreEqual("annotation-xml", dochtml1body1math0annotationxml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0annotationxml0.NodeType);

            var dochtml1body1math0annotationxml0svg0 = dochtml1body1math0annotationxml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1math0annotationxml0svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0annotationxml0svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml1body1math0annotationxml0svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0annotationxml0svg0.NodeType);

            var dochtml1body1u1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1u1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1u1.Attributes.Length);
            Assert.AreEqual("u", dochtml1body1u1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1u1.NodeType);
        }

        [Test]
        public void MathMLElementInSelect()
        {
            var doc = (@"<!DOCTYPE html><body><select><math></math></select>").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1select0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1select0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1select0.Attributes.Length);
            Assert.AreEqual("select", dochtml1body1select0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1select0.NodeType);
        }

        [Test]
        public void MathMLInOptionOfSelect()
        {
            var doc = (@"<!DOCTYPE html><body><select><option><math></math></option></select>").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1select0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1select0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1select0.Attributes.Length);
            Assert.AreEqual("select", dochtml1body1select0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1select0.NodeType);

            var dochtml1body1select0option0 = dochtml1body1select0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1select0option0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1select0option0.Attributes.Length);
            Assert.AreEqual("option", dochtml1body1select0option0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1select0option0.NodeType);
        }

        [Test]
        public void MathMLInTable()
        {
            var doc = (@"<!DOCTYPE html><body><table><math></math></table>").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Length);
            Assert.AreEqual("math", dochtml1body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

            var dochtml1body1table1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table1.Attributes.Length);
            Assert.AreEqual("table", dochtml1body1table1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table1.NodeType);
        }

        [Test]
        public void MathMLWithChildInTable()
        {
            var doc = (@"<!DOCTYPE html><body><table><math><mi>foo</mi></math></table>").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Length);
            Assert.AreEqual("math", dochtml1body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

            var dochtml1body1math0mi0 = dochtml1body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mi0.Attributes.Length);
            Assert.AreEqual("mi", dochtml1body1math0mi0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi0.NodeType);

            var dochtml1body1math0mi0Text0 = dochtml1body1math0mi0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1math0mi0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1math0mi0Text0.TextContent);

            var dochtml1body1table1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table1.Attributes.Length);
            Assert.AreEqual("table", dochtml1body1table1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table1.NodeType);
        }

        [Test]
        public void MathMLWithChildrenInTable()
        {
            var doc = (@"<!DOCTYPE html><body><table><math><mi>foo</mi><mi>bar</mi></math></table>").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Length);
            Assert.AreEqual("math", dochtml1body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

            var dochtml1body1math0mi0 = dochtml1body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mi0.Attributes.Length);
            Assert.AreEqual("mi", dochtml1body1math0mi0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi0.NodeType);

            var dochtml1body1math0mi0Text0 = dochtml1body1math0mi0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1math0mi0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1math0mi0Text0.TextContent);

            var dochtml1body1math0mi1 = dochtml1body1math0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1math0mi1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mi1.Attributes.Length);
            Assert.AreEqual("mi", dochtml1body1math0mi1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi1.NodeType);

            var dochtml1body1math0mi1Text0 = dochtml1body1math0mi1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1math0mi1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1math0mi1Text0.TextContent);

            var dochtml1body1table1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table1.Attributes.Length);
            Assert.AreEqual("table", dochtml1body1table1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table1.NodeType);
        }

        [Test]
        public void MathMLInTBodySectionOfTable()
        {
            var doc = (@"<!DOCTYPE html><body><table><tbody><math><mi>foo</mi><mi>bar</mi></math></tbody></table>").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Length);
            Assert.AreEqual("math", dochtml1body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

            var dochtml1body1math0mi0 = dochtml1body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mi0.Attributes.Length);
            Assert.AreEqual("mi", dochtml1body1math0mi0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi0.NodeType);

            var dochtml1body1math0mi0Text0 = dochtml1body1math0mi0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1math0mi0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1math0mi0Text0.TextContent);

            var dochtml1body1math0mi1 = dochtml1body1math0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1math0mi1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mi1.Attributes.Length);
            Assert.AreEqual("mi", dochtml1body1math0mi1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi1.NodeType);

            var dochtml1body1math0mi1Text0 = dochtml1body1math0mi1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1math0mi1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1math0mi1Text0.TextContent);

            var dochtml1body1table1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table1.Attributes.Length);
            Assert.AreEqual("table", dochtml1body1table1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table1.NodeType);

            var dochtml1body1table1tbody0 = dochtml1body1table1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1table1tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table1tbody0.Attributes.Length);
            Assert.AreEqual("tbody", dochtml1body1table1tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table1tbody0.NodeType);
        }

        [Test]
        public void MathMLInRowOfTable()
        {
            var doc = (@"<!DOCTYPE html><body><table><tbody><tr><math><mi>foo</mi><mi>bar</mi></math></tr></tbody></table>").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Length);
            Assert.AreEqual("math", dochtml1body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

            var dochtml1body1math0mi0 = dochtml1body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mi0.Attributes.Length);
            Assert.AreEqual("mi", dochtml1body1math0mi0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi0.NodeType);

            var dochtml1body1math0mi0Text0 = dochtml1body1math0mi0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1math0mi0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1math0mi0Text0.TextContent);

            var dochtml1body1math0mi1 = dochtml1body1math0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1math0mi1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mi1.Attributes.Length);
            Assert.AreEqual("mi", dochtml1body1math0mi1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi1.NodeType);

            var dochtml1body1math0mi1Text0 = dochtml1body1math0mi1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1math0mi1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1math0mi1Text0.TextContent);

            var dochtml1body1table1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table1.Attributes.Length);
            Assert.AreEqual("table", dochtml1body1table1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table1.NodeType);

            var dochtml1body1table1tbody0 = dochtml1body1table1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table1tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table1tbody0.Attributes.Length);
            Assert.AreEqual("tbody", dochtml1body1table1tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table1tbody0.NodeType);

            var dochtml1body1table1tbody0tr0 = dochtml1body1table1tbody0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1table1tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table1tbody0tr0.Attributes.Length);
            Assert.AreEqual("tr", dochtml1body1table1tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table1tbody0tr0.NodeType);
        }

        [Test]
        public void MathMLInCellOfTable()
        {
            var doc = (@"<!DOCTYPE html><body><table><tbody><tr><td><math><mi>foo</mi><mi>bar</mi></math></td></tr></tbody></table>").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1table0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0.Attributes.Length);
            Assert.AreEqual("table", dochtml1body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0.NodeType);

            var dochtml1body1table0tbody0 = dochtml1body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0.Attributes.Length);
            Assert.AreEqual("tbody", dochtml1body1table0tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0.NodeType);

            var dochtml1body1table0tbody0tr0 = dochtml1body1table0tbody0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0.Attributes.Length);
            Assert.AreEqual("tr", dochtml1body1table0tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0.NodeType);

            var dochtml1body1table0tbody0tr0td0 = dochtml1body1table0tbody0tr0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0.Attributes.Length);
            Assert.AreEqual("td", dochtml1body1table0tbody0tr0td0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0.NodeType);

            var dochtml1body1table0tbody0tr0td0math0 = dochtml1body1table0tbody0tr0td0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1table0tbody0tr0td0math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0math0.Attributes.Length);
            Assert.AreEqual("math", dochtml1body1table0tbody0tr0td0math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0math0.NodeType);

            var dochtml1body1table0tbody0tr0td0math0mi0 = dochtml1body1table0tbody0tr0td0math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0math0mi0.Attributes.Length);
            Assert.AreEqual("mi", dochtml1body1table0tbody0tr0td0math0mi0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0math0mi0.NodeType);

            var dochtml1body1table0tbody0tr0td0math0mi0Text0 = dochtml1body1table0tbody0tr0td0math0mi0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0tbody0tr0td0math0mi0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1table0tbody0tr0td0math0mi0Text0.TextContent);

            var dochtml1body1table0tbody0tr0td0math0mi1 = dochtml1body1table0tbody0tr0td0math0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0math0mi1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0math0mi1.Attributes.Length);
            Assert.AreEqual("mi", dochtml1body1table0tbody0tr0td0math0mi1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0math0mi1.NodeType);

            var dochtml1body1table0tbody0tr0td0math0mi1Text0 = dochtml1body1table0tbody0tr0td0math0mi1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0tbody0tr0td0math0mi1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1table0tbody0tr0td0math0mi1Text0.TextContent);
        }

        [Test]
        public void MathMLCompleteExampleInTable()
        {
            var doc = (@"<!DOCTYPE html><body><table><tbody><tr><td><math><mi>foo</mi><mi>bar</mi></math><p>baz</td></tr></tbody></table>").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1table0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0.Attributes.Length);
            Assert.AreEqual("table", dochtml1body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0.NodeType);

            var dochtml1body1table0tbody0 = dochtml1body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0.Attributes.Length);
            Assert.AreEqual("tbody", dochtml1body1table0tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0.NodeType);

            var dochtml1body1table0tbody0tr0 = dochtml1body1table0tbody0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0.Attributes.Length);
            Assert.AreEqual("tr", dochtml1body1table0tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0.NodeType);

            var dochtml1body1table0tbody0tr0td0 = dochtml1body1table0tbody0tr0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1table0tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0.Attributes.Length);
            Assert.AreEqual("td", dochtml1body1table0tbody0tr0td0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0.NodeType);

            var dochtml1body1table0tbody0tr0td0math0 = dochtml1body1table0tbody0tr0td0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1table0tbody0tr0td0math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0math0.Attributes.Length);
            Assert.AreEqual("math", dochtml1body1table0tbody0tr0td0math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0math0.NodeType);

            var dochtml1body1table0tbody0tr0td0math0mi0 = dochtml1body1table0tbody0tr0td0math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0math0mi0.Attributes.Length);
            Assert.AreEqual("mi", dochtml1body1table0tbody0tr0td0math0mi0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0math0mi0.NodeType);

            var dochtml1body1table0tbody0tr0td0math0mi0Text0 = dochtml1body1table0tbody0tr0td0math0mi0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0tbody0tr0td0math0mi0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1table0tbody0tr0td0math0mi0Text0.TextContent);

            var dochtml1body1table0tbody0tr0td0math0mi1 = dochtml1body1table0tbody0tr0td0math0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0math0mi1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0math0mi1.Attributes.Length);
            Assert.AreEqual("mi", dochtml1body1table0tbody0tr0td0math0mi1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0math0mi1.NodeType);

            var dochtml1body1table0tbody0tr0td0math0mi1Text0 = dochtml1body1table0tbody0tr0td0math0mi1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0tbody0tr0td0math0mi1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1table0tbody0tr0td0math0mi1Text0.TextContent);

            var dochtml1body1table0tbody0tr0td0p1 = dochtml1body1table0tbody0tr0td0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0p1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0p1.Attributes.Length);
            Assert.AreEqual("p", dochtml1body1table0tbody0tr0td0p1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0p1.NodeType);

            var dochtml1body1table0tbody0tr0td0p1Text0 = dochtml1body1table0tbody0tr0td0p1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0tbody0tr0td0p1Text0.NodeType);
            Assert.AreEqual("baz", dochtml1body1table0tbody0tr0td0p1Text0.TextContent);
        }

        [Test]
        public void MathMLInCaptionOfTable()
        {
            var doc = (@"<!DOCTYPE html><body><table><caption><math><mi>foo</mi><mi>bar</mi></math><p>baz</caption></table>").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1table0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0.Attributes.Length);
            Assert.AreEqual("table", dochtml1body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0.NodeType);

            var dochtml1body1table0caption0 = dochtml1body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1table0caption0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0.Attributes.Length);
            Assert.AreEqual("caption", dochtml1body1table0caption0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0.NodeType);

            var dochtml1body1table0caption0math0 = dochtml1body1table0caption0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1table0caption0math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0math0.Attributes.Length);
            Assert.AreEqual("math", dochtml1body1table0caption0math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0math0.NodeType);

            var dochtml1body1table0caption0math0mi0 = dochtml1body1table0caption0math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0caption0math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0math0mi0.Attributes.Length);
            Assert.AreEqual("mi", dochtml1body1table0caption0math0mi0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0math0mi0.NodeType);

            var dochtml1body1table0caption0math0mi0Text0 = dochtml1body1table0caption0math0mi0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0caption0math0mi0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1table0caption0math0mi0Text0.TextContent);

            var dochtml1body1table0caption0math0mi1 = dochtml1body1table0caption0math0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1table0caption0math0mi1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0math0mi1.Attributes.Length);
            Assert.AreEqual("mi", dochtml1body1table0caption0math0mi1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0math0mi1.NodeType);

            var dochtml1body1table0caption0math0mi1Text0 = dochtml1body1table0caption0math0mi1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0caption0math0mi1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1table0caption0math0mi1Text0.TextContent);

            var dochtml1body1table0caption0p1 = dochtml1body1table0caption0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1table0caption0p1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0p1.Attributes.Length);
            Assert.AreEqual("p", dochtml1body1table0caption0p1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0p1.NodeType);

            var dochtml1body1table0caption0p1Text0 = dochtml1body1table0caption0p1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0caption0p1Text0.NodeType);
            Assert.AreEqual("baz", dochtml1body1table0caption0p1Text0.TextContent);
        }

        [Test]
        public void MathMLImplicitlyClosedInTable()
        {
            var doc = (@"<!DOCTYPE html><body><table><caption><math><mi>foo</mi><mi>bar</mi><p>baz</table><p>quux").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1table0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0.Attributes.Length);
            Assert.AreEqual("table", dochtml1body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0.NodeType);

            var dochtml1body1table0caption0 = dochtml1body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1table0caption0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0.Attributes.Length);
            Assert.AreEqual("caption", dochtml1body1table0caption0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0.NodeType);

            var dochtml1body1table0caption0math0 = dochtml1body1table0caption0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1table0caption0math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0math0.Attributes.Length);
            Assert.AreEqual("math", dochtml1body1table0caption0math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0math0.NodeType);

            var dochtml1body1table0caption0math0mi0 = dochtml1body1table0caption0math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0caption0math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0math0mi0.Attributes.Length);
            Assert.AreEqual("mi", dochtml1body1table0caption0math0mi0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0math0mi0.NodeType);

            var dochtml1body1table0caption0math0mi0Text0 = dochtml1body1table0caption0math0mi0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0caption0math0mi0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1table0caption0math0mi0Text0.TextContent);

            var dochtml1body1table0caption0math0mi1 = dochtml1body1table0caption0math0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1table0caption0math0mi1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0math0mi1.Attributes.Length);
            Assert.AreEqual("mi", dochtml1body1table0caption0math0mi1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0math0mi1.NodeType);

            var dochtml1body1table0caption0math0mi1Text0 = dochtml1body1table0caption0math0mi1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0caption0math0mi1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1table0caption0math0mi1Text0.TextContent);

            var dochtml1body1table0caption0p1 = dochtml1body1table0caption0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1table0caption0p1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0p1.Attributes.Length);
            Assert.AreEqual("p", dochtml1body1table0caption0p1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0p1.NodeType);

            var dochtml1body1table0caption0p1Text0 = dochtml1body1table0caption0p1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0caption0p1Text0.NodeType);
            Assert.AreEqual("baz", dochtml1body1table0caption0p1Text0.TextContent);

            var dochtml1body1p1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1p1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1p1.Attributes.Length);
            Assert.AreEqual("p", dochtml1body1p1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p1.NodeType);

            var dochtml1body1p1Text0 = dochtml1body1p1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1p1Text0.NodeType);
            Assert.AreEqual("quux", dochtml1body1p1Text0.TextContent);
        }

        [Test]
        public void MathMLInCaptionImplicitlyClosed()
        {
            var doc = (@"<!DOCTYPE html><body><table><caption><math><mi>foo</mi><mi>bar</mi>baz</table><p>quux").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1table0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0.Attributes.Length);
            Assert.AreEqual("table", dochtml1body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0.NodeType);

            var dochtml1body1table0caption0 = dochtml1body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0caption0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0.Attributes.Length);
            Assert.AreEqual("caption", dochtml1body1table0caption0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0.NodeType);

            var dochtml1body1table0caption0math0 = dochtml1body1table0caption0.ChildNodes[0] as Element;
            Assert.AreEqual(3, dochtml1body1table0caption0math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0math0.Attributes.Length);
            Assert.AreEqual("math", dochtml1body1table0caption0math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0math0.NodeType);

            var dochtml1body1table0caption0math0mi0 = dochtml1body1table0caption0math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0caption0math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0math0mi0.Attributes.Length);
            Assert.AreEqual("mi", dochtml1body1table0caption0math0mi0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0math0mi0.NodeType);

            var dochtml1body1table0caption0math0mi0Text0 = dochtml1body1table0caption0math0mi0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0caption0math0mi0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1table0caption0math0mi0Text0.TextContent);

            var dochtml1body1table0caption0math0mi1 = dochtml1body1table0caption0math0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1table0caption0math0mi1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0math0mi1.Attributes.Length);
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
            Assert.AreEqual(0, dochtml1body1p1.Attributes.Length);
            Assert.AreEqual("p", dochtml1body1p1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p1.NodeType);

            var dochtml1body1p1Text0 = dochtml1body1p1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1p1Text0.NodeType);
            Assert.AreEqual("quux", dochtml1body1p1Text0.TextContent);
        }

        [Test]
        public void MathMLInColgroupOfTable()
        {
            var doc = (@"<!DOCTYPE html><body><table><colgroup><math><mi>foo</mi><mi>bar</mi><p>baz</table><p>quux").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(4, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Length);
            Assert.AreEqual("math", dochtml1body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

            var dochtml1body1math0mi0 = dochtml1body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mi0.Attributes.Length);
            Assert.AreEqual("mi", dochtml1body1math0mi0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi0.NodeType);

            var dochtml1body1math0mi0Text0 = dochtml1body1math0mi0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1math0mi0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1math0mi0Text0.TextContent);

            var dochtml1body1math0mi1 = dochtml1body1math0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1math0mi1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mi1.Attributes.Length);
            Assert.AreEqual("mi", dochtml1body1math0mi1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi1.NodeType);

            var dochtml1body1math0mi1Text0 = dochtml1body1math0mi1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1math0mi1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1math0mi1Text0.TextContent);

            var dochtml1body1p1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1p1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1p1.Attributes.Length);
            Assert.AreEqual("p", dochtml1body1p1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p1.NodeType);

            var dochtml1body1p1Text0 = dochtml1body1p1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1p1Text0.NodeType);
            Assert.AreEqual("baz", dochtml1body1p1Text0.TextContent);

            var dochtml1body1table2 = dochtml1body1.ChildNodes[2] as Element;
            Assert.AreEqual(1, dochtml1body1table2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table2.Attributes.Length);
            Assert.AreEqual("table", dochtml1body1table2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table2.NodeType);

            var dochtml1body1table2colgroup0 = dochtml1body1table2.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1table2colgroup0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table2colgroup0.Attributes.Length);
            Assert.AreEqual("colgroup", dochtml1body1table2colgroup0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table2colgroup0.NodeType);

            var dochtml1body1p3 = dochtml1body1.ChildNodes[3] as Element;
            Assert.AreEqual(1, dochtml1body1p3.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1p3.Attributes.Length);
            Assert.AreEqual("p", dochtml1body1p3.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p3.NodeType);

            var dochtml1body1p3Text0 = dochtml1body1p3.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1p3Text0.NodeType);
            Assert.AreEqual("quux", dochtml1body1p3Text0.TextContent);
        }

        [Test]
        public void MathMLInSelectInTable()
        {
            var doc = (@"<!DOCTYPE html><body><table><tr><td><select><math><mi>foo</mi><mi>bar</mi><p>baz</table><p>quux").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1table0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0.Attributes.Length);
            Assert.AreEqual("table", dochtml1body1table0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0.NodeType);

            var dochtml1body1table0tbody0 = dochtml1body1table0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0.Attributes.Length);
            Assert.AreEqual("tbody", dochtml1body1table0tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0.NodeType);

            var dochtml1body1table0tbody0tr0 = dochtml1body1table0tbody0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0.Attributes.Length);
            Assert.AreEqual("tr", dochtml1body1table0tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0.NodeType);

            var dochtml1body1table0tbody0tr0td0 = dochtml1body1table0tbody0tr0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0.Attributes.Length);
            Assert.AreEqual("td", dochtml1body1table0tbody0tr0td0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0.NodeType);

            var dochtml1body1table0tbody0tr0td0select0 = dochtml1body1table0tbody0tr0td0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0select0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0select0.Attributes.Length);
            Assert.AreEqual("select", dochtml1body1table0tbody0tr0td0select0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0select0.NodeType);

            var dochtml1body1table0tbody0tr0td0select0Text0 = dochtml1body1table0tbody0tr0td0select0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0tbody0tr0td0select0Text0.NodeType);
            Assert.AreEqual("foobarbaz", dochtml1body1table0tbody0tr0td0select0Text0.TextContent);

            var dochtml1body1p1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1p1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1p1.Attributes.Length);
            Assert.AreEqual("p", dochtml1body1p1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p1.NodeType);

            var dochtml1body1p1Text0 = dochtml1body1p1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1p1Text0.NodeType);
            Assert.AreEqual("quux", dochtml1body1p1Text0.TextContent);
        }

        [Test]
        public void MathMLInSelectInTableImplicitlyClosed()
        {
            var doc = (@"<!DOCTYPE html><body><table><select><math><mi>foo</mi><mi>bar</mi><p>baz</table><p>quux").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(3, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1select0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1select0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1select0.Attributes.Length);
            Assert.AreEqual("select", dochtml1body1select0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1select0.NodeType);

            var dochtml1body1select0Text0 = dochtml1body1select0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1select0Text0.NodeType);
            Assert.AreEqual("foobarbaz", dochtml1body1select0Text0.TextContent);

            var dochtml1body1table1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table1.Attributes.Length);
            Assert.AreEqual("table", dochtml1body1table1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table1.NodeType);

            var dochtml1body1p2 = dochtml1body1.ChildNodes[2] as Element;
            Assert.AreEqual(1, dochtml1body1p2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1p2.Attributes.Length);
            Assert.AreEqual("p", dochtml1body1p2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p2.NodeType);

            var dochtml1body1p2Text0 = dochtml1body1p2.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1p2Text0.NodeType);
            Assert.AreEqual("quux", dochtml1body1p2Text0.TextContent);
        }

        [Test]
        public void MathMLOutsideDocumentRoot()
        {
            var doc = (@"<!DOCTYPE html><body></body></html><math><mi>foo</mi><mi>bar</mi><p>baz").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Length);
            Assert.AreEqual("math", dochtml1body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

            var dochtml1body1math0mi0 = dochtml1body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mi0.Attributes.Length);
            Assert.AreEqual("mi", dochtml1body1math0mi0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi0.NodeType);

            var dochtml1body1math0mi0Text0 = dochtml1body1math0mi0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1math0mi0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1math0mi0Text0.TextContent);

            var dochtml1body1math0mi1 = dochtml1body1math0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1math0mi1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mi1.Attributes.Length);
            Assert.AreEqual("mi", dochtml1body1math0mi1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi1.NodeType);

            var dochtml1body1math0mi1Text0 = dochtml1body1math0mi1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1math0mi1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1math0mi1Text0.TextContent);

            var dochtml1body1p1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1p1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1p1.Attributes.Length);
            Assert.AreEqual("p", dochtml1body1p1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p1.NodeType);

            var dochtml1body1p1Text0 = dochtml1body1p1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1p1Text0.NodeType);
            Assert.AreEqual("baz", dochtml1body1p1Text0.TextContent);
        }

        [Test]
        public void MathMLOutsideDocumentImplicitlyClosed()
        {
            var doc = (@"<!DOCTYPE html><body></body><math><mi>foo</mi><mi>bar</mi><p>baz").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Length);
            Assert.AreEqual("math", dochtml1body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

            var dochtml1body1math0mi0 = dochtml1body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mi0.Attributes.Length);
            Assert.AreEqual("mi", dochtml1body1math0mi0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi0.NodeType);

            var dochtml1body1math0mi0Text0 = dochtml1body1math0mi0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1math0mi0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1math0mi0Text0.TextContent);

            var dochtml1body1math0mi1 = dochtml1body1math0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1math0mi1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0mi1.Attributes.Length);
            Assert.AreEqual("mi", dochtml1body1math0mi1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi1.NodeType);

            var dochtml1body1math0mi1Text0 = dochtml1body1math0mi1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1math0mi1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1math0mi1Text0.TextContent);

            var dochtml1body1p1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1p1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1p1.Attributes.Length);
            Assert.AreEqual("p", dochtml1body1p1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p1.NodeType);

            var dochtml1body1p1Text0 = dochtml1body1p1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1p1Text0.NodeType);
            Assert.AreEqual("baz", dochtml1body1p1Text0.TextContent);
        }

        [Test]
        public void MathMLInFrameset()
        {
            var doc = (@"<!DOCTYPE html><frameset><math><mi></mi><mi></mi><p><span>").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1frameset1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1frameset1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1frameset1.Attributes.Length);
            Assert.AreEqual("frameset", dochtml1frameset1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1frameset1.NodeType);
        }

        [Test]
        public void MathMLOutsideFrameset()
        {
            var doc = (@"<!DOCTYPE html><frameset></frameset><math><mi></mi><mi></mi><p><span>").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1frameset1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1frameset1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1frameset1.Attributes.Length);
            Assert.AreEqual("frameset", dochtml1frameset1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1frameset1.NodeType);
        }

        [Test]
        public void MathMLWithXLinkAttributes()
        {
            var doc = (@"<!DOCTYPE html><body xlink:href=foo><math xlink:href=foo></math>").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(1, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
            Assert.AreEqual("foo", dochtml1body1.GetAttribute("xlink:href"));

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml1body1math0.Attributes.Length);
            Assert.AreEqual("math", dochtml1body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

            var attr = dochtml1body1math0.Attributes.GetNamedItem("href");
            Assert.IsNotNull(attr);
            Assert.AreEqual("foo", attr.Value);
            Assert.AreEqual(null, attr.Prefix);
            Assert.AreEqual("http://www.w3.org/1999/xlink", attr.NamespaceUri);
        }

        [Test]
        public void MathMLInBodyWithLangAttribute()
        {
            var doc = (@"<!DOCTYPE html><body xlink:href=foo xml:lang=en><math><mi xml:lang=en xlink:href=foo></mi></math>").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(2, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
            Assert.AreEqual("foo", dochtml1body1.GetAttribute("xlink:href"));
            Assert.AreEqual("en", dochtml1body1.GetAttribute("xml:lang"));

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Length);
            Assert.AreEqual("math", dochtml1body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

            var dochtml1body1math0mi0 = dochtml1body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1math0mi0.ChildNodes.Length);
            Assert.AreEqual(2, dochtml1body1math0mi0.Attributes.Length);
            Assert.AreEqual("mi", dochtml1body1math0mi0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi0.NodeType);

            var attr1 = dochtml1body1math0mi0.Attributes.GetNamedItem("href");
            Assert.IsNotNull(attr1);
            Assert.AreEqual("foo", attr1.Value);
            Assert.AreEqual(null, attr1.Prefix);
            Assert.AreEqual("http://www.w3.org/1999/xlink", attr1.NamespaceUri);

            var attr2 = dochtml1body1math0mi0.Attributes.GetNamedItem("xml:lang");
            Assert.IsNotNull(attr2);
            Assert.AreEqual("en", attr2.Value);
            Assert.AreEqual("xml", attr2.Prefix);
            Assert.AreEqual("http://www.w3.org/XML/1998/namespace", attr2.NamespaceUri);
        }

        [Test]
        public void MathMLWithMiChild()
        {
            var doc = (@"<!DOCTYPE html><body xlink:href=foo xml:lang=en><math><mi xml:lang=en xlink:href=foo /></math>").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(2, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
            Assert.AreEqual("foo", dochtml1body1.GetAttribute("xlink:href"));
            Assert.AreEqual("en", dochtml1body1.GetAttribute("xml:lang"));

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Length);
            Assert.AreEqual("math", dochtml1body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

            var dochtml1body1math0mi0 = dochtml1body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1math0mi0.ChildNodes.Length);
            Assert.AreEqual(2, dochtml1body1math0mi0.Attributes.Length);
            Assert.AreEqual("mi", dochtml1body1math0mi0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi0.NodeType);

            var attr1 = dochtml1body1math0mi0.Attributes.GetNamedItem("href");
            Assert.IsNotNull(attr1);
            Assert.AreEqual("foo", attr1.Value);
            Assert.AreEqual(null, attr1.Prefix);
            Assert.AreEqual("http://www.w3.org/1999/xlink", attr1.NamespaceUri);

            var attr2 = dochtml1body1math0mi0.Attributes.GetNamedItem("xml:lang");
            Assert.IsNotNull(attr2);
            Assert.AreEqual("en", attr2.Value);
            Assert.AreEqual("xml", attr2.Prefix);
            Assert.AreEqual("http://www.w3.org/XML/1998/namespace", attr2.NamespaceUri);
        }

        [Test]
        public void MathMLWithTextNode()
        {
            var doc = (@"<!DOCTYPE html><body xlink:href=foo xml:lang=en><math><mi xml:lang=en xlink:href=foo />bar</math>").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(2, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
            Assert.AreEqual("foo", dochtml1body1.GetAttribute("xlink:href"));
            Assert.AreEqual("en", dochtml1body1.GetAttribute("xml:lang"));

            var dochtml1body1math0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1math0.Attributes.Length);
            Assert.AreEqual("math", dochtml1body1math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0.NodeType);

            var dochtml1body1math0mi0 = dochtml1body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1math0mi0.ChildNodes.Length);
            Assert.AreEqual(2, dochtml1body1math0mi0.Attributes.Length);
            Assert.AreEqual("mi", dochtml1body1math0mi0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1math0mi0.NodeType);

            var attr1 = dochtml1body1math0mi0.Attributes.GetNamedItem("href");
            Assert.IsNotNull(attr1);
            Assert.AreEqual("foo", attr1.Value);
            Assert.AreEqual(null, attr1.Prefix);
            Assert.AreEqual("http://www.w3.org/1999/xlink", attr1.NamespaceUri);

            var attr2 = dochtml1body1math0mi0.Attributes.GetNamedItem("xml:lang");
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
