namespace AngleSharp.Core.Tests.Css
{
    using System.Globalization;
    using AngleSharp.Core.Tests.Mocks;
    using AngleSharp.Dom;
    using NUnit.Framework;

    [TestFixture]
    public class SlickspeedTests
    {
        IDocument document;

        [TestFixtureSetUp]
        public void Setup()
        {
            var config = Configuration.Default.SetCulture("en-US").With(new EnableScripting());
            document = Assets.w3c_selectors.ToHtmlDocument(config);
        }

        [Test]
        public void SlickspeedFindBodyElement()
        {
            var result = document.QuerySelectorAll("body");
            Assert.AreEqual(1, result.Length);
        }

        [Test]
        public void SlickspeedFindDivElement()
        {
            var result = document.QuerySelectorAll("div");
            Assert.AreEqual(51, result.Length);
        }

        [Test]
        public void SlickspeedFindBodyDivElement()
        {
            var result = document.QuerySelectorAll("body div");
            Assert.AreEqual(51, result.Length);
        }

        [Test]
        public void SlickspeedFindDivPElement()
        {
            var result = document.QuerySelectorAll("div p");
            Assert.AreEqual(140, result.Length);
        }

        [Test]
        public void SlickspeedFindDivPChildElement()
        {
            var result = document.QuerySelectorAll("div > p");
            Assert.AreEqual(134, result.Length);
        }

        [Test]
        public void SlickspeedFindDivPSiblingElement()
        {
            var result = document.QuerySelectorAll("div + p");
            Assert.AreEqual(22, result.Length);
        }

        [Test]
        public void SlickspeedFindDivPFollowingElement()
        {
            var result = document.QuerySelectorAll("div ~ p");
            Assert.AreEqual(183, result.Length);
        }

        [Test]
        public void SlickspeedFindDivClassExaClassMpleElement()
        {
            var result = document.QuerySelectorAll("div[class^=exa][class$=mple]");
            Assert.AreEqual(43, result.Length);
        }

        [Test]
        public void SlickspeedFindDivPAElement()
        {
            var result = document.QuerySelectorAll("div p a");
            Assert.AreEqual(12, result.Length);
        }

        [Test]
        public void SlickspeedFindDivOrPOrAElement()
        {
            var result = document.QuerySelectorAll("div, p, a");
            Assert.AreEqual(671, result.Length);
        }

        [Test]
        public void SlickspeedFindNoteElement()
        {
            var result = document.QuerySelectorAll(".note");
            Assert.AreEqual(14, result.Length);
        }

        [Test]
        public void SlickspeedFindDivExampleElement()
        {
            var result = document.QuerySelectorAll("div.example");
            Assert.AreEqual(43, result.Length);
        }

        [Test]
        public void SlickspeedFindUlTocline2Element()
        {
            var result = document.QuerySelectorAll("ul .tocline2");
            Assert.AreEqual(12, result.Length);
        }

        [Test]
        public void SlickspeedFindDivExampleDivNoteElement()
        {
            var result = document.QuerySelectorAll("div.example, div.note");
            Assert.AreEqual(44, result.Length);
        }

        [Test]
        public void SlickspeedFindTitleElement()
        {
            var result = document.QuerySelectorAll("#title");
            Assert.AreEqual(1, result.Length);
        }

        [Test]
        public void SlickspeedFindH1TitleElement()
        {
            var result = document.QuerySelectorAll("h1#title");
            Assert.AreEqual(1, result.Length);
        }

        [Test]
        public void SlickspeedFindDivTitleElement()
        {
            var result = document.QuerySelectorAll("div #title");
            Assert.AreEqual(1, result.Length);
        }

        [Test]
        public void SlickspeedFindUlTocLiTocline2Element()
        {
            var result = document.QuerySelectorAll("ul.toc li.tocline2");
            Assert.AreEqual(12, result.Length);
        }

        [Test]
        public void SlickspeedFindUlTocLiTocline2ChildElement()
        {
            var result = document.QuerySelectorAll("ul.toc > li.tocline2");
            Assert.AreEqual(12, result.Length);
        }

        [Test]
        public void SlickspeedFindH1TitleDivPElement()
        {
            var result = document.QuerySelectorAll("h1#title + div > p");
            Assert.AreEqual(0, result.Length);
        }

        [Test]
        public void SlickspeedFindH1IdContainsSelectorsElement()
        {
            var result = document.QuerySelectorAll("h1[id]:contains(Selectors)");
            Assert.AreEqual(1, result.Length);
        }

        [Test]
        public void SlickspeedFindAHrefLangClassElement()
        {
            var result = document.QuerySelectorAll("a[href][lang][class]");
            Assert.AreEqual(1, result.Length);
        }

        [Test]
        public void SlickspeedFindDivClassElement()
        {
            var result = document.QuerySelectorAll("div[class]");
            Assert.AreEqual(51, result.Length);
        }

        [Test]
        public void SlickspeedFindDivClassExampleElement()
        {
            var result = document.QuerySelectorAll("div[class=example]");
            Assert.AreEqual(43, result.Length);
        }

        [Test]
        public void SlickspeedFindDivClassExaElement()
        {
            var result = document.QuerySelectorAll("div[class^=exa]");
            Assert.AreEqual(43, result.Length);
        }

        [Test]
        public void SlickspeedFindDivClassMpleElement()
        {
            var result = document.QuerySelectorAll("div[class$=mple]");
            Assert.AreEqual(43, result.Length);
        }

        [Test]
        public void SlickspeedFindDivClassEElement()
        {
            var result = document.QuerySelectorAll("div[class*=e]");
            Assert.AreEqual(50, result.Length);
        }

        [Test]
        public void SlickspeedFindDivClassDialogElement()
        {
            var result = document.QuerySelectorAll("div[class|=dialog]");
            Assert.AreEqual(0, result.Length);
        }

        [Test]
        public void SlickspeedFindDivClassMade_UpElement()
        {
            var result = document.QuerySelectorAll("div[class!=made_up]");
            Assert.AreEqual(51, result.Length);
        }

        [Test]
        public void SlickspeedFindDivClassContainsExampleElement()
        {
            var result = document.QuerySelectorAll("div[class~=example]");
            Assert.AreEqual(43, result.Length);
        }

        [Test]
        public void SlickspeedFindDivNotExampleElement()
        {
            var result = document.QuerySelectorAll("div:not(.example)");
            Assert.AreEqual(8, result.Length);
        }

        [Test]
        public void SlickspeedFindPContainsSelectorsElement()
        {
            var result = document.QuerySelectorAll("p:contains(selectors)");
            Assert.AreEqual(54, result.Length);
        }

        [Test]
        public void SlickspeedFindPNthChildEvenElement()
        {
            var result = document.QuerySelectorAll("p:nth-child(even)");
            Assert.AreEqual(158, result.Length);
        }

        [Test]
        public void SlickspeedFindPNthChild2NElement()
        {
            var result = document.QuerySelectorAll("p:nth-child(2n)");
            Assert.AreEqual(158, result.Length);
        }

        [Test]
        public void SlickspeedFindPNthChildOddElement()
        {
            var result = document.QuerySelectorAll("p:nth-child(odd)");
            Assert.AreEqual(166, result.Length);
        }

        [Test]
        public void SlickspeedFindPNthChild2N1Element()
        {
            var result = document.QuerySelectorAll("p:nth-child(2n+1)");
            Assert.AreEqual(166, result.Length);
        }

        [Test]
        public void SlickspeedFindPNthChildNElement()
        {
            var result = document.QuerySelectorAll("p:nth-child(n)");
            Assert.AreEqual(324, result.Length);
        }

        [Test]
        public void SlickspeedFindPOnlyChildElement()
        {
            var result = document.QuerySelectorAll("p:only-child");
            Assert.AreEqual(3, result.Length);
        }

        [Test]
        public void SlickspeedFindPLastChildElement()
        {
            var result = document.QuerySelectorAll("p:last-child");
            Assert.AreEqual(19, result.Length);
        }

        [Test]
        public void SlickspeedFindPFirstChildElement()
        {
            var result = document.QuerySelectorAll("p:first-child");
            Assert.AreEqual(54, result.Length);
        }
    }
}
