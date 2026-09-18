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
    /// pin both halves of the promise: it moves for everything that mutates the tree, an attribute
    /// or character data, and it stands still for a read - and for a parse, which builds a tree
    /// rather than mutating one.
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
            // Worth pinning on its own: InnerHtml replaces the children through ReplaceAll, which
            // removes and inserts each one with the observers suppressed and then reports the whole
            // replacement once. Reading that from the inner steps alone concludes the opposite.
            var document = Doc("<div id=target></div>");
            var before = document.MutationVersion;

            document.GetElementById("target").InnerHtml = "<span>text</span>";

            Assert.AreNotEqual(before, document.MutationVersion);
        }

        [Test]
        public void VersionMovesForATextContentWrite()
        {
            var document = Doc("<div id=target><span></span></div>");
            var before = document.MutationVersion;

            document.GetElementById("target").TextContent = "text";

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
        public void VersionStandsStillForAConstructionPathInsert()
        {
            // AddNode is one of the raw child list operations the tree builder drives directly, so
            // it changes the tree and nothing else. The version belongs to InsertBefore above it,
            // which is where a caller decided a mutation happened.
            var document = Doc("<div id=target></div>");
            var target = (Node)document.GetElementById("target");
            var before = document.MutationVersion;

            target.AddNode((Node)document.CreateElement("span"));

            Assert.AreEqual(before, document.MutationVersion);
        }

        [Test]
        public void VersionStandsStillForAConstructionPathAttribute()
        {
            // Same for AddAttribute: no duplicate check, no attribute change steps, no version.
            var document = Doc("<div id=target></div>");
            var target = (Element)document.GetElementById("target");
            var before = document.MutationVersion;

            target.AddAttribute(new Attr("data-x", "1"));

            Assert.AreEqual(before, document.MutationVersion);
        }

        [Test]
        public void VersionStandsStillForNodesWrittenByAScriptDuringParsing()
        {
            // document.write feeds the tokenizer rather than the DOM: the nodes arrive through the
            // same tree construction path as the rest of the document, so this is still a parse.
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
            Assert.AreEqual(duringScript, document.MutationVersion);
        }

        [Test]
        public void VersionMovesForANodeAppendedToTheDocumentItself()
        {
            // The document node reports a null owner, so the mutation algorithms have to reach for
            // the document they are changing rather than for the owner of the parent.
            var document = Doc("<div></div>");
            var before = document.MutationVersion;

            document.AppendChild(document.CreateComment("trailing"));

            Assert.AreNotEqual(before, document.MutationVersion);
        }

        [Test]
        public void VersionMovesForANodeRemovedFromTheDocumentItself()
        {
            var document = Doc("<div></div>");
            var before = document.MutationVersion;

            document.RemoveChild(document.DocumentElement);

            Assert.AreNotEqual(before, document.MutationVersion);
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
        public void VersionStandsStillForASmallParse()
        {
            var parser = new HtmlParser();
            var document = (Document)parser.ParseDocument("<!doctype html><title>Title</title><div><span>Text</span><input></div>");

            // A parse builds a tree, it does not mutate one, so it comes out the other side having
            // triggered no mutation logic at all - not a record, not an observer, not the version.
            Assert.AreEqual(0, document.MutationVersion);
        }

        [Test]
        public void VersionStandsStillForAParseWithAttributesAndText()
        {
            // Attributes and text reach the tree through their own construction paths - the batch
            // attribute write and the character data append - so they need their own case.
            var parser = new HtmlParser();
            var source = "<!doctype html><html><head><title>T</title></head><body>" +
                "<div id=a class='x y' data-z=1>Hello <b>world</b>, and more text</div></body></html>";
            var document = (Document)parser.ParseDocument(source);

            Assert.AreEqual(3, document.QuerySelector("#a").Attributes.Length);
            Assert.AreEqual(0, document.MutationVersion);
        }

        [TestCase("<!doctype html><body><b><p>mis</b>nested</p>", TestName = "VersionStandsStillForAParseRunningTheAdoptionAgency")]
        [TestCase("<!doctype html><table>foster<tr><td>cell</td></tr></table>", TestName = "VersionStandsStillForAParseFosterParentingText")]
        [TestCase("<!doctype html><body>text<frameset><frame>", TestName = "VersionStandsStillForAParseDroppingABodyElement")]
        public void VersionStandsStillForAParseThatReparentsNodes(String source)
        {
            // These are the three tree construction algorithms that move a node that is already in
            // the tree. They are still construction, so they are as unobserved as the rest of it.
            var parser = new HtmlParser();
            var document = (Document)parser.ParseDocument(source);

            Assert.AreEqual(0, document.MutationVersion);
        }

        [Test]
        public void VersionStandsStillForAFragmentParse()
        {
            var document = Doc("<div id=target></div>");
            var context = document.GetElementById("target");
            var before = document.MutationVersion;

            var parser = new HtmlParser();
            var fragment = parser.ParseFragment("<span>text</span>", context);

            Assert.AreEqual(1, fragment.Length);
            Assert.AreEqual(before, document.MutationVersion);
        }
    }
}
