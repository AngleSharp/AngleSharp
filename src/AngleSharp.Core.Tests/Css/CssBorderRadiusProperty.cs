namespace AngleSharp.Core.Tests.Css
{
    using AngleSharp.Dom.Css;
    using NUnit.Framework;

    [TestFixture]
    public class CssBorderRadiusPropertyTests : CssConstructionFunctions
    {
        [Test]
        public void CssBorderBottomLeftRadiusPxPxLegal()
        {
            var snippet = "border-bottom-left-radius: 40px  40px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-bottom-left-radius", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderBottomLeftRadiusProperty>(property);
            var concrete = (CssBorderBottomLeftRadiusProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("40px 40px", concrete.Value);
        }

        [Test]
        public void CssBorderBottomLeftRadiusPxEmLegal()
        {
            var snippet = "border-bottom-left-radius  : 40px 20em";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-bottom-left-radius", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderBottomLeftRadiusProperty>(property);
            var concrete = (CssBorderBottomLeftRadiusProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("40px 20em", concrete.Value);
        }

        [Test]
        public void CssBorderBottomLeftRadiusPxPercentLegal()
        {
            var snippet = "border-bottom-left-radius: 10px 5%";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-bottom-left-radius", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderBottomLeftRadiusProperty>(property);
            var concrete = (CssBorderBottomLeftRadiusProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("10px 5%", concrete.Value);
        }

        [Test]
        public void CssBorderBottomLeftRadiusPercentLegal()
        {
            var snippet = "border-bottom-left-radius: 10%";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-bottom-left-radius", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderBottomLeftRadiusProperty>(property);
            var concrete = (CssBorderBottomLeftRadiusProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("10%", concrete.Value);
        }

        [Test]
        public void CssBorderBottomRightRadiusZeroLegal()
        {
            var snippet = "border-bottom-right-radius: 0";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-bottom-right-radius", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderBottomRightRadiusProperty>(property);
            var concrete = (CssBorderBottomRightRadiusProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0", concrete.Value);
        }

        [Test]
        public void CssBorderBottomRightRadiusPxLegal()
        {
            var snippet = "border-bottom-right-radius: 20px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-bottom-right-radius", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderBottomRightRadiusProperty>(property);
            var concrete = (CssBorderBottomRightRadiusProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("20px", concrete.Value);
        }

        [Test]
        public void CssBorderTopLeftRadiusCmLegal()
        {
            var snippet = "border-top-left-radius: 3.5cm";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-top-left-radius", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderTopLeftRadiusProperty>(property);
            var concrete = (CssBorderTopLeftRadiusProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("3.5cm", concrete.Value);
        }

        [Test]
        public void CssBorderTopRightRadiusPercentPercentLegal()
        {
            var snippet = "border-top-right-radius: 15% 3.5%";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-top-right-radius", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderTopRightRadiusProperty>(property);
            var concrete = (CssBorderTopRightRadiusProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("15% 3.5%", concrete.Value);
        }

        [Test]
        public void CssBorderRadiusPercentPercentLegal()
        {
            var snippet = "border-radius: 15% 3.5%";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-radius", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderRadiusProperty>(property);
            var concrete = (CssBorderRadiusProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("15% 3.5%", concrete.Value);
        }

        [Test]
        public void CssBorderRadiusZeroLegal()
        {
            var snippet = "border-radius: 0";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-radius", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderRadiusProperty>(property);
            var concrete = (CssBorderRadiusProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            //Assert.AreEqual(Length.Zero, concrete.HorizontalBottomLeft);
            //Assert.AreEqual(Length.Zero, concrete.HorizontalTopLeft);
            //Assert.AreEqual(Length.Zero, concrete.HorizontalBottomRight);
            //Assert.AreEqual(Length.Zero, concrete.HorizontalTopRight);
            //Assert.AreEqual(Length.Zero, concrete.VerticalBottomLeft);
            //Assert.AreEqual(Length.Zero, concrete.VerticalBottomRight);
            //Assert.AreEqual(Length.Zero, concrete.VerticalTopLeft);
            //Assert.AreEqual(Length.Zero, concrete.VerticalTopRight);
            Assert.AreEqual("0", concrete.Value);
        }

        [Test]
        public void CssBorderRadiusThreeLengthsLegal()
        {
            var snippet = "border-radius: 2px 4px 3px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-radius", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderRadiusProperty>(property);
            var concrete = (CssBorderRadiusProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            //Assert.AreEqual(new Length(2f, Length.Unit.Px), concrete.HorizontalTopLeft);
            //Assert.AreEqual(new Length(2f, Length.Unit.Px), concrete.VerticalTopLeft);
            //Assert.AreEqual(new Length(4f, Length.Unit.Px), concrete.HorizontalTopRight);
            //Assert.AreEqual(new Length(4f, Length.Unit.Px), concrete.VerticalTopRight);
            //Assert.AreEqual(new Length(3f, Length.Unit.Px), concrete.HorizontalBottomRight);
            //Assert.AreEqual(new Length(3f, Length.Unit.Px), concrete.VerticalBottomRight);
            //Assert.AreEqual(new Length(4f, Length.Unit.Px), concrete.HorizontalBottomLeft);
            //Assert.AreEqual(new Length(4f, Length.Unit.Px), concrete.VerticalBottomLeft);
            Assert.AreEqual("2px 4px 3px", concrete.Value);
        }

        [Test]
        public void CssBorderRadiusFourLengthsLegal()
        {
            var snippet = "border-radius: 2px 4px 3px 0";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-radius", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderRadiusProperty>(property);
            var concrete = (CssBorderRadiusProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            //Assert.AreEqual(new Length(2f, Length.Unit.Px), concrete.HorizontalTopLeft);
            //Assert.AreEqual(new Length(2f, Length.Unit.Px), concrete.VerticalTopLeft);
            //Assert.AreEqual(new Length(4f, Length.Unit.Px), concrete.HorizontalTopRight);
            //Assert.AreEqual(new Length(4f, Length.Unit.Px), concrete.VerticalTopRight);
            //Assert.AreEqual(new Length(3f, Length.Unit.Px), concrete.HorizontalBottomRight);
            //Assert.AreEqual(new Length(3f, Length.Unit.Px), concrete.VerticalBottomRight);
            //Assert.AreEqual(new Length(0f, Length.Unit.Px), concrete.HorizontalBottomLeft);
            //Assert.AreEqual(new Length(0f, Length.Unit.Px), concrete.VerticalBottomLeft);
            Assert.AreEqual("2px 4px 3px 0", concrete.Value);
        }

        [Test]
        public void CssBorderRadiusFiveLengthsIllegal()
        {
            var snippet = "border-radius: 2px 4px 3px 0 1px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-radius", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderRadiusProperty>(property);
            var concrete = (CssBorderRadiusProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssBorderRadiusLengthFractionLegal()
        {
            var snippet = "border-radius: 1em/5em";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-radius", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderRadiusProperty>(property);
            var concrete = (CssBorderRadiusProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            //Assert.AreEqual(new Length(1f, Length.Unit.Em), concrete.HorizontalTopLeft);
            //Assert.AreEqual(new Length(5f, Length.Unit.Em), concrete.VerticalTopLeft);
            //Assert.AreEqual(new Length(1f, Length.Unit.Em), concrete.HorizontalTopRight);
            //Assert.AreEqual(new Length(5f, Length.Unit.Em), concrete.VerticalTopRight);
            //Assert.AreEqual(new Length(1f, Length.Unit.Em), concrete.HorizontalBottomRight);
            //Assert.AreEqual(new Length(5f, Length.Unit.Em), concrete.VerticalBottomRight);
            //Assert.AreEqual(new Length(1f, Length.Unit.Em), concrete.HorizontalBottomLeft);
            //Assert.AreEqual(new Length(5f, Length.Unit.Em), concrete.VerticalBottomLeft);
            Assert.AreEqual("1em / 5em", concrete.Value);
        }

        [Test]
        public void CssBorderRadiusLengthFractionInbalancedLegal()
        {
            var snippet = "border-radius: 4px 3px 6px / 2px 4px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-radius", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderRadiusProperty>(property);
            var concrete = (CssBorderRadiusProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            //Assert.AreEqual(new Length(4f, Length.Unit.Px), concrete.HorizontalTopLeft);
            //Assert.AreEqual(new Length(2f, Length.Unit.Px), concrete.VerticalTopLeft);
            //Assert.AreEqual(new Length(3f, Length.Unit.Px), concrete.HorizontalTopRight);
            //Assert.AreEqual(new Length(4f, Length.Unit.Px), concrete.VerticalTopRight);
            //Assert.AreEqual(new Length(6f, Length.Unit.Px), concrete.HorizontalBottomRight);
            //Assert.AreEqual(new Length(2f, Length.Unit.Px), concrete.VerticalBottomRight);
            //Assert.AreEqual(new Length(3f, Length.Unit.Px), concrete.HorizontalBottomLeft);
            //Assert.AreEqual(new Length(4f, Length.Unit.Px), concrete.VerticalBottomLeft);
            Assert.AreEqual("4px 3px 6px / 2px 4px", concrete.Value);
        }

        [Test]
        public void CssBorderRadiusFullFractionLegal()
        {
            var snippet = "border-radius: 4px 3px 6px 1em / 2px 4px 0 20%";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-radius", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderRadiusProperty>(property);
            var concrete = (CssBorderRadiusProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("4px 3px 6px 1em / 2px 4px 0 20%", concrete.Value);
        }

        [Test]
        public void CssBorderRadiusFiveTailFractionIllegal()
        {
            var snippet = "border-radius: 4px 3px 6px 1em / 2px 4px 0 20% 0";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-radius", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderRadiusProperty>(property);
            var concrete = (CssBorderRadiusProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssBorderRadiusFiveHeadFractionIllegal()
        {
            var snippet = "border-radius: 4px 3px 6px 1em 0 / 2px 4px 0 20%";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-radius", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderRadiusProperty>(property);
            var concrete = (CssBorderRadiusProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssBorderRadiusCircleShouldBeExpandedAndRecombinedCorrectly()
        {
            var snippet = ".centered { border-radius: 5px; }";
            var expected = ".centered { border-radius: 5px }";
            var result = ParseRule(snippet);
            var actual = result.CssText;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CssBorderRadiusEllipseShouldBeExpandedAndRecombinedCorrectly()
        {
            var snippet = ".centered { border-radius: 5px/3px; }";
            var expected = ".centered { border-radius: 5px / 3px }";
            var result = ParseRule(snippet);
            var actual = result.CssText;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CssBorderRadiusSimplificationShouldWork()
        {
            var snippet = ".centered { border-top-left-radius: 0 1px; border-bottom-left-radius: 1px 2px; border-top-right-radius: 0 3px; border-bottom-right-radius: 1px 4px; }";
            var expected = ".centered { border-radius: 0 0 1px 1px / 1px 3px 4px 2px }";
            var result = ParseRule(snippet);
            var actual = result.CssText;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CssBorderRadiusRecombinationAndReductionCheck()
        {
            var snippet = ".centered { border-top-left-radius: 0 1px; border-bottom-left-radius: 0 1px; border-top-right-radius: 1px 1px; border-bottom-right-radius: 0 1px; }";
            var expected = ".centered { border-radius: 0 1px 0 0 / 1px }";
            var result = ParseRule(snippet);
            var actual = result.CssText;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CssBorderRadiusPureCircleRecombination()
        {
            var snippet = ".test { border-top-left-radius:15px;border-bottom-left-radius:15px;border-bottom-right-radius:0;border-top-right-radius:0;}";
            var expected = ".test { border-radius: 15px 0 0 15px }";
            var result = ParseRule(snippet);
            var actual = result.CssText;
            Assert.AreEqual(expected, actual);
        }
    }
}
