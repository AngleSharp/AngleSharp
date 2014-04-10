using System;
using AngleSharp.DOM.Css;
using AngleSharp.DOM.Css.Properties;
using AngleSharp.Parser.Css;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.Css
{
    [TestClass]
    public class CssListPropertyTests
    {
        [TestMethod]
        public void CssListStylePositionOutsideLegal()
        {
            var snippet = "list-style-position: outside ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("list-style-position", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSListStylePositionProperty));
            var concrete = (CSSListStylePositionProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("outside", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssListStylePositionNoneIllegal()
        {
            var snippet = "list-style-position: none ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("list-style-position", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSListStylePositionProperty));
            var concrete = (CSSListStylePositionProperty)property;
            Assert.AreEqual(CssValueType.Inherit, concrete.Value.CssValueType);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [TestMethod]
        public void CssListStylePositionInsideLegal()
        {
            var snippet = "list-style-position: insiDe ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("list-style-position", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSListStylePositionProperty));
            var concrete = (CSSListStylePositionProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("insiDe", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssListStyleImageNoneLegal()
        {
            var snippet = "list-style-image: none ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("list-style-image", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSListStyleImageProperty));
            var concrete = (CSSListStyleImageProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("none", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssListStyleImageUrlLegal()
        {
            var snippet = "list-style-image: url(http://www.example.com/images/list.png)";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("list-style-image", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSListStyleImageProperty));
            var concrete = (CSSListStyleImageProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("url('http://www.example.com/images/list.png')", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssListStyleTypeDiscLegal()
        {
            var snippet = "list-style-type: disc ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("list-style-type", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSListStyleTypeProperty));
            var concrete = (CSSListStyleTypeProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("disc", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssListStyleTypeLowerAlphaLegal()
        {
            var snippet = "list-style-type: lower-ALPHA ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("list-style-type", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSListStyleTypeProperty));
            var concrete = (CSSListStyleTypeProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("lower-ALPHA", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssListStyleTypeGeorgianLegal()
        {
            var snippet = "list-style-type: georgian ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("list-style-type", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSListStyleTypeProperty));
            var concrete = (CSSListStyleTypeProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("georgian", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssListStyleTypeDecimalLeadingZeroLegal()
        {
            var snippet = "list-style-type: decimal-leading-zerO ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("list-style-type", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSListStyleTypeProperty));
            var concrete = (CSSListStyleTypeProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("decimal-leading-zerO", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssListStyleTypeNumberIllegal()
        {
            var snippet = "list-style-type: number ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("list-style-type", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSListStyleTypeProperty));
            var concrete = (CSSListStyleTypeProperty)property;
            Assert.AreEqual(CssValueType.Inherit, concrete.Value.CssValueType);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [TestMethod]
        public void CssListStyleCircleLegal()
        {
            var snippet = "list-style: circle ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("list-style", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSListStyleProperty));
            var concrete = (CSSListStyleProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("circle", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssListStyleSquareInsideLegal()
        {
            var snippet = "list-style: square inside ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("list-style", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSListStyleProperty));
            var concrete = (CSSListStyleProperty)property;
            Assert.AreEqual(CssValueType.ValueList, concrete.Value.CssValueType);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("square inside", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssListStyleSquareImageInsideLegal()
        {
            var snippet = "list-style: square url('image.png') inside ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("list-style", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSListStyleProperty));
            var concrete = (CSSListStyleProperty)property;
            Assert.AreEqual(CssValueType.ValueList, concrete.Value.CssValueType);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("square url('image.png') inside", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssCounterResetLegal()
        {
            var snippet = "counter-reset: chapter section 1 page;";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("counter-reset", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSCounterResetProperty));
            var concrete = (CSSCounterResetProperty)property;
            Assert.AreEqual(CssValueType.ValueList, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("chapter section 1 page", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssCounterResetSingleLegal()
        {
            var snippet = "counter-reset: counter-name";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("counter-reset", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSCounterResetProperty));
            var concrete = (CSSCounterResetProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("counter-name", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssCounterResetNoneLegal()
        {
            var snippet = "counter-reset: none";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("counter-reset", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSCounterResetProperty));
            var concrete = (CSSCounterResetProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("none", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssCounterResetNumberIllegal()
        {
            var snippet = "counter-reset: 3";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("counter-reset", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSCounterResetProperty));
            var concrete = (CSSCounterResetProperty)property;
            Assert.AreEqual(CssValueType.Inherit, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [TestMethod]
        public void CssCounterResetNegativeLegal()
        {
            var snippet = "counter-reset  :  counter-name   -1";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("counter-reset", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSCounterResetProperty));
            var concrete = (CSSCounterResetProperty)property;
            Assert.AreEqual(CssValueType.ValueList, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("counter-name -1", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssCounterResetTwoCountersExplicitLegal()
        {
            var snippet = "counter-reset  :  counter1   1   counter2   4  ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("counter-reset", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSCounterResetProperty));
            var concrete = (CSSCounterResetProperty)property;
            Assert.AreEqual(CssValueType.ValueList, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("counter1 1 counter2 4", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssCounterIncrementNoneLegal()
        {
            var snippet = "counter-increment: none";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("counter-increment", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSCounterIncrementProperty));
            var concrete = (CSSCounterIncrementProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("none", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssCounterIncrementLegal()
        {
            var snippet = "counter-increment: chapter section 2 page";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("counter-increment", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSCounterIncrementProperty));
            var concrete = (CSSCounterIncrementProperty)property;
            Assert.AreEqual(CssValueType.ValueList, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("chapter section 2 page", concrete.Value.CssText);
        }
    }
}
