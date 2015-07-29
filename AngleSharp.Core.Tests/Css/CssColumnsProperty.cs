namespace AngleSharp.Core.Tests.Css
{
    using AngleSharp.Dom.Css;
    using NUnit.Framework;

    [TestFixture]
    public class CssColumnsPropertyTests : CssConstructionFunctions
    {
        [Test]
        public void CssColumnWidthLengthLegal()
        {
            var snippet = "column-width: 300px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("column-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnWidthProperty>(property);
            var concrete = (CssColumnWidthProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("300px", concrete.Value);
        }

        [Test]
        public void CssColumnWidthPercentIllegal()
        {
            var snippet = "column-width: 30%";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("column-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnWidthProperty>(property);
            var concrete = (CssColumnWidthProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssColumnWidthVwLegal()
        {
            var snippet = "column-width: 0.3vw";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("column-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnWidthProperty>(property);
            var concrete = (CssColumnWidthProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0.3vw", concrete.Value);
        }

        [Test]
        public void CssColumnWidthAutoUppercaseLegal()
        {
            var snippet = "column-width: AUTO";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("column-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnWidthProperty>(property);
            var concrete = (CssColumnWidthProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("auto", concrete.Value);
        }

        [Test]
        public void CssColumnCountAutoLowercaseLegal()
        {
            var snippet = "column-count: auto";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("column-count", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnCountProperty>(property);
            var concrete = (CssColumnCountProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("auto", concrete.Value);
        }

        [Test]
        public void CssColumnCountNumberLegal()
        {
            var snippet = "column-count: 3";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("column-count", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnCountProperty>(property);
            var concrete = (CssColumnCountProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("3", concrete.Value);
        }

        [Test]
        public void CssColumnCountZeroLegal()
        {
            var snippet = "column-count: 0";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("column-count", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnCountProperty>(property);
            var concrete = (CssColumnCountProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0", concrete.Value);
        }

        [Test]
        public void CssColumsZeroLegal()
        {
            var snippet = "columns: 0";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("columns", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnsProperty>(property);
            var concrete = (CssColumnsProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0", concrete.Value);
        }

        [Test]
        public void CssColumsLengthLegal()
        {
            var snippet = "columns: 10px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("columns", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnsProperty>(property);
            var concrete = (CssColumnsProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("10px", concrete.Value);
        }

        [Test]
        public void CssColumsNumberLegal()
        {
            var snippet = "columns: 4";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("columns", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnsProperty>(property);
            var concrete = (CssColumnsProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("4", concrete.Value);
        }

        [Test]
        public void CssColumsLengthNumberLegal()
        {
            var snippet = "columns: 25em 5";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("columns", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnsProperty>(property);
            var concrete = (CssColumnsProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("25em 5", concrete.Value);
        }

        [Test]
        public void CssColumsNumberLengthLegal()
        {
            var snippet = "columns : 5   25em  ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("columns", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnsProperty>(property);
            var concrete = (CssColumnsProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("25em 5", concrete.Value);
        }

        [Test]
        public void CssColumsAutoAutoLegal()
        {
            var snippet = "columns : auto auto";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("columns", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnsProperty>(property);
            var concrete = (CssColumnsProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("auto auto", concrete.Value);
        }

        [Test]
        public void CssColumsAutoLegal()
        {
            var snippet = "columns : auto  ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("columns", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnsProperty>(property);
            var concrete = (CssColumnsProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("auto", concrete.Value);
        }

        [Test]
        public void CssColumsNumberPercenIllegal()
        {
            var snippet = "columns : 5   25%  ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("columns", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnsProperty>(property);
            var concrete = (CssColumnsProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssColumSpanAllLegal()
        {
            var snippet = "column-span: all";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("column-span", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnSpanProperty>(property);
            var concrete = (CssColumnSpanProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("all", concrete.Value);
        }

        [Test]
        public void CssColumSpanNoneUppercaseLegal()
        {
            var snippet = "column-span: None";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("column-span", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnSpanProperty>(property);
            var concrete = (CssColumnSpanProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("none", concrete.Value);
        }

        [Test]
        public void CssColumSpanLengthIllegal()
        {
            var snippet = "column-span: 10px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("column-span", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnSpanProperty>(property);
            var concrete = (CssColumnSpanProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssColumGapLengthLegal()
        {
            var snippet = "column-gap: 20px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("column-gap", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnGapProperty>(property);
            var concrete = (CssColumnGapProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("20px", concrete.Value);
        }

        [Test]
        public void CssColumGapNormalLegal()
        {
            var snippet = "column-gap: normal";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("column-gap", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnGapProperty>(property);
            var concrete = (CssColumnGapProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("normal", concrete.Value);
        }

        [Test]
        public void CssColumGapZeroLegal()
        {
            var snippet = "column-gap: 0";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("column-gap", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnGapProperty>(property);
            var concrete = (CssColumnGapProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0", concrete.Value);
        }

        [Test]
        public void CssColumGapPercentIllegal()
        {
            var snippet = "column-gap: 20%";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("column-gap", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnGapProperty>(property);
            var concrete = (CssColumnGapProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssColumFillBalanceLegal()
        {
            var snippet = "column-fill: balance;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("column-fill", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnFillProperty>(property);
            var concrete = (CssColumnFillProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("balance", concrete.Value);
        }

        [Test]
        public void CssColumFillAutoLegal()
        {
            var snippet = "column-fill: auto;";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("column-fill", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnFillProperty>(property);
            var concrete = (CssColumnFillProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("auto", concrete.Value);
        }

        [Test]
        public void CssColumRuleColorTransparentLegal()
        {
            var snippet = "column-rule-color: transparent";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("column-rule-color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnRuleColorProperty>(property);
            var concrete = (CssColumnRuleColorProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("rgba(0, 0, 0, 0)", concrete.Value);
        }

        [Test]
        public void CssColumRuleColorRgbLegal()
        {
            var snippet = "column-rule-color: rgb(192, 56, 78)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("column-rule-color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnRuleColorProperty>(property);
            var concrete = (CssColumnRuleColorProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("rgb(192, 56, 78)", concrete.Value);
        }

        [Test]
        public void CssColumRuleColorRedLegal()
        {
            var snippet = "column-rule-color: red";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("column-rule-color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnRuleColorProperty>(property);
            var concrete = (CssColumnRuleColorProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("rgb(255, 0, 0)", concrete.Value);
        }

        [Test]
        public void CssColumRuleColorNoneIllegal()
        {
            var snippet = "column-rule-color: none";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("column-rule-color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnRuleColorProperty>(property);
            var concrete = (CssColumnRuleColorProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssColumRuleStyleInsetTailUpperLegal()
        {
            var snippet = "column-rule-style: inSET";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("column-rule-style", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnRuleStyleProperty>(property);
            var concrete = (CssColumnRuleStyleProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("inset", concrete.Value);
        }

        [Test]
        public void CssColumRuleStyleNoneLegal()
        {
            var snippet = "column-rule-style: none";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("column-rule-style", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnRuleStyleProperty>(property);
            var concrete = (CssColumnRuleStyleProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("none", concrete.Value);
        }

        [Test]
        public void CssColumRuleStyleAutoIllegal()
        {
            var snippet = "column-rule-style: auto ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("column-rule-style", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnRuleStyleProperty>(property);
            var concrete = (CssColumnRuleStyleProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssColumRuleWidthLengthLegal()
        {
            var snippet = "column-rule-width: 2px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("column-rule-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnRuleWidthProperty>(property);
            var concrete = (CssColumnRuleWidthProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("2px", concrete.Value);
        }

        [Test]
        public void CssColumRuleWidthThickLegal()
        {
            var snippet = "column-rule-width: thick";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("column-rule-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnRuleWidthProperty>(property);
            var concrete = (CssColumnRuleWidthProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("5px", concrete.Value);
        }

        [Test]
        public void CssColumRuleWidthMediumLegal()
        {
            var snippet = "column-rule-width : medium !important ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("column-rule-width", property.Name);
            Assert.IsTrue(property.IsImportant);
            Assert.IsInstanceOf<CssColumnRuleWidthProperty>(property);
            var concrete = (CssColumnRuleWidthProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("3px", concrete.Value);
        }

        [Test]
        public void CssColumRuleWidthThinUppercaseLegal()
        {
            var snippet = "column-rule-width: THIN";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("column-rule-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnRuleWidthProperty>(property);
            var concrete = (CssColumnRuleWidthProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("1px", concrete.Value);
        }

        [Test]
        public void CssColumRuleDottedLegal()
        {
            var snippet = "column-rule: dotted";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("column-rule", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnRuleProperty>(property);
            var concrete = (CssColumnRuleProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("dotted", concrete.Value);
        }

        [Test]
        public void CssColumRuleSolidBlueLegal()
        {
            var snippet = "column-rule: solid  blue";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("column-rule", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnRuleProperty>(property);
            var concrete = (CssColumnRuleProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("rgb(0, 0, 255) solid", concrete.Value);
        }

        [Test]
        public void CssColumRuleSolidLengthLegal()
        {
            var snippet = "column-rule: solid 8px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("column-rule", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnRuleProperty>(property);
            var concrete = (CssColumnRuleProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("8px solid", concrete.Value);
        }

        [Test]
        public void CssColumRuleThickInsetBlueLegal()
        {
            var snippet = "column-rule: thick inset blue";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("column-rule", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnRuleProperty>(property);
            var concrete = (CssColumnRuleProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("rgb(0, 0, 255) 5px inset", concrete.Value);
        }
    }
}
