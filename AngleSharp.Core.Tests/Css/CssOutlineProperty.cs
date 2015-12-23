namespace AngleSharp.Core.Tests.Css
{
    using AngleSharp.Dom.Css;
    using NUnit.Framework;

    [TestFixture]
    public class CssOutlinePropertyTests : CssConstructionFunctions
    {
        [Test]
        public void CssOutlineStyleDottedLegal()
        {
            var snippet = "outline-style   :  dotTED";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("outline-style", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssOutlineStyleProperty>(property);
            var concrete = (CssOutlineStyleProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("dotted", concrete.Value);
        }

        [Test]
        public void CssOutlineStyleSolidLegal()
        {
            var snippet = "outline-style   :  solid";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("outline-style", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssOutlineStyleProperty>(property);
            var concrete = (CssOutlineStyleProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("solid", concrete.Value);
        }

        [Test]
        public void CssOutlineStyleNoIllegal()
        {
            var snippet = "outline-style   :  no";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("outline-style", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssOutlineStyleProperty>(property);
            var concrete = (CssOutlineStyleProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssOutlineColorInvertLegal()
        {
            var snippet = "outline-color :  invert ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("outline-color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssOutlineColorProperty>(property);
            var concrete = (CssOutlineColorProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("invert", concrete.Value);
        }

        [Test]
        public void CssOutlineColorHslLegal()
        {
            var snippet = "outline-color :  hsl(320, 80%, 50%) ";//equivalent to rgba(229, 26, 161, 1)
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("outline-color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssOutlineColorProperty>(property);
            var concrete = (CssOutlineColorProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("hsl(320deg, 80%, 50%)", concrete.Value);
        }

        [Test]
        public void CssOutlineColorHexLegal()
        {
            var snippet = "outline-color :  #0000FF ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("outline-color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssOutlineColorProperty>(property);
            var concrete = (CssOutlineColorProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("rgb(0, 0, 255)", concrete.Value);
        }

        [Test]
        public void CssOutlineColorRedLegal()
        {
            var snippet = "outline-color :  red ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("outline-color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssOutlineColorProperty>(property);
            var concrete = (CssOutlineColorProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("rgb(255, 0, 0)", concrete.Value);
        }

        [Test]
        public void CssOutlineColorIllegal()
        {
            var snippet = "outline-color :  blau ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("outline-color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssOutlineColorProperty>(property);
            var concrete = (CssOutlineColorProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssOutlineWidthThinImportantLegal()
        {
            var snippet = "outline-width :  thin !important";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("outline-width", property.Name);
            Assert.IsTrue(property.IsImportant);
            Assert.IsInstanceOf<CssOutlineWidthProperty>(property);
            var concrete = (CssOutlineWidthProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("1px", concrete.Value);
        }

        [Test]
        public void CssOutlineWidthNumberIllegal()
        {
            var snippet = "outline-width :  3";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("outline-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssOutlineWidthProperty>(property);
            var concrete = (CssOutlineWidthProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssOutlineWidthLengthLegal()
        {
            var snippet = "outline-width :  0.1em";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("outline-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssOutlineWidthProperty>(property);
            var concrete = (CssOutlineWidthProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0.1em", concrete.Value);
            //Assert.IsInstanceOf<Length>(concrete.Value);
        }

        [Test]
        public void CssOutlineSingleLegal()
        {
            var snippet = "outline :  thin";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("outline", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssOutlineProperty>(property);
            var concrete = (CssOutlineProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("1px", concrete.Value);
        }

        [Test]
        public void CssOutlineDualLegal()
        {
            var snippet = "outline :  thin   invert";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("outline", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssOutlineProperty>(property);
            var concrete = (CssOutlineProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("1px invert", concrete.Value);
        }

        [Test]
        public void CssOutlineAllDottedLegal()
        {
            var snippet = "outline :  dotted 0.3em rgb(255, 255, 255)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("outline", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssOutlineProperty>(property);
            var concrete = (CssOutlineProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0.3em dotted rgb(255, 255, 255)", concrete.Value);
        }

        [Test]
        public void CssOutlineDoubleColorIllegal()
        {
            var snippet = "outline :  dotted #123456 rgb(255, 255, 255)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("outline", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssOutlineProperty>(property);
            var concrete = (CssOutlineProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssOutlineAllSolidLegal()
        {
            var snippet = "outline :  1px solid #000";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("outline", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssOutlineProperty>(property);
            var concrete = (CssOutlineProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("1px solid rgb(0, 0, 0)", concrete.Value);
        }

        [Test]
        public void CssOutlineAllColorNamedLegal()
        {
            var snippet = "outline :  solid black 1px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("outline", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssOutlineProperty>(property);
            var concrete = (CssOutlineProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("1px solid rgb(0, 0, 0)", concrete.Value);
        }
    }
}
