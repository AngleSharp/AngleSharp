using AngleSharp;
using AngleSharp.DOM.Css;
using AngleSharp.DOM.Css.Properties;
using AngleSharp.Parser.Css;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.Css
{
    [TestClass]
    public class CssPropertyTests
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
            var value = concrete.Value;
            Assert.AreEqual("avoid", value.CssText);
            Assert.IsInstanceOfType(value, typeof(CSSIdentifierValue));
        }

        [TestMethod]
        public void CssPageBreakAfterLegalAvoid()
        {
            var snippet = "page-break-after:avoid";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("page-break-after", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSPageBreakAfterProperty));
            var concrete = (CSSPageBreakAfterProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            var value = concrete.Value;
            Assert.AreEqual("avoid", value.CssText);
            Assert.IsInstanceOfType(value, typeof(CSSIdentifierValue));
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
            var value = concrete.Value;
            Assert.AreEqual("Page", value.CssText);
            Assert.IsInstanceOfType(value, typeof(CSSIdentifierValue));
        }

        [TestMethod]
        public void CssPageBreakAfterIllegalAvoidColumn()
        {
            var snippet = "page-break-after:avoid-column";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("page-break-after", property.Name);
            Assert.IsFalse(property.HasValue);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSPageBreakAfterProperty));
            var concrete = (CSSPageBreakAfterProperty)property;
            Assert.AreEqual(CssValueType.Inherit, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
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
            var value = concrete.Value;
            Assert.AreEqual("avoid-column", value.CssText);
            Assert.IsInstanceOfType(value, typeof(CSSIdentifierValue));
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
            var value = concrete.Value;
            Assert.AreEqual("AUTO", value.CssText);
            Assert.IsInstanceOfType(value, typeof(CSSIdentifierValue));
        }

        [TestMethod]
        public void CssPageBreakBeforeLegalAvoid()
        {
            var snippet = "page-break-before:AUTO";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("page-break-before", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSPageBreakBeforeProperty));
            var concrete = (CSSPageBreakBeforeProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            var value = concrete.Value;
            Assert.AreEqual("AUTO", value.CssText);
            Assert.IsInstanceOfType(value, typeof(CSSIdentifierValue));
        }

        [TestMethod]
        public void CssPageBreakBeforeLegalLeft()
        {
            var snippet = "page-break-before:left";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("page-break-before", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSPageBreakBeforeProperty));
            var concrete = (CSSPageBreakBeforeProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            var value = concrete.Value;
            Assert.AreEqual("left", value.CssText);
            Assert.IsInstanceOfType(value, typeof(CSSIdentifierValue));
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
            var value = concrete.Value;
            Assert.AreEqual("avoid-REGION", value.CssText);
            Assert.IsInstanceOfType(value, typeof(CSSIdentifierValue));
        }

        [TestMethod]
        public void CssPageBreakInsideLegalAvoid()
        {
            var snippet = "page-break-inside:avoid";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("page-break-inside", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSPageBreakInsideProperty));
            var concrete = (CSSPageBreakInsideProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            var value = concrete.Value;
            Assert.AreEqual("avoid", value.CssText);
            Assert.IsInstanceOfType(value, typeof(CSSIdentifierValue));
        }

        [TestMethod]
        public void CssPageBreakInsideLegalAutoUppercase()
        {
            var snippet = "page-break-inside:AUTO";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("page-break-inside", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSPageBreakInsideProperty));
            var concrete = (CSSPageBreakInsideProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            var value = concrete.Value;
            Assert.AreEqual("AUTO", value.CssText);
            Assert.IsInstanceOfType(value, typeof(CSSIdentifierValue));
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
            var value = concrete.Value;
            Assert.AreEqual("left", value.CssText);
            Assert.IsInstanceOfType(value, typeof(CSSIdentifierValue));
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
            var value = concrete.Value;
            Assert.AreEqual("both", value.CssText);
            Assert.IsInstanceOfType(value, typeof(CSSIdentifierValue));
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
            var value = concrete.Value;
            Assert.AreEqual("absolute", value.CssText);
            Assert.IsInstanceOfType(value, typeof(CSSIdentifierValue));
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
            var value = concrete.Value;
            Assert.AreEqual("block", value.CssText);
            Assert.IsInstanceOfType(value, typeof(CSSIdentifierValue));
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
            var value = concrete.Value;
            Assert.AreEqual("collapse", value.CssText);
            Assert.IsInstanceOfType(value, typeof(CSSIdentifierValue));
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
            var value = concrete.Value;
            Assert.AreEqual("HIDDEN", value.CssText);
            Assert.IsInstanceOfType(value, typeof(CSSIdentifierValue));
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
            var value = concrete.Value;
            Assert.AreEqual("auto", value.CssText);
            Assert.IsInstanceOfType(value, typeof(CSSIdentifierValue));
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
            var value = concrete.Value;
            Assert.AreEqual("fiXed", value.CssText);
            Assert.IsInstanceOfType(value, typeof(CSSIdentifierValue));
        }

        [TestMethod]
        public void CssBoxShadowOffsetLegal()
        {
            var snippet = "box-shadow:  5px 4px";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("box-shadow", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSBoxShadowProperty));
            var concrete = (CSSBoxShadowProperty)property;
            Assert.AreEqual(CssValueType.ValueList, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("5px 4px", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssBoxShadowInsetOffsetLegal()
        {
            var snippet = "box-shadow: inset 5px 4px";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("box-shadow", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSBoxShadowProperty));
            var concrete = (CSSBoxShadowProperty)property;
            Assert.AreEqual(CssValueType.ValueList, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("inset 5px 4px", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssBoxShadowOffsetColorLegal()
        {
            var snippet = "box-shadow:  5px 4px #000";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("box-shadow", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSBoxShadowProperty));
            var concrete = (CSSBoxShadowProperty)property;
            Assert.AreEqual(CssValueType.ValueList, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("5px 4px rgba(0, 0, 0, 1)", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssBoxShadowOffsetBlurColorLegal()
        {
            var snippet = "box-shadow:  5px 4px 2px #000";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("box-shadow", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSBoxShadowProperty));
            var concrete = (CSSBoxShadowProperty)property;
            Assert.AreEqual(CssValueType.ValueList, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("5px 4px 2px rgba(0, 0, 0, 1)", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssBoxShadowOffsetIllegal()
        {
            var snippet = "box-shadow:  5px 4px 2px 1px 3px #f00";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("box-shadow", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSBoxShadowProperty));
            var concrete = (CSSBoxShadowProperty)property;
            Assert.AreEqual(CssValueType.Inherit, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [TestMethod]
        public void CssClipShapeLegal()
        {
            var snippet = "clip: rect( 2px, 3em, 1in, 0cm )";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("clip", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSClipProperty));
            var concrete = (CSSClipProperty)property;
            Assert.AreEqual(CssValueType.Custom, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("rect(2px, 3em, 1in, 0)", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssClipShapeBackwards()
        {
            var snippet = "clip: rect( 2px 3em 1in 0cm )";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("clip", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSClipProperty));
            var concrete = (CSSClipProperty)property;
            Assert.AreEqual(CssValueType.Custom, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("rect(2px, 3em, 1in, 0)", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssClipShapeZerosLegal()
        {
            var snippet = "clip: rect(0, 0, 0, 0)";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("clip", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSClipProperty));
            var concrete = (CSSClipProperty)property;
            Assert.AreEqual(CssValueType.Custom, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("rect(0, 0, 0, 0)", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssClipShapeZerosIllegal()
        {
            var snippet = "clip: rect(0, 0, 0 0)";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("clip", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSClipProperty));
            var concrete = (CSSClipProperty)property;
            Assert.AreEqual(CssValueType.Inherit, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [TestMethod]
        public void CssClipShapeNonZerosIllegal()
        {
            var snippet = "clip: rect(2px, 1cm, 5mm)";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("clip", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSClipProperty));
            var concrete = (CSSClipProperty)property;
            Assert.AreEqual(CssValueType.Inherit, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [TestMethod]
        public void CssClipShapeSingleValueIllegal()
        {
            var snippet = "clip: rect(1em)";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("clip", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSClipProperty));
            var concrete = (CSSClipProperty)property;
            Assert.AreEqual(CssValueType.Inherit, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [TestMethod]
        public void CssCursorDefaultUppercaseLegal()
        {
            var snippet = "cursor: DEFAULT";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("cursor", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSCursorProperty));
            var concrete = (CSSCursorProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("DEFAULT", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssCursorAutoLegal()
        {
            var snippet = "cursor: auto";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("cursor", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSCursorProperty));
            var concrete = (CSSCursorProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("auto", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssCursorZoomOutLegal()
        {
            var snippet = "cursor  : zoom-out";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("cursor", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSCursorProperty));
            var concrete = (CSSCursorProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("zoom-out", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssCursorUrlLegal()
        {
            var snippet = "cursor  : url(foo.png)";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("cursor", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSCursorProperty));
            var concrete = (CSSCursorProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("url('foo.png')", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssCursorUrlShiftedLegal()
        {
            var snippet = "cursor  : url(foo.png) 0 5";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("cursor", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSCursorProperty));
            var concrete = (CSSCursorProperty)property;
            Assert.AreEqual(CssValueType.ValueList, concrete.Value.CssValueType);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("url('foo.png') 0 5", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssCursorUrlsLegal()
        {
            var snippet = "cursor  : url(foo.png), url(master.png), url(more.png)";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("cursor", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSCursorProperty));
            var concrete = (CSSCursorProperty)property;
            Assert.AreEqual(CssValueType.ValueList, concrete.Value.CssValueType);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("url('foo.png'), url('master.png'), url('more.png')", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssColorHexLegal()
        {
            var snippet = "color : #123456";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("color", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSColorProperty));
            var concrete = (CSSColorProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("rgba(18, 52, 86, 1)", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssColorRgbLegal()
        {
            var snippet = "color : rgb(121, 181, 201)";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("color", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSColorProperty));
            var concrete = (CSSColorProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("rgba(121, 181, 201, 1)", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssColorRgbaLegal()
        {
            var snippet = "color : rgba(255, 255, 201, 0.7)";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("color", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSColorProperty));
            var concrete = (CSSColorProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("rgba(255, 255, 201, 0.7)", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssColorNameLegal()
        {
            var snippet = "color : red";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("color", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSColorProperty));
            var concrete = (CSSColorProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("red", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssColorNameUppercaseLegal()
        {
            var snippet = "color : BLUE";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("color", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSColorProperty));
            var concrete = (CSSColorProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("BLUE", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssColorNameIllegal()
        {
            var snippet = "color : horse";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("color", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSColorProperty));
            var concrete = (CSSColorProperty)property;
            Assert.AreEqual(CssValueType.Inherit, concrete.Value.CssValueType);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [TestMethod]
        public void CssOrphansZeroLegal()
        {
            var snippet = "orphans : 0 ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("orphans", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSOrphansProperty));
            var concrete = (CSSOrphansProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssOrphansTwoLegal()
        {
            var snippet = "orphans : 2 ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("orphans", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSOrphansProperty));
            var concrete = (CSSOrphansProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("2", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssOrphansNegativeIllegal()
        {
            var snippet = "orphans : -2 ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("orphans", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSOrphansProperty));
            var concrete = (CSSOrphansProperty)property;
            Assert.AreEqual(CssValueType.Inherit, concrete.Value.CssValueType);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [TestMethod]
        public void CssOrphansFloatingLegal()
        {
            var snippet = "orphans : 1.5 ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("orphans", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSOrphansProperty));
            var concrete = (CSSOrphansProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("1.5", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssContentNormalLegal()
        {
            var snippet = "content : normal ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("content", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSContentProperty));
            var concrete = (CSSContentProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("normal", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssContentNoneLegalUppercaseN()
        {
            var snippet = "content : noNe ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("content", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSContentProperty));
            var concrete = (CSSContentProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("noNe", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssContentStringLegal()
        {
            var snippet = "content : 'hi' ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("content", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSContentProperty));
            var concrete = (CSSContentProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("'hi'", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssContentNoOpenQuoteNoCloseQuoteLegal()
        {
            var snippet = "content : no-open-quote no-close-quote ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("content", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSContentProperty));
            var concrete = (CSSContentProperty)property;
            Assert.AreEqual(CssValueType.ValueList, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("no-open-quote no-close-quote", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssContentUrlLegal()
        {
            var snippet = "content : url(test.html) ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("content", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSContentProperty));
            var concrete = (CSSContentProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("url('test.html')", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssContentStringsLegal()
        {
            var snippet = "content : 'how' 'are' 'you' ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("content", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSContentProperty));
            var concrete = (CSSContentProperty)property;
            Assert.AreEqual(CssValueType.ValueList, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("'how' 'are' 'you'", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssQuoteStringIllegal()
        {
            var snippet = "quotes : '\"' ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("quotes", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSQuotesProperty));
            var concrete = (CSSQuotesProperty)property;
            Assert.AreEqual(CssValueType.Inherit, concrete.Value.CssValueType);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [TestMethod]
        public void CssQuoteStringsLegal()
        {
            var snippet = "quotes : '\"' '\"' ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("quotes", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSQuotesProperty));
            var concrete = (CSSQuotesProperty)property;
            Assert.AreEqual(CssValueType.ValueList, concrete.Value.CssValueType);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("'\"' '\"'", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssQuoteStringsMultipleLegal()
        {
            var snippet = "quotes : '\"' '\"' '`' '´' ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("quotes", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSQuotesProperty));
            var concrete = (CSSQuotesProperty)property;
            Assert.AreEqual(CssValueType.ValueList, concrete.Value.CssValueType);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("'\"' '\"' '`' '´'", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssQuoteNoneLegal()
        {
            var snippet = "quotes : none";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("quotes", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSQuotesProperty));
            var concrete = (CSSQuotesProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("none", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssQuoteNormalIllegal()
        {
            var snippet = "quotes : normal ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("quotes", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSQuotesProperty));
            var concrete = (CSSQuotesProperty)property;
            Assert.AreEqual(CssValueType.Inherit, concrete.Value.CssValueType);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }
    }
}
