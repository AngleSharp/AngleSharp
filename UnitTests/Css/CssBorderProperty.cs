using AngleSharp;
using AngleSharp.DOM.Css;
using AngleSharp.DOM.Css.Properties;
using AngleSharp.Parser.Css;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.Css
{
    [TestClass]
    public class CssBorderPropertyTests
    {
        [TestMethod]
        public void CssBorderSpacingLengthLegal()
        {
            var snippet = "border-spacing: 20px";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-spacing", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSBorderSpacingProperty));
            var concrete = (CSSBorderSpacingProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.Type);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("20px", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssBorderSpacingZeroLegal()
        {
            var snippet = "border-spacing: 0";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-spacing", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSBorderSpacingProperty));
            var concrete = (CSSBorderSpacingProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.Type);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssBorderSpacingLengthLengthLegal()
        {
            var snippet = "border-spacing: 15px 3em";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-spacing", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSBorderSpacingProperty));
            var concrete = (CSSBorderSpacingProperty)property;
            Assert.AreEqual(CssValueType.ValueList, concrete.Value.Type);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("15px 3em", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssBorderSpacingLengthZeroLegal()
        {
            var snippet = "border-spacing: 15px 0";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-spacing", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSBorderSpacingProperty));
            var concrete = (CSSBorderSpacingProperty)property;
            Assert.AreEqual(CssValueType.ValueList, concrete.Value.Type);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("15px 0", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssBorderSpacingPercentIllegal()
        {
            var snippet = "border-spacing: 15%";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-spacing", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSBorderSpacingProperty));
            var concrete = (CSSBorderSpacingProperty)property;
            Assert.AreEqual(CssValueType.Inherit, concrete.Value.Type);
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [TestMethod]
        public void CssBorderBottomColorRedLegal()
        {
            var snippet = "border-bottom-color: red";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-bottom-color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSBorderBottomColorProperty));
            var concrete = (CSSBorderBottomColorProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("red", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssBorderTopColorHexLegal()
        {
            var snippet = "border-top-color: #0F0";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-top-color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSBorderTopColorProperty));
            var concrete = (CSSBorderTopColorProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("rgba(0, 255, 0, 1)", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssBorderRightColorRgbaLegal()
        {
            var snippet = "border-right-color: rgba(1, 1, 1, 0)";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-right-color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSBorderRightColorProperty));
            var concrete = (CSSBorderRightColorProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("rgba(1, 1, 1, 0)", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssBorderLeftColorRgbLegal()
        {
            var snippet = "border-left-color: rgb(1, 255, 100)  !important";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-left-color", property.Name);
            Assert.IsTrue(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSBorderLeftColorProperty));
            var concrete = (CSSBorderLeftColorProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("rgba(1, 255, 100, 1)", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssBorderColorTransparentLegal()
        {
            var snippet = "border-color: transparent";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSBorderColorProperty));
            var concrete = (CSSBorderColorProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("transparent", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssBorderColorRedGreenLegal()
        {
            var snippet = "border-color: red   green";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSBorderColorProperty));
            var concrete = (CSSBorderColorProperty)property;
            Assert.AreEqual(CssValueType.ValueList, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("red green", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssBorderColorRedRgbLegal()
        {
            var snippet = "border-color: red   rgb(0,0,0)";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSBorderColorProperty));
            var concrete = (CSSBorderColorProperty)property;
            Assert.AreEqual(CssValueType.ValueList, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("red rgba(0, 0, 0, 1)", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssBorderColorRedBlueGreenLegal()
        {
            var snippet = "border-color: red blue green";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSBorderColorProperty));
            var concrete = (CSSBorderColorProperty)property;
            Assert.AreEqual(CssValueType.ValueList, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("red blue green", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssBorderColorRedBlueGreenBlackLegal()
        {
            var snippet = "border-color: red blue green   BLACK";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSBorderColorProperty));
            var concrete = (CSSBorderColorProperty)property;
            Assert.AreEqual(CssValueType.ValueList, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("red blue green BLACK", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssBorderColorRedBlueGreenBlackTransparentIllegal()
        {
            var snippet = "border-color: red blue green black transparent";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSBorderColorProperty));
            var concrete = (CSSBorderColorProperty)property;
            Assert.AreEqual(CssValueType.Inherit, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [TestMethod]
        public void CssBorderStyleDottedLegal()
        {
            var snippet = "border-style: dotted";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-style", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSBorderStyleProperty));
            var concrete = (CSSBorderStyleProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("dotted", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssBorderStyleInsetOutsetUpperLegal()
        {
            var snippet = "border-style: INSET   OUTset";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-style", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSBorderStyleProperty));
            var concrete = (CSSBorderStyleProperty)property;
            Assert.AreEqual(CssValueType.ValueList, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("INSET OUTset", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssBorderStyleDoubleGrooveLegal()
        {
            var snippet = "border-style: double   groove";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-style", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSBorderStyleProperty));
            var concrete = (CSSBorderStyleProperty)property;
            Assert.AreEqual(CssValueType.ValueList, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("double groove", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssBorderStyleRidgeSolidDashedLegal()
        {
            var snippet = "border-style: ridge solid dashed";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-style", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSBorderStyleProperty));
            var concrete = (CSSBorderStyleProperty)property;
            Assert.AreEqual(CssValueType.ValueList, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("ridge solid dashed", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssBorderStyleHiddenDottedNoneNoneLegal()
        {
            var snippet = "border-style   :   hidden  dotted  NONE   nONe";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-style", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSBorderStyleProperty));
            var concrete = (CSSBorderStyleProperty)property;
            Assert.AreEqual(CssValueType.ValueList, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("hidden dotted NONE nONe", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssBorderStyleWavyIllegal()
        {
            var snippet = "border-style: wavy";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-style", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSBorderStyleProperty));
            var concrete = (CSSBorderStyleProperty)property;
            Assert.AreEqual(CssValueType.Inherit, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [TestMethod]
        public void CssBorderBottomStyleGrooveLegal()
        {
            var snippet = "border-bottom-style: GROOVE";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-bottom-style", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSBorderBottomStyleProperty));
            var concrete = (CSSBorderBottomStyleProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("GROOVE", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssBorderTopStyleNoneLegal()
        {
            var snippet = "border-top-style:none";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-top-style", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSBorderTopStyleProperty));
            var concrete = (CSSBorderTopStyleProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("none", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssBorderRightStyleDoubleLegal()
        {
            var snippet = "border-right-style:double";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-right-style", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSBorderRightStyleProperty));
            var concrete = (CSSBorderRightStyleProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("double", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssBorderLeftStyleHiddenLegal()
        {
            var snippet = "border-left-style: hidden  !important";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-left-style", property.Name);
            Assert.IsTrue(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSBorderLeftStyleProperty));
            var concrete = (CSSBorderLeftStyleProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("hidden", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssBorderBottomWidthThinLegal()
        {
            var snippet = "border-bottom-width: THIN";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-bottom-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSBorderBottomWidthProperty));
            var concrete = (CSSBorderBottomWidthProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("THIN", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssBorderTopWidthZeroLegal()
        {
            var snippet = "border-top-width: 0";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-top-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSBorderTopWidthProperty));
            var concrete = (CSSBorderTopWidthProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssBorderRightWidthEmLegal()
        {
            var snippet = "border-right-width: 3em";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-right-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSBorderRightWidthProperty));
            var concrete = (CSSBorderRightWidthProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("3em", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssBorderLeftWidthThickLegal()
        {
            var snippet = "border-left-width: thick !important";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-left-width", property.Name);
            Assert.IsTrue(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSBorderLeftWidthProperty));
            var concrete = (CSSBorderLeftWidthProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("thick", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssBorderWidthMediumLegal()
        {
            var snippet = "border-width: medium";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSBorderWidthProperty));
            var concrete = (CSSBorderWidthProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("medium", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssBorderWidthLengthZeroLegal()
        {
            var snippet = "border-width: 3px   0";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSBorderWidthProperty));
            var concrete = (CSSBorderWidthProperty)property;
            Assert.AreEqual(CssValueType.ValueList, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("3px 0", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssBorderWidthThinLengthLegal()
        {
            var snippet = "border-width: THIN   1px";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSBorderWidthProperty));
            var concrete = (CSSBorderWidthProperty)property;
            Assert.AreEqual(CssValueType.ValueList, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("THIN 1px", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssBorderWidthMediumThinThickLegal()
        {
            var snippet = "border-width: medium thin thick";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSBorderWidthProperty));
            var concrete = (CSSBorderWidthProperty)property;
            Assert.AreEqual(CssValueType.ValueList, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("medium thin thick", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssBorderWidthLengthLengthLengthLengthLegal()
        {
            var snippet = "border-width:  1px  2px   3px  4px  !important ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-width", property.Name);
            Assert.IsTrue(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSBorderWidthProperty));
            var concrete = (CSSBorderWidthProperty)property;
            Assert.AreEqual(CssValueType.ValueList, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("1px 2px 3px 4px", concrete.Value.CssText);
        }

        [TestMethod]
        public void CssBorderWidthLengthInEmZeroLegal()
        {
            var snippet = "border-width:  0.3em 0 ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSBorderWidthProperty));
            var concrete = (CSSBorderWidthProperty)property;
            Assert.AreEqual(CssValueType.ValueList, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0.3em 0", concrete.Value.CssText);
            Assert.AreEqual(new Length(0.3f, Length.Unit.Em), concrete.Top);
            Assert.AreEqual(Length.Zero, concrete.Right);
            Assert.AreEqual(new Length(0.3f, Length.Unit.Em), concrete.Bottom);
            Assert.AreEqual(Length.Zero, concrete.Left);
        }

        [TestMethod]
        public void CssBorderWidthMediumZeroLengthThickLegal()
        {
            var snippet = "border-width:   medium 0 1px thick ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSBorderWidthProperty));
            var concrete = (CSSBorderWidthProperty)property;
            Assert.AreEqual(CssValueType.ValueList, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("medium 0 1px thick", concrete.Value.CssText);
            Assert.AreEqual(Length.Medium, concrete.Top);
            Assert.AreEqual(Length.Zero, concrete.Right);
            Assert.AreEqual(new Length(1f, Length.Unit.Px), concrete.Bottom);
            Assert.AreEqual(Length.Thick, concrete.Left);
        }

        [TestMethod]
        public void CssBorderWidthZerosIllegal()
        {
            var snippet = "border-width: 0 0 0 0 0";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSBorderWidthProperty));
            var concrete = (CSSBorderWidthProperty)property;
            Assert.AreEqual(CssValueType.Inherit, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [TestMethod]
        public void CssBorderLeftZeroLegal()
        {
            var snippet = "border-left:   0 ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-left", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSBorderLeftProperty));
            var concrete = (CSSBorderLeftProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0", concrete.Value.CssText);
            Assert.AreEqual(Length.Zero, concrete.Width);
            Assert.AreEqual(Color.Transparent, concrete.Color);
            Assert.AreEqual(LineStyle.None, concrete.Style);
        }

        [TestMethod]
        public void CssBorderRightLineStyleLegal()
        {
            var snippet = "border-right :   dotted ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-right", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSBorderRightProperty));
            var concrete = (CSSBorderRightProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("dotted", concrete.Value.CssText);
            Assert.AreEqual(Length.Medium, concrete.Width);
            Assert.AreEqual(Color.Transparent, concrete.Color);
            Assert.AreEqual(LineStyle.Dotted, concrete.Style);
        }

        [TestMethod]
        public void CssBorderTopLengthRedLegal()
        {
            var snippet = "border-top :  2px red ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-top", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSBorderTopProperty));
            var concrete = (CSSBorderTopProperty)property;
            Assert.AreEqual(CssValueType.ValueList, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("2px red", concrete.Value.CssText);
            Assert.AreEqual(new Length(2f, Length.Unit.Px), concrete.Width);
            Assert.AreEqual(Color.Red, concrete.Color);
            Assert.AreEqual(LineStyle.None, concrete.Style);
        }

        [TestMethod]
        public void CssBorderBottomRgbLegal()
        {
            var snippet = "border-bottom :  rgb(255, 100, 0) ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border-bottom", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSBorderBottomProperty));
            var concrete = (CSSBorderBottomProperty)property;
            Assert.AreEqual(CssValueType.PrimitiveValue, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("rgba(255, 100, 0, 1)", concrete.Value.CssText);
            Assert.AreEqual(Length.Medium, concrete.Width);
            Assert.AreEqual(Color.FromRgb(255, 100, 0), concrete.Color);
            Assert.AreEqual(LineStyle.None, concrete.Style);
        }

        [TestMethod]
        public void CssBorderGrooveRgbLegal()
        {
            var snippet = "border :  GROOVE rgb(255, 100, 0) ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSBorderProperty));
            var concrete = (CSSBorderProperty)property;
            Assert.AreEqual(CssValueType.ValueList, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("GROOVE rgba(255, 100, 0, 1)", concrete.Value.CssText);
            Assert.AreEqual(Length.Medium, concrete.Width);
            Assert.AreEqual(Color.FromRgb(255, 100, 0), concrete.Color);
            Assert.AreEqual(LineStyle.Groove, concrete.Style);
        }

        [TestMethod]
        public void CssBorderInsetGreenLengthLegal()
        {
            var snippet = "border :  inset  green 3em ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSBorderProperty));
            var concrete = (CSSBorderProperty)property;
            Assert.AreEqual(CssValueType.ValueList, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("inset green 3em", concrete.Value.CssText);
            Assert.AreEqual(new Length(3f, Length.Unit.Em), concrete.Width);
            Assert.AreEqual(Color.Green, concrete.Color);
            Assert.AreEqual(LineStyle.Inset, concrete.Style);
        }

        [TestMethod]
        public void CssBorderRedSolidLengthLegal()
        {
            var snippet = "border :  red  SOLID 1px ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSBorderProperty));
            var concrete = (CSSBorderProperty)property;
            Assert.AreEqual(CssValueType.ValueList, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("red SOLID 1px", concrete.Value.CssText);
            Assert.AreEqual(new Length(1f, Length.Unit.Px), concrete.Width);
            Assert.AreEqual(Color.Red, concrete.Color);
            Assert.AreEqual(LineStyle.Solid, concrete.Style);
        }

        [TestMethod]
        public void CssBorderLengthBlackDoubleLegal()
        {
            var snippet = "border :  0.5px black double ";
            var property = CssParser.ParseDeclaration(snippet);
            Assert.AreEqual("border", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOfType(property, typeof(CSSBorderProperty));
            var concrete = (CSSBorderProperty)property;
            Assert.AreEqual(CssValueType.ValueList, concrete.Value.Type);
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0.5px black double", concrete.Value.CssText);
            Assert.AreEqual(new Length(0.5f, Length.Unit.Px), concrete.Width);
            Assert.AreEqual(Color.Black, concrete.Color);
            Assert.AreEqual(LineStyle.Double, concrete.Style);
        }
    }
}
