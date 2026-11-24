namespace AngleSharp.Core.Tests.Css
{
    using System;
    using System.Linq;
    using AngleSharp.Dom;
    using NUnit.Framework;

    /// <summary>
    /// Pins matching that is answered from remembered state - the index hint of a node and the
    /// sibling positions kept for :nth-child() - which has to give the same answers as counting
    /// afresh, however the tree changed in between.
    /// </summary>
    [TestFixture]
    public class SelectorMatchingTests
    {
        private static String Ids(IHtmlCollection<IElement> elements) => String.Join(",", elements.Select(m => m.Id));

        [Test]
        public void NthChildSeesElementInsertedBeforeBetweenQueries()
        {
            var document = "<ul><li id=a></li><li id=b></li><li id=c></li></ul>".ToHtmlDocument();
            var list = document.QuerySelector("ul")!;

            Assert.AreEqual("a,c", Ids(document.QuerySelectorAll("li:nth-child(odd)")));

            list.Prepend(document.CreateElement("li"));

            Assert.AreEqual("b", Ids(document.QuerySelectorAll("li[id]:nth-child(odd)")));
        }

        [Test]
        public void NthChildSeesTextNodeReplacedByElementBetweenQueries()
        {
            // Swaps a text node for an element: every index stays where it was, while every
            // element position after it moves on by one
            var document = "<ul>x<li id=a></li><li id=b></li></ul>".ToHtmlDocument();
            var list = document.QuerySelector("ul")!;

            Assert.AreEqual("a", Ids(document.QuerySelectorAll("li:nth-child(1)")));

            list.ReplaceChild(document.CreateElement("li"), list.FirstChild!);

            Assert.AreEqual("b", Ids(document.QuerySelectorAll("li[id]:nth-child(3)")));
        }

        [Test]
        public void NthLastChildSeesElementAppendedBetweenQueries()
        {
            var document = "<ul><li id=a></li><li id=b></li></ul>".ToHtmlDocument();
            var list = document.QuerySelector("ul")!;

            Assert.AreEqual("b", Ids(document.QuerySelectorAll("li:nth-last-child(1)")));

            list.AppendChild(document.CreateElement("li"));

            Assert.AreEqual("a", Ids(document.QuerySelectorAll("li[id]:nth-last-child(3)")));
        }

        [Test]
        public void NthChildAnswersElementsAskedOutOfOrder()
        {
            var document = "<ul><li id=a></li>t<li id=b></li><!--c--><li id=c></li><li id=d></li></ul>".ToHtmlDocument();
            var items = document.QuerySelectorAll("li").ToArray();

            for (var round = 0; round < 2; round++)
            {
                Assert.IsTrue(items[3].Matches(":nth-child(4)"));
                Assert.IsTrue(items[1].Matches(":nth-child(2)"));
                Assert.IsTrue(items[2].Matches(":nth-last-child(2)"));
                Assert.IsTrue(items[0].Matches(":nth-last-child(4)"));
                Assert.IsFalse(items[2].Matches(":nth-child(2)"));
            }
        }

        [Test]
        public void NthChildInNestedListsKeepsEachParentApart()
        {
            // The walk enters the children of each item before reaching the next item
            var document = "<ol><li id=a><i id=x></i><i id=y></i></li><li id=b><i id=z></i></li><li id=c></li></ol>".ToHtmlDocument();

            Assert.AreEqual("y,b", Ids(document.QuerySelectorAll("ol :nth-child(even)")));
            Assert.AreEqual("a,x,z,c", Ids(document.QuerySelectorAll("ol :nth-child(odd)")));
        }

        [Test]
        public void SiblingsAreFoundAfterTheTreeChanged()
        {
            var document = "<div><p id=a></p><p id=b></p><p id=c></p></div>".ToHtmlDocument();
            var div = document.QuerySelector("div")!;
            var b = document.GetElementById("b")!;

            Assert.AreEqual("a", b.PreviousElementSibling!.Id);
            Assert.AreEqual("c", b.NextElementSibling!.Id);

            div.RemoveChild(document.GetElementById("a")!);
            div.InsertBefore(document.CreateTextNode("t"), b);
            var n = document.CreateElement("p");
            n.Id = "n";
            div.InsertBefore(n, div.FirstChild);

            Assert.AreEqual("n", b.PreviousElementSibling!.Id);
            Assert.AreEqual("c", b.NextElementSibling!.Id);
            Assert.AreEqual("b", document.QuerySelector("#n ~ p:last-of-type")!.PreviousElementSibling!.Id);
            Assert.AreEqual("c", Ids(document.QuerySelectorAll("#b + p")));
            Assert.AreEqual("b,c", Ids(document.QuerySelectorAll("#n ~ p")));
        }

        [Test]
        public void OnlyChildIgnoresTextButNotElements()
        {
            var document = "<div id=a>x<p id=b></p>y</div><div><p id=c></p><p id=d></p></div>".ToHtmlDocument();

            Assert.AreEqual("b", Ids(document.QuerySelectorAll("p:only-child")));
        }

        [TestCase("a b", "[class~=b]", true)]
        [TestCase("a\tb\nc", "[class~=c]", true)]
        [TestCase("  b  ", "[class~=b]", true)]
        [TestCase("ab", "[class~=b]", false)]
        [TestCase("a bc", "[class~=b]", false)]
        [TestCase("a b", "[class~='a b']", false)]
        [TestCase("A B", "[class~=b i]", true)]
        [TestCase("A B", "[class~=b]", false)]
        [TestCase("", "[class~=b]", false)]
        public void AttributeInListMatchesWholeTokens(String value, String selector, Boolean expected)
        {
            var document = "<p></p>".ToHtmlDocument();
            var p = document.QuerySelector("p")!;
            p.SetAttribute("class", value);

            Assert.AreEqual(expected, p.Matches(selector));
        }
    }
}
