namespace AngleSharp.Core.Tests.Css
{
    using AngleSharp.Dom.Css;
    using NUnit.Framework;

    [TestFixture]
    public class CssTextPropertyTests : CssConstructionFunctions
    {

        [Test]
        public void CssWordSpacingZeroLengthLegal()
        {
            var snippet = "word-spacing: 0";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("word-spacing", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssWordSpacingProperty>(property);
            var concrete = (CssWordSpacingProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0", concrete.Value);
        }

        [Test]
        public void CssWordSpacingLengthFloatRemLegal()
        {
            var snippet = "word-spacing: .3rem ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("word-spacing", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssWordSpacingProperty>(property);
            var concrete = (CssWordSpacingProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0.3rem", concrete.Value);
        }

        [Test]
        public void CssWordSpacingLengthFloatEmLegal()
        {
            var snippet = "word-spacing: 0.3em ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("word-spacing", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssWordSpacingProperty>(property);
            var concrete = (CssWordSpacingProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0.3em", concrete.Value);
        }

        [Test]
        public void CssWordSpacingNormalLegal()
        {
            var snippet = "word-spacing: normal ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("word-spacing", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssWordSpacingProperty>(property);
            var concrete = (CssWordSpacingProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("normal", concrete.Value);
        }

        [Test]
        public void CssTextShadowLegalInsetAtLast()
        {
            var snippet = "text-shadow: 0 0 2px black inset";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-shadow", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTextShadowProperty>(property);
            var concrete = (CssTextShadowProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.AreEqual("inset 0 0 2px rgb(0, 0, 0)", concrete.Value);
        }

        [Test]
        public void CssTextShadowLegalColorInFront()
        {
            var snippet = "text-shadow: rgba(255,255,255,0.5) 0px 3px 3px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-shadow", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTextShadowProperty>(property);
            var concrete = (CssTextShadowProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.AreEqual("0 3px 3px rgba(255, 255, 255, 0.5)", concrete.Value);
        }

        [Test]
        public void CssTextShadowLegalMultipleMultilines()
        {
            var snippet = @"text-shadow: 0px 3px 0px #b2a98f,
             0px 14px 10px rgba(0,0,0,0.15),
             0px 24px 2px rgba(0,0,0,0.1),
             0px 34px 30px rgba(0,0,0,0.1)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-shadow", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTextShadowProperty>(property);
            var concrete = (CssTextShadowProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.AreEqual("0 3px 0 rgb(178, 169, 143), 0 14px 10px rgba(0, 0, 0, 0.15), 0 24px 2px rgba(0, 0, 0, 0.1), 0 34px 30px rgba(0, 0, 0, 0.1)", concrete.Value);
        }

        [Test]
        public void CssTextShadowLegalMultipleInline()
        {
            var snippet = "text-shadow: 4px 3px 0px #fff, 9px 8px 0px rgba(0,0,0,0.15)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-shadow", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTextShadowProperty>(property);
            var concrete = (CssTextShadowProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.AreEqual("4px 3px 0 rgb(255, 255, 255), 9px 8px 0 rgba(0, 0, 0, 0.15)", concrete.Value);
        }

        [Test]
        public void CssTextShadowLegalColorRgbaLast()
        {
            var snippet = "text-shadow: 2px 4px 3px rgba(0,0,0,0.3)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-shadow", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTextShadowProperty>(property);
            var concrete = (CssTextShadowProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.AreEqual("2px 4px 3px rgba(0, 0, 0, 0.3)", concrete.Value);
        }

        [Test]
        public void CssTextAlignLegalJustify()
        {
            var snippet = "text-align:justify";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-align", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTextAlignProperty>(property);
            var concrete = (CssTextAlignProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.AreEqual("justify", concrete.Value);
        }

        [Test]
        public void CssTextIndentLegalLength()
        {
            var snippet = "text-indent:3em";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-indent", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTextIndentProperty>(property);
            var concrete = (CssTextIndentProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.AreEqual("3em", concrete.Value);
        }

        [Test]
        public void CssTextIndentLegalZero()
        {
            var snippet = "text-indent:0";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-indent", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTextIndentProperty>(property);
            var concrete = (CssTextIndentProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.AreEqual("0", concrete.Value);
        }

        [Test]
        public void CssTextIndentLegalPercent()
        {
            var snippet = "text-indent:10%";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-indent", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTextIndentProperty>(property);
            var concrete = (CssTextIndentProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.AreEqual("10%", concrete.Value);
        }

        [Test]
        public void CssTextIndentIllegalNone()
        {
            var snippet = "text-indent:none";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-indent", property.Name);
            Assert.IsFalse(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTextIndentProperty>(property);
            var concrete = (CssTextIndentProperty)property;
        }

        [Test]
        public void CssTextDecorationIllegal()
        {
            var snippet = "text-decoration: line-pass";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-decoration", property.Name);
            Assert.IsFalse(property.HasValue);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTextDecorationProperty>(property);
            var concrete = (CssTextDecorationProperty)property;
        }

        [Test]
        public void CssTextDecorationLegalLineThrough()
        {
            var snippet = "text-decoration: line-Through";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-decoration", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTextDecorationProperty>(property);
            var concrete = (CssTextDecorationProperty)property;
            Assert.AreEqual("line-through", concrete.Value);
        }

        [Test]
        public void CssTextDecorationLegalUnderlineOverline()
        {
            var snippet = "text-decoration:  underline  overline";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-decoration", property.Name);
            Assert.IsInstanceOf<CssTextDecorationProperty>(property);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.IsImportant);
            var concrete = (CssTextDecorationProperty)property;
            Assert.AreEqual("underline overline", concrete.Value);
        }

        [Test]
        public void CssTextDecorationColorLegalHex()
        {
            var snippet = "text-decoration-color: #F00";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-decoration-color", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTextDecorationColorProperty>(property);
            var concrete = (CssTextDecorationColorProperty)property;
            Assert.AreEqual("rgb(255, 0, 0)", concrete.Value);
        }

        [Test]
        public void CssTextDecorationColorLegalRed()
        {
            var snippet = "text-decoration-color: red";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-decoration-color", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTextDecorationColorProperty>(property);
            var concrete = (CssTextDecorationColorProperty)property;
            Assert.AreEqual("rgb(255, 0, 0)", concrete.Value);
        }

        [Test]
        public void CssTextDecorationLineIllegalInteger()
        {
            var snippet = "text-decoration-line: 5";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-decoration-line", property.Name);
            Assert.IsFalse(property.HasValue);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTextDecorationLineProperty>(property);
            var concrete = (CssTextDecorationLineProperty)property;
        }

        [Test]
        public void CssTextDecorationLineLegalNone()
        {
            var snippet = "text-decoration-line: none";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-decoration-line", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTextDecorationLineProperty>(property);
            var concrete = (CssTextDecorationLineProperty)property;
            Assert.AreEqual("none", concrete.Value);
        }

        [Test]
        public void CssTextDecorationLineLegalOverlineUnderlineLineThrough()
        {
            var snippet = "text-decoration-line: overline    underline line-through  ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-decoration-line", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTextDecorationLineProperty>(property);
            var concrete = (CssTextDecorationLineProperty)property;
            Assert.AreEqual("overline underline line-through", concrete.Value);
        }

        [Test]
        public void CssTextDecorationStyleLegalWavyUppercase()
        {
            var snippet = "text-decoration-style: WAVY ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-decoration-style", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTextDecorationStyleProperty>(property);
            var concrete = (CssTextDecorationStyleProperty)property;
            Assert.AreEqual("wavy", concrete.Value);
        }

        [Test]
        public void CssTextDecorationStyleIllegalMultiple()
        {
            var snippet = "text-decoration-style: wavy dotted";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("text-decoration-style", property.Name);
            Assert.IsFalse(property.HasValue);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTextDecorationStyleProperty>(property);
            var concrete = (CssTextDecorationStyleProperty)property;
        }

        [Test]
        public void CssTextDecorationExpansionAndRecombination()
        {
            var snippet = ".centered {text-decoration:underline;}";
            var expected = ".centered { text-decoration: underline }";
            var result = ParseRule(snippet);
            var actual = result.CssText;
            Assert.AreEqual(expected, actual);
        }
    }
}
