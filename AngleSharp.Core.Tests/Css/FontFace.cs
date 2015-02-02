using AngleSharp.Dom.Css;
using AngleSharp.Parser.Css;
using NUnit.Framework;

namespace AngleSharp.Core.Tests.Css
{
    [TestFixture]
    public class FontFaceTests
    {
        [Test]
        public void FontFaceOpenSansWithSource()
        {
            var src = "@font-face{font-family:'Open Sans';src:url(fonts/OpenSans-Light.eot);src:local('Open Sans Light'),local('OpenSans-Light'),url(fonts/OpenSans-Light.ttf) format('truetype'),url(fonts/OpenSans-Light.woff) format('woff');font-style:normal}";
            var css = CssParser.ParseStyleSheet(src);
            Assert.IsNotNull(css);
            Assert.AreEqual(1, css.Rules.Length);
            Assert.IsInstanceOf<CssFontFaceRule>(css.Rules[0]);
            var fontface = (CssFontFaceRule)css.Rules[0];
            Assert.AreEqual(3, fontface.Style.Length);
        }

        [Test]
        public void FontFaceOpenSansNoSource()
        {
            var src = "@font-face{font-family:'Open Sans';font-style:normal}";
            var css = CssParser.ParseStyleSheet(src);
            Assert.IsNotNull(css);
            Assert.AreEqual(1, css.Rules.Length);
            Assert.IsInstanceOf<CssFontFaceRule>(css.Rules[0]);
            var fontface = (CssFontFaceRule)css.Rules[0];
            Assert.AreEqual(2, fontface.Style.Length);
        }
    }
}
