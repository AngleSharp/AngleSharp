using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AngleSharp.Parser.Css;
using AngleSharp.DOM.Css.Properties;
using AngleSharp.DOM.Css;

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
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSColumnWidthProperty));
            var concrete = (CSSColumnWidthProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
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
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSColumnWidthProperty));
            var concrete = (CSSColumnWidthProperty)property;
            Assert.AreEqual(CssValueType.Inherit, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [TestMethod]
        public void CssColumnWidthVwLegal()
        {
            var snippet = "column-width: 0.3vw";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-width", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSColumnWidthProperty));
            var concrete = (CSSColumnWidthProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
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
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSColumnWidthProperty));
            var concrete = (CSSColumnWidthProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("AUTO", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssColumnCountAutoLowercaseLegal()
        {
            var snippet = "column-count: auto";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-count", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSColumnCountProperty));
            var concrete = (CSSColumnCountProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
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
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSColumnCountProperty));
            var concrete = (CSSColumnCountProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
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
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSColumnCountProperty));
            var concrete = (CSSColumnCountProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
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
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSColumnsProperty));
            var concrete = (CSSColumnsProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
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
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSColumnsProperty));
            var concrete = (CSSColumnsProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
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
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSColumnsProperty));
            var concrete = (CSSColumnsProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
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
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSColumnsProperty));
            var concrete = (CSSColumnsProperty)property;
            Assert.AreEqual(CssValueType.ValueList, concrete.Value.CssValueType);
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
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSColumnsProperty));
            var concrete = (CSSColumnsProperty)property;
            Assert.AreEqual(CssValueType.ValueList, concrete.Value.CssValueType);
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
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSColumnsProperty));
            var concrete = (CSSColumnsProperty)property;
            Assert.AreEqual(CssValueType.ValueList, concrete.Value.CssValueType);
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
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSColumnsProperty));
            var concrete = (CSSColumnsProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
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
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSColumnsProperty));
            var concrete = (CSSColumnsProperty)property;
            Assert.AreEqual(CssValueType.Inherit, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [TestMethod]
        public void CssColumSpanAllLegal()
        {
            var snippet = "column-span: all";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-span", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSColumnSpanProperty));
            var concrete = (CSSColumnSpanProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
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
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSColumnSpanProperty));
            var concrete = (CSSColumnSpanProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("None", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssColumSpanLengthIllegal()
        {
            var snippet = "column-span: 10px";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-span", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSColumnSpanProperty));
            var concrete = (CSSColumnSpanProperty)property;
            Assert.AreEqual(CssValueType.Inherit, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [TestMethod]
        public void CssColumGapLengthLegal()
        {
            var snippet = "column-gap: 20px";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-gap", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSColumnGapProperty));
            var concrete = (CSSColumnGapProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
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
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSColumnGapProperty));
            var concrete = (CSSColumnGapProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
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
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSColumnGapProperty));
            var concrete = (CSSColumnGapProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
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
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSColumnGapProperty));
            var concrete = (CSSColumnGapProperty)property;
            Assert.AreEqual(CssValueType.Inherit, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [TestMethod]
        public void CssColumFillBalanceLegal()
        {
            var snippet = "column-fill: balance;";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("column-fill", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSColumnFillProperty));
            var concrete = (CSSColumnFillProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
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
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSColumnFillProperty));
            var concrete = (CSSColumnFillProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("auto", concrete.Value.CssText);
        }
    }
}
