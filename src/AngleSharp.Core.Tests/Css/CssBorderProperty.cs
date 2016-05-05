namespace AngleSharp.Core.Tests.Css
{
    using AngleSharp.Dom.Css;
    using NUnit.Framework;

    [TestFixture]
    public class CssBorderPropertyTests : CssConstructionFunctions
    {
        [Test]
        public void CssBorderSpacingLengthLegal()
        {
            var snippet = "border-spacing: 20px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-spacing", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderSpacingProperty>(property);
            var concrete = (CssBorderSpacingProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("20px", concrete.Value);
        }

        [Test]
        public void CssBorderSpacingZeroLegal()
        {
            var snippet = "border-spacing: 0";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-spacing", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderSpacingProperty>(property);
            var concrete = (CssBorderSpacingProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0", concrete.Value);
        }

        [Test]
        public void CssBorderSpacingLengthLengthLegal()
        {
            var snippet = "border-spacing: 15px 3em";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-spacing", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderSpacingProperty>(property);
            var concrete = (CssBorderSpacingProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("15px 3em", concrete.Value);
        }

        [Test]
        public void CssBorderSpacingLengthZeroLegal()
        {
            var snippet = "border-spacing: 15px 0";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-spacing", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderSpacingProperty>(property);
            var concrete = (CssBorderSpacingProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("15px 0", concrete.Value);
        }

        [Test]
        public void CssBorderSpacingPercentIllegal()
        {
            var snippet = "border-spacing: 15%";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-spacing", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderSpacingProperty>(property);
            var concrete = (CssBorderSpacingProperty)property;
            Assert.IsTrue(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssBorderBottomColorRedLegal()
        {
            var snippet = "border-bottom-color: red";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-bottom-color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderBottomColorProperty>(property);
            var concrete = (CssBorderBottomColorProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("rgb(255, 0, 0)", concrete.Value);
        }

        [Test]
        public void CssBorderTopColorHexLegal()
        {
            var snippet = "border-top-color: #0F0";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-top-color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderTopColorProperty>(property);
            var concrete = (CssBorderTopColorProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("rgb(0, 255, 0)", concrete.Value);
        }

        [Test]
        public void CssBorderRightColorRgbaLegal()
        {
            var snippet = "border-right-color: rgba(1, 1, 1, 0)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-right-color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderRightColorProperty>(property);
            var concrete = (CssBorderRightColorProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("rgba(1, 1, 1, 0)", concrete.Value);
        }

        [Test]
        public void CssBorderLeftColorRgbLegal()
        {
            var snippet = "border-left-color: rgb(1, 255, 100)  !important";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-left-color", property.Name);
            Assert.IsTrue(property.IsImportant);
            Assert.IsInstanceOf<CssBorderLeftColorProperty>(property);
            var concrete = (CssBorderLeftColorProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("rgb(1, 255, 100)", concrete.Value);
        }

        [Test]
        public void CssBorderColorTransparentLegal()
        {
            var snippet = "border-color: transparent";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderColorProperty>(property);
            var concrete = (CssBorderColorProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("rgba(0, 0, 0, 0)", concrete.Value);
        }

        [Test]
        public void CssBorderColorRedGreenLegal()
        {
            var snippet = "border-color: red   green";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderColorProperty>(property);
            var concrete = (CssBorderColorProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("rgb(255, 0, 0) rgb(0, 128, 0)", concrete.Value);
        }

        [Test]
        public void CssBorderColorRedRgbLegal()
        {
            var snippet = "border-color: red   rgb(0,0,0)";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderColorProperty>(property);
            var concrete = (CssBorderColorProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("rgb(255, 0, 0) rgb(0, 0, 0)", concrete.Value);
        }

        [Test]
        public void CssBorderColorRedBlueGreenLegal()
        {
            var snippet = "border-color: red blue green";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderColorProperty>(property);
            var concrete = (CssBorderColorProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("rgb(255, 0, 0) rgb(0, 0, 255) rgb(0, 128, 0)", concrete.Value);
        }

        [Test]
        public void CssBorderColorRedBlueGreenBlackLegal()
        {
            var snippet = "border-color: red blue green   BLACK";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderColorProperty>(property);
            var concrete = (CssBorderColorProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("rgb(255, 0, 0) rgb(0, 0, 255) rgb(0, 128, 0) rgb(0, 0, 0)", concrete.Value);
        }

        [Test]
        public void CssBorderColorRedBlueGreenBlackTransparentIllegal()
        {
            var snippet = "border-color: red blue green black transparent";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-color", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderColorProperty>(property);
            var concrete = (CssBorderColorProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssBorderStyleDottedLegal()
        {
            var snippet = "border-style: dotted";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-style", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderStyleProperty>(property);
            var concrete = (CssBorderStyleProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("dotted", concrete.Value);
        }

        [Test]
        public void CssBorderStyleInsetOutsetUpperLegal()
        {
            var snippet = "border-style: INSET   OUTset";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-style", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderStyleProperty>(property);
            var concrete = (CssBorderStyleProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("inset outset", concrete.Value);
        }

        [Test]
        public void CssBorderStyleDoubleGrooveLegal()
        {
            var snippet = "border-style: double   groove";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-style", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderStyleProperty>(property);
            var concrete = (CssBorderStyleProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("double groove", concrete.Value);
        }

        [Test]
        public void CssBorderStyleRidgeSolidDashedLegal()
        {
            var snippet = "border-style: ridge solid dashed";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-style", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderStyleProperty>(property);
            var concrete = (CssBorderStyleProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("ridge solid dashed", concrete.Value);
        }

        [Test]
        public void CssBorderStyleHiddenDottedNoneNoneLegal()
        {
            var snippet = "border-style   :   hidden  dotted  NONE   nONe";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-style", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderStyleProperty>(property);
            var concrete = (CssBorderStyleProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("hidden dotted none none", concrete.Value);
        }

        [Test]
        public void CssBorderStyleWavyIllegal()
        {
            var snippet = "border-style: wavy";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-style", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderStyleProperty>(property);
            var concrete = (CssBorderStyleProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssBorderBottomStyleGrooveLegal()
        {
            var snippet = "border-bottom-style: GROOVE";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-bottom-style", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderBottomStyleProperty>(property);
            var concrete = (CssBorderBottomStyleProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("groove", concrete.Value);
        }

        [Test]
        public void CssBorderTopStyleNoneLegal()
        {
            var snippet = "border-top-style:none";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-top-style", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderTopStyleProperty>(property);
            var concrete = (CssBorderTopStyleProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("none", concrete.Value);
        }

        [Test]
        public void CssBorderRightStyleDoubleLegal()
        {
            var snippet = "border-right-style:double";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-right-style", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderRightStyleProperty>(property);
            var concrete = (CssBorderRightStyleProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("double", concrete.Value);
        }

        [Test]
        public void CssBorderLeftStyleHiddenLegal()
        {
            var snippet = "border-left-style: hidden  !important";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-left-style", property.Name);
            Assert.IsTrue(property.IsImportant);
            Assert.IsInstanceOf<CssBorderLeftStyleProperty>(property);
            var concrete = (CssBorderLeftStyleProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("hidden", concrete.Value);
        }

        [Test]
        public void CssBorderBottomWidthThinLegal()
        {
            var snippet = "border-bottom-width: THIN";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-bottom-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderBottomWidthProperty>(property);
            var concrete = (CssBorderBottomWidthProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("1px", concrete.Value);
        }

        [Test]
        public void CssBorderTopWidthZeroLegal()
        {
            var snippet = "border-top-width: 0";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-top-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderTopWidthProperty>(property);
            var concrete = (CssBorderTopWidthProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0", concrete.Value);
        }

        [Test]
        public void CssBorderRightWidthEmLegal()
        {
            var snippet = "border-right-width: 3em";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-right-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderRightWidthProperty>(property);
            var concrete = (CssBorderRightWidthProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("3em", concrete.Value);
        }

        [Test]
        public void CssBorderLeftWidthThickLegal()
        {
            var snippet = "border-left-width: thick !important";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-left-width", property.Name);
            Assert.IsTrue(property.IsImportant);
            Assert.IsInstanceOf<CssBorderLeftWidthProperty>(property);
            var concrete = (CssBorderLeftWidthProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("5px", concrete.Value);
        }

        [Test]
        public void CssBorderWidthMediumLegal()
        {
            var snippet = "border-width: medium";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderWidthProperty>(property);
            var concrete = (CssBorderWidthProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("3px", concrete.Value);
        }

        [Test]
        public void CssBorderWidthLengthZeroLegal()
        {
            var snippet = "border-width: 3px   0";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderWidthProperty>(property);
            var concrete = (CssBorderWidthProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("3px 0", concrete.Value);
        }

        [Test]
        public void CssBorderWidthThinLengthLegal()
        {
            var snippet = "border-width: THIN   1px";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderWidthProperty>(property);
            var concrete = (CssBorderWidthProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("1px", concrete.Value);
        }

        [Test]
        public void CssBorderWidthMediumThinThickLegal()
        {
            var snippet = "border-width: medium thin thick";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderWidthProperty>(property);
            var concrete = (CssBorderWidthProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("3px 1px 5px", concrete.Value);
        }

        [Test]
        public void CssBorderWidthLengthLengthLengthLengthLegal()
        {
            var snippet = "border-width:  1px  2px   3px  4px  !important ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-width", property.Name);
            Assert.IsTrue(property.IsImportant);
            Assert.IsInstanceOf<CssBorderWidthProperty>(property);
            var concrete = (CssBorderWidthProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("1px 2px 3px 4px", concrete.Value);
        }

        [Test]
        public void CssBorderWidthLengthInEmZeroLegal()
        {
            var snippet = "border-width:  0.3em 0 ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderWidthProperty>(property);
            var concrete = (CssBorderWidthProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0.3em 0", concrete.Value);
            //Assert.AreEqual(new Length(0.3f, Length.Unit.Em), concrete.Top);
            //Assert.AreEqual(Length.Zero, concrete.Right);
            //Assert.AreEqual(new Length(0.3f, Length.Unit.Em), concrete.Bottom);
            //Assert.AreEqual(Length.Zero, concrete.Left);
        }

        [Test]
        public void CssBorderWidthMediumZeroLengthThickLegal()
        {
            var snippet = "border-width:   medium 0 1px thick ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderWidthProperty>(property);
            var concrete = (CssBorderWidthProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("3px 0 1px 5px", concrete.Value);
            //Assert.AreEqual(Length.Medium, concrete.Top);
            //Assert.AreEqual(Length.Zero, concrete.Right);
            //Assert.AreEqual(new Length(1f, Length.Unit.Px), concrete.Bottom);
            //Assert.AreEqual(Length.Thick, concrete.Left);
        }

        [Test]
        public void CssBorderWidthZerosIllegal()
        {
            var snippet = "border-width: 0 0 0 0 0";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-width", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderWidthProperty>(property);
            var concrete = (CssBorderWidthProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsFalse(concrete.HasValue);
        }

        [Test]
        public void CssBorderLeftZeroLegal()
        {
            var snippet = "border-left:   0 ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-left", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderLeftProperty>(property);
            var concrete = (CssBorderLeftProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0", concrete.Value);
            //Assert.AreEqual(Length.Zero, concrete.Width);
            //Assert.AreEqual(Color.Transparent, concrete.Color);
            //Assert.AreEqual(LineStyle.None, concrete.Style);
        }

        [Test]
        public void CssBorderRightLineStyleLegal()
        {
            var snippet = "border-right :   dotted ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-right", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderRightProperty>(property);
            var concrete = (CssBorderRightProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("dotted", concrete.Value);
            //Assert.AreEqual(Length.Medium, concrete.Width);
            //Assert.AreEqual(Color.Transparent, concrete.Color);
            //Assert.AreEqual(LineStyle.Dotted, concrete.Style);
        }

        [Test]
        public void CssBorderTopLengthRedLegal()
        {
            var snippet = "border-top :  2px red ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-top", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderTopProperty>(property);
            var concrete = (CssBorderTopProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("2px rgb(255, 0, 0)", concrete.Value);
            //Assert.AreEqual(new Length(2f, Length.Unit.Px), concrete.Width);
            //Assert.AreEqual(Color.Red, concrete.Color);
            //Assert.AreEqual(LineStyle.None, concrete.Style);
        }

        [Test]
        public void CssBorderBottomRgbLegal()
        {
            var snippet = "border-bottom :  rgb(255, 100, 0) ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border-bottom", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderBottomProperty>(property);
            var concrete = (CssBorderBottomProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("rgb(255, 100, 0)", concrete.Value);
            //Assert.AreEqual(Length.Medium, concrete.Width);
            //Assert.AreEqual(Color.FromRgb(255, 100, 0), concrete.Color);
            //Assert.AreEqual(LineStyle.None, concrete.Style);
        }

        [Test]
        public void CssBorderGrooveRgbLegal()
        {
            var snippet = "border :  GROOVE rgb(255, 100, 0) ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderProperty>(property);
            var concrete = (CssBorderProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            //Assert.AreEqual("groove rgb(255, 100, 0)", concrete.Value.CssText);
            //Assert.AreEqual(Length.Medium, concrete.Width);
            //Assert.AreEqual(Color.FromRgb(255, 100, 0), concrete.Color);
            //Assert.AreEqual(LineStyle.Groove, concrete.Style);
        }

        [Test]
        public void CssBorderInsetGreenLengthLegal()
        {
            var snippet = "border :  inset  green 3em ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderProperty>(property);
            var concrete = (CssBorderProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("3em inset rgb(0, 128, 0)", concrete.Value);
            //Assert.AreEqual(new Length(3f, Length.Unit.Em), concrete.Width);
            //Assert.AreEqual(Color.Green, concrete.Color);
            //Assert.AreEqual(LineStyle.Inset, concrete.Style);
        }

        [Test]
        public void CssBorderRedSolidLengthLegal()
        {
            var snippet = "border :  red  SOLID 1px ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderProperty>(property);
            var concrete = (CssBorderProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            //Assert.AreEqual("red solid 1px", concrete.Value.CssText);
            //Assert.AreEqual(new Length(1f, Length.Unit.Px), concrete.Width);
            //Assert.AreEqual(Color.Red, concrete.Color);
            //Assert.AreEqual(LineStyle.Solid, concrete.Style);
        }

        [Test]
        public void CssBorderLengthBlackDoubleLegal()
        {
            var snippet = "border :  0.5px black double ";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderProperty>(property);
            var concrete = (CssBorderProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("0.5px double rgb(0, 0, 0)", concrete.Value);
            //Assert.AreEqual(new Length(0.5f, Length.Unit.Px), concrete.Width);
            //Assert.AreEqual(Color.Black, concrete.Color);
            //Assert.AreEqual(LineStyle.Double, concrete.Style);
        }

        [Test]
        public void CssBorderOutSetCurrentColor()
        {
            var snippet = "border: 1px outset currentColor";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderProperty>(property);
            var concrete = (CssBorderProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("1px outset currentColor", concrete.Value);
            //Assert.AreEqual(new Length(1f, Length.Unit.Px), concrete.Width);
            //Assert.AreEqual(Color.Transparent, concrete.Color);
            //Assert.AreEqual(LineStyle.Outset, concrete.Style);
        }

        [Test]
        public void CssBorderOutSetWithNoColor()
        {
            var snippet = "border: 1px outset";
            var property = ParseDeclaration(snippet);
            Assert.AreEqual("border", property.Name);
            Assert.IsFalse(property.IsImportant);
            Assert.IsInstanceOf<CssBorderProperty>(property);
            var concrete = (CssBorderProperty)property;
            Assert.IsFalse(concrete.IsInherited);
            Assert.IsTrue(concrete.HasValue);
            Assert.AreEqual("1px outset", concrete.Value);
            //Assert.AreEqual(new Length(1f, Length.Unit.Px), concrete.Width);
            //Assert.AreEqual(Color.Transparent, concrete.Color);
            //Assert.AreEqual(LineStyle.Outset, concrete.Style);
        }
    }
}
