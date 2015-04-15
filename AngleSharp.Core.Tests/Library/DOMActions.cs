namespace AngleSharp.Core.Tests.Library
{
    using System;
    using System.Linq;
    using AngleSharp.Dom;
    using AngleSharp.Dom.Html;
    using AngleSharp.Html;
    using AngleSharp.Linq;
    using NUnit.Framework;

    [TestFixture]
    public class DOMActions
    {
        static IDocument CreateEmpty(String url)
        {
            return BrowsingContext.New().OpenNewAsync(url).Result;
        }

        static IDocument Create(String source)
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
            Assert.IsNull(form.Elements[-1]);
            Assert.IsNull(form.Elements["-1"]);
            Assert.AreEqual(two, form.Elements[0]);
            Assert.AreEqual(othree, form.Elements[1]);
            Assert.IsNull(form.Elements[2]);
            Assert.AreEqual(two, form.Elements["2"]);
            Assert.IsNull(form.Elements[03]);
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

            Assert.AreNotEqual(original, clone);
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
            Assert.AreEqual(0, (table.Rows[0] as IHtmlTableRowElement).Cells.Length);
            Assert.AreEqual(0, (table.Rows[1] as IHtmlTableRowElement).Cells.Length);
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
            Assert.AreEqual(0, (first.Rows[0] as IHtmlTableRowElement).Cells.Length);
            Assert.AreEqual(1, (first.Rows[1] as IHtmlTableRowElement).Cells.Length);
            Assert.AreEqual(1, second.Rows.Length);
            Assert.AreEqual(0, (second.Rows[0] as IHtmlTableRowElement).Cells.Length);
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
            Assert.AreEqual(Namespaces.HtmlUri, div.NamespaceUri);
        }

        [Test]
        public void XmlTagShouldBeEqualToNodeNameAndPreserveCase()
        {
            var document = String.Empty.ToHtmlDocument();
            var myTag = document.CreateElement(Namespaces.XmlUri, "xml:myTag");
            Assert.AreEqual("myTag", myTag.LocalName);
            Assert.AreEqual("xml", myTag.Prefix);
            Assert.AreEqual("xml:myTag", myTag.NodeName);
            Assert.AreEqual("xml:myTag", myTag.TagName);
            Assert.AreEqual(Namespaces.XmlUri, myTag.NamespaceUri);
        }

        [Test]
        public void SvgTagShouldBeEqualToNodeNameAndLowercase()
        {
            var document = String.Empty.ToHtmlDocument();
            var title = document.CreateElement(Namespaces.SvgUri, "title");
            Assert.AreEqual("title", title.LocalName);
            Assert.IsNull(title.Prefix);
            Assert.AreEqual("title", title.NodeName);
            Assert.AreEqual("title", title.TagName);
            Assert.AreEqual(Namespaces.SvgUri, title.NamespaceUri);
        }

        [Test]
        public void MathMlTagShouldBeEqualToNodeNameAndLowercase()
        {
            var document = String.Empty.ToHtmlDocument();
            var mi = document.CreateElement(Namespaces.MathMlUri, "mi");
            Assert.AreEqual("mi", mi.LocalName);
            Assert.IsNull(mi.Prefix);
            Assert.AreEqual("mi", mi.NodeName);
            Assert.AreEqual("mi", mi.TagName);
            Assert.AreEqual(Namespaces.MathMlUri, mi.NamespaceUri);
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
    }
}
