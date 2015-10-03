namespace AngleSharp.Core.Tests
{
    using AngleSharp.Dom;
    using NUnit.Framework;

    [TestFixture]
    public class CompareDocumentPositionTests
    {
        IDocument doc;

        IElement parent1, parent2, child1, child2;

        [SetUp]
        public void SetUp()
        {
            doc = "<!DOCTYPE html><html><head><title>Title</title></head><body><div id='parent-1'><span id='child-1'>1</span></div><div id='parent-2'><span id='child-2'>2</span></div></body></html>".ToHtmlDocument();

            parent1 = doc.QuerySelector("#parent-1");
            parent2 = doc.QuerySelector("#parent-2");
            child1 = doc.QuerySelector("#child-1");
            child2 = doc.QuerySelector("#child-2");

        }

        [Test]
        public void SameParent()
        {
            Assert.AreEqual(DocumentPositions.Following, parent1.CompareDocumentPosition(parent2), "Parent 2 follows parent 1.");
            Assert.AreEqual(DocumentPositions.Preceding, parent2.CompareDocumentPosition(parent1), "Parent 1 precedes parent 2.");
        }

        [Test]
        public void NestedChildren()
        {
            Assert.AreEqual(DocumentPositions.Following, child1.CompareDocumentPosition(child2), "Child 2 follows child 1.");
            Assert.AreEqual(DocumentPositions.Preceding, child2.CompareDocumentPosition(child1), "Child 1 precedes child 2.");
        }

        [Test]
        public void Containing()
        {
            Assert.AreEqual(DocumentPositions.ContainedBy | DocumentPositions.Following, parent1.CompareDocumentPosition(child1), "Child 1 is contained by and follows parent 1.");
            Assert.AreEqual(DocumentPositions.Contains | DocumentPositions.Preceding, child1.CompareDocumentPosition(parent1), "Parent 1 contains and precedes child 1.");
        }

        [Test]
        public void DifferentLevels()
        {
            Assert.AreEqual(DocumentPositions.Following, parent1.CompareDocumentPosition(child2), "Child 2 follows parent 1.");
            Assert.AreEqual(DocumentPositions.Preceding, child2.CompareDocumentPosition(parent1), "Parent 1 precedes child 2.");

            Assert.AreEqual(DocumentPositions.Following, child1.CompareDocumentPosition(parent2), "Parent 2 follows child 1.");
            Assert.AreEqual(DocumentPositions.Preceding, parent2.CompareDocumentPosition(child1), "Child 1 precedes parent 2.");
        }
    }
}