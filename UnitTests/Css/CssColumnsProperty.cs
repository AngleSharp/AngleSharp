using AngleSharp.DOM.Css;
using AngleSharp.Parser.Css;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.Css
{
    [TestClass]
    public class CssColumnsPropertyTests
    {
        [TestMethod]
        public void CssColumnWidthLengthLegal()
        {
            var snippet = "column-width: 300px";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSColumnWidthProperty));
            var concrete = (CSSColumnWidthProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("300px", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssColumnWidthPercentIllegal()
        {
            var snippet = "column-width: 30%";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSColumnWidthProperty));
            var concrete = (CSSColumnWidthProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [TestMethod]
        public void CssColumnWidthVwLegal()
        {
            var snippet = "column-width: 0.3vw";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSColumnWidthProperty));
            var concrete = (CSSColumnWidthProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0.3vw", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssColumnWidthAutoUppercaseLegal()
        {
            var snippet = "column-width: AUTO";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSColumnWidthProperty));
            var concrete = (CSSColumnWidthProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("auto", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssColumnCountAutoLowercaseLegal()
        {
            var snippet = "column-count: auto";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-count", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSColumnCountProperty));
            var concrete = (CSSColumnCountProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("auto", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssColumnCountNumberLegal()
        {
            var snippet = "column-count: 3";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-count", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSColumnCountProperty));
            var concrete = (CSSColumnCountProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("3", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssColumnCountZeroLegal()
        {
            var snippet = "column-count: 0";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-count", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSColumnCountProperty));
            var concrete = (CSSColumnCountProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssColumsZeroLegal()
        {
            var snippet = "columns: 0";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("columns", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSColumnsProperty));
            var concrete = (CSSColumnsProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssColumsLengthLegal()
        {
            var snippet = "columns: 10px";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("columns", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSColumnsProperty));
            var concrete = (CSSColumnsProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("10px", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssColumsNumberLegal()
        {
            var snippet = "columns: 4";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("columns", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSColumnsProperty));
            var concrete = (CSSColumnsProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("4", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssColumsLengthNumberLegal()
        {
            var snippet = "columns: 25em 5";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("columns", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSColumnsProperty));
            var concrete = (CSSColumnsProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("25em 5", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssColumsNumberLengthLegal()
        {
            var snippet = "columns : 5   25em  ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("columns", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSColumnsProperty));
            var concrete = (CSSColumnsProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("5 25em", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssColumsAutoAutoLegal()
        {
            var snippet = "columns : auto auto";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("columns", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSColumnsProperty));
            var concrete = (CSSColumnsProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("auto auto", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssColumsAutoLegal()
        {
            var snippet = "columns : auto  ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("columns", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSColumnsProperty));
            var concrete = (CSSColumnsProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("auto", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssColumsNumberPercenIllegal()
        {
            var snippet = "columns : 5   25%  ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("columns", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSColumnsProperty));
            var concrete = (CSSColumnsProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [TestMethod]
        public void CssColumSpanAllLegal()
        {
            var snippet = "column-span: all";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-span", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSColumnSpanProperty));
            var concrete = (CSSColumnSpanProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("all", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssColumSpanNoneUppercaseLegal()
        {
            var snippet = "column-span: None";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-span", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSColumnSpanProperty));
            var concrete = (CSSColumnSpanProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("none", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssColumSpanLengthIllegal()
        {
            var snippet = "column-span: 10px";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-span", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSColumnSpanProperty));
            var concrete = (CSSColumnSpanProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [TestMethod]
        public void CssColumGapLengthLegal()
        {
            var snippet = "column-gap: 20px";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-gap", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSColumnGapProperty));
            var concrete = (CSSColumnGapProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("20px", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssColumGapNormalLegal()
        {
            var snippet = "column-gap: normal";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-gap", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSColumnGapProperty));
            var concrete = (CSSColumnGapProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("normal", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssColumGapZeroLegal()
        {
            var snippet = "column-gap: 0";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-gap", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSColumnGapProperty));
            var concrete = (CSSColumnGapProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssColumGapPercentIllegal()
        {
            var snippet = "column-gap: 20%";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-gap", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSColumnGapProperty));
            var concrete = (CSSColumnGapProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [TestMethod]
        public void CssColumFillBalanceLegal()
        {
            var snippet = "column-fill: balance;";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-fill", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSColumnFillProperty));
            var concrete = (CSSColumnFillProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("balance", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssColumFillAutoLegal()
        {
            var snippet = "column-fill: auto;";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-fill", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSColumnFillProperty));
            var concrete = (CSSColumnFillProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("auto", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssColumRuleColorTransparentLegal()
        {
            var snippet = "column-rule-color: transparent";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-rule-color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSColumnRuleColorProperty));
            var concrete = (CSSColumnRuleColorProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("transparent", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssColumRuleColorRgbLegal()
        {
            var snippet = "column-rule-color: rgb(192, 56, 78)";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-rule-color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSColumnRuleColorProperty));
            var concrete = (CSSColumnRuleColorProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("rgb(192, 56, 78)", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssColumRuleColorRedLegal()
        {
            var snippet = "column-rule-color: red";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-rule-color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSColumnRuleColorProperty));
            var concrete = (CSSColumnRuleColorProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("red", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssColumRuleColorNoneIllegal()
        {
            var snippet = "column-rule-color: none";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-rule-color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSColumnRuleColorProperty));
            var concrete = (CSSColumnRuleColorProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [TestMethod]
        public void CssColumRuleStyleInsetTailUpperLegal()
        {
            var snippet = "column-rule-style: inSET";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-rule-style", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSColumnRuleStyleProperty));
            var concrete = (CSSColumnRuleStyleProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("inset", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssColumRuleStyleNoneLegal()
        {
            var snippet = "column-rule-style: none";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-rule-style", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSColumnRuleStyleProperty));
            var concrete = (CSSColumnRuleStyleProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("none", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssColumRuleStyleAutoIllegal()
        {
            var snippet = "column-rule-style: auto ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-rule-style", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSColumnRuleStyleProperty));
            var concrete = (CSSColumnRuleStyleProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [TestMethod]
        public void CssColumRuleWidthLengthLegal()
        {
            var snippet = "column-rule-width: 2px";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-rule-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSColumnRuleWidthProperty));
            var concrete = (CSSColumnRuleWidthProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("2px", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssColumRuleWidthThickLegal()
        {
            var snippet = "column-rule-width: thick";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-rule-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSColumnRuleWidthProperty));
            var concrete = (CSSColumnRuleWidthProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("thick", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssColumRuleWidthMediumLegal()
        {
            var snippet = "column-rule-width : medium !important ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-rule-width", property.Name);
            Assert.IsTrue(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSColumnRuleWidthProperty));
            var concrete = (CSSColumnRuleWidthProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("medium", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssColumRuleWidthThinUppercaseLegal()
        {
            var snippet = "column-rule-width: THIN";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-rule-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSColumnRuleWidthProperty));
            var concrete = (CSSColumnRuleWidthProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("thin", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssColumRuleDottedLegal()
        {
            var snippet = "column-rule: dotted";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-rule", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSColumnRuleProperty));
            var concrete = (CSSColumnRuleProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("dotted", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssColumRuleSolidBlueLegal()
        {
            var snippet = "column-rule: solid  blue";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-rule", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSColumnRuleProperty));
            var concrete = (CSSColumnRuleProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("solid blue", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssColumRuleSolidLengthLegal()
        {
            var snippet = "column-rule: solid 8px";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-rule", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSColumnRuleProperty));
            var concrete = (CSSColumnRuleProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("solid 8px", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssColumRuleThickInsetBlueLegal()
        {
            var snippet = "column-rule: thick inset blue";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-rule", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSColumnRuleProperty));
            var concrete = (CSSColumnRuleProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("thick inset blue", concrete.Value.CssText);
        }
    }
}
