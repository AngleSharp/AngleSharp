using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AngleSharp.Parser.Css;
using AngleSharp.DOM.Css;

namespace UnitTests.Css
{
    [TestClass]
    public class CssMediaListTests
    {
        [TestMethod]
        public void SimpleScreenMediaList()
        {
            var source = @"@media screen {
    h1 { color: green }
}";
            var sheet = CssParser.ParseStyleSheet(source);
            Assert.AreEqual(1, sheet.CssRules.Length);
            Assert.IsInstanceOfType(sheet.CssRules[0], typeof(CSSMediaRule));
            var media = (CSSMediaRule)sheet.CssRules[0];
            Assert.AreEqual("screen", media.ConditionText);
            var list = media.Media;
            Assert.AreEqual(1, list.Length);
            Assert.AreEqual(1, media.CssRules.Length);
        }

        [TestMethod]
        public void SimpleScreenTvMediaList()
        {
            var source = @"@media screen,tv {
    h1 { color: green }
}";
            var sheet = CssParser.ParseStyleSheet(source);
            Assert.AreEqual(1, sheet.CssRules.Length);
            Assert.IsInstanceOfType(sheet.CssRules[0], typeof(CSSMediaRule));
            var media = (CSSMediaRule)sheet.CssRules[0];
            Assert.AreEqual("screen, tv", media.ConditionText);
            var list = media.Media;
            Assert.AreEqual(2, list.Length);
            Assert.AreEqual(1, media.CssRules.Length);
        }

        [TestMethod]
        public void OnlyScreenTvMediaList()
        {
            var source = @"@media only screen,tv {
    h1 { color: green }
}";
            var sheet = CssParser.ParseStyleSheet(source);
            Assert.AreEqual(1, sheet.CssRules.Length);
            Assert.IsInstanceOfType(sheet.CssRules[0], typeof(CSSMediaRule));
            var media = (CSSMediaRule)sheet.CssRules[0];
            Assert.AreEqual("only screen, tv", media.ConditionText);
            var list = media.Media;
            Assert.AreEqual(2, list.Length);
            Assert.AreEqual(1, media.CssRules.Length);
        }

        [TestMethod]
        public void NotScreenTvMediaList()
        {
            var source = @"@media not screen,tv {
    h1 { color: green }
}";
            var sheet = CssParser.ParseStyleSheet(source);
            Assert.AreEqual(1, sheet.CssRules.Length);
            Assert.IsInstanceOfType(sheet.CssRules[0], typeof(CSSMediaRule));
            var media = (CSSMediaRule)sheet.CssRules[0];
            Assert.AreEqual("not screen, tv", media.ConditionText);
            var list = media.Media;
            Assert.AreEqual(2, list.Length);
            Assert.AreEqual(1, media.CssRules.Length);
        }

        [TestMethod]
        public void FeatureMinWidthMediaList()
        {
            var source = @"@media (min-width:30px) {
    h1 { color: green }
}";
            var sheet = CssParser.ParseStyleSheet(source);
            Assert.AreEqual(1, sheet.CssRules.Length);
            Assert.IsInstanceOfType(sheet.CssRules[0], typeof(CSSMediaRule));
            var media = (CSSMediaRule)sheet.CssRules[0];
            Assert.AreEqual("(min-width: 30px)", media.ConditionText);
            var list = media.Media;
            Assert.AreEqual(1, list.Length);
            Assert.AreEqual(1, media.CssRules.Length);
        }

        [TestMethod]
        public void OnlyFeatureWidthMediaList()
        {
            var source = @"@media only (width: 640px) {
    h1 { color: green }
}";
            var sheet = CssParser.ParseStyleSheet(source);
            Assert.AreEqual(1, sheet.CssRules.Length);
            Assert.IsInstanceOfType(sheet.CssRules[0], typeof(CSSMediaRule));
            var media = (CSSMediaRule)sheet.CssRules[0];
            Assert.AreEqual("only (width: 640px)", media.ConditionText);
            var list = media.Media;
            Assert.AreEqual(1, list.Length);
            Assert.AreEqual(1, media.CssRules.Length);
        }

        [TestMethod]
        public void NotFeatureDeviceWidthMediaList()
        {
            var source = @"@media not (device-width: 640px) {
    h1 { color: green }
}";
            var sheet = CssParser.ParseStyleSheet(source);
            Assert.AreEqual(1, sheet.CssRules.Length);
            Assert.IsInstanceOfType(sheet.CssRules[0], typeof(CSSMediaRule));
            var media = (CSSMediaRule)sheet.CssRules[0];
            Assert.AreEqual("not (device-width: 640px)", media.ConditionText);
            var list = media.Media;
            Assert.AreEqual(1, list.Length);
            Assert.AreEqual(1, media.CssRules.Length);
        }

        [TestMethod]
        public void AllFeatureMaxWidthMediaListMissingAnd()
        {
            var source = @"@media all (max-width:30px) {
    h1 { color: red }
}";
            var sheet = CssParser.ParseStyleSheet(source);
            Assert.AreEqual(0, sheet.CssRules.Length);
        }

        [TestMethod]
        public void NoMediaQueryGivenSkip()
        {
            var source = @"@media {
    h1 { color: red }
}";
            var sheet = CssParser.ParseStyleSheet(source);
            Assert.AreEqual(0, sheet.CssRules.Length);
        }

        [TestMethod]
        public void NotNoMediaTypeOrExpressionSkip()
        {
            var source = @"@media not {
    h1 { color: red }
}";
            var sheet = CssParser.ParseStyleSheet(source);
            Assert.AreEqual(0, sheet.CssRules.Length);
        }

        [TestMethod]
        public void OnlyNoMediaTypeOrExpressionSkip()
        {
            var source = @"@media only {
    h1 { color: red }
}";
            var sheet = CssParser.ParseStyleSheet(source);
            Assert.AreEqual(0, sheet.CssRules.Length);
        }

        [TestMethod]
        public void MediaFeatureMissingSkip()
        {
            var source = @"@media () {
    h1 { color: red }
}";

            var sheet = CssParser.ParseStyleSheet(source);
            Assert.AreEqual(0, sheet.CssRules.Length);
        }

        [TestMethod]
        public void MediaFeatureMissingSkipReadNext()
        {
            var source = @"@media () {
    h1 { color: red }
}
h1 { color: green }";
            var sheet = CssParser.ParseStyleSheet(source);
            Assert.AreEqual(1, sheet.CssRules.Length);
            Assert.IsInstanceOfType(sheet.CssRules[0], typeof(CSSStyleRule));
            var style = (CSSStyleRule)sheet.CssRules[0];
            Assert.AreEqual("green", style.Style.Color);
            Assert.AreEqual("h1", style.SelectorText);
        }

        [TestMethod]
        public void FeatureMaxWidthMediaListMissingConnectedAnd()
        {
            var source = @"@media (max-width:30px) (min-width:10px) {
    h1 { color: red }
}";
            var sheet = CssParser.ParseStyleSheet(source);
            Assert.AreEqual(0, sheet.CssRules.Length);
        }

        [TestMethod]
        public void TvScreenMediaListMissingComma()
        {
            var source = @"@media tv screen {
    h1 { color: red }
}";
            var sheet = CssParser.ParseStyleSheet(source);
            Assert.AreEqual(0, sheet.CssRules.Length);
        }

        [TestMethod]
        public void AllFeatureMaxWidthMediaListWithAndKeyword()
        {
            var source = @"@media all and (max-width:30px) {
    h1 { color: green }
}";
            var sheet = CssParser.ParseStyleSheet(source);
            Assert.AreEqual(1, sheet.CssRules.Length);
            Assert.IsInstanceOfType(sheet.CssRules[0], typeof(CSSMediaRule));
            var media = (CSSMediaRule)sheet.CssRules[0];
            Assert.AreEqual("all and (max-width: 30px)", media.ConditionText);
            var list = media.Media;
            Assert.AreEqual(1, list.Length);
            Assert.AreEqual(1, media.CssRules.Length);
        }

        [TestMethod]
        public void FeatureAspectRatioMediaList()
        {
            var source = @"@media (aspect-ratio: 16/9) {
    h1 { color: green }
}";
            var sheet = CssParser.ParseStyleSheet(source);
            Assert.AreEqual(1, sheet.CssRules.Length);
            Assert.IsInstanceOfType(sheet.CssRules[0], typeof(CSSMediaRule));
            var media = (CSSMediaRule)sheet.CssRules[0];
            Assert.AreEqual("(aspect-ratio: 16 / 9)", media.ConditionText);
            var list = media.Media;
            Assert.AreEqual(1, list.Length);
            Assert.AreEqual(1, media.CssRules.Length);
        }

        [TestMethod]
        public void PrintFeatureMaxWidthAndMinDeviceWidthMediaList()
        {
            var source = @"@media print and (max-width:30px) and (min-device-width:100px) {
    h1 { color: green }
}";
            var sheet = CssParser.ParseStyleSheet(source);
            Assert.AreEqual(1, sheet.CssRules.Length);
            Assert.IsInstanceOfType(sheet.CssRules[0], typeof(CSSMediaRule));
            var media = (CSSMediaRule)sheet.CssRules[0];
            Assert.AreEqual("print and (max-width: 30px) and (min-device-width: 100px)", media.ConditionText);
            var list = media.Media;
            Assert.AreEqual(1, list.Length);
            Assert.AreEqual(1, media.CssRules.Length);
        }

        [TestMethod]
        public void AllFeatureMinWidthAndMinDeviceWidthScreenMediaList()
        {
            var source = @"@media all and (min-width:0) and (min-device-width:100px), screen {
    h1 { color: green }
}";
            var sheet = CssParser.ParseStyleSheet(source);
            Assert.AreEqual(1, sheet.CssRules.Length);
            Assert.IsInstanceOfType(sheet.CssRules[0], typeof(CSSMediaRule));
            var media = (CSSMediaRule)sheet.CssRules[0];
            Assert.AreEqual("all and (min-width: 0) and (min-device-width: 100px), screen", media.ConditionText);
            var list = media.Media;
            Assert.AreEqual(2, list.Length);
            Assert.AreEqual(1, media.CssRules.Length);
        }

        [TestMethod]
        public void ImplicitAllFeatureResolutionMediaList()
        {
            var source = @"@media (resolution:72dpi) {
    h1 { color: green }
}";
            var sheet = CssParser.ParseStyleSheet(source);
            Assert.AreEqual(1, sheet.CssRules.Length);
            Assert.IsInstanceOfType(sheet.CssRules[0], typeof(CSSMediaRule));
            var media = (CSSMediaRule)sheet.CssRules[0];
            Assert.AreEqual("(resolution: 72dpi)", media.ConditionText);
            var list = media.Media;
            Assert.AreEqual(1, list.Length);
            Assert.AreEqual(1, media.CssRules.Length);
        }

        [TestMethod]
        public void ImplicitAllFeatureMinResolutionAndMaxResolutionMediaList()
        {
            var source = @"@media (min-resolution:72dpi) and (max-resolution:140dpi) {
    h1 { color: green }
}";
            var sheet = CssParser.ParseStyleSheet(source);
            Assert.AreEqual(1, sheet.CssRules.Length);
            Assert.IsInstanceOfType(sheet.CssRules[0], typeof(CSSMediaRule));
            var media = (CSSMediaRule)sheet.CssRules[0];
            Assert.AreEqual("(min-resolution: 72dpi) and (max-resolution: 140dpi)", media.ConditionText);
            var list = media.Media;
            Assert.AreEqual(1, list.Length);
            Assert.AreEqual(1, media.CssRules.Length);
        }
    }
}
