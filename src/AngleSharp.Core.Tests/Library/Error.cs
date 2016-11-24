namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Core.Tests.Mocks;
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using AngleSharp.Html.Dom.Events;
    using AngleSharp.Html.Parser;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [TestFixture]
    public class ErrorTests
    {
        [Test]
        public async Task GivenLineNumbersShouldBeCorrect()
        {
            var source = @"<article class=""grid-item large"">
    <div class=""grid-image""><a href=""/News/Page/298/cpp-mva-course""><img src=""/img/news/maxresdefault700x240.png"" alt=""Icon"" title=""C++ MVA Course"" /></a></div>
    <div class=""grid-title""><a href=""/News/Page/298/cpp-mva-course"">C++ MVA Course</a></div>
    <div class=""grid-abstract"">My Microsoft Virtual Academy course about modern C++ is now available.</div>
    <div class=""grid-date"">6/5/2015</div>
        <div class=""grid-admin"">        <a href=""/Page/Delete/298"">Delete</a> | <a href=""/Page/Edit/298"">Edit</a> | <a href=""/Page/Create?parentId=1"">Create New</a>
</div>
    </article>
<article class=""grid-item medium"">
    <div class=""grid-image""><a href=""/News/Page/297/dotnet-developer-conference""><img src=""/img/news/8640.pic23150x150.png"" alt=""Icon"" title="".NET Developer Conference"" /></a></div>
    <div class=""grid-title""><a href=""/News/Page/297/dotnet-developer-conference"">.NET Developer Conference</a></div>
    <div class=""grid-abstract"">The sixth edition of the DDC features many interesting talks.</div>
    <div class=""grid-date"">5/28/2015</div>
        <div class=""grid-admin"">        <a href=""/Page/Delete/297"">Delete</a> | <a href=""/Page/Edit/297"">Edit</a> | <a href=""/Page/Create?parentId=1"">Create New</a>
</div>
    </article>
<article class=""grid-item normal even"">
    <div class=""grid-image""><a href=""/News/Page/296/spider-language""><img src=""/img/news/programming-languages_2150x150.png"" alt=""Icon"" title=""Spider Language"" /></a></div>
    <div class=""grid-title""><a href=""/News/Page/296/spider-language"">Spider Language</a></div>
    <div class=""grid-abstract"">Bored of JavaScript&#39;s slow evolution? Maybe its time to try Spider.</div>
        <div class=""grid-date"">5/25/2015</div>
        <div class=""grid-admin"">        <a href=""/Page/Delete/296"">Delete</a> | <a href=""/Page/Edit/296"">Edit</a> | <a href=""/Page/Create?parentId=1"">Create New</a>
</div>
    </article>";
            var context = BrowsingContext.New();
            var parseErrors = new EventReceiver<HtmlErrorEvent>(handler => context.GetService<IHtmlParser>().Error += handler);
            await context.OpenAsync(m => m.Content(source));

            Assert.AreEqual(1, parseErrors.Received.Count);
            Assert.AreEqual((Int32)HtmlParseError.DoctypeMissing, parseErrors.Received[0].Code);
            Assert.AreEqual(1, parseErrors.Received[0].Position.Column);
            Assert.AreEqual(1, parseErrors.Received[0].Position.Line);
        }

        [Test]
        public void MultipleCarriageReturnsShouldBeNewLines()
        {
            var html = "<!doctype html><div>a\r\r\r\n\rb</div>";
            var parser = new HtmlParser();
            var document = parser.ParseDocument(html);
            var div = document.QuerySelector<IHtmlElement>("div");
            var content = div.TextContent;
            Assert.AreEqual("a\n\n\n\nb", content);
        }

        [Test]
        public void NormalModeShouldError()
        {
            var html = @"<!DOCTYPE html>
<title>Test</title>
<body>
    <div myAttribute=""blabla>123</div>
</body>";
            var errors = new List<HtmlErrorEvent>();
            var options = new HtmlParserOptions { IsStrictMode = false };
            var parser = new HtmlParser(options);
            parser.Error += (s, ev) => errors.Add((HtmlErrorEvent)ev);
            parser.ParseDocument(html);
            Assert.AreEqual(1, errors.Count);
        }

        [Test]
        public void StrictModeShouldYieldException()
        {
            var html = @"<!DOCTYPE html>
<title>Test</title>
<body>
    <div myAttribute=""blabla>123</div>
</body>";
            var options = new HtmlParserOptions { IsStrictMode = true };
            var parser = new HtmlParser(options);
            Assert.Catch<HtmlParseException>(() => parser.ParseDocument(html));
        }
    }
}
