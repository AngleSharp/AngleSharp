using AngleSharp.Dom.Css;
using AngleSharp.Parser.Css;
using NUnit.Framework;

namespace AngleSharp.Core.Tests.Css
{
    [TestFixture]
    public class CssBorderImagePropertyTests
    {
        [Test]
        public void CssBorderImageSourceNoneLegal()
        {
            var snippet = "border-image-source: none    ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-image-source", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderImageSourceProperty>(property);
            var concrete = (CssBorderImageSourceProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("none", concrete.Value.CssText);
        }

        [Test]
        public void CssBorderImageSourceUrlLegal()
        {
            var snippet = "border-image-source: url(image.jpg)";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-image-source", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderImageSourceProperty>(property);
            var concrete = (CssBorderImageSourceProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("url(\"image.jpg\")", concrete.Value.CssText);
        }

        [Test]
        public void CssBorderImageSourceLinearGradientLegal()
        {
            var snippet = "border-image-source: linear-gradient(to top, red, yellow)";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-image-source", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderImageSourceProperty>(property);
            var concrete = (CssBorderImageSourceProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("linear-gradient(to top, red, yellow)", concrete.Value.CssText);
        }

        [Test]
        public void CssBorderImageOutsetZeroLegal()
        {
            var snippet = "border-image-outset: 0";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-image-outset", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderImageOutsetProperty>(property);
            var concrete = (CssBorderImageOutsetProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0", concrete.Value.CssText);
        }

        [Test]
        public void CssBorderImageOutsetLengthPercentLegal()
        {
            var snippet = "border-image-outset: 10px   25%";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-image-outset", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderImageOutsetProperty>(property);
            var concrete = (CssBorderImageOutsetProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("10px 25%", concrete.Value.CssText);
        }

        [Test]
        public void CssBorderImageOutsetLengthPercentZeroLegal()
        {
            var snippet = "border-image-outset: 10px   25% 0";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-image-outset", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderImageOutsetProperty>(property);
            var concrete = (CssBorderImageOutsetProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("10px 25% 0", concrete.Value.CssText);
        }

        [Test]
        public void CssBorderImageOutsetLengthPercentZeroPercentLegal()
        {
            var snippet = "border-image-outset: 10px   25% 0 10%";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-image-outset", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderImageOutsetProperty>(property);
            var concrete = (CssBorderImageOutsetProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("10px 25% 0 10%", concrete.Value.CssText);
        }

        [Test]
        public void CssBorderImageOutsetZerosIllegal()
        {
            var snippet = "border-image-outset: 0 0 0 0 0";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-image-outset", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderImageOutsetProperty>(property);
            var concrete = (CssBorderImageOutsetProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssBorderImageWidthZeroLegal()
        {
            var snippet = "border-image-width: 0";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-image-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderImageWidthProperty>(property);
            var concrete = (CssBorderImageWidthProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0", concrete.Value.CssText);
        }

        [Test]
        public void CssBorderImageWidthAutoLegal()
        {
            var snippet = "border-image-width: auto";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-image-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderImageWidthProperty>(property);
            var concrete = (CssBorderImageWidthProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("auto", concrete.Value.CssText);
        }

        [Test]
        public void CssBorderImageWidthMultipleLegal()
        {
            var snippet = "border-image-width: 5";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-image-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderImageWidthProperty>(property);
            var concrete = (CssBorderImageWidthProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("5", concrete.Value.CssText);
        }

        [Test]
        public void CssBorderImageWidthLengthPercentLegal()
        {
            var snippet = "border-image-width: 10px   25%";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-image-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderImageWidthProperty>(property);
            var concrete = (CssBorderImageWidthProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("10px 25%", concrete.Value.CssText);
        }

        [Test]
        public void CssBorderImageWidthLengthPercentZeroLegal()
        {
            var snippet = "border-image-width: 10px   25% 0";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-image-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderImageWidthProperty>(property);
            var concrete = (CssBorderImageWidthProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("10px 25% 0", concrete.Value.CssText);
        }

        [Test]
        public void CssBorderImageWidthLengthPercentAutoPercentLegal()
        {
            var snippet = "border-image-width: 10px   25% auto 10%";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-image-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderImageWidthProperty>(property);
            var concrete = (CssBorderImageWidthProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("10px 25% auto 10%", concrete.Value.CssText);
        }

        [Test]
        public void CssBorderImageWidthZerosIllegal()
        {
            var snippet = "border-image-width: 0 0 0 0 0";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-image-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderImageWidthProperty>(property);
            var concrete = (CssBorderImageWidthProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssBorderImageRepeatStretchUppercaseLegal()
        {
            var snippet = "border-image-repeat:   StRETCH";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-image-repeat", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderImageRepeatProperty>(property);
            var concrete = (CssBorderImageRepeatProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("stretch", concrete.Value.CssText);
        }

        [Test]
        public void CssBorderImageRepeatRepeatLegal()
        {
            var snippet = "border-image-repeat:   repeat";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-image-repeat", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderImageRepeatProperty>(property);
            var concrete = (CssBorderImageRepeatProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("repeat", concrete.Value.CssText);
        }

        [Test]
        public void CssBorderImageRepeatRoundLegal()
        {
            var snippet = "border-image-repeat:   round";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-image-repeat", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderImageRepeatProperty>(property);
            var concrete = (CssBorderImageRepeatProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("round", concrete.Value.CssText);
        }

        [Test]
        public void CssBorderImageRepeatStretchRoundLegal()
        {
            var snippet = "border-image-repeat: stretch round";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-image-repeat", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderImageRepeatProperty>(property);
            var concrete = (CssBorderImageRepeatProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("stretch round", concrete.Value.CssText);
        }

        [Test]
        public void CssBorderImageRepeatNoRepeatIllegal()
        {
            var snippet = "border-image-repeat: no-repeat";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-image-repeat", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderImageRepeatProperty>(property);
            var concrete = (CssBorderImageRepeatProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssBorderImageSlicePixelsLegal()
        {
            var snippet = "border-image-slice: 3";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-image-slice", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderImageSliceProperty>(property);
            var concrete = (CssBorderImageSliceProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("3", concrete.Value.CssText);
        }

        [Test]
        public void CssBorderImageSlicePercentLegal()
        {
            var snippet = "border-image-slice: 10%";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-image-slice", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderImageSliceProperty>(property);
            var concrete = (CssBorderImageSliceProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("10%", concrete.Value.CssText);
        }

        [Test]
        public void CssBorderImageSliceFillLegal()
        {
            var snippet = "border-image-slice: fill";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-image-slice", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderImageSliceProperty>(property);
            var concrete = (CssBorderImageSliceProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            //Assert.AreEqual(true, concrete.IsFilled);
            //Assert.AreEqual(Length.Full, concrete.SliceLeft);
            //Assert.AreEqual(Length.Full, concrete.SliceRight);
            //Assert.AreEqual(Length.Full, concrete.SliceTop);
            //Assert.AreEqual(Length.Full, concrete.SliceBottom);
        }

        [Test]
        public void CssBorderImageSlicePercentFillLegal()
        {
            var snippet = "border-image-slice: 10% fill";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-image-slice", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderImageSliceProperty>(property);
            var concrete = (CssBorderImageSliceProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("10% fill", concrete.Value.CssText);
        }

        [Test]
        public void CssBorderImageSlicePercentPixelsFillLegal()
        {
            var snippet = "border-image-slice: 10% 30 fill";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-image-slice", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderImageSliceProperty>(property);
            var concrete = (CssBorderImageSliceProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("10% 30 fill", concrete.Value.CssText);
        }

        [Test]
        public void CssBorderImageSlicePercentPixelsFillZerosLegal()
        {
            var snippet = "border-image-slice: 10% 30 fill 0 0";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-image-slice", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderImageSliceProperty>(property);
            var concrete = (CssBorderImageSliceProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("10% 30 fill 0 0", concrete.Value.CssText);
        }

        [Test]
        public void CssBorderImageSlicePercentPixelsFillZerosIllegal()
        {
            var snippet = "border-image-slice: 10% 30 fill 0 0 0";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-image-slice", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderImageSliceProperty>(property);
            var concrete = (CssBorderImageSliceProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssBorderImageSlicePercentPixelsZerosFillIllegal()
        {
            var snippet = "border-image-slice: 10% 30  0 0 0 fill";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-image-slice", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderImageSliceProperty>(property);
            var concrete = (CssBorderImageSliceProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssBorderImageNoneLegal()
        {
            var snippet = "border-image: none    ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-image", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderImageProperty>(property);
            var concrete = (CssBorderImageProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("none", concrete.Value.CssText);
        }

        [Test]
        public void CssBorderImageUrlOffsetLegal()
        {
            var snippet = "border-image: url(image.png) 50 50";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-image", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderImageProperty>(property);
            var concrete = (CssBorderImageProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("url(\"image.png\") 50 50", concrete.Value.CssText);
        }

        [Test]
        public void CssBorderImageUrlOffsetRepeatLegal()
        {
            var snippet = "border-image: url(image.png) 30 30 repeat";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-image", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderImageProperty>(property);
            var concrete = (CssBorderImageProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("url(\"image.png\") 30 30 repeat", concrete.Value.CssText);
        }

        [Test]
        public void CssBorderImageUrlStretchUppercaseLegal()
        {
            var snippet = "border-image: url(image.png) STRETCH";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-image", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderImageProperty>(property);
            var concrete = (CssBorderImageProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("url(\"image.png\") stretch", concrete.Value.CssText);
        }

        [Test]
        public void CssBorderImageUrlOffsetWidthTwoLegal()
        {
            var snippet = "border-image: url(image.png) 30 30 / 15px 15px";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-image", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderImageProperty>(property);
            var concrete = (CssBorderImageProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("url(\"image.png\") 30 30 / 15px 15px", concrete.Value.CssText);
        }

        [Test]
        public void CssBorderImageUrlOffsetWidthFourLegal()
        {
            var snippet = "border-image: url(image.png) 30 30 0 10 / 15px 0 15px 2em";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-image", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderImageProperty>(property);
            var concrete = (CssBorderImageProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("url(\"image.png\") 30 30 0 10 / 15px 0 15px 2em", concrete.Value.CssText);
        }

        [Test]
        public void CssBorderImageUrlOffsetWidthOutsetLegal()
        {
            var snippet = "border-image: url(image.png) 30 30 / 15px 15px / 5% 2% 0 10%";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-image", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderImageProperty>(property);
            var concrete = (CssBorderImageProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("url(\"image.png\") 30 30 / 15px 15px / 5% 2% 0 10%", concrete.Value.CssText);
        }
    }
}
