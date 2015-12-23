namespace AngleSharp.Core.Tests.Css
{
    using AngleSharp.Dom.Css;
    using NUnit.Framework;

    [TestFixture]
    public class CssBackgroundPropertyTests : CssConstructionFunctions
    {
        [Test]
        public void CssBackgroundAttachmentScrollLegal()
        {
            var snippet = "background-attachment : scroll";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("background-attachment", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBackgroundAttachmentProperty>(property);
            var concrete = (CssBackgroundAttachmentProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("scroll", concrete.Value);
        }

        [Test]
        public void CssBackgroundAttachmentInitialLegal()
        {
            var snippet = "background-attachment : initial";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("background-attachment", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBackgroundAttachmentProperty>(property);
            var concrete = (CssBackgroundAttachmentProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("initial", concrete.Value);
        }

        [Test]
        public void CssBackgroundAttachmentFixedUppercaseLegal()
        {
            var snippet = "background-attachment : Fixed ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("background-attachment", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBackgroundAttachmentProperty>(property);
            var concrete = (CssBackgroundAttachmentProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("fixed", concrete.Value);
        }

        [Test]
        public void CssBackgroundAttachmentFixedLocalLegal()
        {
            var snippet = "background-attachment : fixed  ,  local ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("background-attachment", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBackgroundAttachmentProperty>(property);
            var concrete = (CssBackgroundAttachmentProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("fixed, local", concrete.Value);
        }

        [Test]
        public void CssBackgroundAttachmentFixedLocalScrollScrollLegal()
        {
            var snippet = "background-attachment : fixed  ,  local,scroll,scroll ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("background-attachment", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBackgroundAttachmentProperty>(property);
            var concrete = (CssBackgroundAttachmentProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("fixed, local, scroll, scroll", concrete.Value);
        }

        [Test]
        public void CssBackgroundAttachmentNoneIllegal()
        {
            var snippet = "background-attachment : none ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("background-attachment", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBackgroundAttachmentProperty>(property);
            var concrete = (CssBackgroundAttachmentProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssBackgroundClipPaddingBoxUppercaseLegal()
        {
            var snippet = "background-clip : Padding-Box ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("background-clip", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBackgroundClipProperty>(property);
            var concrete = (CssBackgroundClipProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("padding-box", concrete.Value);
        }

        [Test]
        public void CssBackgroundClipPaddingBoxBorderBoxLegal()
        {
            var snippet = "background-clip : Padding-Box, border-box ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("background-clip", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBackgroundClipProperty>(property);
            var concrete = (CssBackgroundClipProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("padding-box, border-box", concrete.Value);
        }

        [Test]
        public void CssBackgroundClipContentBoxLegal()
        {
            var snippet = "background-clip : content-box";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("background-clip", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBackgroundClipProperty>(property);
            var concrete = (CssBackgroundClipProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("content-box", concrete.Value);
        }

        [Test]
        public void CssBackgroundColorTealLegal()
        {
            var snippet = "background-color : teal";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("background-color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBackgroundColorProperty>(property);
            var concrete = (CssBackgroundColorProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("rgb(0, 128, 128)", concrete.Value);
        }

        [Test]
        public void CssBackgroundColorRgbLegal()
        {
            var snippet = "background-color : rgb(255  ,  255  ,  128)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("background-color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBackgroundColorProperty>(property);
            var concrete = (CssBackgroundColorProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("rgb(255, 255, 128)", concrete.Value);
        }

        [Test]
        public void CssBackgroundColorHslaLegal()
        {
            var snippet = "background-color : hsla(50, 33%, 25%, 0.75)";//equal to rgba(85, 78, 43, 0.75)
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("background-color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBackgroundColorProperty>(property);
            var concrete = (CssBackgroundColorProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("hsla(50deg, 33%, 25%, 0.75)", concrete.Value);
        }

        [Test]
        public void CssBackgroundColorTransparentLegal()
        {
            var snippet = "background-color : Transparent";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("background-color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBackgroundColorProperty>(property);
            var concrete = (CssBackgroundColorProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("rgba(0, 0, 0, 0)", concrete.Value);
        }

        [Test]
        public void CssBackgroundColorHexLegal()
        {
            var snippet = "background-color : #bbff00";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("background-color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBackgroundColorProperty>(property);
            var concrete = (CssBackgroundColorProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("rgb(187, 255, 0)", concrete.Value);
        }

        [Test]
        public void CssBackgroundColorMultipleIllegal()
        {
            var snippet = "background-color : #bbff00, transparent, red, #ff00ff";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("background-color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBackgroundColorProperty>(property);
            var concrete = (CssBackgroundColorProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssBackgroundImageNoneLegal()
        {
            var snippet = "background-image: NONE";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("background-image", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBackgroundImageProperty>(property);
            var concrete = (CssBackgroundImageProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("none", concrete.Value);
        }

        [Test]
        public void CssBackgroundImageUrlAndNoneLegal()
        {
            var snippet = "background-image: url(\"img/sprites.svg?v=1bc768be1b3c\"),none";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("background-image", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBackgroundImageProperty>(property);
            var concrete = (CssBackgroundImageProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("url(\"img/sprites.svg?v=1bc768be1b3c\"), none", concrete.Value);
        }

        [Test]
        public void CssBackgroundImageUrlLegal()
        {
            var snippet = "background-image: url(image.png)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("background-image", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBackgroundImageProperty>(property);
            var concrete = (CssBackgroundImageProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("url(\"image.png\")", concrete.Value);
        }

        [Test]
        public void CssBackgroundImageUrlAbsoluteLegal()
        {
            var snippet = "background-image: url(http://www.example.com/images/bck.png)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("background-image", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBackgroundImageProperty>(property);
            var concrete = (CssBackgroundImageProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("url(\"http://www.example.com/images/bck.png\")", concrete.Value);
        }

        [Test]
        public void CssBackgroundImageUrlsLegal()
        {
            var snippet = "background-image: url(image.png),url('bla.png')";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("background-image", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBackgroundImageProperty>(property);
            var concrete = (CssBackgroundImageProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("url(\"image.png\"), url(\"bla.png\")", concrete.Value);
        }

        [Test]
        public void CssBackgroundImageUrlNoneUrlLegal()
        {
            var snippet = "background-image: url(image.png),none, url(foo.gif)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("background-image", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBackgroundImageProperty>(property);
            var concrete = (CssBackgroundImageProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("url(\"image.png\"), none, url(\"foo.gif\")", concrete.Value);
        }

        [Test]
        public void CssBackgroundOriginContentBoxLegal()
        {
            var snippet = "background-origin: CONTENT-BOX";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("background-origin", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBackgroundOriginProperty>(property);
            var concrete = (CssBackgroundOriginProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("content-box", concrete.Value);
        }

        [Test]
        public void CssBackgroundOriginContentBoxPaddingBoxLegal()
        {
            var snippet = "background-origin: CONTENT-BOX, Padding-Box";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("background-origin", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBackgroundOriginProperty>(property);
            var concrete = (CssBackgroundOriginProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("content-box, padding-box", concrete.Value);
        }

        [Test]
        public void CssBackgroundOriginBorderBoxLegal()
        {
            var snippet = "background-origin: border-box";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("background-origin", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBackgroundOriginProperty>(property);
            var concrete = (CssBackgroundOriginProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("border-box", concrete.Value);
        }

        [Test]
        public void CssBackgroundPositionTopLegal()
        {
            var snippet = "background-position: top";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("background-position", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBackgroundPositionProperty>(property);
            var concrete = (CssBackgroundPositionProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("top", concrete.Value);
        }

        [Test]
        public void CssBackgroundPositionPercentPercentLegal()
        {
            var snippet = "background-position: 25% 75%";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("background-position", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBackgroundPositionProperty>(property);
            var concrete = (CssBackgroundPositionProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("25% 75%", concrete.Value);
        }

        [Test]
        public void CssBackgroundPositionCenterPercentLegal()
        {
            var snippet = "background-position: center 75%";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("background-position", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBackgroundPositionProperty>(property);
            var concrete = (CssBackgroundPositionProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("center 75%", concrete.Value);
        }

        [Test]
        public void CssBackgroundPositionRightLengthBottomLengthLegal()
        {
            var snippet = "background-position: right 20px bottom 20px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("background-position", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBackgroundPositionProperty>(property);
            var concrete = (CssBackgroundPositionProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("right 20px bottom 20px", concrete.Value);
        }

        [Test]
        public void CssBackgroundPositionLengthLengthCenterMultipleLegal()
        {
            var snippet = "background-position: 10px 20px, center";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("background-position", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBackgroundPositionProperty>(property);
            var concrete = (CssBackgroundPositionProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("10px 20px, center", concrete.Value);
        }

        [Test]
        public void CssBackgroundPositionZeroMultipleLegal()
        {
            var snippet = "background-position: 0 0, 0 0";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("background-position", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBackgroundPositionProperty>(property);
            var concrete = (CssBackgroundPositionProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0 0, 0 0", concrete.Value);
        }

        [Test]
        public void CssBackgroundRepeatRepeatXLegal()
        {
            var snippet = "background-repeat: repeat-x";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("background-repeat", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBackgroundRepeatProperty>(property);
            var concrete = (CssBackgroundRepeatProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("repeat-x", concrete.Value);
        }

        [Test]
        public void CssBackgroundRepeatRepeatYLegal()
        {
            var snippet = "background-repeat: repeat-y";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("background-repeat", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBackgroundRepeatProperty>(property);
            var concrete = (CssBackgroundRepeatProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("repeat-y", concrete.Value);
        }

        [Test]
        public void CssBackgroundRepeatRepeatLegal()
        {
            var snippet = "background-repeat: REPEAT";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("background-repeat", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBackgroundRepeatProperty>(property);
            var concrete = (CssBackgroundRepeatProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("repeat", concrete.Value);
        }

        [Test]
        public void CssBackgroundRepeatRoundLegal()
        {
            var snippet = "background-repeat: rounD";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("background-repeat", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBackgroundRepeatProperty>(property);
            var concrete = (CssBackgroundRepeatProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("round", concrete.Value);
        }

        [Test]
        public void CssBackgroundRepeatRepeatSpaceLegal()
        {
            var snippet = "background-repeat: repeat space";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("background-repeat", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBackgroundRepeatProperty>(property);
            var concrete = (CssBackgroundRepeatProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("repeat space", concrete.Value);
        }

        [Test]
        public void CssBackgroundRepeatRepeatXSpaceIllegal()
        {
            var snippet = "background-repeat: repeat-x space";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("background-repeat", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBackgroundRepeatProperty>(property);
            var concrete = (CssBackgroundRepeatProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssBackgroundRepeatRepeatXRepeatYMultipleLegal()
        {
            var snippet = "background-repeat: repeat-X, repeat-Y";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("background-repeat", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBackgroundRepeatProperty>(property);
            var concrete = (CssBackgroundRepeatProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("repeat-x, repeat-y", concrete.Value);
        }

        [Test]
        public void CssBackgroundRepeatSpaceRoundLegal()
        {
            var snippet = "background-repeat: space round";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("background-repeat", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBackgroundRepeatProperty>(property);
            var concrete = (CssBackgroundRepeatProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("space round", concrete.Value);
        }

        [Test]
        public void CssBackgroundRepeatNoRepeatRepeatXIllegal()
        {
            var snippet = "background-repeat: no-repeat repeat-x";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("background-repeat", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBackgroundRepeatProperty>(property);
            var concrete = (CssBackgroundRepeatProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssBackgroundRepeatRepeatRepeatNoRepeatRepeatLegal()
        {
            var snippet = "background-repeat: repeat repeat, no-repeat repeat";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("background-repeat", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBackgroundRepeatProperty>(property);
            var concrete = (CssBackgroundRepeatProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("repeat repeat, no-repeat repeat", concrete.Value);
        }

        [Test]
        public void CssBackgroundSizeLengthLegal()
        {
            var snippet = "background-size: 2em";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("background-size", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBackgroundSizeProperty>(property);
            var concrete = (CssBackgroundSizeProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("2em", concrete.Value);
        }

        [Test]
        public void CssBackgroundSizePercentLegal()
        {
            var snippet = "background-size: 20%";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("background-size", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBackgroundSizeProperty>(property);
            var concrete = (CssBackgroundSizeProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("20%", concrete.Value);
        }

        [Test]
        public void CssBackgroundSizeAutoAutoLegal()
        {
            var snippet = "background-size: auto auto";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("background-size", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBackgroundSizeProperty>(property);
            var concrete = (CssBackgroundSizeProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("auto auto", concrete.Value);
        }

        [Test]
        public void CssBackgroundSizeAutoLengthLegal()
        {
            var snippet = "background-size: auto 50px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("background-size", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBackgroundSizeProperty>(property);
            var concrete = (CssBackgroundSizeProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("auto 50px", concrete.Value);
        }

        [Test]
        public void CssBackgroundSizeLengthLengthLegal()
        {
            var snippet = "background-size: 25px 50px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("background-size", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBackgroundSizeProperty>(property);
            var concrete = (CssBackgroundSizeProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("25px 50px", concrete.Value);
        }

        [Test]
        public void CssBackgroundSizePercentPercentLegal()
        {
            var snippet = "background-size: 50% 50%";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("background-size", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBackgroundSizeProperty>(property);
            var concrete = (CssBackgroundSizeProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("50% 50%", concrete.Value);
        }

        [Test]
        public void CssBackgroundSizeAutoUppercaseLegal()
        {
            var snippet = "background-size: AUTO";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("background-size", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBackgroundSizeProperty>(property);
            var concrete = (CssBackgroundSizeProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("auto", concrete.Value);
        }

        [Test]
        public void CssBackgroundSizeCoverLegal()
        {
            var snippet = "background-size: cover";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("background-size", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBackgroundSizeProperty>(property);
            var concrete = (CssBackgroundSizeProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("cover", concrete.Value);
        }

        [Test]
        public void CssBackgroundSizeContainCoverMultipleLegal()
        {
            var snippet = "background-size: contain,cover";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("background-size", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBackgroundSizeProperty>(property);
            var concrete = (CssBackgroundSizeProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("contain, cover", concrete.Value);
        }

        [Test]
        public void CssBackgroundSizeContainLengthAutoPercentLegal()
        {
            var snippet = "background-size: contain,100px,auto,20%";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("background-size", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBackgroundSizeProperty>(property);
            var concrete = (CssBackgroundSizeProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("contain, 100px, auto, 20%", concrete.Value);
        }

        [Test]
        public void CssBackgroundRedLegal()
        {
            var snippet = "background: red";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("background", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBackgroundProperty>(property);
            var concrete = (CssBackgroundProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("rgb(255, 0, 0)", concrete.Value);
        }

        [Test]
        public void CssBackgroundWhiteImageLegal()
        {
            var snippet = "background: white url(\"pendant.png\");";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("background", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBackgroundProperty>(property);
            var concrete = (CssBackgroundProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("url(\"pendant.png\") rgb(255, 255, 255)", concrete.Value);
        }

        [Test]
        public void CssBackgroundImageLegal()
        {
            var snippet = "background: url(\"topbanner.png\") #00d repeat-y fixed";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("background", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBackgroundProperty>(property);
            var concrete = (CssBackgroundProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("url(\"topbanner.png\") repeat-y fixed rgb(0, 0, 221)", concrete.Value);
        }

        [Test]
        public void CssBackgroundWithoutColorLegal()
        {
            var snippet = "background: url(\"img_tree.png\") no-repeat right top";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("background", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBackgroundProperty>(property);
            var concrete = (CssBackgroundProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("url(\"img_tree.png\") right top no-repeat", concrete.Value);
        }

        [Test]
        public void CssBackgroundImageDataUrlLegal()
        {
            var url = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAEcAAAAcCAMAAAAEJ1IZAAAABGdBTUEAALGPC/xhBQAAVAI/VAI/VAI/VAI/VAI/VAI/VAAAA////AI/VRZ0U8AAAAFJ0Uk5TYNV4S2UbgT/Gk6uQt585w2wGXS0zJO2lhGttJK6j4YqZSobH1AAAAAElFTkSuQmCC";
            var snippet = "background-image: url('" + url + "')";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("background-image", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBackgroundImageProperty>(property);
            var concrete = (CssBackgroundImageProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("url(\"" + url + "\")", concrete.Value);
        }
    }
}
