using AngleSharp;
using AngleSharp.DOM.Css;
using AngleSharp.DOM.Css.Properties;
using AngleSharp.Parser.Css;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.Css
{
    [TestClass]
    public class CssTextPropertyTests
    {

        [TestMethod]
        public void CssWordSpacingZeroLengthLegal()
        {
            var snippet = "word-spacing: 0";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("word-spacing", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSWordSpacingProperty));
            var concrete = (CSSWordSpacingProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssWordSpacingLengthFloatRemLegal()
        {
            var snippet = "word-spacing: .3rem ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("word-spacing", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSWordSpacingProperty));
            var concrete = (CSSWordSpacingProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0.3rem", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssWordSpacingLengthFloatEmLegal()
        {
            var snippet = "word-spacing: 0.3em ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("word-spacing", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSWordSpacingProperty));
            var concrete = (CSSWordSpacingProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0.3em", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssWordSpacingNormalLegal()
        {
            var snippet = "word-spacing: normal ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("word-spacing", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSWordSpacingProperty));
            var concrete = (CSSWordSpacingProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("normal", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssTextAlignLegalJustify()
        {
            var snippet = "text-align:justify";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("text-align", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSTextAlignProperty));
            var concrete = (CSSTextAlignProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsTrue(concrete.IsInherited);
            var value = concrete.Value;
            Assert.AreEqual("justify", value.CssText);
            Assert.IsInstanceOfType(value, typeof(CSSPrimitiveValue));
        }

        [TestMethod]
        public void CssTextAlignLegalJustifyChangedToLeftAndThenIllegal()
        {
            var snippet = "text-align:justify";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("text-align", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSTextAlignProperty));
            var concrete = (CSSTextAlignProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsTrue(concrete.IsInherited);
            var value = concrete.Value;
            Assert.AreEqual("justify", value.CssText);
            Assert.IsInstanceOfType(value, typeof(CSSPrimitiveValue));
            concrete.Value = new CSSPrimitiveValue(new CssIdentifier("left"));
            value = concrete.Value;
            Assert.AreEqual("left", value.CssText);
            Assert.IsInstanceOfType(value, typeof(CSSPrimitiveValue));
            concrete.Value = new CSSPrimitiveValue(new CssIdentifier("whatever"));
            Assert.AreEqual(value, concrete.Value);
        }

        [TestMethod]
        public void CssTextIndentLegalLength()
        {
            var snippet = "text-indent:3em";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("text-indent", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSTextIndentProperty));
            var concrete = (CSSTextIndentProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsTrue(concrete.IsInherited);
            var value = concrete.Value;
            Assert.AreEqual("3em", value.CssText);
            Assert.IsInstanceOfType(value, typeof(CSSPrimitiveValue));
        }

        [TestMethod]
        public void CssTextIndentLegalZero()
        {
            var snippet = "text-indent:0";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("text-indent", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSTextIndentProperty));
            var concrete = (CSSTextIndentProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsTrue(concrete.IsInherited);
            var value = concrete.Value;
            Assert.AreEqual("0", value.CssText);
            Assert.IsInstanceOfType(value, typeof(CSSPrimitiveValue));
        }

        [TestMethod]
        public void CssTextIndentLegalPercent()
        {
            var snippet = "text-indent:10%";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("text-indent", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSTextIndentProperty));
            var concrete = (CSSTextIndentProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsTrue(concrete.IsInherited);
            var value = concrete.Value;
            Assert.AreEqual("10%", value.CssText);
            Assert.IsInstanceOfType(value, typeof(CSSPrimitiveValue));
        }

        [TestMethod]
        public void CssTextIndentIllegalNone()
        {
            var snippet = "text-indent:none";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("text-indent", property.Name);
            Assert.IsFalse(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSTextIndentProperty));
            var concrete = (CSSTextIndentProperty)property;
            Assert.AreEqual(CssValueType.Inherit, concrete.Value.Type);
        }

        [TestMethod]
        public void CssTextDecorationIllegal()
        {
            var snippet = "text-decoration: line-pass";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("text-decoration", property.Name);
            Assert.IsFalse(property.HasValue);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSTextDecorationProperty));
            var concrete = (CSSTextDecorationProperty)property;
            Assert.AreEqual(CssValueType.Inherit, concrete.Value.Type);
        }

        [TestMethod]
        public void CssTextDecorationLegalLineThrough()
        {
            var snippet = "text-decoration: line-Through";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("text-decoration", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSTextDecorationProperty));
            var concrete = (CSSTextDecorationProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.AreEqual("line-Through", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssTextDecorationLegalUnderlineOverline()
        {
            var snippet = "text-decoration:  underline  overline";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("text-decoration", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSTextDecorationProperty));
            var concrete = (CSSTextDecorationProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.AreEqual("underline overline", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssTextDecorationColorLegalHex()
        {
            var snippet = "text-decoration-color: #F00";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("text-decoration-color", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSTextDecorationColorProperty));
            var concrete = (CSSTextDecorationColorProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.AreEqual("rgba(255, 0, 0, 1)", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssTextDecorationColorLegalRed()
        {
            var snippet = "text-decoration-color: red";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("text-decoration-color", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSTextDecorationColorProperty));
            var concrete = (CSSTextDecorationColorProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.AreEqual("red", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssTextDecorationLineIllegalInteger()
        {
            var snippet = "text-decoration-line: 5";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("text-decoration-line", property.Name);
            Assert.IsFalse(property.HasValue);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSTextDecorationLineProperty));
            var concrete = (CSSTextDecorationLineProperty)property;
            Assert.AreEqual(CssValueType.Inherit, concrete.Value.Type);
        }

        [TestMethod]
        public void CssTextDecorationLineLegalNone()
        {
            var snippet = "text-decoration-line: none";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("text-decoration-line", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSTextDecorationLineProperty));
            var concrete = (CSSTextDecorationLineProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.AreEqual("none", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssTextDecorationLineLegalOverlineUnderlineLineThrough()
        {
            var snippet = "text-decoration-line: overline    underline line-through  ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("text-decoration-line", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSTextDecorationLineProperty));
            var concrete = (CSSTextDecorationLineProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.AreEqual("overline underline line-through", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssTextDecorationStyleLegalWavyUppercase()
        {
            var snippet = "text-decoration-style: WAVY ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("text-decoration-style", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSTextDecorationStyleProperty));
            var concrete = (CSSTextDecorationStyleProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.AreEqual("WAVY", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssTextDecorationStyleIllegalMultiple()
        {
            var snippet = "text-decoration-style: wavy dotted";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("text-decoration-style", property.Name);
            Assert.IsFalse(property.HasValue);
            Assert.IsFalse(property.IsInherited);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSTextDecorationStyleProperty));
            var concrete = (CSSTextDecorationStyleProperty)property;
            Assert.AreEqual(CssValueType.Inherit, concrete.Value.Type);
        }
    }
}
