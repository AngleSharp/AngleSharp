using AngleSharp;
using AngleSharp.DOM.Css;
using AngleSharp.Parser.Css;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.Css
{
    [TestClass]
    public class CssCoordinatePropertyTests
    {
        [TestMethod]
        public void CssHeightLegalPercentage()
        {
            var snippet = "height:   28% ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("height", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSHeightProperty));
            var concrete = (CSSHeightProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            var value = concrete.Value;
            Assert.AreEqual("28%", value.CssText);
            Assert.IsInstanceOfType(value, typeof(CSSPrimitiveValue));
        }

        [TestMethod]
        public void CssHeightLegalLengthInEm()
        {
            var snippet = "height:   0.3em ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("height", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSHeightProperty));
            var concrete = (CSSHeightProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            var value = concrete.Value;
            Assert.AreEqual("0.3em", value.CssText);
            Assert.IsInstanceOfType(value, typeof(CSSPrimitiveValue));
        }

        [TestMethod]
        public void CssHeightLegalLengthInPx()
        {
            var snippet = "height:   144px ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("height", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSHeightProperty));
            var concrete = (CSSHeightProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            var value = concrete.Value;
            Assert.AreEqual("144px", value.CssText);
            Assert.IsInstanceOfType(value, typeof(CSSPrimitiveValue));
        }

        [TestMethod]
        public void CssHeightLegalAutoUppercase()
        {
            var snippet = "height: AUTO ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("height", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSHeightProperty));
            var concrete = (CSSHeightProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            var value = concrete.Value;
            Assert.AreEqual("AUTO", value.CssText);
            Assert.IsInstanceOfType(value, typeof(CSSPrimitiveValue));
        }

        [TestMethod]
        public void CssWidthLegalLengthInCm()
        {
            var snippet = "width:0.5cm";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("width", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSWidthProperty));
            var concrete = (CSSWidthProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            var value = concrete.Value;
            Assert.AreEqual("0.5cm", value.CssText);
            Assert.IsInstanceOfType(value, typeof(CSSPrimitiveValue));
        }

        [TestMethod]
        public void CssWidthLegalLengthInMm()
        {
            var snippet = "width:1.5mm";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("width", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSWidthProperty));
            var concrete = (CSSWidthProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            var value = concrete.Value;
            Assert.AreEqual("1.5mm", value.CssText);
            Assert.IsInstanceOfType(value, typeof(CSSPrimitiveValue));
        }

        [TestMethod]
        public void CssWidthIllegalLength()
        {
            var snippet = "width:1.5 meter";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("width", property.Name);
            Assert.IsFalse(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSWidthProperty));
            var concrete = (CSSWidthProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.AreEqual(CSSValue.Initial, concrete.Value);
        }

        [TestMethod]
        public void CssLeftLegalPixel()
        {
            var snippet = "left: 25px";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("left", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSLeftProperty));
            var concrete = (CSSLeftProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            var value = (CSSPrimitiveValue)concrete.Value;
            Assert.AreEqual(new Length(25f, Length.Unit.Px), value.Value);
        }

        [TestMethod]
        public void CssTopLegalEm()
        {
            var snippet = "top:  0.7em ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("top", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSTopProperty));
            var concrete = (CSSTopProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            var value = (CSSPrimitiveValue)concrete.Value;
            Assert.AreEqual(new Length(0.7f, Length.Unit.Em), value.Value);
        }

        [TestMethod]
        public void CssRightLegalMm()
        {
            var snippet = "right:  1.5mm";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("right", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSRightProperty));
            var concrete = (CSSRightProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            var value = (CSSPrimitiveValue)concrete.Value;
            Assert.AreEqual(new Length(1.5f, Length.Unit.Mm), value.Value);
        }

        [TestMethod]
        public void CssBottomLegalPercent()
        {
            var snippet = "bottom:  50%";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("bottom", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSBottomProperty));
            var concrete = (CSSBottomProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            var value = (CSSPrimitiveValue)concrete.Value;
            Assert.AreEqual(new Percent(50f), value.Value);
        }

        [TestMethod]
        public void CssHeightZeroLegal()
        {
            var snippet = "height:0";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("height", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSHeightProperty));
            var concrete = (CSSHeightProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            var value = (CSSPrimitiveValue)concrete.Value;
            Assert.AreEqual(new Number(), value.Value);
        }

        [TestMethod]
        public void CssWidthZeroLegal()
        {
            var snippet = "width  :  0";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSWidthProperty));
            var concrete = (CSSWidthProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            var value = (CSSPrimitiveValue)concrete.Value;
            Assert.AreEqual(new Number(), value.Value);
        }

        [TestMethod]
        public void CssWidthPercentLegal()
        {
            var snippet = "width  :  20.5%";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSWidthProperty));
            var concrete = (CSSWidthProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            var value = (CSSPrimitiveValue)concrete.Value;
            Assert.AreEqual(new Percent(20.5f), value.Value);
        }

        [TestMethod]
        public void CssWidthPercentInLegal()
        {
            var snippet = "width  :  3in";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSWidthProperty));
            var concrete = (CSSWidthProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            var value = (CSSPrimitiveValue)concrete.Value;
            Assert.AreEqual(new Length(3f, Length.Unit.In), value.Value);
        }

        [TestMethod]
        public void CssHeightAngleIllegal()
        {
            var snippet = "height  :  3deg";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("height", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSHeightProperty));
            var concrete = (CSSHeightProperty)property;
            Assert.IsFalse(concrete.HasValue);
            Assert.IsFalse(concrete.IsInherited);
        }

        [TestMethod]
        public void CssHeightResolutionIllegal()
        {
            var snippet = "height  :  3dpi";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("height", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSHeightProperty));
            var concrete = (CSSHeightProperty)property;
            Assert.IsFalse(concrete.HasValue);
            Assert.IsFalse(concrete.IsInherited);
        }

        [TestMethod]
        public void CssTopLegalRem()
        {
            var snippet = "top:  1.2rem ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("top", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSTopProperty));
            var concrete = (CSSTopProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            var value = (CSSPrimitiveValue)concrete.Value;
            Assert.AreEqual(new Length(1.2f, Length.Unit.Rem), value.Value);
        }

        [TestMethod]
        public void CssRightLegalCm()
        {
            var snippet = "right:  0.5cm";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("right", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSRightProperty));
            var concrete = (CSSRightProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            var value = (CSSPrimitiveValue)concrete.Value;
            Assert.AreEqual(new Length(0.5f, Length.Unit.Cm), value.Value);
        }

        [TestMethod]
        public void CssBottomLegalPercentTwo()
        {
            var snippet = "bottom:  0.50%";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("bottom", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSBottomProperty));
            var concrete = (CSSBottomProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            var value = (CSSPrimitiveValue)concrete.Value;
            Assert.AreEqual(new Percent(0.5f), value.Value);
        }

        [TestMethod]
        public void CssBottomLegalZero()
        {
            var snippet = "bottom:  0";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("bottom", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSBottomProperty));
            var concrete = (CSSBottomProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            var value = (CSSPrimitiveValue)concrete.Value;
            Assert.AreEqual(new Number(), value.Value);
        }

        [TestMethod]
        public void CssBottomIllegalNumber()
        {
            var snippet = "bottom:  20";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("bottom", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSBottomProperty));
            var concrete = (CSSBottomProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [TestMethod]
        public void CssMinHeightLegalZero()
        {
            var snippet = "min-height:  0";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("min-height", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSMinHeightProperty));
            var concrete = (CSSMinHeightProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            var value = (CSSPrimitiveValue)concrete.Value;
            Assert.AreEqual(new Number(), value.Value);
        }

        [TestMethod]
        public void CssMaxHeightIllegalAuto()
        {
            var snippet = "max-height:  auto";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("max-height", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSMaxHeightProperty));
            var concrete = (CSSMaxHeightProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [TestMethod]
        public void CssMaxWidthLegalNone()
        {
            var snippet = "max-width:  none";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("max-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSMaxWidthProperty));
            var concrete = (CSSMaxWidthProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("none", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssMaxWidthLegalLength()
        {
            var snippet = "max-width:  15px";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("max-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSMaxWidthProperty));
            var concrete = (CSSMaxWidthProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("15px", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssMinWidthLegalPercent()
        {
            var snippet = "min-width:  15%";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("min-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSMinWidthProperty));
            var concrete = (CSSMinWidthProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("15%", concrete.Value.CssText);
        }
    }
}
