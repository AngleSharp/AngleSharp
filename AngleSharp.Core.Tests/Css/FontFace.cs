namespace AngleSharp.Core.Tests.Css
{
    using AngleSharp.Dom.Css;
    using AngleSharp.Parser.Css;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class FontFaceTests
    {
        static ICssStyleSheet ParseStyleSheet(String source)
        {
            var parser = new CssParser();
            return parser.ParseStylesheet(source);
        }

        [Test]
        public void FontFaceOpenSansWithSource()
        {
            var src = "@font-face{font-family:'Open Sans';src:url(fonts/OpenSans-Light.eot);src:local('Open Sans Light'),local('OpenSans-Light'),url(fonts/OpenSans-Light.ttf) format('truetype'),url(fonts/OpenSans-Light.woff) format('woff');font-style:normal}";
            var sheet = ParseStyleSheet(src);
            Assert.IsNotNull(sheet);
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<CssFontFaceRule>(sheet.Rules[0]);
            var fontface = (ICssFontFaceRule)sheet.Rules[0];
            Assert.AreEqual("\"Open Sans\"", fontface.Family);
            Assert.AreEqual("", fontface.Features);
            Assert.AreEqual("", fontface.Range);
            Assert.AreNotEqual("", fontface.Source);
            Assert.AreEqual("", fontface.Stretch);
            Assert.AreEqual("normal", fontface.Style);
            Assert.AreEqual("", fontface.Variant);
            Assert.AreEqual("", fontface.Weight);
        }

        [Test]
        public void FontFaceOpenSansNoSource()
        {
            var src = "@font-face{font-family:'Open Sans';font-style:normal}";
            var sheet = ParseStyleSheet(src);
            Assert.IsNotNull(sheet);
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<CssFontFaceRule>(sheet.Rules[0]);
            var fontface = (ICssFontFaceRule)sheet.Rules[0];
            Assert.AreEqual("\"Open Sans\"", fontface.Family);
            Assert.AreEqual("", fontface.Features);
            Assert.AreEqual("", fontface.Range);
            Assert.AreEqual("", fontface.Source);
            Assert.AreEqual("", fontface.Stretch);
            Assert.AreEqual("normal", fontface.Style);
            Assert.AreEqual("", fontface.Variant);
            Assert.AreEqual("", fontface.Weight);
        }
    }
}
