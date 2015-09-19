namespace AngleSharp.Core.Tests.Css
{
    using AngleSharp.Dom.Css;
    using NUnit.Framework;

    [TestFixture]
    public class CssMarginPropertyTests : CssConstructionFunctions
    {
        [Test]
        public void CssMarginLeftLengthLegal()
        {
            var snippet = "margin-left: 15px ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("margin-left", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssMarginLeftProperty>(property);
            var concrete = (CssMarginLeftProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("15px", concrete.Value);
        }

        [Test]
        public void CssMarginLeftInitialLegal()
        {
            var snippet = "margin-left: initial ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("margin-left", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssMarginLeftProperty>(property);
            var concrete = (CssMarginLeftProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("initial", concrete.Value);
        }

        [Test]
        public void CssMarginRightLengthImportantLegal()
        {
            var snippet = "margin-right: 3em!important";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("margin-right", property.Name);
            Assert.IsTrue(property.IsImportant);
            Assert.IsInstanceOf<CssMarginRightProperty>(property);
            var concrete = (CssMarginRightProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("3em", concrete.Value);
        }

        [Test]
        public void CssMarginRightPercentLegal()
        {
            var snippet = "margin-right: 10%";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("margin-right", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssMarginRightProperty>(property);
            var concrete = (CssMarginRightProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("10%", concrete.Value);
        }

        [Test]
        public void CssMarginTopPercentLegal()
        {
            var snippet = "margin-top: 4% ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("margin-top", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssMarginTopProperty>(property);
            var concrete = (CssMarginTopProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("4%", concrete.Value);
        }

        [Test]
        public void CssMarginBottomZeroLegal()
        {
            var snippet = "margin-bottom: 0 ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("margin-bottom", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssMarginBottomProperty>(property);
            var concrete = (CssMarginBottomProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0", concrete.Value);
        }

        [Test]
        public void CssMarginBottomNegativeLegal()
        {
            var snippet = "margin-bottom: -3px ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("margin-bottom", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssMarginBottomProperty>(property);
            var concrete = (CssMarginBottomProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("-3px", concrete.Value);
        }

        [Test]
        public void CssMarginBottomAutoLegal()
        {
            var snippet = "margin-bottom: auto ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("margin-bottom", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssMarginBottomProperty>(property);
            var concrete = (CssMarginBottomProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("auto", concrete.Value);
        }

        [Test]
        public void CssMarginAllZeroLegal()
        {
            var snippet = "margin: 0 ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("margin", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssMarginProperty>(property);
            var concrete = (CssMarginProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0", concrete.Value);
        }

        [Test]
        public void CssMarginAllPercentLegal()
        {
            var snippet = "margin: 25% ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("margin", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssMarginProperty>(property);
            var concrete = (CssMarginProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("25%", concrete.Value);
        }

        [Test]
        public void CssMarginSidesLengthLegal()
        {
            var snippet = "margin: 10px 3em ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("margin", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssMarginProperty>(property);
            var concrete = (CssMarginProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("10px 3em", concrete.Value);
        }

        [Test]
        public void CssMarginSidesLengthAndAutoLegal()
        {
            var snippet = "margin: 10px auto ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("margin", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssMarginProperty>(property);
            var concrete = (CssMarginProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("10px auto", concrete.Value);
        }

        [Test]
        public void CssMarginAutoLegal()
        {
            var snippet = "margin: auto ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("margin", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssMarginProperty>(property);
            var concrete = (CssMarginProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("auto", concrete.Value);
        }

        [Test]
        public void CssMarginThreeValuesLegal()
        {
            var snippet = "margin: 10px 3em 5px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("margin", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssMarginProperty>(property);
            var concrete = (CssMarginProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("10px 3em 5px", concrete.Value);
        }

        [Test]
        public void CssMarginAllValuesWithPercentAndAutoLegal()
        {
            var snippet = "margin: 10px 5% auto 2% ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("margin", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssMarginProperty>(property);
            var concrete = (CssMarginProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("10px 5% auto 2%", concrete.Value);
        }

        [Test]
        public void CssMarginTooManyValuesIllegal()
        {
            var snippet = "margin: 10px 5% 8px 2% 3px auto";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("margin", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssMarginProperty>(property);
            var concrete = (CssMarginProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssMarginShouldBeRecombinedCorrectly()
        {
            var snippet = ".centered {margin-bottom: 1px; margin-top: 2px; margin-left: 3px; margin-right: 4px}";
            var expected = ".centered { margin: 2px 4px 1px 3px }";
            var result = ParseRule(snippet);
            var actual = result.CssText;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CssMarginShouldBeSimplifiedCorrectly()
        {
            var snippet = ".centered {margin:0;margin-left:auto;margin-right:auto;text-align:left;}";
            var expected = ".centered { margin: 0 auto; text-align: left }";
            var result = ParseRule(snippet);
            var actual = result.CssText;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CssMarginShouldBeReducedCompletely()
        {
            var snippet = ".centered {margin-bottom: 0px; margin-top: 0; margin-left: 0px; margin-right: 0}";
            var expected = ".centered { margin: 0 }";
            var result = ParseRule(snippet);
            var actual = result.CssText;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CssMarginReductionForPeriodicExpansion()
        {
            var snippet = "p { margin: 0 auto; }";
            var expected = "p { margin: 0 auto }";
            var result = ParseRule(snippet);
            var actual = result.CssText;
            Assert.AreEqual(expected, actual);
        }
    }
}
