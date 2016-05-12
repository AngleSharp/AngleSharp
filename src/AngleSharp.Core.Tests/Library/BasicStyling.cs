namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Core.Tests.Mocks;
    using AngleSharp.Dom;
    using AngleSharp.Dom.Html;
    using AngleSharp.Extensions;
    using NUnit.Framework;
    using System;
    using System.Threading.Tasks;

    [TestFixture]
    public class BasicStylingTests
    {
        static Task<IDocument> CreateDocumentWithOptions(String source)
        {
            var mockRequester = new MockRequester();
            mockRequester.BuildResponse = request =>
            {
                if (request.Address.Path.EndsWith("a.css"))
                {
                    return "div#A   { color: blue;	}";
                }
                else if (request.Address.Path.EndsWith("b.css"))
                {
                    return "div#B   { color: red;   }";
                }

                return null;
            };
            var config = Configuration.Default.WithCss().WithMockRequester(mockRequester);
            var context = BrowsingContext.New(config);
            return context.OpenAsync(m => m.Content(source));
        }

        [Test]
        public async Task ExternalStyleSheetIsPreferred()
        {
            var source = @"<!doctype html>
<html>
    <head>
    	<link rel=""stylesheet"" media=""screen"" type=""text/css"" title=""A"" href=""a.css"" />
    	<link rel=""stylesheet alternate"" media=""screen"" type=""text/css"" title=""B"" href=""b.css"" />
    </head>
</html>";
            var document = await CreateDocumentWithOptions(source);
            var link = document.QuerySelector<IHtmlLinkElement>("link");

            Assert.AreEqual(2, document.StyleSheets.Length);
            Assert.AreEqual(2, document.StyleSheetSets.Length);
            Assert.IsTrue(link.IsPreferred());
            Assert.IsFalse(link.IsAlternate());
            Assert.IsFalse(link.IsPersistent());
        }

        [Test]
        public async Task ExternalStyleSheetIsPersistent()
        {
            var source = @"<!doctype html>
<html>
    <head>
    	<link rel=""stylesheet"" media=""screen"" type=""text/css"" href=""a.css"" />
    	<link rel=""stylesheet alternate"" media=""screen"" type=""text/css"" title=""B"" href=""b.css"" />
    </head>
</html>";
            var document = await CreateDocumentWithOptions(source);
            var link = document.QuerySelector<IHtmlLinkElement>("link");

            Assert.AreEqual(2, document.StyleSheets.Length);
            Assert.AreEqual(1, document.StyleSheetSets.Length);
            Assert.IsFalse(link.IsPreferred());
            Assert.IsFalse(link.IsAlternate());
            Assert.IsTrue(link.IsPersistent());
        }

        [Test]
        public async Task ExternalStyleSheetIsAlternate()
        {
            var source = @"<!doctype html>
<html>
    <head>
    	<link rel=""stylesheet alternate"" media=""screen"" type=""text/css"" title=""A"" href=""a.css"" />
    	<link rel=""stylesheet"" media=""screen"" type=""text/css"" title=""B"" href=""b.css"" />
    </head>
</html>";
            var document = await CreateDocumentWithOptions(source);
            var link = document.QuerySelector<IHtmlLinkElement>("link");

            Assert.AreEqual(2, document.StyleSheets.Length);
            Assert.AreEqual(2, document.StyleSheetSets.Length);
            Assert.IsFalse(link.IsPreferred());
            Assert.IsTrue(link.IsAlternate());
            Assert.IsFalse(link.IsPersistent());
        }

        [Test]
        public async Task GetComputedStyleFromHelperShouldBeOkay()
        {
            var source = "<!doctype html><head><style>p > span { color: blue; } span.bold { font-weight: bold; }</style></head><body><div><p><span class='bold'>Bold text";
            var document = await CreateDocumentWithOptions(source);
            var element = document.QuerySelector("span.bold");
            Assert.AreEqual("span", element.LocalName);
            Assert.AreEqual("bold", element.ClassName);
            var style = element.ComputeCurrentStyle();
            Assert.IsNotNull(style);
            Assert.AreEqual(2, style.Length);
        }
    }
}
