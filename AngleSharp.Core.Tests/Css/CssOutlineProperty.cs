using AngleSharp;
using AngleSharp.Css;
using AngleSharp.Dom.Css;
using AngleSharp.Parser.Css;
using NUnit.Framework;

namespace AngleSharp.Core.Tests.Css
{
    [TestFixture]
    public class CssOutlinePropertyTests
    {
        [Test]
        public void CssOutlineStyleDottedLegal()
        {
            var snippet = "outline-style   :  dotTED";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("outline-style", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssOutlineStyleProperty>(property);
            var concrete = (CssOutlineStyleProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("dotted", concrete.Value.CssText);
        }

        [Test]
        public void CssOutlineStyleSolidLegal()
        {
            var snippet = "outline-style   :  solid";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("outline-style", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssOutlineStyleProperty>(property);
            var concrete = (CssOutlineStyleProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("solid", concrete.Value.CssText);
        }

        [Test]
        public void CssOutlineStyleNoIllegal()
        {
            var snippet = "outline-style   :  no";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("outline-style", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssOutlineStyleProperty>(property);
            var concrete = (CssOutlineStyleProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssOutlineColorInvertLegal()
        {
            var snippet = "outline-color :  invert ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("outline-color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssOutlineColorProperty>(property);
            var concrete = (CssOutlineColorProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("invert", concrete.Value.CssText);
        }

        [Test]
        public void CssOutlineColorHslLegal()
        {
            var snippet = "outline-color :  hsl(320, 80%, 50%) ";//equivalent to rgba(229, 26, 161, 1)
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("outline-color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssOutlineColorProperty>(property);
            var concrete = (CssOutlineColorProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("hsl(320, 80%, 50%)", concrete.Value.CssText);
        }

        [Test]
        public void CssOutlineColorHexLegal()
        {
            var snippet = "outline-color :  #0000FF ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("outline-color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssOutlineColorProperty>(property);
            var concrete = (CssOutlineColorProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("rgba(0, 0, 255, 1)", concrete.Value.CssText);
        }

        [Test]
        public void CssOutlineColorRedLegal()
        {
            var snippet = "outline-color :  red ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("outline-color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssOutlineColorProperty>(property);
            var concrete = (CssOutlineColorProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("red", concrete.Value.CssText);
        }

        [Test]
        public void CssOutlineColorIllegal()
        {
            var snippet = "outline-color :  blau ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("outline-color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssOutlineColorProperty>(property);
            var concrete = (CssOutlineColorProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssOutlineWidthThinImportantLegal()
        {
            var snippet = "outline-width :  thin !important";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("outline-width", property.Name);
            Assert.IsTrue(property.IsImportant);
            Assert.IsInstanceOf<CssOutlineWidthProperty>(property);
            var concrete = (CssOutlineWidthProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("thin", concrete.Value.CssText);
        }

        [Test]
        public void CssOutlineWidthNumberIllegal()
        {
            var snippet = "outline-width :  3";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("outline-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssOutlineWidthProperty>(property);
            var concrete = (CssOutlineWidthProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssOutlineWidthLengthLegal()
        {
            var snippet = "outline-width :  0.1em";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("outline-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssOutlineWidthProperty>(property);
            var concrete = (CssOutlineWidthProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0.1em", concrete.Value.CssText);
            Assert.IsInstanceOf<Length>(concrete.Value);
            var length = (Length)concrete.Value;
            Assert.AreEqual(new Length(0.1f, Length.Unit.Em), length);
        }

        [Test]
        public void CssOutlineSingleLegal()
        {
            var snippet = "outline :  thin";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("outline", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssOutlineProperty>(property);
            var concrete = (CssOutlineProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("thin", concrete.Value.CssText);
        }

        [Test]
        public void CssOutlineDualLegal()
        {
            var snippet = "outline :  thin   invert";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("outline", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssOutlineProperty>(property);
            var concrete = (CssOutlineProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("thin invert", concrete.Value.CssText);
        }

        [Test]
        public void CssOutlineAllDottedLegal()
        {
            var snippet = "outline :  dotted 0.3em rgb(255, 255, 255)";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("outline", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssOutlineProperty>(property);
            var concrete = (CssOutlineProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("dotted 0.3em rgb(255, 255, 255)", concrete.Value.CssText);
        }

        [Test]
        public void CssOutlineDoubleColorIllegal()
        {
            var snippet = "outline :  dotted #123456 rgb(255, 255, 255)";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("outline", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssOutlineProperty>(property);
            var concrete = (CssOutlineProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssOutlineAllSolidLegal()
        {
            var snippet = "outline :  1px solid #000";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("outline", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssOutlineProperty>(property);
            var concrete = (CssOutlineProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("1px solid rgba(0, 0, 0, 1)", concrete.Value.CssText);
        }

        [Test]
        public void CssOutlineAllColorNamedLegal()
        {
            var snippet = "outline :  solid black 1px";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("outline", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssOutlineProperty>(property);
            var concrete = (CssOutlineProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("solid black 1px", concrete.Value.CssText);
        }
    }
}
