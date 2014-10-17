using AngleSharp;
using AngleSharp.DOM;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.Library
{
    [TestClass]
    public class DOMActions
    {
        [TestMethod]
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
            Assert.IsTrue(clonedParent.HasChilds);
            //all the children (and grandchildren) of the cloned element have no parent?
            var cloneElement = (IElement)clonedParent;
            Assert.IsNotNull(cloneElement.FirstChild.Parent);
        }

        [TestMethod]
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
            Assert.IsFalse(clonedParent.HasChilds);
        }

        [TestMethod]
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

            Assert.IsTrue(divOne.IsEqualNode(divTwo));
            Assert.IsTrue(divOne.IsEqualNode(divThree));
            Assert.IsTrue(divTwo.IsEqualNode(divThree));
        }

        [TestMethod]
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
            Assert.IsTrue(original.IsEqualNode(clone));
            Assert.IsFalse(original.IsEqualNode(doc.Body));
        }

        [TestMethod]
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
    }
}
