using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using AngleSharp.Linq;
using NUnit.Framework;

namespace AngleSharp.Core.Tests.Library
{
    [TestFixture]
    public class DOMActions
    {
        [Test]
        public void ChangeImageSourceWithRelativeUrlResultsInUpdatedAbsoluteUrl()
        {
            var document = DocumentBuilder.Html("", url: "http://localhost");
            var img = document.CreateElement<IHtmlImageElement>();
            img.Source = "test.png";
            Assert.AreEqual("http://localhost/test.png", img.Source);
            var url = new Url(img.Source);
            Assert.AreEqual("test.png", url.Path);
        }

        [Test]
        public void ChangeImageSourceWithAbsoluteUrlResultsInUpdatedAbsoluteUrl()
        {
            var document = DocumentBuilder.Html("", url: "http://localhost");
            var img = document.CreateElement<IHtmlImageElement>();
            img.Source = "http://www.test.com/test.png";
            Assert.AreEqual("http://www.test.com/test.png", img.Source);
            var url = new Url(img.Source);
            Assert.AreEqual("test.png", url.Path);
        }

        [Test]
        public void ChangeVideoSourceResultsInUpdatedAbsoluteUrl()
        {
            var document = DocumentBuilder.Html("", url: "http://localhost");
            var video = document.CreateElement<IHtmlVideoElement>();
            video.Source = "test.mp4";
            Assert.AreEqual("http://localhost/test.mp4", video.Source);
            var url = new Url(video.Source);
            Assert.AreEqual("test.mp4", url.Path);
        }

        [Test]
        public void ChangeVideoPosterResultsInUpdatedAbsoluteUrl()
        {
            var document = DocumentBuilder.Html("", url: "http://localhost");
            var video = document.CreateElement<IHtmlVideoElement>();
            video.Poster = "test.jpg";
            Assert.AreEqual("http://localhost/test.jpg", video.Poster);
            var url = new Url(video.Poster);
            Assert.AreEqual("test.jpg", url.Path);
        }

        [Test]
        public void ChangeAudioSourceResultsInUpdatedAbsoluteUrl()
        {
            var document = DocumentBuilder.Html("", url: "http://localhost");
            var audio = document.CreateElement<IHtmlAudioElement>();
            audio.Source = "test.mp3";
            Assert.AreEqual("http://localhost/test.mp3", audio.Source);
            var url = new Url(audio.Source);
            Assert.AreEqual("test.mp3", url.Path);
        }

        [Test]
        public void ChangeObjectSourceResultsInUpdatedAbsoluteUrl()
        {
            var document = DocumentBuilder.Html("", url: "http://localhost");
            var obj = document.CreateElement<IHtmlObjectElement>();
            obj.Source = "test.swv";
            Assert.AreEqual("http://localhost/test.swv", obj.Source);
            var url = new Url(obj.Source);
            Assert.AreEqual("test.swv", url.Path);
        }

        [Test]
        public void PassEmptyArrayToPrependNodes()
        {
            var document = DocumentBuilder.Html("");
            Assert.AreEqual(0, document.Body.ChildElementCount);
            document.Body.Prepend();
            Assert.AreEqual(0, document.Body.ChildElementCount);
        }

        [Test]
        public void PassSingleElementWithPrependNodes()
        {
            var document = DocumentBuilder.Html("");
            var newDiv = document.CreateElement<IHtmlDivElement>();
            Assert.AreEqual(0, document.Body.ChildElementCount);
            document.Body.Prepend(newDiv);
            Assert.AreEqual(1, document.Body.ChildElementCount);
            Assert.AreEqual("div", document.Body.Children[0].NodeName);
        }

        [Test]
        public void PassTwoElementsWithPrependNodes()
        {
            var document = DocumentBuilder.Html("");
            var newDiv = document.CreateElement<IHtmlDivElement>();
            var newAnchor = document.CreateElement<IHtmlAnchorElement>();
            Assert.AreEqual(0, document.Body.ChildElementCount);
            document.Body.Prepend(newDiv, newAnchor);
            Assert.AreEqual(2, document.Body.ChildElementCount);
            Assert.AreEqual("div", document.Body.Children[0].NodeName);
            Assert.AreEqual("a", document.Body.Children[1].NodeName);
        }

        [Test]
        public void PassTwoElementsWithPrependNodesToNonEmptyElement()
        {
            var document = DocumentBuilder.Html("<span></span>");
            var newDiv = document.CreateElement<IHtmlDivElement>();
            var newAnchor = document.CreateElement<IHtmlAnchorElement>();
            Assert.AreEqual(1, document.Body.ChildElementCount);
            document.Body.Prepend(newDiv, newAnchor);
            Assert.AreEqual(3, document.Body.ChildElementCount);
            Assert.AreEqual("div", document.Body.Children[0].NodeName);
            Assert.AreEqual("a", document.Body.Children[1].NodeName);
            Assert.AreEqual("span", document.Body.Children[2].NodeName);
        }

        [Test]
        public void PassEmptyArrayToAppendNodes()
        {
            var document = DocumentBuilder.Html("");
            Assert.AreEqual(0, document.Body.ChildElementCount);
            document.Body.Append();
            Assert.AreEqual(0, document.Body.ChildElementCount);
        }

        [Test]
        public void PassSingleElementWithAppendNodes()
        {
            var document = DocumentBuilder.Html("");
            var newDiv = document.CreateElement<IHtmlDivElement>();
            Assert.AreEqual(0, document.Body.ChildElementCount);
            document.Body.Append(newDiv);
            Assert.AreEqual(1, document.Body.ChildElementCount);
            Assert.AreEqual("div", document.Body.Children[0].NodeName);
        }

        [Test]
        public void PassTwoElementsWithAppendNodes()
        {
            var document = DocumentBuilder.Html("");
            var newDiv = document.CreateElement<IHtmlDivElement>();
            var newAnchor = document.CreateElement<IHtmlAnchorElement>();
            Assert.AreEqual(0, document.Body.ChildElementCount);
            document.Body.Append(newDiv, newAnchor);
            Assert.AreEqual(2, document.Body.ChildElementCount);
            Assert.AreEqual("div", document.Body.Children[0].NodeName);
            Assert.AreEqual("a", document.Body.Children[1].NodeName);
        }

        [Test]
        public void PassTwoElementsWithAppendNodesToNonEmptyElement()
        {
            var document = DocumentBuilder.Html("<span></span>");
            var newDiv = document.CreateElement<IHtmlDivElement>();
            var newAnchor = document.CreateElement<IHtmlAnchorElement>();
            Assert.AreEqual(1, document.Body.ChildElementCount);
            document.Body.Append(newDiv, newAnchor);
            Assert.AreEqual(3, document.Body.ChildElementCount);
            Assert.AreEqual("span", document.Body.Children[0].NodeName);
            Assert.AreEqual("div", document.Body.Children[1].NodeName);
            Assert.AreEqual("a", document.Body.Children[2].NodeName);
        }

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

        [Test]
        public void GetRowsFromTable()
        {
            var html = @"<table><tr></tr><tr></tr></table>";
            var doc = DocumentBuilder.Html(html);
            var table = doc.QuerySelector("table") as IHtmlTableElement;

            Assert.IsNotNull(table);
            Assert.AreEqual(2, table.Rows.Length);
            Assert.AreEqual(0, (table.Rows[0] as IHtmlTableRowElement).Cells.Length);
            Assert.AreEqual(0, (table.Rows[1] as IHtmlTableRowElement).Cells.Length);
        }

        [Test]
        public void GetRowsFromTableWithNesting()
        {
            var html = @"<table id=first><tr></tr><tr><td><table id=second><tr></tr></table></td></tr></table>";
            var doc = DocumentBuilder.Html(html);
            var first = doc.QuerySelector("#first") as IHtmlTableElement;
            var second = doc.QuerySelector("#second") as IHtmlTableElement;

            Assert.IsNotNull(first);
            Assert.IsNotNull(second);

            Assert.AreEqual(2, first.Rows.Length);
            Assert.AreEqual(0, (first.Rows[0] as IHtmlTableRowElement).Cells.Length);
            Assert.AreEqual(1, (first.Rows[1] as IHtmlTableRowElement).Cells.Length);
            Assert.AreEqual(1, second.Rows.Length);
            Assert.AreEqual(0, (second.Rows[0] as IHtmlTableRowElement).Cells.Length);
        }
    }
}
