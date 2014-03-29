using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AngleSharp.Parser.Css;
using AngleSharp.DOM.Css.Properties;
using AngleSharp.DOM.Css;

namespace UnitTests.Css
{
    [TestClass]
    public class CssProperties
    {
        [TestMethod]
        public void CssBreakAfterLegalAvoid()
        {
            var snippet = "break-after:avoid";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("break-after", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSBreakAfterProperty));
            var concrete = (CSSBreakAfterProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            var value = (CSSPrimitiveValue)concrete.Value;
            Assert.AreEqual("avoid", value.CssText);
            Assert.AreEqual(CssUnit.Ident, value.PrimitiveType);
        }

        [TestMethod]
        public void CssBreakAfterLegalPageCapital()
        {
            var snippet = "break-after:Page";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("break-after", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSBreakAfterProperty));
            var concrete = (CSSBreakAfterProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            var value = (CSSPrimitiveValue)concrete.Value;
            Assert.AreEqual("Page", value.CssText);
            Assert.AreEqual(CssUnit.Ident, value.PrimitiveType);
        }

        [TestMethod]
        public void CssBreakAfterLegalAvoidColumn()
        {
            var snippet = "break-after:avoid-column";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("break-after", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSBreakAfterProperty));
            var concrete = (CSSBreakAfterProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            var value = (CSSPrimitiveValue)concrete.Value;
            Assert.AreEqual("avoid-column", value.CssText);
            Assert.AreEqual(CssUnit.Ident, value.PrimitiveType);
        }

        [TestMethod]
        public void CssBreakBeforeLegalAvoidColumn()
        {
            var snippet = "break-before:AUTO";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("break-before", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSBreakBeforeProperty));
            var concrete = (CSSBreakBeforeProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            var value = (CSSPrimitiveValue)concrete.Value;
            Assert.AreEqual("AUTO", value.CssText);
            Assert.AreEqual(CssUnit.Ident, value.PrimitiveType);
        }

        [TestMethod]
        public void CssBreakBeforeIllegalValue()
        {
            var snippet = "break-before:whatever";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("break-before", property.Name);
            Assert.IsFalse(property.HasValue);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSBreakBeforeProperty));
            var concrete = (CSSBreakBeforeProperty)property;
            Assert.AreEqual(CSSValue.Inherit, concrete.Value);
        }

        [TestMethod]
        public void CssBreakInsideIllegalPage()
        {
            var snippet = "break-inside:page";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("break-inside", property.Name);
            Assert.IsFalse(property.HasValue);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSBreakInsideProperty));
            var concrete = (CSSBreakInsideProperty)property;
            Assert.AreEqual(CSSValue.Inherit, concrete.Value);
        }

        [TestMethod]
        public void CssBreakInsideLegalAvoidRegionUppercase()
        {
            var snippet = "break-inside:avoid-REGION";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("break-inside", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSBreakInsideProperty));
            var concrete = (CSSBreakInsideProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            var value = (CSSPrimitiveValue)concrete.Value;
            Assert.AreEqual("avoid-REGION", value.CssText);
            Assert.AreEqual(CssUnit.Ident, value.PrimitiveType);
        }

        [TestMethod]
        public void CssClearLegalLeft()
        {
            var snippet = "clear:left";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("clear", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSClearProperty));
            var concrete = (CSSClearProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            var value = (CSSPrimitiveValue)concrete.Value;
            Assert.AreEqual("left", value.CssText);
            Assert.AreEqual(CssUnit.Ident, value.PrimitiveType);
        }

        [TestMethod]
        public void CssClearLegalBoth()
        {
            var snippet = "clear:both";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("clear", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSClearProperty));
            var concrete = (CSSClearProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            var value = (CSSPrimitiveValue)concrete.Value;
            Assert.AreEqual("both", value.CssText);
            Assert.AreEqual(CssUnit.Ident, value.PrimitiveType);
        }

        [TestMethod]
        public void CssClearInherited()
        {
            var snippet = "clear:inherit";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("clear", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSClearProperty));
            var concrete = (CSSClearProperty)property;
            Assert.AreEqual(CSSValue.Inherit, concrete.Value);
        }

        [TestMethod]
        public void CssClearIllegal()
        {
            var snippet = "clear:yes";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("clear", property.Name);
            Assert.IsFalse(property.HasValue);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSClearProperty));
            var concrete = (CSSClearProperty)property;
            Assert.AreEqual(CSSValue.Inherit, concrete.Value);
        }

        [TestMethod]
        public void CssHeightLegalPercentage()
        {
            var snippet = "height:   28% ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("height", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSHeightProperty));
            var concrete = (CSSHeightProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            var value = (CSSPrimitiveValue)concrete.Value;
            Assert.AreEqual("28%", value.CssText);
            Assert.AreEqual(CssUnit.Percentage, value.PrimitiveType);
        }

        [TestMethod]
        public void CssHeightLegalLengthInEm()
        {
            var snippet = "height:   0.3em ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("height", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSHeightProperty));
            var concrete = (CSSHeightProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            var value = (CSSPrimitiveValue)concrete.Value;
            Assert.AreEqual("0.3em", value.CssText);
            Assert.AreEqual(CssUnit.Ems, value.PrimitiveType);
        }

        [TestMethod]
        public void CssHeightLegalLengthInPx()
        {
            var snippet = "height:   144px ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("height", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSHeightProperty));
            var concrete = (CSSHeightProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            var value = (CSSPrimitiveValue)concrete.Value;
            Assert.AreEqual("144px", value.CssText);
            Assert.AreEqual(CssUnit.Px, value.PrimitiveType);
        }

        [TestMethod]
        public void CssHeightLegalAutoUppercase()
        {
            var snippet = "height: AUTO ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("height", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSHeightProperty));
            var concrete = (CSSHeightProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            var value = (CSSPrimitiveValue)concrete.Value;
            Assert.AreEqual("AUTO", value.CssText);
            Assert.AreEqual(CssUnit.Ident, value.PrimitiveType);
        }

        [TestMethod]
        public void CssWidthLegalLengthInCm()
        {
            var snippet = "width:0.5cm";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("width", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSWidthProperty));
            var concrete = (CSSWidthProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            var value = (CSSPrimitiveValue)concrete.Value;
            Assert.AreEqual("0.5cm", value.CssText);
            Assert.AreEqual(CssUnit.Cm, value.PrimitiveType);
        }

        [TestMethod]
        public void CssWidthLegalLengthInMm()
        {
            var snippet = "width:1.5mm";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("width", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSWidthProperty));
            var concrete = (CSSWidthProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            var value = (CSSPrimitiveValue)concrete.Value;
            Assert.AreEqual("1.5mm", value.CssText);
            Assert.AreEqual(CssUnit.Mm, value.PrimitiveType);
        }

        [TestMethod]
        public void CssWidthIllegalLength()
        {
            var snippet = "width:1.5 meter";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("width", property.Name);
            Assert.IsFalse(property.HasValue);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSWidthProperty));
            var concrete = (CSSWidthProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.AreEqual(CSSValue.Inherit, concrete.Value);
        }

        [TestMethod]
        public void CssPositionLegalAbsolute()
        {
            var snippet = "position:absolute";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("position", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSPositionProperty));
            var concrete = (CSSPositionProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            var value = (CSSPrimitiveValue)concrete.Value;
            Assert.AreEqual("absolute", value.CssText);
            Assert.AreEqual(CssUnit.Ident, value.PrimitiveType);
        }

        [TestMethod]
        public void CssDisplayLegalBlock()
        {
            var snippet = "display:   block ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("display", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSDisplayProperty));
            var concrete = (CSSDisplayProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            var value = (CSSPrimitiveValue)concrete.Value;
            Assert.AreEqual("block", value.CssText);
            Assert.AreEqual(CssUnit.Ident, value.PrimitiveType);
        }

        [TestMethod]
        public void CssVisibilityLegalCollapse()
        {
            var snippet = "visibility:collapse";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("visibility", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSVisibilityProperty));
            var concrete = (CSSVisibilityProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsTrue(concrete.IsInherited);
            var value = (CSSPrimitiveValue)concrete.Value;
            Assert.AreEqual("collapse", value.CssText);
            Assert.AreEqual(CssUnit.Ident, value.PrimitiveType);
        }

        [TestMethod]
        public void CssVisibilityLegalHiddenCompleteUppercase()
        {
            var snippet = "VISIBILITY:HIDDEN";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("visibility", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSVisibilityProperty));
            var concrete = (CSSVisibilityProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsTrue(concrete.IsInherited);
            var value = (CSSPrimitiveValue)concrete.Value;
            Assert.AreEqual("HIDDEN", value.CssText);
            Assert.AreEqual(CssUnit.Ident, value.PrimitiveType);
        }

        [TestMethod]
        public void CssOverflowLegalAuto()
        {
            var snippet = "overflow:auto";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("overflow", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSOverflowProperty));
            var concrete = (CSSOverflowProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            var value = (CSSPrimitiveValue)concrete.Value;
            Assert.AreEqual("auto", value.CssText);
            Assert.AreEqual(CssUnit.Ident, value.PrimitiveType);
        }

        [TestMethod]
        public void CssTableLayoutLegalFixedCapitalX()
        {
            var snippet = "table-layout: fiXed";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("table-layout", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSTableLayoutProperty));
            var concrete = (CSSTableLayoutProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            var value = (CSSPrimitiveValue)concrete.Value;
            Assert.AreEqual("fiXed", value.CssText);
            Assert.AreEqual(CssUnit.Ident, value.PrimitiveType);
        }

        [TestMethod]
        public void CssTextAlignLegalJustify()
        {
            var snippet = "text-align:justify";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("text-align", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSTextAlignProperty));
            var concrete = (CSSTextAlignProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsTrue(concrete.IsInherited);
            var value = (CSSPrimitiveValue)concrete.Value;
            Assert.AreEqual("justify", value.CssText);
            Assert.AreEqual(CssUnit.Ident, value.PrimitiveType);
        }

        [TestMethod]
        public void CssTextAlignLegalJustifyChangedToLeftAndThenIllegal()
        {
            var snippet = "text-align:justify";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("text-align", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSTextAlignProperty));
            var concrete = (CSSTextAlignProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsTrue(concrete.IsInherited);
            var value = (CSSPrimitiveValue)concrete.Value;
            Assert.AreEqual("justify", value.CssText);
            Assert.AreEqual(CssUnit.Ident, value.PrimitiveType);
            concrete.Value = new CSSIdentifier("left");
            value = (CSSPrimitiveValue)concrete.Value;
            Assert.AreEqual("left", value.CssText);
            Assert.AreEqual(CssUnit.Ident, value.PrimitiveType);
            concrete.Value = new CSSIdentifier("whatever");
            Assert.AreEqual(value, concrete.Value);
        }
    }
}
