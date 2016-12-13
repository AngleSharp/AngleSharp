namespace AngleSharp.Core.Tests.Html
{
    using AngleSharp.Dom;
    using NUnit.Framework;

    /// <summary>
    /// Tests from https://github.com/html5lib/html5lib-tests:
    /// tree-construction/tests10.dat
    /// tree-construction/tests11.dat
    /// </summary>
    [TestFixture]
    public class HtmlWithSVGTests
    {
        [Test]
        public void SvgCheckAttributesCaseNormalUnchanged()
        {
            var doc = (@"<!DOCTYPE html><body><svg attributeName='' attributeType='' baseFrequency='' baseProfile='' calcMode='' clipPathUnits='' contentScriptType='' contentStyleType='' diffuseConstant='' edgeMode='' externalResourcesRequired='' filterRes='' filterUnits='' glyphRef='' gradientTransform='' gradientUnits='' kernelMatrix='' kernelUnitLength='' keyPoints='' keySplines='' keyTimes='' lengthAdjust='' limitingConeAngle='' markerHeight='' markerUnits='' markerWidth='' maskContentUnits='' maskUnits='' numOctaves='' pathLength='' patternContentUnits='' patternTransform='' patternUnits='' pointsAtX='' pointsAtY='' pointsAtZ='' preserveAlpha='' preserveAspectRatio='' primitiveUnits='' refX='' refY='' repeatCount='' repeatDur='' requiredExtensions='' requiredFeatures='' specularConstant='' specularExponent='' spreadMethod='' startOffset='' stdDeviation='' stitchTiles='' surfaceScale='' systemLanguage='' tableValues='' targetX='' targetY='' textLength='' viewBox='' viewTarget='' xChannelSelector='' yChannelSelector='' zoomAndPan=''></svg>").ToHtmlDocument();

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

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(62, dochtml1body1svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml1body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("attributeName"));
            Assert.AreEqual("attributeName", dochtml1body1svg0.Attributes.GetNamedItem("attributeName").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("attributeName").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("attributeType"));
            Assert.AreEqual("attributeType", dochtml1body1svg0.Attributes.GetNamedItem("attributeType").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("attributeType").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("baseFrequency"));
            Assert.AreEqual("baseFrequency", dochtml1body1svg0.Attributes.GetNamedItem("baseFrequency").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("baseFrequency").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("baseProfile"));
            Assert.AreEqual("baseProfile", dochtml1body1svg0.Attributes.GetNamedItem("baseProfile").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("baseProfile").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("calcMode"));
            Assert.AreEqual("calcMode", dochtml1body1svg0.Attributes.GetNamedItem("calcMode").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("calcMode").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("clipPathUnits"));
            Assert.AreEqual("clipPathUnits", dochtml1body1svg0.Attributes.GetNamedItem("clipPathUnits").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("clipPathUnits").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("contentScriptType"));
            Assert.AreEqual("contentScriptType", dochtml1body1svg0.Attributes.GetNamedItem("contentScriptType").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("contentScriptType").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("contentStyleType"));
            Assert.AreEqual("contentStyleType", dochtml1body1svg0.Attributes.GetNamedItem("contentStyleType").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("contentStyleType").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("diffuseConstant"));
            Assert.AreEqual("diffuseConstant", dochtml1body1svg0.Attributes.GetNamedItem("diffuseConstant").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("diffuseConstant").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("edgeMode"));
            Assert.AreEqual("edgeMode", dochtml1body1svg0.Attributes.GetNamedItem("edgeMode").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("edgeMode").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("externalResourcesRequired"));
            Assert.AreEqual("externalResourcesRequired", dochtml1body1svg0.Attributes.GetNamedItem("externalResourcesRequired").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("externalResourcesRequired").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("filterRes"));
            Assert.AreEqual("filterRes", dochtml1body1svg0.Attributes.GetNamedItem("filterRes").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("filterRes").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("filterUnits"));
            Assert.AreEqual("filterUnits", dochtml1body1svg0.Attributes.GetNamedItem("filterUnits").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("filterUnits").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("glyphRef"));
            Assert.AreEqual("glyphRef", dochtml1body1svg0.Attributes.GetNamedItem("glyphRef").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("glyphRef").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("gradientTransform"));
            Assert.AreEqual("gradientTransform", dochtml1body1svg0.Attributes.GetNamedItem("gradientTransform").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("gradientTransform").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("gradientUnits"));
            Assert.AreEqual("gradientUnits", dochtml1body1svg0.Attributes.GetNamedItem("gradientUnits").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("gradientUnits").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("kernelMatrix"));
            Assert.AreEqual("kernelMatrix", dochtml1body1svg0.Attributes.GetNamedItem("kernelMatrix").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("kernelMatrix").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("kernelUnitLength"));
            Assert.AreEqual("kernelUnitLength", dochtml1body1svg0.Attributes.GetNamedItem("kernelUnitLength").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("kernelUnitLength").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("keyPoints"));
            Assert.AreEqual("keyPoints", dochtml1body1svg0.Attributes.GetNamedItem("keyPoints").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("keyPoints").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("keySplines"));
            Assert.AreEqual("keySplines", dochtml1body1svg0.Attributes.GetNamedItem("keySplines").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("keySplines").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("keyTimes"));
            Assert.AreEqual("keyTimes", dochtml1body1svg0.Attributes.GetNamedItem("keyTimes").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("keyTimes").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("lengthAdjust"));
            Assert.AreEqual("lengthAdjust", dochtml1body1svg0.Attributes.GetNamedItem("lengthAdjust").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("lengthAdjust").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("limitingConeAngle"));
            Assert.AreEqual("limitingConeAngle", dochtml1body1svg0.Attributes.GetNamedItem("limitingConeAngle").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("limitingConeAngle").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("markerHeight"));
            Assert.AreEqual("markerHeight", dochtml1body1svg0.Attributes.GetNamedItem("markerHeight").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("markerHeight").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("markerUnits"));
            Assert.AreEqual("markerUnits", dochtml1body1svg0.Attributes.GetNamedItem("markerUnits").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("markerUnits").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("markerWidth"));
            Assert.AreEqual("markerWidth", dochtml1body1svg0.Attributes.GetNamedItem("markerWidth").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("markerWidth").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("maskContentUnits"));
            Assert.AreEqual("maskContentUnits", dochtml1body1svg0.Attributes.GetNamedItem("maskContentUnits").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("maskContentUnits").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("maskUnits"));
            Assert.AreEqual("maskUnits", dochtml1body1svg0.Attributes.GetNamedItem("maskUnits").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("maskUnits").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("numOctaves"));
            Assert.AreEqual("numOctaves", dochtml1body1svg0.Attributes.GetNamedItem("numOctaves").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("numOctaves").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("pathLength"));
            Assert.AreEqual("pathLength", dochtml1body1svg0.Attributes.GetNamedItem("pathLength").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("pathLength").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("patternContentUnits"));
            Assert.AreEqual("patternContentUnits", dochtml1body1svg0.Attributes.GetNamedItem("patternContentUnits").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("patternContentUnits").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("patternTransform"));
            Assert.AreEqual("patternTransform", dochtml1body1svg0.Attributes.GetNamedItem("patternTransform").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("patternTransform").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("patternUnits"));
            Assert.AreEqual("patternUnits", dochtml1body1svg0.Attributes.GetNamedItem("patternUnits").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("patternUnits").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("pointsAtX"));
            Assert.AreEqual("pointsAtX", dochtml1body1svg0.Attributes.GetNamedItem("pointsAtX").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("pointsAtX").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("pointsAtY"));
            Assert.AreEqual("pointsAtY", dochtml1body1svg0.Attributes.GetNamedItem("pointsAtY").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("pointsAtY").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("pointsAtZ"));
            Assert.AreEqual("pointsAtZ", dochtml1body1svg0.Attributes.GetNamedItem("pointsAtZ").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("pointsAtZ").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("preserveAlpha"));
            Assert.AreEqual("preserveAlpha", dochtml1body1svg0.Attributes.GetNamedItem("preserveAlpha").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("preserveAlpha").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("preserveAspectRatio"));
            Assert.AreEqual("preserveAspectRatio", dochtml1body1svg0.Attributes.GetNamedItem("preserveAspectRatio").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("preserveAspectRatio").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("primitiveUnits"));
            Assert.AreEqual("primitiveUnits", dochtml1body1svg0.Attributes.GetNamedItem("primitiveUnits").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("primitiveUnits").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("refX"));
            Assert.AreEqual("refX", dochtml1body1svg0.Attributes.GetNamedItem("refX").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("refX").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("refY"));
            Assert.AreEqual("refY", dochtml1body1svg0.Attributes.GetNamedItem("refY").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("refY").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("repeatCount"));
            Assert.AreEqual("repeatCount", dochtml1body1svg0.Attributes.GetNamedItem("repeatCount").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("repeatCount").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("repeatDur"));
            Assert.AreEqual("repeatDur", dochtml1body1svg0.Attributes.GetNamedItem("repeatDur").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("repeatDur").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("requiredExtensions"));
            Assert.AreEqual("requiredExtensions", dochtml1body1svg0.Attributes.GetNamedItem("requiredExtensions").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("requiredExtensions").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("requiredFeatures"));
            Assert.AreEqual("requiredFeatures", dochtml1body1svg0.Attributes.GetNamedItem("requiredFeatures").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("requiredFeatures").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("specularConstant"));
            Assert.AreEqual("specularConstant", dochtml1body1svg0.Attributes.GetNamedItem("specularConstant").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("specularConstant").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("specularExponent"));
            Assert.AreEqual("specularExponent", dochtml1body1svg0.Attributes.GetNamedItem("specularExponent").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("specularExponent").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("spreadMethod"));
            Assert.AreEqual("spreadMethod", dochtml1body1svg0.Attributes.GetNamedItem("spreadMethod").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("spreadMethod").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("startOffset"));
            Assert.AreEqual("startOffset", dochtml1body1svg0.Attributes.GetNamedItem("startOffset").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("startOffset").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("stdDeviation"));
            Assert.AreEqual("stdDeviation", dochtml1body1svg0.Attributes.GetNamedItem("stdDeviation").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("stdDeviation").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("stitchTiles"));
            Assert.AreEqual("stitchTiles", dochtml1body1svg0.Attributes.GetNamedItem("stitchTiles").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("stitchTiles").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("surfaceScale"));
            Assert.AreEqual("surfaceScale", dochtml1body1svg0.Attributes.GetNamedItem("surfaceScale").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("surfaceScale").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("systemLanguage"));
            Assert.AreEqual("systemLanguage", dochtml1body1svg0.Attributes.GetNamedItem("systemLanguage").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("systemLanguage").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("tableValues"));
            Assert.AreEqual("tableValues", dochtml1body1svg0.Attributes.GetNamedItem("tableValues").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("tableValues").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("targetX"));
            Assert.AreEqual("targetX", dochtml1body1svg0.Attributes.GetNamedItem("targetX").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("targetX").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("targetY"));
            Assert.AreEqual("targetY", dochtml1body1svg0.Attributes.GetNamedItem("targetY").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("targetY").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("textLength"));
            Assert.AreEqual("textLength", dochtml1body1svg0.Attributes.GetNamedItem("textLength").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("textLength").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("viewBox"));
            Assert.AreEqual("viewBox", dochtml1body1svg0.Attributes.GetNamedItem("viewBox").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("viewBox").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("viewTarget"));
            Assert.AreEqual("viewTarget", dochtml1body1svg0.Attributes.GetNamedItem("viewTarget").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("viewTarget").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("xChannelSelector"));
            Assert.AreEqual("xChannelSelector", dochtml1body1svg0.Attributes.GetNamedItem("xChannelSelector").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("xChannelSelector").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("yChannelSelector"));
            Assert.AreEqual("yChannelSelector", dochtml1body1svg0.Attributes.GetNamedItem("yChannelSelector").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("yChannelSelector").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("zoomAndPan"));
            Assert.AreEqual("zoomAndPan", dochtml1body1svg0.Attributes.GetNamedItem("zoomAndPan").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("zoomAndPan").Value);
        }

        [Test]
        public void SvgCheckAttributesCaseUppercaseModified()
        {
            var doc = (@"<!DOCTYPE html><BODY><SVG ATTRIBUTENAME='' ATTRIBUTETYPE='' BASEFREQUENCY='' BASEPROFILE='' CALCMODE='' CLIPPATHUNITS='' CONTENTSCRIPTTYPE='' CONTENTSTYLETYPE='' DIFFUSECONSTANT='' EDGEMODE='' EXTERNALRESOURCESREQUIRED='' FILTERRES='' FILTERUNITS='' GLYPHREF='' GRADIENTTRANSFORM='' GRADIENTUNITS='' KERNELMATRIX='' KERNELUNITLENGTH='' KEYPOINTS='' KEYSPLINES='' KEYTIMES='' LENGTHADJUST='' LIMITINGCONEANGLE='' MARKERHEIGHT='' MARKERUNITS='' MARKERWIDTH='' MASKCONTENTUNITS='' MASKUNITS='' NUMOCTAVES='' PATHLENGTH='' PATTERNCONTENTUNITS='' PATTERNTRANSFORM='' PATTERNUNITS='' POINTSATX='' POINTSATY='' POINTSATZ='' PRESERVEALPHA='' PRESERVEASPECTRATIO='' PRIMITIVEUNITS='' REFX='' REFY='' REPEATCOUNT='' REPEATDUR='' REQUIREDEXTENSIONS='' REQUIREDFEATURES='' SPECULARCONSTANT='' SPECULAREXPONENT='' SPREADMETHOD='' STARTOFFSET='' STDDEVIATION='' STITCHTILES='' SURFACESCALE='' SYSTEMLANGUAGE='' TABLEVALUES='' TARGETX='' TARGETY='' TEXTLENGTH='' VIEWBOX='' VIEWTARGET='' XCHANNELSELECTOR='' YCHANNELSELECTOR='' ZOOMANDPAN=''></SVG>").ToHtmlDocument();

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

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(62, dochtml1body1svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml1body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("attributeName"));
            Assert.AreEqual("attributeName", dochtml1body1svg0.Attributes.GetNamedItem("attributeName").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("attributeName").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("attributeType"));
            Assert.AreEqual("attributeType", dochtml1body1svg0.Attributes.GetNamedItem("attributeType").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("attributeType").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("baseFrequency"));
            Assert.AreEqual("baseFrequency", dochtml1body1svg0.Attributes.GetNamedItem("baseFrequency").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("baseFrequency").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("baseProfile"));
            Assert.AreEqual("baseProfile", dochtml1body1svg0.Attributes.GetNamedItem("baseProfile").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("baseProfile").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("calcMode"));
            Assert.AreEqual("calcMode", dochtml1body1svg0.Attributes.GetNamedItem("calcMode").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("calcMode").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("clipPathUnits"));
            Assert.AreEqual("clipPathUnits", dochtml1body1svg0.Attributes.GetNamedItem("clipPathUnits").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("clipPathUnits").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("contentScriptType"));
            Assert.AreEqual("contentScriptType", dochtml1body1svg0.Attributes.GetNamedItem("contentScriptType").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("contentScriptType").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("contentStyleType"));
            Assert.AreEqual("contentStyleType", dochtml1body1svg0.Attributes.GetNamedItem("contentStyleType").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("contentStyleType").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("diffuseConstant"));
            Assert.AreEqual("diffuseConstant", dochtml1body1svg0.Attributes.GetNamedItem("diffuseConstant").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("diffuseConstant").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("edgeMode"));
            Assert.AreEqual("edgeMode", dochtml1body1svg0.Attributes.GetNamedItem("edgeMode").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("edgeMode").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("externalResourcesRequired"));
            Assert.AreEqual("externalResourcesRequired", dochtml1body1svg0.Attributes.GetNamedItem("externalResourcesRequired").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("externalResourcesRequired").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("filterRes"));
            Assert.AreEqual("filterRes", dochtml1body1svg0.Attributes.GetNamedItem("filterRes").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("filterRes").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("filterUnits"));
            Assert.AreEqual("filterUnits", dochtml1body1svg0.Attributes.GetNamedItem("filterUnits").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("filterUnits").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("glyphRef"));
            Assert.AreEqual("glyphRef", dochtml1body1svg0.Attributes.GetNamedItem("glyphRef").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("glyphRef").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("gradientTransform"));
            Assert.AreEqual("gradientTransform", dochtml1body1svg0.Attributes.GetNamedItem("gradientTransform").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("gradientTransform").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("gradientUnits"));
            Assert.AreEqual("gradientUnits", dochtml1body1svg0.Attributes.GetNamedItem("gradientUnits").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("gradientUnits").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("kernelMatrix"));
            Assert.AreEqual("kernelMatrix", dochtml1body1svg0.Attributes.GetNamedItem("kernelMatrix").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("kernelMatrix").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("kernelUnitLength"));
            Assert.AreEqual("kernelUnitLength", dochtml1body1svg0.Attributes.GetNamedItem("kernelUnitLength").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("kernelUnitLength").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("keyPoints"));
            Assert.AreEqual("keyPoints", dochtml1body1svg0.Attributes.GetNamedItem("keyPoints").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("keyPoints").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("keySplines"));
            Assert.AreEqual("keySplines", dochtml1body1svg0.Attributes.GetNamedItem("keySplines").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("keySplines").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("keyTimes"));
            Assert.AreEqual("keyTimes", dochtml1body1svg0.Attributes.GetNamedItem("keyTimes").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("keyTimes").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("lengthAdjust"));
            Assert.AreEqual("lengthAdjust", dochtml1body1svg0.Attributes.GetNamedItem("lengthAdjust").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("lengthAdjust").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("limitingConeAngle"));
            Assert.AreEqual("limitingConeAngle", dochtml1body1svg0.Attributes.GetNamedItem("limitingConeAngle").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("limitingConeAngle").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("markerHeight"));
            Assert.AreEqual("markerHeight", dochtml1body1svg0.Attributes.GetNamedItem("markerHeight").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("markerHeight").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("markerUnits"));
            Assert.AreEqual("markerUnits", dochtml1body1svg0.Attributes.GetNamedItem("markerUnits").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("markerUnits").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("markerWidth"));
            Assert.AreEqual("markerWidth", dochtml1body1svg0.Attributes.GetNamedItem("markerWidth").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("markerWidth").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("maskContentUnits"));
            Assert.AreEqual("maskContentUnits", dochtml1body1svg0.Attributes.GetNamedItem("maskContentUnits").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("maskContentUnits").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("maskUnits"));
            Assert.AreEqual("maskUnits", dochtml1body1svg0.Attributes.GetNamedItem("maskUnits").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("maskUnits").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("numOctaves"));
            Assert.AreEqual("numOctaves", dochtml1body1svg0.Attributes.GetNamedItem("numOctaves").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("numOctaves").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("pathLength"));
            Assert.AreEqual("pathLength", dochtml1body1svg0.Attributes.GetNamedItem("pathLength").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("pathLength").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("patternContentUnits"));
            Assert.AreEqual("patternContentUnits", dochtml1body1svg0.Attributes.GetNamedItem("patternContentUnits").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("patternContentUnits").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("patternTransform"));
            Assert.AreEqual("patternTransform", dochtml1body1svg0.Attributes.GetNamedItem("patternTransform").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("patternTransform").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("patternUnits"));
            Assert.AreEqual("patternUnits", dochtml1body1svg0.Attributes.GetNamedItem("patternUnits").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("patternUnits").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("pointsAtX"));
            Assert.AreEqual("pointsAtX", dochtml1body1svg0.Attributes.GetNamedItem("pointsAtX").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("pointsAtX").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("pointsAtY"));
            Assert.AreEqual("pointsAtY", dochtml1body1svg0.Attributes.GetNamedItem("pointsAtY").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("pointsAtY").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("pointsAtZ"));
            Assert.AreEqual("pointsAtZ", dochtml1body1svg0.Attributes.GetNamedItem("pointsAtZ").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("pointsAtZ").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("preserveAlpha"));
            Assert.AreEqual("preserveAlpha", dochtml1body1svg0.Attributes.GetNamedItem("preserveAlpha").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("preserveAlpha").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("preserveAspectRatio"));
            Assert.AreEqual("preserveAspectRatio", dochtml1body1svg0.Attributes.GetNamedItem("preserveAspectRatio").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("preserveAspectRatio").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("primitiveUnits"));
            Assert.AreEqual("primitiveUnits", dochtml1body1svg0.Attributes.GetNamedItem("primitiveUnits").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("primitiveUnits").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("refX"));
            Assert.AreEqual("refX", dochtml1body1svg0.Attributes.GetNamedItem("refX").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("refX").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("refY"));
            Assert.AreEqual("refY", dochtml1body1svg0.Attributes.GetNamedItem("refY").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("refY").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("repeatCount"));
            Assert.AreEqual("repeatCount", dochtml1body1svg0.Attributes.GetNamedItem("repeatCount").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("repeatCount").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("repeatDur"));
            Assert.AreEqual("repeatDur", dochtml1body1svg0.Attributes.GetNamedItem("repeatDur").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("repeatDur").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("requiredExtensions"));
            Assert.AreEqual("requiredExtensions", dochtml1body1svg0.Attributes.GetNamedItem("requiredExtensions").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("requiredExtensions").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("requiredFeatures"));
            Assert.AreEqual("requiredFeatures", dochtml1body1svg0.Attributes.GetNamedItem("requiredFeatures").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("requiredFeatures").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("specularConstant"));
            Assert.AreEqual("specularConstant", dochtml1body1svg0.Attributes.GetNamedItem("specularConstant").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("specularConstant").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("specularExponent"));
            Assert.AreEqual("specularExponent", dochtml1body1svg0.Attributes.GetNamedItem("specularExponent").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("specularExponent").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("spreadMethod"));
            Assert.AreEqual("spreadMethod", dochtml1body1svg0.Attributes.GetNamedItem("spreadMethod").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("spreadMethod").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("startOffset"));
            Assert.AreEqual("startOffset", dochtml1body1svg0.Attributes.GetNamedItem("startOffset").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("startOffset").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("stdDeviation"));
            Assert.AreEqual("stdDeviation", dochtml1body1svg0.Attributes.GetNamedItem("stdDeviation").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("stdDeviation").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("stitchTiles"));
            Assert.AreEqual("stitchTiles", dochtml1body1svg0.Attributes.GetNamedItem("stitchTiles").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("stitchTiles").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("surfaceScale"));
            Assert.AreEqual("surfaceScale", dochtml1body1svg0.Attributes.GetNamedItem("surfaceScale").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("surfaceScale").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("systemLanguage"));
            Assert.AreEqual("systemLanguage", dochtml1body1svg0.Attributes.GetNamedItem("systemLanguage").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("systemLanguage").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("tableValues"));
            Assert.AreEqual("tableValues", dochtml1body1svg0.Attributes.GetNamedItem("tableValues").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("tableValues").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("targetX"));
            Assert.AreEqual("targetX", dochtml1body1svg0.Attributes.GetNamedItem("targetX").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("targetX").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("targetY"));
            Assert.AreEqual("targetY", dochtml1body1svg0.Attributes.GetNamedItem("targetY").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("targetY").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("textLength"));
            Assert.AreEqual("textLength", dochtml1body1svg0.Attributes.GetNamedItem("textLength").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("textLength").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("viewBox"));
            Assert.AreEqual("viewBox", dochtml1body1svg0.Attributes.GetNamedItem("viewBox").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("viewBox").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("viewTarget"));
            Assert.AreEqual("viewTarget", dochtml1body1svg0.Attributes.GetNamedItem("viewTarget").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("viewTarget").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("xChannelSelector"));
            Assert.AreEqual("xChannelSelector", dochtml1body1svg0.Attributes.GetNamedItem("xChannelSelector").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("xChannelSelector").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("yChannelSelector"));
            Assert.AreEqual("yChannelSelector", dochtml1body1svg0.Attributes.GetNamedItem("yChannelSelector").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("yChannelSelector").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("zoomAndPan"));
            Assert.AreEqual("zoomAndPan", dochtml1body1svg0.Attributes.GetNamedItem("zoomAndPan").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("zoomAndPan").Value);
        }

        [Test]
        public void SvgCheckAttributesCaseLowercaseModified()
        {
            var doc = (@"<!DOCTYPE html><body><svg attributename='' attributetype='' basefrequency='' baseprofile='' calcmode='' clippathunits='' contentscripttype='' contentstyletype='' diffuseconstant='' edgemode='' externalresourcesrequired='' filterres='' filterunits='' glyphref='' gradienttransform='' gradientunits='' kernelmatrix='' kernelunitlength='' keypoints='' keysplines='' keytimes='' lengthadjust='' limitingconeangle='' markerheight='' markerunits='' markerwidth='' maskcontentunits='' maskunits='' numoctaves='' pathlength='' patterncontentunits='' patterntransform='' patternunits='' pointsatx='' pointsaty='' pointsatz='' preservealpha='' preserveaspectratio='' primitiveunits='' refx='' refy='' repeatcount='' repeatdur='' requiredextensions='' requiredfeatures='' specularconstant='' specularexponent='' spreadmethod='' startoffset='' stddeviation='' stitchtiles='' surfacescale='' systemlanguage='' tablevalues='' targetx='' targety='' textlength='' viewbox='' viewtarget='' xchannelselector='' ychannelselector='' zoomandpan=''></svg>").ToHtmlDocument();

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

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(62, dochtml1body1svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml1body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("attributeName"));
            Assert.AreEqual("attributeName", dochtml1body1svg0.Attributes.GetNamedItem("attributeName").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("attributeName").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("attributeType"));
            Assert.AreEqual("attributeType", dochtml1body1svg0.Attributes.GetNamedItem("attributeType").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("attributeType").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("baseFrequency"));
            Assert.AreEqual("baseFrequency", dochtml1body1svg0.Attributes.GetNamedItem("baseFrequency").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("baseFrequency").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("baseProfile"));
            Assert.AreEqual("baseProfile", dochtml1body1svg0.Attributes.GetNamedItem("baseProfile").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("baseProfile").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("calcMode"));
            Assert.AreEqual("calcMode", dochtml1body1svg0.Attributes.GetNamedItem("calcMode").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("calcMode").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("clipPathUnits"));
            Assert.AreEqual("clipPathUnits", dochtml1body1svg0.Attributes.GetNamedItem("clipPathUnits").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("clipPathUnits").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("contentScriptType"));
            Assert.AreEqual("contentScriptType", dochtml1body1svg0.Attributes.GetNamedItem("contentScriptType").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("contentScriptType").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("contentStyleType"));
            Assert.AreEqual("contentStyleType", dochtml1body1svg0.Attributes.GetNamedItem("contentStyleType").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("contentStyleType").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("diffuseConstant"));
            Assert.AreEqual("diffuseConstant", dochtml1body1svg0.Attributes.GetNamedItem("diffuseConstant").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("diffuseConstant").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("edgeMode"));
            Assert.AreEqual("edgeMode", dochtml1body1svg0.Attributes.GetNamedItem("edgeMode").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("edgeMode").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("externalResourcesRequired"));
            Assert.AreEqual("externalResourcesRequired", dochtml1body1svg0.Attributes.GetNamedItem("externalResourcesRequired").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("externalResourcesRequired").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("filterRes"));
            Assert.AreEqual("filterRes", dochtml1body1svg0.Attributes.GetNamedItem("filterRes").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("filterRes").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("filterUnits"));
            Assert.AreEqual("filterUnits", dochtml1body1svg0.Attributes.GetNamedItem("filterUnits").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("filterUnits").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("glyphRef"));
            Assert.AreEqual("glyphRef", dochtml1body1svg0.Attributes.GetNamedItem("glyphRef").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("glyphRef").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("gradientTransform"));
            Assert.AreEqual("gradientTransform", dochtml1body1svg0.Attributes.GetNamedItem("gradientTransform").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("gradientTransform").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("gradientUnits"));
            Assert.AreEqual("gradientUnits", dochtml1body1svg0.Attributes.GetNamedItem("gradientUnits").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("gradientUnits").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("kernelMatrix"));
            Assert.AreEqual("kernelMatrix", dochtml1body1svg0.Attributes.GetNamedItem("kernelMatrix").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("kernelMatrix").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("kernelUnitLength"));
            Assert.AreEqual("kernelUnitLength", dochtml1body1svg0.Attributes.GetNamedItem("kernelUnitLength").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("kernelUnitLength").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("keyPoints"));
            Assert.AreEqual("keyPoints", dochtml1body1svg0.Attributes.GetNamedItem("keyPoints").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("keyPoints").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("keySplines"));
            Assert.AreEqual("keySplines", dochtml1body1svg0.Attributes.GetNamedItem("keySplines").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("keySplines").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("keyTimes"));
            Assert.AreEqual("keyTimes", dochtml1body1svg0.Attributes.GetNamedItem("keyTimes").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("keyTimes").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("lengthAdjust"));
            Assert.AreEqual("lengthAdjust", dochtml1body1svg0.Attributes.GetNamedItem("lengthAdjust").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("lengthAdjust").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("limitingConeAngle"));
            Assert.AreEqual("limitingConeAngle", dochtml1body1svg0.Attributes.GetNamedItem("limitingConeAngle").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("limitingConeAngle").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("markerHeight"));
            Assert.AreEqual("markerHeight", dochtml1body1svg0.Attributes.GetNamedItem("markerHeight").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("markerHeight").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("markerUnits"));
            Assert.AreEqual("markerUnits", dochtml1body1svg0.Attributes.GetNamedItem("markerUnits").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("markerUnits").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("markerWidth"));
            Assert.AreEqual("markerWidth", dochtml1body1svg0.Attributes.GetNamedItem("markerWidth").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("markerWidth").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("maskContentUnits"));
            Assert.AreEqual("maskContentUnits", dochtml1body1svg0.Attributes.GetNamedItem("maskContentUnits").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("maskContentUnits").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("maskUnits"));
            Assert.AreEqual("maskUnits", dochtml1body1svg0.Attributes.GetNamedItem("maskUnits").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("maskUnits").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("numOctaves"));
            Assert.AreEqual("numOctaves", dochtml1body1svg0.Attributes.GetNamedItem("numOctaves").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("numOctaves").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("pathLength"));
            Assert.AreEqual("pathLength", dochtml1body1svg0.Attributes.GetNamedItem("pathLength").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("pathLength").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("patternContentUnits"));
            Assert.AreEqual("patternContentUnits", dochtml1body1svg0.Attributes.GetNamedItem("patternContentUnits").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("patternContentUnits").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("patternTransform"));
            Assert.AreEqual("patternTransform", dochtml1body1svg0.Attributes.GetNamedItem("patternTransform").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("patternTransform").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("patternUnits"));
            Assert.AreEqual("patternUnits", dochtml1body1svg0.Attributes.GetNamedItem("patternUnits").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("patternUnits").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("pointsAtX"));
            Assert.AreEqual("pointsAtX", dochtml1body1svg0.Attributes.GetNamedItem("pointsAtX").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("pointsAtX").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("pointsAtY"));
            Assert.AreEqual("pointsAtY", dochtml1body1svg0.Attributes.GetNamedItem("pointsAtY").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("pointsAtY").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("pointsAtZ"));
            Assert.AreEqual("pointsAtZ", dochtml1body1svg0.Attributes.GetNamedItem("pointsAtZ").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("pointsAtZ").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("preserveAlpha"));
            Assert.AreEqual("preserveAlpha", dochtml1body1svg0.Attributes.GetNamedItem("preserveAlpha").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("preserveAlpha").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("preserveAspectRatio"));
            Assert.AreEqual("preserveAspectRatio", dochtml1body1svg0.Attributes.GetNamedItem("preserveAspectRatio").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("preserveAspectRatio").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("primitiveUnits"));
            Assert.AreEqual("primitiveUnits", dochtml1body1svg0.Attributes.GetNamedItem("primitiveUnits").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("primitiveUnits").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("refX"));
            Assert.AreEqual("refX", dochtml1body1svg0.Attributes.GetNamedItem("refX").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("refX").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("refY"));
            Assert.AreEqual("refY", dochtml1body1svg0.Attributes.GetNamedItem("refY").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("refY").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("repeatCount"));
            Assert.AreEqual("repeatCount", dochtml1body1svg0.Attributes.GetNamedItem("repeatCount").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("repeatCount").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("repeatDur"));
            Assert.AreEqual("repeatDur", dochtml1body1svg0.Attributes.GetNamedItem("repeatDur").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("repeatDur").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("requiredExtensions"));
            Assert.AreEqual("requiredExtensions", dochtml1body1svg0.Attributes.GetNamedItem("requiredExtensions").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("requiredExtensions").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("requiredFeatures"));
            Assert.AreEqual("requiredFeatures", dochtml1body1svg0.Attributes.GetNamedItem("requiredFeatures").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("requiredFeatures").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("specularConstant"));
            Assert.AreEqual("specularConstant", dochtml1body1svg0.Attributes.GetNamedItem("specularConstant").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("specularConstant").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("specularExponent"));
            Assert.AreEqual("specularExponent", dochtml1body1svg0.Attributes.GetNamedItem("specularExponent").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("specularExponent").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("spreadMethod"));
            Assert.AreEqual("spreadMethod", dochtml1body1svg0.Attributes.GetNamedItem("spreadMethod").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("spreadMethod").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("startOffset"));
            Assert.AreEqual("startOffset", dochtml1body1svg0.Attributes.GetNamedItem("startOffset").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("startOffset").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("stdDeviation"));
            Assert.AreEqual("stdDeviation", dochtml1body1svg0.Attributes.GetNamedItem("stdDeviation").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("stdDeviation").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("stitchTiles"));
            Assert.AreEqual("stitchTiles", dochtml1body1svg0.Attributes.GetNamedItem("stitchTiles").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("stitchTiles").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("surfaceScale"));
            Assert.AreEqual("surfaceScale", dochtml1body1svg0.Attributes.GetNamedItem("surfaceScale").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("surfaceScale").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("systemLanguage"));
            Assert.AreEqual("systemLanguage", dochtml1body1svg0.Attributes.GetNamedItem("systemLanguage").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("systemLanguage").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("tableValues"));
            Assert.AreEqual("tableValues", dochtml1body1svg0.Attributes.GetNamedItem("tableValues").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("tableValues").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("targetX"));
            Assert.AreEqual("targetX", dochtml1body1svg0.Attributes.GetNamedItem("targetX").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("targetX").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("targetY"));
            Assert.AreEqual("targetY", dochtml1body1svg0.Attributes.GetNamedItem("targetY").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("targetY").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("textLength"));
            Assert.AreEqual("textLength", dochtml1body1svg0.Attributes.GetNamedItem("textLength").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("textLength").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("viewBox"));
            Assert.AreEqual("viewBox", dochtml1body1svg0.Attributes.GetNamedItem("viewBox").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("viewBox").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("viewTarget"));
            Assert.AreEqual("viewTarget", dochtml1body1svg0.Attributes.GetNamedItem("viewTarget").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("viewTarget").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("xChannelSelector"));
            Assert.AreEqual("xChannelSelector", dochtml1body1svg0.Attributes.GetNamedItem("xChannelSelector").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("xChannelSelector").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("yChannelSelector"));
            Assert.AreEqual("yChannelSelector", dochtml1body1svg0.Attributes.GetNamedItem("yChannelSelector").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("yChannelSelector").Value);
            Assert.IsNotNull(dochtml1body1svg0.Attributes.GetNamedItem("zoomAndPan"));
            Assert.AreEqual("zoomAndPan", dochtml1body1svg0.Attributes.GetNamedItem("zoomAndPan").Name);
            Assert.AreEqual("", dochtml1body1svg0.Attributes.GetNamedItem("zoomAndPan").Value);

        }

        [Test]
        public void SvgCheckTagCaseNormalUnchanged()
        {
            var doc = (@"<!DOCTYPE html><body><svg><altGlyph /><altGlyphDef /><altGlyphItem /><animateColor /><animateMotion /><animateTransform /><clipPath /><feBlend /><feColorMatrix /><feComponentTransfer /><feComposite /><feConvolveMatrix /><feDiffuseLighting /><feDisplacementMap /><feDistantLight /><feFlood /><feFuncA /><feFuncB /><feFuncG /><feFuncR /><feGaussianBlur /><feImage /><feMerge /><feMergeNode /><feMorphology /><feOffset /><fePointLight /><feSpecularLighting /><feSpotLight /><feTile /><feTurbulence /><foreignObject /><glyphRef /><linearGradient /><radialGradient /><textPath /></svg>").ToHtmlDocument();

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

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(36, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml1body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);

            var dochtml1body1svg0altGlyph0 = dochtml1body1svg0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1svg0altGlyph0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0altGlyph0.Attributes.Length);
            Assert.AreEqual("altGlyph", dochtml1body1svg0altGlyph0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0altGlyph0.NodeType);

            var dochtml1body1svg0altGlyphDef1 = dochtml1body1svg0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1svg0altGlyphDef1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0altGlyphDef1.Attributes.Length);
            Assert.AreEqual("altGlyphDef", dochtml1body1svg0altGlyphDef1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0altGlyphDef1.NodeType);

            var dochtml1body1svg0altGlyphItem2 = dochtml1body1svg0.ChildNodes[2] as Element;
            Assert.AreEqual(0, dochtml1body1svg0altGlyphItem2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0altGlyphItem2.Attributes.Length);
            Assert.AreEqual("altGlyphItem", dochtml1body1svg0altGlyphItem2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0altGlyphItem2.NodeType);

            var dochtml1body1svg0animateColor3 = dochtml1body1svg0.ChildNodes[3] as Element;
            Assert.AreEqual(0, dochtml1body1svg0animateColor3.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0animateColor3.Attributes.Length);
            Assert.AreEqual("animateColor", dochtml1body1svg0animateColor3.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0animateColor3.NodeType);

            var dochtml1body1svg0animateMotion4 = dochtml1body1svg0.ChildNodes[4] as Element;
            Assert.AreEqual(0, dochtml1body1svg0animateMotion4.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0animateMotion4.Attributes.Length);
            Assert.AreEqual("animateMotion", dochtml1body1svg0animateMotion4.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0animateMotion4.NodeType);

            var dochtml1body1svg0animateTransform5 = dochtml1body1svg0.ChildNodes[5] as Element;
            Assert.AreEqual(0, dochtml1body1svg0animateTransform5.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0animateTransform5.Attributes.Length);
            Assert.AreEqual("animateTransform", dochtml1body1svg0animateTransform5.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0animateTransform5.NodeType);

            var dochtml1body1svg0clipPath6 = dochtml1body1svg0.ChildNodes[6] as Element;
            Assert.AreEqual(0, dochtml1body1svg0clipPath6.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0clipPath6.Attributes.Length);
            Assert.AreEqual("clipPath", dochtml1body1svg0clipPath6.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0clipPath6.NodeType);

            var dochtml1body1svg0feBlend7 = dochtml1body1svg0.ChildNodes[7] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feBlend7.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feBlend7.Attributes.Length);
            Assert.AreEqual("feBlend", dochtml1body1svg0feBlend7.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feBlend7.NodeType);

            var dochtml1body1svg0feColorMatrix8 = dochtml1body1svg0.ChildNodes[8] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feColorMatrix8.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feColorMatrix8.Attributes.Length);
            Assert.AreEqual("feColorMatrix", dochtml1body1svg0feColorMatrix8.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feColorMatrix8.NodeType);

            var dochtml1body1svg0feComponentTransfer9 = dochtml1body1svg0.ChildNodes[9] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feComponentTransfer9.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feComponentTransfer9.Attributes.Length);
            Assert.AreEqual("feComponentTransfer", dochtml1body1svg0feComponentTransfer9.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feComponentTransfer9.NodeType);

            var dochtml1body1svg0feComposite10 = dochtml1body1svg0.ChildNodes[10] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feComposite10.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feComposite10.Attributes.Length);
            Assert.AreEqual("feComposite", dochtml1body1svg0feComposite10.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feComposite10.NodeType);

            var dochtml1body1svg0feConvolveMatrix11 = dochtml1body1svg0.ChildNodes[11] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feConvolveMatrix11.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feConvolveMatrix11.Attributes.Length);
            Assert.AreEqual("feConvolveMatrix", dochtml1body1svg0feConvolveMatrix11.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feConvolveMatrix11.NodeType);

            var dochtml1body1svg0feDiffuseLighting12 = dochtml1body1svg0.ChildNodes[12] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feDiffuseLighting12.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feDiffuseLighting12.Attributes.Length);
            Assert.AreEqual("feDiffuseLighting", dochtml1body1svg0feDiffuseLighting12.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feDiffuseLighting12.NodeType);

            var dochtml1body1svg0feDisplacementMap13 = dochtml1body1svg0.ChildNodes[13] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feDisplacementMap13.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feDisplacementMap13.Attributes.Length);
            Assert.AreEqual("feDisplacementMap", dochtml1body1svg0feDisplacementMap13.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feDisplacementMap13.NodeType);

            var dochtml1body1svg0feDistantLight14 = dochtml1body1svg0.ChildNodes[14] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feDistantLight14.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feDistantLight14.Attributes.Length);
            Assert.AreEqual("feDistantLight", dochtml1body1svg0feDistantLight14.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feDistantLight14.NodeType);

            var dochtml1body1svg0feFlood15 = dochtml1body1svg0.ChildNodes[15] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feFlood15.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feFlood15.Attributes.Length);
            Assert.AreEqual("feFlood", dochtml1body1svg0feFlood15.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feFlood15.NodeType);

            var dochtml1body1svg0feFuncA16 = dochtml1body1svg0.ChildNodes[16] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feFuncA16.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feFuncA16.Attributes.Length);
            Assert.AreEqual("feFuncA", dochtml1body1svg0feFuncA16.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feFuncA16.NodeType);

            var dochtml1body1svg0feFuncB17 = dochtml1body1svg0.ChildNodes[17] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feFuncB17.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feFuncB17.Attributes.Length);
            Assert.AreEqual("feFuncB", dochtml1body1svg0feFuncB17.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feFuncB17.NodeType);

            var dochtml1body1svg0feFuncG18 = dochtml1body1svg0.ChildNodes[18] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feFuncG18.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feFuncG18.Attributes.Length);
            Assert.AreEqual("feFuncG", dochtml1body1svg0feFuncG18.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feFuncG18.NodeType);

            var dochtml1body1svg0feFuncR19 = dochtml1body1svg0.ChildNodes[19] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feFuncR19.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feFuncR19.Attributes.Length);
            Assert.AreEqual("feFuncR", dochtml1body1svg0feFuncR19.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feFuncR19.NodeType);

            var dochtml1body1svg0feGaussianBlur20 = dochtml1body1svg0.ChildNodes[20] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feGaussianBlur20.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feGaussianBlur20.Attributes.Length);
            Assert.AreEqual("feGaussianBlur", dochtml1body1svg0feGaussianBlur20.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feGaussianBlur20.NodeType);

            var dochtml1body1svg0feImage21 = dochtml1body1svg0.ChildNodes[21] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feImage21.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feImage21.Attributes.Length);
            Assert.AreEqual("feImage", dochtml1body1svg0feImage21.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feImage21.NodeType);

            var dochtml1body1svg0feMerge22 = dochtml1body1svg0.ChildNodes[22] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feMerge22.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feMerge22.Attributes.Length);
            Assert.AreEqual("feMerge", dochtml1body1svg0feMerge22.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feMerge22.NodeType);

            var dochtml1body1svg0feMergeNode23 = dochtml1body1svg0.ChildNodes[23] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feMergeNode23.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feMergeNode23.Attributes.Length);
            Assert.AreEqual("feMergeNode", dochtml1body1svg0feMergeNode23.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feMergeNode23.NodeType);

            var dochtml1body1svg0feMorphology24 = dochtml1body1svg0.ChildNodes[24] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feMorphology24.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feMorphology24.Attributes.Length);
            Assert.AreEqual("feMorphology", dochtml1body1svg0feMorphology24.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feMorphology24.NodeType);

            var dochtml1body1svg0feOffset25 = dochtml1body1svg0.ChildNodes[25] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feOffset25.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feOffset25.Attributes.Length);
            Assert.AreEqual("feOffset", dochtml1body1svg0feOffset25.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feOffset25.NodeType);

            var dochtml1body1svg0fePointLight26 = dochtml1body1svg0.ChildNodes[26] as Element;
            Assert.AreEqual(0, dochtml1body1svg0fePointLight26.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0fePointLight26.Attributes.Length);
            Assert.AreEqual("fePointLight", dochtml1body1svg0fePointLight26.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0fePointLight26.NodeType);

            var dochtml1body1svg0feSpecularLighting27 = dochtml1body1svg0.ChildNodes[27] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feSpecularLighting27.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feSpecularLighting27.Attributes.Length);
            Assert.AreEqual("feSpecularLighting", dochtml1body1svg0feSpecularLighting27.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feSpecularLighting27.NodeType);

            var dochtml1body1svg0feSpotLight28 = dochtml1body1svg0.ChildNodes[28] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feSpotLight28.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feSpotLight28.Attributes.Length);
            Assert.AreEqual("feSpotLight", dochtml1body1svg0feSpotLight28.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feSpotLight28.NodeType);

            var dochtml1body1svg0feTile29 = dochtml1body1svg0.ChildNodes[29] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feTile29.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feTile29.Attributes.Length);
            Assert.AreEqual("feTile", dochtml1body1svg0feTile29.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feTile29.NodeType);

            var dochtml1body1svg0feTurbulence30 = dochtml1body1svg0.ChildNodes[30] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feTurbulence30.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feTurbulence30.Attributes.Length);
            Assert.AreEqual("feTurbulence", dochtml1body1svg0feTurbulence30.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feTurbulence30.NodeType);

            var dochtml1body1svg0foreignObject31 = dochtml1body1svg0.ChildNodes[31] as Element;
            Assert.AreEqual(0, dochtml1body1svg0foreignObject31.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0foreignObject31.Attributes.Length);
            Assert.AreEqual("foreignObject", dochtml1body1svg0foreignObject31.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0foreignObject31.NodeType);

            var dochtml1body1svg0glyphRef32 = dochtml1body1svg0.ChildNodes[32] as Element;
            Assert.AreEqual(0, dochtml1body1svg0glyphRef32.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0glyphRef32.Attributes.Length);
            Assert.AreEqual("glyphRef", dochtml1body1svg0glyphRef32.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0glyphRef32.NodeType);

            var dochtml1body1svg0linearGradient33 = dochtml1body1svg0.ChildNodes[33] as Element;
            Assert.AreEqual(0, dochtml1body1svg0linearGradient33.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0linearGradient33.Attributes.Length);
            Assert.AreEqual("linearGradient", dochtml1body1svg0linearGradient33.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0linearGradient33.NodeType);

            var dochtml1body1svg0radialGradient34 = dochtml1body1svg0.ChildNodes[34] as Element;
            Assert.AreEqual(0, dochtml1body1svg0radialGradient34.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0radialGradient34.Attributes.Length);
            Assert.AreEqual("radialGradient", dochtml1body1svg0radialGradient34.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0radialGradient34.NodeType);

            var dochtml1body1svg0textPath35 = dochtml1body1svg0.ChildNodes[35] as Element;
            Assert.AreEqual(0, dochtml1body1svg0textPath35.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0textPath35.Attributes.Length);
            Assert.AreEqual("textPath", dochtml1body1svg0textPath35.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0textPath35.NodeType);
        }

        [Test]
        public void SvgCheckTagCaseLowercaseModified()
        {
            var doc = (@"<!DOCTYPE html><body><svg><altglyph /><altglyphdef /><altglyphitem /><animatecolor /><animatemotion /><animatetransform /><clippath /><feblend /><fecolormatrix /><fecomponenttransfer /><fecomposite /><feconvolvematrix /><fediffuselighting /><fedisplacementmap /><fedistantlight /><feflood /><fefunca /><fefuncb /><fefuncg /><fefuncr /><fegaussianblur /><feimage /><femerge /><femergenode /><femorphology /><feoffset /><fepointlight /><fespecularlighting /><fespotlight /><fetile /><feturbulence /><foreignobject /><glyphref /><lineargradient /><radialgradient /><textpath /></svg>").ToHtmlDocument();

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

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(36, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml1body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);

            var dochtml1body1svg0altGlyph0 = dochtml1body1svg0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1svg0altGlyph0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0altGlyph0.Attributes.Length);
            Assert.AreEqual("altGlyph", dochtml1body1svg0altGlyph0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0altGlyph0.NodeType);

            var dochtml1body1svg0altGlyphDef1 = dochtml1body1svg0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1svg0altGlyphDef1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0altGlyphDef1.Attributes.Length);
            Assert.AreEqual("altGlyphDef", dochtml1body1svg0altGlyphDef1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0altGlyphDef1.NodeType);

            var dochtml1body1svg0altGlyphItem2 = dochtml1body1svg0.ChildNodes[2] as Element;
            Assert.AreEqual(0, dochtml1body1svg0altGlyphItem2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0altGlyphItem2.Attributes.Length);
            Assert.AreEqual("altGlyphItem", dochtml1body1svg0altGlyphItem2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0altGlyphItem2.NodeType);

            var dochtml1body1svg0animateColor3 = dochtml1body1svg0.ChildNodes[3] as Element;
            Assert.AreEqual(0, dochtml1body1svg0animateColor3.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0animateColor3.Attributes.Length);
            Assert.AreEqual("animateColor", dochtml1body1svg0animateColor3.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0animateColor3.NodeType);

            var dochtml1body1svg0animateMotion4 = dochtml1body1svg0.ChildNodes[4] as Element;
            Assert.AreEqual(0, dochtml1body1svg0animateMotion4.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0animateMotion4.Attributes.Length);
            Assert.AreEqual("animateMotion", dochtml1body1svg0animateMotion4.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0animateMotion4.NodeType);

            var dochtml1body1svg0animateTransform5 = dochtml1body1svg0.ChildNodes[5] as Element;
            Assert.AreEqual(0, dochtml1body1svg0animateTransform5.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0animateTransform5.Attributes.Length);
            Assert.AreEqual("animateTransform", dochtml1body1svg0animateTransform5.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0animateTransform5.NodeType);

            var dochtml1body1svg0clipPath6 = dochtml1body1svg0.ChildNodes[6] as Element;
            Assert.AreEqual(0, dochtml1body1svg0clipPath6.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0clipPath6.Attributes.Length);
            Assert.AreEqual("clipPath", dochtml1body1svg0clipPath6.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0clipPath6.NodeType);

            var dochtml1body1svg0feBlend7 = dochtml1body1svg0.ChildNodes[7] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feBlend7.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feBlend7.Attributes.Length);
            Assert.AreEqual("feBlend", dochtml1body1svg0feBlend7.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feBlend7.NodeType);

            var dochtml1body1svg0feColorMatrix8 = dochtml1body1svg0.ChildNodes[8] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feColorMatrix8.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feColorMatrix8.Attributes.Length);
            Assert.AreEqual("feColorMatrix", dochtml1body1svg0feColorMatrix8.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feColorMatrix8.NodeType);

            var dochtml1body1svg0feComponentTransfer9 = dochtml1body1svg0.ChildNodes[9] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feComponentTransfer9.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feComponentTransfer9.Attributes.Length);
            Assert.AreEqual("feComponentTransfer", dochtml1body1svg0feComponentTransfer9.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feComponentTransfer9.NodeType);

            var dochtml1body1svg0feComposite10 = dochtml1body1svg0.ChildNodes[10] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feComposite10.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feComposite10.Attributes.Length);
            Assert.AreEqual("feComposite", dochtml1body1svg0feComposite10.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feComposite10.NodeType);

            var dochtml1body1svg0feConvolveMatrix11 = dochtml1body1svg0.ChildNodes[11] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feConvolveMatrix11.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feConvolveMatrix11.Attributes.Length);
            Assert.AreEqual("feConvolveMatrix", dochtml1body1svg0feConvolveMatrix11.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feConvolveMatrix11.NodeType);

            var dochtml1body1svg0feDiffuseLighting12 = dochtml1body1svg0.ChildNodes[12] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feDiffuseLighting12.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feDiffuseLighting12.Attributes.Length);
            Assert.AreEqual("feDiffuseLighting", dochtml1body1svg0feDiffuseLighting12.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feDiffuseLighting12.NodeType);

            var dochtml1body1svg0feDisplacementMap13 = dochtml1body1svg0.ChildNodes[13] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feDisplacementMap13.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feDisplacementMap13.Attributes.Length);
            Assert.AreEqual("feDisplacementMap", dochtml1body1svg0feDisplacementMap13.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feDisplacementMap13.NodeType);

            var dochtml1body1svg0feDistantLight14 = dochtml1body1svg0.ChildNodes[14] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feDistantLight14.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feDistantLight14.Attributes.Length);
            Assert.AreEqual("feDistantLight", dochtml1body1svg0feDistantLight14.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feDistantLight14.NodeType);

            var dochtml1body1svg0feFlood15 = dochtml1body1svg0.ChildNodes[15] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feFlood15.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feFlood15.Attributes.Length);
            Assert.AreEqual("feFlood", dochtml1body1svg0feFlood15.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feFlood15.NodeType);

            var dochtml1body1svg0feFuncA16 = dochtml1body1svg0.ChildNodes[16] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feFuncA16.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feFuncA16.Attributes.Length);
            Assert.AreEqual("feFuncA", dochtml1body1svg0feFuncA16.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feFuncA16.NodeType);

            var dochtml1body1svg0feFuncB17 = dochtml1body1svg0.ChildNodes[17] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feFuncB17.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feFuncB17.Attributes.Length);
            Assert.AreEqual("feFuncB", dochtml1body1svg0feFuncB17.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feFuncB17.NodeType);

            var dochtml1body1svg0feFuncG18 = dochtml1body1svg0.ChildNodes[18] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feFuncG18.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feFuncG18.Attributes.Length);
            Assert.AreEqual("feFuncG", dochtml1body1svg0feFuncG18.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feFuncG18.NodeType);

            var dochtml1body1svg0feFuncR19 = dochtml1body1svg0.ChildNodes[19] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feFuncR19.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feFuncR19.Attributes.Length);
            Assert.AreEqual("feFuncR", dochtml1body1svg0feFuncR19.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feFuncR19.NodeType);

            var dochtml1body1svg0feGaussianBlur20 = dochtml1body1svg0.ChildNodes[20] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feGaussianBlur20.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feGaussianBlur20.Attributes.Length);
            Assert.AreEqual("feGaussianBlur", dochtml1body1svg0feGaussianBlur20.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feGaussianBlur20.NodeType);

            var dochtml1body1svg0feImage21 = dochtml1body1svg0.ChildNodes[21] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feImage21.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feImage21.Attributes.Length);
            Assert.AreEqual("feImage", dochtml1body1svg0feImage21.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feImage21.NodeType);

            var dochtml1body1svg0feMerge22 = dochtml1body1svg0.ChildNodes[22] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feMerge22.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feMerge22.Attributes.Length);
            Assert.AreEqual("feMerge", dochtml1body1svg0feMerge22.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feMerge22.NodeType);

            var dochtml1body1svg0feMergeNode23 = dochtml1body1svg0.ChildNodes[23] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feMergeNode23.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feMergeNode23.Attributes.Length);
            Assert.AreEqual("feMergeNode", dochtml1body1svg0feMergeNode23.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feMergeNode23.NodeType);

            var dochtml1body1svg0feMorphology24 = dochtml1body1svg0.ChildNodes[24] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feMorphology24.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feMorphology24.Attributes.Length);
            Assert.AreEqual("feMorphology", dochtml1body1svg0feMorphology24.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feMorphology24.NodeType);

            var dochtml1body1svg0feOffset25 = dochtml1body1svg0.ChildNodes[25] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feOffset25.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feOffset25.Attributes.Length);
            Assert.AreEqual("feOffset", dochtml1body1svg0feOffset25.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feOffset25.NodeType);

            var dochtml1body1svg0fePointLight26 = dochtml1body1svg0.ChildNodes[26] as Element;
            Assert.AreEqual(0, dochtml1body1svg0fePointLight26.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0fePointLight26.Attributes.Length);
            Assert.AreEqual("fePointLight", dochtml1body1svg0fePointLight26.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0fePointLight26.NodeType);

            var dochtml1body1svg0feSpecularLighting27 = dochtml1body1svg0.ChildNodes[27] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feSpecularLighting27.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feSpecularLighting27.Attributes.Length);
            Assert.AreEqual("feSpecularLighting", dochtml1body1svg0feSpecularLighting27.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feSpecularLighting27.NodeType);

            var dochtml1body1svg0feSpotLight28 = dochtml1body1svg0.ChildNodes[28] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feSpotLight28.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feSpotLight28.Attributes.Length);
            Assert.AreEqual("feSpotLight", dochtml1body1svg0feSpotLight28.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feSpotLight28.NodeType);

            var dochtml1body1svg0feTile29 = dochtml1body1svg0.ChildNodes[29] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feTile29.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feTile29.Attributes.Length);
            Assert.AreEqual("feTile", dochtml1body1svg0feTile29.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feTile29.NodeType);

            var dochtml1body1svg0feTurbulence30 = dochtml1body1svg0.ChildNodes[30] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feTurbulence30.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feTurbulence30.Attributes.Length);
            Assert.AreEqual("feTurbulence", dochtml1body1svg0feTurbulence30.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feTurbulence30.NodeType);

            var dochtml1body1svg0foreignObject31 = dochtml1body1svg0.ChildNodes[31] as Element;
            Assert.AreEqual(0, dochtml1body1svg0foreignObject31.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0foreignObject31.Attributes.Length);
            Assert.AreEqual("foreignObject", dochtml1body1svg0foreignObject31.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0foreignObject31.NodeType);

            var dochtml1body1svg0glyphRef32 = dochtml1body1svg0.ChildNodes[32] as Element;
            Assert.AreEqual(0, dochtml1body1svg0glyphRef32.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0glyphRef32.Attributes.Length);
            Assert.AreEqual("glyphRef", dochtml1body1svg0glyphRef32.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0glyphRef32.NodeType);

            var dochtml1body1svg0linearGradient33 = dochtml1body1svg0.ChildNodes[33] as Element;
            Assert.AreEqual(0, dochtml1body1svg0linearGradient33.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0linearGradient33.Attributes.Length);
            Assert.AreEqual("linearGradient", dochtml1body1svg0linearGradient33.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0linearGradient33.NodeType);

            var dochtml1body1svg0radialGradient34 = dochtml1body1svg0.ChildNodes[34] as Element;
            Assert.AreEqual(0, dochtml1body1svg0radialGradient34.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0radialGradient34.Attributes.Length);
            Assert.AreEqual("radialGradient", dochtml1body1svg0radialGradient34.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0radialGradient34.NodeType);

            var dochtml1body1svg0textPath35 = dochtml1body1svg0.ChildNodes[35] as Element;
            Assert.AreEqual(0, dochtml1body1svg0textPath35.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0textPath35.Attributes.Length);
            Assert.AreEqual("textPath", dochtml1body1svg0textPath35.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0textPath35.NodeType);
        }

        [Test]
        public void SvgCheckTagCaseUppercaseModified()
        {
            var doc = (@"<!DOCTYPE html><BODY><SVG><ALTGLYPH /><ALTGLYPHDEF /><ALTGLYPHITEM /><ANIMATECOLOR /><ANIMATEMOTION /><ANIMATETRANSFORM /><CLIPPATH /><FEBLEND /><FECOLORMATRIX /><FECOMPONENTTRANSFER /><FECOMPOSITE /><FECONVOLVEMATRIX /><FEDIFFUSELIGHTING /><FEDISPLACEMENTMAP /><FEDISTANTLIGHT /><FEFLOOD /><FEFUNCA /><FEFUNCB /><FEFUNCG /><FEFUNCR /><FEGAUSSIANBLUR /><FEIMAGE /><FEMERGE /><FEMERGENODE /><FEMORPHOLOGY /><FEOFFSET /><FEPOINTLIGHT /><FESPECULARLIGHTING /><FESPOTLIGHT /><FETILE /><FETURBULENCE /><FOREIGNOBJECT /><GLYPHREF /><LINEARGRADIENT /><RADIALGRADIENT /><TEXTPATH /></SVG>").ToHtmlDocument();

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

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(36, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml1body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);

            var dochtml1body1svg0altGlyph0 = dochtml1body1svg0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1svg0altGlyph0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0altGlyph0.Attributes.Length);
            Assert.AreEqual("altGlyph", dochtml1body1svg0altGlyph0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0altGlyph0.NodeType);

            var dochtml1body1svg0altGlyphDef1 = dochtml1body1svg0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1svg0altGlyphDef1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0altGlyphDef1.Attributes.Length);
            Assert.AreEqual("altGlyphDef", dochtml1body1svg0altGlyphDef1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0altGlyphDef1.NodeType);

            var dochtml1body1svg0altGlyphItem2 = dochtml1body1svg0.ChildNodes[2] as Element;
            Assert.AreEqual(0, dochtml1body1svg0altGlyphItem2.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0altGlyphItem2.Attributes.Length);
            Assert.AreEqual("altGlyphItem", dochtml1body1svg0altGlyphItem2.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0altGlyphItem2.NodeType);

            var dochtml1body1svg0animateColor3 = dochtml1body1svg0.ChildNodes[3] as Element;
            Assert.AreEqual(0, dochtml1body1svg0animateColor3.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0animateColor3.Attributes.Length);
            Assert.AreEqual("animateColor", dochtml1body1svg0animateColor3.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0animateColor3.NodeType);

            var dochtml1body1svg0animateMotion4 = dochtml1body1svg0.ChildNodes[4] as Element;
            Assert.AreEqual(0, dochtml1body1svg0animateMotion4.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0animateMotion4.Attributes.Length);
            Assert.AreEqual("animateMotion", dochtml1body1svg0animateMotion4.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0animateMotion4.NodeType);

            var dochtml1body1svg0animateTransform5 = dochtml1body1svg0.ChildNodes[5] as Element;
            Assert.AreEqual(0, dochtml1body1svg0animateTransform5.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0animateTransform5.Attributes.Length);
            Assert.AreEqual("animateTransform", dochtml1body1svg0animateTransform5.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0animateTransform5.NodeType);

            var dochtml1body1svg0clipPath6 = dochtml1body1svg0.ChildNodes[6] as Element;
            Assert.AreEqual(0, dochtml1body1svg0clipPath6.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0clipPath6.Attributes.Length);
            Assert.AreEqual("clipPath", dochtml1body1svg0clipPath6.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0clipPath6.NodeType);

            var dochtml1body1svg0feBlend7 = dochtml1body1svg0.ChildNodes[7] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feBlend7.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feBlend7.Attributes.Length);
            Assert.AreEqual("feBlend", dochtml1body1svg0feBlend7.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feBlend7.NodeType);

            var dochtml1body1svg0feColorMatrix8 = dochtml1body1svg0.ChildNodes[8] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feColorMatrix8.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feColorMatrix8.Attributes.Length);
            Assert.AreEqual("feColorMatrix", dochtml1body1svg0feColorMatrix8.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feColorMatrix8.NodeType);

            var dochtml1body1svg0feComponentTransfer9 = dochtml1body1svg0.ChildNodes[9] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feComponentTransfer9.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feComponentTransfer9.Attributes.Length);
            Assert.AreEqual("feComponentTransfer", dochtml1body1svg0feComponentTransfer9.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feComponentTransfer9.NodeType);

            var dochtml1body1svg0feComposite10 = dochtml1body1svg0.ChildNodes[10] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feComposite10.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feComposite10.Attributes.Length);
            Assert.AreEqual("feComposite", dochtml1body1svg0feComposite10.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feComposite10.NodeType);

            var dochtml1body1svg0feConvolveMatrix11 = dochtml1body1svg0.ChildNodes[11] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feConvolveMatrix11.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feConvolveMatrix11.Attributes.Length);
            Assert.AreEqual("feConvolveMatrix", dochtml1body1svg0feConvolveMatrix11.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feConvolveMatrix11.NodeType);

            var dochtml1body1svg0feDiffuseLighting12 = dochtml1body1svg0.ChildNodes[12] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feDiffuseLighting12.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feDiffuseLighting12.Attributes.Length);
            Assert.AreEqual("feDiffuseLighting", dochtml1body1svg0feDiffuseLighting12.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feDiffuseLighting12.NodeType);

            var dochtml1body1svg0feDisplacementMap13 = dochtml1body1svg0.ChildNodes[13] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feDisplacementMap13.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feDisplacementMap13.Attributes.Length);
            Assert.AreEqual("feDisplacementMap", dochtml1body1svg0feDisplacementMap13.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feDisplacementMap13.NodeType);

            var dochtml1body1svg0feDistantLight14 = dochtml1body1svg0.ChildNodes[14] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feDistantLight14.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feDistantLight14.Attributes.Length);
            Assert.AreEqual("feDistantLight", dochtml1body1svg0feDistantLight14.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feDistantLight14.NodeType);

            var dochtml1body1svg0feFlood15 = dochtml1body1svg0.ChildNodes[15] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feFlood15.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feFlood15.Attributes.Length);
            Assert.AreEqual("feFlood", dochtml1body1svg0feFlood15.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feFlood15.NodeType);

            var dochtml1body1svg0feFuncA16 = dochtml1body1svg0.ChildNodes[16] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feFuncA16.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feFuncA16.Attributes.Length);
            Assert.AreEqual("feFuncA", dochtml1body1svg0feFuncA16.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feFuncA16.NodeType);

            var dochtml1body1svg0feFuncB17 = dochtml1body1svg0.ChildNodes[17] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feFuncB17.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feFuncB17.Attributes.Length);
            Assert.AreEqual("feFuncB", dochtml1body1svg0feFuncB17.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feFuncB17.NodeType);

            var dochtml1body1svg0feFuncG18 = dochtml1body1svg0.ChildNodes[18] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feFuncG18.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feFuncG18.Attributes.Length);
            Assert.AreEqual("feFuncG", dochtml1body1svg0feFuncG18.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feFuncG18.NodeType);

            var dochtml1body1svg0feFuncR19 = dochtml1body1svg0.ChildNodes[19] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feFuncR19.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feFuncR19.Attributes.Length);
            Assert.AreEqual("feFuncR", dochtml1body1svg0feFuncR19.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feFuncR19.NodeType);

            var dochtml1body1svg0feGaussianBlur20 = dochtml1body1svg0.ChildNodes[20] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feGaussianBlur20.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feGaussianBlur20.Attributes.Length);
            Assert.AreEqual("feGaussianBlur", dochtml1body1svg0feGaussianBlur20.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feGaussianBlur20.NodeType);

            var dochtml1body1svg0feImage21 = dochtml1body1svg0.ChildNodes[21] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feImage21.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feImage21.Attributes.Length);
            Assert.AreEqual("feImage", dochtml1body1svg0feImage21.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feImage21.NodeType);

            var dochtml1body1svg0feMerge22 = dochtml1body1svg0.ChildNodes[22] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feMerge22.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feMerge22.Attributes.Length);
            Assert.AreEqual("feMerge", dochtml1body1svg0feMerge22.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feMerge22.NodeType);

            var dochtml1body1svg0feMergeNode23 = dochtml1body1svg0.ChildNodes[23] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feMergeNode23.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feMergeNode23.Attributes.Length);
            Assert.AreEqual("feMergeNode", dochtml1body1svg0feMergeNode23.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feMergeNode23.NodeType);

            var dochtml1body1svg0feMorphology24 = dochtml1body1svg0.ChildNodes[24] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feMorphology24.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feMorphology24.Attributes.Length);
            Assert.AreEqual("feMorphology", dochtml1body1svg0feMorphology24.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feMorphology24.NodeType);

            var dochtml1body1svg0feOffset25 = dochtml1body1svg0.ChildNodes[25] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feOffset25.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feOffset25.Attributes.Length);
            Assert.AreEqual("feOffset", dochtml1body1svg0feOffset25.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feOffset25.NodeType);

            var dochtml1body1svg0fePointLight26 = dochtml1body1svg0.ChildNodes[26] as Element;
            Assert.AreEqual(0, dochtml1body1svg0fePointLight26.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0fePointLight26.Attributes.Length);
            Assert.AreEqual("fePointLight", dochtml1body1svg0fePointLight26.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0fePointLight26.NodeType);

            var dochtml1body1svg0feSpecularLighting27 = dochtml1body1svg0.ChildNodes[27] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feSpecularLighting27.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feSpecularLighting27.Attributes.Length);
            Assert.AreEqual("feSpecularLighting", dochtml1body1svg0feSpecularLighting27.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feSpecularLighting27.NodeType);

            var dochtml1body1svg0feSpotLight28 = dochtml1body1svg0.ChildNodes[28] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feSpotLight28.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feSpotLight28.Attributes.Length);
            Assert.AreEqual("feSpotLight", dochtml1body1svg0feSpotLight28.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feSpotLight28.NodeType);

            var dochtml1body1svg0feTile29 = dochtml1body1svg0.ChildNodes[29] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feTile29.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feTile29.Attributes.Length);
            Assert.AreEqual("feTile", dochtml1body1svg0feTile29.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feTile29.NodeType);

            var dochtml1body1svg0feTurbulence30 = dochtml1body1svg0.ChildNodes[30] as Element;
            Assert.AreEqual(0, dochtml1body1svg0feTurbulence30.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0feTurbulence30.Attributes.Length);
            Assert.AreEqual("feTurbulence", dochtml1body1svg0feTurbulence30.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0feTurbulence30.NodeType);

            var dochtml1body1svg0foreignObject31 = dochtml1body1svg0.ChildNodes[31] as Element;
            Assert.AreEqual(0, dochtml1body1svg0foreignObject31.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0foreignObject31.Attributes.Length);
            Assert.AreEqual("foreignObject", dochtml1body1svg0foreignObject31.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0foreignObject31.NodeType);

            var dochtml1body1svg0glyphRef32 = dochtml1body1svg0.ChildNodes[32] as Element;
            Assert.AreEqual(0, dochtml1body1svg0glyphRef32.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0glyphRef32.Attributes.Length);
            Assert.AreEqual("glyphRef", dochtml1body1svg0glyphRef32.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0glyphRef32.NodeType);

            var dochtml1body1svg0linearGradient33 = dochtml1body1svg0.ChildNodes[33] as Element;
            Assert.AreEqual(0, dochtml1body1svg0linearGradient33.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0linearGradient33.Attributes.Length);
            Assert.AreEqual("linearGradient", dochtml1body1svg0linearGradient33.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0linearGradient33.NodeType);

            var dochtml1body1svg0radialGradient34 = dochtml1body1svg0.ChildNodes[34] as Element;
            Assert.AreEqual(0, dochtml1body1svg0radialGradient34.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0radialGradient34.Attributes.Length);
            Assert.AreEqual("radialGradient", dochtml1body1svg0radialGradient34.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0radialGradient34.NodeType);

            var dochtml1body1svg0textPath35 = dochtml1body1svg0.ChildNodes[35] as Element;
            Assert.AreEqual(0, dochtml1body1svg0textPath35.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0textPath35.Attributes.Length);
            Assert.AreEqual("textPath", dochtml1body1svg0textPath35.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0textPath35.NodeType);
        }

        [Test]
        public void SvgSingleNodeInBody()
        {
            var doc = (@"<!DOCTYPE html><body><svg><solidColor /></svg>").ToHtmlDocument();

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

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml1body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);

            var dochtml1body1svg0solidcolor0 = dochtml1body1svg0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1svg0solidcolor0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0solidcolor0.Attributes.Length);
            Assert.AreEqual("solidcolor", dochtml1body1svg0solidcolor0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0solidcolor0.NodeType);
        }

        [Test]
        public void SvgSingleElement()
        {
            var doc = (@"<!DOCTYPE html><svg></svg>").ToHtmlDocument();

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

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml1body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);
        }

        [Test]
        public void SvgSingleElementFollowedByCdata()
        {
            var doc = (@"<!DOCTYPE html><svg></svg><![CDATA[a]]>").ToHtmlDocument();

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

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml1body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);

            var dochtml1body1Comment1 = dochtml1body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Comment, dochtml1body1Comment1.NodeType);
            Assert.AreEqual(@"[CDATA[a]]", dochtml1body1Comment1.TextContent);
        }

        [Test]
        public void SvgSingleElementInBody()
        {
            var doc = (@"<!DOCTYPE html><body><svg></svg>").ToHtmlDocument();

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

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml1body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);
        }

        [Test]
        public void SvgSingleElementInSelect()
        {
            var doc = (@"<!DOCTYPE html><body><select><svg></svg></select>").ToHtmlDocument();

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
        public void SvgSingleElementInOptionInSelect()
        {
            var doc = (@"<!DOCTYPE html><body><select><option><svg></svg></option></select>").ToHtmlDocument();

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
        public void SvgSingleElementInTable()
        {
            var doc = (@"<!DOCTYPE html><body><table><svg></svg></table>").ToHtmlDocument();

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

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml1body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);

            var dochtml1body1table1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table1.Attributes.Length);
            Assert.AreEqual("table", dochtml1body1table1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table1.NodeType);
        }

        [Test]
        public void SvgElementWithGroupInTable()
        {
            var doc = (@"<!DOCTYPE html><body><table><svg><g>foo</g></svg></table>").ToHtmlDocument();

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

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml1body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);

            var dochtml1body1svg0g0 = dochtml1body1svg0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1svg0g0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0g0.Attributes.Length);
            Assert.AreEqual("g", dochtml1body1svg0g0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0g0.NodeType);

            var dochtml1body1svg0g0Text0 = dochtml1body1svg0g0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1svg0g0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1svg0g0Text0.TextContent);

            var dochtml1body1table1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table1.Attributes.Length);
            Assert.AreEqual("table", dochtml1body1table1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table1.NodeType);
        }

        [Test]
        public void SvgElementWithGroupAndTextInTable()
        {
            var doc = (@"<!DOCTYPE html><body><table><svg><g>foo</g><g>bar</g></svg></table>").ToHtmlDocument();

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

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml1body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);

            var dochtml1body1svg0g0 = dochtml1body1svg0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1svg0g0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0g0.Attributes.Length);
            Assert.AreEqual("g", dochtml1body1svg0g0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0g0.NodeType);

            var dochtml1body1svg0g0Text0 = dochtml1body1svg0g0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1svg0g0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1svg0g0Text0.TextContent);

            var dochtml1body1svg0g1 = dochtml1body1svg0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1svg0g1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0g1.Attributes.Length);
            Assert.AreEqual("g", dochtml1body1svg0g1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0g1.NodeType);

            var dochtml1body1svg0g1Text0 = dochtml1body1svg0g1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1svg0g1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1svg0g1Text0.TextContent);

            var dochtml1body1table1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table1.Attributes.Length);
            Assert.AreEqual("table", dochtml1body1table1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table1.NodeType);

        }

        [Test]
        public void SvgElementWithGroupInTbody()
        {
            var doc = (@"<!DOCTYPE html><body><table><tbody><svg><g>foo</g><g>bar</g></svg></tbody></table>").ToHtmlDocument();

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

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml1body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);

            var dochtml1body1svg0g0 = dochtml1body1svg0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1svg0g0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0g0.Attributes.Length);
            Assert.AreEqual("g", dochtml1body1svg0g0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0g0.NodeType);

            var dochtml1body1svg0g0Text0 = dochtml1body1svg0g0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1svg0g0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1svg0g0Text0.TextContent);

            var dochtml1body1svg0g1 = dochtml1body1svg0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1svg0g1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0g1.Attributes.Length);
            Assert.AreEqual("g", dochtml1body1svg0g1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0g1.NodeType);

            var dochtml1body1svg0g1Text0 = dochtml1body1svg0g1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1svg0g1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1svg0g1Text0.TextContent);

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
        public void SvgElementWithGroupAndTextInTbody()
        {
            var doc = (@"<!DOCTYPE html><body><table><tbody><tr><svg><g>foo</g><g>bar</g></svg></tr></tbody></table>").ToHtmlDocument();

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

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml1body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);

            var dochtml1body1svg0g0 = dochtml1body1svg0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1svg0g0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0g0.Attributes.Length);
            Assert.AreEqual("g", dochtml1body1svg0g0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0g0.NodeType);

            var dochtml1body1svg0g0Text0 = dochtml1body1svg0g0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1svg0g0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1svg0g0Text0.TextContent);

            var dochtml1body1svg0g1 = dochtml1body1svg0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1svg0g1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0g1.Attributes.Length);
            Assert.AreEqual("g", dochtml1body1svg0g1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0g1.NodeType);

            var dochtml1body1svg0g1Text0 = dochtml1body1svg0g1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1svg0g1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1svg0g1Text0.TextContent);

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
        public void SvgElementWithGroupInTableCell()
        {
            var doc = (@"<!DOCTYPE html><body><table><tbody><tr><td><svg><g>foo</g><g>bar</g></svg></td></tr></tbody></table>").ToHtmlDocument();

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

            var dochtml1body1table0tbody0tr0td0svg0 = dochtml1body1table0tbody0tr0td0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1table0tbody0tr0td0svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml1body1table0tbody0tr0td0svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0svg0.NodeType);

            var dochtml1body1table0tbody0tr0td0svg0g0 = dochtml1body1table0tbody0tr0td0svg0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0svg0g0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0svg0g0.Attributes.Length);
            Assert.AreEqual("g", dochtml1body1table0tbody0tr0td0svg0g0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0svg0g0.NodeType);

            var dochtml1body1table0tbody0tr0td0svg0g0Text0 = dochtml1body1table0tbody0tr0td0svg0g0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0tbody0tr0td0svg0g0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1table0tbody0tr0td0svg0g0Text0.TextContent);

            var dochtml1body1table0tbody0tr0td0svg0g1 = dochtml1body1table0tbody0tr0td0svg0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0svg0g1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0svg0g1.Attributes.Length);
            Assert.AreEqual("g", dochtml1body1table0tbody0tr0td0svg0g1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0svg0g1.NodeType);

            var dochtml1body1table0tbody0tr0td0svg0g1Text0 = dochtml1body1table0tbody0tr0td0svg0g1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0tbody0tr0td0svg0g1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1table0tbody0tr0td0svg0g1Text0.TextContent);
        }

        [Test]
        public void SvgElementWithGroupAndTextInTableCell()
        {
            var doc = (@"<!DOCTYPE html><body><table><tbody><tr><td><svg><g>foo</g><g>bar</g></svg><p>baz</td></tr></tbody></table>").ToHtmlDocument();

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

            var dochtml1body1table0tbody0tr0td0svg0 = dochtml1body1table0tbody0tr0td0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1table0tbody0tr0td0svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml1body1table0tbody0tr0td0svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0svg0.NodeType);

            var dochtml1body1table0tbody0tr0td0svg0g0 = dochtml1body1table0tbody0tr0td0svg0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0svg0g0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0svg0g0.Attributes.Length);
            Assert.AreEqual("g", dochtml1body1table0tbody0tr0td0svg0g0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0svg0g0.NodeType);

            var dochtml1body1table0tbody0tr0td0svg0g0Text0 = dochtml1body1table0tbody0tr0td0svg0g0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0tbody0tr0td0svg0g0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1table0tbody0tr0td0svg0g0Text0.TextContent);

            var dochtml1body1table0tbody0tr0td0svg0g1 = dochtml1body1table0tbody0tr0td0svg0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1table0tbody0tr0td0svg0g1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0tbody0tr0td0svg0g1.Attributes.Length);
            Assert.AreEqual("g", dochtml1body1table0tbody0tr0td0svg0g1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0tbody0tr0td0svg0g1.NodeType);

            var dochtml1body1table0tbody0tr0td0svg0g1Text0 = dochtml1body1table0tbody0tr0td0svg0g1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0tbody0tr0td0svg0g1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1table0tbody0tr0td0svg0g1Text0.TextContent);

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
        public void SvgElementWithGroupAndTextInTableCaption()
        {
            var doc = (@"<!DOCTYPE html><body><table><caption><svg><g>foo</g><g>bar</g></svg><p>baz</caption></table>").ToHtmlDocument();

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

            var dochtml1body1table0caption0svg0 = dochtml1body1table0caption0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1table0caption0svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml1body1table0caption0svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0svg0.NodeType);

            var dochtml1body1table0caption0svg0g0 = dochtml1body1table0caption0svg0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0caption0svg0g0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0svg0g0.Attributes.Length);
            Assert.AreEqual("g", dochtml1body1table0caption0svg0g0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0svg0g0.NodeType);

            var dochtml1body1table0caption0svg0g0Text0 = dochtml1body1table0caption0svg0g0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0caption0svg0g0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1table0caption0svg0g0Text0.TextContent);

            var dochtml1body1table0caption0svg0g1 = dochtml1body1table0caption0svg0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1table0caption0svg0g1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0svg0g1.Attributes.Length);
            Assert.AreEqual("g", dochtml1body1table0caption0svg0g1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0svg0g1.NodeType);

            var dochtml1body1table0caption0svg0g1Text0 = dochtml1body1table0caption0svg0g1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0caption0svg0g1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1table0caption0svg0g1Text0.TextContent);

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
        public void SvgElementWithGroupInTableCaption()
        {
            var doc = (@"<!DOCTYPE html><body><table><caption><svg><g>foo</g><g>bar</g><p>baz</table><p>quux").ToHtmlDocument();

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

            var dochtml1body1table0caption0svg0 = dochtml1body1table0caption0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1table0caption0svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml1body1table0caption0svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0svg0.NodeType);

            var dochtml1body1table0caption0svg0g0 = dochtml1body1table0caption0svg0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0caption0svg0g0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0svg0g0.Attributes.Length);
            Assert.AreEqual("g", dochtml1body1table0caption0svg0g0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0svg0g0.NodeType);

            var dochtml1body1table0caption0svg0g0Text0 = dochtml1body1table0caption0svg0g0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0caption0svg0g0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1table0caption0svg0g0Text0.TextContent);

            var dochtml1body1table0caption0svg0g1 = dochtml1body1table0caption0svg0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1table0caption0svg0g1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0svg0g1.Attributes.Length);
            Assert.AreEqual("g", dochtml1body1table0caption0svg0g1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0svg0g1.NodeType);

            var dochtml1body1table0caption0svg0g1Text0 = dochtml1body1table0caption0svg0g1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0caption0svg0g1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1table0caption0svg0g1Text0.TextContent);

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
        public void SvgElementInCaptionWithMisclosedEnding()
        {
            var doc = (@"<!DOCTYPE html><body><table><caption><svg><g>foo</g><g>bar</g>baz</table><p>quux").ToHtmlDocument();

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

            var dochtml1body1table0caption0svg0 = dochtml1body1table0caption0.ChildNodes[0] as Element;
            Assert.AreEqual(3, dochtml1body1table0caption0svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml1body1table0caption0svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0svg0.NodeType);

            var dochtml1body1table0caption0svg0g0 = dochtml1body1table0caption0svg0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1table0caption0svg0g0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0svg0g0.Attributes.Length);
            Assert.AreEqual("g", dochtml1body1table0caption0svg0g0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1table0caption0svg0g0.NodeType);

            var dochtml1body1table0caption0svg0g0Text0 = dochtml1body1table0caption0svg0g0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1table0caption0svg0g0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1table0caption0svg0g0Text0.TextContent);

            var dochtml1body1table0caption0svg0g1 = dochtml1body1table0caption0svg0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1table0caption0svg0g1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1table0caption0svg0g1.Attributes.Length);
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
            Assert.AreEqual(0, dochtml1body1p1.Attributes.Length);
            Assert.AreEqual("p", dochtml1body1p1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p1.NodeType);

            var dochtml1body1p1Text0 = dochtml1body1p1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1p1Text0.NodeType);
            Assert.AreEqual("quux", dochtml1body1p1Text0.TextContent);
        }

        [Test]
        public void SvgElementInColgroupWithMisclosedEnding()
        {
            var doc = (@"<!DOCTYPE html><body><table><colgroup><svg><g>foo</g><g>bar</g><p>baz</table><p>quux").ToHtmlDocument();

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

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml1body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);

            var dochtml1body1svg0g0 = dochtml1body1svg0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1svg0g0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0g0.Attributes.Length);
            Assert.AreEqual("g", dochtml1body1svg0g0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0g0.NodeType);

            var dochtml1body1svg0g0Text0 = dochtml1body1svg0g0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1svg0g0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1svg0g0Text0.TextContent);

            var dochtml1body1svg0g1 = dochtml1body1svg0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1svg0g1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0g1.Attributes.Length);
            Assert.AreEqual("g", dochtml1body1svg0g1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0g1.NodeType);

            var dochtml1body1svg0g1Text0 = dochtml1body1svg0g1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1svg0g1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1svg0g1Text0.TextContent);

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
        public void SvgElementWithGroupInSelectMisclosed()
        {
            var doc = (@"<!DOCTYPE html><body><table><tr><td><select><svg><g>foo</g><g>bar</g><p>baz</table><p>quux").ToHtmlDocument();

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
        public void SvgElementWithGroupInSelectAndTableMisclosed()
        {
            var doc = (@"<!DOCTYPE html><body><table><select><svg><g>foo</g><g>bar</g><p>baz</table><p>quux").ToHtmlDocument();

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
        public void SvgElementOutsideDocumentRoot()
        {
            var doc = (@"<!DOCTYPE html><body></body></html><svg><g>foo</g><g>bar</g><p>baz").ToHtmlDocument();

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

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml1body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);

            var dochtml1body1svg0g0 = dochtml1body1svg0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1svg0g0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0g0.Attributes.Length);
            Assert.AreEqual("g", dochtml1body1svg0g0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0g0.NodeType);

            var dochtml1body1svg0g0Text0 = dochtml1body1svg0g0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1svg0g0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1svg0g0Text0.TextContent);

            var dochtml1body1svg0g1 = dochtml1body1svg0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1svg0g1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0g1.Attributes.Length);
            Assert.AreEqual("g", dochtml1body1svg0g1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0g1.NodeType);

            var dochtml1body1svg0g1Text0 = dochtml1body1svg0g1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1svg0g1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1svg0g1Text0.TextContent);

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
        public void SvgElementOutsideBody()
        {
            var doc = (@"<!DOCTYPE html><body></body><svg><g>foo</g><g>bar</g><p>baz").ToHtmlDocument();

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

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml1body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);

            var dochtml1body1svg0g0 = dochtml1body1svg0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1svg0g0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0g0.Attributes.Length);
            Assert.AreEqual("g", dochtml1body1svg0g0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0g0.NodeType);

            var dochtml1body1svg0g0Text0 = dochtml1body1svg0g0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1svg0g0Text0.NodeType);
            Assert.AreEqual("foo", dochtml1body1svg0g0Text0.TextContent);

            var dochtml1body1svg0g1 = dochtml1body1svg0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1svg0g1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0g1.Attributes.Length);
            Assert.AreEqual("g", dochtml1body1svg0g1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0g1.NodeType);

            var dochtml1body1svg0g1Text0 = dochtml1body1svg0g1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1svg0g1Text0.NodeType);
            Assert.AreEqual("bar", dochtml1body1svg0g1Text0.TextContent);

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
        public void SvgElementInFrameset()
        {
            var doc = (@"<!DOCTYPE html><frameset><svg><g></g><g></g><p><span>").ToHtmlDocument();

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
        public void SvgElementOutsideFrameset()
        {
            var doc = (@"<!DOCTYPE html><frameset></frameset><svg><g></g><g></g><p><span>").ToHtmlDocument();

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
        public void SvgElementInBodyWithXlinkAttribute()
        {
            var doc = (@"<!DOCTYPE html><body xlink:href=foo><svg xlink:href=foo></svg>").ToHtmlDocument();

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
            Assert.IsNotNull(dochtml1body1.Attributes.GetNamedItem("xlink:href"));
            Assert.AreEqual("xlink:href", dochtml1body1.Attributes.GetNamedItem("xlink:href").Name);
            Assert.AreEqual("foo", dochtml1body1.Attributes.GetNamedItem("xlink:href").Value);

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(1, dochtml1body1svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml1body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);

            var attr = dochtml1body1svg0.Attributes.GetNamedItem("href");
            Assert.IsNotNull(attr);
            Assert.AreEqual("foo", attr.Value);
            Assert.AreEqual(null, attr.Prefix);
            Assert.AreEqual("http://www.w3.org/1999/xlink", attr.NamespaceUri);
        }

        [Test]
        public void SvgElementWithGroupThatHasXlinkAttribute()
        {
            var doc = (@"<!DOCTYPE html><body xlink:href=foo xml:lang=en><svg><g xml:lang=en xlink:href=foo></g></svg>").ToHtmlDocument();

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
            Assert.IsNotNull(dochtml1body1.Attributes.GetNamedItem("xlink:href"));
            Assert.AreEqual("xlink:href", dochtml1body1.Attributes.GetNamedItem("xlink:href").Name);
            Assert.AreEqual("foo", dochtml1body1.Attributes.GetNamedItem("xlink:href").Value);
            Assert.IsNotNull(dochtml1body1.Attributes.GetNamedItem("xml:lang"));
            Assert.AreEqual("xml:lang", dochtml1body1.Attributes.GetNamedItem("xml:lang").Name);
            Assert.AreEqual("en", dochtml1body1.Attributes.GetNamedItem("xml:lang").Value);

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml1body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);

            var dochtml1body1svg0g0 = dochtml1body1svg0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1svg0g0.ChildNodes.Length);
            Assert.AreEqual(2, dochtml1body1svg0g0.Attributes.Length);
            Assert.AreEqual("g", dochtml1body1svg0g0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0g0.NodeType);

            var attr1 = dochtml1body1svg0g0.Attributes.GetNamedItem("href");
            Assert.IsNotNull(attr1);
            Assert.AreEqual("foo", attr1.Value);
            Assert.AreEqual(null, attr1.Prefix);
            Assert.AreEqual("http://www.w3.org/1999/xlink", attr1.NamespaceUri);

            var attr2 = dochtml1body1svg0g0.Attributes.GetNamedItem("xml:lang");
            Assert.IsNotNull(attr2);
            Assert.AreEqual("en", attr2.Value);
            Assert.AreEqual("xml", attr2.Prefix);
            Assert.AreEqual("http://www.w3.org/XML/1998/namespace", attr2.NamespaceUri);
        }

        [Test]
        public void SvgElementWithGroupThatHasNamespacedAttributes()
        {
            var doc = (@"<!DOCTYPE html><body xlink:href=foo xml:lang=en><svg><g xml:lang=en xlink:href=foo /></svg>").ToHtmlDocument();

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
            Assert.IsNotNull(dochtml1body1.Attributes.GetNamedItem("xlink:href"));
            Assert.AreEqual("xlink:href", dochtml1body1.Attributes.GetNamedItem("xlink:href").Name);
            Assert.AreEqual("foo", dochtml1body1.Attributes.GetNamedItem("xlink:href").Value);
            Assert.IsNotNull(dochtml1body1.Attributes.GetNamedItem("xml:lang"));
            Assert.AreEqual("xml:lang", dochtml1body1.Attributes.GetNamedItem("xml:lang").Name);
            Assert.AreEqual("en", dochtml1body1.Attributes.GetNamedItem("xml:lang").Value);

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml1body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);

            var dochtml1body1svg0g0 = dochtml1body1svg0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1svg0g0.ChildNodes.Length);
            Assert.AreEqual(2, dochtml1body1svg0g0.Attributes.Length);
            Assert.AreEqual("g", dochtml1body1svg0g0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0g0.NodeType);

            var attr1 = dochtml1body1svg0g0.Attributes.GetNamedItem("href");
            Assert.IsNotNull(attr1);
            Assert.AreEqual("foo", attr1.Value);
            Assert.AreEqual(null, attr1.Prefix);
            Assert.AreEqual("http://www.w3.org/1999/xlink", attr1.NamespaceUri);

            var attr2 = dochtml1body1svg0g0.Attributes.GetNamedItem("xml:lang");
            Assert.IsNotNull(attr2);
            Assert.AreEqual("en", attr2.Value);
            Assert.AreEqual("xml", attr2.Prefix);
            Assert.AreEqual("http://www.w3.org/XML/1998/namespace", attr2.NamespaceUri);
        }

        [Test]
        public void SvgElementWithSelfClosingGroup()
        {
            var doc = (@"<!DOCTYPE html><body xlink:href=foo xml:lang=en><svg><g xml:lang=en xlink:href=foo />bar</svg>").ToHtmlDocument();

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
            Assert.IsNotNull(dochtml1body1.Attributes.GetNamedItem("xlink:href"));
            Assert.AreEqual("xlink:href", dochtml1body1.Attributes.GetNamedItem("xlink:href").Name);
            Assert.AreEqual("foo", dochtml1body1.Attributes.GetNamedItem("xlink:href").Value);
            Assert.IsNotNull(dochtml1body1.Attributes.GetNamedItem("xml:lang"));
            Assert.AreEqual("xml:lang", dochtml1body1.Attributes.GetNamedItem("xml:lang").Name);
            Assert.AreEqual("en", dochtml1body1.Attributes.GetNamedItem("xml:lang").Value);

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml1body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);

            var dochtml1body1svg0g0 = dochtml1body1svg0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1svg0g0.ChildNodes.Length);
            Assert.AreEqual(2, dochtml1body1svg0g0.Attributes.Length);
            Assert.AreEqual("g", dochtml1body1svg0g0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0g0.NodeType);

            var attr1 = dochtml1body1svg0g0.Attributes.GetNamedItem("href");
            Assert.IsNotNull(attr1);
            Assert.AreEqual("foo", attr1.Value);
            Assert.AreEqual(null, attr1.Prefix);
            Assert.AreEqual("http://www.w3.org/1999/xlink", attr1.NamespaceUri);

            var attr2 = dochtml1body1svg0g0.Attributes.GetNamedItem("xml:lang");
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
            var doc = (@"<svg></path>").ToHtmlDocument();

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

            var dochtml0body1svg0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml0body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0.NodeType);
        }

        [Test]
        public void SvgElementInDivMisclosed()
        {
            var doc = (@"<div><svg></div>a").ToHtmlDocument();

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
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1div0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);

            var dochtml0body1div0svg0 = dochtml0body1div0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1div0svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml0body1div0svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0svg0.NodeType);


            var dochtml0body1Text1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text1.NodeType);
            Assert.AreEqual("a", dochtml0body1Text1.TextContent);
        }

        [Test]
        public void SvgElementWithPathInDivMisclosed()
        {
            var doc = (@"<div><svg><path></div>a").ToHtmlDocument();

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
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1div0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);

            var dochtml0body1div0svg0 = dochtml0body1div0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1div0svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml0body1div0svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0svg0.NodeType);

            var dochtml0body1div0svg0path0 = dochtml0body1div0svg0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1div0svg0path0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0svg0path0.Attributes.Length);
            Assert.AreEqual("path", dochtml0body1div0svg0path0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0svg0path0.NodeType);

            var dochtml0body1Text1 = dochtml0body1.ChildNodes[1];
            Assert.AreEqual(NodeType.Text, dochtml0body1Text1.NodeType);
            Assert.AreEqual("a", dochtml0body1Text1.TextContent);
        }

        [Test]
        public void SvgElementWithMisclosedPathInDiv()
        {
            var doc = (@"<div><svg><path></svg><path>").ToHtmlDocument();

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

            var dochtml0body1div0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);

            var dochtml0body1div0svg0 = dochtml0body1div0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1div0svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml0body1div0svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0svg0.NodeType);

            var dochtml0body1div0svg0path0 = dochtml0body1div0svg0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1div0svg0path0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0svg0path0.Attributes.Length);
            Assert.AreEqual("path", dochtml0body1div0svg0path0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0svg0path0.NodeType);

            var dochtml0body1div0path1 = dochtml0body1div0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1div0path1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0path1.Attributes.Length);
            Assert.AreEqual("path", dochtml0body1div0path1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0path1.NodeType);
        }

        [Test]
        public void SvgElementWithPathAndMathInDiv()
        {
            var doc = (@"<div><svg><path><foreignObject><math></div>a").ToHtmlDocument();

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

            var dochtml0body1div0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);

            var dochtml0body1div0svg0 = dochtml0body1div0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1div0svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml0body1div0svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0svg0.NodeType);

            var dochtml0body1div0svg0path0 = dochtml0body1div0svg0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1div0svg0path0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0svg0path0.Attributes.Length);
            Assert.AreEqual("path", dochtml0body1div0svg0path0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0svg0path0.NodeType);

            var dochtml0body1div0svg0path0foreignObject0 = dochtml0body1div0svg0path0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1div0svg0path0foreignObject0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0svg0path0foreignObject0.Attributes.Length);
            Assert.AreEqual("foreignObject", dochtml0body1div0svg0path0foreignObject0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0svg0path0foreignObject0.NodeType);

            var dochtml0body1div0svg0path0foreignObject0math0 = dochtml0body1div0svg0path0foreignObject0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1div0svg0path0foreignObject0math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0svg0path0foreignObject0math0.Attributes.Length);
            Assert.AreEqual("math", dochtml0body1div0svg0path0foreignObject0math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0svg0path0foreignObject0math0.NodeType);

            var dochtml0body1div0svg0path0foreignObject0math0Text0 = dochtml0body1div0svg0path0foreignObject0math0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0svg0path0foreignObject0math0Text0.NodeType);
            Assert.AreEqual("a", dochtml0body1div0svg0path0foreignObject0math0Text0.TextContent);
        }

        [Test]
        public void SvgElementWithPathAndForeignObjectInDiv()
        {
            var doc = (@"<div><svg><path><foreignObject><p></div>a").ToHtmlDocument();

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

            var dochtml0body1div0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);

            var dochtml0body1div0svg0 = dochtml0body1div0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1div0svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml0body1div0svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0svg0.NodeType);

            var dochtml0body1div0svg0path0 = dochtml0body1div0svg0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1div0svg0path0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0svg0path0.Attributes.Length);
            Assert.AreEqual("path", dochtml0body1div0svg0path0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0svg0path0.NodeType);

            var dochtml0body1div0svg0path0foreignObject0 = dochtml0body1div0svg0path0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1div0svg0path0foreignObject0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0svg0path0foreignObject0.Attributes.Length);
            Assert.AreEqual("foreignObject", dochtml0body1div0svg0path0foreignObject0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0svg0path0foreignObject0.NodeType);

            var dochtml0body1div0svg0path0foreignObject0p0 = dochtml0body1div0svg0path0foreignObject0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1div0svg0path0foreignObject0p0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0svg0path0foreignObject0p0.Attributes.Length);
            Assert.AreEqual("p", dochtml0body1div0svg0path0foreignObject0p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0svg0path0foreignObject0p0.NodeType);

            var dochtml0body1div0svg0path0foreignObject0p0Text0 = dochtml0body1div0svg0path0foreignObject0p0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0body1div0svg0path0foreignObject0p0Text0.NodeType);
            Assert.AreEqual("a", dochtml0body1div0svg0path0foreignObject0p0Text0.TextContent);
        }

        [Test]
        public void SvgElementWithDescDivAndAnotherSvg()
        {
            var doc = (@"<!DOCTYPE html><svg><desc><div><svg><ul>a").ToHtmlDocument();

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

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml1body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);

            var dochtml1body1svg0desc0 = dochtml1body1svg0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1svg0desc0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0desc0.Attributes.Length);
            Assert.AreEqual("desc", dochtml1body1svg0desc0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0desc0.NodeType);

            var dochtml1body1svg0desc0div0 = dochtml1body1svg0desc0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1svg0desc0div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0desc0div0.Attributes.Length);
            Assert.AreEqual("div", dochtml1body1svg0desc0div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0desc0div0.NodeType);

            var dochtml1body1svg0desc0div0svg0 = dochtml1body1svg0desc0div0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1svg0desc0div0svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0desc0div0svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml1body1svg0desc0div0svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0desc0div0svg0.NodeType);

            var dochtml1body1svg0desc0div0ul1 = dochtml1body1svg0desc0div0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1svg0desc0div0ul1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0desc0div0ul1.Attributes.Length);
            Assert.AreEqual("ul", dochtml1body1svg0desc0div0ul1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0desc0div0ul1.NodeType);

            var dochtml1body1svg0desc0div0ul1Text0 = dochtml1body1svg0desc0div0ul1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1svg0desc0div0ul1Text0.NodeType);
            Assert.AreEqual("a", dochtml1body1svg0desc0div0ul1Text0.TextContent);
        }

        [Test]
        public void SvgElementWithDescAndAnotherSvgElement()
        {
            var doc = (@"<!DOCTYPE html><svg><desc><svg><ul>a").ToHtmlDocument();

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

            var dochtml1body1svg0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml1body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0.NodeType);

            var dochtml1body1svg0desc0 = dochtml1body1svg0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml1body1svg0desc0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0desc0.Attributes.Length);
            Assert.AreEqual("desc", dochtml1body1svg0desc0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0desc0.NodeType);

            var dochtml1body1svg0desc0svg0 = dochtml1body1svg0desc0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1svg0desc0svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0desc0svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml1body1svg0desc0svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0desc0svg0.NodeType);

            var dochtml1body1svg0desc0ul1 = dochtml1body1svg0desc0.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1svg0desc0ul1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1svg0desc0ul1.Attributes.Length);
            Assert.AreEqual("ul", dochtml1body1svg0desc0ul1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1svg0desc0ul1.NodeType);

            var dochtml1body1svg0desc0ul1Text0 = dochtml1body1svg0desc0ul1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1svg0desc0ul1Text0.NodeType);
            Assert.AreEqual("a", dochtml1body1svg0desc0ul1Text0.TextContent);
        }

        [Test]
        public void SvgElementInParagraph()
        {
            var doc = (@"<!DOCTYPE html><p><svg><desc><p>").ToHtmlDocument();

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

            var dochtml1body1p0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1p0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1p0.Attributes.Length);
            Assert.AreEqual("p", dochtml1body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0.NodeType);

            var dochtml1body1p0svg0 = dochtml1body1p0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1p0svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1p0svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml1body1p0svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0svg0.NodeType);

            var dochtml1body1p0svg0desc0 = dochtml1body1p0svg0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1p0svg0desc0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1p0svg0desc0.Attributes.Length);
            Assert.AreEqual("desc", dochtml1body1p0svg0desc0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0svg0desc0.NodeType);

            var dochtml1body1p0svg0desc0p0 = dochtml1body1p0svg0desc0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1p0svg0desc0p0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1p0svg0desc0p0.Attributes.Length);
            Assert.AreEqual("p", dochtml1body1p0svg0desc0p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0svg0desc0p0.NodeType);
        }

        [Test]
        public void SvgElementWithTitleInSvgNamespace()
        {
            var doc = (@"<!DOCTYPE html><p><svg><title><p>").ToHtmlDocument();

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

            var dochtml1body1p0 = dochtml1body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1p0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1p0.Attributes.Length);
            Assert.AreEqual("p", dochtml1body1p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0.NodeType);

            var dochtml1body1p0svg0 = dochtml1body1p0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1p0svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1p0svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml1body1p0svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0svg0.NodeType);

            var dochtml1body1p0svg0title0 = dochtml1body1p0svg0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1body1p0svg0title0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1p0svg0title0.Attributes.Length);
            Assert.AreEqual("title", dochtml1body1p0svg0title0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0svg0title0.NodeType);

            var dochtml1body1p0svg0title0p0 = dochtml1body1p0svg0title0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1body1p0svg0title0p0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1p0svg0title0p0.Attributes.Length);
            Assert.AreEqual("p", dochtml1body1p0svg0title0p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1p0svg0title0p0.NodeType);
        }

        [Test]
        public void SvgElementInDivWithForeignObject()
        {
            var doc = (@"<div><svg><path><foreignObject><p></foreignObject><p>").ToHtmlDocument();

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

            var dochtml0body1div0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0.NodeType);

            var dochtml0body1div0svg0 = dochtml0body1div0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1div0svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml0body1div0svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0svg0.NodeType);

            var dochtml0body1div0svg0path0 = dochtml0body1div0svg0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1div0svg0path0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0svg0path0.Attributes.Length);
            Assert.AreEqual("path", dochtml0body1div0svg0path0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0svg0path0.NodeType);

            var dochtml0body1div0svg0path0foreignObject0 = dochtml0body1div0svg0path0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0body1div0svg0path0foreignObject0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0svg0path0foreignObject0.Attributes.Length);
            Assert.AreEqual("foreignObject", dochtml0body1div0svg0path0foreignObject0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0svg0path0foreignObject0.NodeType);

            var dochtml0body1div0svg0path0foreignObject0p0 = dochtml0body1div0svg0path0foreignObject0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1div0svg0path0foreignObject0p0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0svg0path0foreignObject0p0.Attributes.Length);
            Assert.AreEqual("p", dochtml0body1div0svg0path0foreignObject0p0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0svg0path0foreignObject0p0.NodeType);

            var dochtml0body1div0svg0path0foreignObject0p1 = dochtml0body1div0svg0path0foreignObject0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1div0svg0path0foreignObject0p1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1div0svg0path0foreignObject0p1.Attributes.Length);
            Assert.AreEqual("p", dochtml0body1div0svg0path0foreignObject0p1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1div0svg0path0foreignObject0p1.NodeType);
        }

        [Test]
        public void SvgWithScriptAndPathElement()
        {
            var doc = (@"<svg><script></script><path>").ToHtmlDocument();

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

            var dochtml0body1svg0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml0body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0.NodeType);

            var dochtml0body1svg0script0 = dochtml0body1svg0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1svg0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1svg0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml0body1svg0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0script0.NodeType);

            var dochtml0body1svg0path1 = dochtml0body1svg0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1svg0path1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1svg0path1.Attributes.Length);
            Assert.AreEqual("path", dochtml0body1svg0path1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0path1.NodeType);
        }

        [Test]
        public void SvgInsideTableWithRow()
        {
            var doc = (@"<table><svg></svg><tr>").ToHtmlDocument();

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
            Assert.AreEqual(2, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);

            var dochtml0body1svg0 = dochtml0body1.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml0body1svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1svg0.NodeType);

            var dochtml0body1table1 = dochtml0body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml0body1table1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table1.Attributes.Length);
            Assert.AreEqual("table", dochtml0body1table1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table1.NodeType);

            var dochtml0body1table1tbody0 = dochtml0body1table1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1table1tbody0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table1tbody0.Attributes.Length);
            Assert.AreEqual("tbody", dochtml0body1table1tbody0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table1tbody0.NodeType);

            var dochtml0body1table1tbody0tr0 = dochtml0body1table1tbody0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1table1tbody0tr0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1table1tbody0tr0.Attributes.Length);
            Assert.AreEqual("tr", dochtml0body1table1tbody0tr0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1table1tbody0tr0.NodeType);
        }

        [Test]
        public void SvgInsideMathMLWithAnnotationXml()
        {
            var doc = (@"<math><annotation-xml><svg></svg></annotation-xml><mi>").ToHtmlDocument();

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

            var dochtml0body1math0annotationxml0 = dochtml0body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0annotationxml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0annotationxml0.Attributes.Length);
            Assert.AreEqual("annotation-xml", dochtml0body1math0annotationxml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0annotationxml0.NodeType);

            var dochtml0body1math0annotationxml0svg0 = dochtml0body1math0annotationxml0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1math0annotationxml0svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0annotationxml0svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml0body1math0annotationxml0svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0annotationxml0svg0.NodeType);

            var dochtml0body1math0mi1 = dochtml0body1math0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1math0mi1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mi1.Attributes.Length);
            Assert.AreEqual("mi", dochtml0body1math0mi1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mi1.NodeType);
        }

        [Test]
        public void SvgInsideMathMLWithAnnotationXmlAndForeignObject()
        {
            var doc = (@"<math><annotation-xml><svg><foreignObject><div><math><mi></mi></math><span></span></div></foreignObject><path></path></svg></annotation-xml><mi>").ToHtmlDocument();

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

            var dochtml0body1math0annotationxml0 = dochtml0body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0annotationxml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0annotationxml0.Attributes.Length);
            Assert.AreEqual("annotation-xml", dochtml0body1math0annotationxml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0annotationxml0.NodeType);

            var dochtml0body1math0annotationxml0svg0 = dochtml0body1math0annotationxml0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0body1math0annotationxml0svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0annotationxml0svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml0body1math0annotationxml0svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0annotationxml0svg0.NodeType);

            var dochtml0body1math0annotationxml0svg0foreignObject0 = dochtml0body1math0annotationxml0svg0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0annotationxml0svg0foreignObject0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0annotationxml0svg0foreignObject0.Attributes.Length);
            Assert.AreEqual("foreignObject", dochtml0body1math0annotationxml0svg0foreignObject0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0annotationxml0svg0foreignObject0.NodeType);

            var dochtml0body1math0annotationxml0svg0foreignObject0div0 = dochtml0body1math0annotationxml0svg0foreignObject0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0body1math0annotationxml0svg0foreignObject0div0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0annotationxml0svg0foreignObject0div0.Attributes.Length);
            Assert.AreEqual("div", dochtml0body1math0annotationxml0svg0foreignObject0div0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0annotationxml0svg0foreignObject0div0.NodeType);

            var dochtml0body1math0annotationxml0svg0foreignObject0div0math0 = dochtml0body1math0annotationxml0svg0foreignObject0div0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0annotationxml0svg0foreignObject0div0math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0annotationxml0svg0foreignObject0div0math0.Attributes.Length);
            Assert.AreEqual("math", dochtml0body1math0annotationxml0svg0foreignObject0div0math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0annotationxml0svg0foreignObject0div0math0.NodeType);

            var dochtml0body1math0annotationxml0svg0foreignObject0div0math0mi0 = dochtml0body1math0annotationxml0svg0foreignObject0div0math0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1math0annotationxml0svg0foreignObject0div0math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0annotationxml0svg0foreignObject0div0math0mi0.Attributes.Length);
            Assert.AreEqual("mi", dochtml0body1math0annotationxml0svg0foreignObject0div0math0mi0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0annotationxml0svg0foreignObject0div0math0mi0.NodeType);

            var dochtml0body1math0annotationxml0svg0foreignObject0div0span1 = dochtml0body1math0annotationxml0svg0foreignObject0div0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1math0annotationxml0svg0foreignObject0div0span1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0annotationxml0svg0foreignObject0div0span1.Attributes.Length);
            Assert.AreEqual("span", dochtml0body1math0annotationxml0svg0foreignObject0div0span1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0annotationxml0svg0foreignObject0div0span1.NodeType);

            var dochtml0body1math0annotationxml0svg0path1 = dochtml0body1math0annotationxml0svg0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1math0annotationxml0svg0path1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0annotationxml0svg0path1.Attributes.Length);
            Assert.AreEqual("path", dochtml0body1math0annotationxml0svg0path1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0annotationxml0svg0path1.NodeType);

            var dochtml0body1math0mi1 = dochtml0body1math0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1math0mi1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mi1.Attributes.Length);
            Assert.AreEqual("mi", dochtml0body1math0mi1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mi1.NodeType);
        }

        [Test]
        public void SvgInsideMathMLWithAnnotationXmlAndOthers()
        {
            var doc = (@"<math><annotation-xml><svg><foreignObject><math><mi><svg></svg></mi><mo></mo></math><span></span></foreignObject><path></path></svg></annotation-xml><mi>").ToHtmlDocument();

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

            var dochtml0body1math0annotationxml0 = dochtml0body1math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0annotationxml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0annotationxml0.Attributes.Length);
            Assert.AreEqual("annotation-xml", dochtml0body1math0annotationxml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0annotationxml0.NodeType);

            var dochtml0body1math0annotationxml0svg0 = dochtml0body1math0annotationxml0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0body1math0annotationxml0svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0annotationxml0svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml0body1math0annotationxml0svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0annotationxml0svg0.NodeType);

            var dochtml0body1math0annotationxml0svg0foreignObject0 = dochtml0body1math0annotationxml0svg0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0body1math0annotationxml0svg0foreignObject0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0annotationxml0svg0foreignObject0.Attributes.Length);
            Assert.AreEqual("foreignObject", dochtml0body1math0annotationxml0svg0foreignObject0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0annotationxml0svg0foreignObject0.NodeType);

            var dochtml0body1math0annotationxml0svg0foreignObject0math0 = dochtml0body1math0annotationxml0svg0foreignObject0.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0body1math0annotationxml0svg0foreignObject0math0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0annotationxml0svg0foreignObject0math0.Attributes.Length);
            Assert.AreEqual("math", dochtml0body1math0annotationxml0svg0foreignObject0math0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0annotationxml0svg0foreignObject0math0.NodeType);

            var dochtml0body1math0annotationxml0svg0foreignObject0math0mi0 = dochtml0body1math0annotationxml0svg0foreignObject0math0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0body1math0annotationxml0svg0foreignObject0math0mi0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0annotationxml0svg0foreignObject0math0mi0.Attributes.Length);
            Assert.AreEqual("mi", dochtml0body1math0annotationxml0svg0foreignObject0math0mi0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0annotationxml0svg0foreignObject0math0mi0.NodeType);

            var dochtml0body1math0annotationxml0svg0foreignObject0math0mi0svg0 = dochtml0body1math0annotationxml0svg0foreignObject0math0mi0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0body1math0annotationxml0svg0foreignObject0math0mi0svg0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0annotationxml0svg0foreignObject0math0mi0svg0.Attributes.Length);
            Assert.AreEqual("svg", dochtml0body1math0annotationxml0svg0foreignObject0math0mi0svg0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0annotationxml0svg0foreignObject0math0mi0svg0.NodeType);

            var dochtml0body1math0annotationxml0svg0foreignObject0math0mo1 = dochtml0body1math0annotationxml0svg0foreignObject0math0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1math0annotationxml0svg0foreignObject0math0mo1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0annotationxml0svg0foreignObject0math0mo1.Attributes.Length);
            Assert.AreEqual("mo", dochtml0body1math0annotationxml0svg0foreignObject0math0mo1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0annotationxml0svg0foreignObject0math0mo1.NodeType);

            var dochtml0body1math0annotationxml0svg0foreignObject0span1 = dochtml0body1math0annotationxml0svg0foreignObject0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1math0annotationxml0svg0foreignObject0span1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0annotationxml0svg0foreignObject0span1.Attributes.Length);
            Assert.AreEqual("span", dochtml0body1math0annotationxml0svg0foreignObject0span1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0annotationxml0svg0foreignObject0span1.NodeType);

            var dochtml0body1math0annotationxml0svg0path1 = dochtml0body1math0annotationxml0svg0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1math0annotationxml0svg0path1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0annotationxml0svg0path1.Attributes.Length);
            Assert.AreEqual("path", dochtml0body1math0annotationxml0svg0path1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0annotationxml0svg0path1.NodeType);

            var dochtml0body1math0mi1 = dochtml0body1math0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1math0mi1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1math0mi1.Attributes.Length);
            Assert.AreEqual("mi", dochtml0body1math0mi1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1math0mi1.NodeType);
        }
    }
}
