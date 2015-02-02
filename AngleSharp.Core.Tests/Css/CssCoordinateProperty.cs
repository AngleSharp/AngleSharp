using AngleSharp;
using AngleSharp.Css;
using AngleSharp.Dom.Css;
using AngleSharp.Parser.Css;
using NUnit.Framework;

namespace AngleSharp.Core.Tests.Css
{
    [TestFixture]
    public class CssCoordinatePropertyTests
    {
        [Test]
        public void CssHeightLegalPercentage()
        {
            var snippet = "height:   28% ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("height", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssHeightProperty>(property);
            var concrete = (CssHeightProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            var value = concrete.Value;
            Assert.AreEqual("28%", value.CssText);
            Assert.IsInstanceOf<Percent>(value);
        }

        [Test]
        public void CssHeightLegalLengthInEm()
        {
            var snippet = "height:   0.3em ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("height", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssHeightProperty>(property);
            var concrete = (CssHeightProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            var value = concrete.Value;
            Assert.AreEqual("0.3em", value.CssText);
            Assert.IsInstanceOf<Length>(value);
        }

        [Test]
        public void CssHeightLegalLengthInPx()
        {
            var snippet = "height:   144px ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("height", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssHeightProperty>(property);
            var concrete = (CssHeightProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            var value = concrete.Value;
            Assert.AreEqual("144px", value.CssText);
            Assert.IsInstanceOf<Length>(value);
        }

        [Test]
        public void CssHeightLegalAutoUppercase()
        {
            var snippet = "height: AUTO ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("height", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssHeightProperty>(property);
            var concrete = (CssHeightProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            var value = concrete.Value;
            Assert.AreEqual("auto", value.CssText);
            Assert.IsInstanceOf<CssIdentifier>(value);
        }

        [Test]
        public void CssWidthLegalLengthInCm()
        {
            var snippet = "width:0.5cm";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("width", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssWidthProperty>(property);
            var concrete = (CssWidthProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            var value = concrete.Value;
            Assert.AreEqual("0.5cm", value.CssText);
            Assert.IsInstanceOf<Length>(value);
        }

        [Test]
        public void CssWidthLegalLengthInMm()
        {
            var snippet = "width:1.5mm";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("width", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssWidthProperty>(property);
            var concrete = (CssWidthProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            var value = concrete.Value;
            Assert.AreEqual("1.5mm", value.CssText);
            Assert.IsInstanceOf<Length>(value);
        }

        [Test]
        public void CssWidthIllegalLength()
        {
            var snippet = "width:1.5 meter";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("width", property.Name);
            Assert.IsFalse(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssWidthProperty>(property);
            var concrete = (CssWidthProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.AreEqual(CssValue.Initial, concrete.Value);
        }

        [Test]
        public void CssLeftLegalPixel()
        {
            var snippet = "left: 25px";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("left", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssLeftProperty>(property);
            var concrete = (CssLeftProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            var value = (Length)concrete.Value;
            Assert.AreEqual(new Length(25f, Length.Unit.Px), value);
        }

        [Test]
        public void CssTopLegalEm()
        {
            var snippet = "top:  0.7em ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("top", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTopProperty>(property);
            var concrete = (CssTopProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            var value = (Length)concrete.Value;
            Assert.AreEqual(new Length(0.7f, Length.Unit.Em), value);
        }

        [Test]
        public void CssRightLegalMm()
        {
            var snippet = "right:  1.5mm";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("right", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssRightProperty>(property);
            var concrete = (CssRightProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            var value = (Length)concrete.Value;
            Assert.AreEqual(new Length(1.5f, Length.Unit.Mm), value);
        }

        [Test]
        public void CssBottomLegalPercent()
        {
            var snippet = "bottom:  50%";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("bottom", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBottomProperty>(property);
            var concrete = (CssBottomProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            var value = (Percent)concrete.Value;
            Assert.AreEqual(new Percent(50f), value);
        }

        [Test]
        public void CssHeightZeroLegal()
        {
            var snippet = "height:0";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("height", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssHeightProperty>(property);
            var concrete = (CssHeightProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            var value = (Number)concrete.Value;
            Assert.AreEqual(Number.Zero, value);
        }

        [Test]
        public void CssWidthZeroLegal()
        {
            var snippet = "width  :  0";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssWidthProperty>(property);
            var concrete = (CssWidthProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            var value = (Number)concrete.Value;
            Assert.AreEqual(Number.Zero, value);
        }

        [Test]
        public void CssWidthPercentLegal()
        {
            var snippet = "width  :  20.5%";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssWidthProperty>(property);
            var concrete = (CssWidthProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            var value = (Percent)concrete.Value;
            Assert.AreEqual(new Percent(20.5f), value);
        }

        [Test]
        public void CssWidthPercentInLegal()
        {
            var snippet = "width  :  3in";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssWidthProperty>(property);
            var concrete = (CssWidthProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            var value = (Length)concrete.Value;
            Assert.AreEqual(new Length(3f, Length.Unit.In), value);
        }

        [Test]
        public void CssHeightAngleIllegal()
        {
            var snippet = "height  :  3deg";
            var property = CssParser.ParseDeclaration(snippet);
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
            var property = CssParser.ParseDeclaration(snippet);
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
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("top", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTopProperty>(property);
            var concrete = (CssTopProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            var value = (Length)concrete.Value;
            Assert.AreEqual(new Length(1.2f, Length.Unit.Rem), value);
        }

        [Test]
        public void CssRightLegalCm()
        {
            var snippet = "right:  0.5cm";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("right", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssRightProperty>(property);
            var concrete = (CssRightProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            var value = (Length)concrete.Value;
            Assert.AreEqual(new Length(0.5f, Length.Unit.Cm), value);
        }

        [Test]
        public void CssBottomLegalPercentTwo()
        {
            var snippet = "bottom:  0.50%";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("bottom", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBottomProperty>(property);
            var concrete = (CssBottomProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            var value = (Percent)concrete.Value;
            Assert.AreEqual(new Percent(0.5f), value);
        }

        [Test]
        public void CssBottomLegalZero()
        {
            var snippet = "bottom:  0";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("bottom", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBottomProperty>(property);
            var concrete = (CssBottomProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            var value = (Number)concrete.Value;
            Assert.AreEqual(Number.Zero, value);
        }

        [Test]
        public void CssBottomIllegalNumber()
        {
            var snippet = "bottom:  20";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("bottom", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBottomProperty>(property);
            var concrete = (CssBottomProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssMinHeightLegalZero()
        {
            var snippet = "min-height:  0";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("min-height", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssMinHeightProperty>(property);
            var concrete = (CssMinHeightProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            var value = (Number)concrete.Value;
            Assert.AreEqual(Number.Zero, value);
        }

        [Test]
        public void CssMaxHeightIllegalAuto()
        {
            var snippet = "max-height:  auto";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("max-height", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssMaxHeightProperty>(property);
            var concrete = (CssMaxHeightProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssMaxWidthLegalNone()
        {
            var snippet = "max-width:  none";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("max-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssMaxWidthProperty>(property);
            var concrete = (CssMaxWidthProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("none", concrete.Value.CssText);
        }

        [Test]
        public void CssMaxWidthLegalLength()
        {
            var snippet = "max-width:  15px";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("max-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssMaxWidthProperty>(property);
            var concrete = (CssMaxWidthProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("15px", concrete.Value.CssText);
        }

        [Test]
        public void CssMinWidthLegalPercent()
        {
            var snippet = "min-width:  15%";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("min-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssMinWidthProperty>(property);
            var concrete = (CssMinWidthProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("15%", concrete.Value.CssText);
        }
    }
}
