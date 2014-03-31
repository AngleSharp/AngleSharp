using AngleSharp;
using AngleSharp.DOM.Css;
using AngleSharp.DOM.Css.Properties;
using AngleSharp.Parser.Css;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            var value = (CSSPrimitiveValue)concrete.Value;
            Assert.AreEqual("Page", value.CssText);
            Assert.IsInstanceOfType(value, typeof(CSSIdentifierValue));
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
            var value = (CSSPrimitiveValue)concrete.Value;
            Assert.AreEqual("AUTO", value.CssText);
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
            var value = (CSSPrimitiveValue)concrete.Value;
            Assert.AreEqual("avoid-REGION", value.CssText);
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
            var value = (CSSPrimitiveValue)concrete.Value;
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
            var value = (CSSPrimitiveValue)concrete.Value;
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
            Assert.IsInstanceOfType(value, typeof(CSSPercentValue));
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
            Assert.IsInstanceOfType(value, typeof(CSSLengthValue));
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
            Assert.IsInstanceOfType(value, typeof(CSSLengthValue));
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
            Assert.IsInstanceOfType(value, typeof(CSSIdentifierValue));
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
            Assert.IsInstanceOfType(value, typeof(CSSLengthValue));
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
            Assert.IsInstanceOfType(value, typeof(CSSLengthValue));
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
            var value = (CSSPrimitiveValue)concrete.Value;
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
            var value = (CSSPrimitiveValue)concrete.Value;
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
            var value = (CSSPrimitiveValue)concrete.Value;
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
            var value = (CSSPrimitiveValue)concrete.Value;
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
            var value = (CSSPrimitiveValue)concrete.Value;
            Assert.AreEqual("fiXed", value.CssText);
            Assert.IsInstanceOfType(value, typeof(CSSIdentifierValue));
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
            Assert.IsInstanceOfType(value, typeof(CSSIdentifierValue));
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
            Assert.IsInstanceOfType(value, typeof(CSSIdentifierValue));
            concrete.Value = new CSSIdentifierValue("left");
            value = (CSSPrimitiveValue)concrete.Value;
            Assert.AreEqual("left", value.CssText);
            Assert.IsInstanceOfType(value, typeof(CSSIdentifierValue));
            concrete.Value = new CSSIdentifierValue("whatever");
            Assert.AreEqual(value, concrete.Value);
        }

        [TestMethod]
        public void CssLeftLegalPixel()
        {
            var snippet = "left: 25px";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("left", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSLeftProperty));
            var concrete = (CSSLeftProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            var value = (CSSLengthValue)concrete.Value;
            Assert.AreEqual(new Length(25f, Length.Unit.Px), value.Length);
        }

        [TestMethod]
        public void CssTopLegalEm()
        {
            var snippet = "top:  0.7em ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("top", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSTopProperty));
            var concrete = (CSSTopProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            var value = (CSSLengthValue)concrete.Value;
            Assert.AreEqual(new Length(0.7f, Length.Unit.Em), value.Length);
        }

        [TestMethod]
        public void CssRightLegalMm()
        {
            var snippet = "right:  1.5mm";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("right", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSRightProperty));
            var concrete = (CSSRightProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            var value = (CSSLengthValue)concrete.Value;
            Assert.AreEqual(new Length(1.5f, Length.Unit.Mm), value.Length);
        }

        [TestMethod]
        public void CssBottomLegalPercent()
        {
            var snippet = "bottom:  50%";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("bottom", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSBottomProperty));
            var concrete = (CSSBottomProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            var value = (CSSPercentValue)concrete.Value;
            Assert.AreEqual(50f, value.Value);
        }

        [TestMethod]
        public void CssHeightZeroLegal()
        {
            var snippet = "height:0";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("height", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSHeightProperty));
            var concrete = (CSSHeightProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            var value = (CSSNumberValue)concrete.Value;
            Assert.AreEqual(0f, value.Value);
        }

        [TestMethod]
        public void CssWidthZeroLegal()
        {
            var snippet = "width  :  0";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("width", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSWidthProperty));
            var concrete = (CSSWidthProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            var value = (CSSNumberValue)concrete.Value;
            Assert.AreEqual(0f, value.Value);
        }

        [TestMethod]
        public void CssWidthPercentLegal()
        {
            var snippet = "width  :  20.5%";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("width", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSWidthProperty));
            var concrete = (CSSWidthProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            var value = (CSSPercentValue)concrete.Value;
            Assert.AreEqual(20.5f, value.Value);
        }

        [TestMethod]
        public void CssWidthPercentInLegal()
        {
            var snippet = "width  :  3in";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("width", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSWidthProperty));
            var concrete = (CSSWidthProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            var value = (CSSLengthValue)concrete.Value;
            Assert.AreEqual(new Length(3f, Length.Unit.In), value.Length);
        }

        [TestMethod]
        public void CssHeightAngleIllegal()
        {
            var snippet = "height  :  3deg";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("height", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSHeightProperty));
            var concrete = (CSSHeightProperty)property;
            Assert.IsFalse(concrete.HasValue);
            Assert.IsFalse(concrete.IsInherited);
        }

        [TestMethod]
        public void CssHeightResolutionIllegal()
        {
            var snippet = "height  :  3dpi";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("height", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSHeightProperty));
            var concrete = (CSSHeightProperty)property;
            Assert.IsFalse(concrete.HasValue);
            Assert.IsFalse(concrete.IsInherited);
        }

        [TestMethod]
        public void CssTopLegalRem()
        {
            var snippet = "top:  1.2rem ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("top", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSTopProperty));
            var concrete = (CSSTopProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            var value = (CSSLengthValue)concrete.Value;
            Assert.AreEqual(new Length(1.2f, Length.Unit.Rem), value.Length);
        }

        [TestMethod]
        public void CssRightLegalCm()
        {
            var snippet = "right:  0.5cm";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("right", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSRightProperty));
            var concrete = (CSSRightProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            var value = (CSSLengthValue)concrete.Value;
            Assert.AreEqual(new Length(0.5f, Length.Unit.Cm), value.Length);
        }

        [TestMethod]
        public void CssBottomLegalPercentTwo()
        {
            var snippet = "bottom:  0.50%";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("bottom", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSBottomProperty));
            var concrete = (CSSBottomProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            var value = (CSSPercentValue)concrete.Value;
            Assert.AreEqual(0.50f, value.Value);
        }

        [TestMethod]
        public void CssBottomLegalZero()
        {
            var snippet = "bottom:  0";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("bottom", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSBottomProperty));
            var concrete = (CSSBottomProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            var value = (CSSNumberValue)concrete.Value;
            Assert.AreEqual(0f, value.Value);
        }

        [TestMethod]
        public void CssBottomIllegalNumber()
        {
            var snippet = "bottom:  20";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("bottom", property.Name);
            Assert.IsFalse(property.Important);
            Assert.IsInstanceOfType(property, typeof(CSSBottomProperty));
            var concrete = (CSSBottomProperty)property;
            Assert.AreEqual(CssValueType.Inherit, concrete.Value.CssValueType);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
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
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
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
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
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
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.CssValueType);
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
    }
}
