using AngleSharp;
using AngleSharp.Css;
using AngleSharp.Dom.Css;
using AngleSharp.Parser.Css;
using NUnit.Framework;

namespace AngleSharp.Core.Tests.Css
{
    [TestFixture]
    public class CssTextPropertyTests
    {

        [Test]
        public void CssWordSpacingZeroLengthLegal()
        {
            var snippet = "word-spacing: 0";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("word-spacing", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssWordSpacingProperty>(property);
            var concrete = (CssWordSpacingProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0", concrete.Value.CssText);
        }

        [Test]
        public void CssWordSpacingLengthFloatRemLegal()
        {
            var snippet = "word-spacing: .3rem ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("word-spacing", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssWordSpacingProperty>(property);
            var concrete = (CssWordSpacingProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0.3rem", concrete.Value.CssText);
        }

        [Test]
        public void CssWordSpacingLengthFloatEmLegal()
        {
            var snippet = "word-spacing: 0.3em ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("word-spacing", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssWordSpacingProperty>(property);
            var concrete = (CssWordSpacingProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0.3em", concrete.Value.CssText);
        }

        [Test]
        public void CssWordSpacingNormalLegal()
        {
            var snippet = "word-spacing: normal ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("word-spacing", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssWordSpacingProperty>(property);
            var concrete = (CssWordSpacingProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("normal", concrete.Value.CssText);
        }

        [Test]
        public void CssTextShadowLegalInsetAtLast()
        {
            var snippet = "text-shadow: 0 0 2px black inset";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("text-shadow", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTextShadowProperty>(property);
            var concrete = (CssTextShadowProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            var value = concrete.Value;
            Assert.AreEqual("0 0 2px black inset", value.CssText);
        }

        [Test]
        public void CssTextShadowLegalColorInFront()
        {
            var snippet = "text-shadow: rgba(255,255,255,0.5) 0px 3px 3px";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("text-shadow", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTextShadowProperty>(property);
            var concrete = (CssTextShadowProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            var value = concrete.Value;
            Assert.AreEqual("rgba(255, 255, 255, 0.5) 0 3px 3px", value.CssText);
        }

        [Test]
        public void CssTextShadowLegalMultipleMultilines()
        {
            var snippet = @"text-shadow: 0px 3px 0px #b2a98f,
             0px 14px 10px rgba(0,0,0,0.15),
             0px 24px 2px rgba(0,0,0,0.1),
             0px 34px 30px rgba(0,0,0,0.1)";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("text-shadow", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTextShadowProperty>(property);
            var concrete = (CssTextShadowProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            var value = concrete.Value;
            Assert.AreEqual("0 3px 0 rgba(178, 169, 143, 1), 0 14px 10px rgba(0, 0, 0, 0.15), 0 24px 2px rgba(0, 0, 0, 0.1), 0 34px 30px rgba(0, 0, 0, 0.1)", value.CssText);
        }

        [Test]
        public void CssTextShadowLegalMultipleInline()
        {
            var snippet = "text-shadow: 4px 3px 0px #fff, 9px 8px 0px rgba(0,0,0,0.15)";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("text-shadow", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTextShadowProperty>(property);
            var concrete = (CssTextShadowProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            var value = concrete.Value;
            Assert.AreEqual("4px 3px 0 rgba(255, 255, 255, 1), 9px 8px 0 rgba(0, 0, 0, 0.15)", value.CssText);
        }

        [Test]
        public void CssTextShadowLegalColorRgbaLast()
        {
            var snippet = "text-shadow: 2px 4px 3px rgba(0,0,0,0.3)";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("text-shadow", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTextShadowProperty>(property);
            var concrete = (CssTextShadowProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            var value = concrete.Value;
            Assert.AreEqual("2px 4px 3px rgba(0, 0, 0, 0.3)", value.CssText);
        }

        [Test]
        public void CssTextAlignLegalJustify()
        {
            var snippet = "text-align:justify";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("text-align", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTextAlignProperty>(property);
            var concrete = (CssTextAlignProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            var value = concrete.Value;
            Assert.AreEqual("justify", value.CssText);
        }

        [Test]
        public void CssTextAlignLegalJustifyChangedToLeftAndThenIllegal()
        {
            var snippet = "text-align:justify";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("text-align", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTextAlignProperty>(property);
            var concrete = (CssTextAlignProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            var value = concrete.Value;
            Assert.AreEqual("justify", value.CssText);
            Assert.IsInstanceOf<CssIdentifier>(value);
            concrete.TrySetValue(new CssIdentifier("left"));
            value = concrete.Value;
            Assert.AreEqual("left", value.CssText);
            Assert.IsInstanceOf<CssIdentifier>(value);
            concrete.TrySetValue(new CssIdentifier("whatever"));
            Assert.AreEqual(value, concrete.Value);
        }

        [Test]
        public void CssTextIndentLegalLength()
        {
            var snippet = "text-indent:3em";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("text-indent", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTextIndentProperty>(property);
            var concrete = (CssTextIndentProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            var value = concrete.Value;
            Assert.AreEqual("3em", value.CssText);
        }

        [Test]
        public void CssTextIndentLegalZero()
        {
            var snippet = "text-indent:0";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("text-indent", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTextIndentProperty>(property);
            var concrete = (CssTextIndentProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            var value = concrete.Value;
            Assert.AreEqual("0", value.CssText);
        }

        [Test]
        public void CssTextIndentLegalPercent()
        {
            var snippet = "text-indent:10%";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("text-indent", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTextIndentProperty>(property);
            var concrete = (CssTextIndentProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            var value = concrete.Value;
            Assert.AreEqual("10%", value.CssText);
        }

        [Test]
        public void CssTextIndentIllegalNone()
        {
            var snippet = "text-indent:none";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("text-indent", property.Name);
            Assert.IsFalse(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTextIndentProperty>(property);
            var concrete = (CssTextIndentProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
        }

        [Test]
        public void CssTextDecorationIllegal()
        {
            var snippet = "text-decoration: line-pass";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("text-decoration", property.Name);
            Assert.IsFalse(property.HasValue);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTextDecorationProperty>(property);
            var concrete = (CssTextDecorationProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
        }

        [Test]
        public void CssTextDecorationLegalLineThrough()
        {
            var snippet = "text-decoration: line-Through";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("text-decoration", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTextDecorationProperty>(property);
            var concrete = (CssTextDecorationProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.AreEqual("line-through", concrete.Value.CssText);
        }

        [Test]
        public void CssTextDecorationLegalUnderlineOverline()
        {
            var snippet = "text-decoration:  underline  overline";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("text-decoration", property.Name);
            Assert.IsInstanceOf<CssTextDecorationProperty>(property);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.IsImportant);
            var concrete = (CssTextDecorationProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.AreEqual("underline overline", concrete.Value.CssText);
        }

        [Test]
        public void CssTextDecorationColorLegalHex()
        {
            var snippet = "text-decoration-color: #F00";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("text-decoration-color", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTextDecorationColorProperty>(property);
            var concrete = (CssTextDecorationColorProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.AreEqual("rgba(255, 0, 0, 1)", concrete.Value.CssText);
        }

        [Test]
        public void CssTextDecorationColorLegalRed()
        {
            var snippet = "text-decoration-color: red";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("text-decoration-color", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTextDecorationColorProperty>(property);
            var concrete = (CssTextDecorationColorProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.AreEqual("red", concrete.Value.CssText);
        }

        [Test]
        public void CssTextDecorationLineIllegalInteger()
        {
            var snippet = "text-decoration-line: 5";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("text-decoration-line", property.Name);
            Assert.IsFalse(property.HasValue);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTextDecorationLineProperty>(property);
            var concrete = (CssTextDecorationLineProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
        }

        [Test]
        public void CssTextDecorationLineLegalNone()
        {
            var snippet = "text-decoration-line: none";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("text-decoration-line", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTextDecorationLineProperty>(property);
            var concrete = (CssTextDecorationLineProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.AreEqual("none", concrete.Value.CssText);
        }

        [Test]
        public void CssTextDecorationLineLegalOverlineUnderlineLineThrough()
        {
            var snippet = "text-decoration-line: overline    underline line-through  ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("text-decoration-line", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTextDecorationLineProperty>(property);
            var concrete = (CssTextDecorationLineProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.AreEqual("overline underline line-through", concrete.Value.CssText);
        }

        [Test]
        public void CssTextDecorationStyleLegalWavyUppercase()
        {
            var snippet = "text-decoration-style: WAVY ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("text-decoration-style", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTextDecorationStyleProperty>(property);
            var concrete = (CssTextDecorationStyleProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.AreEqual("wavy", concrete.Value.CssText);
        }

        [Test]
        public void CssTextDecorationStyleIllegalMultiple()
        {
            var snippet = "text-decoration-style: wavy dotted";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("text-decoration-style", property.Name);
            Assert.IsFalse(property.HasValue);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTextDecorationStyleProperty>(property);
            var concrete = (CssTextDecorationStyleProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
        }
    }
}
