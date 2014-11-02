using AngleSharp.DOM.Css;
using AngleSharp.Parser.Css;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.Css
{
    [TestClass]
    public class FontFaceTests
    {
        [TestMethod]
        public void FontFaceOpenSansWithSource()
        {
            var src = "@font-face{font-family:'Open Sans';src:url(fonts/OpenSans-Light.eot);src:local('Open Sans Light'),local('OpenSans-Light'),url(fonts/OpenSans-Light.ttf) format('truetype'),url(fonts/OpenSans-Light.woff) format('woff');font-style:normal}";
            var css = CssParser.ParseStyleSheet(src);
            Assert.IsNotNull(css);
            Assert.AreEqual(1, css.Rules.Length);
            Assert.IsInstanceOfType(css.Rules[0], typeof(CSSFontFaceRule));
            var fontface = (CSSFontFaceRule)css.Rules[0];
            Assert.AreEqual(3, fontface.Style.Length);
        }

        [TestMethod]
        public void FontFaceOpenSansNoSource()
        {
            var src = "@font-face{font-family:'Open Sans';font-style:normal}";
            var css = CssParser.ParseStyleSheet(src);
            Assert.IsNotNull(css);
            Assert.AreEqual(1, css.Rules.Length);
            Assert.IsInstanceOfType(css.Rules[0], typeof(CSSFontFaceRule));
            var fontface = (CSSFontFaceRule)css.Rules[0];
            Assert.AreEqual(2, fontface.Style.Length);
        }
    }
}
