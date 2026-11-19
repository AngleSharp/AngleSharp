namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using AngleSharp.Io;
    using NUnit.Framework;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Behavior the Acid3 test (acid3.acidtests.org) relies on.
    /// </summary>
    [TestFixture]
    public class Acid3Tests
    {
        [Test]
        public void SetEndKeepsTheStartOfANewRange()
        {
            var document = "<body></body>".ToHtmlDocument();
            var range = document.CreateRange();
            range.Insert(document.CreateComment("c"));
            range.EndWith(document, 1);

            Assert.AreEqual(document, range.Head);
            Assert.AreEqual(0, range.Start);
            Assert.AreEqual(1, range.End);
            Assert.IsFalse(range.IsCollapsed);
        }

        [Test]
        public void SetStartBeforeANodeAfterTheEndCollapsesTheRange()
        {
            var document = "<body><a></a><b></b><c></c></body>".ToHtmlDocument();
            var body = document.Body;
            var range = document.CreateRange();
            range.StartWith(body.Children[1], 0);
            range.EndWith(body.Children[2], 0);
            range.EndWith(body.Children[0], 0);

            Assert.IsTrue(range.IsCollapsed);

            range.StartBefore(body.Children[2]);

            Assert.IsTrue(range.IsCollapsed);
            Assert.AreEqual(body, range.Head);
            Assert.AreEqual(2, range.Start);
            Assert.AreEqual(body, range.Tail);
            Assert.AreEqual(2, range.End);
        }

        [Test]
        public void ClonedContentsOfACollapsedRangeInTheDocumentAreEmpty()
        {
            var document = "<body></body>".ToHtmlDocument();
            var range = document.CreateRange();

            Assert.AreEqual(0, range.CopyContent().ChildNodes.Length);
            Assert.AreEqual(0, range.ExtractContent().ChildNodes.Length);
        }

        [Test]
        public void RangeStringifiesOnlyTheTextInIt()
        {
            var document = "<body><p>12345</p></body>".ToHtmlDocument();
            var p = document.QuerySelector("p");
            var t1 = (IText)p.FirstChild;
            var t2 = document.CreateTextNode("ABCDE");
            p.AppendChild(t2);
            var range = document.CreateRange();
            range.StartWith(t1, 2);
            range.EndWith(t1, 3);
            range.Insert(t2);

            Assert.AreEqual(3, p.ChildNodes.Length);
            Assert.AreEqual("12", t1.Data);
            Assert.AreEqual("ABCDE3", range.ToString());
        }

        [Test]
        public void CreateElementWithAnEmptyPrefixThrows()
        {
            var document = "<body></body>".ToHtmlDocument();

            foreach (var name in new[] { ":div", "a::b", "a:" })
            {
                var error = Assert.Throws<DomException>(() => document.CreateElement("http://example.com/", name));
                Assert.AreEqual((Int32)DomError.Namespace, error.Code, name);
            }

            Assert.IsNotNull(document.CreateElement("http://example.com/", "a:b"));
        }

        [Test]
        public void NodeIteratorFollowsTheNodeItsFilterRemoves()
        {
            var document = "<body><t1></t1><t2></t2><t3></t3></body>".ToHtmlDocument();
            var body = document.Body;
            var t1 = body.Children[0];
            var t2 = body.Children[1];
            var t3 = body.Children[2];
            var iterator = document.CreateNodeIterator(body, FilterSettings.Element, node =>
            {
                if (node == t2 && t2.Parent is not null)
                {
                    body.RemoveChild(t2);
                }

                return FilterResult.Accept;
            });

            Assert.AreEqual(body, iterator.Next());
            Assert.AreEqual(t1, iterator.Next());
            Assert.AreEqual(t2, iterator.Next());
            Assert.AreEqual(t3, iterator.Next());
            Assert.AreEqual(t3, iterator.Previous());
            Assert.AreEqual(t1, iterator.Previous());
        }

        [Test]
        public void ClickDispatchesItsEventSynchronously()
        {
            var document = "<body><div><input type=reset></div></body>".ToHtmlDocument();
            var input = (IHtmlInputElement)document.QuerySelector("input");
            var clicks = 0;
            input.AddEventListener("click", (_, _) => clicks++);

            input.DoClick();

            Assert.AreEqual(1, clicks);
        }

        [Test]
        public void ClickTogglesACheckbox()
        {
            var document = "<body><input type=checkbox></body>".ToHtmlDocument();
            var input = (IHtmlInputElement)document.QuerySelector("input");
            var changes = 0;
            input.AddEventListener("change", (_, _) => changes++);

            input.DoClick();
            Assert.IsTrue(input.IsChecked);
            Assert.AreEqual(1, changes);

            input.AddEventListener("click", (_, ev) => ev.Cancel());
            input.DoClick();
            Assert.IsTrue(input.IsChecked);
            Assert.AreEqual(1, changes);
        }

        [Test]
        public void StoppingAnEventWhileCapturingSkipsTheBubblingListenersOfTheSameElement()
        {
            var document = "<body><div><input></div></body>".ToHtmlDocument();
            var div = document.QuerySelector("div");
            var input = (IHtmlInputElement)document.QuerySelector("input");
            var capturing = 0;
            var bubbling = 0;
            div.AddEventListener("click", (_, ev) => { capturing++; ev.Stop(); }, true);
            div.AddEventListener("click", (_, _) => capturing++, true);
            div.AddEventListener("click", (_, _) => bubbling++);

            input.DoClick();

            Assert.AreEqual(2, capturing);
            Assert.AreEqual(0, bubbling);
        }

        [Test]
        public void FormElementsOfAFormNotInTheDocument()
        {
            var document = "<body></body>".ToHtmlDocument();
            var form = (IHtmlFormElement)document.CreateElement("form");
            var input = document.CreateElement("input");
            form.AppendChild(input);

            Assert.AreEqual(1, form.Elements.Length);
            Assert.AreEqual(input, form.Elements[0]);

            document.Body.AppendChild(form);

            Assert.AreEqual(1, form.Elements.Length);
        }

        [Test]
        public async Task ImageInAFrameIsShownInADocumentOfItsOwn()
        {
            var config = Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            var document = await context.OpenAsync(res => res
                .Content("<p>FAIL</p>")
                .Address("http://localhost/image.png")
                .Header(HeaderNames.ContentType, "image/png"));

            Assert.AreEqual(0, document.QuerySelectorAll("p").Length);
            Assert.AreEqual("http://localhost/image.png", document.QuerySelector("img")?.GetAttribute("src"));
            Assert.AreEqual(DocumentReadyState.Complete, document.ReadyState);
        }

        [Test]
        public async Task PlainTextDocumentCompletes()
        {
            var context = BrowsingContext.New(Configuration.Default);
            var document = await context.OpenAsync(res => res
                .Content("hello")
                .Address("http://localhost/a.txt")
                .Header(HeaderNames.ContentType, "text/plain"));

            Assert.AreEqual("hello", document.QuerySelector("pre")?.TextContent);
            Assert.AreEqual(DocumentReadyState.Complete, document.ReadyState);
        }
    }
}
