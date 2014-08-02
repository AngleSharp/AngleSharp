using AngleSharp;
using AngleSharp.DOM.Css;
using AngleSharp.DOM.Css.Properties;
using AngleSharp.Parser.Css;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.Css
{
    [TestClass]
    public class CssOutlinePropertyTests
    {
        [TestMethod]
        public void CssOutlineStyleDottedLegal()
        {
            var snippet = "outline-style   :  dotTED";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("outline-style", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSOutlineStyleProperty));
            var concrete = (CSSOutlineStyleProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("dotTED", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssOutlineStyleSolidLegal()
        {
            var snippet = "outline-style   :  solid";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("outline-style", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSOutlineStyleProperty));
            var concrete = (CSSOutlineStyleProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("solid", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssOutlineStyleNoIllegal()
        {
            var snippet = "outline-style   :  no";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("outline-style", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSOutlineStyleProperty));
            var concrete = (CSSOutlineStyleProperty)property;
            Assert.AreEqual(CssValueType.Inherit, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [TestMethod]
        public void CssOutlineColorInvertLegal()
        {
            var snippet = "outline-color :  invert ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("outline-color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSOutlineColorProperty));
            var concrete = (CSSOutlineColorProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("invert", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssOutlineColorHslLegal()
        {
            var snippet = "outline-color :  hsl(320, 80%, 50%) ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("outline-color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSOutlineColorProperty));
            var concrete = (CSSOutlineColorProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("rgba(229, 26, 161, 1)", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssOutlineColorHexLegal()
        {
            var snippet = "outline-color :  #0000FF ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("outline-color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSOutlineColorProperty));
            var concrete = (CSSOutlineColorProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("rgba(0, 0, 255, 1)", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssOutlineColorRedLegal()
        {
            var snippet = "outline-color :  red ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("outline-color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSOutlineColorProperty));
            var concrete = (CSSOutlineColorProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("red", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssOutlineColorIllegal()
        {
            var snippet = "outline-color :  blau ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("outline-color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSOutlineColorProperty));
            var concrete = (CSSOutlineColorProperty)property;
            Assert.AreEqual(CssValueType.Inherit, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [TestMethod]
        public void CssOutlineWidthThinImportantLegal()
        {
            var snippet = "outline-width :  thin !important";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("outline-width", property.Name);
            Assert.IsTrue(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSOutlineWidthProperty));
            var concrete = (CSSOutlineWidthProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("thin", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssOutlineWidthNumberIllegal()
        {
            var snippet = "outline-width :  3";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("outline-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSOutlineWidthProperty));
            var concrete = (CSSOutlineWidthProperty)property;
            Assert.AreEqual(CssValueType.Inherit, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [TestMethod]
        public void CssOutlineWidthLengthLegal()
        {
            var snippet = "outline-width :  0.1em";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("outline-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSOutlineWidthProperty));
            var concrete = (CSSOutlineWidthProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0.1em", concrete.Value.CssText);
            Assert.IsInstanceOfType(concrete.Value, typeof(CSSPrimitiveValue<Length>));
            var length = (CSSPrimitiveValue<Length>)concrete.Value;
            Assert.AreEqual(new Length(0.1f, Length.Unit.Em), length.Value);
        }

        [TestMethod]
        public void CssOutlineSingleLegal()
        {
            var snippet = "outline :  thin";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("outline", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSOutlineProperty));
            var concrete = (CSSOutlineProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("thin", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssOutlineDualLegal()
        {
            var snippet = "outline :  thin   invert";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("outline", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSOutlineProperty));
            var concrete = (CSSOutlineProperty)property;
            Assert.AreEqual(CssValueType.ValueList, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("thin invert", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssOutlineAllDottedLegal()
        {
            var snippet = "outline :  dotted 0.3em rgb(255, 255, 255)";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("outline", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSOutlineProperty));
            var concrete = (CSSOutlineProperty)property;
            Assert.AreEqual(CssValueType.ValueList, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("dotted 0.3em rgba(255, 255, 255, 1)", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssOutlineDoubleColorIllegal()
        {
            var snippet = "outline :  dotted #123456 rgb(255, 255, 255)";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("outline", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSOutlineProperty));
            var concrete = (CSSOutlineProperty)property;
            Assert.AreEqual(CssValueType.Inherit, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [TestMethod]
        public void CssOutlineAllSolidLegal()
        {
            var snippet = "outline :  1px solid #000";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("outline", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSOutlineProperty));
            var concrete = (CSSOutlineProperty)property;
            Assert.AreEqual(CssValueType.ValueList, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("1px solid rgba(0, 0, 0, 1)", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssOutlineAllColorNamedLegal()
        {
            var snippet = "outline :  solid black 1px";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("outline", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSOutlineProperty));
            var concrete = (CSSOutlineProperty)property;
            Assert.AreEqual(CssValueType.ValueList, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("solid black 1px", concrete.Value.CssText);
        }
    }
}
