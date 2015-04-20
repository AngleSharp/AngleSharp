using AngleSharp.Dom.Css;
using NUnit.Framework;

namespace AngleSharp.Core.Tests
{
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
            Assert.AreEqual("green", rule.Value.CssText);
        }
    }
}
