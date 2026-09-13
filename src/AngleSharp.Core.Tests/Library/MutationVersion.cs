namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Core.Tests.Mocks;
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using AngleSharp.Html.Parser;
    using NUnit.Framework;
    using System;

    /// <summary>
    /// Document.MutationVersion answers "may anything have changed since I last looked?". These
    /// pin both halves of the promise: it moves for everything that changes the tree, an attribute
    /// or character data, and it stands still for a read.
    /// </summary>
    [TestFixture]
    public class MutationVersionTests
    {
        private static Document Doc(String source) => (Document)source.ToHtmlDocument();

        [Test]
        public void VersionMovesForAnInsert()
        {
            var document = Doc("<div id=target></div>");
            var before = document.MutationVersion;

            document.GetElementById("target").AppendChild(document.CreateElement("span"));

            Assert.AreNotEqual(before, document.MutationVersion);
        }

        [Test]
        public void VersionMovesForARemove()
        {
            var document = Doc("<div id=target><span></span></div>");
            var target = document.GetElementById("target");
            var before = document.MutationVersion;

            target.RemoveChild(target.FirstChild);

            Assert.AreNotEqual(before, document.MutationVersion);
        }

        [Test]
        public void VersionMovesForAReplace()
        {
            var document = Doc("<div id=target><span></span></div>");
            var target = document.GetElementById("target");
            var before = document.MutationVersion;

            target.ReplaceChild(document.CreateElement("em"), target.FirstChild);

            Assert.AreNotEqual(before, document.MutationVersion);
        }

        [Test]
        public void VersionMovesForAnInnerHtmlWrite()
        {
            var document = Doc("<div id=target></div>");
            var before = document.MutationVersion;

            document.GetElementById("target").InnerHtml = "<span>text</span>";

            Assert.AreNotEqual(before, document.MutationVersion);
        }

        [Test]
        public void VersionMovesForATextChange()
        {
            var document = Doc("<div id=target>text</div>");
            var text = (IText)document.GetElementById("target").FirstChild;
            var before = document.MutationVersion;

            text.Data = "other";

            Assert.AreNotEqual(before, document.MutationVersion);
        }

        [Test]
        public void VersionMovesForATextAppend()
        {
            var document = Doc("<div id=target>text</div>");
            var text = (IText)document.GetElementById("target").FirstChild;
            var before = document.MutationVersion;

            text.Append("!");

            Assert.AreNotEqual(before, document.MutationVersion);
        }

        [Test]
        public void VersionMovesForSetAttribute()
        {
            var document = Doc("<div id=target></div>");
            var before = document.MutationVersion;

            document.GetElementById("target").SetAttribute("data-x", "1");

            Assert.AreNotEqual(before, document.MutationVersion);
        }

        [Test]
        public void VersionMovesForAnAttributeValueWrite()
        {
            var document = Doc("<div id=target data-x=1></div>");
            var attribute = document.GetElementById("target").Attributes["data-x"];
            var before = document.MutationVersion;

            attribute.Value = "2";

            Assert.AreNotEqual(before, document.MutationVersion);
        }

        [Test]
        public void VersionMovesForRemoveAttribute()
        {
            var document = Doc("<div id=target data-x=1></div>");
            var before = document.MutationVersion;

            document.GetElementById("target").RemoveAttribute("data-x");

            Assert.AreNotEqual(before, document.MutationVersion);
        }

        [Test]
        public void VersionMovesForABooleanAttributeBeingCleared()
        {
            var document = Doc("<input id=target disabled>");
            var target = (IHtmlInputElement)document.GetElementById("target");
            var before = document.MutationVersion;

            target.IsDisabled = false;

            Assert.AreNotEqual(before, document.MutationVersion);
        }

        [Test]
        public void VersionMovesForAClassNameWrite()
        {
            var document = Doc("<div id=target></div>");
            var before = document.MutationVersion;

            document.GetElementById("target").ClassName = "alpha";

            Assert.AreNotEqual(before, document.MutationVersion);
        }

        [Test]
        public void VersionMovesForAClassListWrite()
        {
            var document = Doc("<div id=target></div>");
            var target = document.GetElementById("target");
            var before = document.MutationVersion;

            target.ClassList.Add("alpha");

            Assert.AreNotEqual(before, document.MutationVersion);
        }

        [Test]
        public void VersionMovesForADatasetWrite()
        {
            var document = Doc("<div id=target></div>");
            var target = (IHtmlElement)document.GetElementById("target");
            var before = document.MutationVersion;

            target.Dataset["foo"] = "bar";

            Assert.AreNotEqual(before, document.MutationVersion);
        }

        [Test]
        public void VersionMovesForAConstructionPathInsert()
        {
            // The parser builds the tree through these low level mutators and queues no mutation
            // record for any of it, so a counter that only followed the record funnel would miss
            // every parser write.
            var document = Doc("<div id=target></div>");
            var target = (Node)document.GetElementById("target");
            var before = document.MutationVersion;

            target.AddNode((Node)document.CreateElement("span"));

            Assert.AreNotEqual(before, document.MutationVersion);
        }

        [Test]
        public void VersionMovesForAConstructionPathAttribute()
        {
            var document = Doc("<div id=target></div>");
            var target = (Element)document.GetElementById("target");
            var before = document.MutationVersion;

            target.AddAttribute(new Attr("data-x", "1"));

            Assert.AreNotEqual(before, document.MutationVersion);
        }

        [Test]
        public void VersionMovesForNodesWrittenByAScriptDuringParsing()
        {
            if (TestRuntime.UsePrefetchedTextSource)
            {
                Assert.Ignore("Prefetched text source is read only");
            }

            var duringScript = 0L;
            var scripting = new CallbackScriptEngine(options =>
            {
                duringScript = ((Document)options.Document).MutationVersion;
                options.Document.Write("<b>written</b>");
            });
            var config = Configuration.Default.WithScripts(scripting);
            var source = "<title>Some title</title><body><script type='c-sharp'>//...</script>";
            var document = (Document)source.ToHtmlDocument(config);

            Assert.AreEqual(1, document.QuerySelectorAll("b").Length);
            Assert.AreNotEqual(duringScript, document.MutationVersion);
        }

        [Test]
        public void VersionStandsStillForReads()
        {
            var document = Doc("<div id=target class=alpha data-x=1><span>text</span></div>");
            var target = document.GetElementById("target");
            var before = document.MutationVersion;

            Assert.IsNotNull(document.QuerySelectorAll(".alpha"));
            Assert.IsNotNull(target.GetAttribute("data-x"));
            Assert.IsNotNull(target.ClassList);
            Assert.IsTrue(target.ClassList.Contains("alpha"));
            Assert.IsNotNull(target.TextContent);
            Assert.IsNotNull(target.InnerHtml);
            Assert.IsNotNull(target.OuterHtml);
            Assert.IsNotNull(document.Body.ChildNodes);

            Assert.AreEqual(before, document.MutationVersion);
        }

        [Test]
        public void VersionStandsStillForAWriteThatChangesNothing()
        {
            var document = Doc("<div id=target class=alpha></div>");
            var target = document.GetElementById("target");
            var before = document.MutationVersion;

            // The token list knows it is already there and never reaches the attribute.
            target.ClassList.Add("alpha");

            Assert.AreEqual(before, document.MutationVersion);
        }

        [Test]
        public void VersionsOfTwoDocumentsAreIndependent()
        {
            var first = Doc("<div id=target></div>");
            var second = Doc("<div id=target></div>");
            var firstBefore = first.MutationVersion;
            var secondBefore = second.MutationVersion;

            second.GetElementById("target").SetAttribute("data-x", "1");

            Assert.AreEqual(firstBefore, first.MutationVersion);
            Assert.AreNotEqual(secondBefore, second.MutationVersion);
        }

        [Test]
        public void VersionOfTheOwningDocumentMovesWhenADetachedNodeIsAttached()
        {
            var document = Doc("<div id=target></div>");
            var detached = document.CreateElement("span");
            var before = document.MutationVersion;

            detached.SetAttribute("data-x", "1");
            detached.AppendChild(document.CreateTextNode("text"));

            Assert.AreNotEqual(before, document.MutationVersion);
        }

        [Test]
        public void VersionAdvancesWhileParsing()
        {
            var parser = new HtmlParser();
            var document = (Document)parser.ParseDocument("<!doctype html><div class=alpha><span>text</span></div>");

            // Construction is itself a long sequence of mutations, so a freshly parsed document is
            // nowhere near the starting value.
            Assert.Greater(document.MutationVersion, 0);
        }
    }
}
