namespace AngleSharp.Core.Tests.Css
{
    using AngleSharp.Dom.Css;
    using AngleSharp.Parser.Css;
    using NUnit.Framework;

    [TestFixture]
    public class CssPropertyTests : CssConstructionFunctions
    {
        [Test]
        public void CssBreakAfterLegalAvoid()
        {
            var snippet = "break-after:avoid";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("break-after", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBreakAfterProperty>(property);
            var concrete = (CssBreakAfterProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.AreEqual("avoid", concrete.Value);
        }

        [Test]
        public void CssPageBreakAfterLegalAvoid()
        {
            var snippet = "page-break-after:avoid";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("page-break-after", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssPageBreakAfterProperty>(property);
            var concrete = (CssPageBreakAfterProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.AreEqual("avoid", concrete.Value);
        }

        [Test]
        public void CssBreakAfterLegalPageCapital()
        {
            var snippet = "break-after:Page";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("break-after", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBreakAfterProperty>(property);
            var concrete = (CssBreakAfterProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.AreEqual("page", concrete.Value);
        }

        [Test]
        public void CssPageBreakAfterIllegalAvoidColumn()
        {
            var snippet = "page-break-after:avoid-column";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("page-break-after", property.Name);
            Assert.IsFalse(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssPageBreakAfterProperty>(property);
            var concrete = (CssPageBreakAfterProperty)property;
            Assert.IsFalse(concrete.IsInherited);
        }

        [Test]
        public void CssBreakAfterLegalAvoidColumn()
        {
            var snippet = "break-after:avoid-column";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("break-after", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBreakAfterProperty>(property);
            var concrete = (CssBreakAfterProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.AreEqual("avoid-column", concrete.Value);
        }

        [Test]
        public void CssBreakBeforeLegalAvoidColumn()
        {
            var snippet = "break-before:AUTO";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("break-before", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBreakBeforeProperty>(property);
            var concrete = (CssBreakBeforeProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.AreEqual("auto", concrete.Value);
        }

        [Test]
        public void CssPageBreakBeforeLegalAvoid()
        {
            var snippet = "page-break-before:AUTO";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("page-break-before", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssPageBreakBeforeProperty>(property);
            var concrete = (CssPageBreakBeforeProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.AreEqual("auto", concrete.Value);
        }

        [Test]
        public void CssPageBreakBeforeLegalLeft()
        {
            var snippet = "page-break-before:left";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("page-break-before", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssPageBreakBeforeProperty>(property);
            var concrete = (CssPageBreakBeforeProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.AreEqual("left", concrete.Value);
        }

        [Test]
        public void CssBreakBeforeIllegalValue()
        {
            var snippet = "break-before:whatever";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("break-before", property.Name);
            Assert.IsFalse(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBreakBeforeProperty>(property);
            var concrete = (CssBreakBeforeProperty)property;
            Assert.IsNotNull(concrete);
        }

        [Test]
        public void CssBreakInsideIllegalPage()
        {
            var snippet = "break-inside:page";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("break-inside", property.Name);
            Assert.IsFalse(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBreakInsideProperty>(property);
            var concrete = (CssBreakInsideProperty)property;
            Assert.IsNotNull(concrete);
        }

        [Test]
        public void CssBreakInsideLegalAvoidRegionUppercase()
        {
            var snippet = "break-inside:avoid-REGION";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("break-inside", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBreakInsideProperty>(property);
            var concrete = (CssBreakInsideProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.AreEqual("avoid-region", concrete.Value);
        }

        [Test]
        public void CssPageBreakInsideLegalAvoid()
        {
            var snippet = "page-break-inside:avoid";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("page-break-inside", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssPageBreakInsideProperty>(property);
            var concrete = (CssPageBreakInsideProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.AreEqual("avoid", concrete.Value);
        }

        [Test]
        public void CssPageBreakInsideLegalAutoUppercase()
        {
            var snippet = "page-break-inside:AUTO";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("page-break-inside", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssPageBreakInsideProperty>(property);
            var concrete = (CssPageBreakInsideProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.AreEqual("auto", concrete.Value);
        }

        [Test]
        public void CssClearLegalLeft()
        {
            var snippet = "clear:left";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("clear", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssClearProperty>(property);
            var concrete = (CssClearProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.AreEqual("left", concrete.Value);
        }

        [Test]
        public void CssClearLegalBoth()
        {
            var snippet = "clear:both";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("clear", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssClearProperty>(property);
            var concrete = (CssClearProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.AreEqual("both", concrete.Value);
        }

        [Test]
        public void CssClearInherited()
        {
            var snippet = "clear:inherit";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("clear", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsTrue(property.IsInherited);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssClearProperty>(property);
            var concrete = (CssClearProperty)property;
            Assert.IsNotNull(concrete);
        }

        [Test]
        public void CssClearIllegal()
        {
            var snippet = "clear:yes";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("clear", property.Name);
            Assert.IsFalse(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssClearProperty>(property);
            var concrete = (CssClearProperty)property;
            Assert.IsNotNull(concrete);
        }

        [Test]
        public void CssPositionLegalAbsolute()
        {
            var snippet = "position:absolute";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("position", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssPositionProperty>(property);
            var concrete = (CssPositionProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.AreEqual("absolute", concrete.Value);
        }

        [Test]
        public void CssDisplayLegalBlock()
        {
            var snippet = "display:   block ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("display", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssDisplayProperty>(property);
            var concrete = (CssDisplayProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.AreEqual("block", concrete.Value);
        }

        [Test]
        public void CssVisibilityLegalCollapse()
        {
            var snippet = "visibility:collapse";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("visibility", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssVisibilityProperty>(property);
            var concrete = (CssVisibilityProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.AreEqual("collapse", concrete.Value);
        }

        [Test]
        public void CssVisibilityLegalHiddenCompleteUppercase()
        {
            var snippet = "VISIBILITY:HIDDEN";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("visibility", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssVisibilityProperty>(property);
            var concrete = (CssVisibilityProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.AreEqual("hidden", concrete.Value);
        }

        [Test]
        public void CssOverflowLegalAuto()
        {
            var snippet = "overflow:auto";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("overflow", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssOverflowProperty>(property);
            var concrete = (CssOverflowProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.AreEqual("auto", concrete.Value);
        }

        [Test]
        public void CssTableLayoutLegalFixedCapitalX()
        {
            var snippet = "table-layout: fiXed";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("table-layout", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssTableLayoutProperty>(property);
            var concrete = (CssTableLayoutProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.AreEqual("fixed", concrete.Value);
        }

        [Test]
        public void CssBoxShadowOffsetLegal()
        {
            var snippet = "box-shadow:  5px 4px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("box-shadow", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBoxShadowProperty>(property);
            var concrete = (CssBoxShadowProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("5px 4px", concrete.Value);
        }

        [Test]
        public void CssBoxShadowInsetOffsetLegal()
        {
            var snippet = "box-shadow: inset 5px 4px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("box-shadow", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBoxShadowProperty>(property);
            var concrete = (CssBoxShadowProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("inset 5px 4px", concrete.Value);
        }

        [Test]
        public void CssBoxShadowNoneUppercaseLegal()
        {
            var snippet = "box-shadow: NONE";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("box-shadow", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBoxShadowProperty>(property);
            var concrete = (CssBoxShadowProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("none", concrete.Value);
        }

        [Test]
        public void CssBoxShadowNormalTealLegal()
        {
            var snippet = "box-shadow: 60px -16px teal";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("box-shadow", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBoxShadowProperty>(property);
            var concrete = (CssBoxShadowProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("60px -16px rgb(0, 128, 128)", concrete.Value);
        }

        [Test]
        public void CssBoxShadowNormalSpreadBlackLegal()
        {
            var snippet = "box-shadow: 10px 5px 5px black";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("box-shadow", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBoxShadowProperty>(property);
            var concrete = (CssBoxShadowProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("10px 5px 5px rgb(0, 0, 0)", concrete.Value);
        }

        [Test]
        public void CssBoxShadowOliveAndRedLegal()
        {
            var snippet = "box-shadow: 3px 3px red, -1em 0 0.4em olive";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("box-shadow", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBoxShadowProperty>(property);
            var concrete = (CssBoxShadowProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("3px 3px rgb(255, 0, 0), -1em 0 0.4em rgb(128, 128, 0)", concrete.Value);
        }

        [Test]
        public void CssBoxShadowInsetGoldLegal()
        {
            var snippet = "box-shadow: inset 5em 1em gold";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("box-shadow", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBoxShadowProperty>(property);
            var concrete = (CssBoxShadowProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("inset 5em 1em rgb(255, 215, 0)", concrete.Value);
        }

        [Test]
        public void CssBoxShadowZeroGoldLegal()
        {
            var snippet = "box-shadow: 0 0 1em gold";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("box-shadow", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBoxShadowProperty>(property);
            var concrete = (CssBoxShadowProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0 0 1em rgb(255, 215, 0)", concrete.Value);
        }

        [Test]
        public void CssBoxShadowInsetZeroGoldLegal()
        {
            var snippet = "box-shadow: inset  0 0 1em gold";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("box-shadow", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBoxShadowProperty>(property);
            var concrete = (CssBoxShadowProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("inset 0 0 1em rgb(255, 215, 0)", concrete.Value);
        }

        [Test]
        public void CssBoxShadowInsetZeroGoldAndNormalRedLegal()
        {
            var snippet = "box-shadow: inset  0 0 1em  gold   ,  0 0   1em   red !important";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("box-shadow", property.Name);
            Assert.IsTrue(property.IsImportant);
            Assert.IsInstanceOf<CssBoxShadowProperty>(property);
            var concrete = (CssBoxShadowProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("inset 0 0 1em rgb(255, 215, 0), 0 0 1em rgb(255, 0, 0)", concrete.Value);
        }

        [Test]
        public void CssBoxShadowOffsetColorLegal()
        {
            var snippet = "box-shadow:  5px 4px #000";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("box-shadow", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBoxShadowProperty>(property);
            var concrete = (CssBoxShadowProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("5px 4px rgb(0, 0, 0)", concrete.Value);
        }

        [Test]
        public void CssBoxShadowOffsetBlurColorLegal()
        {
            var snippet = "box-shadow:  5px 4px 2px #000";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("box-shadow", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBoxShadowProperty>(property);
            var concrete = (CssBoxShadowProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("5px 4px 2px rgb(0, 0, 0)", concrete.Value);
        }

        [Test]
        public void CssBoxShadowInitialUppercaseLegal()
        {
            var snippet = "box-shadow:  INITIAL";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("box-shadow", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBoxShadowProperty>(property);
            var concrete = (CssBoxShadowProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("initial", concrete.Value);
        }

        [Test]
        public void CssBoxShadowOffsetIllegal()
        {
            var snippet = "box-shadow:  5px 4px 2px 1px 3px #f00";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("box-shadow", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBoxShadowProperty>(property);
            var concrete = (CssBoxShadowProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssClipShapeLegal()
        {
            var snippet = "clip: rect( 2px, 3em, 1in, 0cm )";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("clip", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssClipProperty>(property);
            var concrete = (CssClipProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("rect(2px, 3em, 1in, 0)", concrete.Value);
        }

        [Test]
        public void CssClipShapeBackwards()
        {
            var snippet = "clip: rect( 2px 3em 1in 0cm )";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("clip", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssClipProperty>(property);
            var concrete = (CssClipProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("rect(2px 3em 1in 0)", concrete.Value);
        }

        [Test]
        public void CssClipShapeZerosLegal()
        {
            var snippet = "clip: rect(0, 0, 0, 0)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("clip", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssClipProperty>(property);
            var concrete = (CssClipProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("rect(0, 0, 0, 0)", concrete.Value);
        }

        [Test]
        public void CssClipShapeZerosIllegal()
        {
            var snippet = "clip: rect(0, 0, 0 0)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("clip", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssClipProperty>(property);
            var concrete = (CssClipProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssClipShapeNonZerosIllegal()
        {
            var snippet = "clip: rect(2px, 1cm, 5mm)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("clip", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssClipProperty>(property);
            var concrete = (CssClipProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssClipShapeSingleValueIllegal()
        {
            var snippet = "clip: rect(1em)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("clip", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssClipProperty>(property);
            var concrete = (CssClipProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssCursorDefaultUppercaseLegal()
        {
            var snippet = "cursor: DEFAULT";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("cursor", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssCursorProperty>(property);
            var concrete = (CssCursorProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("default", concrete.Value);
        }

        [Test]
        public void CssCursorAutoLegal()
        {
            var snippet = "cursor: auto";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("cursor", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssCursorProperty>(property);
            var concrete = (CssCursorProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("auto", concrete.Value);
        }

        [Test]
        public void CssCursorZoomOutLegal()
        {
            var snippet = "cursor  : zoom-out";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("cursor", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssCursorProperty>(property);
            var concrete = (CssCursorProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("zoom-out", concrete.Value);
        }

        [Test]
        public void CssCursorUrlNoFallbackIllegal()
        {
            var snippet = "cursor  : url(foo.png)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("cursor", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssCursorProperty>(property);
            var concrete = (CssCursorProperty)property;
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssCursorUrlLegal()
        {
            var snippet = "cursor  : url(foo.png), default";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("cursor", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssCursorProperty>(property);
            var concrete = (CssCursorProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("url(\"foo.png\"), default", concrete.Value);
        }

        [Test]
        public void CssCursorUrlShiftedLegal()
        {
            var snippet = "cursor  : url(foo.png) 0 5, auto";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("cursor", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssCursorProperty>(property);
            var concrete = (CssCursorProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("url(\"foo.png\") 0 5, auto", concrete.Value);
        }

        [Test]
        public void CssCursorUrlShiftedNoFallbackIllegal()
        {
            var snippet = "cursor  : url(foo.png) 0 5";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("cursor", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssCursorProperty>(property);
            var concrete = (CssCursorProperty)property;
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssCursorUrlsLegal()
        {
            var snippet = "cursor  : url(foo.png), url(master.png), url(more.png), wait";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("cursor", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssCursorProperty>(property);
            var concrete = (CssCursorProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("url(\"foo.png\"), url(\"master.png\"), url(\"more.png\"), wait", concrete.Value);
        }

        [Test]
        public void CssColorHexLegal()
        {
            var snippet = "color : #123456";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColorProperty>(property);
            var concrete = (CssColorProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("rgb(18, 52, 86)", concrete.Value);
        }

        [Test]
        public void CssColorRgbLegal()
        {
            var snippet = "color : rgb(121, 181, 201)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColorProperty>(property);
            var concrete = (CssColorProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("rgb(121, 181, 201)", concrete.Value);
        }

        [Test]
        public void CssColorRgbaLegal()
        {
            var snippet = "color : rgba(255, 255, 201, 0.7)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColorProperty>(property);
            var concrete = (CssColorProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("rgba(255, 255, 201, 0.7)", concrete.Value);
        }

        [Test]
        public void CssColorNameLegal()
        {
            var snippet = "color : red";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColorProperty>(property);
            var concrete = (CssColorProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("rgb(255, 0, 0)", concrete.Value);
        }

        [Test]
        public void CssColorNameUppercaseLegal()
        {
            var snippet = "color : BLUE";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColorProperty>(property);
            var concrete = (CssColorProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("rgb(0, 0, 255)", concrete.Value);
        }

        [Test]
        public void CssColorNameIllegal()
        {
            var snippet = "color : horse";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssColorProperty>(property);
            var concrete = (CssColorProperty)property;
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssOrphansZeroLegal()
        {
            var snippet = "orphans : 0 ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("orphans", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssOrphansProperty>(property);
            var concrete = (CssOrphansProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0", concrete.Value);
        }

        [Test]
        public void CssOrphansTwoLegal()
        {
            var snippet = "orphans : 2 ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("orphans", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssOrphansProperty>(property);
            var concrete = (CssOrphansProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("2", concrete.Value);
        }

        [Test]
        public void CssOrphansNegativeIllegal()
        {
            var snippet = "orphans : -2 ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("orphans", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssOrphansProperty>(property);
            var concrete = (CssOrphansProperty)property;
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssOrphansFloatingIllegal()
        {
            var snippet = "orphans : 1.5 ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("orphans", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssOrphansProperty>(property);
            var concrete = (CssOrphansProperty)property;
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssBoxDecorationBreakNumberIllegal()
        {
            var snippet = "box-decoration-break : 1.5 ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("box-decoration-break", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBoxDecorationBreak>(property);
            var concrete = (CssBoxDecorationBreak)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssBoxDecorationBreakSliceLegal()
        {
            var snippet = "box-decoration-break : slice ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("box-decoration-break", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBoxDecorationBreak>(property);
            var concrete = (CssBoxDecorationBreak)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("slice", concrete.Value);
        }

        [Test]
        public void CssBoxDecorationBreakClonePascalLegal()
        {
            var snippet = "box-decoration-break : Clone ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("box-decoration-break", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBoxDecorationBreak>(property);
            var concrete = (CssBoxDecorationBreak)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("clone", concrete.Value);
        }

        [Test]
        public void CssBoxDecorationBreakInheritLegal()
        {
            var snippet = "box-decoration-break : inherit!important ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("box-decoration-break", property.Name);
            Assert.IsTrue(property.IsImportant);
            Assert.IsInstanceOf<CssBoxDecorationBreak>(property);
            var concrete = (CssBoxDecorationBreak)property;
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("inherit", concrete.Value);
        }

        [Test]
        public void CssContentNormalLegal()
        {
            var snippet = "content : normal ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("content", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssContentProperty>(property);
            var concrete = (CssContentProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("normal", concrete.Value);
        }

        [Test]
        public void CssContentNoneLegalUppercaseN()
        {
            var snippet = "content : noNe ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("content", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssContentProperty>(property);
            var concrete = (CssContentProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("none", concrete.Value);
        }

        [Test]
        public void CssContentStringLegal()
        {
            var snippet = "content : 'hi' ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("content", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssContentProperty>(property);
            var concrete = (CssContentProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("\"hi\"", concrete.Value);
        }

        [Test]
        public void CssContentNoOpenQuoteNoCloseQuoteLegal()
        {
            var snippet = "content : no-open-quote no-close-quote ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("content", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssContentProperty>(property);
            var concrete = (CssContentProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("no-open-quote no-close-quote", concrete.Value);
        }

        [Test]
        public void CssContentUrlLegal()
        {
            var snippet = "content : url(test.html) ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("content", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssContentProperty>(property);
            var concrete = (CssContentProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("url(\"test.html\")", concrete.Value);
        }

        [Test]
        public void CssContentStringsLegal()
        {
            var snippet = "content : 'how' 'are' 'you' ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("content", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssContentProperty>(property);
            var concrete = (CssContentProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("\"how\" \"are\" \"you\"", concrete.Value);
        }

        [Test]
        public void CssQuoteStringIllegal()
        {
            var snippet = "quotes : '\"' ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("quotes", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssQuotesProperty>(property);
            var concrete = (CssQuotesProperty)property;
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssQuoteStringsLegal()
        {
            var snippet = "quotes : '\"' '\"' ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("quotes", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssQuotesProperty>(property);
            var concrete = (CssQuotesProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("\"\\\"\" \"\\\"\"", concrete.Value);
        }

        [Test]
        public void CssQuoteStringsIllegal()
        {
            var snippet = "quotes : \"'\"";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("quotes", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssQuotesProperty>(property);
            var concrete = (CssQuotesProperty)property;
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssQuoteStringsMultipleLegal()
        {
            var snippet = "quotes : '\"' '\"' '`' '´' ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("quotes", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssQuotesProperty>(property);
            var concrete = (CssQuotesProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("\"\\\"\" \"\\\"\" \"`\" \"´\"", concrete.Value);
        }

        [Test]
        public void CssQuoteStringsMultipleIllegal()
        {
            var snippet = "quotes : '\"' '\"' '`' ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("quotes", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssQuotesProperty>(property);
            var concrete = (CssQuotesProperty)property;
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssQuoteNoneLegal()
        {
            var snippet = "quotes : none";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("quotes", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssQuotesProperty>(property);
            var concrete = (CssQuotesProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("none", concrete.Value);
        }

        [Test]
        public void CssQuoteNoneStringIllegal()
        {
            var snippet = "quotes : 'none'";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("quotes", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssQuotesProperty>(property);
            var concrete = (CssQuotesProperty)property;
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssQuoteNormalIllegal()
        {
            var snippet = "quotes : normal ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("quotes", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssQuotesProperty>(property);
            var concrete = (CssQuotesProperty)property;
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssWidowsZeroLegal()
        {
            var snippet = "widows: 0";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("widows", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssWidowsProperty>(property);
            var concrete = (CssWidowsProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            //Assert.AreEqual(0, concrete.Count);
            Assert.AreEqual("0", concrete.Value);
        }

        [Test]
        public void CssWidowsThreeLegal()
        {
            var snippet = "widows: 3";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("widows", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssWidowsProperty>(property);
            var concrete = (CssWidowsProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            //Assert.AreEqual(3, concrete.Count);
            Assert.AreEqual("3", concrete.Value);
        }

        [Test]
        public void CssWidowsLengthIllegal()
        {
            var snippet = "widows: 5px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("widows", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssWidowsProperty>(property);
            var concrete = (CssWidowsProperty)property;
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
            //Assert.AreEqual(2, concrete.Count);
        }

        [Test]
        public void CssUnicodeBidiEmbedLegal()
        {
            var snippet = "unicode-BIDI: Embed";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("unicode-bidi", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssUnicodeBidiProperty>(property);
            var concrete = (CssUnicodeBidiProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            //Assert.AreEqual(UnicodeMode.Embed, concrete.State);
            Assert.AreEqual("embed", concrete.Value);
        }

        [Test]
        public void CssUnicodeBidiIsolateLegal()
        {
            var snippet = "unicode-Bidi: isolate";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("unicode-bidi", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssUnicodeBidiProperty>(property);
            var concrete = (CssUnicodeBidiProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            //Assert.AreEqual(UnicodeMode.Isolate, concrete.State);
            Assert.AreEqual("isolate", concrete.Value);
        }

        [Test]
        public void CssUnicodeBidiBidiOverrideLegal()
        {
            var snippet = "unicode-Bidi: Bidi-Override";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("unicode-bidi", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssUnicodeBidiProperty>(property);
            var concrete = (CssUnicodeBidiProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            //Assert.AreEqual(UnicodeMode.BidiOverride, concrete.State);
            Assert.AreEqual("bidi-override", concrete.Value);
        }

        [Test]
        public void CssUnicodeBidiPlaintextLegal()
        {
            var snippet = "unicode-Bidi: PLAINTEXT";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("unicode-bidi", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssUnicodeBidiProperty>(property);
            var concrete = (CssUnicodeBidiProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            //Assert.AreEqual(UnicodeMode.Plaintext, concrete.State);
            Assert.AreEqual("plaintext", concrete.Value);
        }

        [Test]
        public void CssUnicodeBidiIllegal()
        {
            var snippet = "unicode-bidi: none";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("unicode-bidi", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssUnicodeBidiProperty>(property);
            var concrete = (CssUnicodeBidiProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
            //Assert.AreEqual(UnicodeMode.Normal, concrete.State);
        }

        [Test]
        public void CssPropertyFactoryCalls()
        {
            var parser = new CssParser();
            var decl = new CssStyleDeclaration(parser);
            var invalid = decl.CreateProperty("invalid");
            var border = decl.CreateProperty("border");
            var color = decl.CreateProperty("color");
            decl.SetProperty(color);
            var colorAgain = decl.CreateProperty("color");

            Assert.IsNull(invalid);
            Assert.IsNotNull(border);
            Assert.IsNotNull(color);
            Assert.IsNotNull(colorAgain);

            Assert.IsInstanceOf<CssBorderProperty>(border);
            Assert.IsInstanceOf<CssColorProperty>(color);
            Assert.AreEqual(color, colorAgain);
        }

        [Test]
        public void CssUnknownPropertyPreservesCase()
        {
            var snippet = "my-Property: something";
            var property = ParseDeclaration(snippet, new CssParserOptions { IsIncludingUnknownDeclarations = true });
            Assert.AreEqual("my-Property", property.Name);
            Assert.IsInstanceOf<CssUnknownProperty>(property);
        }
    }
}
