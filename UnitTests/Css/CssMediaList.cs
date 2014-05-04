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
        public void AllFeatureMinWidthMediaList()
        {
            var source = @"@media all (max-width:30px) {
    h1 { color: green }
}";
            var sheet = CssParser.ParseStyleSheet(source);
            Assert.AreEqual(1, sheet.CssRules.Length);
            Assert.IsInstanceOfType(sheet.CssRules[0], typeof(CSSMediaRule));
            var media = (CSSMediaRule)sheet.CssRules[0];
            Assert.AreEqual("all", media.ConditionText);
            var list = media.Media;
            Assert.AreEqual(1, list.Length);
            Assert.AreEqual(1, media.CssRules.Length);
        }
    }
}
