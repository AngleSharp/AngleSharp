namespace AngleSharp.Core.Tests.Html
{
    using AngleSharp.Dom;
    using NUnit.Framework;

    [TestFixture]
    public class CompareDocumentPositionTests
    {
        [Test]
        public void CompareDocumentPositionsWithSameParent()
        {
            var doc = "<!DOCTYPE html><div id='parent-1'><span id='child-1'>1</span></div><div id='parent-2'><span id='child-2'>2</span></div>".ToHtmlDocument();

            var parent1 = doc.QuerySelector("#parent-1");
            var parent2 = doc.QuerySelector("#parent-2");

            Assert.AreEqual(DocumentPositions.Following, parent1.CompareDocumentPosition(parent2));
            Assert.AreEqual(DocumentPositions.Preceding, parent2.CompareDocumentPosition(parent1));
        }

        [Test]
        public void CompareDocumentPositionsWithSameNodes()
        {
            var doc = "<!DOCTYPE html><div id='parent-1'><span id='child-1'>1</span></div><div id='parent-2'><span id='child-2'>2</span></div>".ToHtmlDocument();

            var parent1 = doc.QuerySelector("#parent-1");
            var child1 = doc.QuerySelector("#child-1");

            Assert.AreEqual(DocumentPositions.Same, parent1.CompareDocumentPosition(parent1));
            Assert.AreEqual(DocumentPositions.Same, child1.CompareDocumentPosition(child1));
        }

        [Test]
        public void CompareDocumentPositionsWithDifferentTrees()
        {
            var doc1 = "<!DOCTYPE html><div id='parent-1'><span id='child-1'>1</span></div><div id='parent-2'><span id='child-2'>2</span></div>".ToHtmlDocument();
            var doc2 = "<!DOCTYPE html><div id='parent-1'><span id='child-1'>1</span></div><div id='parent-2'><span id='child-2'>2</span></div>".ToHtmlDocument();

            var parent1 = doc1.QuerySelector("#parent-1");
            var parent2 = doc2.QuerySelector("#parent-1");

            var initial = parent1.CompareDocumentPosition(parent2);
            var reverse = parent2.CompareDocumentPosition(parent1);

            if ((initial & DocumentPositions.Preceding) == DocumentPositions.Preceding)
            {
                Assert.AreEqual(DocumentPositions.ImplementationSpecific | DocumentPositions.Disconnected | DocumentPositions.Preceding, initial);
                Assert.AreEqual(DocumentPositions.ImplementationSpecific | DocumentPositions.Disconnected | DocumentPositions.Following, reverse);
            }
            else
            {
                Assert.AreEqual(DocumentPositions.ImplementationSpecific | DocumentPositions.Disconnected | DocumentPositions.Following, initial);
                Assert.AreEqual(DocumentPositions.ImplementationSpecific | DocumentPositions.Disconnected | DocumentPositions.Preceding, reverse);
            }
        }

        [Test]
        public void CompareDocumentPositionsWithNestedChildren()
        {
            var doc = "<!DOCTYPE html><div id='parent-1'><span id='child-1'>1</span></div><div id='parent-2'><span id='child-2'>2</span></div>".ToHtmlDocument();

            var child1 = doc.QuerySelector("#child-1");
            var child2 = doc.QuerySelector("#child-2");

            Assert.AreEqual(DocumentPositions.Following, child1.CompareDocumentPosition(child2));
            Assert.AreEqual(DocumentPositions.Preceding, child2.CompareDocumentPosition(child1));
        }

        [Test]
        public void CompareDocumentPositionsWithContainingElements()
        {
            var doc = "<!DOCTYPE html><div id='parent-1'><span id='child-1'>1</span></div><div id='parent-2'><span id='child-2'>2</span></div>".ToHtmlDocument();

            var parent1 = doc.QuerySelector("#parent-1");
            var child1 = doc.QuerySelector("#child-1");

            Assert.AreEqual(DocumentPositions.ContainedBy | DocumentPositions.Following, parent1.CompareDocumentPosition(child1));

            var parent2 = doc.QuerySelector("#parent-2");
            var child2 = doc.QuerySelector("#child-2");

            Assert.AreEqual(DocumentPositions.Contains | DocumentPositions.Preceding, child2.CompareDocumentPosition(parent2));
        }

        [Test]
        public void CompareDocumentPositionsWithDifferentLevelsOfElements()
        {
            var doc = "<!DOCTYPE html><div id='parent-1'><span id='child-1'>1</span></div><div id='parent-2'><span id='child-2'>2</span></div>".ToHtmlDocument();

            var parent1 = doc.QuerySelector("#parent-1");
            var child2 = doc.QuerySelector("#child-2");

            Assert.AreEqual(DocumentPositions.Following, parent1.CompareDocumentPosition(child2));
            Assert.AreEqual(DocumentPositions.Preceding, child2.CompareDocumentPosition(parent1));

            var parent2 = doc.QuerySelector("#parent-2");
            var child1 = doc.QuerySelector("#child-1");

            Assert.AreEqual(DocumentPositions.Following, child1.CompareDocumentPosition(parent2));
            Assert.AreEqual(DocumentPositions.Preceding, parent2.CompareDocumentPosition(child1));
        }

        [Test]
        public void CompareDocumentPositionsHeadBeforeBody()
        {
            var doc = "<!DOCTYPE html><div></div>".ToHtmlDocument();

            var head = doc.Head;
            var body = doc.Body;

            Assert.AreEqual(DocumentPositions.Following, head.CompareDocumentPosition(body));
        }

        [Test]
        public void CompareDocumentPositionsFromQuirksMode()
        {
            var doc = @"<div class=testHTML><p id=test class=testClass><b id=testB></b>.</p>
<p id=test2 class=""nonsense testClass""></p>
<p><ppk></ppk></p>
<div id=test></div></div>".ToHtmlDocument();
            var x = doc.QuerySelector("#test");
            var y = doc.QuerySelector("#test2");
            var z = doc.QuerySelector("#testB");

            Assert.AreEqual(DocumentPositions.Following, x.CompareDocumentPosition(y));
            Assert.AreEqual(DocumentPositions.Following | DocumentPositions.ContainedBy, x.CompareDocumentPosition(z));
        }

        [Test]
        public void CompareDocumentPositionsShim()
        {
            var doc = "".ToHtmlDocument();
            var docfrag = doc.CreateDocumentFragment();
            var el = docfrag.AppendChild(doc.CreateElement("div"));
            var txt = docfrag.AppendChild(doc.CreateTextNode("foo"));

            Assert.AreEqual(DocumentPositions.Contains | DocumentPositions.Preceding, el.CompareDocumentPosition(docfrag));
            Assert.AreEqual(DocumentPositions.ContainedBy | DocumentPositions.Following, docfrag.CompareDocumentPosition(el));
            Assert.AreEqual(DocumentPositions.Same, el.CompareDocumentPosition(el));
            Assert.AreEqual(DocumentPositions.Following, el.CompareDocumentPosition(txt));
        }

        [Test]
        public void CompareDocumentPositionEmptyQueue()
        {
            var doc = "<!DOCTYPE html><html><head></head><body><div id=\"result\"></div><script></script></body></html>".ToHtmlDocument();
            var div1 = doc.CreateElement("div");
            var div2 = doc.CreateElement("div");
            var result = div1.CompareDocumentPosition(div2);
            Assert.AreEqual(DocumentPositions.Following, result);
        }
    }
}