using AngleSharp;
using AngleSharp.Css;
using AngleSharp.Dom.Css;
using AngleSharp.Parser.Css;
using NUnit.Framework;

namespace AngleSharp.Core.Tests.Css
{
    [TestFixture]
    public class CssBorderRadiusPropertyTest
    {
        [Test]
        public void CssBorderBottomLeftRadiusPxPxLegal()
        {
            var snippet = "border-bottom-left-radius: 40px  40px";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-bottom-left-radius", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderBottomLeftRadiusProperty>(property);
            var concrete = (CssBorderBottomLeftRadiusProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("40px 40px", concrete.Value.CssText);
        }

        [Test]
        public void CssBorderBottomLeftRadiusPxEmLegal()
        {
            var snippet = "border-bottom-left-radius  : 40px 20em";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-bottom-left-radius", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderBottomLeftRadiusProperty>(property);
            var concrete = (CssBorderBottomLeftRadiusProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("40px 20em", concrete.Value.CssText);
        }

        [Test]
        public void CssBorderBottomLeftRadiusPxPercentLegal()
        {
            var snippet = "border-bottom-left-radius: 10px 5%";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-bottom-left-radius", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderBottomLeftRadiusProperty>(property);
            var concrete = (CssBorderBottomLeftRadiusProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("10px 5%", concrete.Value.CssText);
        }

        [Test]
        public void CssBorderBottomLeftRadiusPercentLegal()
        {
            var snippet = "border-bottom-left-radius: 10%";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-bottom-left-radius", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderBottomLeftRadiusProperty>(property);
            var concrete = (CssBorderBottomLeftRadiusProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("10%", concrete.Value.CssText);
        }

        [Test]
        public void CssBorderBottomRightRadiusZeroLegal()
        {
            var snippet = "border-bottom-right-radius: 0";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-bottom-right-radius", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderBottomRightRadiusProperty>(property);
            var concrete = (CssBorderBottomRightRadiusProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0", concrete.Value.CssText);
        }

        [Test]
        public void CssBorderBottomRightRadiusPxLegal()
        {
            var snippet = "border-bottom-right-radius: 20px";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-bottom-right-radius", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderBottomRightRadiusProperty>(property);
            var concrete = (CssBorderBottomRightRadiusProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("20px", concrete.Value.CssText);
        }

        [Test]
        public void CssBorderTopLeftRadiusCmLegal()
        {
            var snippet = "border-top-left-radius: 3.5cm";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-top-left-radius", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderTopLeftRadiusProperty>(property);
            var concrete = (CssBorderTopLeftRadiusProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("3.5cm", concrete.Value.CssText);
        }

        [Test]
        public void CssBorderTopRightRadiusPercentPercentLegal()
        {
            var snippet = "border-top-right-radius: 15% 3.5%";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-top-right-radius", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderTopRightRadiusProperty>(property);
            var concrete = (CssBorderTopRightRadiusProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("15% 3.5%", concrete.Value.CssText);
        }

        [Test]
        public void CssBorderRadiusPercentPercentLegal()
        {
            var snippet = "border-radius: 15% 3.5%";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-radius", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderRadiusProperty>(property);
            var concrete = (CssBorderRadiusProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("15% 3.5%", concrete.Value.CssText);
        }

        [Test]
        public void CssBorderRadiusZeroLegal()
        {
            var snippet = "border-radius: 0";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-radius", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderRadiusProperty>(property);
            var concrete = (CssBorderRadiusProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
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
            Assert.AreEqual("0", concrete.Value.CssText);
        }

        [Test]
        public void CssBorderRadiusThreeLengthsLegal()
        {
            var snippet = "border-radius: 2px 4px 3px";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-radius", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderRadiusProperty>(property);
            var concrete = (CssBorderRadiusProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
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
            Assert.AreEqual("2px 4px 3px", concrete.Value.CssText);
        }

        [Test]
        public void CssBorderRadiusFourLengthsLegal()
        {
            var snippet = "border-radius: 2px 4px 3px 0";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-radius", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderRadiusProperty>(property);
            var concrete = (CssBorderRadiusProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
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
            Assert.AreEqual("2px 4px 3px 0", concrete.Value.CssText);
        }

        [Test]
        public void CssBorderRadiusFiveLengthsIllegal()
        {
            var snippet = "border-radius: 2px 4px 3px 0 1px";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-radius", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderRadiusProperty>(property);
            var concrete = (CssBorderRadiusProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssBorderRadiusLengthFractionLegal()
        {
            var snippet = "border-radius: 1em/5em";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-radius", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderRadiusProperty>(property);
            var concrete = (CssBorderRadiusProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
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
            Assert.AreEqual("1em / 5em", concrete.Value.CssText);
        }

        [Test]
        public void CssBorderRadiusLengthFractionInbalancedLegal()
        {
            var snippet = "border-radius: 4px 3px 6px / 2px 4px";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-radius", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderRadiusProperty>(property);
            var concrete = (CssBorderRadiusProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
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
            Assert.AreEqual("4px 3px 6px / 2px 4px", concrete.Value.CssText);
        }

        [Test]
        public void CssBorderRadiusFullFractionLegal()
        {
            var snippet = "border-radius: 4px 3px 6px 1em / 2px 4px 0 20%";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-radius", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderRadiusProperty>(property);
            var concrete = (CssBorderRadiusProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("4px 3px 6px 1em / 2px 4px 0 20%", concrete.Value.CssText);
        }

        [Test]
        public void CssBorderRadiusFiveTailFractionIllegal()
        {
            var snippet = "border-radius: 4px 3px 6px 1em / 2px 4px 0 20% 0";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-radius", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderRadiusProperty>(property);
            var concrete = (CssBorderRadiusProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssBorderRadiusFiveHeadFractionIllegal()
        {
            var snippet = "border-radius: 4px 3px 6px 1em 0 / 2px 4px 0 20%";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-radius", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderRadiusProperty>(property);
            var concrete = (CssBorderRadiusProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }
    }
}
