namespace AngleSharp.Core.Tests.Css
{
    using AngleSharp.Dom.Css;
    using NUnit.Framework;

    [TestFixture]
    public class CssTransformPropertyTests : CssConstructionFunctions
    {
        [Test]
        public void CssPerspectiveNoneUppercaseLegal()
        {
            var snippet = "perspective:  NONE ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("perspective", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssPerspectiveProperty>(property);
            var concrete = (CssPerspectiveProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("none", concrete.Value);
        }

        [Test]
        public void CssPerspectiveLengthPixelLegal()
        {
            var snippet = "perspective:  20px  ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("perspective", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssPerspectiveProperty>(property);
            var concrete = (CssPerspectiveProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("20px", concrete.Value);
        }

        [Test]
        public void CssPerspectiveLengthEmLegal()
        {
            var snippet = "perspective:  3.5em  ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("perspective", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssPerspectiveProperty>(property);
            var concrete = (CssPerspectiveProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("3.5em", concrete.Value);
        }

        [Test]
        public void CssPerspectiveZeroLegal()
        {
            var snippet = "perspective:  0  ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("perspective", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssPerspectiveProperty>(property);
            var concrete = (CssPerspectiveProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0", concrete.Value);
        }

        [Test]
        public void CssPerspectivePercentIllegal()
        {
            var snippet = "perspective:  10%  ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("perspective", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssPerspectiveProperty>(property);
            var concrete = (CssPerspectiveProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssPerspectiveOriginZeroLegal()
        {
            var snippet = "perspective-origin:  0  ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("perspective-origin", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssPerspectiveOriginProperty>(property);
            var concrete = (CssPerspectiveOriginProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0", concrete.Value);
        }

        [Test]
        public void CssPerspectiveOriginLengthLegal()
        {
            var snippet = "perspective-origin:  20px  ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("perspective-origin", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssPerspectiveOriginProperty>(property);
            var concrete = (CssPerspectiveOriginProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("20px", concrete.Value);
        }

        [Test]
        public void CssPerspectiveOriginLeftLegal()
        {
            var snippet = "perspective-origin:  left  ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("perspective-origin", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssPerspectiveOriginProperty>(property);
            var concrete = (CssPerspectiveOriginProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("left", concrete.Value);
        }

        [Test]
        public void CssPerspectiveOriginPercentLegal()
        {
            var snippet = "perspective-origin:  15%  ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("perspective-origin", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssPerspectiveOriginProperty>(property);
            var concrete = (CssPerspectiveOriginProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("15%", concrete.Value);
        }

        [Test]
        public void CssPerspectiveOriginPercentPercentLegal()
        {
            var snippet = "perspective-origin:  15% 25% ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("perspective-origin", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssPerspectiveOriginProperty>(property);
            var concrete = (CssPerspectiveOriginProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("15% 25%", concrete.Value);
        }

        [Test]
        public void CssPerspectiveOriginLeftCenterLegal()
        {
            var snippet = "perspective-origin:  left center ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("perspective-origin", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssPerspectiveOriginProperty>(property);
            var concrete = (CssPerspectiveOriginProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("left center", concrete.Value);
        }

        [Test]
        public void CssPerspectiveOriginRightBottomLegal()
        {
            var snippet = "perspective-origin:  right BOTTOM ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("perspective-origin", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssPerspectiveOriginProperty>(property);
            var concrete = (CssPerspectiveOriginProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("right bottom", concrete.Value);
        }

        [Test]
        public void CssPerspectiveOriginTopCenterLegal()
        {
            var snippet = "perspective-origin:  top center ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("perspective-origin", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssPerspectiveOriginProperty>(property);
            var concrete = (CssPerspectiveOriginProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("center top", concrete.Value);
        }

        [Test]
        public void CssTransformStylePreserve3DLegal()
        {
            var snippet = "transform-style:  preserve-3d ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transform-style", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransformStyleProperty>(property);
            var concrete = (CssTransformStyleProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("preserve-3d", concrete.Value);
        }

        [Test]
        public void CssTransformStyleNoneIllegal()
        {
            var snippet = "transform-style:  none ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transform-style", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransformStyleProperty>(property);
            var concrete = (CssTransformStyleProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssTransformOriginXOffsetLegal()
        {
            var snippet = "transform-origin:  2px ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transform-origin", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransformOriginProperty>(property);
            var concrete = (CssTransformOriginProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("2px", concrete.Value);
        }

        [Test]
        public void CssTransformOriginXOffsetKeywordLegal()
        {
            var snippet = "transform-origin:  bottom ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transform-origin", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransformOriginProperty>(property);
            var concrete = (CssTransformOriginProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("bottom", concrete.Value);
        }

        [Test]
        public void CssTransformOriginYOffsetLegal()
        {
            var snippet = "transform-origin:  3cm 2px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transform-origin", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransformOriginProperty>(property);
            var concrete = (CssTransformOriginProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("3cm 2px", concrete.Value);
        }

        [Test]
        public void CssTransformOriginYOffsetXKeywordLegal()
        {
            var snippet = "transform-origin:  2px left";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transform-origin", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransformOriginProperty>(property);
            var concrete = (CssTransformOriginProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("2px left", concrete.Value);
        }

        [Test]
        public void CssTransformOriginXKeywordYOffsetLegal()
        {
            var snippet = "transform-origin:  left 2px ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transform-origin", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransformOriginProperty>(property);
            var concrete = (CssTransformOriginProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("left 2px", concrete.Value);
        }

        [Test]
        public void CssTransformOriginXKeywordYKeywordLegal()
        {
            var snippet = "transform-origin:  right top ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transform-origin", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransformOriginProperty>(property);
            var concrete = (CssTransformOriginProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("right top", concrete.Value);
        }

        [Test]
        public void CssTransformOriginYKeywordXKeywordLegal()
        {
            var snippet = "transform-origin:  top  right ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transform-origin", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransformOriginProperty>(property);
            var concrete = (CssTransformOriginProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("right top", concrete.Value);
        }

        [Test]
        public void CssTransformOriginXYZLegal()
        {
            var snippet = "transform-origin:  2px 30% 10px ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transform-origin", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransformOriginProperty>(property);
            var concrete = (CssTransformOriginProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("2px 30% 10px", concrete.Value);
        }

        [Test]
        public void CssTransformOriginYXKeywordZLegal()
        {
            var snippet = "transform-origin:  2px left 10px ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transform-origin", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransformOriginProperty>(property);
            var concrete = (CssTransformOriginProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("2px left 10px", concrete.Value);
        }

        [Test]
        public void CssTransformOriginXKeywordYZLegal()
        {
            var snippet = "transform-origin:  left 5px -3px ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transform-origin", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransformOriginProperty>(property);
            var concrete = (CssTransformOriginProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("left 5px -3px", concrete.Value);
        }

        [Test]
        public void CssTransformOriginXKeywordYKeywordZLegal()
        {
            var snippet = "transform-origin:  right bottom 2cm ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transform-origin", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransformOriginProperty>(property);
            var concrete = (CssTransformOriginProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("right bottom 2cm", concrete.Value);
        }

        [Test]
        public void CssTransformOriginYKeywordXKeywordZLegal()
        {
            var snippet = "transform-origin:  bottom  right  2cm ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transform-origin", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransformOriginProperty>(property);
            var concrete = (CssTransformOriginProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("right bottom 2cm", concrete.Value);
        }

        [Test]
        public void CssTransformNoneLegal()
        {
            var snippet = "transform:  none ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transform", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransformProperty>(property);
            var concrete = (CssTransformProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("none", concrete.Value);
        }

        [Test]
        public void CssTransformMatrixLegal()
        {
            var snippet = "transform:  matrix(1.0, 2.0, 3.0, 4.0, 5.0, 6.0) ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transform", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransformProperty>(property);
            var concrete = (CssTransformProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("matrix(1, 2, 3, 4, 5, 6)", concrete.Value);
        }

        [Test]
        public void CssTransformTranslateLegal()
        {
            var snippet = "transform:  translate(12px, 50%) ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transform", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransformProperty>(property);
            var concrete = (CssTransformProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("translate(12px, 50%)", concrete.Value);
        }

        [Test]
        public void CssTransformTranslateXLegal()
        {
            var snippet = "transform:  translateX(2em) ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transform", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransformProperty>(property);
            var concrete = (CssTransformProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("translateX(2em)", concrete.Value);
        }

        [Test]
        public void CssTransformTranslateYLegal()
        {
            var snippet = "transform:  translateY(3in) ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transform", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransformProperty>(property);
            var concrete = (CssTransformProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("translateY(3in)", concrete.Value);
        }

        [Test]
        public void CssTransformScaleLegal()
        {
            var snippet = "transform:  scale(2, 0.5) ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transform", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransformProperty>(property);
            var concrete = (CssTransformProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("scale(2, 0.5)", concrete.Value);
        }

        [Test]
        public void CssTransformScaleXLegal()
        {
            var snippet = "transform:  scaleX(0.1) ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transform", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransformProperty>(property);
            var concrete = (CssTransformProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("scaleX(0.1)", concrete.Value);
        }

        [Test]
        public void CssTransformScaleYLegal()
        {
            var snippet = "transform:  scaleY(1.5) ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transform", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransformProperty>(property);
            var concrete = (CssTransformProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("scaleY(1.5)", concrete.Value);
        }

        [Test]
        public void CssTransformRotateLegal()
        {
            var snippet = "transform:  rotate(0.5turn) ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transform", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransformProperty>(property);
            var concrete = (CssTransformProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("rotate(0.5turn)", concrete.Value);
        }

        [Test]
        public void CssTransformSkewXLegal()
        {
            var snippet = "transform:  skewX(  30deg  ) ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transform", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransformProperty>(property);
            var concrete = (CssTransformProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("skewX(30deg)", concrete.Value);
        }

        [Test]
        public void CssTransformSkewYLegal()
        {
            var snippet = "transform:  skewY(  1.07rad  ) ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transform", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransformProperty>(property);
            var concrete = (CssTransformProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("skewY(1.07rad)", concrete.Value);
        }

        [Test]
        public void CssTransformMultipleLegal()
        {
            var snippet = "transform:  translate(50%, 50%) rotate(45deg) scale(1.5)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transform", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransformProperty>(property);
            var concrete = (CssTransformProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("translate(50%, 50%) rotate(45deg) scale(1.5)", concrete.Value);
            //var elements = concrete.Transforms.ToArray();
            //Assert.AreEqual(3, elements.Length);
        }

        [Test]
        public void CssTransformMatrix3dLegal()
        {
            var snippet = "transform:  matrix3d(1.0, 2.0, 3.0, 4.0, 5.0, 6.0, 7.0, 8.0, 9.0, 10.0, 11.0, 12.0, 13.0, 14.0, 15.0, 16.0)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transform", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransformProperty>(property);
            var concrete = (CssTransformProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            //var elements = concrete.Transforms.ToArray();
            //Assert.AreEqual(1, elements.Length);
            //var element = elements[0];
            //Assert.IsInstanceOf<MatrixTransform>(element);
        }

        [Test]
        public void CssTransformTranslate3dLegal()
        {
            var snippet = "transform:  translate3d(12px, 50%, 3em)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transform", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransformProperty>(property);
            var concrete = (CssTransformProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            //var elements = concrete.Transforms.ToArray();
            //Assert.AreEqual(1, elements.Length);
            //var element = elements[0];
            //Assert.IsInstanceOf<TranslateTransform>(element);
        }

        [Test]
        public void CssTransformTranslateZLegal()
        {
            var snippet = "transform:  translateZ(2px)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transform", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransformProperty>(property);
            var concrete = (CssTransformProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            //var elements = concrete.Transforms.ToArray();
            //Assert.AreEqual(1, elements.Length);
            //var element = elements[0];
            //Assert.IsInstanceOf<TranslateTransform>(element);
        }

        [Test]
        public void CssTransformScale3dLegal()
        {
            var snippet = "transform:  scale3d(2.5, 1.2, 0.3)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transform", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransformProperty>(property);
            var concrete = (CssTransformProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            //var elements = concrete.Transforms.ToArray();
            //Assert.AreEqual(1, elements.Length);
            //var element = elements[0];
            //Assert.IsInstanceOf<ScaleTransform>(element);
        }

        [Test]
        public void CssTransformScaleZLegal()
        {
            var snippet = "transform:  scaleZ(0.3)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transform", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransformProperty>(property);
            var concrete = (CssTransformProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            //var elements = concrete.Transforms.ToArray();
            //Assert.AreEqual(1, elements.Length);
            //var element = elements[0];
            //Assert.IsInstanceOf<ScaleTransform>(element);
        }

        [Test]
        public void CssTransformRotate3dLegal()
        {
            var snippet = "transform:  rotate3d(1, 2.0, 3.0, 10deg)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transform", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransformProperty>(property);
            var concrete = (CssTransformProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            //var elements = concrete.Transforms.ToArray();
            //Assert.AreEqual(1, elements.Length);
            //var element = elements[0];
            //Assert.IsInstanceOf<RotateTransform>(element);
        }

        [Test]
        public void CssTransformRotateXLegal()
        {
            var snippet = "transform:  rotateX(10deg)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transform", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransformProperty>(property);
            var concrete = (CssTransformProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            //var elements = concrete.Transforms.ToArray();
            //Assert.AreEqual(1, elements.Length);
            //var element = elements[0];
            //Assert.IsInstanceOf<RotateTransform>(element);
        }

        [Test]
        public void CssTransformRotateYLegal()
        {
            var snippet = "transform:  rotateY(10deg)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transform", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransformProperty>(property);
            var concrete = (CssTransformProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            //var elements = concrete.Transforms.ToArray();
            //Assert.AreEqual(1, elements.Length);
            //var element = elements[0];
            //Assert.IsInstanceOf<RotateTransform>(element);
        }

        [Test]
        public void CssTransformRotateZLegal()
        {
            var snippet = "transform: rotateZ(10deg)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transform", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransformProperty>(property);
            var concrete = (CssTransformProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            //var elements = concrete.Transforms.ToArray();
            //Assert.AreEqual(1, elements.Length);
            //var element = elements[0];
            //Assert.IsInstanceOf<RotateTransform>(element);
        }

        [Test]
        public void CssTransformPerspectiveLegal()
        {
            var snippet = "transform: perspective(17px)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("transform", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTransformProperty>(property);
            var concrete = (CssTransformProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            //var elements = concrete.Transforms.ToArray();
            //Assert.AreEqual(1, elements.Length);
            //var element = elements[0];
            //Assert.IsInstanceOf<PerspectiveTransform>(element);
        }
    }
}
