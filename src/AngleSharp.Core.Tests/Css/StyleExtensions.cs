namespace AngleSharp.Core.Tests.Css
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AngleSharp.Css;
    using AngleSharp.Css.Dom;
    using AngleSharp.Html.Parser;
    using AngleSharp.Io;
    using Dom;
    using Mocks;
    using NUnit.Framework;
    using Text;

    public class StylesheetExtensions
    {
        [TestCase(10)]
        [TestCase(100)]
        [TestCase(1000)]
        [TestCase(10000)]
        public void TestStyleSheetsDoesNotThrowStackOverflowException(Int32 count)
        {
            var thread = new Thread( () =>
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

        public MockStyleSheet(StyleOptions options)
        {
            _options = options;
        }

        public void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
        }

        public String Type { get; } = "text/css";

        public String Href { get; } = null;

        public IElement OwnerNode => _options.Element;

        public String Title { get; } = "";

        public IMediaList Media { get; } = null;

        public Boolean IsDisabled { get => _options.IsDisabled; set { } }

        public IBrowsingContext Context => _options.Document.Context;

        public TextSource Source { get; } = null;

        public void SetOwner(IElement element)
        {
        }

        public String LocateNamespace(String prefix) => null;
    }
}