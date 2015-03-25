using AngleSharp;
using AngleSharp.Parser;
using AngleSharp.Parser.Css;
using NUnit.Framework;

namespace AngleSharp.Core.Tests
{
    [TestFixture]
    public class CssTokenizationTests
    {
        [Test]
        public void CssParserIdentifier()
        {
            var teststring = "h1 { background: blue; }";
            var parser = new CssTokenizer(new TextSource(teststring), null);
            var list = parser.Tokens;
            CssToken token = null;

            foreach (var item in list)
            {
                token = item;
                break;
            }

            Assert.AreEqual(CssTokenType.Ident, token.Type);
        }

        [Test]
        public void CssParserAtRule()
        {
            var teststring = "@media { background: blue; }";
            var parser = new CssTokenizer(new TextSource(teststring), null);
            var list = parser.Tokens;
            CssToken token = null;

            foreach (var item in list)
            {
                token = item;
                break;
            }

            Assert.AreEqual(CssTokenType.AtKeyword, token.Type);
        }

        [Test]
        public void CssParserUrlUnquoted()
        {
            var url = "http://someurl";
            var teststring = "url(" + url + ")";
            var parser = new CssTokenizer(new TextSource(teststring), null);
            var list = parser.Tokens;
            CssStringToken token = null;

            foreach (var item in list)
            {
                token = item as CssStringToken;
                break;
            }

            Assert.AreEqual(url, token.Data);
        }

        [Test]
        public void CssParserUrlDoubleQuoted()
        {
            var url = "http://someurl";
            var teststring = "url(\"" + url + "\")";
            var parser = new CssTokenizer(new TextSource(teststring), null);
            var list = parser.Tokens;
            CssStringToken token = null;

            foreach (var item in list)
            {
                token = item as CssStringToken;
                break;
            }

            Assert.AreEqual(url, token.Data);
        }

        [Test]
        public void CssParserUrlSingleQuoted()
        {
            var url = "http://someurl";
            var teststring = "url('" + url + "')";
            var parser = new CssTokenizer(new TextSource(teststring), null);
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
