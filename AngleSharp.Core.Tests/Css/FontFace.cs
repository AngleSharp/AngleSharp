using AngleSharp.Dom.Css;
using AngleSharp.Parser.Css;
using NUnit.Framework;
using System;

namespace AngleSharp.Core.Tests.Css
{
    [TestFixture]
    public class FontFaceTests
    {
        static ICssStyleSheet ParseStyleSheet(String source)
        {
            var parser = new CssParser(source);
            return parser.Parse();
        }

        [Test]
        public void FontFaceOpenSansWithSource()
        {
            var src = "@font-face{font-family:'Open Sans';src:url(fonts/OpenSans-Light.eot);src:local('Open Sans Light'),local('OpenSans-Light'),url(fonts/OpenSans-Light.ttf) format('truetype'),url(fonts/OpenSans-Light.woff) format('woff');font-style:normal}";
            var sheet = ParseStyleSheet(src);
            Assert.IsNotNull(sheet);
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<CssFontFaceRule>(sheet.Rules[0]);
            var fontface = (CssFontFaceRule)sheet.Rules[0];
            Assert.AreEqual(3, fontface.Style.Length);
        }

        [Test]
        public void FontFaceOpenSansNoSource()
        {
            var src = "@font-face{font-family:'Open Sans';font-style:normal}";
            var sheet = ParseStyleSheet(src);
            Assert.IsNotNull(sheet);
            Assert.AreEqual(1, sheet.Rules.Length);
            Assert.IsInstanceOf<CssFontFaceRule>(sheet.Rules[0]);
            var fontface = (CssFontFaceRule)sheet.Rules[0];
            Assert.AreEqual(2, fontface.Style.Length);
        }
    }
}
