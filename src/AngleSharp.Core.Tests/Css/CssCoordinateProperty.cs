namespace AngleSharp.Core.Tests.Css
{
    using AngleSharp.Dom.Css;
    using NUnit.Framework;
    using System.Linq;

    [TestFixture]
    public class CssCoordinatePropertyTests : CssConstructionFunctions
    {
        [Test]
        public void CssHeightLegalPercentage()
        {
            var snippet = "height:   28% ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("height", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssHeightProperty>(property);
            var concrete = (CssHeightProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.AreEqual("28%", concrete.Value);
            //Assert.IsInstanceOf<Percent>(value);
        }

        [Test]
        public void CssHeightLegalLengthInEm()
        {
            var snippet = "height:   0.3em ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("height", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssHeightProperty>(property);
            var concrete = (CssHeightProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.AreEqual("0.3em", concrete.Value);
            //Assert.IsInstanceOf<Length>(value);
        }

        [Test]
        public void CssHeightLegalLengthInPx()
        {
            var snippet = "height:   144px ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("height", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssHeightProperty>(property);
            var concrete = (CssHeightProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.AreEqual("144px", concrete.Value);
            //Assert.IsInstanceOf<Length>(value);
        }

        [Test]
        public void CssHeightLegalAutoUppercase()
        {
            var snippet = "height: AUTO ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("height", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssHeightProperty>(property);
            var concrete = (CssHeightProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.AreEqual("auto", concrete.Value);
        }

        [Test]
        public void CssWidthLegalLengthInCm()
        {
            var snippet = "width:0.5cm";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("width", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssWidthProperty>(property);
            var concrete = (CssWidthProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.AreEqual("0.5cm", concrete.Value);
            //Assert.IsInstanceOf<Length>(value);
        }

        [Test]
        public void CssWidthLegalLengthInMm()
        {
            var snippet = "width:1.5mm";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("width", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssWidthProperty>(property);
            var concrete = (CssWidthProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.AreEqual("1.5mm", concrete.Value);
            //Assert.IsInstanceOf<Length>(value);
        }

        [Test]
        public void CssWidthIllegalLength()
        {
            var snippet = "width:1.5 meter";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("width", property.Name);
            Assert.IsFalse(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssWidthProperty>(property);
            var concrete = (CssWidthProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsNotNull(concrete);
        }

        [Test]
        public void CssLeftLegalPixel()
        {
            var snippet = "left: 25px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("left", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssLeftProperty>(property);
            var concrete = (CssLeftProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
        }

        [Test]
        public void CssTopLegalEm()
        {
            var snippet = "top:  0.7em ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("top", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTopProperty>(property);
            var concrete = (CssTopProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
        }

        [Test]
        public void CssRightLegalMm()
        {
            var snippet = "right:  1.5mm";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("right", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssRightProperty>(property);
            var concrete = (CssRightProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
        }

        [Test]
        public void CssBottomFoundInStyleDeclaration()
        {
            var snippet = "bottom:  50%";
            var style = ParseDeclarations(snippet);
            Assert.AreEqual(1, style.Length);
            var bottom = style.Declarations.First();
            Assert.AreEqual("bottom", bottom.Name);
            Assert.AreEqual("50%", ((ICssStyleDeclaration)style).Bottom);
        }

        [Test]
        public void CssBottomLegalPercent()
        {
            var snippet = "bottom:  50%";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("bottom", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBottomProperty>(property);
            var concrete = (CssBottomProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
        }

        [Test]
        public void CssHeightZeroLegal()
        {
            var snippet = "height:0";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("height", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssHeightProperty>(property);
            var concrete = (CssHeightProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
        }

        [Test]
        public void CssWidthZeroLegal()
        {
            var snippet = "width  :  0";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssWidthProperty>(property);
            var concrete = (CssWidthProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
        }

        [Test]
        public void CssWidthPercentLegal()
        {
            var snippet = "width  :  20.5%";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssWidthProperty>(property);
            var concrete = (CssWidthProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
        }

        [Test]
        public void CssWidthPercentInLegal()
        {
            var snippet = "width  :  3in";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssWidthProperty>(property);
            var concrete = (CssWidthProperty)property;
            Assert.IsFalse(concrete.IsInherited);
        }

        [Test]
        public void CssHeightAngleIllegal()
        {
            var snippet = "height  :  3deg";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("height", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssHeightProperty>(property);
            var concrete = (CssHeightProperty)property;
            Assert.IsFalse(concrete.HasValue);
            Assert.IsFalse(concrete.IsInherited);
        }

        [Test]
        public void CssHeightResolutionIllegal()
        {
            var snippet = "height  :  3dpi";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("height", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssHeightProperty>(property);
            var concrete = (CssHeightProperty)property;
            Assert.IsFalse(concrete.HasValue);
            Assert.IsFalse(concrete.IsInherited);
        }

        [Test]
        public void CssTopLegalRem()
        {
            var snippet = "top:  1.2rem ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("top", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTopProperty>(property);
            var concrete = (CssTopProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
        }

        [Test]
        public void CssRightLegalCm()
        {
            var snippet = "right:  0.5cm";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("right", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssRightProperty>(property);
            var concrete = (CssRightProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
        }

        [Test]
        public void CssBottomLegalPercentTwo()
        {
            var snippet = "bottom:  0.50%";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("bottom", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBottomProperty>(property);
            var concrete = (CssBottomProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
        }

        [Test]
        public void CssBottomLegalZero()
        {
            var snippet = "bottom:  0";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("bottom", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBottomProperty>(property);
            var concrete = (CssBottomProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
        }

        [Test]
        public void CssBottomIllegalNumber()
        {
            var snippet = "bottom:  20";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("bottom", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBottomProperty>(property);
            var concrete = (CssBottomProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssMinHeightLegalZero()
        {
            var snippet = "min-height:  0";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("min-height", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssMinHeightProperty>(property);
            var concrete = (CssMinHeightProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
        }

        [Test]
        public void CssMaxHeightIllegalAuto()
        {
            var snippet = "max-height:  auto";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("max-height", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssMaxHeightProperty>(property);
            var concrete = (CssMaxHeightProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssMaxWidthLegalNone()
        {
            var snippet = "max-width:  none";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("max-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssMaxWidthProperty>(property);
            var concrete = (CssMaxWidthProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("none", concrete.Value);
        }

        [Test]
        public void CssMaxWidthLegalLength()
        {
            var snippet = "max-width:  15px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("max-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssMaxWidthProperty>(property);
            var concrete = (CssMaxWidthProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("15px", concrete.Value);
        }

        [Test]
        public void CssMinWidthLegalPercent()
        {
            var snippet = "min-width:  15%";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("min-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssMinWidthProperty>(property);
            var concrete = (CssMinWidthProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("15%", concrete.Value);
        }
    }
}
