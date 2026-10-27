namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Dom;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The child list keeps a free region at its front as well as at its end, so these drive
    /// insertions and removals at every position against a plain list as the reference.
    /// </summary>
    [TestFixture]
    public class ChildListTests
    {
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void RandomInsertsAndRemovalsMatchAPlainList(Int32 seed)
        {
            var document = "<body>".ToHtmlDocument();
            var parent = document.CreateElement("div");
            var expected = new List<INode>();
            var random = new Random(seed);

            for (var step = 0; step < 5000; step++)
            {
                var operation = random.Next(10);

                if (operation < 6 || expected.Count == 0)
                {
                    var node = document.CreateTextNode(step.ToString());
                    var index = PickPosition(random, expected.Count + 1);

                    if (index == expected.Count)
                    {
                        parent.AppendChild(node);
                    }
                    else
                    {
                        parent.InsertBefore(node, expected[index]);
                    }

                    expected.Insert(index, node);
                }
                else
                {
                    var index = PickPosition(random, expected.Count);
                    parent.RemoveChild(expected[index]);
                    expected.RemoveAt(index);
                }

                AssertSame(expected, parent);
            }

            while (expected.Count > 0)
            {
                parent.RemoveChild(parent.FirstChild);
                expected.RemoveAt(0);
                AssertSame(expected, parent);
            }
        }

        [Test]
        public void MovingAllChildrenOffTheFrontKeepsTheirOrder()
        {
            var document = ("<body>" + String.Concat(Repeat("<p></p>x", 3000))).ToHtmlDocument();
            var expected = new List<INode>(document.Body.ChildNodes);
            var target = document.CreateElement("div");

            while (document.Body.FirstChild is INode child)
            {
                target.AppendChild(child);
            }

            Assert.AreEqual(0, document.Body.ChildNodes.Length);
            AssertSame(expected, target);
        }

        [Test]
        public void PrependingManyChildrenKeepsTheirOrder()
        {
            var document = "<body>".ToHtmlDocument();
            var parent = document.CreateElement("div");
            var expected = new List<INode>();

            for (var i = 0; i < 3000; i++)
            {
                var node = document.CreateElement("span");
                parent.InsertBefore(node, parent.FirstChild);
                expected.Insert(0, node);
            }

            AssertSame(expected, parent);
        }

        [Test]
        public void ChangingChildrenWhileEnumeratingThrows()
        {
            var document = "<body><p></p><p></p>".ToHtmlDocument();

            Assert.Throws<InvalidOperationException>(() =>
            {
                foreach (var child in document.Body.ChildNodes)
                {
                    document.Body.RemoveChild(child);
                }
            });
        }

        [Test]
        public void IndexOutsideTheChildrenThrows()
        {
            var document = "<body><p></p>".ToHtmlDocument();
            var children = document.Body.ChildNodes;

            Assert.Throws<ArgumentOutOfRangeException>(() => _ = children[1]);
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = children[-1]);
        }

        private static Int32 PickPosition(Random random, Int32 count)
        {
            // Biased towards both ends, which is where the free regions are used up and reclaimed.
            switch (random.Next(4))
            {
                case 0:
                    return 0;
                case 1:
                    return count - 1;
                default:
                    return random.Next(count);
            }
        }

        private static IEnumerable<String> Repeat(String value, Int32 count)
        {
            for (var i = 0; i < count; i++)
            {
                yield return value;
            }
        }

        private static void AssertSame(List<INode> expected, INode parent)
        {
            var children = parent.ChildNodes;
            Assert.AreEqual(expected.Count, children.Length);

            var i = 0;

            foreach (var child in children)
            {
                Assert.AreSame(expected[i], child);
                Assert.AreSame(expected[i], children[i]);
                Assert.AreSame(parent, child.Parent);
                i++;
            }

            if (expected.Count > 0)
            {
                Assert.AreSame(expected[0], parent.FirstChild);
                Assert.AreSame(expected[expected.Count - 1], parent.LastChild);
                Assert.IsNull(parent.FirstChild.PreviousSibling);
                Assert.IsNull(parent.LastChild.NextSibling);
            }
        }
    }
}
