namespace AngleSharp.Core.Tests.Css
{
    using AngleSharp.Css.Parser;
    using AngleSharp.Text;
    using NUnit.Framework;

    [TestFixture]
    public class CssTokenizationTests
    {
        [Test]
        public void CssParserIdentifier()
        {
            var teststring = "h1 { background: blue; }";
            var tokenizer = new CssTokenizer(new TextSource(teststring));
            var token = tokenizer.Get();
            Assert.AreEqual(CssTokenType.Ident, token.Type);
        }

        [Test]
        public void CssParserAtRule()
        {
            var teststring = "@media { background: blue; }";
            var tokenizer = new CssTokenizer(new TextSource(teststring));
            var token = tokenizer.Get();
            Assert.AreEqual(CssTokenType.AtKeyword, token.Type);
        }

        [Test]
        public void CssParserUrlUnquoted()
        {
            var url = "http://someurl";
            var teststring = "url(" + url + ")";
            var tokenizer = new CssTokenizer(new TextSource(teststring));
            var token = tokenizer.Get();
            Assert.AreEqual(url, token.Data);
        }

        [Test]
        public void CssParserUrlDoubleQuoted()
        {
            var url = "http://someurl";
            var teststring = "url(\"" + url + "\")";
            var tokenizer = new CssTokenizer(new TextSource(teststring));
            var token = tokenizer.Get();
            Assert.AreEqual(url, token.Data);
        }

        [Test]
        public void CssParserUrlSingleQuoted()
        {
            var url = "http://someurl";
            var teststring = "url('" + url + "')";
            var tokenizer = new CssTokenizer(new TextSource(teststring));
            var token = tokenizer.Get();
            Assert.AreEqual(url, token.Data);
        }

        [Test]
        public void CssTokenizerOnlyCarriageReturn()
        {
            var teststring = "\r";
            var tokenizer = new CssTokenizer(new TextSource(teststring));
            var token = tokenizer.Get();
            Assert.AreEqual("\n", token.Data);
        }

        [Test]
        public void CssTokenizerCarriageReturnLineFeed()
        {
            var teststring = "\r\n";
            var tokenizer = new CssTokenizer(new TextSource(teststring));
            var token = tokenizer.Get();
            Assert.AreEqual("\n", token.Data);
        }

        [Test]
        public void CssTokenizerOnlyLineFeed()
        {
            var teststring = "\n";
            var tokenizer = new CssTokenizer(new TextSource(teststring));
            var token = tokenizer.Get();
            Assert.AreEqual("\n", token.Data);
        }
    }
}
