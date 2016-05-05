namespace AngleSharp.Core.Tests.Css
{
    using AngleSharp.Dom.Css;
    using AngleSharp.Parser.Css;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class CssContentPropertyTests
    {
        static ICssStyleRule Parse(String source)
        {
            var parser = new CssParser();
            return parser.ParseStylesheet(source).Rules[0] as ICssStyleRule;
        }

        [Test]
        public void CssContentParseStringWithDoubleQuoteEscape()
        {
            var source = "a{content:\"\\\"\"}";
            var parsed = Parse(source);
            Assert.AreEqual("\"\\\"\"", parsed.Style.Content);
        }

        [Test]
        public void CssContentParseStringWithSingleQuoteEscape()
        {
            var source = "a{content:'\\''}";
            var parsed = Parse(source);
            Assert.AreEqual("\"'\"", parsed.Style.Content);
        }

        [Test]
        public void CssContentParseStringWithDoubleQuoteMultipleEscapes()
        {
            var source = "a{content:\"abc\\\"\\\"d\\\"ef\"}";
            var parsed = Parse(source);
            Assert.AreEqual("\"abc\\\"\\\"d\\\"ef\"", parsed.Style.Content);
        }

        [Test]
        public void CssContentParseStringWithSingleQuoteMultipleEscapes()
        {
            var source = "a{content:'abc\\'\\'d\\'ef'}";
            var parsed = Parse(source);
            Assert.AreEqual("\"abc''d'ef\"", parsed.Style.Content);
        }
    }
}
