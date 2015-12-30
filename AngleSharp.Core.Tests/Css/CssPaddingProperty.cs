namespace AngleSharp.Core.Tests.Css
{
    using AngleSharp.Dom.Css;
    using NUnit.Framework;

    [TestFixture]
    public class CssPaddingPropertyTests : CssConstructionFunctions
    {
        [Test]
        public void CssPaddingLeftLengthLegal()
        {
            var snippet = "padding-left: 15px ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("padding-left", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssPaddingLeftProperty>(property);
            var concrete = (CssPaddingLeftProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("15px", concrete.Value);
        }

        [Test]
        public void CssPaddingRightLengthImportantLegal()
        {
            var snippet = "padding-right: 3em!important";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("padding-right", property.Name);
            Assert.IsTrue(property.IsImportant);
            Assert.IsInstanceOf<CssPaddingRightProperty>(property);
            var concrete = (CssPaddingRightProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("3em", concrete.Value);
        }

        [Test]
        public void CssPaddingTopPercentLegal()
        {
            var snippet = "padding-top: 4% ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("padding-top", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssPaddingTopProperty>(property);
            var concrete = (CssPaddingTopProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("4%", concrete.Value);
        }

        [Test]
        public void CssPaddingBottomZeroLegal()
        {
            var snippet = "padding-bottom: 0 ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("padding-bottom", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssPaddingBottomProperty>(property);
            var concrete = (CssPaddingBottomProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0", concrete.Value);
        }

        [Test]
        public void CssPaddingAllZeroLegal()
        {
            var snippet = "padding: 0 ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("padding", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssPaddingProperty>(property);
            var concrete = (CssPaddingProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0", concrete.Value);
        }

        [Test]
        public void CssPaddingAllPercentLegal()
        {
            var snippet = "padding: 25% ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("padding", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssPaddingProperty>(property);
            var concrete = (CssPaddingProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("25%", concrete.Value);
        }

        [Test]
        public void CssPaddingSidesLengthLegal()
        {
            var snippet = "padding: 10px 3em ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("padding", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssPaddingProperty>(property);
            var concrete = (CssPaddingProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("10px 3em", concrete.Value);
        }

        [Test]
        public void CssPaddingAutoIllegal()
        {
            var snippet = "padding: auto ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("padding", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssPaddingProperty>(property);
            var concrete = (CssPaddingProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssPaddingThreeValuesLegal()
        {
            var snippet = "padding: 10px 3em 5px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("padding", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssPaddingProperty>(property);
            var concrete = (CssPaddingProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("10px 3em 5px", concrete.Value);
        }

        [Test]
        public void CssPaddingAllValuesWithPercentLegal()
        {
            var snippet = "padding: 10px 5% 8px 2% ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("padding", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssPaddingProperty>(property);
            var concrete = (CssPaddingProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("10px 5% 8px 2%", concrete.Value);
        }

        [Test]
        public void CssPaddingTooManyValuesIllegal()
        {
            var snippet = "padding: 10px 5% 8px 2% 3px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("padding", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssPaddingProperty>(property);
            var concrete = (CssPaddingProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssPaddingShouldBeRecombinedCorrectly()
        {
            var snippet = ".centered {padding-bottom: 2em; padding-top: 2.5em; padding-left: 0; padding-right: 0}";
            var expected = ".centered { padding: 2.5em 0 2em }";
            var result = ParseRule(snippet);
            var actual = result.CssText;
            Assert.AreEqual(expected, actual);
        }
    }
}
