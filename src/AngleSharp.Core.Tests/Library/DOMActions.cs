namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using AngleSharp.Io;
    using NUnit.Framework;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    [TestFixture]
    public class DOMActionsTests
    {
        private static IDocument CreateEmpty(String url)
        {
            return BrowsingContext.New().OpenNewAsync(url).Result;
        }

        private static IDocument Create(String source)
        {
            return source.ToHtmlDocument();
        }

        [Test]
        public void ChangeImageSourceWithRelativeUrlResultsInUpdatedAbsoluteUrl()
        {
            var document = CreateEmpty("http://localhost");
            var img = document.CreateElement<IHtmlImageElement>();
            img.Source = "test.png";
            Assert.AreEqual("http://localhost/test.png", img.Source);
            var url = new Url(img.Source);
            Assert.AreEqual("test.png", url.Path);
        }

        [Test]
        public void ChangeImageSourceWithAbsoluteUrlResultsInUpdatedAbsoluteUrl()
        {
            var document = CreateEmpty("http://localhost");
            var img = document.CreateElement<IHtmlImageElement>();
            img.Source = "http://www.test.com/test.png";
            Assert.AreEqual("http://www.test.com/test.png", img.Source);
            var url = new Url(img.Source);
            Assert.AreEqual("test.png", url.Path);
        }

        [Test]
        public void ChangeVideoSourceResultsInUpdatedAbsoluteUrl()
        {
            var document = CreateEmpty("http://localhost");
            var video = document.CreateElement<IHtmlVideoElement>();
            video.Source = "test.mp4";
            Assert.AreEqual("http://localhost/test.mp4", video.Source);
            var url = new Url(video.Source);
            Assert.AreEqual("test.mp4", url.Path);
        }

        [Test]
        public void ChangeVideoPosterResultsInUpdatedAbsoluteUrl()
        {
            var document = CreateEmpty("http://localhost");
            var video = document.CreateElement<IHtmlVideoElement>();
            video.Poster = "test.jpg";
            Assert.AreEqual("http://localhost/test.jpg", video.Poster);
            var url = new Url(video.Poster);
            Assert.AreEqual("test.jpg", url.Path);
        }

        [Test]
        public void ChangeAudioSourceResultsInUpdatedAbsoluteUrl()
        {
            var document = CreateEmpty("http://localhost");
            var audio = document.CreateElement<IHtmlAudioElement>();
            audio.Source = "test.mp3";
            Assert.AreEqual("http://localhost/test.mp3", audio.Source);
            var url = new Url(audio.Source);
            Assert.AreEqual("test.mp3", url.Path);
        }

        [Test]
        public void ChangeObjectSourceResultsInUpdatedAbsoluteUrl()
        {
            var document = CreateEmpty("http://localhost");
            var obj = document.CreateElement<IHtmlObjectElement>();
            obj.Source = "test.swv";
            Assert.AreEqual("http://localhost/test.swv", obj.Source);
            var url = new Url(obj.Source);
            Assert.AreEqual("test.swv", url.Path);
        }

        [Test]
        public void InputTypeImageShouldNotBePresentInTheFormElementsCollection()
        {
            var document = Create(@"<form id=""form"">
<input type=""image"">
</form>");
            Assert.AreEqual(0, document.Forms[0].Elements.Length);
        }

        [Test]
        public void FormElementsShouldIncludeElementsWhoseNameStartsWithANumber()
        {
            var document = Create(@"<form id=""form"">
<input type=""image"">
</form>");
            var form = document.Forms[0];
            var two = document.CreateElement<IHtmlInputElement>();
            two.Name = "2";
            form.AppendChild(two);
            var othree = document.CreateElement<IHtmlInputElement>();
            othree.Name = "03";
            form.AppendChild(othree);
            Assert.IsNull(form.Elements["-1"]);
            Assert.AreEqual(two, form.Elements[0]);
            Assert.AreEqual(othree, form.Elements[1]);
            Assert.AreEqual(2, form.Elements.Length);
            Assert.AreEqual(two, form.Elements["2"]);
            Assert.AreEqual(othree, form.Elements["03"]);
            CollectionAssert.AreEqual(new IHtmlElement[] { two, othree }, form.Elements.ToArray());
            form.RemoveChild(two);
            form.RemoveChild(othree);
        }

        [Test]
        public void ReplaceSingleNodeWithNothing()
        {
            var document = Create("<span></span><em></em>");
            document.QuerySelector("span").Replace();
            Assert.AreEqual("<em></em>", document.Body.InnerHtml);
        }

        [Test]
        public void PassEmptyArrayToPrependNodes()
        {
            var document = String.Empty.ToHtmlDocument();
            Assert.AreEqual(0, document.Body.ChildElementCount);
            document.Body.Prepend();
            Assert.AreEqual(0, document.Body.ChildElementCount);
        }

        [Test]
        public void PassSingleElementWithPrependNodes()
        {
            var document = String.Empty.ToHtmlDocument();
            var newDiv = document.CreateElement<IHtmlDivElement>();
            Assert.AreEqual(0, document.Body.ChildElementCount);
            document.Body.Prepend(newDiv);
            Assert.AreEqual(1, document.Body.ChildElementCount);
            Assert.AreEqual("div", document.Body.Children[0].GetTagName());
        }

        [Test]
        public void PassTwoElementsWithPrependNodes()
        {
            var document = String.Empty.ToHtmlDocument();
            var newDiv = document.CreateElement<IHtmlDivElement>();
            var newAnchor = document.CreateElement<IHtmlAnchorElement>();
            Assert.AreEqual(0, document.Body.ChildElementCount);
            document.Body.Prepend(newDiv, newAnchor);
            Assert.AreEqual(2, document.Body.ChildElementCount);
            Assert.AreEqual("div", document.Body.Children[0].GetTagName());
            Assert.AreEqual("a", document.Body.Children[1].GetTagName());
        }

        [Test]
        public void PassTwoElementsWithPrependNodesToNonEmptyElement()
        {
            var document = Create("<span></span>");
            var newDiv = document.CreateElement<IHtmlDivElement>();
            var newAnchor = document.CreateElement<IHtmlAnchorElement>();
            Assert.AreEqual(1, document.Body.ChildElementCount);
            document.Body.Prepend(newDiv, newAnchor);
            Assert.AreEqual(3, document.Body.ChildElementCount);
            Assert.AreEqual("div", document.Body.Children[0].GetTagName());
            Assert.AreEqual("a", document.Body.Children[1].GetTagName());
            Assert.AreEqual("span", document.Body.Children[2].GetTagName());
        }

        [Test]
        public void PassEmptyArrayToAppendNodes()
        {
            var document = String.Empty.ToHtmlDocument();
            Assert.AreEqual(0, document.Body.ChildElementCount);
            document.Body.Append();
            Assert.AreEqual(0, document.Body.ChildElementCount);
        }

        [Test]
        public void PassSingleElementWithAppendNodes()
        {
            var document = String.Empty.ToHtmlDocument();
            var newDiv = document.CreateElement<IHtmlDivElement>();
            Assert.AreEqual(0, document.Body.ChildElementCount);
            document.Body.Append(newDiv);
            Assert.AreEqual(1, document.Body.ChildElementCount);
            Assert.AreEqual("div", document.Body.Children[0].GetTagName());
        }

        [Test]
        public void PassTwoElementsWithAppendNodes()
        {
            var document = String.Empty.ToHtmlDocument();
            var newDiv = document.CreateElement<IHtmlDivElement>();
            var newAnchor = document.CreateElement<IHtmlAnchorElement>();
            Assert.AreEqual(0, document.Body.ChildElementCount);
            document.Body.Append(newDiv, newAnchor);
            Assert.AreEqual(2, document.Body.ChildElementCount);
            Assert.AreEqual("div", document.Body.Children[0].GetTagName());
            Assert.AreEqual("a", document.Body.Children[1].GetTagName());
        }

        [Test]
        public void PassTwoElementsWithAppendNodesToNonEmptyElement()
        {
            var document = Create("<span></span>");
            var newDiv = document.CreateElement<IHtmlDivElement>();
            var newAnchor = document.CreateElement<IHtmlAnchorElement>();
            Assert.AreEqual(1, document.Body.ChildElementCount);
            document.Body.Append(newDiv, newAnchor);
            Assert.AreEqual(3, document.Body.ChildElementCount);
            Assert.AreEqual("span", document.Body.Children[0].GetTagName());
            Assert.AreEqual("div", document.Body.Children[1].GetTagName());
            Assert.AreEqual("a", document.Body.Children[2].GetTagName());
        }

        [Test]
        public void ParentReplacementByCloneWithChildrenExpectedToHaveAParent()
        {
            var document = Create(@"
<html>
<body>
    <div class='parent'>
        <div class='child'>
        </div>
    </div>
</body>
</html>
");
            var originalParent = document.QuerySelector(".parent");

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
            var document = Create(@"
<html>
<body>
    <div class='parent'>
        <div class='child'>
        </div>
    </div>
</body>
</html>
");
            var originalParent = document.QuerySelector(".parent");

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
            var document = Create(@"
<html>
<body>
    <div class='parent'>
        <div class='child'>
        </div>
    </div>
</body>
</html>
");
            var divOne = document.QuerySelector(".parent");
            var divTwo = document.Body.FirstElementChild;
            var divThree = document.QuerySelectorAll("div")[0];

            Assert.AreEqual(divOne, divThree);
            Assert.AreEqual(divTwo, divThree);

            Assert.IsTrue(divOne.Equals(divTwo));
            Assert.IsTrue(divOne.Equals(divThree));
            Assert.IsTrue(divTwo.Equals(divThree));
        }

        [Test]
        public void IsEqualNodesWithClonedNode()
        {
            var document = Create(@"
<html>
<body>
    <div class='parent'>
        <div class='child'>
        </div>
    </div>
</body>
</html>
");
            var original = document.QuerySelector(".parent");
            var clone = original.Clone();

            Assert.AreNotSame(original, clone);
            Assert.IsTrue(original.Equals(clone));
            Assert.IsFalse(original.Equals(document.Body));
        }

        [Test]
        public void ContainsWithChildNodes()
        {
            var document = Create(@"
<html>
<body>
    <div class='parent'>
        <div class='child'>
            <div class='grandchild'></div>
        </div>
    </div>
</body>
</html>
");
            var parent = document.QuerySelector(".parent");
            var child = document.QuerySelector(".child");
            var grandchild = document.QuerySelector(".grandchild");

            Assert.IsFalse(parent.Contains(document.Body));
            Assert.IsTrue(parent.Contains(parent));
            Assert.IsTrue(parent.Contains(child));
            Assert.IsTrue(parent.Contains(grandchild));
        }

        [Test]
        public void ReturnTextFromBody()
        {
            var test = "Some text";
            var html = String.Format(@"
<html>
<body>{0}</body></html>", test);
            var document = Create(html);
            Assert.AreEqual(test, document.Body.TextContent);
            Assert.AreEqual(test, document.Body.Text());
            Assert.AreEqual(test, document.Body.ChildNodes[0].TextContent);

            var text = document.Body.ChildNodes[0] as TextNode;
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
            var test = String.Concat(test1, test2, test3);
            var html = @"
<html>
<body></body></html>";
            var document = Create(html);
            var text1 = document.CreateTextNode(test1);
            var text2 = document.CreateTextNode(test2);
            var text3 = document.CreateTextNode(test3);
            document.Body.Append(text1);
            document.Body.Append(text2);
            document.Body.Append(text3);
            Assert.AreEqual(test, document.Body.TextContent);
            Assert.AreEqual(test, document.Body.Text());
            Assert.AreEqual(test1, document.Body.ChildNodes[0].TextContent);

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
            var document = Create("<table><tr></tr><tr></tr></table>");
            var table = document.QuerySelector("table") as IHtmlTableElement;

            Assert.IsNotNull(table);
            Assert.AreEqual(2, table.Rows.Length);
            Assert.AreEqual(0, table.Rows[0].Cells.Length);
            Assert.AreEqual(0, table.Rows[1].Cells.Length);
        }

        [Test]
        public void GetRowsFromTableWithNesting()
        {
            var html = @"<table id=first><tr></tr><tr><td><table id=second><tr></tr></table></td></tr></table>";
            var document = Create(html);
            var first = document.QuerySelector("#first") as IHtmlTableElement;
            var second = document.QuerySelector("#second") as IHtmlTableElement;

            Assert.IsNotNull(first);
            Assert.IsNotNull(second);

            Assert.AreEqual(2, first.Rows.Length);
            Assert.AreEqual(0, first.Rows[0].Cells.Length);
            Assert.AreEqual(1, first.Rows[1].Cells.Length);
            Assert.AreEqual(1, second.Rows.Length);
            Assert.AreEqual(0, second.Rows[0].Cells.Length);
        }

        [Test]
        public void PlainOutputElement()
        {
            var document = String.Empty.ToHtmlDocument();
            var output = document.CreateElement<IHtmlOutputElement>();
            Assert.AreEqual("output", output.Type);
            Assert.AreEqual("", output.TextContent);
            Assert.AreEqual("", output.Value);
            Assert.AreEqual("", output.DefaultValue);
        }

        [Test]
        public void OutputElementWithTextContent()
        {
            var document = String.Empty.ToHtmlDocument();
            var output = document.CreateElement<IHtmlOutputElement>();
            output.TextContent = "5";
            Assert.AreEqual("output", output.Type);
            Assert.AreEqual("5", output.TextContent);
            Assert.AreEqual("5", output.Value);
            Assert.AreEqual("5", output.DefaultValue);
        }

        [Test]
        public void OutputElementWithDefaultValueOverridesTextContent()
        {
            var document = String.Empty.ToHtmlDocument();
            var output = document.CreateElement<IHtmlOutputElement>();
            output.TextContent = "5";
            output.DefaultValue = "10";
            Assert.AreEqual("output", output.Type);
            Assert.AreEqual("10", output.TextContent);
            Assert.AreEqual("10", output.Value);
            Assert.AreEqual("10", output.DefaultValue);
        }

        [Test]
        public void OutputElementWithCustomValueOverridesDefaultValue()
        {
            var document = String.Empty.ToHtmlDocument();
            var output = document.CreateElement<IHtmlOutputElement>();
            output.TextContent = "5";
            output.DefaultValue = "10";
            output.Value = "20";
            Assert.AreEqual("output", output.Type);
            Assert.AreEqual("20", output.TextContent);
            Assert.AreEqual("20", output.Value);
            Assert.AreEqual("10", output.DefaultValue);
        }

        [Test]
        public void OutputElementWithCustomValueAndDefaultValue()
        {
            var document = String.Empty.ToHtmlDocument();
            var output = document.CreateElement<IHtmlOutputElement>();
            output.TextContent = "5";
            output.DefaultValue = "10";
            output.Value = "20";
            output.DefaultValue = "15";
            Assert.AreEqual("output", output.Type);
            Assert.AreEqual("20", output.TextContent);
            Assert.AreEqual("20", output.Value);
            Assert.AreEqual("15", output.DefaultValue);
        }

        [Test]
        public void OptionWithId()
        {
            var document = Create(@"<select>
  <option id=op1>A</option>
  <option name=op2>B</option>
  <option id=op3 name=op4>C</option>
  <option id=>D</option>
  <option name=>D</option>
</select>");
            var select = document.QuerySelector("select") as IHtmlSelectElement;
            Assert.AreEqual(select.Options[0], select.Options["op1"]);
        }

        [Test]
        public void OptionWithName()
        {
            var document = Create(@"<select>
  <option id=op1>A</option>
  <option name=op2>B</option>
  <option id=op3 name=op4>C</option>
  <option id=>D</option>
  <option name=>D</option>
</select>");
            var select = document.QuerySelector("select") as IHtmlSelectElement;
            Assert.AreEqual(select.Options[1], select.Options["op2"]);
        }

        [Test]
        public void OptionWithNameAndId()
        {
            var document = Create(@"<select>
  <option id=op1>A</option>
  <option name=op2>B</option>
  <option id=op3 name=op4>C</option>
  <option id=>D</option>
  <option name=>D</option>
</select>");
            var select = document.QuerySelector("select") as IHtmlSelectElement;
            Assert.AreEqual(select.Options[2], select.Options["op3"]);
            Assert.AreEqual(select.Options[2], select.Options["op4"]);
        }

        [Test]
        public void OptionEmptyStringName()
        {
            var document = Create(@"<select>
  <option id=op1>A</option>
  <option name=op2>B</option>
  <option id=op3 name=op4>C</option>
  <option id=>D</option>
  <option name=>D</option>
</select>");
            var select = document.QuerySelector("select") as IHtmlSelectElement;
            Assert.AreEqual(null, select.Options[""]);
        }

        [Test]
        public void SelectRemoveOptionShouldWork()
        {
            var document = String.Empty.ToHtmlDocument();
            var div = document.CreateElement<IHtmlDivElement>();
            var select = document.CreateElement<IHtmlSelectElement>();
            div.AppendChild(select);
            Assert.AreEqual(div, select.Parent);
            var options = new IHtmlOptionElement[3];

            for (int i = 0; i < options.Length; i++)
            {
                options[i] = document.CreateElement<IHtmlOptionElement>();
                options[i].TextContent = i.ToString();
                select.AppendChild(options[i]);
            }

            select.RemoveOptionAt(-1);
            CollectionAssert.AreEqual(options, select.Options);

            select.RemoveOptionAt(3);
            CollectionAssert.AreEqual(options, select.Options);

            select.RemoveOptionAt(0);
            CollectionAssert.AreEqual(new[] { options[1], options[2] }, select.Options);
        }

        [Test]
        public void SelectOptionsRemoveOptionShouldWork()
        {
            var document = String.Empty.ToHtmlDocument();
            var div = document.CreateElement<IHtmlDivElement>();
            var select = document.CreateElement<IHtmlSelectElement>();
            div.AppendChild(select);
            Assert.AreEqual(div, select.Parent);
            var options = new IHtmlOptionElement[3];

            for (int i = 0; i < options.Length; i++)
            {
                options[i] = document.CreateElement<IHtmlOptionElement>();
                options[i].TextContent = i.ToString();
                select.AppendChild(options[i]);
            }

            select.Options.Remove(-1);
            CollectionAssert.AreEqual(options, select.Options);

            select.Options.Remove(3);
            CollectionAssert.AreEqual(options, select.Options);

            select.Options.Remove(0);
            CollectionAssert.AreEqual(new[] { options[1], options[2] }, select.Options);
        }

        [Test]
        public void RemoveShouldWorkOnSelectElements()
        {
            var document = String.Empty.ToHtmlDocument();
            var div = document.CreateElement<IHtmlDivElement>();
            var select = document.CreateElement<IHtmlSelectElement>();
            div.AppendChild(select);
            Assert.AreEqual(div, select.Parent);
            Assert.AreEqual(select, div.FirstChild);
            select.Remove();
            Assert.AreEqual(null, select.Parent);
            Assert.AreEqual(null, div.FirstChild);
        }

        [Test]
        public void HtmlTagShouldBeEqualToNodeNameAndUppercase()
        {
            var document = String.Empty.ToHtmlDocument();
            var div = document.CreateElement("div");
            Assert.AreEqual("div", div.LocalName);
            Assert.IsNull(div.Prefix);
            Assert.AreEqual("DIV", div.NodeName);
            Assert.AreEqual("DIV", div.TagName);
            Assert.AreEqual(NamespaceNames.HtmlUri, div.NamespaceUri);
        }

        [Test]
        public void XmlTagShouldBeEqualToNodeNameAndPreserveCase()
        {
            var document = String.Empty.ToHtmlDocument();
            var myTag = document.CreateElement(NamespaceNames.XmlUri, "xml:myTag");
            Assert.AreEqual("myTag", myTag.LocalName);
            Assert.AreEqual("xml", myTag.Prefix);
            Assert.AreEqual("xml:myTag", myTag.NodeName);
            Assert.AreEqual("xml:myTag", myTag.TagName);
            Assert.AreEqual(NamespaceNames.XmlUri, myTag.NamespaceUri);
        }

        [Test]
        public void SvgTagShouldBeEqualToNodeNameAndLowercase()
        {
            var document = String.Empty.ToHtmlDocument();
            var title = document.CreateElement(NamespaceNames.SvgUri, "title");
            Assert.AreEqual("title", title.LocalName);
            Assert.IsNull(title.Prefix);
            Assert.AreEqual("title", title.NodeName);
            Assert.AreEqual("title", title.TagName);
            Assert.AreEqual(NamespaceNames.SvgUri, title.NamespaceUri);
        }

        [Test]
        public void MathMlTagShouldBeEqualToNodeNameAndLowercase()
        {
            var document = String.Empty.ToHtmlDocument();
            var mi = document.CreateElement(NamespaceNames.MathMlUri, "mi");
            Assert.AreEqual("mi", mi.LocalName);
            Assert.IsNull(mi.Prefix);
            Assert.AreEqual("mi", mi.NodeName);
            Assert.AreEqual("mi", mi.TagName);
            Assert.AreEqual(NamespaceNames.MathMlUri, mi.NamespaceUri);
        }

        [Test]
        public void CustomTagShouldBeEqualToNodeNameAndPreserveCase()
        {
            var document = String.Empty.ToHtmlDocument();
            var element = document.CreateElement("", "fooBar");
            Assert.AreEqual("fooBar", element.LocalName);
            Assert.IsNull(element.Prefix);
            Assert.AreEqual("fooBar", element.NodeName);
            Assert.AreEqual("fooBar", element.TagName);
            Assert.IsNull(element.NamespaceUri);
        }

        [Test]
        public void TheTypeAttributeMustReturnFieldset()
        {
            var source = (@"<form name=fm1>
  <fieldset id=fs_outer>
  <legend><input type=""checkbox"" name=""cb""></legend>
  <input type=text name=""txt"" id=""ctl1"">
  <button id=""ctl2"" name=""btn"">BUTTON</button>
    <fieldset id=fs_inner>
      <input type=text name=""txt_inner"">
      <progress name=""pg"" value=""0.5""></progress>
    </fieldset>
  </fieldset>
</form>");
            var document = source.ToHtmlDocument();
            var fm1 = document.Forms["fm1"];
            var fs_outer = document.GetElementById("fs_outer") as IHtmlFieldSetElement;
            var children_outer = fs_outer.Elements;
            Assert.AreEqual("fieldset", fs_outer.Type);
        }

        [Test]
        public void TheFormAttributeMustReturnTheFieldsetsFormOwner()
        {
            var source = (@"<form name=fm1>
  <fieldset id=fs_outer>
  <legend><input type=""checkbox"" name=""cb""></legend>
  <input type=text name=""txt"" id=""ctl1"">
  <button id=""ctl2"" name=""btn"">BUTTON</button>
    <fieldset id=fs_inner>
      <input type=text name=""txt_inner"">
      <progress name=""pg"" value=""0.5""></progress>
    </fieldset>
  </fieldset>
</form>");
            var document = source.ToHtmlDocument();
            var fm1 = document.Forms["fm1"];
            var fs_outer = document.GetElementById("fs_outer") as IHtmlFieldSetElement;
            var children_outer = fs_outer.Elements;
            Assert.AreEqual(fm1, fs_outer.Form);
        }

        [Test]
        public void TheElementsMustReturnAnHtmlFormControlsCollectionObject()
        {
            var source = (@"<form name=fm1>
  <fieldset id=fs_outer>
  <legend><input type=""checkbox"" name=""cb""></legend>
  <input type=text name=""txt"" id=""ctl1"">
  <button id=""ctl2"" name=""btn"">BUTTON</button>
    <fieldset id=fs_inner>
      <input type=text name=""txt_inner"">
      <progress name=""pg"" value=""0.5""></progress>
    </fieldset>
  </fieldset>
</form>");
            var document = source.ToHtmlDocument();
            var fm1 = document.Forms["fm1"];
            var fs_outer = document.GetElementById("fs_outer") as IHtmlFieldSetElement;
            var children_outer = fs_outer.Elements;
            Assert.IsInstanceOf<IHtmlFormControlsCollection>(children_outer);
            Assert.IsNotNull(children_outer);
        }

        [Test]
        public void TheControlsMustRootAtTheFieldsetElement()
        {
            var source = (@"<form name=fm1>
  <fieldset id=fs_outer>
  <legend><input type=""checkbox"" name=""cb""></legend>
  <input type=text name=""txt"" id=""ctl1"">
  <button id=""ctl2"" name=""btn"">BUTTON</button>
    <fieldset id=fs_inner>
      <input type=text name=""txt_inner"">
      <progress name=""pg"" value=""0.5""></progress>
    </fieldset>
  </fieldset>
</form>");
            var document = source.ToHtmlDocument();
            var fm1 = document.Forms["fm1"];
            var fs_outer = document.GetElementById("fs_outer") as IHtmlFieldSetElement;
            var children_outer = fs_outer.Elements;
            var fs_inner = document.GetElementById("fs_inner") as IHtmlFieldSetElement;
            var children_inner = fs_inner.Elements;
            CollectionAssert.AreEqual(new[] { fm1["txt_inner"] as IHtmlElement }, children_inner.ToArray());
            CollectionAssert.AreEqual(new[] { fm1["cb"], fm1["txt"], fm1["btn"], fm1["fs_inner"], fm1["txt_inner"] }.OfType<IHtmlElement>().ToArray(), children_outer.ToArray());
        }

        [Test]
        public void TheDisabledAttributeCausesAllFormControlDescendantsOfTheFieldsetElementToBeDisabled()
        {
            var source = (@"<form>
  <fieldset disabled>
    <legend>
      <input type=checkbox id=clubc_l1>
      <input type=radio id=clubr_l1>
      <input type=text id=clubt_l1>
    </legend>
    <legend><input type=checkbox id=club_l2></legend>
    <p><label>Name on card: <input id=clubname required></label></p>
    <p><label>Card number: <input id=clubnum required pattern=""[-0-9]+""></label></p>
  </fieldset>
</form>");
            var document = source.ToHtmlDocument();
            Assert.IsTrue(document.QuerySelector<IHtmlFieldSetElement>("fieldset").IsDisabled);
            Assert.IsFalse(document.QuerySelector<IHtmlInputElement>("#clubname").WillValidate);
            Assert.IsFalse(document.QuerySelector<IHtmlInputElement>("#clubnum").WillValidate);
            Assert.IsTrue(document.QuerySelector<IHtmlInputElement>("#clubc_l1").WillValidate);
            Assert.IsTrue(document.QuerySelector<IHtmlInputElement>("#clubr_l1").WillValidate);
            Assert.IsTrue(document.QuerySelector<IHtmlInputElement>("#clubt_l1").WillValidate);
            Assert.IsFalse(document.QuerySelector<IHtmlInputElement>("#club_l2").WillValidate);
        }

        [Test]
        public void TheFirstLegendElementIsNotAChildOfTheDisabledFieldsetDescendantsShouldBeDisabled()
        {
            var source = (@"<form>
  <fieldset disabled>
    <p><legend><input type=checkbox id=club2></legend></p>
    <p><label>Name on card: <input id=clubname2 required></label></p>
    <p><label>Card number: <input id=clubnum2 required pattern=""[-0-9]+""></label></p>
  </fieldset>
</form>");
            var document = source.ToHtmlDocument();
            Assert.IsTrue(document.QuerySelector<IHtmlFieldSetElement>("fieldset").IsDisabled);
            Assert.IsFalse(document.QuerySelector<IHtmlInputElement>("#clubname2").WillValidate);
            Assert.IsFalse(document.QuerySelector<IHtmlInputElement>("#clubnum2").WillValidate);
            Assert.IsFalse(document.QuerySelector<IHtmlInputElement>("#club2").WillValidate);
        }

        [Test]
        public void TheLegendElementIsNotAChildOfTheDisabledFieldsetDescendantsShouldBeDisabled()
        {
            var source = @"<form>
  <fieldset disabled>
    <fieldset>
      <legend><input type=checkbox id=club3></legend>
    </fieldset>
    <p><label>Name on card: <input id=clubname3 required></label></p>
    <p><label>Card number: <input id=clubnum3 required pattern=""[-0-9]+""></label></p>
  </fieldset>
</form>";
            var document = source.ToHtmlDocument();
            Assert.IsTrue(document.QuerySelector<IHtmlFieldSetElement>("fieldset").IsDisabled);
            Assert.IsFalse(document.QuerySelector<IHtmlInputElement>("#clubname3").WillValidate);
            Assert.IsFalse(document.QuerySelector<IHtmlInputElement>("#clubnum3").WillValidate);
            Assert.IsFalse(document.QuerySelector<IHtmlInputElement>("#club3").WillValidate);
        }

        [Test]
        public void TheLegendElementIsChildOfTheDisabledFieldsetDescendantsShouldNotBeDisabled()
        {
            var source = (@"<form>
  <fieldset disabled>
    <legend>
      <fieldset><input type=checkbox id=club4></fieldset>
    </legend>
    <p><label>Name on card: <input id=clubname4 required></label></p>
    <p><label>Card number: <input id=clubnum4 required pattern=""[-0-9]+""></label></p>
  </fieldset>
</form>");
            var document = source.ToHtmlDocument();
            Assert.IsTrue(document.QuerySelector<IHtmlFieldSetElement>("fieldset").IsDisabled);
            Assert.IsFalse(document.QuerySelector<IHtmlInputElement>("#clubname4").WillValidate);
            Assert.IsFalse(document.QuerySelector<IHtmlInputElement>("#clubnum4").WillValidate);
            Assert.IsTrue(document.QuerySelector<IHtmlInputElement>("#club4").WillValidate);
        }

        [Test]
        public async Task IframeWithDocumentViaDocSrc()
        {
            var cfg = Configuration.Default.WithDefaultLoader(new LoaderOptions { IsResourceLoadingEnabled = true });
            var html = @"<!doctype html><iframe id=myframe srcdoc='<span>Hello World!</span>'></iframe></script>";
            var document = await BrowsingContext.New(cfg).OpenAsync(m => m.Content(html));
            var iframe = document.QuerySelector<IHtmlInlineFrameElement>("#myframe");
            Assert.IsNotNull(iframe);
            Assert.IsNotNull(iframe.ContentDocument);
            Assert.AreEqual("Hello World!", iframe.ContentDocument.Body.TextContent);
            Assert.AreEqual(iframe.ContentDocument, iframe.ContentWindow.Document);
        }

        [Test]
        public async Task IframeWithDocumentPreferDocSrcToDataSrc()
        {
            var cfg = Configuration.Default.WithDefaultLoader(new LoaderOptions { IsResourceLoadingEnabled = true });
            var html = @"<!doctype html><iframe id=myframe srcdoc='Green' src='data:text/html,Red'></iframe></script>";
            var document = await BrowsingContext.New(cfg).OpenAsync(m => m.Content(html));
            var iframe = document.QuerySelector<IHtmlInlineFrameElement>("#myframe");
            Assert.IsNotNull(iframe);
            Assert.IsNotNull(iframe.ContentDocument);
            Assert.AreEqual("Green", iframe.ContentDocument.Body.TextContent);
            Assert.AreEqual(iframe.ContentDocument, iframe.ContentWindow.Document);
        }

        [Test]
        public async Task WindowTimeoutIsWorkingOnce()
        {
            var cfg = Configuration.Default;
            var count = 0;
            var document = await BrowsingContext.New(cfg).OpenNewAsync();
            document.DefaultView.SetTimeout(window => count++, 10);
            await Task.Delay(100);
            Assert.AreEqual(1, count);
        }

        [Test]
        public async Task WindowTimeoutCanBeCancelled()
        {
            var cfg = Configuration.Default;
            var count = 0;
            var document = await BrowsingContext.New(cfg).OpenNewAsync();
            var id = document.DefaultView.SetTimeout(window => count++, 10);
            document.DefaultView.ClearTimeout(id);
            await Task.Delay(100);
            Assert.AreEqual(0, count);
        }

        [Test]
        public async Task WindowIntervalIsWorkingMultipleTimes()
        {
            var cfg = Configuration.Default;
            var count = 0;
            var document = await BrowsingContext.New(cfg).OpenNewAsync();
            var id = document.DefaultView.SetInterval(window => count++, 10);
            await Task.Delay(100);
            Assert.Greater(count, 1);
            document.DefaultView.ClearInterval(id);
        }

        [Test]
        public async Task WindowIntervalCanBeCancelled()
        {
            var cfg = Configuration.Default;
            var count = 0;
            var document = await BrowsingContext.New(cfg).OpenNewAsync();
            var id = document.DefaultView.SetInterval(window => count++, 10);
            document.DefaultView.ClearInterval(id);
            await Task.Delay(100);
            Assert.AreEqual(0, count);
        }

        [Test]
        public async Task ElementInsertBeforeBeginPrependsNewElement()
        {
            var source = "<div></div>";
            var cfg = Configuration.Default;
            var document = await BrowsingContext.New(cfg).OpenAsync(res => res.Content(source));
            var div = document.QuerySelector("div");

            Assert.IsNotNull(div);

            div.Insert(AdjacentPosition.BeforeBegin, "<span>Test</span>");

            var span = document.QuerySelector("span");

            Assert.IsNotNull(span);
            Assert.AreEqual("Test", span.TextContent);
            Assert.AreEqual(div, span.NextElementSibling);
        }

        [Test]
        public async Task ElementInsertAfterEndAppendsNewElement()
        {
            var source = "<div></div>";
            var cfg = Configuration.Default;
            var document = await BrowsingContext.New(cfg).OpenAsync(res => res.Content(source));
            var div = document.QuerySelector("div");

            Assert.IsNotNull(div);

            div.Insert(AdjacentPosition.AfterEnd, "<span>Test</span>");

            var span = document.QuerySelector("span");

            Assert.IsNotNull(span);
            Assert.AreEqual("Test", span.TextContent);
            Assert.AreEqual(div, span.PreviousElementSibling);
        }

        [Test]
        public async Task ElementInsertAfterBeginInsertsNewElement()
        {
            var source = "<div>After</div>";
            var cfg = Configuration.Default;
            var document = await BrowsingContext.New(cfg).OpenAsync(res => res.Content(source));
            var div = document.QuerySelector("div");

            Assert.IsNotNull(div);

            div.Insert(AdjacentPosition.AfterBegin, "<span>Test</span>");

            var span = document.QuerySelector("span");

            Assert.IsNotNull(span);
            Assert.AreEqual("TestAfter", div.TextContent);
            Assert.AreEqual(div, span.Parent);
        }

        [Test]
        public async Task ElementInsertBeforeEndInsertsNewElement()
        {
            var source = "<div>Before</div>";
            var cfg = Configuration.Default;
            var document = await BrowsingContext.New(cfg).OpenAsync(res => res.Content(source));
            var div = document.QuerySelector("div");

            Assert.IsNotNull(div);

            div.Insert(AdjacentPosition.BeforeEnd, "<span>Test</span>");

            var span = document.QuerySelector("span");

            Assert.IsNotNull(span);
            Assert.AreEqual("BeforeTest", div.TextContent);
            Assert.AreEqual(div, span.Parent);
        }

        [Test]
        public async Task TogglingOptionIsDisabledWorksAsExpected()
        {
            var source = "<option></option>";
            var cfg = Configuration.Default;
            var document = await BrowsingContext.New(cfg).OpenAsync(res => res.Content(source));
            var option = document.QuerySelector<IHtmlOptionElement>("option");

            Assert.IsFalse(option.IsDisabled);

            option.IsDisabled = true;

            Assert.IsTrue(option.IsDisabled);

            option.IsDisabled = false;

            Assert.IsFalse(option.IsDisabled);
            Assert.AreEqual(0, option.Attributes.Length);
        }

        [Test]
        public async Task TogglingOptionIsSelectedWorksAsExpected()
        {
            var source = "<option></option>";
            var cfg = Configuration.Default;
            var document = await BrowsingContext.New(cfg).OpenAsync(res => res.Content(source));
            var option = document.QuerySelector<IHtmlOptionElement>("option");

            Assert.IsFalse(option.IsSelected);

            option.IsSelected = true;

            Assert.IsTrue(option.IsSelected);

            option.IsSelected = false;

            Assert.IsFalse(option.IsSelected);
            Assert.AreEqual(0, option.Attributes.Length);
        }

        [Test]
        public async Task TogglingOptionIsDefaultSelectedWorksAsExpected()
        {
            var source = "<option></option>";
            var cfg = Configuration.Default;
            var document = await BrowsingContext.New(cfg).OpenAsync(res => res.Content(source));
            var option = document.QuerySelector<IHtmlOptionElement>("option");

            Assert.IsFalse(option.IsDefaultSelected);

            option.IsDefaultSelected = true;

            Assert.IsTrue(option.IsDefaultSelected);

            option.IsDefaultSelected = false;

            Assert.IsFalse(option.IsDefaultSelected);
            Assert.AreEqual(0, option.Attributes.Length);
        }

        [Test]
        public async Task TogglingAnchorIsTranslatedWorksAsExpected()
        {
            var source = "<a></a>";
            var cfg = Configuration.Default;
            var document = await BrowsingContext.New(cfg).OpenAsync(res => res.Content(source));
            var a = document.QuerySelector<IHtmlAnchorElement>("a");

            Assert.IsTrue(a.IsTranslated);

            a.IsTranslated = false;

            Assert.IsFalse(a.IsTranslated);

            a.IsTranslated = true;

            Assert.IsTrue(a.IsTranslated);
        }

        [Test]
        public async Task TogglingDivIsDraggableWorksAsExpected()
        {
            var source = "<div></div>";
            var cfg = Configuration.Default;
            var document = await BrowsingContext.New(cfg).OpenAsync(res => res.Content(source));
            var div = document.QuerySelector<IHtmlElement>("div");

            Assert.IsFalse(div.IsDraggable);

            div.IsDraggable = true;

            Assert.IsTrue(div.IsDraggable);

            div.IsDraggable = false;

            Assert.IsFalse(div.IsDraggable);
        }

        [Test]
        public async Task TogglingDivIsSpellCheckedWorksAsExpected()
        {
            var source = "<div></div>";
            var cfg = Configuration.Default;
            var document = await BrowsingContext.New(cfg).OpenAsync(res => res.Content(source));
            var div = document.QuerySelector<IHtmlElement>("div");

            Assert.IsFalse(div.IsSpellChecked);

            div.IsSpellChecked = true;

            Assert.IsTrue(div.IsSpellChecked);

            div.IsSpellChecked = false;

            Assert.IsFalse(div.IsSpellChecked);
        }

        [Test]
        public async Task TogglingSpanIsHiddenCheckedWorksAsExpected()
        {
            var source = "<span></span>";
            var cfg = Configuration.Default;
            var document = await BrowsingContext.New(cfg).OpenAsync(res => res.Content(source));
            var span = document.QuerySelector<IHtmlElement>("span");

            Assert.IsFalse(span.IsHidden);

            span.IsHidden = true;

            Assert.IsTrue(span.IsHidden);

            span.IsHidden = false;

            Assert.IsFalse(span.IsHidden);
            Assert.AreEqual(0, span.Attributes.Length);
        }

        [Test]
        public async Task TogglingDetailsIsOpenCheckedWorksAsExpected()
        {
            var source = "<details></details>";
            var cfg = Configuration.Default;
            var document = await BrowsingContext.New(cfg).OpenAsync(res => res.Content(source));
            var details = document.QuerySelector<IHtmlDetailsElement>("details");

            Assert.IsFalse(details.IsOpen);

            details.IsOpen = true;

            Assert.IsTrue(details.IsOpen);

            details.IsOpen = false;

            Assert.IsFalse(details.IsOpen);
            Assert.AreEqual(0, details.Attributes.Length);
        }

        [Test]
        public async Task TogglingInputAutoFocusWorksAsExpected()
        {
            var source = "<input></input>";
            var cfg = Configuration.Default;
            var document = await BrowsingContext.New(cfg).OpenAsync(res => res.Content(source));
            var input = document.QuerySelector<IHtmlInputElement>("input");

            Assert.IsFalse(input.Autofocus);

            input.Autofocus = true;

            Assert.IsTrue(input.Autofocus);

            input.Autofocus = false;

            Assert.IsFalse(input.Autofocus);
            Assert.AreEqual(0, input.Attributes.Length);
        }

        [Test]
        public async Task TogglingInputFormNoValidateWorksAsExpected()
        {
            var source = "<form><input></input></form>";
            var cfg = Configuration.Default;
            var document = await BrowsingContext.New(cfg).OpenAsync(res => res.Content(source));
            var input = document.QuerySelector<IHtmlInputElement>("input");

            Assert.IsFalse(input.FormNoValidate);

            input.FormNoValidate = true;

            Assert.IsTrue(input.FormNoValidate);

            input.FormNoValidate = false;

            Assert.IsFalse(input.FormNoValidate);
        }

        [Test]
        public void InsertAdjacentHtmlRequiresParentForBeforeBegin()
        {
            var document = CreateEmpty(String.Empty);
            var div = document.CreateElement("div") as IElement;
            Assert.Catch<DomException>(() =>
            {
                div.Insert(AdjacentPosition.BeforeBegin, "<span></span>");
            });
        }

        [Test]
        public void InsertAdjacentHtmlWorksWithSpanElement()
        {
            var document = CreateEmpty(String.Empty);
            var div = document.CreateElement("div");
            var x = div.AppendChild(document.CreateElement("span")) as IElement;
            x.Insert(AdjacentPosition.AfterEnd, "<p class=\"cls\">Text</p>");
            Assert.AreEqual("<span></span><p class=\"cls\">Text</p>", div.InnerHtml);
        }

        [Test]
        public void InsertAdjacentHtmlWorksWithSelectElement()
        {
            var document = CreateEmpty(String.Empty);
            var div = document.CreateElement("div");
            var x = div.AppendChild(document.CreateElement("select")) as IElement;
            x.Insert(AdjacentPosition.AfterEnd, "<p class=\"cls\">Text</p>");
            Assert.AreEqual("<select></select><p class=\"cls\">Text</p>", div.InnerHtml);
        }

        [Test]
        public void CloningDocumentAlsoAdoptsClonedChildren()
        {
            var source = "<div>document</div>";
            var originalDocument = source.ToHtmlDocument();
            var newDocument = (IDocument)originalDocument.Clone(true);

            var div = newDocument.QuerySelector("div");
            Assert.AreSame(newDocument, div.Owner);

            div.TextContent = "cloned document";
            var newHtml = div.Owner.DocumentElement.OuterHtml;
            Assert.True(newHtml.Contains("cloned document"));
        }

        [Test]
        public void CloningBodyDoesNotAdoptsClonedChildren()
        {
            var source = "<div>document</div>";
            var originalDocument = source.ToHtmlDocument();
            var newBody = (IElement)originalDocument.Body.Clone(true);

            var div = newBody.QuerySelector("div");
            Assert.AreSame(originalDocument, div.Owner);

            div.TextContent = "cloned document";
            var newHtml = div.Owner.DocumentElement.OuterHtml;
            Assert.True(newHtml.Contains("document"));
        }

        [Test]
        public void ReplaceBodyNodeWithImportedNode()
        {
            var document = ("<html><body><div>abc</div></body></html>").ToHtmlDocument();
            var newdocument = String.Empty.ToHtmlDocument();
            var import = newdocument.Import(document.Body, true);

            newdocument.Body.Replace(import);
            Assert.AreEqual(import, newdocument.Body);
        }

        [Test]
        public void ReplaceBodyNodeWithClonedNode()
        {
            var document = ("<html><body><div>abc</div></body></html>").ToHtmlDocument();
            var clone = document.Body.Clone(false);

            document.Body.Replace(clone);
            Assert.AreEqual(clone, document.Body);
        }

        [Test]
        public void ReplaceBodyNodeWithImportedClonedNode()
        {
            var document = ("<html><body><div>abc</div></body></html>").ToHtmlDocument();
            var newdocument = String.Empty.ToHtmlDocument();
            var clone = document.Body.Clone(true);
            var import = newdocument.Import(clone, true);

            newdocument.Body.Replace(import);
            Assert.AreEqual(import, newdocument.Body);
        }

        [Test]
        public async Task SettingLinksBackRemainsRelative_Issue659()
        {
            var source = @"<a href=""/home.htm"">Foo</a>";
            var cfg = Configuration.Default;
            var document = await BrowsingContext.New(cfg).OpenAsync(res => res.Content(source).Address("http://example.com"));
            var a = document.QuerySelector<IHtmlAnchorElement>("a");

            Assert.AreEqual("http://example.com/home.htm", a.Href);
            Assert.AreEqual("/home.htm", a.GetAttribute("href"));

            a.Href = "/foo.htm";

            Assert.AreEqual("http://example.com/foo.htm", a.Href);
            Assert.AreEqual("/foo.htm", a.GetAttribute("href"));
        }

        [Test]
        public void GetClosestAncestor()
        {
            var document = Create(@"<div id=""div1""><div id=""div2""><table><tr><td id=""exampletd""><div id=""div3""></div></td></tr><tr></tr></table></div></div>");
            var cell = document.QuerySelector("#exampletd") as IHtmlTableCellElement;

            Assert.IsNotNull(cell);
            Assert.AreEqual(cell.Closest("td"), cell);
            Assert.AreEqual(cell.Closest("a"), null);
            Assert.AreEqual(cell.Closest("div"), document.QuerySelector("#div2"));
        }

        [Test]
        public void OuterHtmlForFrame_Issue741()
        {
            var document = Create("<html><body><iframe src=\"https://google.com\"></iframe></body></html>");
            var iframe = document.QuerySelector("iframe");

            Assert.IsNotNull(iframe);

            iframe.OuterHtml = $"<div>{iframe.OuterHtml}</div>";

            var newContent = document.Body.InnerHtml;

            Assert.AreEqual("<div><iframe src=\"https://google.com\"></iframe></div>", newContent);
        }
    }
}
