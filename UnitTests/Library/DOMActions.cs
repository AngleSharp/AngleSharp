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

            //all the children (and grandchildren) of the cloned element have no parent?
            var cloneElement = (IElement)clonedParent;
            Assert.IsNotNull(cloneElement.FirstChild.Parent); //FAILS
        }
    }
}
