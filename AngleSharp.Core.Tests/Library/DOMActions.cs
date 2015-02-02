using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Linq;
using NUnit.Framework;

namespace AngleSharp.Core.Tests.Library
{
    [TestFixture]
    public class DOMActions
    {
        [Test]
        public void ParentReplacementByCloneWithChildrenExpectedToHaveAParent()
        {
            const string html = @"
<html>
<body>
    <div class='parent'>
        <div class='child'>
        </div>
    </div>
</body>
</html>
";
            var doc = DocumentBuilder.Html(html);
            var originalParent = doc.QuerySelector(".parent");

            //clone the parent
            var clonedParent = originalParent.Clone();
            Assert.IsNull(clonedParent.Parent);

            //remove the original parent
            var grandparent = originalParent.Parent;
            originalParent.Remove();
            Assert.IsNull(originalParent.Parent);
            Assert.IsNotNull(grandparent);

            //replace the original parent with the cloned parent
            grandparent.AppendChild(clonedParent);
            //the clone itself has the correct parent
            Assert.AreEqual(grandparent, clonedParent.Parent);
            //Children on this one
            Assert.IsTrue(clonedParent.HasChildNodes);
            //all the children (and grandchildren) of the cloned element have no parent?
            var cloneElement = (IElement)clonedParent;
            Assert.IsNotNull(cloneElement.FirstChild.Parent);
        }

        [Test]
        public void ParentReplacementByCloneWithNoChildren()
        {
            const string html = @"
<html>
<body>
    <div class='parent'>
        <div class='child'>
        </div>
    </div>
</body>
</html>
";
            var doc = DocumentBuilder.Html(html);
            var originalParent = doc.QuerySelector(".parent");

            //clone the parent
            var clonedParent = originalParent.Clone(false);
            Assert.IsNull(clonedParent.Parent);

            //remove the original parent
            var grandparent = originalParent.Parent;
            originalParent.Remove();
            Assert.IsNull(originalParent.Parent);
            Assert.IsNotNull(grandparent);

            //replace the original parent with the cloned parent
            grandparent.AppendChild(clonedParent);
            //the clone itself has the correct parent
            Assert.AreEqual(grandparent, clonedParent.Parent);
            //No children on this one
            Assert.IsFalse(clonedParent.HasChildNodes);
        }

        [Test]
        public void IsEqualNodesWithExactlyTheSameNodes()
        {
            const string html = @"
<html>
<body>
    <div class='parent'>
        <div class='child'>
        </div>
    </div>
</body>
</html>
";
            var doc = DocumentBuilder.Html(html);
            var divOne = doc.QuerySelector(".parent");
            var divTwo = doc.Body.FirstElementChild;
            var divThree = doc.QuerySelectorAll("div")[0];

            Assert.AreEqual(divOne, divThree);
            Assert.AreEqual(divTwo, divThree);

            Assert.IsTrue(divOne.Equals(divTwo));
            Assert.IsTrue(divOne.Equals(divThree));
            Assert.IsTrue(divTwo.Equals(divThree));
        }

        [Test]
        public void IsEqualNodesWithClonedNode()
        {
            const string html = @"
<html>
<body>
    <div class='parent'>
        <div class='child'>
        </div>
    </div>
</body>
</html>
";
            var doc = DocumentBuilder.Html(html);
            var original = doc.QuerySelector(".parent");
            var clone = original.Clone();

            Assert.AreNotEqual(original, clone);
            Assert.IsTrue(original.Equals(clone));
            Assert.IsFalse(original.Equals(doc.Body));
        }

        [Test]
        public void ContainsWithChildNodes()
        {
            const string html = @"
<html>
<body>
    <div class='parent'>
        <div class='child'>
            <div class='grandchild'></div>
        </div>
    </div>
</body>
</html>
";
            var doc = DocumentBuilder.Html(html);
            var parent = doc.QuerySelector(".parent");
            var child = doc.QuerySelector(".child");
            var grandchild = doc.QuerySelector(".grandchild");

            Assert.IsFalse(parent.Contains(doc.Body));
            Assert.IsTrue(parent.Contains(parent));
            Assert.IsTrue(parent.Contains(child));
            Assert.IsTrue(parent.Contains(grandchild));
        }

        [Test]
        public void ReturnTextFromBody()
        {
            var test = "Some text";
            var html = string.Format(@"
<html>
<body>{0}</body></html>", test);
            var doc = DocumentBuilder.Html(html);
            Assert.AreEqual(test, doc.Body.TextContent);
            Assert.AreEqual(test, doc.Body.Text());
            Assert.AreEqual(test, doc.Body.ChildNodes[0].TextContent);

            var text = doc.Body.ChildNodes[0] as TextNode;
            Assert.IsNotNull(text);
            Assert.AreEqual(test, text.Data);
            Assert.AreEqual(test, text.Text);
        }

        [Test]
        public void ReturnConcatTextFromBody()
        {
            var test1 = "Some text";
            var test2 = "Other text";
            var test3 = "Another test";
            var test = string.Concat(test1, test2, test3);
            var html = @"
<html>
<body></body></html>";
            var doc = DocumentBuilder.Html(html);
            var text1 = doc.CreateTextNode(test1);
            var text2 = doc.CreateTextNode(test2);
            var text3 = doc.CreateTextNode(test3);
            doc.Body.Append(text1);
            doc.Body.Append(text2);
            doc.Body.Append(text3);
            Assert.AreEqual(test, doc.Body.TextContent);
            Assert.AreEqual(test, doc.Body.Text());
            Assert.AreEqual(test1, doc.Body.ChildNodes[0].TextContent);

            Assert.AreEqual(test1, text1.Data);
            Assert.AreEqual(test, text1.Text);
            Assert.AreEqual(test2, text2.Data);
            Assert.AreEqual(test, text2.Text);
            Assert.AreEqual(test3, text3.Data);
            Assert.AreEqual(test, text3.Text);
        }
    }
}
