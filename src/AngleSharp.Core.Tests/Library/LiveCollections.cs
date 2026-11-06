namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Core.Tests.Mocks;
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [TestFixture]
    public class LiveCollectionsTests
    {
        private static IDocument Html(String code) => new HtmlParser().ParseDocument(code);

        [Test]
        public void ByClassNameFollowsRemovals()
        {
            var doc = Html("<div id=r><i class=a></i><i class=a></i><u class=a></u></div>");
            var list = doc.GetElementById("r").GetElementsByClassName("a");
            var steps = 0;

            while (list.Length > 0 && steps++ < 10)
            {
                list[0].Remove();
            }

            Assert.AreEqual(3, steps);
            Assert.AreEqual(0, list.Length);
        }

        [Test]
        public void ByClassNameFollowsAdditionsAndClassChanges()
        {
            var doc = Html("<div id=r><i class=a></i><b id=b></b></div>");
            var list = doc.GetElementById("r").GetElementsByClassName("a");
            Assert.AreEqual(1, list.Length);

            doc.GetElementById("b").ClassName = "a";
            Assert.AreEqual(2, list.Length);

            doc.GetElementById("b").ClassList.Remove("a");
            Assert.AreEqual(1, list.Length);

            var added = doc.CreateElement("p");
            added.SetAttribute("class", "a");
            doc.GetElementById("r").AppendChild(added);
            Assert.AreEqual(2, list.Length);
            Assert.AreSame(added, list[1]);
        }

        [Test]
        public void ByTagNameFollowsTheDocument()
        {
            var doc = Html("<p></p>");
            var all = doc.GetElementsByTagName("p");
            var stars = doc.DocumentElement.GetElementsByTagName("*");
            var before = stars.Length;

            doc.Body.AppendChild(doc.CreateElement("p"));

            Assert.AreEqual(2, all.Length);
            Assert.AreEqual(before + 1, stars.Length);
        }

        [Test]
        public void ByNameFollowsTheDocument()
        {
            var doc = Html("<input name=a>");
            var list = doc.GetElementsByName("a");
            Assert.AreEqual(1, list.Length);

            list[0].SetAttribute("name", "b");

            Assert.AreEqual(0, list.Length);
        }

        [Test]
        public void ByTagNameOfAFragmentFollowsItsTree()
        {
            var doc = Html("");
            var fragment = doc.CreateDocumentFragment();
            var list = ((AngleSharp.Dom.DocumentFragment)fragment).GetElementsByTagName("b");
            Assert.AreEqual(0, list.Length);

            fragment.AppendChild(doc.CreateElement("b"));

            Assert.AreEqual(1, list.Length);
        }

        [Test]
        public void GoingOverACollectionMayChangeTheTree()
        {
            var doc = Html("<i class=a></i><i class=a></i><i class=a></i>");
            var list = doc.GetElementsByClassName("a");
            var count = 0;

            foreach (var element in list)
            {
                element.Remove();
                count++;
            }

            Assert.AreEqual(3, count);
            Assert.AreEqual(0, list.Length);
        }

        [Test]
        public void ItemsAreFoundByIdAndTheyAreDetachedFromTheCollectionWhenGone()
        {
            var doc = Html("<i id=x class=a></i>");
            var list = doc.GetElementsByClassName("a");

            Assert.IsNotNull(list["x"]);
            list["x"].Remove();
            Assert.IsNull(list["x"]);
        }

        [Test]
        public void SearchingAgainIsNotNeededWithoutAChange()
        {
            var doc = Html("<i class=a></i>");
            var searches = 0;
            var list = new LiveHtmlCollection((Node)doc, () =>
            {
                searches++;
                return doc.QuerySelectorAll(".a");
            });

            Assert.AreEqual(1, list.Length);
            Assert.AreEqual(1, list.Length);
            Assert.AreEqual(1, searches);

            doc.Body.SetAttribute("x", "1");

            Assert.AreEqual(1, list.Length);
            Assert.AreEqual(2, searches);
        }

        [Test]
        public void CollectionFromAScriptFollowsWhatIsParsedAfterIt()
        {
            IHtmlCollection<IElement> list = null;
            var lengths = new List<Int32>();
            var scripting = new CallbackScriptEngine(options =>
            {
                list ??= options.Document.GetElementsByTagName("b");
                lengths.Add(list.Length);
            });
            var source = "<b></b><script type='c-sharp'>//</script><b></b><script type='c-sharp'>//</script><b></b>";
            var document = source.ToHtmlDocument(Configuration.Default.WithScripts(scripting));

            CollectionAssert.AreEqual(new[] { 1, 2 }, lengths);
            Assert.AreEqual(3, list.Length);
            Assert.AreEqual(3, document.GetElementsByTagName("b").Length);
        }

        [Test]
        public void CollectionIsEmptyAfterTheDocumentWasCleared()
        {
            var doc = Html("<b></b>");
            var list = doc.GetElementsByTagName("b");
            Assert.AreEqual(1, list.Length);

            ((Document)doc).Clear();

            Assert.AreEqual(0, list.Length);
        }

        [Test]
        public async Task CollectionIsEmptyAfterTheDocumentWasOpenedAgain()
        {
            var doc = await BrowsingContext.New().OpenAsync(m => m.Content("<b></b>"));
            var list = doc.GetElementsByTagName("b");
            Assert.AreEqual(1, list.Length);

            doc.Open();
            doc.Close();

            Assert.IsFalse(doc.ReadyState == DocumentReadyState.Loading);
            Assert.AreEqual(0, list.Length);
        }

        [Test]
        public void RootNodeIsTheTopOfTheTree()
        {
            var doc = Html("<div id=d><b id=b></b></div>");
            var detached = doc.CreateElement("p");
            var child = doc.CreateElement("i");
            detached.AppendChild(child);

            Assert.AreSame(doc, doc.GetElementById("b").GetRootNode());
            Assert.AreSame(doc, doc.GetRootNode());
            Assert.AreSame(detached, child.GetRootNode());
            Assert.AreSame(detached, detached.GetRootNode());
        }

        [Test]
        public void RootNodeStopsAtAShadowRootUnlessComposed()
        {
            var doc = Html("<div id=d></div>");
            var host = doc.GetElementById("d");
            var shadow = host.AttachShadow(ShadowRootMode.Open);
            var inner = doc.CreateElement("b");
            shadow.AppendChild(inner);

            Assert.AreSame(shadow, inner.GetRootNode());
            Assert.AreSame(doc, inner.GetRootNode(new GetRootNodeOptions { IsComposed = true }));
        }
    }
}
