using AngleSharp.DOM.Css;
using AngleSharp.Parser.Css;
using NUnit.Framework;

namespace UnitTests.Css
{
    [TestFixture]
    public class CssPropertyTests
    {
        [Test]
        public void CssBreakAfterLegalAvoid()
        {
            var snippet = "break-after:avoid";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("break-after", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSBreakAfterProperty>(property);
            var concrete = (CSSBreakAfterProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            var value = concrete.Value;
            Assert.AreEqual("avoid", value.CssText);
        }

        [Test]
        public void CssPageBreakAfterLegalAvoid()
        {
            var snippet = "page-break-after:avoid";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("page-break-after", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSPageBreakAfterProperty>(property);
            var concrete = (CSSPageBreakAfterProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            var value = concrete.Value;
            Assert.AreEqual("avoid", value.CssText);
        }

        [Test]
        public void CssBreakAfterLegalPageCapital()
        {
            var snippet = "break-after:Page";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("break-after", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSBreakAfterProperty>(property);
            var concrete = (CSSBreakAfterProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            var value = concrete.Value;
            Assert.AreEqual("page", value.CssText);
        }

        [Test]
        public void CssPageBreakAfterIllegalAvoidColumn()
        {
            var snippet = "page-break-after:avoid-column";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("page-break-after", property.Name);
            Assert.IsFalse(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSPageBreakAfterProperty>(property);
            var concrete = (CSSPageBreakAfterProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
        }

        [Test]
        public void CssBreakAfterLegalAvoidColumn()
        {
            var snippet = "break-after:avoid-column";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("break-after", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSBreakAfterProperty>(property);
            var concrete = (CSSBreakAfterProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            var value = concrete.Value;
            Assert.AreEqual("avoid-column", value.CssText);
        }

        [Test]
        public void CssBreakBeforeLegalAvoidColumn()
        {
            var snippet = "break-before:AUTO";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("break-before", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSBreakBeforeProperty>(property);
            var concrete = (CSSBreakBeforeProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            var value = concrete.Value;
            Assert.AreEqual("auto", value.CssText);
        }

        [Test]
        public void CssPageBreakBeforeLegalAvoid()
        {
            var snippet = "page-break-before:AUTO";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("page-break-before", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSPageBreakBeforeProperty>(property);
            var concrete = (CSSPageBreakBeforeProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            var value = concrete.Value;
            Assert.AreEqual("auto", value.CssText);
        }

        [Test]
        public void CssPageBreakBeforeLegalLeft()
        {
            var snippet = "page-break-before:left";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("page-break-before", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSPageBreakBeforeProperty>(property);
            var concrete = (CSSPageBreakBeforeProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            var value = concrete.Value;
            Assert.AreEqual("left", value.CssText);
        }

        [Test]
        public void CssBreakBeforeIllegalValue()
        {
            var snippet = "break-before:whatever";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("break-before", property.Name);
            Assert.IsFalse(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSBreakBeforeProperty>(property);
            var concrete = (CSSBreakBeforeProperty)property;
            Assert.AreEqual(CssValue.Initial, concrete.Value);
        }

        [Test]
        public void CssBreakInsideIllegalPage()
        {
            var snippet = "break-inside:page";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("break-inside", property.Name);
            Assert.IsFalse(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSBreakInsideProperty>(property);
            var concrete = (CSSBreakInsideProperty)property;
            Assert.AreEqual(CssValue.Initial, concrete.Value);
        }

        [Test]
        public void CssBreakInsideLegalAvoidRegionUppercase()
        {
            var snippet = "break-inside:avoid-REGION";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("break-inside", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSBreakInsideProperty>(property);
            var concrete = (CSSBreakInsideProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            var value = concrete.Value;
            Assert.AreEqual("avoid-region", value.CssText);
        }

        [Test]
        public void CssPageBreakInsideLegalAvoid()
        {
            var snippet = "page-break-inside:avoid";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("page-break-inside", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSPageBreakInsideProperty>(property);
            var concrete = (CSSPageBreakInsideProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            var value = concrete.Value;
            Assert.AreEqual("avoid", value.CssText);
        }

        [Test]
        public void CssPageBreakInsideLegalAutoUppercase()
        {
            var snippet = "page-break-inside:AUTO";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("page-break-inside", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSPageBreakInsideProperty>(property);
            var concrete = (CSSPageBreakInsideProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            var value = concrete.Value;
            Assert.AreEqual("auto", value.CssText);
        }

        [Test]
        public void CssClearLegalLeft()
        {
            var snippet = "clear:left";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("clear", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSClearProperty>(property);
            var concrete = (CSSClearProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            var value = concrete.Value;
            Assert.AreEqual("left", value.CssText);
        }

        [Test]
        public void CssClearLegalBoth()
        {
            var snippet = "clear:both";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("clear", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSClearProperty>(property);
            var concrete = (CSSClearProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            var value = concrete.Value;
            Assert.AreEqual("both", value.CssText);
        }

        [Test]
        public void CssClearInherited()
        {
            var snippet = "clear:inherit";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("clear", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsTrue(property.IsInherited);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSClearProperty>(property);
            var concrete = (CSSClearProperty)property;
            Assert.AreEqual(CssValue.Inherit, concrete.Value);
        }

        [Test]
        public void CssClearIllegal()
        {
            var snippet = "clear:yes";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("clear", property.Name);
            Assert.IsFalse(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSClearProperty>(property);
            var concrete = (CSSClearProperty)property;
            Assert.AreEqual(CssValue.Initial, concrete.Value);
        }

        [Test]
        public void CssPositionLegalAbsolute()
        {
            var snippet = "position:absolute";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("position", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSPositionProperty>(property);
            var concrete = (CSSPositionProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            var value = concrete.Value;
            Assert.AreEqual("absolute", value.CssText);
        }

        [Test]
        public void CssDisplayLegalBlock()
        {
            var snippet = "display:   block ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("display", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSDisplayProperty>(property);
            var concrete = (CSSDisplayProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            var value = concrete.Value;
            Assert.AreEqual("block", value.CssText);
        }

        [Test]
        public void CssVisibilityLegalCollapse()
        {
            var snippet = "visibility:collapse";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("visibility", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSVisibilityProperty>(property);
            var concrete = (CSSVisibilityProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            var value = concrete.Value;
            Assert.AreEqual("collapse", value.CssText);
        }

        [Test]
        public void CssVisibilityLegalHiddenCompleteUppercase()
        {
            var snippet = "VISIBILITY:HIDDEN";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("visibility", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSVisibilityProperty>(property);
            var concrete = (CSSVisibilityProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            var value = concrete.Value;
            Assert.AreEqual("hidden", value.CssText);
        }

        [Test]
        public void CssOverflowLegalAuto()
        {
            var snippet = "overflow:auto";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("overflow", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSOverflowProperty>(property);
            var concrete = (CSSOverflowProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            var value = concrete.Value;
            Assert.AreEqual("auto", value.CssText);
        }

        [Test]
        public void CssTableLayoutLegalFixedCapitalX()
        {
            var snippet = "table-layout: fiXed";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("table-layout", property.Name);
            Assert.IsTrue(property.HasValue);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSTableLayoutProperty>(property);
            var concrete = (CSSTableLayoutProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            var value = concrete.Value;
            Assert.AreEqual("fixed", value.CssText);
        }

        [Test]
        public void CssBoxShadowOffsetLegal()
        {
            var snippet = "box-shadow:  5px 4px";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("box-shadow", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSBoxShadowProperty>(property);
            var concrete = (CSSBoxShadowProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("5px 4px", concrete.Value.CssText);
        }

        [Test]
        public void CssBoxShadowInsetOffsetLegal()
        {
            var snippet = "box-shadow: inset 5px 4px";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("box-shadow", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSBoxShadowProperty>(property);
            var concrete = (CSSBoxShadowProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("inset 5px 4px", concrete.Value.CssText);
        }

        [Test]
        public void CssBoxShadowNoneUppercaseLegal()
        {
            var snippet = "box-shadow: NONE";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("box-shadow", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSBoxShadowProperty>(property);
            var concrete = (CSSBoxShadowProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("none", concrete.Value.CssText);
        }

        [Test]
        public void CssBoxShadowNormalTealLegal()
        {
            var snippet = "box-shadow: 60px -16px teal";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("box-shadow", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSBoxShadowProperty>(property);
            var concrete = (CSSBoxShadowProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("60px -16px teal", concrete.Value.CssText);
        }

        [Test]
        public void CssBoxShadowNormalSpreadBlackLegal()
        {
            var snippet = "box-shadow: 10px 5px 5px black";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("box-shadow", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSBoxShadowProperty>(property);
            var concrete = (CSSBoxShadowProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("10px 5px 5px black", concrete.Value.CssText);
        }

        [Test]
        public void CssBoxShadowOliveAndRedLegal()
        {
            var snippet = "box-shadow: 3px 3px red, -1em 0 0.4em olive";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("box-shadow", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSBoxShadowProperty>(property);
            var concrete = (CSSBoxShadowProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("3px 3px red, -1em 0 0.4em olive", concrete.Value.CssText);
        }

        [Test]
        public void CssBoxShadowInsetGoldLegal()
        {
            var snippet = "box-shadow: inset 5em 1em gold";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("box-shadow", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSBoxShadowProperty>(property);
            var concrete = (CSSBoxShadowProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("inset 5em 1em gold", concrete.Value.CssText);
        }

        [Test]
        public void CssBoxShadowZeroGoldLegal()
        {
            var snippet = "box-shadow: 0 0 1em gold";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("box-shadow", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSBoxShadowProperty>(property);
            var concrete = (CSSBoxShadowProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0 0 1em gold", concrete.Value.CssText);
        }

        [Test]
        public void CssBoxShadowInsetZeroGoldLegal()
        {
            var snippet = "box-shadow: inset  0 0 1em gold";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("box-shadow", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSBoxShadowProperty>(property);
            var concrete = (CSSBoxShadowProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("inset 0 0 1em gold", concrete.Value.CssText);
        }

        [Test]
        public void CssBoxShadowInsetZeroGoldAndNormalRedLegal()
        {
            var snippet = "box-shadow: inset  0 0 1em  gold   ,  0 0   1em   red !important";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("box-shadow", property.Name);
            Assert.IsTrue(property.IsImportant);
            Assert.IsInstanceOf<CSSBoxShadowProperty>(property);
            var concrete = (CSSBoxShadowProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("inset 0 0 1em gold, 0 0 1em red", concrete.Value.CssText);
        }

        [Test]
        public void CssBoxShadowOffsetColorLegal()
        {
            var snippet = "box-shadow:  5px 4px #000";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("box-shadow", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSBoxShadowProperty>(property);
            var concrete = (CSSBoxShadowProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("5px 4px rgba(0, 0, 0, 1)", concrete.Value.CssText);
        }

        [Test]
        public void CssBoxShadowOffsetBlurColorLegal()
        {
            var snippet = "box-shadow:  5px 4px 2px #000";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("box-shadow", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSBoxShadowProperty>(property);
            var concrete = (CSSBoxShadowProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("5px 4px 2px rgba(0, 0, 0, 1)", concrete.Value.CssText);
        }

        [Test]
        public void CssBoxShadowInitialUppercaseLegal()
        {
            var snippet = "box-shadow:  INITIAL";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("box-shadow", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSBoxShadowProperty>(property);
            var concrete = (CSSBoxShadowProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("initial", concrete.Value.CssText);
        }

        [Test]
        public void CssBoxShadowOffsetIllegal()
        {
            var snippet = "box-shadow:  5px 4px 2px 1px 3px #f00";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("box-shadow", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSBoxShadowProperty>(property);
            var concrete = (CSSBoxShadowProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssClipShapeLegal()
        {
            var snippet = "clip: rect( 2px, 3em, 1in, 0cm )";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("clip", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSClipProperty>(property);
            var concrete = (CSSClipProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("rect(2px, 3em, 1in, 0)", concrete.Value.CssText);
        }

        [Test]
        public void CssClipShapeBackwards()
        {
            var snippet = "clip: rect( 2px 3em 1in 0cm )";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("clip", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSClipProperty>(property);
            var concrete = (CSSClipProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("rect(2px 3em 1in 0)", concrete.Value.CssText);
        }

        [Test]
        public void CssClipShapeZerosLegal()
        {
            var snippet = "clip: rect(0, 0, 0, 0)";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("clip", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSClipProperty>(property);
            var concrete = (CSSClipProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("rect(0, 0, 0, 0)", concrete.Value.CssText);
        }

        [Test]
        public void CssClipShapeZerosIllegal()
        {
            var snippet = "clip: rect(0, 0, 0 0)";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("clip", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSClipProperty>(property);
            var concrete = (CSSClipProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssClipShapeNonZerosIllegal()
        {
            var snippet = "clip: rect(2px, 1cm, 5mm)";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("clip", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSClipProperty>(property);
            var concrete = (CSSClipProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssClipShapeSingleValueIllegal()
        {
            var snippet = "clip: rect(1em)";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("clip", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSClipProperty>(property);
            var concrete = (CSSClipProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssCursorDefaultUppercaseLegal()
        {
            var snippet = "cursor: DEFAULT";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("cursor", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSCursorProperty>(property);
            var concrete = (CSSCursorProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("default", concrete.Value.CssText);
        }

        [Test]
        public void CssCursorAutoLegal()
        {
            var snippet = "cursor: auto";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("cursor", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSCursorProperty>(property);
            var concrete = (CSSCursorProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("auto", concrete.Value.CssText);
        }

        [Test]
        public void CssCursorZoomOutLegal()
        {
            var snippet = "cursor  : zoom-out";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("cursor", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSCursorProperty>(property);
            var concrete = (CSSCursorProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("zoom-out", concrete.Value.CssText);
        }

        [Test]
        public void CssCursorUrlNoFallbackIllegal()
        {
            var snippet = "cursor  : url(foo.png)";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("cursor", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSCursorProperty>(property);
            var concrete = (CSSCursorProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssCursorUrlLegal()
        {
            var snippet = "cursor  : url(foo.png), default";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("cursor", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSCursorProperty>(property);
            var concrete = (CSSCursorProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("url(\"foo.png\"), default", concrete.Value.CssText);
        }

        [Test]
        public void CssCursorUrlShiftedLegal()
        {
            var snippet = "cursor  : url(foo.png) 0 5, auto";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("cursor", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSCursorProperty>(property);
            var concrete = (CSSCursorProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("url(\"foo.png\") 0 5, auto", concrete.Value.CssText);
        }

        [Test]
        public void CssCursorUrlShiftedNoFallbackIllegal()
        {
            var snippet = "cursor  : url(foo.png) 0 5";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("cursor", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSCursorProperty>(property);
            var concrete = (CSSCursorProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssCursorUrlsLegal()
        {
            var snippet = "cursor  : url(foo.png), url(master.png), url(more.png), wait";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("cursor", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSCursorProperty>(property);
            var concrete = (CSSCursorProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("url(\"foo.png\"), url(\"master.png\"), url(\"more.png\"), wait", concrete.Value.CssText);
        }

        [Test]
        public void CssColorHexLegal()
        {
            var snippet = "color : #123456";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSColorProperty>(property);
            var concrete = (CSSColorProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("rgba(18, 52, 86, 1)", concrete.Value.CssText);
        }

        [Test]
        public void CssColorRgbLegal()
        {
            var snippet = "color : rgb(121, 181, 201)";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSColorProperty>(property);
            var concrete = (CSSColorProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("rgb(121, 181, 201)", concrete.Value.CssText);
        }

        [Test]
        public void CssColorRgbaLegal()
        {
            var snippet = "color : rgba(255, 255, 201, 0.7)";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSColorProperty>(property);
            var concrete = (CSSColorProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("rgba(255, 255, 201, 0.7)", concrete.Value.CssText);
        }

        [Test]
        public void CssColorNameLegal()
        {
            var snippet = "color : red";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSColorProperty>(property);
            var concrete = (CSSColorProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("red", concrete.Value.CssText);
        }

        [Test]
        public void CssColorNameUppercaseLegal()
        {
            var snippet = "color : BLUE";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSColorProperty>(property);
            var concrete = (CSSColorProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("blue", concrete.Value.CssText);
        }

        [Test]
        public void CssColorNameIllegal()
        {
            var snippet = "color : horse";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSColorProperty>(property);
            var concrete = (CSSColorProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssOrphansZeroLegal()
        {
            var snippet = "orphans : 0 ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("orphans", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSOrphansProperty>(property);
            var concrete = (CSSOrphansProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0", concrete.Value.CssText);
        }

        [Test]
        public void CssOrphansTwoLegal()
        {
            var snippet = "orphans : 2 ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("orphans", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSOrphansProperty>(property);
            var concrete = (CSSOrphansProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("2", concrete.Value.CssText);
        }

        [Test]
        public void CssOrphansNegativeIllegal()
        {
            var snippet = "orphans : -2 ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("orphans", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSOrphansProperty>(property);
            var concrete = (CSSOrphansProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssOrphansFloatingIllegal()
        {
            var snippet = "orphans : 1.5 ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("orphans", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSOrphansProperty>(property);
            var concrete = (CSSOrphansProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssBoxDecorationBreakNumberIllegal()
        {
            var snippet = "box-decoration-break : 1.5 ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("box-decoration-break", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSBoxDecorationBreak>(property);
            var concrete = (CSSBoxDecorationBreak)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssBoxDecorationBreakSliceLegal()
        {
            var snippet = "box-decoration-break : slice ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("box-decoration-break", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSBoxDecorationBreak>(property);
            var concrete = (CSSBoxDecorationBreak)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("slice", concrete.Value.CssText);
        }

        [Test]
        public void CssBoxDecorationBreakClonePascalLegal()
        {
            var snippet = "box-decoration-break : Clone ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("box-decoration-break", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSBoxDecorationBreak>(property);
            var concrete = (CSSBoxDecorationBreak)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("clone", concrete.Value.CssText);
        }

        [Test]
        public void CssBoxDecorationBreakInheritLegal()
        {
            var snippet = "box-decoration-break : inherit!important ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("box-decoration-break", property.Name);
            Assert.IsTrue(property.IsImportant);
            Assert.IsInstanceOf<CSSBoxDecorationBreak>(property);
            var concrete = (CSSBoxDecorationBreak)property;
            Assert.AreEqual(CssValueType.Inherit, concrete.Value.Type);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("inherit", concrete.Value.CssText);
        }

        [Test]
        public void CssContentNormalLegal()
        {
            var snippet = "content : normal ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("content", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSContentProperty>(property);
            var concrete = (CSSContentProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("normal", concrete.Value.CssText);
        }

        [Test]
        public void CssContentNoneLegalUppercaseN()
        {
            var snippet = "content : noNe ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("content", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSContentProperty>(property);
            var concrete = (CSSContentProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("none", concrete.Value.CssText);
        }

        [Test]
        public void CssContentStringLegal()
        {
            var snippet = "content : 'hi' ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("content", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSContentProperty>(property);
            var concrete = (CSSContentProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("\"hi\"", concrete.Value.CssText);
        }

        [Test]
        public void CssContentNoOpenQuoteNoCloseQuoteLegal()
        {
            var snippet = "content : no-open-quote no-close-quote ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("content", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSContentProperty>(property);
            var concrete = (CSSContentProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("no-open-quote no-close-quote", concrete.Value.CssText);
        }

        [Test]
        public void CssContentUrlLegal()
        {
            var snippet = "content : url(test.html) ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("content", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSContentProperty>(property);
            var concrete = (CSSContentProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("url(\"test.html\")", concrete.Value.CssText);
        }

        [Test]
        public void CssContentStringsLegal()
        {
            var snippet = "content : 'how' 'are' 'you' ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("content", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSContentProperty>(property);
            var concrete = (CSSContentProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("\"how\" \"are\" \"you\"", concrete.Value.CssText);
        }

        [Test]
        public void CssQuoteStringIllegal()
        {
            var snippet = "quotes : '\"' ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("quotes", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSQuotesProperty>(property);
            var concrete = (CSSQuotesProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssQuoteStringsLegal()
        {
            var snippet = "quotes : '\"' '\"' ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("quotes", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSQuotesProperty>(property);
            var concrete = (CSSQuotesProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("\"\\\"\" \"\\\"\"", concrete.Value.CssText);
        }

        [Test]
        public void CssQuoteStringsIllegal()
        {
            var snippet = "quotes : \"'\"";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("quotes", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSQuotesProperty>(property);
            var concrete = (CSSQuotesProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssQuoteStringsMultipleLegal()
        {
            var snippet = "quotes : '\"' '\"' '`' '´' ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("quotes", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSQuotesProperty>(property);
            var concrete = (CSSQuotesProperty)property;
            Assert.AreEqual(CssValueType.List, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("\"\\\"\" \"\\\"\" \"`\" \"´\"", concrete.Value.CssText);
        }

        [Test]
        public void CssQuoteStringsMultipleIllegal()
        {
            var snippet = "quotes : '\"' '\"' '`' ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("quotes", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSQuotesProperty>(property);
            var concrete = (CSSQuotesProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssQuoteNoneLegal()
        {
            var snippet = "quotes : none";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("quotes", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSQuotesProperty>(property);
            var concrete = (CSSQuotesProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("none", concrete.Value.CssText);
        }

        [Test]
        public void CssQuoteNoneStringIllegal()
        {
            var snippet = "quotes : 'none'";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("quotes", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSQuotesProperty>(property);
            var concrete = (CSSQuotesProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssQuoteNormalIllegal()
        {
            var snippet = "quotes : normal ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("quotes", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSQuotesProperty>(property);
            var concrete = (CSSQuotesProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssWidowsZeroLegal()
        {
            var snippet = "widows: 0";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("widows", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSWidowsProperty>(property);
            var concrete = (CSSWidowsProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual(0, concrete.Count);
            Assert.AreEqual("0", concrete.Value.CssText);
        }

        [Test]
        public void CssWidowsThreeLegal()
        {
            var snippet = "widows: 3";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("widows", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSWidowsProperty>(property);
            var concrete = (CSSWidowsProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual(3, concrete.Count);
            Assert.AreEqual("3", concrete.Value.CssText);
        }

        [Test]
        public void CssWidowsLengthIllegal()
        {
            var snippet = "widows: 5px";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("widows", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSWidowsProperty>(property);
            var concrete = (CSSWidowsProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
            Assert.AreEqual(2, concrete.Count);
        }

        [Test]
        public void CssUnicodeBidiEmbedLegal()
        {
            var snippet = "unicode-BIDI: Embed";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("unicode-bidi", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSUnicodeBidiProperty>(property);
            var concrete = (CSSUnicodeBidiProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual(UnicodeMode.Embed, concrete.State);
            Assert.AreEqual("embed", concrete.Value.CssText);
        }

        [Test]
        public void CssUnicodeBidiIsolateLegal()
        {
            var snippet = "unicode-Bidi: isolate";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("unicode-bidi", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSUnicodeBidiProperty>(property);
            var concrete = (CSSUnicodeBidiProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual(UnicodeMode.Isolate, concrete.State);
            Assert.AreEqual("isolate", concrete.Value.CssText);
        }

        [Test]
        public void CssUnicodeBidiBidiOverrideLegal()
        {
            var snippet = "unicode-Bidi: Bidi-Override";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("unicode-bidi", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSUnicodeBidiProperty>(property);
            var concrete = (CSSUnicodeBidiProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual(UnicodeMode.BidiOverride, concrete.State);
            Assert.AreEqual("bidi-override", concrete.Value.CssText);
        }

        [Test]
        public void CssUnicodeBidiPlaintextLegal()
        {
            var snippet = "unicode-Bidi: PLAINTEXT";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("unicode-bidi", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSUnicodeBidiProperty>(property);
            var concrete = (CSSUnicodeBidiProperty)property;
            Assert.AreEqual(CssValueType.Primitive, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual(UnicodeMode.Plaintext, concrete.State);
            Assert.AreEqual("plaintext", concrete.Value.CssText);
        }

        [Test]
        public void CssUnicodeBidiIllegal()
        {
            var snippet = "unicode-bidi: none";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("unicode-bidi", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CSSUnicodeBidiProperty>(property);
            var concrete = (CSSUnicodeBidiProperty)property;
            Assert.AreEqual(CssValueType.Initial, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
            Assert.AreEqual(UnicodeMode.Normal, concrete.State);
        }

        [Test]
        public void CssPropertyFactoryCalls()
        {
            var decl = new CSSStyleDeclaration();
            var invalid = decl.CreateProperty("invalid");
            var border = decl.CreateProperty("border");
            var color = decl.CreateProperty("color");
            decl.SetProperty(color);
            var colorAgain = decl.CreateProperty("color");

            Assert.IsNull(invalid);
            Assert.IsNotNull(border);
            Assert.IsNotNull(color);
            Assert.IsNotNull(colorAgain);

            Assert.IsInstanceOf<CSSBorderProperty>(border);
            Assert.IsInstanceOf<CSSColorProperty>(color);
            Assert.AreEqual(color, colorAgain);
        }
    }
}
