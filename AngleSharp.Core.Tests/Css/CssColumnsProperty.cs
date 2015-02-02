using AngleSharp.Dom.Css;
using AngleSharp.Parser.Css;
using NUnit.Framework;

namespace AngleSharp.Core.Tests.Css
{
    [TestFixture]
    public class CssColumnsPropertyTests
    {
        [Test]
        public void CssColumnWidthLengthLegal()
        {
            var snippet = "column-width: 300px";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnWidthProperty>(property);
            var concrete = (CssColumnWidthProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("300px", concrete.Value.CssText);
        }

        [Test]
        public void CssColumnWidthPercentIllegal()
        {
            var snippet = "column-width: 30%";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnWidthProperty>(property);
            var concrete = (CssColumnWidthProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssColumnWidthVwLegal()
        {
            var snippet = "column-width: 0.3vw";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnWidthProperty>(property);
            var concrete = (CssColumnWidthProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0.3vw", concrete.Value.CssText);
        }

        [Test]
        public void CssColumnWidthAutoUppercaseLegal()
        {
            var snippet = "column-width: AUTO";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnWidthProperty>(property);
            var concrete = (CssColumnWidthProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("auto", concrete.Value.CssText);
        }

        [Test]
        public void CssColumnCountAutoLowercaseLegal()
        {
            var snippet = "column-count: auto";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-count", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnCountProperty>(property);
            var concrete = (CssColumnCountProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("auto", concrete.Value.CssText);
        }

        [Test]
        public void CssColumnCountNumberLegal()
        {
            var snippet = "column-count: 3";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-count", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnCountProperty>(property);
            var concrete = (CssColumnCountProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("3", concrete.Value.CssText);
        }

        [Test]
        public void CssColumnCountZeroLegal()
        {
            var snippet = "column-count: 0";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-count", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnCountProperty>(property);
            var concrete = (CssColumnCountProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0", concrete.Value.CssText);
        }

        [Test]
        public void CssColumsZeroLegal()
        {
            var snippet = "columns: 0";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("columns", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnsProperty>(property);
            var concrete = (CssColumnsProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0", concrete.Value.CssText);
        }

        [Test]
        public void CssColumsLengthLegal()
        {
            var snippet = "columns: 10px";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("columns", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnsProperty>(property);
            var concrete = (CssColumnsProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("10px", concrete.Value.CssText);
        }

        [Test]
        public void CssColumsNumberLegal()
        {
            var snippet = "columns: 4";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("columns", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnsProperty>(property);
            var concrete = (CssColumnsProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("4", concrete.Value.CssText);
        }

        [Test]
        public void CssColumsLengthNumberLegal()
        {
            var snippet = "columns: 25em 5";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("columns", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnsProperty>(property);
            var concrete = (CssColumnsProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("25em 5", concrete.Value.CssText);
        }

        [Test]
        public void CssColumsNumberLengthLegal()
        {
            var snippet = "columns : 5   25em  ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("columns", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnsProperty>(property);
            var concrete = (CssColumnsProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("5 25em", concrete.Value.CssText);
        }

        [Test]
        public void CssColumsAutoAutoLegal()
        {
            var snippet = "columns : auto auto";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("columns", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnsProperty>(property);
            var concrete = (CssColumnsProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("auto auto", concrete.Value.CssText);
        }

        [Test]
        public void CssColumsAutoLegal()
        {
            var snippet = "columns : auto  ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("columns", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnsProperty>(property);
            var concrete = (CssColumnsProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("auto", concrete.Value.CssText);
        }

        [Test]
        public void CssColumsNumberPercenIllegal()
        {
            var snippet = "columns : 5   25%  ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("columns", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnsProperty>(property);
            var concrete = (CssColumnsProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssColumSpanAllLegal()
        {
            var snippet = "column-span: all";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-span", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnSpanProperty>(property);
            var concrete = (CssColumnSpanProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("all", concrete.Value.CssText);
        }

        [Test]
        public void CssColumSpanNoneUppercaseLegal()
        {
            var snippet = "column-span: None";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-span", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnSpanProperty>(property);
            var concrete = (CssColumnSpanProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("none", concrete.Value.CssText);
        }

        [Test]
        public void CssColumSpanLengthIllegal()
        {
            var snippet = "column-span: 10px";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-span", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnSpanProperty>(property);
            var concrete = (CssColumnSpanProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssColumGapLengthLegal()
        {
            var snippet = "column-gap: 20px";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-gap", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnGapProperty>(property);
            var concrete = (CssColumnGapProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("20px", concrete.Value.CssText);
        }

        [Test]
        public void CssColumGapNormalLegal()
        {
            var snippet = "column-gap: normal";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-gap", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnGapProperty>(property);
            var concrete = (CssColumnGapProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("normal", concrete.Value.CssText);
        }

        [Test]
        public void CssColumGapZeroLegal()
        {
            var snippet = "column-gap: 0";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-gap", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnGapProperty>(property);
            var concrete = (CssColumnGapProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0", concrete.Value.CssText);
        }

        [Test]
        public void CssColumGapPercentIllegal()
        {
            var snippet = "column-gap: 20%";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-gap", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnGapProperty>(property);
            var concrete = (CssColumnGapProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssColumFillBalanceLegal()
        {
            var snippet = "column-fill: balance;";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-fill", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnFillProperty>(property);
            var concrete = (CssColumnFillProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("balance", concrete.Value.CssText);
        }

        [Test]
        public void CssColumFillAutoLegal()
        {
            var snippet = "column-fill: auto;";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-fill", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnFillProperty>(property);
            var concrete = (CssColumnFillProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("auto", concrete.Value.CssText);
        }

        [Test]
        public void CssColumRuleColorTransparentLegal()
        {
            var snippet = "column-rule-color: transparent";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-rule-color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnRuleColorProperty>(property);
            var concrete = (CssColumnRuleColorProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("transparent", concrete.Value.CssText);
        }

        [Test]
        public void CssColumRuleColorRgbLegal()
        {
            var snippet = "column-rule-color: rgb(192, 56, 78)";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-rule-color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnRuleColorProperty>(property);
            var concrete = (CssColumnRuleColorProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("rgb(192, 56, 78)", concrete.Value.CssText);
        }

        [Test]
        public void CssColumRuleColorRedLegal()
        {
            var snippet = "column-rule-color: red";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-rule-color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnRuleColorProperty>(property);
            var concrete = (CssColumnRuleColorProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("red", concrete.Value.CssText);
        }

        [Test]
        public void CssColumRuleColorNoneIllegal()
        {
            var snippet = "column-rule-color: none";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-rule-color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnRuleColorProperty>(property);
            var concrete = (CssColumnRuleColorProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssColumRuleStyleInsetTailUpperLegal()
        {
            var snippet = "column-rule-style: inSET";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-rule-style", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnRuleStyleProperty>(property);
            var concrete = (CssColumnRuleStyleProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("inset", concrete.Value.CssText);
        }

        [Test]
        public void CssColumRuleStyleNoneLegal()
        {
            var snippet = "column-rule-style: none";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-rule-style", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnRuleStyleProperty>(property);
            var concrete = (CssColumnRuleStyleProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("none", concrete.Value.CssText);
        }

        [Test]
        public void CssColumRuleStyleAutoIllegal()
        {
            var snippet = "column-rule-style: auto ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-rule-style", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnRuleStyleProperty>(property);
            var concrete = (CssColumnRuleStyleProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssColumRuleWidthLengthLegal()
        {
            var snippet = "column-rule-width: 2px";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-rule-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnRuleWidthProperty>(property);
            var concrete = (CssColumnRuleWidthProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("2px", concrete.Value.CssText);
        }

        [Test]
        public void CssColumRuleWidthThickLegal()
        {
            var snippet = "column-rule-width: thick";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-rule-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnRuleWidthProperty>(property);
            var concrete = (CssColumnRuleWidthProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("thick", concrete.Value.CssText);
        }

        [Test]
        public void CssColumRuleWidthMediumLegal()
        {
            var snippet = "column-rule-width : medium !important ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-rule-width", property.Name);
            Assert.IsTrue(property.IsImportant);
            Assert.IsInstanceOf<CssColumnRuleWidthProperty>(property);
            var concrete = (CssColumnRuleWidthProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("medium", concrete.Value.CssText);
        }

        [Test]
        public void CssColumRuleWidthThinUppercaseLegal()
        {
            var snippet = "column-rule-width: THIN";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-rule-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnRuleWidthProperty>(property);
            var concrete = (CssColumnRuleWidthProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("thin", concrete.Value.CssText);
        }

        [Test]
        public void CssColumRuleDottedLegal()
        {
            var snippet = "column-rule: dotted";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-rule", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnRuleProperty>(property);
            var concrete = (CssColumnRuleProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("dotted", concrete.Value.CssText);
        }

        [Test]
        public void CssColumRuleSolidBlueLegal()
        {
            var snippet = "column-rule: solid  blue";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-rule", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnRuleProperty>(property);
            var concrete = (CssColumnRuleProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("solid blue", concrete.Value.CssText);
        }

        [Test]
        public void CssColumRuleSolidLengthLegal()
        {
            var snippet = "column-rule: solid 8px";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-rule", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnRuleProperty>(property);
            var concrete = (CssColumnRuleProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("solid 8px", concrete.Value.CssText);
        }

        [Test]
        public void CssColumRuleThickInsetBlueLegal()
        {
            var snippet = "column-rule: thick inset blue";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-rule", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColumnRuleProperty>(property);
            var concrete = (CssColumnRuleProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("thick inset blue", concrete.Value.CssText);
        }
    }
}
