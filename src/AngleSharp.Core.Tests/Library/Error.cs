namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Core.Tests.Mocks;
    using AngleSharp.Dom.Events;
    using AngleSharp.Dom.Html;
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;
    using AngleSharp.Parser.Html;
    using NUnit.Framework;
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
            var parseErrors = new EventReceiver<HtmlErrorEvent>(handler => context.ParseError += handler);
            var document = await context.OpenAsync(m => m.Content(source));

            Assert.AreEqual(1, parseErrors.Received.Count);
            Assert.AreEqual((int)HtmlParseError.DoctypeMissing, parseErrors.Received[0].Code);
            Assert.AreEqual(1, parseErrors.Received[0].Position.Column);
            Assert.AreEqual(1, parseErrors.Received[0].Position.Line);
        }

        [Test]
        public void ParseInlineStyleWithToleratedInvalidValueShouldReturnThatValue()
        {
            var html = "<div style=\"background-image: url(javascript:alert(1))\"></div>";
            var options = new CssParserOptions
            {
                IsIncludingUnknownDeclarations = true,
                IsIncludingUnknownRules = true,
                IsToleratingInvalidConstraints = true,
                IsToleratingInvalidValues = true
            };
            var config = Configuration.Default.WithCss(e => e.Options = options);
            var parser = new HtmlParser(config);
            var dom = parser.Parse(html);
            var div = dom.QuerySelector<IHtmlElement>("div");
            Assert.AreEqual(1, div.Style.Length);
            Assert.AreEqual("background-image", div.Style[0]);
            Assert.AreEqual("url(\"javascript:alert(1)\")", div.Style.BackgroundImage);
        }

        [Test]
        public void ParseInlineStyleWithUnknownDeclarationShouldBeAbleToRemoveThatDeclaration()
        {
            var html = @"<DIV STYLE='background: url(""javascript:alert(foo)"")'>";
            var options = new CssParserOptions
            {
                IsIncludingUnknownDeclarations = true,
                IsIncludingUnknownRules = true,
                IsToleratingInvalidConstraints = true,
                IsToleratingInvalidValues = true
            };
            var config = Configuration.Default.WithCss(e => e.Options = options);
            var parser = new HtmlParser(config);
            var dom = parser.Parse(html);
            var div = dom.QuerySelector<IHtmlElement>("div");
            Assert.AreEqual(1, div.Style.Length);
            Assert.AreEqual("background", div.Style[0]);
            div.Style.RemoveProperty("background");
            Assert.AreEqual(0, div.Style.Length);
        }

        [Test]
        public void MultipleCarriageReturnsShouldBeNewLines()
        {
            var html = "<!doctype html><div>a\r\r\r\n\rb</div>";
            var parser = new HtmlParser();
            var document = parser.Parse(html);
            var div = document.QuerySelector<IHtmlElement>("div");
            var content = div.TextContent;
            Assert.AreEqual("a\n\n\n\nb", content);
        }
    }
}
