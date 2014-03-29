using AngleSharp.Parser;
using AngleSharp.Parser.Css;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class CssTokenizationTests
    {
        [TestMethod]
        public void CssParserIdentifier()
        {
            var teststring = "h1 { background: blue; }";
            var parser = new CssTokenizer(new SourceManager(teststring));
            var list = parser.Tokens;
            CssToken token = null;

            foreach (var item in list)
            {
                token = item;
                break;
            }

            Assert.AreEqual(CssTokenType.Ident, token.Type);
        }

        [TestMethod]
        public void CssParserAtRule()
        {
            var teststring = "@media { background: blue; }";
            var parser = new CssTokenizer(new SourceManager(teststring));
            var list = parser.Tokens;
            CssToken token = null;

            foreach (var item in list)
            {
                token = item;
                break;
            }

            Assert.AreEqual(CssTokenType.AtKeyword, token.Type);
        }

        [TestMethod]
        public void CssParserUrlUnquoted()
        {
            var url = "http://someurl";
            var teststring = "url(" + url + ")";
            var parser = new CssTokenizer(new SourceManager(teststring));
            var list = parser.Tokens;
            CssStringToken token = null;

            foreach (var item in list)
            {
                token = item as CssStringToken;
                break;
            }

            Assert.AreEqual(url, token.Data);
        }

        [TestMethod]
        public void CssParserUrlDoubleQuoted()
        {
            var url = "http://someurl";
            var teststring = "url(\"" + url + "\")";
            var parser = new CssTokenizer(new SourceManager(teststring));
            var list = parser.Tokens;
            CssStringToken token = null;

            foreach (var item in list)
            {
                token = item as CssStringToken;
                break;
            }

            Assert.AreEqual(url, token.Data);
        }

        [TestMethod]
        public void CssParserUrlSingleQuoted()
        {
            var url = "http://someurl";
            var teststring = "url('" + url + "')";
            var parser = new CssTokenizer(new SourceManager(teststring));
            var list = parser.Tokens;
            CssStringToken token = null;

            foreach (var item in list)
            {
                token = item as CssStringToken;
                break;
            }

            Assert.AreEqual(url, token.Data);
        }
    }
}
