namespace AngleSharp.Core.Tests
{
    using AngleSharp.Core.Tests.Mocks;
    using AngleSharp.Dom.Css;
    using AngleSharp.Dom.Html;
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;
    using AngleSharp.Parser.Html;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

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

        [Test]
        public async Task CssWithImportRuleShouldBeAbleToHandleNestedStylesheets()
        {
            var files = new Dictionary<String, String>
            {
                { "index.html", "<!doctype html><html><link rel=stylesheet href=origin.css type=text/css><style>@import url('linked2.css');</style>" },
                { "origin.css", "@import url(linked1.css);" },
                { "linked1.css", "" },
                { "linked2.css", "@import url(\"linked3.css\"); @import 'linked4.css';" },
                { "linked3.css", "" },
                { "linked4.css", "" },
            };
            var requester = new TestServerRequester(files);
            var config = Configuration.Default.WithDefaultLoader(setup => setup.IsResourceLoadingEnabled = true, new [] { requester }).WithCss();
            var document = await BrowsingContext.New(config).OpenAsync("http://localhost/index.html");
            var link = document.QuerySelector<IHtmlLinkElement>("link");
            var style = document.QuerySelector<IHtmlStyleElement>("style");

            await Task.Delay(100);

            Assert.IsNotNull(link);
            Assert.IsNotNull(style);

            var origin = link.Sheet as ICssStyleSheet;
            Assert.IsNotNull(origin);
            Assert.AreEqual("http://localhost/origin.css", origin.Href);
            Assert.AreEqual(1, origin.Rules.Length);
            Assert.AreEqual(CssRuleType.Import, origin.Rules[0].Type);

            var linked1 = (origin.Rules[0] as ICssImportRule).Sheet;
            Assert.IsNotNull(linked1);
            Assert.AreEqual("http://localhost/linked1.css", linked1.Href);
            Assert.AreEqual(0, linked1.Rules.Length);

            var styleSheet = style.Sheet as ICssStyleSheet;
            Assert.IsNotNull(styleSheet);
            Assert.AreEqual(null, styleSheet.Href);
            Assert.AreEqual(1, styleSheet.Rules.Length);
            Assert.AreEqual(CssRuleType.Import, styleSheet.Rules[0].Type);

            var linked2 = (styleSheet.Rules[0] as ICssImportRule).Sheet;
            Assert.IsNotNull(linked2);
            Assert.AreEqual("http://localhost/linked2.css", linked2.Href);
            Assert.AreEqual(2, linked2.Rules.Length);
            Assert.AreEqual(CssRuleType.Import, linked2.Rules[0].Type);
            Assert.AreEqual(CssRuleType.Import, linked2.Rules[1].Type);

            var linked3 = (linked2.Rules[0] as ICssImportRule).Sheet;
            Assert.IsNotNull(linked3);
            Assert.AreEqual("http://localhost/linked3.css", linked3.Href);
            Assert.AreEqual(0, linked3.Rules.Length);

            var linked4 = (linked2.Rules[1] as ICssImportRule).Sheet;
            Assert.IsNotNull(linked4);
            Assert.AreEqual("http://localhost/linked4.css", linked4.Href);
            Assert.AreEqual(0, linked4.Rules.Length);
        }

        [Test]
        public async Task CssWithImportRuleShouldStopRecursion()
        {
            var files = new Dictionary<String, String>
            {
                { "index.html", "<!doctype html><html><link rel=stylesheet href=origin.css type=text/css>" },
                { "origin.css", "@import url(linked.css);" },
                { "linked.css", "@import url(origin.css);" },
            };
            var requester = new TestServerRequester(files);
            var config = Configuration.Default.WithDefaultLoader(setup => setup.IsResourceLoadingEnabled = true, new[] { requester }).WithCss();
            var document = await BrowsingContext.New(config).OpenAsync("http://localhost/index.html");
            var link = document.QuerySelector<IHtmlLinkElement>("link");

            await Task.Delay(100);

            Assert.IsNotNull(link);

            var origin = link.Sheet as ICssStyleSheet;
            Assert.IsNotNull(origin);
            Assert.AreEqual("http://localhost/origin.css", origin.Href);
            Assert.AreEqual(1, origin.Rules.Length);
            Assert.AreEqual(CssRuleType.Import, origin.Rules[0].Type);

            var linked = (origin.Rules[0] as ICssImportRule).Sheet;
            Assert.IsNotNull(linked);
            Assert.AreEqual("http://localhost/linked.css", linked.Href);
            Assert.AreEqual(1, linked.Rules.Length);

            var originAborted = (linked.Rules[0] as ICssImportRule).Sheet;
            Assert.IsNull(originAborted);
        }

        [Test]
        public async Task StylePropertyOfElementFromDocumentWithCssShouldNotBeNull()
        {
            var config = Configuration.Default.WithCss();
            var document = await BrowsingContext.New(config).OpenNewAsync();
            var div = document.CreateElement<IHtmlDivElement>();
            Assert.IsNotNull(div.Style);
        }

        [Test]
        public async Task StylePropertyOfClonedElementShouldNotBeNull()
        {
            var config = Configuration.Default.WithCss();
            var document = await BrowsingContext.New(config).OpenNewAsync();
            var div = document.CreateElement<IHtmlDivElement>();
            var clone = div.Clone(true) as IHtmlDivElement;
            Assert.IsNotNull(clone);
            Assert.IsNotNull(clone.Style);
        }
    }
}
