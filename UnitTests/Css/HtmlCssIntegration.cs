using AngleSharp;
using AngleSharp.DOM.Css;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class HtmlCssIntegrationTests
    {
        [TestMethod]
        public void DetectStylesheet()
        {
            var html = @"<!DOCTYPE html>

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

            var doc = DocumentBuilder.Html(html);
            Assert.AreEqual(1, doc.StyleSheets.Length);
            var css = doc.StyleSheets[0] as CSSStyleSheet;
            Assert.AreEqual(1, css.Rules.Length);
            var style = css.Rules[0] as CSSStyleRule;
            Assert.AreEqual("body", style.SelectorText);
            Assert.AreEqual(1, style.Style.Length);
            var decl = style.Style as CSSStyleDeclaration;
            Assert.IsNotNull(decl);
            var rule = decl.GetProperty("background-color");
            Assert.IsTrue(rule.IsImportant);
            Assert.AreEqual("background-color", rule.Name);
            Assert.AreEqual(rule.Name, decl[0]);
            Assert.AreEqual("green", rule.Value.CssText);
        }
    }
}
