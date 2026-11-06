namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    [TestFixture]
    public class UrlSearchParamsIterationTests
    {
        [Test]
        public void EntriesKeepTheOrderAndDecode()
        {
            var parameters = new UrlSearchParams("b=1&a=x%20y&b=2");

            Assert.AreEqual(new[] { "b", "1" }, parameters.Entries()[0]);
            Assert.AreEqual(new[] { "a", "x y" }, parameters.Entries()[1]);
            Assert.AreEqual(new[] { "b", "2" }, parameters.Entries()[2]);
        }

        [Test]
        public void KeysAndValuesAreTheOnesOfTheEntries()
        {
            var parameters = new UrlSearchParams("b=1&a=2");

            Assert.AreEqual(new[] { "b", "a" }, parameters.Keys());
            Assert.AreEqual(new[] { "1", "2" }, parameters.Values());
        }

        [Test]
        public void LeadingQuestionMarkIsNotPartOfTheName()
        {
            Assert.AreEqual(new[] { "a", "1" }, new UrlSearchParams("?a=1").Entries()[0]);
            Assert.IsEmpty(new UrlSearchParams("?").Entries());
        }

        [Test]
        public void EmptyHasNoEntries()
        {
            Assert.IsEmpty(new UrlSearchParams().Entries());
        }

        [Test]
        public void ForEachGetsValueNameAndTheParams()
        {
            var parameters = new UrlSearchParams("a=1&b=2");
            var seen = new List<String>();

            parameters.ForEach((value, name, parent) =>
            {
                Assert.AreSame(parameters, parent);
                seen.Add(name + "=" + value);
            });

            Assert.AreEqual(new[] { "a=1", "b=2" }, seen);
        }

        [Test]
        public void ForEachMayChangeTheParams()
        {
            var parameters = new UrlSearchParams("a=1&b=2");

            parameters.ForEach((value, name, parent) => parent.Delete(name));

            Assert.IsEmpty(parameters.Entries());
        }

        [Test]
        public void ReplaceChildrenTakesTheGivenNodes()
        {
            var document = new HtmlParser().ParseDocument("<div id=d><a></a>text<b></b></div><i id=i></i><u id=u></u>");
            var div = document.QuerySelector("#d");
            var i = document.QuerySelector("#i");
            var u = document.QuerySelector("#u");

            div.ReplaceChildren(i, u);

            Assert.AreEqual("<i id=\"i\"></i><u id=\"u\"></u>", div.InnerHtml);
            Assert.AreEqual(2, div.ChildElementCount);
            Assert.IsNull(document.Body.QuerySelector("body > #i"));

            div.ReplaceChildren();

            Assert.AreEqual(0, div.ChildNodes.Length);
        }

        [Test]
        public void ReplaceChildrenWorksOnAFragment()
        {
            var document = new HtmlParser().ParseDocument("");
            var fragment = document.CreateDocumentFragment();
            fragment.AppendChild(document.CreateElement("a"));

            fragment.ReplaceChildren(document.CreateElement("b"));

            Assert.AreEqual("b", fragment.FirstChild.NodeName.ToLowerInvariant());
            Assert.AreEqual(1, fragment.ChildNodes.Length);
        }
    }
}
