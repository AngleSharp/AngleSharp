namespace AngleSharp.Core.Tests
{
    using AngleSharp.Dom.Css;
    using AngleSharp.Dom.Html;
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;
    using AngleSharp.Parser.Html;
    using NUnit.Framework;

    [TestFixture]
    public class HtmlCssIntegrationTests
    {
        [Test]
        public void DetectStylesheet()
        {
            var source = @"<!DOCTYPE html>

<html>
<head>
    <meta charset=""utf-8"" />
    <title></title>
    <style>
        body
        {
            background-color: green !important;
        }
    </style>
</head>
<body>
</body>
</html>";

            var config = new Configuration().WithCss();
            var doc = source.ToHtmlDocument(config);
            Assert.AreEqual(1, doc.StyleSheets.Length);
            var css = doc.StyleSheets[0] as CssStyleSheet;
            Assert.AreEqual(1, css.Rules.Length);
            var style = css.Rules[0] as CssStyleRule;
            Assert.AreEqual("body", style.SelectorText);
            Assert.AreEqual(1, style.Style.Length);
            var decl = style.Style as CssStyleDeclaration;
            Assert.IsNotNull(decl);
            var rule = decl.GetProperty("background-color");
            Assert.IsTrue(rule.IsImportant);
            Assert.AreEqual("background-color", rule.Name);
            Assert.AreEqual(rule.Name, decl[0]);
            Assert.AreEqual("rgb(0, 128, 0)", rule.Value);
        }

        [Test]
        public void ParsedCssCanHaveExtraWhitespace()
        {
            var html = "<div style=\"background-color: http://www.codeplex.com?url=<!--[if gte IE 4]><SCRIPT>alert('XSS');</SCRIPT><![endif]-->\">";
            var parser = new HtmlParser(Configuration.Default.WithCss(e => e.Options = new CssParserOptions
            {
                IsIncludingUnknownDeclarations = true,
                IsIncludingUnknownRules = true,
                IsToleratingInvalidConstraints = true,
                IsToleratingInvalidValues = true
            }));
            var dom = parser.Parse(html);
            var div = dom.QuerySelector<IHtmlElement>("div");
            Assert.AreEqual("http://www.codeplex.com?url=<!--[if gte IE 4]><SCRIPT>alert(\"XSS\")", div.Style["background-color"]);
            Assert.AreEqual("background-color: http://www.codeplex.com?url=<!--[if gte IE 4]><SCRIPT>alert(\"XSS\")", div.Style.CssText);
        }
    }
}
