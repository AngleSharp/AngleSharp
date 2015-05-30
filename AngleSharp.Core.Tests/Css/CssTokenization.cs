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
            var tokenizer = new CssTokenizer(new TextSource(teststring), null);
            var token = tokenizer.Get();
            Assert.AreEqual(CssTokenType.Ident, token.Type);
        }

        [Test]
        public void CssParserAtRule()
        {
            var teststring = "@media { background: blue; }";
            var tokenizer = new CssTokenizer(new TextSource(teststring), null);
            var token = tokenizer.Get();
            Assert.AreEqual(CssTokenType.AtKeyword, token.Type);
        }

        [Test]
        public void CssParserUrlUnquoted()
        {
            var url = "http://someurl";
            var teststring = "url(" + url + ")";
            var tokenizer = new CssTokenizer(new TextSource(teststring), null);
            var token = tokenizer.Get();
            Assert.AreEqual(url, token.Data);
        }

        [Test]
        public void CssParserUrlDoubleQuoted()
        {
            var url = "http://someurl";
            var teststring = "url(\"" + url + "\")";
            var tokenizer = new CssTokenizer(new TextSource(teststring), null);
            var token = tokenizer.Get();
            Assert.AreEqual(url, token.Data);
        }

        [Test]
        public void CssParserUrlSingleQuoted()
        {
            var url = "http://someurl";
            var teststring = "url('" + url + "')";
            var tokenizer = new CssTokenizer(new TextSource(teststring), null);
            var token = tokenizer.Get();
            Assert.AreEqual(url, token.Data);
        }
    }
}
