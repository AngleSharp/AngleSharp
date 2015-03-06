using AngleSharp.Dom.Css;
using AngleSharp.Parser.Css;
using NUnit.Framework;

namespace AngleSharp.Core.Tests.Css
{
    [TestFixture]
    public class CssListPropertyTests
    {
        [Test]
        public void CssListStylePositionOutsideLegal()
        {
            var snippet = "list-style-position: outside ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("list-style-position", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssListStylePositionProperty>(property);
            var concrete = (CssListStylePositionProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("outside", concrete.Value.CssText);
        }

        [Test]
        public void CssListStylePositionOutsideIllegal()
        {
            var snippet = "list-style-position: out-side ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("list-style-position", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssListStylePositionProperty>(property);
            var concrete = (CssListStylePositionProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssListStylePositionNoneIllegal()
        {
            var snippet = "list-style-position: none ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("list-style-position", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssListStylePositionProperty>(property);
            var concrete = (CssListStylePositionProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssListStylePositionInsideLegal()
        {
            var snippet = "list-style-position: insiDe ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("list-style-position", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssListStylePositionProperty>(property);
            var concrete = (CssListStylePositionProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("inside", concrete.Value.CssText);
        }

        [Test]
        public void CssListStyleImageNoneLegal()
        {
            var snippet = "list-style-image: none ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("list-style-image", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssListStyleImageProperty>(property);
            var concrete = (CssListStyleImageProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("none", concrete.Value.CssText);
        }

        [Test]
        public void CssListStyleImageUrlLegal()
        {
            var snippet = "list-style-image: url(http://www.example.com/images/list.png)";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("list-style-image", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssListStyleImageProperty>(property);
            var concrete = (CssListStyleImageProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("url(\"http://www.example.com/images/list.png\")", concrete.Value.CssText);
        }

        [Test]
        public void CssListStyleTypeDiscLegal()
        {
            var snippet = "list-style-type: disc ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("list-style-type", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssListStyleTypeProperty>(property);
            var concrete = (CssListStyleTypeProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("disc", concrete.Value.CssText);
        }

        [Test]
        public void CssListStyleTypeLowerAlphaLegal()
        {
            var snippet = "list-style-type: lower-ALPHA ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("list-style-type", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssListStyleTypeProperty>(property);
            var concrete = (CssListStyleTypeProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("lower-alpha", concrete.Value.CssText);
        }

        [Test]
        public void CssListStyleTypeGeorgianLegal()
        {
            var snippet = "list-style-type: georgian ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("list-style-type", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssListStyleTypeProperty>(property);
            var concrete = (CssListStyleTypeProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("georgian", concrete.Value.CssText);
        }

        [Test]
        public void CssListStyleTypeDecimalLeadingZeroLegal()
        {
            var snippet = "list-style-type: decimal-leading-zerO ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("list-style-type", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssListStyleTypeProperty>(property);
            var concrete = (CssListStyleTypeProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("decimal-leading-zero", concrete.Value.CssText);
        }

        [Test]
        public void CssListStyleTypeNumberIllegal()
        {
            var snippet = "list-style-type: number ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("list-style-type", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssListStyleTypeProperty>(property);
            var concrete = (CssListStyleTypeProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssListStyleCircleLegal()
        {
            var snippet = "list-style: circle ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("list-style", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssListStyleProperty>(property);
            var concrete = (CssListStyleProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("circle", concrete.Value.CssText);
        }

        [Test]
        public void CssListStyleNone()
        {
            var snippet = "list-style: none";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("list-style", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssListStyleProperty>(property);
            var concrete = (CssListStyleProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("none", concrete.Value.CssText);
        }

        [Test]
        public void CssListStyleSquareInsideLegal()
        {
            var snippet = "list-style: square inside ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("list-style", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssListStyleProperty>(property);
            var concrete = (CssListStyleProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("square inside", concrete.Value.CssText);
        }

        [Test]
        public void CssListStyleSquareImageInsideLegal()
        {
            var snippet = "list-style: square url('image.png') inside ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("list-style", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssListStyleProperty>(property);
            var concrete = (CssListStyleProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("square url(\"image.png\") inside", concrete.Value.CssText);
        }

        [Test]
        public void CssCounterResetLegal()
        {
            var snippet = "counter-reset: chapter section 1 page;";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("counter-reset", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssCounterResetProperty>(property);
            var concrete = (CssCounterResetProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("chapter section 1 page", concrete.Value.CssText);
        }

        [Test]
        public void CssCounterResetSingleLegal()
        {
            var snippet = "counter-reset: counter-name";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("counter-reset", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssCounterResetProperty>(property);
            var concrete = (CssCounterResetProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("counter-name", concrete.Value.CssText);
        }

        [Test]
        public void CssCounterResetNoneLegal()
        {
            var snippet = "counter-reset: none";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("counter-reset", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssCounterResetProperty>(property);
            var concrete = (CssCounterResetProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("none", concrete.Value.CssText);
        }

        [Test]
        public void CssCounterResetNumberIllegal()
        {
            var snippet = "counter-reset: 3";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("counter-reset", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssCounterResetProperty>(property);
            var concrete = (CssCounterResetProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssCounterResetNegativeLegal()
        {
            var snippet = "counter-reset  :  counter-name   -1";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("counter-reset", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssCounterResetProperty>(property);
            var concrete = (CssCounterResetProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("counter-name -1", concrete.Value.CssText);
        }

        [Test]
        public void CssCounterResetTwoCountersExplicitLegal()
        {
            var snippet = "counter-reset  :  counter1   1   counter2   4  ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("counter-reset", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssCounterResetProperty>(property);
            var concrete = (CssCounterResetProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("counter1 1 counter2 4", concrete.Value.CssText);
        }

        [Test]
        public void CssCounterIncrementNoneLegal()
        {
            var snippet = "counter-increment: none";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("counter-increment", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssCounterIncrementProperty>(property);
            var concrete = (CssCounterIncrementProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("none", concrete.Value.CssText);
        }

        [Test]
        public void CssCounterIncrementLegal()
        {
            var snippet = "counter-increment: chapter section 2 page";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("counter-increment", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssCounterIncrementProperty>(property);
            var concrete = (CssCounterIncrementProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("chapter section 2 page", concrete.Value.CssText);
        }
    }
}
