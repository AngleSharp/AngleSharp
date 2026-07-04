namespace AngleSharp.Core.Tests.Css
{
    using AngleSharp.Css;
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser;
    using AngleSharp.Io;
    using AngleSharp.Text;
    using NUnit.Framework;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class StylesheetExtensions
    {
        [TestCase(10)]
        [TestCase(100)]
        [TestCase(1000)]
        [TestCase(10000)]
        public void TestStyleSheetsDoesNotThrowStackOverflowException(Int32 count)
        {
            var thread = new Thread(() =>
            {
                var beginTags = String.Join("", Enumerable.Repeat("<div>", count));
                var endTags = String.Join("", Enumerable.Repeat("</div>", count));
                var html = $"<html><head><style></style></head><body>{beginTags}<span><style></style></span>{endTags}</body></html>";

                var stylingService = new MockStylingService();
                var context = BrowsingContext.New(Configuration.Default.WithOnly<IStylingService>(stylingService));
                var document = context.GetService<IHtmlParser>()!.ParseDocument(html);

                var sheets = document.StyleSheets;
                Assert.AreEqual(sheets.Length, 2);
                Assert.AreEqual("HEAD", sheets[0]!.OwnerNode.ParentElement?.TagName);
                Assert.AreEqual("SPAN", sheets[1]!.OwnerNode.ParentElement?.TagName);

            }, (64 + 1) * 1024);

            thread.Start();
            thread.Join();
        }

        [Test]
        public async Task AppendingStylesheetLinkShouldLoadResource()
        {
            var request = default(Request);

            var stylingService = new MockStylingService();
            var cfg = Configuration.Default
                                        .WithOnly<IStylingService>(stylingService)
                                        .WithMockRequester(req => request = req);
            var html = @"<!doctype html><head><link rel=stylesheet href=/mock-stylesheet-1.css /></head><body></body>";
            var document = await BrowsingContext.New(cfg).OpenAsync(m => m.Content(html));

            // test with link element in the initial HTML
            Assert.IsNotNull(request);
            Assert.AreEqual("/mock-stylesheet-1.css", request.Address.PathName);

            request = default(Request);

            // test with dynamically added link element
            var link = document.CreateElement("link");
            link.SetAttribute(AttributeNames.Rel, "stylesheet");
            link.SetAttribute(AttributeNames.Href, "/mock-stylesheet-2.css");

            document.Body.AppendChild(link);

            Assert.IsNotNull(request);
            Assert.AreEqual("/mock-stylesheet-2.css", request.Address.PathName);
        }

        [Test]
        public async Task HtmlInlineStyleSheetShouldLoad()
        {
            var stylingService = new MockStylingService();
            var cfg = Configuration.Default.WithOnly<IStylingService>(stylingService);
            var html = @"<!doctype html><head><style>p { color: blue; }</style></head><body></body>";
            var document = await BrowsingContext.New(cfg).OpenAsync(m => m.Content(html));

            Assert.AreEqual(1, document.Head.ChildNodes.Length);

            var htmlHeadStyle = document.Head.ChildNodes[0];
            Assert.AreEqual("style", htmlHeadStyle.GetTagName());
            Assert.AreEqual(NodeType.Element, htmlHeadStyle.NodeType);

            var style = htmlHeadStyle as ILinkStyle;
            Assert.IsNotNull(style);
            Assert.AreEqual(style.Sheet.OwnerNode.TextContent, "p { color: blue; }");
        }

        [Test]
        public async Task SvgInlineStyleSheetShouldLoad()
        {
            var stylingService = new MockStylingService();
            var cfg = Configuration.Default.WithOnly<IStylingService>(stylingService);
            var svg = @"<svg><style>circle { fill: gold; }</style></svg>";
            var document = await BrowsingContext.New(cfg).OpenAsync(m => m.Content(svg));

            Assert.AreEqual(1, document.Body.ChildNodes.Length);

            var svgBodySvg = document.Body.ChildNodes[0];
            Assert.AreEqual(1, svgBodySvg.ChildNodes.Length);
            Assert.AreEqual("svg", svgBodySvg.GetTagName());
            Assert.AreEqual(NodeType.Element, svgBodySvg.NodeType);

            var svgBodySvgStyle = svgBodySvg.ChildNodes[0];
            Assert.AreEqual("style", svgBodySvgStyle.GetTagName());
            Assert.AreEqual(NodeType.Element, svgBodySvgStyle.NodeType);

            var style = svgBodySvgStyle as ILinkStyle;
            Assert.IsNotNull(style);
            Assert.AreEqual(style.Sheet.OwnerNode.TextContent, "circle { fill: gold; }");
        }

        [Test]
        public async Task SvgStyleMediaAttributeShouldUpdateStyleSheetMedia()
        {
            var stylingService = new MockStylingService();
            var cfg = Configuration.Default.WithOnly<IStylingService>(stylingService);
            var svg = @"<svg><style>circle { fill: gold; }</style></svg>";
            var document = await BrowsingContext.New(cfg).OpenAsync(m => m.Content(svg));

            var style = document.QuerySelector("svg > style");
            var sheet = ((ILinkStyle)style).Sheet;
            Assert.IsNotNull(sheet);

            style.SetAttribute(AttributeNames.Media, "print");

            Assert.AreEqual("print", sheet.Media.MediaText);
        }
    }

    internal class MockStylingService : IStylingService
    {
        public Boolean SupportsType(String mimeType) => "text/css" == mimeType;

        public Task<IStyleSheet> ParseStylesheetAsync(IResponse response, StyleOptions options, CancellationToken cancel)
        {
            return Task.FromResult<IStyleSheet>(new MockStyleSheet(options));
        }
    }


    internal class MockStyleSheet : IStyleSheet
    {
        private readonly StyleOptions _options;
        private readonly IMediaList _mediaList;

        public MockStyleSheet(StyleOptions options)
        {
            _options = options;
            _mediaList = new MockMediaList();
        }

        public void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
        }

        public String Type { get; } = "text/css";

        public String Href { get; } = null;

        public IElement OwnerNode => _options.Element;

        public String Title { get; } = "";

        public IMediaList Media { get => _mediaList; }

        public Boolean IsDisabled { get => _options.IsDisabled; set { } }

        public IBrowsingContext Context => _options.Document.Context;

        public TextSource Source { get; } = null;

        public void SetOwner(IElement element)
        {
        }

        public String LocateNamespace(String prefix) => null;
    }

    internal class MockMediaList : IMediaList
    {
        public String this[Int32 index] => "";

        public String MediaText { get; set; } = "";

        public Int32 Length => 0;

        public void Add(String medium)
        {
        }

        public IEnumerator<ICssMedium> GetEnumerator()
        {
            return default;
        }

        public void Remove(String medium)
        {
        }

        public void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return default;
        }
    }
}